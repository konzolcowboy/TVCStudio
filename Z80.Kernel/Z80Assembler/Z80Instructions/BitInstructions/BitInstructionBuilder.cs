using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitInstructions
{
    internal class BitInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public BitInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
        }
        public void Build()
        {
            if (m_Operands == null)
            {
                throw new Z80AssemblerException($"A(z) {m_Instruction} utasításnak két operandusra van szüksége!");
            }

            bool bitNumberParse = byte.TryParse(m_Operands[0].ToString(), out var bitNumber);

            if (!bitNumberParse || bitNumber > 7)
            {
                throw new Z80AssemblerException($"A(z) {m_Instruction} utasítás első operandusa csak szám lehet 0 és 7 között!");
            }

            Z80InstructionResolver resolver;
            switch (m_Instruction)
            {
                case "BIT":
                    resolver = new BitInstructionResolver();
                    break;
                case "SET":
                    resolver = new SetInstructionResolver();
                    break;
                case "RES":
                    resolver = new ResInstructionResolver();
                    break;

                default:
                    throw new Z80AssemblerException($"Az '{m_Instruction}' utasítás nem bit utasítás!");
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
