using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.PortInstructions
{
    internal class OutInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            foreach (KeyValuePair<string, byte[]> keyValuePair in OutInstructionsSingleRegister)
            {
                string joinedoperands = string.Join(",", operands);
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
                if (matches.Count > 0)
                {
                    InstructionBytes = keyValuePair.Value;
                    ClockCycles = 12;
                    return true;
                }
            }

            foreach (KeyValuePair<string, byte[]> keyValuePair in OutInstructionsWithOneBytePointer)
            {
                string joinedoperands = string.Join(",", operands);
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
                if (matches.Count > 0)
                {
                    byte constantValue = matches[0].Groups[1].Value.ResolveByteConstant();
                    var resultBytes = new List<byte>();
                    resultBytes.AddRange(keyValuePair.Value);
                    resultBytes.Add(constantValue);
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = 11;
                    return true;
                }
            }
            return false;

        }
        private static readonly Dictionary<string, byte[]> OutInstructionsSingleRegister =
            new Dictionary<string, byte[]>
            {
                {@"^\(C\),A$",new byte []{0xED, 0x79}},
                {@"^\(C\),B$",new byte []{0xED, 0x41 }},
                {@"^\(C\),C$",new byte []{0xED, 0x49 }},
                {@"^\(C\),D$",new byte []{0xED, 0x51 }},
                {@"^\(C\),E$",new byte []{0xED, 0x59 }},
                {@"^\(C\),H$",new byte []{0xED, 0x61 }},
                {@"^\(C\),L$",new byte []{0xED, 0x69 }},
                {@"^\(C\),0$",new byte []{0xED, 0x71 }}
            };
        private static readonly Dictionary<string, byte[]> OutInstructionsWithOneBytePointer =
            new Dictionary<string, byte[]>
            {
                {@"^\((\${0,1}[0-9A-F]{1,3})\),A$",new byte []{0xD3}}
            };

    }
}
