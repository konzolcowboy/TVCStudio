using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal class DwInstructionResolver : AssemblerInstructionResolver
    {
        public DwInstructionResolver(IReadOnlyDictionary<string, Symbol> symbolTable) : base(symbolTable)
        {
            InstructionBytes = new List<byte>();
        }
        public override ParseResult Resolve(AssemblyRow row)
        {
            if (row.Operands == null || row.Operands.Count == 0)
            {
                return new ParseResult(ParseResultCode.Error, 0, $" Az {row.Instruction.Mnemonic} utasításnak szüksége van operandusra. Az operandus egy vagy több két byte-os érték lehet vesszővel elválasztva!");
            }

            bool rowContainsFutureSymbol = false;

            foreach (Operand operand in row.Operands)
            {
                if (SymbolTable.Any(s => s.Value.Name.Equals(operand.Value)))
                {
                    operand.State = OperandState.FutureSymbol;
                }

                if (operand.Info.DataType != OperandType.Decimal && operand.Info.DataType != OperandType.Word && 
                    operand.Info.DataType != OperandType.Byte && operand.Info.DataType != OperandType.Expression)
                {
                    if (operand.State != OperandState.Valid && 
                        operand.State != OperandState.FutureSymbol)
                    {
                        return new ParseResult(ParseResultCode.Error, 0,
                            $"Az operandus adat típusa({operand.Info.DataType}) a {row.Instruction.Mnemonic} utasításban nem támogatott!");
                    }
                }

                if (operand.Info.DataType == OperandType.Expression ||
                    operand.State == OperandState.FutureSymbol)
                {
                    var expressionParser = new ExpressionParser(operand.Value, SymbolTable);
                    var result = expressionParser.Parse();
                    if (result.ResultCode == ParseResultCode.Error)
                    {
                        return result;
                    }

                    InstructionBytes.Add((byte)(result.ResultValue & 0xFF));
                    InstructionBytes.Add((byte)(result.ResultValue >> 8));
                    if (result.ResultCode == ParseResultCode.ContainsFutureSymbol)
                    {
                        rowContainsFutureSymbol = true;
                    }
                }
                else
                {
                    ushort value = operand.ToString().ResolveUshortConstant();
                    byte highByte = (byte)(value >> 8);
                    byte lowByte = (byte)(value & 0xFF);
                    InstructionBytes.Add(lowByte);
                    InstructionBytes.Add(highByte);
                }
            }

            return new ParseResult(rowContainsFutureSymbol? ParseResultCode.ContainsFutureSymbol : ParseResultCode.Ok);
        }
    }
}
