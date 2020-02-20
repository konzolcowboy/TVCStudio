using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z80.Kernel.Preprocessor;
using Z80.Kernel.Z80Assembler.AssemblerInstructions;
using Z80.Kernel.Z80Assembler.ExpressionHandling;
using Z80.Kernel.Z80Assembler.Z80Instructions;
using AssemblerConstans = Z80.Kernel.Z80Assembler.AssemblerInstructions.AssemblerConstans;

namespace Z80.Kernel.Z80Assembler
{
    public sealed class Z80Assembler
    {
        #region public members
        public List<AssemblyRow> InterPretedAssemblyRows { get; }

        public string StatusMessage => m_StatusMessage.ToString();

        public List<string> IncludeDirectories { get; }

        public ushort LocationCounter => AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value;

        public Z80Program AssembledProgram { get; }

        public int WrongLineNumber { get; private set; }

        public IReadOnlyList<int> SkippedLineNumbers => m_SkippedLineNumbers;

        public byte[] AssembledProgramBytes => AssembledProgram.ProgramBytes.ToArray();

        public Z80Assembler(List<string> programText, List<string> includeDirectories = null, string fileName = "")
        {
            InterPretedAssemblyRows = new List<AssemblyRow>();
            m_ProgramText = programText;
            m_StatusMessage = new StringBuilder();
            m_InstructionBuilderFactory = new Z80InstructionBuilderFactory();
            AssembledProgram = new Z80Program();
            m_CurrentProgramSection = null;
            m_FileName = fileName;
            WrongLineNumber = 0;
            IncludeDirectories = includeDirectories ?? new List<string> { @"C:\" };
            m_SkippedLineNumbers = new List<int>();
        }

        public bool BuildProgram()
        {
            if (!PreprocessRows())
            {
                return false;
            }
            if (!InterpretRowsAndSymbols())
            {
                return false;
            }
            if (!BuildAssemblyRows())
            {
                return false;
            }

            LinkProgram();
            return true;
        }

        private bool PreprocessRows()
        {
            Z80Preprocessor preprocessor = new Z80Preprocessor(m_ProgramText, IncludeDirectories, m_FileName);

            if (!preprocessor.Preprocess())
            {
                m_StatusMessage.AppendLine(preprocessor.StatusMessage);
                WrongLineNumber = preprocessor.WrongLineNumber;
                return false;
            }

            InterPretedAssemblyRows.AddRange(preprocessor.PreprocessedProgramLines);
            m_SkippedLineNumbers.AddRange(preprocessor.SkippedLineNumbers);
            return true;
        }

        #endregion

        private bool BuildProcessorInstruction(AssemblyRow row)
        {
            try
            {
                if (row.State == AssemblyRowState.Assembled)
                {
                    return true;
                }

                var operandresolver = new OperandResolver(row, AssembledProgram.SymbolTable);
                var resolvedOperands = operandresolver.Resolve();

                IZ80InstructionBuilder builder =
                    m_InstructionBuilderFactory.GetInstructionBuilder(row.Instruction.Mnemonic, resolvedOperands,
                        AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value);

                if (builder != null)
                {
                    builder.Build();
                    if (builder.InstructionBytes != null)
                    {
                        row.InstructionBytes.Clear();
                        row.InstructionBytes.AddRange(builder.InstructionBytes);
                        row.State = resolvedOperands.Any(o => o.State == OperandState.FutureSymbol) ? AssemblyRowState.ContainsFutureSymbol : AssemblyRowState.Assembled;
                        row.ClockCycles = builder.ClockCycles;
                    }
                }
            }
            catch (Z80AssemblerException exception)
            {
                m_StatusMessage.AppendLine($"{exception.Message} Sor:{row.RowNumber}");
                WrongLineNumber = row.RowNumber;
                return false;
            }
            return true;
        }

        private bool InterpretRowsAndSymbols()
        {
            foreach (AssemblyRow programRow in InterPretedAssemblyRows)
            {
                var rowNumber = programRow.RowNumber;
                try
                {
                    if (programRow.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.End)
                    {
                        return true;
                    }

                    if (!string.IsNullOrEmpty(programRow.Label))
                    {
                        if (AssembledProgram.SymbolTable.ContainsKey(programRow.Label))
                        {
                            throw new Z80AssemblerException($"A(z) {programRow.Label} szimbólum duplán lett definiálva!");
                        }

                        var newSymbol = new Symbol { Name = programRow.Label.ToUpper(), LineNumber = programRow.RowNumber };
                        AssembledProgram.SymbolTable.Add(newSymbol.Name, newSymbol);

                        if (programRow.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.Equ ||
                            programRow.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.EqualSymbol)
                        {
                            newSymbol.Type = SymbolType.Constant;
                        }
                    }
                }
                catch (Z80AssemblerException exception)
                {
                    m_StatusMessage.AppendLine($"{exception.Message} Sor:{rowNumber}");
                    WrongLineNumber = rowNumber;
                    return false;
                }
            }
            return true;
        }
        private bool ResolveAssemblerInstruction(AssemblyRow row)
        {
            try
            {
                if (row.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.Org)
                {
                    var parser = new OrgInstructionParser(row, AssembledProgram.SymbolTable);
                    var result = parser.Parse();
                    if (result.ResultCode == ParseResultCode.Error)
                    {
                        throw new Z80AssemblerException(result.Message);
                    }

                    if (result.ResultCode == ParseResultCode.Ok)
                    {
                        ushort orgOperandValue = result.ResultValue;
                        if (row.State != AssemblyRowState.Interpreted)
                        {
                            if (m_CurrentProgramSection == null)
                            {
                                m_CurrentProgramSection = new ProgramSection { ProgramStartAddress = orgOperandValue };
                            }
                            else
                            {
                                m_CurrentProgramSection.SectionLength =
                                    (ushort)(AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value -
                                              m_CurrentProgramSection.ProgramStartAddress);
                                AssembledProgram.ProgramSections.Add(m_CurrentProgramSection);
                                m_CurrentProgramSection = new ProgramSection { ProgramStartAddress = orgOperandValue };
                            }
                        }

                        AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value = orgOperandValue;
                        if (!string.IsNullOrEmpty(row.Label))
                        {
                            AssembledProgram.SymbolTable[row.Label] = new Symbol { Name = row.Label, Value = orgOperandValue, LineNumber = row.RowNumber };
                        }

                        row.State = AssemblyRowState.Interpreted;
                    }

                    return true;
                }

                if (row.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.End)
                {
                    if (row.State != AssemblyRowState.Interpreted)
                    {
                        if (m_CurrentProgramSection == null)
                        {
                            throw new Z80AssemblerException(" Hiányzó 'ORG' utasítás!");
                        }

                        m_CurrentProgramSection.SectionLength =
                            (ushort)(AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value - m_CurrentProgramSection.ProgramStartAddress);
                        AssembledProgram.ProgramSections.Add(m_CurrentProgramSection);
                        m_CurrentProgramSection = null;
                    }
                    row.State = AssemblyRowState.Interpreted;
                    return true;
                }

                if (row.State == AssemblyRowState.Interpreted || row.State == AssemblyRowState.Assembled)
                {
                    return true;
                }

                if (row.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.Equ ||
                    row.Instruction.Mnemonic == AssemblerConstans.AssemblerInstructions.EqualSymbol)
                {
                    var parser = new EquInstructionParser(row, AssembledProgram.SymbolTable);

                    var parseResult = parser.Parse();
                    if (parseResult.ResultCode == ParseResultCode.Error)
                    {
                        throw new Z80AssemblerException(parseResult.Message);
                    }

                    if (parseResult.ResultCode == ParseResultCode.Ok)
                    {
                        var symbol = AssembledProgram.SymbolTable.First(s => s.Key == row.Label.ToUpper());
                        symbol.Value.Value = parseResult.ResultValue;
                        row.State = AssemblyRowState.Interpreted;
                    }
                    else
                    {
                        row.State = AssemblyRowState.ContainsFutureSymbol;
                    }

                    return true;
                }

                row.InstructionBytes.Clear();
                var resolver = AssemblerInstructionResolver.Create(row.Instruction.Mnemonic,
                    AssembledProgram.SymbolTable,
                    IncludeDirectories);
                var resolverResult = resolver.Resolve(row);

                if (resolverResult.ResultCode == ParseResultCode.Error)
                {
                    throw new Z80AssemblerException(resolverResult.Message);
                }

                row.InstructionBytes.AddRange(resolver.InstructionBytes);
                row.State = resolverResult.ResultCode == ParseResultCode.Ok
                    ? AssemblyRowState.Assembled
                    : AssemblyRowState.ContainsFutureSymbol;

            }
            catch (Z80AssemblerException exception)
            {
                m_StatusMessage.AppendLine($"{exception.Message} Sor:{row.RowNumber}");
                WrongLineNumber = row.RowNumber;
                return false;
            }
            return true;
        }

        private bool BuildAssemblyRows()
        {
            do
            {
                foreach (AssemblyRow row in InterPretedAssemblyRows)
                {
                    switch (row.Instruction.Type)
                    {
                        case InstructionType.AssemblerInstruction:
                            {
                                if (!ResolveAssemblerInstruction(row))
                                {
                                    return false;
                                }
                            }
                            break;
                        case InstructionType.ProcessorInstruction:
                            {
                                if (!BuildProcessorInstruction(row))
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    if (!string.IsNullOrEmpty(row.Label))
                    {
                        Symbol addressSymbol = AssembledProgram.SymbolTable.FirstOrDefault(s => s.Key == row.Label && s.Value.Type == SymbolType.Address).Value;
                        if (addressSymbol != null && addressSymbol.State == SymbolState.Unresolved)
                        {
                            addressSymbol.Value = AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value;
                        }
                    }
                    if (row.InstructionBytes.Count > 0)
                    {
                        row.Address = AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value;
                        ushort newLocationCounter = AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value;
                        newLocationCounter += (ushort)row.InstructionBytes.Count;
                        AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value = newLocationCounter;
                    }
                }

                // The program is not closed with the 'END' assembler instruction, in this case the last row is handled as latest
                if (m_CurrentProgramSection != null)
                {
                    m_CurrentProgramSection.SectionLength =
                        (ushort)(AssembledProgram.SymbolTable[AssemblerConstans.LocationCounterSymbol].Value - m_CurrentProgramSection.ProgramStartAddress);
                    AssembledProgram.ProgramSections.Add(m_CurrentProgramSection);
                    m_CurrentProgramSection = null;
                }

            } while (InterPretedAssemblyRows.Any(row => row.State == AssemblyRowState.ContainsFutureSymbol));

            if (AssembledProgram.ProgramSections.Count == 0)
            {
                m_StatusMessage.AppendLine(@"A program nem tartalmaz 'ORG' utasítást!");
                WrongLineNumber = 1;
                return false;
            }
            return true;
        }

        private void LinkProgram()
        {
            foreach (AssemblyRow row in InterPretedAssemblyRows)
            {
                if (row.State == AssemblyRowState.Assembled)
                {
                    AssembledProgram.ProgramBytes.AddRange(row.InstructionBytes);
                }
            }
        }

        private readonly List<string> m_ProgramText;
        private readonly StringBuilder m_StatusMessage;
        private readonly Z80InstructionBuilderFactory m_InstructionBuilderFactory;
        private ProgramSection m_CurrentProgramSection;
        private readonly string m_FileName;
        private List<int> m_SkippedLineNumbers;
    }
}
