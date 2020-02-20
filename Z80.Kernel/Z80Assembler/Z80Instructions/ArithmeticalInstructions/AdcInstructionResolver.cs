using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal class AdcInstructionResolver : ArithmeticalInstructionResolverBase
    {

        private static readonly Dictionary<string, Tuple<byte[], int>> AdcInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,\(HL\)$",new Tuple<byte[], int>(new byte []{0x8E},7)},
                {@"^A,A$",new Tuple<byte[], int>(new byte []{0x8F},4)},
                {@"^A,B$",new Tuple<byte[], int>(new byte []{0x88},4)},
                {@"^A,C$",new Tuple<byte[], int>(new byte []{0x89},4)},
                {@"^A,D$",new Tuple<byte[], int>(new byte []{0x8A},4)},
                {@"^A,E$",new Tuple<byte[], int>(new byte []{0x8B},4)},
                {@"^A,H$",new Tuple<byte[], int>(new byte []{0x8C},4)},
                {@"^A,L$",new Tuple<byte[], int>(new byte []{0x8D},4)},
                {@"^HL,BC$",new Tuple<byte[], int>(new byte []{0xED,0x4A},15)},
                {@"^HL,DE$",new Tuple<byte[], int>(new byte []{0xED,0x5A},15)},
                {@"^HL,HL$",new Tuple<byte[], int>(new byte []{0xED,0x6A},15)},
                {@"^HL,SP$",new Tuple<byte[], int>(new byte []{0xED,0x7A},15)},

                //not published instructions
                {@"^A,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x8C},8)},
                {@"^A,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x8D},8)},
                {@"^A,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x8C},8)},
                {@"^A,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x8D},8)},
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> AdcInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x8E},19)},
                {@"^A,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x8E},19)}
            };
        private static readonly Dictionary<string, Tuple<byte[], int>> AdcInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xCE},7)}
            };

        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return AdcInstructionsSingleRegister;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return AdcInstructionsWithIndexRegisters;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstants()
        {
            return AdcInstructionsWithOneByteConstants;
        }
    }
}
