using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.SubRoutineInstructions
{
    internal class RstInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            if (operands == null || operands.Count != 1)
            {
                throw new Z80AssemblerException("Az 'RST' utasításnak csak egy operandusra van szüksége!");
            }

            byte jumpAddress = operands[0].ToString().ResolveByteConstant();

            switch (jumpAddress)
            {
                case 0x00:
                    InstructionBytes = new byte[] {0xC7 };
                    break;
                case 0x08:
                    InstructionBytes = new byte[] { 0xCF };
                    break;
                case 0x10:
                    InstructionBytes = new byte[] { 0xD7 };
                    break;
                case 0x18:
                    InstructionBytes = new byte[] { 0xDF };
                    break;
                case 0x20:
                    InstructionBytes = new byte[] { 0xE7 };
                    break;
                case 0x28:
                    InstructionBytes = new byte[] { 0xEF };
                    break;
                case 0x30:
                    InstructionBytes = new byte[] { 0xF7 };
                    break;
                case 0x38:
                    InstructionBytes = new byte[] { 0xFF };
                    break;
                default: throw new Z80AssemblerException($"Érvénytelen cím {jumpAddress} az 'RST' utasításnak!");
            }

            ClockCycles = 11;
            return true;
        }
    }
}
