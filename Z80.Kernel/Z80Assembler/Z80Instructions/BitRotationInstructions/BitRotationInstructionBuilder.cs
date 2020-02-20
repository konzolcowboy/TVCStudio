using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitRotationInstructions
{
    internal class BitRotationInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes
        {
            get; private set;
        }

        public BitRotationInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
        }
        public void Build()
        {
            switch (m_Instruction)
            {
                case "RLA":
                    InstructionBytes = new byte[] { 0x17 };
                    ClockCycles = 4;
                    break;
                case "RLCA":
                    InstructionBytes = new byte[] { 0x07 };
                    ClockCycles = 4;
                    break;
                case "RRA":
                    InstructionBytes = new byte[] { 0x1F };
                    ClockCycles = 4;
                    break;
                case "RRCA":
                    InstructionBytes = new byte[] { 0x0F };
                    ClockCycles = 4;
                    break;
                case "RLD":
                    InstructionBytes = new byte[] { 0xED, 0x6F };
                    ClockCycles = 18;
                    break;
                case "RRD":
                    InstructionBytes = new byte[] { 0xED, 0x67 };
                    ClockCycles = 18;
                    break;
                case "CCF":
                    InstructionBytes = new byte[] { 0x3F };
                    ClockCycles = 4;
                    break;
                case "SCF":
                    InstructionBytes = new byte[] { 0x37 };
                    ClockCycles = 4;
                    break;
                default:
                    {
                        if (m_Operands == null || m_Operands.Count == 0)
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak szüksége van operandusra!");
                        }

                        Z80InstructionResolver resolver = new BitRotationInstructionResolver(m_Instruction);

                        if (resolver.Resolve(m_Operands))
                        {
                            InstructionBytes = resolver.InstructionBytes;
                            ClockCycles = resolver.ClockCycles;
                            return;
                        }

                        throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen, vagy érvénytelen operandusokat tartalmaz!");
                    }
            }
        }

        public int ClockCycles
        {
            get; private set;
        }
        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
    }
}
