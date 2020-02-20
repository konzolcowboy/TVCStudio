using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions
{
    class LoadingInstructionWithOneByteConstant : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            string joinedoperands = string.Join(",", operands);
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in InstructionsWithOneByteConstans)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
                if (matches.Count > 0)
                {
                    var resultBytes = new List<byte>();
                    resultBytes.AddRange(keyValuePair.Value.Item1);
                    resultBytes.Add(matches[0].Groups[1].Value.ResolveByteConstant());
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }
            return false;

        }

        private static readonly Dictionary<string, Tuple<byte[], int>> InstructionsWithOneByteConstans =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\),(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x36},10)},
                {@"^A,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x3E},7)},
                {@"^B,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x06},7)},
                {@"^C,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x0E},7)},
                {@"^D,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x16},7)},
                {@"^E,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x1E},7)},
                {@"^H,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x26},7)},
                {@"^L,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0x2E},7)},

                // not published instructions
                {@"^IXH,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xDD,0x26},11)},
                {@"^IXL,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xDD,0x2E},11)},
                {@"^IYH,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xFD,0x26},11)},
                {@"^IYL,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xFD,0x2E},11)},

            };
    }
}
