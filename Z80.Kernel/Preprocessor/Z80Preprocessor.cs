using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Preprocessor
{
    internal class Z80Preprocessor
    {
        public Z80Preprocessor(List<string> programLines, IReadOnlyList<string> includeDirectories, string filename = "", Dictionary<string, string> defines = null)
        {
            m_ProgramLines = programLines;
            m_PreprocessedProgramLines = new List<AssemblyRow>();
            m_StatusMessage = new StringBuilder();
            m_Defines = defines ?? new Dictionary<string, string>();
            m_FileName = filename;
            m_Macros = new Dictionary<string, MacroPreprocessor>();
            m_IncludeDirectories = includeDirectories;
            m_IncludedFiles = new Dictionary<string, string>();
            m_SkippedLineNumbers = new List<int>();
        }

        public IReadOnlyList<AssemblyRow> PreprocessedProgramLines => m_PreprocessedProgramLines;

        public IReadOnlyList<int> SkippedLineNumbers => m_SkippedLineNumbers;

        public string StatusMessage => m_StatusMessage.ToString();

        public static IReadOnlyList<string> SupportedInstructions => SSupportedInstructions;

        public int WrongLineNumber { get; private set; }

        public bool Preprocess()
        {
            int lineNumber = 0;
            int rowIndex = -1;
            while (++rowIndex < m_ProgramLines.Count)
            {
                try
                {
                    lineNumber++;
                    string line = m_ProgramLines[rowIndex].TrimStart(' ', '\t');
                    if (line.StartsWith("#"))
                    {
                        PreprocessorRowTokenizer tokenizer = new PreprocessorRowTokenizer(line.ToUpper());
                        tokenizer.TokenizeRow();
                        if (tokenizer.Tokens.Count == 0)
                        {
                            throw new Z80AssemblerException("Üres preprocesszor utasítás!");
                        }

                        switch (tokenizer.Tokens[0])
                        {
                            case PreprocessorConstans.PreprocessorDirectives.IfnDef:
                            case PreprocessorConstans.PreprocessorDirectives.IfDef:
                                {
                                    if (tokenizer.Tokens.Count < 2)
                                    {
                                        throw new Z80AssemblerException($"Az {tokenizer.Tokens[0]} utasításnak szüksége van operandusra!");
                                    }

                                    ProcessConditionalGroup(ref rowIndex, ref lineNumber, tokenizer);
                                }
                                break;
                            default:
                                InterpretPreprocessorDirective(ref rowIndex, ref lineNumber, tokenizer);
                                break;

                        }
                    }
                    else
                    {
                        PreprocessAssemblyRow(line, lineNumber);
                    }
                }
                catch (Z80AssemblerException exception)
                {
                    m_StatusMessage.AppendLine($"{exception.Message} Sor:{lineNumber} File:{m_FileName}");
                    WrongLineNumber = lineNumber;
                    return false;
                }
            }
            return true;
        }

        private void ProcessConditionalGroup(ref int rowIndex, ref int lineNumber, PreprocessorRowTokenizer tokenizer)
        {
            bool skipNextRow = tokenizer.Tokens[0] == PreprocessorConstans.PreprocessorDirectives.IfDef
                ? m_Defines.All(kvp => kvp.Key != tokenizer.Tokens[1])
                : m_Defines.Any(kvp => kvp.Key == tokenizer.Tokens[1]);

            bool stop = false;
            bool conditionContainsElse = false;
            while (!stop)
            {
                if (++rowIndex == m_ProgramLines.Count)
                {
                    throw new Z80AssemblerException($"Az {tokenizer} utasítás nincs lezárva #ENDIF utasítással!");
                }

                lineNumber++;
                string line = m_ProgramLines[rowIndex].TrimStart(' ', '\t');
                if (line.StartsWith("#"))
                {
                    tokenizer = new PreprocessorRowTokenizer(line.ToUpper());
                    tokenizer.TokenizeRow();
                    if (tokenizer.Tokens.Count == 0)
                    {
                        throw new Z80AssemblerException("Hibás preprocesszor utasítás!");
                    }

                    switch (tokenizer.Tokens[0])
                    {
                        case PreprocessorConstans.PreprocessorDirectives.Else:
                            {
                                if (conditionContainsElse)
                                {
                                    throw new Z80AssemblerException("Egy feltételes szekció csak egy #ELSE utasítást tartalmazhat!");
                                }

                                skipNextRow = !skipNextRow;
                                conditionContainsElse = true;
                            }
                            continue;
                        case PreprocessorConstans.PreprocessorDirectives.EndIf:
                            stop = true;
                            continue;
                        case PreprocessorConstans.PreprocessorDirectives.IfDef:
                        case PreprocessorConstans.PreprocessorDirectives.IfnDef:
                            ProcessConditionalGroup(ref rowIndex, ref lineNumber, tokenizer);
                            continue;
                        default:
                            {
                                if (!skipNextRow)
                                {
                                    InterpretPreprocessorDirective(ref rowIndex, ref lineNumber, tokenizer);
                                }
                                else
                                {
                                    m_SkippedLineNumbers.Add(lineNumber);
                                }
                            }
                            continue;
                    }
                }
                if (!skipNextRow)
                {
                    PreprocessAssemblyRow(line, lineNumber);
                }
                else
                {
                    m_SkippedLineNumbers.Add(lineNumber);
                }
            }
        }

        private string ApplyDefine(string programRow)
        {
            string line = programRow;
            foreach (KeyValuePair<string, string> keyValuePair in m_Defines)
            {
                line = Regex.Replace(line, keyValuePair.Key, keyValuePair.Value, RegexOptions.IgnoreCase);
            }

            return line;
        }

        private bool TryDetectMacroCall(string token, out MacroPreprocessor macroPreprocessor)
        {
            macroPreprocessor = null;
            string macroNameSearchString = string.Empty;
            foreach (char c in token)
            {
                macroNameSearchString += c;
                var searchString = macroNameSearchString;
                macroPreprocessor = m_Macros.Where(kvp => kvp.Key.Equals(searchString)).Select(kvp => kvp.Value)
                    .FirstOrDefault();
                if (macroPreprocessor != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void PreprocessAssemblyRow(string programRow, int lineNumber)
        {
            programRow = ApplyDefine(programRow);
            if (TryDetectMacroCall(programRow.ToUpper(), out var m))
            {
                if (!m.Call(programRow.ToUpper()))
                {
                    throw new Z80AssemblerException(m.StatusMessage);
                }

                m_PreprocessedProgramLines.AddRange(m.Body);
                return;
            }

            AssemblyRowTokenizer rowTokenizer = new AssemblyRowTokenizer(programRow);
            rowTokenizer.TokenizeRow();

            if (rowTokenizer.Tokens.Count > 0)
            {

                AssemblyRowInterpreter interpreter = new AssemblyRowInterpreter(rowTokenizer.Tokens);
                interpreter.InterpretRow();

                if (!string.IsNullOrEmpty(interpreter.InterpretedAssemblyRow.Label) &&
                    string.IsNullOrEmpty(interpreter.InterpretedAssemblyRow.Instruction.Mnemonic))
                {
                    throw new Z80AssemblerException($"Cimke nem állhat utasítás nélkül:{interpreter.InterpretedAssemblyRow.Label}");
                }

                interpreter.InterpretedAssemblyRow.RowNumber = lineNumber;

                m_PreprocessedProgramLines.Add(interpreter.InterpretedAssemblyRow);
            }
        }

        private void InterpretPreprocessorDirective(ref int rowIndex, ref int lineNumber, PreprocessorRowTokenizer tokenizer)
        {
            switch (tokenizer.Tokens[0])
            {
                case PreprocessorConstans.PreprocessorDirectives.Define:
                    PreprocessDefine(tokenizer);
                    break;
                case PreprocessorConstans.PreprocessorDirectives.Undef:
                    ProcessUndef(tokenizer);
                    break;
                case PreprocessorConstans.PreprocessorDirectives.Include:
                    ProcessInclude(tokenizer);
                    break;
                case PreprocessorConstans.PreprocessorDirectives.Macro:
                    ProcessMacro(ref rowIndex, ref lineNumber, tokenizer);
                    break;
                case PreprocessorConstans.PreprocessorDirectives.Else:
                    throw new Z80AssemblerException("Az #ELSE utasítást csak az #IFDEF/#IFNDEF és #ENDIF utasítások között lehet használni!");
                case PreprocessorConstans.PreprocessorDirectives.EndIf:
                    throw new Z80AssemblerException("Az #ENDIF utasítást #IFDEF vagy #IFNDEF utasításoknak kell megelőznie!");
                case PreprocessorConstans.PreprocessorDirectives.Endm:
                    throw new Z80AssemblerException("Az #ENDM utasítást a #MACRO utasításnak kell megelőznie!");
            }
        }

        private void ProcessMacro(ref int rowIndex, ref int lineNumber, PreprocessorRowTokenizer tokenizer)
        {
            MacroPreprocessor m = new MacroPreprocessor(m_IncludeDirectories, m_FileName, m_Defines);
            if (!m.PreprocessMacro(ref rowIndex, ref lineNumber, m_ProgramLines, tokenizer))
            {
                throw new Z80AssemblerException(m.StatusMessage);
            }

            if (!m_Macros.ContainsKey(m.Name))
            {
                m_Macros.Add(m.Name, m);
            }
            else
            {
                throw new Z80AssemblerException($"A '{m.Name}' nevű makró már definiálva van!");
            }
        }

        private void ProcessInclude(PreprocessorRowTokenizer tokenizer)
        {
            var includePreprocessor = new IncludePreprocessor(m_IncludeDirectories, m_IncludedFiles, m_FileName, m_Defines);
            if (!includePreprocessor.Preprocess(tokenizer))
            {
                throw new Z80AssemblerException(includePreprocessor.StatusMessage);
            }
            m_PreprocessedProgramLines.AddRange(includePreprocessor.PreprocessedProgramLines);
        }

        private void ProcessUndef(PreprocessorRowTokenizer tokenizer)
        {
            if (tokenizer.Tokens.Count < 2)
            {
                throw new Z80AssemblerException("Az #UNDEF utasítás nem állhat üresen! ");
            }

            if (m_Defines.ContainsKey(tokenizer.Tokens[1]))
            {
                m_Defines.Remove(tokenizer.Tokens[1]);
            }
        }

        private void PreprocessDefine(PreprocessorRowTokenizer tokenizer)
        {
            var definePreprocessor = new DefinePreprocessor(tokenizer);
            definePreprocessor.Preprocess();
            m_Defines[definePreprocessor.DefineName] = definePreprocessor.DefineValue ?? string.Empty;
        }


        private static List<string> SSupportedInstructions { get; } = new List<string>
        {
            PreprocessorConstans.PreprocessorDirectives.Include, PreprocessorConstans.PreprocessorDirectives.Define,
            PreprocessorConstans.PreprocessorDirectives.Else,PreprocessorConstans.PreprocessorDirectives.EndIf,
            PreprocessorConstans.PreprocessorDirectives.Endm,PreprocessorConstans.PreprocessorDirectives.IfDef,
            PreprocessorConstans.PreprocessorDirectives.IfnDef,PreprocessorConstans.PreprocessorDirectives.Macro,
            PreprocessorConstans.PreprocessorDirectives.Undef
        };

        private readonly List<string> m_ProgramLines;
        private readonly List<AssemblyRow> m_PreprocessedProgramLines;
        private readonly Dictionary<string, string> m_Defines;
        private readonly StringBuilder m_StatusMessage;
        private readonly string m_FileName;
        private readonly Dictionary<string, MacroPreprocessor> m_Macros;
        private readonly IReadOnlyList<string> m_IncludeDirectories;
        private readonly Dictionary<string, string> m_IncludedFiles;
        private readonly List<int> m_SkippedLineNumbers;
    }
}
