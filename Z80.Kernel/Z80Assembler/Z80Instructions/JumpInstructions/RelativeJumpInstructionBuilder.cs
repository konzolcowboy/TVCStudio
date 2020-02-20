using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.JumpInstructions
{
    internal class RelativeJumpInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public RelativeJumpInstructionBuilder(string instruction, List<Operand> operands, ushort actualInstructionAddress)
        {
            m_Instruction = instruction;
            m_Operands = operands;
            m_ActualInstructionAddress = (ushort)(actualInstructionAddress + 2);
        }
        public void Build()
        {
            if (m_Instruction == "DJNZ")
            {
                if (m_Operands == null || m_Operands.Count == 0)
                {
                    throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasításnak szüksége van operandusra!");
                }

                if (m_Operands.Count > 1)
                {
                    throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás operandusa csak egy 16 bites memória cím lehet!");
                }

                ushort jumpAddress = m_Operands[0].ToString().ResolveUshortConstant();
                byte relativeJumpbyte = m_Operands[0].State == OperandState.FutureSymbol ? (byte)0x00 : CalculateRelativByteAddress(m_ActualInstructionAddress, jumpAddress);
                InstructionBytes = new byte[] { 0x10, relativeJumpbyte };
                ClockCycles = 13;
                return;
            }
            if (m_Instruction == "JR")
            {
                if (m_Operands == null || m_Operands.Count == 0)
                {
                    throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasításnak szüksége van legalább egy operandusra!");
                }

                if (m_Operands.Count == 1)
                {
                    ushort jumpAddress = m_Operands[0].ToString().ResolveUshortConstant();
                    byte relativeJumpbyte = m_Operands[0].State == OperandState.FutureSymbol ? (byte)0x00 : CalculateRelativByteAddress(m_ActualInstructionAddress, jumpAddress);
                    InstructionBytes = new byte[] { 0x18, relativeJumpbyte };
                    ClockCycles = 12;
                    return;
                }

                if (m_Operands.Count == 2)
                {
                    if (Conditions.ContainsKey(m_Operands[0].ToString()))
                    {
                        byte ib = Conditions[m_Operands[0].ToString()];
                        ushort jumpAddress = m_Operands[1].ToString().ResolveUshortConstant();
                        byte relativeJumpbyte = m_Operands[1].State == OperandState.FutureSymbol ? (byte)0x00 : CalculateRelativByteAddress(m_ActualInstructionAddress, jumpAddress);
                        InstructionBytes = new[] { ib, relativeJumpbyte };
                        ClockCycles = 12;
                        return;
                    }

                    throw new Z80AssemblerException($"Hibás ugrási feltétel a(z) '{m_Instruction}' utasításban!");
                }

                throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasításnak legfeljebb két operandusa lehet!");
            }
        }

        public int ClockCycles { get; private set; }


        private byte CalculateRelativByteAddress(ushort currentAddress, ushort jumpAddress)
        {
            short addressDifference = (short)(jumpAddress - currentAddress);
            if (addressDifference < -128 || addressDifference > 127)
            {
                throw new Z80AssemblerException($"Hibás ugrási cím a(z) '{m_Instruction}' utasításnak. Az utasítás előre 127 byte-ot, visszafele pedig 128 byte-ot ugorhat!");
            }

            return addressDifference < 0 ? (byte)(256 - (-addressDifference)) : (byte)addressDifference;
        }

        private static readonly Dictionary<string, byte> Conditions =
            new Dictionary<string, byte>
            {
                {@"C",0x38},
                {@"NC",0x30},
                {@"Z",0x28},
                {@"NZ",0x20},
            };

        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
        private readonly ushort m_ActualInstructionAddress;
    }
}
