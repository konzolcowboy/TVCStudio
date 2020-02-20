using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.PortInstructions
{
    internal class PortInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public PortInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;

        }
        public void Build()
        {
            switch (m_Instruction)
            {
                case "IND":
                    InstructionBytes = new byte[] { 0xED, 0xAA };
                    ClockCycles = 16;
                    break;
                case "INDR":
                    InstructionBytes = new byte[] { 0xED, 0xBA };
                    ClockCycles = 21;
                    break;
                case "INI":
                    InstructionBytes = new byte[] { 0xED, 0xA2 };
                    ClockCycles = 16;
                    break;
                case "INIR":
                    InstructionBytes = new byte[] { 0xED, 0xB2 };
                    ClockCycles = 21;
                    break;
                case "OUTD":
                    InstructionBytes = new byte[] { 0xED, 0xAB };
                    ClockCycles = 16;
                    break;
                case "OTDR":
                    InstructionBytes = new byte[] { 0xED, 0xBB };
                    ClockCycles = 21;
                    break;
                case "OUTI":
                    InstructionBytes = new byte[] { 0xED, 0xA3 };
                    ClockCycles = 16;
                    break;
                case "OTIR":
                    InstructionBytes = new byte[] { 0xED, 0xB3 };
                    ClockCycles = 21;
                    break;
                case "IN":
                    {
                        if (m_Operands == null)
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak szüksége van operandusra!");
                        }

                        Z80InstructionResolver resolver = new InInstructionResolver();
                        if (resolver.Resolve(m_Operands))
                        {
                            InstructionBytes = resolver.InstructionBytes;
                            ClockCycles = resolver.ClockCycles;
                        }
                    }
                    break;
                case "OUT":
                    {
                        if (m_Operands == null)
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak szüksége van operandusra!");
                        }

                        Z80InstructionResolver resolver = new OutInstructionResolver();
                        if (resolver.Resolve(m_Operands))
                        {
                            InstructionBytes = resolver.InstructionBytes;
                            ClockCycles = resolver.ClockCycles;
                        }
                    }
                    break;
                default: throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen, vagy érvénytelen operandusokat tartalmaz!");
            }
        }

        public int ClockCycles { get; private set; }
        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;

    }
}
