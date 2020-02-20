using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LogicalInstructions
{
    internal sealed class LogicalInstructionBuilder : IZ80InstructionBuilder
    {
        public LogicalInstructionBuilder(string instruction, List<Operand> operands)
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

            if (m_Instruction == "NEG")
            {
                InstructionBytes = new byte[] { 0xED, 0x44 };
                ClockCycles = 8;
                return;
            }

            if (m_Instruction == "CPL")
            {
                InstructionBytes = new byte[] { 0x2F };
                ClockCycles = 4;
                return;
            }

            if (m_Operands == null || m_Operands.Count != 1)
            {
                throw new Z80AssemblerException($"Az '{m_Instruction}' utasításnak egy operandusra van szüksége!");
            }

            Z80InstructionResolver resolver;

            switch (m_Instruction)
            {
                case "AND":
                    {
                        resolver = new AndInstructionResolver();
                    }
                    break;
                case "OR":
                    {
                        resolver =new OrInstructionResolver();
                    }
                    break;
                case "XOR":
                    {
                        resolver = new XorInstructionResolver();
                    }
                    break;
                default:
                    throw new Z80AssemblerException($"Az '{m_Instruction}' utasítás nem logikai utasítás!");
            }

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
