using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions
{
    internal class LoadingInstructionSingleRegister : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            string joinedoperands = string.Join(",", operands);
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in SingleRegisterInstructions)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
                if (matches.Count > 0)
                {
                    InstructionBytes = keyValuePair.Value.Item1;
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }

            return false;

        }
        private static readonly Dictionary<string, Tuple<byte[], int>> SingleRegisterInstructions = new Dictionary<string, Tuple<byte[], int>>
        {
            {@"^\(BC\),A$",new Tuple<byte[], int>(new byte []{0x02},7)},
            {@"^\(DE\),A$",new Tuple<byte[], int>(new byte []{0x12},7)},
            {@"^\(HL\),A$",new Tuple<byte[], int>(new byte []{0x77},7)},
            {@"^\(HL\),B$",new Tuple<byte[], int>(new byte []{0x70},7)},
            {@"^\(HL\),C$",new Tuple<byte[], int>(new byte []{0x71},7)},
            {@"^\(HL\),D$",new Tuple<byte[], int>(new byte []{0x72},7)},
            {@"^\(HL\),E$",new Tuple<byte[], int>(new byte []{0x73},7)},
            {@"^\(HL\),H$",new Tuple<byte[], int>(new byte []{0x74},7)},
            {@"^\(HL\),L$",new Tuple<byte[], int>(new byte []{0x75},7)},
            {@"^A,\(BC\)$",new Tuple<byte[], int>(new byte []{0x0A},7)},
            {@"^A,\(DE\)$",new Tuple<byte[], int>(new byte []{0x1A},7)},
            {@"^A,\(HL\)$",new Tuple<byte[], int>(new byte []{0x7E},7)},
            {@"^A,A$",new Tuple<byte[], int>(new byte []{0x7F},4)},
            {@"^A,B$",new Tuple<byte[], int>(new byte []{0x78},4)},
            {@"^A,C$",new Tuple<byte[], int>(new byte []{0x79},4)},
            {@"^A,D$",new Tuple<byte[], int>(new byte []{0x7A},4)},
            {@"^A,E$",new Tuple<byte[], int>(new byte []{0x7B},4)},
            {@"^A,H$",new Tuple<byte[], int>(new byte []{0x7C},4)},
            {@"^A,I$",new Tuple<byte[], int>(new byte []{0xED,0x57},9)},
            {@"^A,L$",new Tuple<byte[], int>(new byte []{0x7D},4)},
            {@"^A,R$",new Tuple<byte[], int>(new byte []{0xED,0x5F},9)},
            {@"^B,\(HL\)$",new Tuple<byte[], int>(new byte []{0x46},7)},
            {@"^B,A$",new Tuple<byte[], int>(new byte []{0x47},4)},
            {@"^B,B$",new Tuple<byte[], int>(new byte []{0x40},4)},
            {@"^B,C$",new Tuple<byte[], int>(new byte []{0x41},4)},
            {@"^B,D$",new Tuple<byte[], int>(new byte []{0x42},4)},
            {@"^B,E$",new Tuple<byte[], int>(new byte []{0x43},4)},
            {@"^B,H$",new Tuple<byte[], int>(new byte []{0x44},4)},
            {@"^B,L$",new Tuple<byte[], int>(new byte []{0x45},4)},
            {@"^C,\(HL\)$",new Tuple<byte[], int>(new byte []{0x4E},7)},
            {@"^C,A$",new Tuple<byte[], int>(new byte []{0x4F},4)},
            {@"^C,B$",new Tuple<byte[], int>(new byte []{0x48},4)},
            {@"^C,C$",new Tuple<byte[], int>(new byte []{0x49},4)},
            {@"^C,D$",new Tuple<byte[], int>(new byte []{0x4A},4)},
            {@"^C,E$",new Tuple<byte[], int>(new byte []{0x4B},4)},
            {@"^C,H$",new Tuple<byte[], int>(new byte []{0x4C},4)},
            {@"^C,L$",new Tuple<byte[], int>(new byte []{0x4D},4)},
            {@"^D,\(HL\)$",new Tuple<byte[], int>(new byte []{0x56},7)},
            {@"^D,A$",new Tuple<byte[], int>(new byte []{0x57},4)},
            {@"^D,B$",new Tuple<byte[], int>(new byte []{0x50},4)},
            {@"^D,C$",new Tuple<byte[], int>(new byte []{0x51},4)},
            {@"^D,D$",new Tuple<byte[], int>(new byte []{0x52},4)},
            {@"^D,E$",new Tuple<byte[], int>(new byte []{0x53},4)},
            {@"^D,H$",new Tuple<byte[], int>(new byte []{0x54},4)},
            {@"^D,L$",new Tuple<byte[], int>(new byte []{0x55},4)},
            {@"^E,\(HL\)$",new Tuple<byte[], int>(new byte []{0x5E},4)},
            {@"^E,A$",new Tuple<byte[], int>(new byte []{0x5F},4)},
            {@"^E,B$",new Tuple<byte[], int>(new byte []{0x58},4)},
            {@"^E,C$",new Tuple<byte[], int>(new byte []{0x59},4)},
            {@"^E,D$",new Tuple<byte[], int>(new byte []{0x5A},4)},
            {@"^E,E$",new Tuple<byte[], int>(new byte []{0x5B},4)},
            {@"^E,H$",new Tuple<byte[], int>(new byte []{0x5C},4)},
            {@"^E,L$",new Tuple<byte[], int>(new byte []{0x5D},4)},
            {@"^H,\(HL\)$",new Tuple<byte[], int>(new byte []{0x66},7)},
            {@"^H,A$",new Tuple<byte[], int>(new byte []{0x67},4)},
            {@"^H,B$",new Tuple<byte[], int>(new byte []{0x60},4)},
            {@"^H,C$",new Tuple<byte[], int>(new byte []{0x61},4)},
            {@"^H,D$",new Tuple<byte[], int>(new byte []{0x62},4)},
            {@"^H,E$",new Tuple<byte[], int>(new byte []{0x63},4)},
            {@"^H,H$",new Tuple<byte[], int>(new byte []{0x64},4)},
            {@"^H,L$",new Tuple<byte[], int>(new byte []{0x65},4)},
            {@"^I,A$",new Tuple<byte[], int>(new byte []{0xED,0x47},9)},
            {@"^R,A$",new Tuple<byte[], int>(new byte []{0xED,0x4F},9)},
            {@"^L,\(HL\)$",new Tuple<byte[], int>(new byte []{0x6E},7)},
            {@"^L,A$",new Tuple<byte[], int>(new byte []{0x6F},4)},
            {@"^L,B$",new Tuple<byte[], int>(new byte []{0x68},4)},
            {@"^L,C$",new Tuple<byte[], int>(new byte []{0x69},4)},
            {@"^L,D$",new Tuple<byte[], int>(new byte []{0x6A},4)},
            {@"^L,E$",new Tuple<byte[], int>(new byte []{0x6B},4)},
            {@"^L,H$",new Tuple<byte[], int>(new byte []{0x6C},4)},
            {@"^L,L$",new Tuple<byte[], int>(new byte []{0x6D},4)},
            {@"^SP,HL$",new Tuple<byte[], int>(new byte []{0xF9},6)},
            {@"^SP,IX$",new Tuple<byte[], int>(new byte []{0xDD,0xF9},10)},
            {@"^SP,IY$",new Tuple<byte[], int>(new byte []{0xFD,0xF9},10)},

            // Not published instructions
            {@"^A,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x7C},8)},
            {@"^A,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x7D},8)},
            {@"^B,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x44},8)},
            {@"^B,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x45},8)},
            {@"^C,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x4C},8)},
            {@"^C,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x4D},8)},
            {@"^D,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x54},8)},
            {@"^D,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x55},8)},
            {@"^E,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x5C},8)},
            {@"^E,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x5D},8)},

            { @"^IXH,A$",new Tuple<byte[], int>(new byte []{0xDD,0x67},8)},
            {@"^IXH,B$",new Tuple<byte[], int>(new byte []{0xDD,0x60},8)},
            {@"^IXH,C$",new Tuple<byte[], int>(new byte []{0xDD,0x61},8)},
            {@"^IXH,D$",new Tuple<byte[], int>(new byte []{0xDD,0x62},8)},
            {@"^IXH,E$",new Tuple<byte[], int>(new byte []{0xDD,0x63},8)},
            {@"^IXH,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x64},8)},
            {@"^IXH,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x65},8)},

            {@"^IXL,A$",new Tuple<byte[], int>(new byte []{0xDD,0x6F},8)},
            {@"^IXL,B$",new Tuple<byte[], int>(new byte []{0xDD,0x68},8)},
            {@"^IXL,C$",new Tuple<byte[], int>(new byte []{0xDD,0x69},8)},
            {@"^IXL,D$",new Tuple<byte[], int>(new byte []{0xDD,0x6A},8)},
            {@"^IXL,E$",new Tuple<byte[], int>(new byte []{0xDD,0x6B},8)},
            {@"^IXL,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x6D},8)},
            {@"^IXL,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x6C},8)},


            {@"^A,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x7C},8)},
            {@"^A,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x7D},8)},
            {@"^B,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x44},8)},
            {@"^B,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x45},8)},
            {@"^C,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x4C},8)},
            {@"^C,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x4D},8)},
            {@"^D,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x54},8)},
            {@"^D,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x55},8)},
            {@"^E,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x5C},8)},
            {@"^E,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x5D},8)},

            { @"^IYH,A$",new Tuple<byte[], int>(new byte []{0xFD,0x67},8)},
            {@"^IYH,B$",new Tuple<byte[], int>(new byte []{0xFD,0x60},8)},
            {@"^IYH,C$",new Tuple<byte[], int>(new byte []{0xFD,0x61},8)},
            {@"^IYH,D$",new Tuple<byte[], int>(new byte []{0xFD,0x62},8)},
            {@"^IYH,E$",new Tuple<byte[], int>(new byte []{0xFD,0x63},8)},
            {@"^IYH,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x64},8)},
            {@"^IYH,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x65},8)},

            {@"^IYL,A$",new Tuple<byte[], int>(new byte []{0xFD,0x6F},8)},
            {@"^IYL,B$",new Tuple<byte[], int>(new byte []{0xFD,0x68},8)},
            {@"^IYL,C$",new Tuple<byte[], int>(new byte []{0xFD,0x69},8)},
            {@"^IYL,D$",new Tuple<byte[], int>(new byte []{0xFD,0x6A},8)},
            {@"^IYL,E$",new Tuple<byte[], int>(new byte []{0xFD,0x6B},8)},
            {@"^IYL,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x6D},8)},
            {@"^IYL,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x6C},8)}
        };

    }
}
