using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.CompareInstructions
{
    internal class CompareInstructionBuilder : IZ80InstructionBuilder
    {
        public CompareInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;

        }
        public byte[] InstructionBytes { get; private set; }
        public void Build()
        {
            if (m_Operands == null)
            {
                throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak szüksége van operandusra!");
            }

            switch (m_Instruction)
            {
                case "CPD":
                    InstructionBytes = new byte[] { 0xED, 0xA9 };
                    ClockCycles = 16;
                    break;
                case "CPI":
                    InstructionBytes = new byte[] { 0xED, 0xA1 };
                    ClockCycles = 16;
                    break;
                case "CPDR":
                    InstructionBytes = new byte[] { 0xED, 0xB9 };
                    ClockCycles = 16;
                    break;
                case "CPIR":
                    InstructionBytes = new byte[] { 0xED, 0xB1 };
                    ClockCycles = 16;
                    break;
                case "DAA":
                    InstructionBytes = new byte[] { 0x27 };
                    ClockCycles = 4;
                    break;
                case "CP":
                    {
                        Z80InstructionResolver resolver = new CpInstructionResolver();
                        if (resolver.Resolve(m_Operands))
                        {
                            InstructionBytes = resolver.InstructionBytes;
                            ClockCycles = resolver.ClockCycles;
                        }
                    }
                    break;
                default:
                    throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen, vagy érvénytelen operandusokat tartalmaz!");
            }

        }

        public int ClockCycles { get; private set; }
        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
    }
}
