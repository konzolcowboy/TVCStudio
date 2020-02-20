using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.OtherInstructions
{
    internal class OtherInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public OtherInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
        }
        public void Build()
        {
            if (m_Instruction == "IM")
            {
                if (m_Operands == null || m_Operands.Count == 0)
                {
                    throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás operandusai a következők lehetnek: 0, 1 vagy 2!");
                }
                byte operand = m_Operands[0].ToString().ResolveByteConstant();

                if (operand == 0)
                {
                    InstructionBytes = new byte[] {0xED, 0x46};
                    ClockCycles = 8;
                    return;
                }
                if (operand == 1)
                {
                    InstructionBytes = new byte[] { 0xED, 0x56 };
                    ClockCycles = 8;
                    return;
                }
                if (operand == 2)
                {
                    InstructionBytes = new byte[] { 0xED, 0x5E };
                    ClockCycles = 8;
                    return;
                }

                throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás operandusai a következők lehetnek: 0, 1 vagy 2!");
            }

            if (m_Instruction == "DI")
            {
                InstructionBytes = new byte[] { 0xF3 };
                ClockCycles = 4;
                return;
            }

            if (m_Instruction == "EI")
            {
                InstructionBytes = new byte[] { 0xFB };
                ClockCycles = 4;
                return;
            }

            if (m_Instruction == "HALT")
            {
                InstructionBytes = new byte[] { 0x76 };
                ClockCycles = 4;
                return;
            }

            if (m_Instruction == "NOP")
            {
                InstructionBytes = new byte[] { 0x00 };
                ClockCycles = 4;
                return;
            }

            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
        }

        public int ClockCycles { get; private set; }
        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
    }
}
