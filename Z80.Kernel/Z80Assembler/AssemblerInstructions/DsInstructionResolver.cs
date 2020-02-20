using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal class DsInstructionResolver : AssemblerInstructionResolver
    {
        public DsInstructionResolver(IReadOnlyDictionary<string, Symbol> symbolTable) : base(symbolTable)
        {
            InstructionBytes = new List<byte>();
        }
        public override ParseResult Resolve(AssemblyRow row)
        {
            if (row.Operands == null || row.Operands.Count == 0)
            {
                return new ParseResult(ParseResultCode.Error, 0, $" Az {row.Instruction.Mnemonic} utasításnak a következő operandusai lehetnek: byte-ok száma[,alapérték]");
            }

            byte[] initialValue = { 0x00 };
            if (row.Operands.Count == 2)
            {
                switch (row.Operands[1].Info.DataType)
                {
                    case OperandType.Byte:
                        initialValue = new[] { row.Operands[1].Value.ResolveByteConstant() };
                        break;
                    case OperandType.Decimal:
                        {
                            ushort value = row.Operands[1].Value.ResolveUshortConstant();
                            if (value > 0xFF)
                            {
                                byte highByte = (byte)(value >> 8);
                                byte lowByte = (byte)(value & 0xFF);
                                initialValue = new[] { lowByte, highByte };
                            }
                            else
                            {
                                initialValue = new[] { (byte)value };
                            }
                        }
                        break;
                    case OperandType.Word:
                        {
                            ushort value = row.Operands[1].Value.ResolveUshortConstant();
                            byte highByte = (byte)(value >> 8);
                            byte lowByte = (byte)(value & 0xFF);
                            initialValue = new[] { lowByte, highByte };
                        }
                        break;
                    case OperandType.Character:
                    case OperandType.Literal:
                        initialValue = row.Operands[1].Value.ToTvcAsciiBytes();
                        break;
                    case OperandType.Expression:
                        {
                            var expressionParser = new ExpressionParser(row.Operands[1].Value, SymbolTable);
                            var result = expressionParser.Parse();
                            if (result.ResultCode == ParseResultCode.Error || result.ResultCode == ParseResultCode.ContainsFutureSymbol)
                            {
                                return result;
                            }

                            initialValue = result.ResultValue <= 0xff ? new[] { (byte)result.ResultValue } : new[] { (byte)(result.ResultValue & 0xFF), (byte)(result.ResultValue >> 8) };
                        }
                        break;
                    default:
                        {
                            Symbol symbol = SymbolTable.Where(s => s.Key.Equals(row.Operands[1].Value)).Select(s => s.Value).FirstOrDefault();
                            if (symbol == null)
                            {
                                return new ParseResult(ParseResultCode.Error, 0, $"A(z) {row.Operands[1]} operandus típusa({row.Operands[1].Info.DataType}) a {row.Instruction.Mnemonic} utasításban nem támogatott!");
                            }

                            if (symbol.State == SymbolState.Unresolved)
                            {
                                return new ParseResult(ParseResultCode.ContainsFutureSymbol);
                            }

                            initialValue = symbol.Value <= 0xff ? new[] { (byte)symbol.Value } : new[] { (byte)(symbol.Value & 0xFF), (byte)(symbol.Value >> 8) };
                            break;
                        }
                }
            }

            ushort byteCount;
            if (row.Operands[0].Info.DataType != OperandType.Decimal &&
                row.Operands[0].Info.DataType != OperandType.Byte &&
                row.Operands[0].Info.DataType != OperandType.Word &&
                row.Operands[0].Info.DataType != OperandType.Expression)
            {
                Symbol symbol = SymbolTable.Where(s => s.Key.Equals(row.Operands[0].Value)).Select(s => s.Value).FirstOrDefault();
                if (symbol == null)
                {
                    return new ParseResult(ParseResultCode.Error, 0, $"A(z) {row.Operands[0]} operandus típusa({row.Operands[0].Info.DataType}) a {row.Instruction.Mnemonic} utasításban nem támogatott!");
                }
                if (symbol.State == SymbolState.Unresolved)
                {
                    return new ParseResult(ParseResultCode.ContainsFutureSymbol);
                }
                
                byteCount = symbol.Value;
            }

            else if (row.Operands[0].Info.DataType == OperandType.Expression)
            {
                var expressionParser = new ExpressionParser(row.Operands[0].Value, SymbolTable);
                var result = expressionParser.Parse();
                if (result.ResultCode == ParseResultCode.Error || result.ResultCode == ParseResultCode.ContainsFutureSymbol)
                {
                    return result;
                }

                byteCount = result.ResultValue;
            }
            else
            {
                byteCount = row.Operands[0].ToString().ResolveUshortConstant();
            }


            for (int i = 1; i <= byteCount; i++)
            {
                InstructionBytes.AddRange(initialValue);
            }

            return new ParseResult(ParseResultCode.Ok);
        }
    }
}
