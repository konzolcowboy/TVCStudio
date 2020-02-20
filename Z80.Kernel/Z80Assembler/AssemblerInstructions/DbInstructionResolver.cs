using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal class DbInstructionResolver : AssemblerInstructionResolver
    {
        public DbInstructionResolver(IReadOnlyDictionary<string, Symbol> symbolTable) : base(symbolTable)
        {
            InstructionBytes = new List<byte>();
        }

        public override ParseResult Resolve(AssemblyRow row)
        {
            if (row.Operands == null || row.Operands.Count == 0)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"A {row.Instruction.Mnemonic} utasításnak szüksége van operandusa!");
            }

            foreach (Operand operand in row.Operands)
            {
                if (operand.Info.DataType != OperandType.Byte && operand.Info.DataType != OperandType.Character &&
                    operand.Info.DataType != OperandType.Literal && operand.Info.DataType != OperandType.Decimal &&
                    operand.Info.DataType != OperandType.Expression)
                {
                    Symbol symbol = SymbolTable.Where(s => s.Key.Equals(operand.Value)).Select(s => s.Value).FirstOrDefault();
                    if (symbol == null)
                    {
                        return new ParseResult(ParseResultCode.Error, 0, $"A(z) {operand} operandus típusa({operand.Info.DataType}) a {row.Instruction.Mnemonic} utasításban nem támogatott!");
                    }

                    if (symbol.State == SymbolState.Unresolved)
                    {
                        return new ParseResult(ParseResultCode.ContainsFutureSymbol);
                    }
                    if (symbol.Value > 0xff)
                    {
                        return new ParseResult(ParseResultCode.Error, 0, $"Az operandusként megadott szimbúlum '{symbol.Name}' értéke nem fér el egy byte-on!");
                    }

                    InstructionBytes.Add((byte)symbol.Value);
                    continue;
                }

                switch (operand.Info.DataType)
                {
                    case OperandType.Byte:
                        InstructionBytes.Add(operand.ToString().ResolveByteConstant());
                        break;
                    case OperandType.Character:
                        InstructionBytes.AddRange(operand.Value.ToTvcAsciiBytes());
                        break;
                    case OperandType.Decimal:
                        {
                            ushort v = operand.ToString().ResolveUshortConstant();
                            if (v > 0xFF)
                            {
                                return new ParseResult(ParseResultCode.Error, 0, $"Helytelen decimális érték '{operand}' a {row.Instruction.Mnemonic} utasításban! Az értéknek 0 és 255 közé kell esnie!");
                            }
                            InstructionBytes.Add((byte)v);
                        }
                        break;
                    case OperandType.Expression:
                        {
                            var expressionParser = new ExpressionParser(operand.Value, SymbolTable);
                            var result = expressionParser.Parse();
                            if (result.ResultCode == ParseResultCode.Error || result.ResultCode == ParseResultCode.ContainsFutureSymbol)
                            {
                                return result;
                            }

                            if (result.ResultValue > 0xff)
                            {
                                return new ParseResult(ParseResultCode.Error, 0, $"A kifejezés '{operand}' eredménye nem fért el egy byte-on!");
                            }

                            InstructionBytes.Add((byte)result.ResultValue);
                        }
                        break;
                    case OperandType.Literal:
                        InstructionBytes.AddRange(operand.Value.ToTvcAsciiBytes());
                        break;
                    default:
                        return new ParseResult(ParseResultCode.Error, 0, $"A(z) {operand} operandus típusa({operand.Info.DataType}) a {row.Instruction.Mnemonic} utasításban nem támogatott!");
                }
            }
            
            return new ParseResult(ParseResultCode.Ok);
        }
    }
}
