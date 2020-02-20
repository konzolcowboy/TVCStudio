using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions
{
    internal abstract class Z80InstructionResolver
    {
        public abstract bool Resolve(List<Operand> operands);
        public byte[] InstructionBytes { get; protected set; }

        public int ClockCycles { get; protected set; }
    }
}
