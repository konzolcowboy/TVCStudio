using System;
using System.Collections.Generic;
using System.IO;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal class IncBinInstructionResolver : AssemblerInstructionResolver
    {
        public override ParseResult Resolve(AssemblyRow row)
        {
            if (row.Operands == null || row.Operands.Count == 0)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"A {row.Instruction.Mnemonic} utasításnak szüksége van operandusra!");
            }

            if (row.Operands.Count > 3)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"A {row.Instruction.Mnemonic} utasításnak maximum három operandusa lehet!");
            }

            if (row.Operands[0].Info.DataType != OperandType.Literal)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"A {row.Instruction.Mnemonic} utasítás első operandusa kötelezően file név string!");
            }

            foreach (string includeDirectory in m_IncludeDirectories)
            {
                var filePath = Path.Combine(includeDirectory, row.Operands[0].Value.Replace("\"", ""));

                if (File.Exists(filePath))
                {
                    try
                    {
                        FileInfo info = new FileInfo(filePath);
                        var result = ParseOperands(row, info.Length, out var position, out var count);
                        if (result.ResultCode != ParseResultCode.Ok)
                        {
                            return result;
                        }

                        count = (int)(count == -1 || count > info.Length ? info.Length : count);
                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                br.BaseStream.Position = position;
                                InstructionBytes.AddRange(br.ReadBytes(count));
                            }
                        }
                        return new ParseResult(ParseResultCode.Ok);
                    }
                    catch (Exception e)
                    {
                        return new ParseResult(ParseResultCode.Error, 0, $"A file '{row.Operands[0].Value}' feldolgozása sikertelen:{e.Message}");
                    }
                }
            }

            return new ParseResult(ParseResultCode.Error, 0, $"Nem találom a megadott file-t: '{row.Operands[0].Value}'");
        }

        public IncBinInstructionResolver(IReadOnlyDictionary<string, Symbol> symbolTable, IReadOnlyList<string> includeDirectories) : base(symbolTable)
        {
            InstructionBytes = new List<byte>();
            m_IncludeDirectories = includeDirectories;
        }

        private ParseResult ParseOperands(AssemblyRow row, long fileLength, out int position, out int count)
        {
            position = 0;
            count = -1;

            if (row.Operands.Count == 2 || row.Operands.Count == 3)
            {

                Operand secondOperand = row.Operands[1];
                switch (secondOperand.Info.DataType)
                {
                    case OperandType.Decimal:
                        position = Convert.ToInt32(secondOperand.Value);
                        break;
                    case OperandType.Byte:
                        position = secondOperand.Value.ResolveByteConstant();
                        break;
                    case OperandType.Word:
                        position = secondOperand.Value.ResolveUshortConstant();
                        break;
                    default: return new ParseResult(ParseResultCode.Error, 0, $"Az utasítás '{row.Instruction.Mnemonic}' második operandusa csak szám lehet!");
                }

                if (position < 0 || position > fileLength)
                {
                    return new ParseResult(ParseResultCode.Error, 0, $"Az utasítás '{row.Instruction.Mnemonic}' második operandusa nem lehet kisebb nullánál, vagy nagyobb a file hosszánál!");
                }

                count = (int)(fileLength - position);
            }

            if (row.Operands.Count == 3)
            {

                Operand thirdOperand = row.Operands[2];
                switch (thirdOperand.Info.DataType)
                {
                    case OperandType.Decimal:
                        count = Convert.ToInt32(thirdOperand.Value);
                        break;
                    case OperandType.Byte:
                        count = thirdOperand.Value.ResolveByteConstant();
                        break;
                    case OperandType.Word:
                        count = thirdOperand.Value.ResolveUshortConstant();
                        break;
                    default: return new ParseResult(ParseResultCode.Error, 0, $"Az utasítás '{row.Instruction.Mnemonic}' harmadik operandusa csak szám lehet!");
                }

                if (count < 1 || count > fileLength)
                {
                    return new ParseResult(ParseResultCode.Error, 0, $"Az utasítás '{row.Instruction.Mnemonic}' harmadik operandusa nem lehet kisebb egynél vagy nagyobb a file hosszánál!");
                }
            }

            return new ParseResult(ParseResultCode.Ok);
        }

        private readonly IReadOnlyList<string> m_IncludeDirectories;
    }
}
