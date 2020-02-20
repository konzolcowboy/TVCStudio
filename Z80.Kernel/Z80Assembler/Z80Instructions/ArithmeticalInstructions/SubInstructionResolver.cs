using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal class SubInstructionResolver : ArithmeticalInstructionResolverBase
    {
        private static readonly Dictionary<string, Tuple<byte[], int>> SubInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[], int>(new byte []{0x96},7)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0x97},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0x90},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0x91},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0x92},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0x93},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0x94},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0x95},4)},

                //not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x94},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x95},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x94},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x95},8)},
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> SubInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x96},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x96},19)}
            };
        private static readonly Dictionary<string, Tuple<byte[], int>> SubInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xD6},7)}
            };

        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return SubInstructionsSingleRegister;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return SubInstructionsWithIndexRegisters;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstants()
        {
            return SubInstructionsWithOneByteConstants;
        }
    }
}
