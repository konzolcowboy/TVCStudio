using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.JumpInstructions
{
    internal class JumpInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes { get; private set; }

        public JumpInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;
        }
        public void Build()
        {
            if (m_Operands == null || m_Operands.Count == 0)
            {
                throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasításnak szüksége van operandusra!");
            }

            if (m_Operands.Count == 1)
            {
                if (m_Operands[0].ToString() == "(HL)")
                {
                    InstructionBytes = new byte[] { 0xE9 };
                    ClockCycles = 4;
                    return;
                }
                if (m_Operands[0].ToString() == "(IX)")
                {
                    InstructionBytes = new byte[] { 0xDD, 0xE9 };
                    ClockCycles = 8;
                    return;
                }
                if (m_Operands[0].ToString() == "(IY)")
                {
                    InstructionBytes = new byte[] { 0xFD, 0xE9 };
                    ClockCycles = 8;
                    return;
                }

                List<byte> resultBytes = new List<byte> { 0xC3 };
                resultBytes.AddRange(ResolveConstant(m_Operands[0].ToString()));
                InstructionBytes = resultBytes.ToArray();
                ClockCycles = 10;
                return;
            }

            if (m_Operands.Count == 2)
            {
                if (Conditions.ContainsKey(m_Operands[0].ToString()))
                {
                    List<byte> resultBytes = new List<byte> { Conditions[m_Operands[0].ToString()] };
                    resultBytes.AddRange(ResolveConstant(m_Operands[1].ToString()));
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = 10;
                    return;
                }

                throw new Z80AssemblerException($"Érvénytelen feltétel '{m_Operands[0]}' a 'JP' utasításnak!");
            }

            throw new Z80AssemblerException("Hibás operandusok a 'JP' utasításnak!");
        }

        public int ClockCycles { get; private set; }

        private byte[] ResolveConstant(string numberString)
        {
            ushort number = numberString.ResolveUshortConstant();
            byte highByte = (byte)(number >> 8);
            byte lowByte = (byte)(number & 0xFF);

            return new[] { lowByte, highByte };

        }

        private static readonly Dictionary<string, byte> Conditions =
            new Dictionary<string, byte>
            {
                {@"C",0xDA},
                {@"NC",0xD2},
                {@"Z",0xCA},
                {@"NZ",0xC2},
                {@"P",0xF2},
                {@"M",0xFA},
                {@"P0",0xE2},
                {@"PO",0xE2},
                {@"PE",0xEA}
            };

        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;
    }
}
