using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.SubRoutineInstructions
{
    internal class RetInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            if (operands == null || operands.Count == 0)
            {
                InstructionBytes = new byte[] { 0xC9 };
                ClockCycles = 10;
                return true;
            }

            if (operands.Count == 1)
            {
                if (Conditions.ContainsKey(operands[0].ToString()))
                {
                    InstructionBytes = new [] {Conditions[operands[0].ToString()] };
                    ClockCycles = 11;
                    return true;
                }

                throw new Z80AssemblerException($"Helytelen feltétel {operands[0]} a 'RET' utasításnak!");
            }

            throw new Z80AssemblerException($"Helytelen paraméterek lettek megadva a 'RET' utasításnak!");
        }
        private static readonly Dictionary<string, byte> Conditions =
            new Dictionary<string, byte>
            {
                {@"C",0xD8},
                {@"NC",0xD0},
                {@"Z",0xC8},
                {@"NZ",0xC0},
                {@"P",0xF0},
                {@"M",0xF8},
                {@"P0",0xE0},
                {@"PO",0xE0},
                {@"PE",0xE8},
            };

    }
}
