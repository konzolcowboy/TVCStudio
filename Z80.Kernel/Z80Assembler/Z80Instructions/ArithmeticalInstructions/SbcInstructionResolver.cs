using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    class SbcInstructionResolver : ArithmeticalInstructionResolverBase
    {

        private static readonly Dictionary<string, Tuple<byte[], int>> SbcInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,\(HL\)$",new Tuple<byte[], int>(new byte []{0x9E},7)},
                {@"^A,A$",new Tuple<byte[], int>(new byte []{0x9F},4)},
                {@"^A,B$",new Tuple<byte[], int>(new byte []{0x98},4)},
                {@"^A,C$",new Tuple<byte[], int>(new byte []{0x99},4)},
                {@"^A,D$",new Tuple<byte[], int>(new byte []{0x9A},4)},
                {@"^A,E$",new Tuple<byte[], int>(new byte []{0x9B},4)},
                {@"^A,H$",new Tuple<byte[], int>(new byte []{0x9C},4)},
                {@"^A,L$",new Tuple<byte[], int>(new byte []{0x9D},4)},
                {@"^HL,BC$",new Tuple<byte[], int>(new byte []{0xED,0x42},15)},
                {@"^HL,DE$",new Tuple<byte[], int>(new byte []{0xED,0x52},15)},
                {@"^HL,HL$",new Tuple<byte[], int>(new byte []{0xED,0x62},15)},
                {@"^HL,SP$",new Tuple<byte[], int>(new byte []{0xED,0x72},15)},

                //not published instructions
                {@"^A,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x9C},8)},
                {@"^A,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x9D},8)},
                {@"^A,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x9C},8)},
                {@"^A,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x9D},8)},
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> SbcInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x9E},19)},
                {@"^A,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x9E},19)}
            };
        private static readonly Dictionary<string, Tuple<byte[], int>> SbcInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xDE},7)}
            };

        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return SbcInstructionsSingleRegister;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return SbcInstructionsWithIndexRegisters;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstants()
        {
            return SbcInstructionsWithOneByteConstants;
        }
    }
}
