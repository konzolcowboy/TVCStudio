using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LogicalInstructions
{
    internal sealed class OrInstructionResolver : AndInstructionResolver
    {
        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return OrInstructionsSingleRegister;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return OrInstructionsWithIndexRegisters;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstant()
        {
            return OrInstructionsWithOneByteConstants;
        }

        private static readonly Dictionary<string, Tuple<byte[], int>> OrInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[], int>(new byte []{0xB6},7)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0xB7},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0xB0},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0xB1},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0xB2},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0xB3},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0xB4},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0xB5},4)},

                //not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0xB4},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0xB5},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0xB4},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0xB5},8)},

            };

        private static readonly Dictionary<string, Tuple<byte[], int>> OrInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0xB6},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0xB6},19)}
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> OrInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xF6},7)}
            };
    }
}
