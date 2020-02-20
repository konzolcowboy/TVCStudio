using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions
{
    internal class LoadingInstructionWithTwoBytePointer : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            string joinedoperands = string.Join(",", operands);
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in InstructionsWithTwoBytePointer)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
                if (matches.Count > 0)
                {
                    ushort value = matches[0].Groups[1].Value.ResolveUshortConstant();

                    var resultBytes = new List<byte>();
                    resultBytes.AddRange(keyValuePair.Value.Item1);
                    byte highByte = (byte)(value >> 8);
                    byte lowByte = (byte) (value & 0xFF);
                    resultBytes.Add(lowByte);
                    resultBytes.Add(highByte);
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }
            return false;
        }

        private static readonly Dictionary<string, Tuple<byte[], int>> InstructionsWithTwoBytePointer =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\((\${0,1}[0-9A-F]{1,5})\),A$",new Tuple<byte[], int>(new byte[]{ 0x32},13)},
                {@"^\((\${0,1}[0-9A-F]{1,5})\),BC$",new Tuple<byte[], int>(new byte[]{ 0xED,0x43},20)},
                {@"^\((\${0,1}[0-9A-F]{1,5})\),DE$",new Tuple<byte[], int>(new byte[]{ 0xED,0x53},20)},
                {@"^\((\${0,1}[0-9A-F]{1,5})\),HL$",new Tuple<byte[], int>(new byte[]{ 0x22},16)},
                {@"^\((\${0,1}[0-9A-F]{1,5})\),IX$",new Tuple<byte[], int>(new byte[]{ 0xDD,0x22},20)},
                {@"^\((\${0,1}[0-9A-F]{1,5})\),IY$",new Tuple<byte[], int>(new byte[]{ 0xFD,0x22},20)},
                {@"^\((\${0,1}[0-9A-F]{1,5})\),SP$",new Tuple<byte[], int>(new byte[]{ 0xED,0x73},20)},
                {@"^A,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0x3A},13)},
                {@"^BC,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0xED,0x4B},20)},
                {@"^BC,(\${0,1}[0-9A-F]{1,5})$",new Tuple<byte[], int>(new byte[]{ 0x01},10)},
                {@"^DE,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0xED,0x5B},20)},
                {@"^DE,(\${0,1}[0-9A-F]{1,5})$",new Tuple<byte[], int>(new byte[]{ 0x11},10)},
                {@"^HL,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0x2A},16)},
                {@"^HL,(\${0,1}[0-9A-F]{1,5})$",new Tuple<byte[], int>(new byte[]{ 0x21},10)},
                {@"^IX,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0xDD, 0x2A},20)},
                {@"^IX,(\${0,1}[0-9A-F]{1,5})$",new Tuple<byte[], int>(new byte[]{ 0xDD,0x21},14)},
                {@"^IY,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0xFD, 0x2A},20)},
                {@"^IY,(\${0,1}[0-9A-F]{1,5})$",new Tuple<byte[], int>(new byte[]{ 0xFD,0x21},14)},
                {@"^SP,\((\${0,1}[0-9A-F]{1,5})\)$",new Tuple<byte[], int>(new byte[]{ 0xED, 0x7B},20)},
                {@"^SP,(\${0,1}[0-9A-F]{1,5})$",new Tuple<byte[], int>(new byte[]{ 0x31},10)}
            };

    }
}
