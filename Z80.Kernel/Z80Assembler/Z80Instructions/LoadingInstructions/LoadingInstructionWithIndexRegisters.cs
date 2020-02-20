using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions
{
    class LoadingInstructionWithIndexRegisters : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            string joinedoperands = string.Join(",", operands);
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in InstructionsWithXIndexRegister)
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
        private static readonly Dictionary<string, Tuple<byte[], int>> InstructionsWithXIndexRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),A$",new Tuple<byte[], int>(new byte []{0xDD,0x77},19)},
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),B$",new Tuple<byte[], int>(new byte []{0xDD,0x70},19)},
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),C$",new Tuple<byte[], int>(new byte []{0xDD,0x71},19)},
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),D$",new Tuple<byte[], int>(new byte []{0xDD,0x72},19)},
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),E$",new Tuple<byte[], int>(new byte []{0xDD,0x73},19)},
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),H$",new Tuple<byte[], int>(new byte []{0xDD,0x74},19)},
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),L$",new Tuple<byte[], int>(new byte []{0xDD,0x75},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),A$",new Tuple<byte[], int>(new byte []{0xFD,0x77},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),B$",new Tuple<byte[], int>(new byte []{0xFD,0x70},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),C$",new Tuple<byte[], int>(new byte []{0xFD,0x71},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),D$",new Tuple<byte[], int>(new byte []{0xFD,0x72},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),E$",new Tuple<byte[], int>(new byte []{0xFD,0x73},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),H$",new Tuple<byte[], int>(new byte []{0xFD,0x74},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),L$",new Tuple<byte[], int>(new byte []{0xFD,0x75},19)},
                {@"^A,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x7E},19)},
                {@"^A,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x7E},19)},
                {@"^B,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x46},19)},
                {@"^B,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x46},19)},
                {@"^C,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x4E},19)},
                {@"^C,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x4E},19)},
                {@"^D,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x56},19)},
                {@"^D,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x56},19)},
                {@"^E,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x5E},19)},
                {@"^E,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x5E},19)},
                {@"^H,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x66},19)},
                {@"^H,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x66},19)},
                {@"^L,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x6E},19)},
                {@"^L,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x6E},19)},
            };
    }
}
