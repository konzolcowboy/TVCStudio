using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.PortInstructions
{
    internal class InInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            foreach (KeyValuePair<string, byte[]> keyValuePair in InInstructionsSingleRegister)
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
            foreach (KeyValuePair<string, byte[]> keyValuePair in InInstructionsWithOneBytePointer)
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
                    ClockCycles = 12;
                    return true;
                }
            }
            return false;
        }

        private static readonly Dictionary<string, byte[]> InInstructionsSingleRegister =
            new Dictionary<string, byte[]>
            {
                {@"^A,\(C\)$",new byte []{0xED, 0x78}},
                {@"^B,\(C\)$",new byte []{0xED, 0x40 }},
                {@"^C,\(C\)$",new byte []{0xED, 0x48 }},
                {@"^D,\(C\)$",new byte []{0xED, 0x50 }},
                {@"^E,\(C\)$",new byte []{0xED, 0x58 }},
                {@"^H,\(C\)$",new byte []{0xED, 0x60 }},
                {@"^L,\(C\)$",new byte []{0xED, 0x68 }},
                {@"^\(C\)$",new byte []{0xED, 0x70 }}
            };
        private static readonly Dictionary<string, byte[]> InInstructionsWithOneBytePointer =
            new Dictionary<string, byte[]>
            {
                {@"^A,\((\${0,1}[0-9A-F]{1,3})\)$",new byte []{0xDB}}
            };

    }
}
