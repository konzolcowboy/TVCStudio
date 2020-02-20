using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.AssemblerInstructions;

namespace Z80.Kernel.Z80Assembler
{
    public class AssemblyRowInterpreter
    {
        public AssemblyRow InterpretedAssemblyRow
        {
            get; private set;
        }

        public AssemblyRowInterpreter(List<string> tokenizedRows)
        {
            m_TokenizedRows = tokenizedRows;
        }
        public void InterpretRow()
        {
            InterpretedAssemblyRow = new AssemblyRow();
            InterpretStateMachine sMachine = InterpretStateMachine.Init;
            foreach (string token in m_TokenizedRows)
            {
                switch (sMachine)
                {
                    case InterpretStateMachine.Init:
                        if (token.StartsWith(";"))
                        {
                            InterpretedAssemblyRow.Comment = token;
                        }
                        else if (IsSymbol(token))
                        {
                            if (!char.IsLetter(token[0]))
                            {
                                throw new Z80AssemblerException($"A cimke csak betűvel kezdődhet:'{token}'");
                            }

                            InterpretedAssemblyRow.Label = token.ToUpper();
                            sMachine = InterpretStateMachine.Label;
                        }
                        else if (IsProcessorInstruction(token))
                        {
                            InterpretedAssemblyRow.Instruction.Mnemonic = token.ToUpper();
                            InterpretedAssemblyRow.Instruction.Type = InstructionType.ProcessorInstruction;
                            sMachine = InterpretStateMachine.Instruction;
                        }
                        else if (IsPseudoInstruction(token))
                        {
                            InterpretedAssemblyRow.Instruction.Mnemonic = token.ToUpper();
                            InterpretedAssemblyRow.Instruction.Type = InstructionType.AssemblerInstruction;
                            sMachine = InterpretStateMachine.Instruction;

                        }
                        else
                        {
                            throw new Z80AssemblerException($"Helytelen szó a sorban:'{token}'");
                        }
                        continue;
                    case InterpretStateMachine.Label:
                        if (IsProcessorInstruction(token))
                        {
                            InterpretedAssemblyRow.Instruction.Mnemonic = token.ToUpper();
                            InterpretedAssemblyRow.Instruction.Type = InstructionType.ProcessorInstruction;
                            sMachine = InterpretStateMachine.Instruction;
                        }
                        else if (IsPseudoInstruction(token))
                        {
                            InterpretedAssemblyRow.Instruction.Mnemonic = token.ToUpper();
                            InterpretedAssemblyRow.Instruction.Type = InstructionType.AssemblerInstruction;
                            sMachine = InterpretStateMachine.Instruction;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"Helytelen utasítás:'{token.ToUpper()}'! Az utasítás csak processzor, vagy assembler utasítás lehet!");
                        }
                        continue;
                    case InterpretStateMachine.Instruction:
                        {
                            if (token.StartsWith(";"))
                            {
                                InterpretedAssemblyRow.Comment = token;
                            }
                            else
                            {
                                var operandStrings = SplitOperands(token, ',');
                                foreach (string operandString in operandStrings)
                                {
                                    if (!string.IsNullOrEmpty(operandString))
                                    {
                                        if (operandString.StartsWith("'") && !operandString.EndsWith("'") ||
                                            operandString.StartsWith("\"") && !operandString.EndsWith("\""))
                                        {
                                            throw new Z80AssemblerException(
                                                $"Szintaxis hiba: a karakterlánc nincs lezárva:{operandString}!");
                                        }

                                        InterpretedAssemblyRow.Operands.Add(new Operand(IsLiteral(operandString) ? operandString : operandString.ToUpper()));
                                    }
                                }
                                sMachine = InterpretStateMachine.Operands;
                            }
                            continue;
                        }
                    case InterpretStateMachine.Operands:
                        if (token.StartsWith(";"))
                        {
                            InterpretedAssemblyRow.Comment = token;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"Helytelen szó a sorban:'{token}'");
                        }
                        continue;
                }
            }
        }

        #region Private members

        private enum InterpretStateMachine
        {
            Init,
            Label,
            Instruction,
            Operands
        }

        private static string[] ProcessorInstructions
        {
            get;
        } =
        {
            "LD", "LDIR", "LDDR", "LDI", "LDD",
            "EX","EXX","ADD", "ADC", "SUB", "SBC", "INC", "DEC",
            "AND", "OR", "XOR", "NEG", "CPL",
            "CP", "CPD", "CPI", "CPDR", "CPIR", "DAA",
            "BIT", "SET", "RES", "SLA", "SRA", "SRL","SLL",
            "RL", "RLC", "RR", "RRC","RLA","RLCA","RRA","RRCA", "RLD", "RRD", "CCF", "SCF",
            "IN", "OUT", "IND", "INDR", "INI", "INIR", "OUTD", "OTDR", "OUTI", "OTIR",
            "CALL", "RST", "RET", "RETI", "RETN",
            "PUSH", "POP","JP","JR", "DJNZ",
            "IM","DI", "EI", "HALT","NOP"
        };

        private static string[] PseudoInstructions
        {
            get;
        } =
        {
           AssemblerConstans.AssemblerInstructions.Org,
            AssemblerConstans.AssemblerInstructions.Db,
            AssemblerConstans.AssemblerInstructions.Dw,
            AssemblerConstans.AssemblerInstructions.Ds,
            AssemblerConstans.AssemblerInstructions.Equ,
            AssemblerConstans.AssemblerInstructions.EqualSymbol,
            AssemblerConstans.AssemblerInstructions.End,
            AssemblerConstans.AssemblerInstructions.IncBin
        };

        private readonly List<string> m_TokenizedRows;

        private IEnumerable<string> SplitOperands(string inputString, char splitChar)
        {
            List<string> result = new List<string>();
            string temp = string.Empty;
            bool inLiteral = false;

            foreach (char c in inputString)
            {
                if (c == '"' || c=='\'')
                {
                    inLiteral = !inLiteral;
                }

                if (c == splitChar && !inLiteral)
                {
                    result.Add(temp);
                    temp = string.Empty;
                    continue;
                }

                temp += c;
            }

            if (!string.IsNullOrEmpty(temp))
            {
                result.Add(temp);
            }

            return result;
        }

        private bool IsPseudoInstruction(string token)
        {
            return PseudoInstructions.Any(i => i == token.ToUpper());
        }

        private bool IsExpressionOperator(string token)
        {
            return ExpressionHandling.ExpressionParser.SupportedOperators.Any(kvp => kvp.Key == token.ToUpper());
        }

        private bool IsPreprocessorInstruction(string token)
        {
            return Preprocessor.Z80Preprocessor.SupportedInstructions.Any(s => s.Equals(token.ToUpper()));
        }

        private bool IsProcessorInstruction(string token)
        {
            return ProcessorInstructions.Any(i => i == token.ToUpper());
        }

        private bool IsLiteral(string token)
        {
            return (token.StartsWith("'") && token.EndsWith("'")) || 
                (token.StartsWith("\"") && token.EndsWith("\""));
        }

        private bool IsSymbol(string token)
        {
            if (token.ToUpper().ToCharArray().Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            {
                return false;
            }

            return !IsPseudoInstruction(token) &&
                   !IsProcessorInstruction(token) &&
                   !IsExpressionOperator(token) &&
                   !IsPreprocessorInstruction(token);
        }

        #endregion

    }
}
