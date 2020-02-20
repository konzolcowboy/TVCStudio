using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.SubRoutineInstructions
{
    internal class SubRoutineInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public SubRoutineInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;

        }
        public void Build()
        {
            if (m_Instruction == "RETI")
            {
                InstructionBytes = new byte[] { 0xED, 0x4D };
                ClockCycles = 14;
                return;
            }
            if (m_Instruction == "RETN")
            {
                InstructionBytes = new byte[] { 0xED, 0x45 };
                ClockCycles = 15;
                return;
            }

            Z80InstructionResolver resolver;
            switch (m_Instruction)
            {
                case "CALL":
                        resolver = new CallInstructionResolver();
                    break;
                case "RST":
                    resolver = new RstInstructionResolver();
                    break;
                case "RET":
                    resolver = new RetInstructionResolver();
                    break;
                default: throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen, vagy érvénytelen operandusokat tartalmaz!");
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
