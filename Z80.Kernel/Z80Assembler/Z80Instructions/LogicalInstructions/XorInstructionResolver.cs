using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LogicalInstructions
{
    internal sealed class XorInstructionResolver : AndInstructionResolver
    {
        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return XorInstructionsSingleRegister;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return XorInstructionsWithIndexRegisters;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstant()
        {
            return XorInstructionsWithOneByteConstants;
        }

        private static readonly Dictionary<string, Tuple<byte[], int>> XorInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[], int>(new byte []{0xAE},7)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0xAF},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0xA8},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0xA9},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0xAA},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0xAB},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0xAC},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0xAD},4)},

                //not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0xAC},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0xAD},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0xAC},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0xAD},8)},
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> XorInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0xAE},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0xAE},19)}
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> XorInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xEE},7)}
            };

    }
}
