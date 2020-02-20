using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitInstructions
{
    internal abstract class BitInstructionResolverBase : Z80InstructionResolver
    {
        private readonly string m_Instruction;

        protected BitInstructionResolverBase(string instruction)
        {
            m_Instruction = instruction;
        }
        public override bool Resolve(List<Operand> operands)
        {
            bool parsed = byte.TryParse(operands[0].ToString(), out var bitNumber);

            if (!parsed)
            {
                return false;
            }

            foreach (KeyValuePair<string, byte[]> keyValuePair in IndexRegisterRegexes)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operands[1].ToString());
                if (matches.Count > 0)
                {
                    InstructionBytes = new byte[4];
                    InstructionBytes[0] = keyValuePair.Value[0];
                    InstructionBytes[1] = keyValuePair.Value[1];
                    InstructionBytes[2] = matches[0].Groups[1].Value.ResolveByteConstant();
                    byte ib = GetIndexByte(bitNumber);
                    byte ob = GetOffsetByteForIndexRegisters(operands, bitNumber);
                    InstructionBytes[3] = (byte)(ib + ob);
                    ClockCycles = GetClockCycles(operands[1].ToString());
                    return true;
                }
            }

            byte indexByte = GetIndexByte(bitNumber);
            byte offsetByte = GetOffsetByte(operands[1].ToString(), bitNumber);
            InstructionBytes = new byte[2];
            InstructionBytes[0] = 0xCB;
            InstructionBytes[1] = (byte)(indexByte + offsetByte);
            ClockCycles = GetClockCycles(operands[1].ToString());
            return true;
        }

        protected abstract byte GetIndexByte(byte bitNumber);

        protected abstract int GetClockCycles(string registerString);

        private byte GetOffsetByteForIndexRegisters(List<Operand> operands, byte bitNumber)
        {
            if (operands.Count == 3 &&
               operands[1].Info.DataType != OperandType.ByteWithIndexRegister &&
               operands[1].Info.DataType != OperandType.DecimalWithIndexRegister)
            {
                throw new Z80AssemblerException($"Ha az '{m_Instruction}' utasításnak három operandusa van, akkor a második operandus kötelezően aZ '(IX+*)' vagy (IY+*)!");
            }

            if (operands.Count == 2)
            {
                return GetOffsetByte(operands[1].Value, bitNumber);
            }

            if (operands.Count == 3)
            {
                return GetOffsetByte(operands[2].Value, bitNumber);
            }


            throw new Z80AssemblerException($"Érvénytelen oprandusok '{string.Join(",", operands)}' az '{m_Instruction}' utasításnak!");
        }

        private byte GetOffsetByte(string registerString, byte bitNumber)
        {
            if (bitNumber > 7)
            {
                throw new Z80AssemblerException($"Az '{m_Instruction}' utasítás első operandusának 0 és 7 közé kell esnie!");
            }

            if (registerString.Equals(@"(HL)") || registerString.StartsWith(@"(IX+") || registerString.StartsWith(@"(IY+"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x06;
                }

                return 0x0E;
            }

            if (registerString.Equals(@"A"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x07;
                }

                return 0x0F;
            }

            if (registerString.Equals(@"B"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x00;
                }

                return 0x08;
            }

            if (registerString.Equals(@"C"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x01;
                }

                return 0x09;
            }

            if (registerString.Equals(@"D"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x02;
                }

                return 0x0A;
            }

            if (registerString.Equals(@"E"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x03;
                }

                return 0x0B;
            }

            if (registerString.Equals(@"H"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x04;
                }

                return 0x0C;
            }

            if (registerString.Equals(@"L"))
            {
                if (bitNumber % 2 == 0)
                {
                    return 0x05;
                }

                return 0x0D;
            }

            throw new Z80AssemblerException($"Helytelen operandus {registerString} az '{m_Instruction}' utasításnak!");

        }

        protected static Dictionary<string, byte[]> IndexRegisterRegexes { get; } =
            new Dictionary<string, byte[]>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new byte []{0xDD,0xCB}},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new byte []{0xFD,0xCB}}
            };
    }
}
