using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.SubRoutineInstructions
{
    internal class CallInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            if (operands == null)
            {
                throw new Z80AssemblerException($"A 'CALL' utasításnak szüksége van operandusra!");
            }

            if (operands.Count == 1)
            {
                List<byte> resultBytes = new List<byte> { 0xCD };
                resultBytes.AddRange(ResolveConstant(operands[0].ToString()));
                InstructionBytes = resultBytes.ToArray();
                ClockCycles = 17;
                return true;
            }

            if (operands.Count == 2)
            {
                if (Conditions.ContainsKey(operands[0].ToString()))
                {
                    List<byte> resultBytes = new List<byte> {Conditions[operands[0].ToString()] };
                    resultBytes.AddRange(ResolveConstant(operands[1].ToString()));
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = 17;
                    return true;
                }

                throw new Z80AssemblerException($"Érvénytelen feltétel '{operands[0]}' a 'CALL' utasításnak!");
            }

            throw new Z80AssemblerException("Hibás operandusok a 'CALL' utasításnak!");
        }

        private byte[] ResolveConstant(string numberString)
        {
            ushort number = numberString.ResolveUshortConstant();
            byte highByte = (byte)(number >> 8);
            byte lowByte = (byte)(number & 0xFF);

            return new[] { lowByte,highByte };

        }

        private static readonly Dictionary<string, byte> Conditions =
            new Dictionary<string, byte>
            {
                {@"C",0xDC},
                {@"NC",0xD4},
                {@"Z",0xCC},
                {@"NZ",0xC4},
                {@"P",0xF4},
                {@"M",0xFC},
                {@"P0",0xE4},
                {@"PO",0xE4},
                {@"PE",0xEC},
            };
    }
}
