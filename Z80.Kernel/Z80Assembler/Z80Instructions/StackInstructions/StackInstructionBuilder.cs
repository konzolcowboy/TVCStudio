using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.StackInstructions
{
    internal class StackInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public StackInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
        }
        public void Build()
        {
            if (m_Operands == null)
            {
                throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasításnak szüksége van operandusra!");
            }
            switch (m_Operands[0].ToString())
            {
                case "AF":
                    {
                        if (m_Instruction == "PUSH")
                        {
                            InstructionBytes = new byte[] { 0xF5 };
                            ClockCycles = 11;
                        }
                        else if (m_Instruction == "POP")
                        {
                            InstructionBytes = new byte[] { 0xF1 };
                            ClockCycles = 10;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
                        }
                    }
                    break;
                case "BC":
                    {
                        if (m_Instruction == "PUSH")
                        {
                            InstructionBytes = new byte[] { 0xC5 };
                            ClockCycles = 11;
                        }
                        else if (m_Instruction == "POP")
                        {
                            InstructionBytes = new byte[] { 0xC1 };
                            ClockCycles = 10;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
                        }
                    }
                    break;
                case "DE":
                    {
                        if (m_Instruction == "PUSH")
                        {
                            InstructionBytes = new byte[] { 0xD5 };
                            ClockCycles = 11;
                        }
                        else if (m_Instruction == "POP")
                        {
                            InstructionBytes = new byte[] { 0xD1 };
                            ClockCycles = 10;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
                        }
                    }
                    break;
                case "HL":
                    {
                        if (m_Instruction == "PUSH")
                        {
                            InstructionBytes = new byte[] { 0xE5 };
                            ClockCycles = 11;
                        }
                        else if (m_Instruction == "POP")
                        {
                            InstructionBytes = new byte[] { 0xE1 };
                            ClockCycles = 10;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
                        }
                    }
                    break;
                case "IX":
                    {
                        if (m_Instruction == "PUSH")
                        {
                            InstructionBytes = new byte[] { 0xDD, 0xE5 };
                            ClockCycles = 15;
                        }
                        else if (m_Instruction == "POP")
                        {
                            InstructionBytes = new byte[] { 0xDD, 0xE1 };
                            ClockCycles = 14;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
                        }
                    }
                    break;
                case "IY":
                    {
                        if (m_Instruction == "PUSH")
                        {
                            InstructionBytes = new byte[] { 0xFD, 0xE5 };
                            ClockCycles = 15;
                        }
                        else if (m_Instruction == "POP")
                        {
                            InstructionBytes = new byte[] { 0xFD, 0xE1 };
                            ClockCycles = 14;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
                        }
                    }
                    break;
                default: throw new Z80AssemblerException($"Hibás operandus a(z) '{m_Instruction}' utasításnak!");
            }
        }

        public int ClockCycles { get; private set; }
        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;

    }
}
