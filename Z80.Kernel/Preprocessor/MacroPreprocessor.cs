using System;
using System.Collections.Generic;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Preprocessor
{
    internal class MacroPreprocessor
    {
        private class MacroParameter
        {
            public string ParameterName { get; set; }
            public string ParameterValue { get; set; }
        }

        private enum ProcessState
        {
            Init,
            Name,
            OperandName,
            OperandValue,
            End
        }

        public string Name { get; private set; }

        public IReadOnlyList<AssemblyRow> Body => m_Body;

        public string StatusMessage { get; private set; }

        public MacroPreprocessor(IReadOnlyList<string> includeDirectories, string fileName, Dictionary<string, string> defines)
        {
            m_Body = new List<AssemblyRow>();
            m_MacroLines = new List<string>();
            m_Parameters = new List<MacroParameter>();
            m_Defines = defines;
            m_IncludeDirectories = includeDirectories;
            m_FileName = fileName;
        }

        public bool PreprocessMacro(ref int rowIndex, ref int lineNumber, IReadOnlyList<string> programLines, PreprocessorRowTokenizer tokenizer)
        {
            try
            {
                if (tokenizer.Tokens.Count < 2)
                {
                    throw new Z80AssemblerException("A macro nevét meg kell adni!");
                }

                PreprocessMacroAndParameterNames(tokenizer.Tokens[1]);

                bool stopProcessing = false;
                while (!stopProcessing)
                {
                    lineNumber++;
                    if (++rowIndex == programLines.Count)
                    {
                        throw new Z80AssemblerException($"Az {tokenizer} utasítás nincs lezárva #ENDM utasítással!");
                    }

                    string line = programLines[rowIndex].ToUpper().TrimStart(' ', '\t');
                    if (line.StartsWith("#"))
                    {
                        tokenizer = new PreprocessorRowTokenizer(line);
                        tokenizer.TokenizeRow();
                        if (tokenizer.Tokens.Count == 0)
                        {
                            throw new Z80AssemblerException("Hibás preprocesszor utasítás!");
                        }

                        switch (tokenizer.Tokens[0])
                        {
                            case PreprocessorConstans.PreprocessorDirectives.Include:
                            case PreprocessorConstans.PreprocessorDirectives.Macro:
                                {
                                    throw new Z80AssemblerException("Makró definíció nem tartalmazhat #MACRO és #INCLUDE utasításokat!");
                                }
                            case PreprocessorConstans.PreprocessorDirectives.Endm:
                                {
                                    stopProcessing = true;
                                }
                                continue;
                            default:
                                {
                                    m_MacroLines.Add(line);
                                }
                                continue;
                        }
                    }

                    m_MacroLines.Add(line);
                }

                if (m_MacroLines.Count == 0)
                {
                    throw new Z80AssemblerException("Üres makró definíció!");
                }

                return true;
            }
            catch (Z80AssemblerException e)
            {
                StatusMessage = e.Message;
                return false;
            }

        }

        public bool Call(string macroCallString)
        {
            try
            {
                ProcessParameterValues(macroCallString);
                ResolveParametersInMacroLines();
                Z80Preprocessor preprocessor = new Z80Preprocessor(m_MacroLines, m_IncludeDirectories, m_FileName, m_Defines);
                if (!preprocessor.Preprocess())
                {
                    StatusMessage = preprocessor.StatusMessage;
                    return false;
                }

                m_Body.AddRange(preprocessor.PreprocessedProgramLines);
                return true;
            }
            catch (Z80AssemblerException e)
            {
                StatusMessage = e.Message;
                return false;
            }
        }

        private void ResolveParametersInMacroLines()
        {
            for (int macroLineIndex = 0; macroLineIndex < m_MacroLines.Count; macroLineIndex++)
            {
                m_MacroLines[macroLineIndex] = SwapParameterNameWithValue(m_MacroLines[macroLineIndex]);
            }
        }

        private string SwapParameterNameWithValue(string programLine)
        {
            string line = programLine;
            foreach (MacroParameter macroParameter in m_Parameters)
            {
                if (string.IsNullOrEmpty(macroParameter.ParameterValue))
                {
                    throw new Z80AssemblerException($"Helytelen makró hívás! A '{macroParameter.ParameterName}' paraméter nem kapott értéket a makró hívásakor!");
                }

                line = line.Replace(macroParameter.ParameterName, macroParameter.ParameterValue);
            }

            return line;
        }

        private void ProcessParameterValues(string macroCallString)
        {
            ProcessState stateMachine = ProcessState.Init;
            string token = string.Empty;
            int macroParameterIndex = 0;
            int currentCharIndex = -1;
            foreach (char c in macroCallString)
            {
                currentCharIndex++;
                switch (stateMachine)
                {
                    case ProcessState.Init:
                        {
                            if (char.IsLetter(c))
                            {
                                token += c;
                                stateMachine = ProcessState.Name;
                            }
                        }
                        continue;
                    case ProcessState.Name:
                        {
                            if (char.IsLetterOrDigit(c))
                            {
                                token += c;
                            }

                            else if (c == '(')
                            {
                                if (token == string.Empty)
                                {
                                    throw new Z80AssemblerException("Szintaxis hiba a makró hívásban! Hiányzó makró név!");
                                }

                                if (!Name.Equals(token))
                                {
                                    throw new Z80AssemblerException($"Helytelen makró hívás! A '{Name}' makró helyett a '{token}' makró került meghívásra!");
                                }

                                token = string.Empty;
                                stateMachine = ProcessState.OperandValue;
                            }

                            else if (c != ' ' && c != '\t')
                            {
                                throw new Z80AssemblerException($"Helytelen karakter:'{c}' a makró hívásakor!");
                            }
                        }
                        continue;
                    case ProcessState.OperandValue:
                        {
                            if ((c == ')' && currentCharIndex == macroCallString.Length - 1) || c == ',')
                            {
                                if (token == string.Empty)
                                {
                                    throw new Z80AssemblerException("Szintaxis hiba a makró hívásban! Üres paraméter érték!");
                                }

                                try
                                {
                                    m_Parameters[macroParameterIndex++].ParameterValue = token;
                                    token = string.Empty;
                                    if (currentCharIndex == macroCallString.Length - 1)
                                    {
                                        stateMachine = ProcessState.End;
                                    }
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    throw new Z80AssemblerException("Szintaxis hiba a makró hívásban!");
                                }

                            }
                            else
                            {
                                token += c;
                            }
                        }
                        continue;
                }
            }
            if (stateMachine == ProcessState.Name && !Name.Equals(token))
            {
                throw new Z80AssemblerException($"Helytelen makró hívás! A '{Name}' makró helyett a '{token}' makró került meghívásra!");
            }

            if (stateMachine == ProcessState.OperandValue)
            {
                throw new Z80AssemblerException("Szintaxis hiba a makró hívásban! Hiányzió ')' karakter!");
            }
        }

        private void PreprocessMacroAndParameterNames(string parameterString)
        {
            ProcessState stateMachine = ProcessState.Init;
            string token = string.Empty;
            foreach (char c in parameterString)
            {
                switch (stateMachine)
                {
                    case ProcessState.Init:
                        {
                            if (!char.IsLetter(c))
                            {
                                throw new Z80AssemblerException("A makró neve csak betűvel kezdődhet!");
                            }

                            token += c;
                            stateMachine = ProcessState.Name;
                        }
                        continue;
                    case ProcessState.Name:
                        {
                            if (char.IsLetterOrDigit(c) || c == '_')
                            {
                                token += c;
                            }

                            else if (c == '(')
                            {
                                if (token == string.Empty)
                                {
                                    throw new Z80AssemblerException("Szintaxis hiba a makró definícióban! Hiányzó makró név!");
                                }

                                Name = token;
                                token = string.Empty;
                                stateMachine = ProcessState.OperandName;
                            }

                            else if (c != ' ' && c != '\t')
                            {
                                throw new Z80AssemblerException($"Helytelen karakter:'{c}' a makró nevében!");
                            }
                        }
                        continue;
                    case ProcessState.OperandName:
                        {
                            if (c == ')' || c == ',')
                            {
                                if (token == string.Empty)
                                {
                                    throw new Z80AssemblerException("Szintaxis hiba a makró definícióban! Hiányzó paraméter név!");
                                }

                                m_Parameters.Add(new MacroParameter { ParameterName = token, ParameterValue = string.Empty });
                                token = string.Empty;
                                if (c == ')')
                                {
                                    stateMachine = ProcessState.End;
                                }
                            }
                            else if (char.IsLetterOrDigit(c) || c == '_')
                            {
                                token += c;
                            }
                            else if (c != ' ' && c != '\t')
                            {
                                throw new Z80AssemblerException($"Helytelen karakter: {c} a makró paraméter nevében!");
                            }

                        }
                        continue;
                }
            }

            if (stateMachine == ProcessState.Name && !string.IsNullOrEmpty(token))
            {
                Name = token;
            }

            if (stateMachine == ProcessState.OperandName)
            {
                throw new Z80AssemblerException("Szintaxis hiba a makró hívásban! Hiányzió ')' karakter!");
            }
        }

        private readonly List<string> m_MacroLines;
        private readonly List<MacroParameter> m_Parameters;
        private readonly List<AssemblyRow> m_Body;
        private readonly Dictionary<string, string> m_Defines;
        private readonly IReadOnlyList<string> m_IncludeDirectories;
        private readonly string m_FileName;
    }
}
