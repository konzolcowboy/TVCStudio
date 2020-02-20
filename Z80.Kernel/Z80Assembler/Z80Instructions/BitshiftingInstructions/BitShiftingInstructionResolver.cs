using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitshiftingInstructions
{
    internal class BitShiftingInstructionResolver : Z80InstructionResolver
    {
        public BitShiftingInstructionResolver(string instruction)
        {
            m_Instruction = instruction;
        }
        public override bool Resolve(List<Operand> operands)
        {
            foreach (KeyValuePair<string, byte[]> keyValuePair in IndexRegisterRegexes)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operands[0].ToString());
                if (matches.Count > 0)
                {
                    InstructionBytes = new byte[4];
                    InstructionBytes[0] = keyValuePair.Value[0];
                    InstructionBytes[1] = keyValuePair.Value[1];
                    InstructionBytes[2] = matches[0].Groups[1].Value.ResolveByteConstant();
                    byte ib = GetIndexByte();
                    byte ob = GetOffsetByte(operands);
                    InstructionBytes[3] = (byte)(ib + ob);
                    ClockCycles = 23;
                    return true;
                }
            }

            byte indexByte = GetIndexByte();
            byte offsetByte = GetOffsetByte(operands);
            InstructionBytes = new byte[2];
            InstructionBytes[0] = 0xCB;
            InstructionBytes[1] = (byte)(indexByte + offsetByte);
            ClockCycles = operands[0].Value.Equals(@"(HL)") ? 15 : 8;
            return true;

        }

        private byte GetIndexByte()
        {
            switch (m_Instruction)
            {
                case "SLA":
                case "SRA":
                    return 0x20;
                case "SRL":
                case "SLL":
                    return 0x30;
                default:
                    throw new Z80AssemblerException($"Az {m_Instruction} utasítás nem bit léptető utasítás!");
            }
        }

        private byte GetOffsetByte(List<Operand> operands)
        {
            if (operands.Count == 2 && operands[0].Info.DataType != OperandType.ByteWithIndexRegister && operands[0].Info.DataType != OperandType.DecimalWithIndexRegister)
            {
                throw new Z80AssemblerException($"Ha az '{m_Instruction}' utasításnak két operandusa van, akkor az első operandus kötelezően az '(IX+*)', vagy az '(IY+*)'!");
            }

            if (operands[0].Info.DataType == OperandType.ByteWithIndexRegister ||
                operands[0].Info.DataType == OperandType.DecimalWithIndexRegister)
            {
                return GetOffsetByteForIndexRegisterInstructions(operands);
            }

            if (operands[0].Value.Equals(@"(HL)"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0E;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x06;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            return OffsetByteForSingleRegister(operands[0]);
        }

        private byte GetOffsetByteForIndexRegisterInstructions(List<Operand> operands)
        {
            if (operands.Count == 1)
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0E;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x06;
                }

                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            return OffsetByteForSingleRegister(operands[1]);
        }

        private byte OffsetByteForSingleRegister(Operand operand)
        {
            if (operand.Value.Equals(@"A"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0F;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x07;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            if (operand.Value.Equals(@"B"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x08;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x00;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            if (operand.Value.Equals(@"C"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x09;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x01;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            if (operand.Value.Equals(@"D"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0A;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x02;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            if (operand.Value.Equals(@"E"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0B;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x03;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            if (operand.Value.Equals(@"H"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0C;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x04;
                }
                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            if (operand.Value.Equals(@"L"))
            {
                if (m_Instruction.Equals(@"SRL") || m_Instruction.Equals(@"SRA"))
                {
                    return 0x0D;
                }
                if (m_Instruction.Equals(@"SLA") || m_Instruction.Equals(@"SLL"))
                {
                    return 0x05;
                }

                throw new Z80AssemblerException($"Az {m_Instruction} utasítás ismeretlen!");
            }

            throw new Z80AssemblerException($"Érvénytelen oprandus '{operand.Value}' az '{m_Instruction}' utasításnak!");
        }

        private static Dictionary<string, byte[]> IndexRegisterRegexes
        {
            get;
        } =
            new Dictionary<string, byte[]>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new byte []{0xDD,0xCB}},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new byte []{0xFD,0xCB}},
            };
        private readonly string m_Instruction;
    }
}
