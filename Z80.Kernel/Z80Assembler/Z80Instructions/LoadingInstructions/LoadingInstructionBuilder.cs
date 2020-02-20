using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions
{
    internal sealed class LoadingInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public LoadingInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
            m_LoadingInstructionResolvers = new List<Z80InstructionResolver>
            {
                new LoadingInstructionSingleRegister(),
                new LoadingInstructionWithOneByteConstant(),
                new LoadingInstructionWithIndexRegisters(),
                new LoadingInstructionWithTwoBytePointer(),
                new LoadingInstructionConstantIntoIndexRegisters(),
            };
        }

        public void Build()
        {
            if (m_Instruction.Equals("LD") && (m_Operands == null || m_Operands.Count < 2))
            {
                throw new Z80AssemblerException($"Az '{m_Instruction}' utasításnak két operandusra van szüksége!");
            }

            if (!m_Instruction.Equals("LD") && m_Operands != null && m_Operands.Count > 0)
            {
                throw new Z80AssemblerException($"Az '{m_Instruction}' utasításnak nincs szüksége operandusra!");
            }

            switch (m_Instruction)
            {
                case "LD":
                    {
                        bool instructionFound = false;
                        foreach (Z80InstructionResolver instructionResolver in m_LoadingInstructionResolvers)
                        {
                            if (instructionResolver.Resolve(m_Operands))
                            {
                                InstructionBytes = instructionResolver.InstructionBytes;
                                instructionFound = true;
                                ClockCycles = instructionResolver.ClockCycles;
                                break;
                            }
                        }
                        if (!instructionFound)
                        {
                            throw new Z80AssemblerException(
                                $"Az '{m_Instruction}' utasítás érvénytelen operandusokat tartalmaz!");
                        }
                    }
                    break;
                case "LDIR":
                    InstructionBytes = new byte[] { 0xED, 0xB0 };
                    ClockCycles = 21;
                    break;
                case "LDDR":
                    InstructionBytes = new byte[] { 0xED, 0xB8 };
                    ClockCycles = 21;
                    break;
                case "LDI":
                    InstructionBytes = new byte[] { 0xED, 0xA0 };
                    ClockCycles = 16;
                    break;
                case "LDD":
                    InstructionBytes = new byte[] { 0xED, 0xA8 };
                    ClockCycles = 16;
                    break;
                default:
                    throw new Z80AssemblerException($"Az '{m_Instruction}' utasítás nem töltő utasítás!");
            }
        }

        public int ClockCycles { get; private set; }

        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
        private readonly List<Z80InstructionResolver> m_LoadingInstructionResolvers;
    }
}
