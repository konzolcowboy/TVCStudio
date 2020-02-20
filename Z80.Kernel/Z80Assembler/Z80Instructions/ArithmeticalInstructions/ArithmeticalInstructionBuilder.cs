using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal class ArithmeticalInstructionBuilder : IZ80InstructionBuilder
    {
        public ArithmeticalInstructionBuilder(string instruction, List<Operand> operands)
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

            Z80InstructionResolver resolver;

            switch (m_Instruction)
            {
                case "ADC":
                    {
                        if (m_Operands[0].ToString() != "A" && m_Operands[0].ToString() != "HL")
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasítás első operandusa csak 'A' vagy 'HL' lehet!");
                        }

                        resolver = new AdcInstructionResolver();
                    }
                    break;
                case "ADD":
                    {
                        if (m_Operands[0].ToString() != "A" && m_Operands[0].ToString() != "HL" &&
                            m_Operands[0].ToString() != "IX" && m_Operands[0].ToString() != "IY")
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasítás első operandusa csak 'A', 'HL', 'IX', 'IY' lehet!");
                        }

                        resolver = new AddInstructionResolver();

                    }
                    break;
                case "SBC":
                    {
                        if (m_Operands[0].ToString() != "A" && m_Operands[0].ToString() != "HL")
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasítás első operandusa csak 'A' vagy 'HL' lehet!");
                        }

                        resolver = new SbcInstructionResolver();

                    }
                    break;
                case "SUB":
                    {
                        if (m_Operands.Count != 1)
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak egy operandusra van szüksége!");
                        }

                        resolver = new SubInstructionResolver();
                    }
                    break;
                case "INC":
                    {
                        if (m_Operands.Count != 1)
                        {
                            throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak egy operandusra van szüksége!");
                        }

                        resolver = new IncInstructionResolver();
                    }
                    break;
                case "DEC":
                {
                    if (m_Operands.Count != 1)
                    {
                        throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak egy operandusra van szüksége!");
                    }

                    resolver = new DecInstructionResolver();
                }
                    break;
                default:
                    throw new Z80AssemblerException($"Az '{m_Instruction}' utasítás nem aritmetikai utasítás!");

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
