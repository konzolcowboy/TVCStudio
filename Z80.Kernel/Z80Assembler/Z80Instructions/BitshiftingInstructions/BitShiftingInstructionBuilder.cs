using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitshiftingInstructions
{
    class BitShiftingInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public BitShiftingInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
        }
        public void Build()
        {
            if (m_Operands == null || m_Operands.Count == 0)
            {
                throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak szüksége van operandusra!");
            }

            Z80InstructionResolver resolver = new BitShiftingInstructionResolver(m_Instruction);

            if (resolver.Resolve(m_Operands))
            {
                InstructionBytes = resolver.InstructionBytes;
                ClockCycles = resolver.ClockCycles;
                return;
            }

            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen, vagy érvénytelen operandusokat tartalmaz!");
        }

        public int ClockCycles { get; private set; }
        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
    }
}
