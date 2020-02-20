using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal sealed class DecInstructionResolver : IncInstructionResolver
    {
        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return DecInstructionsSingleRegister;
        }
        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return DecInstructionsWithIndexRegisters;
        }

        private static readonly Dictionary<string, Tuple<byte[], int>> DecInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[], int>(new byte []{0x35},11)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0x3D},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0x05},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0x0D},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0x15},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0x1D},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0x25},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0x2D},4)},
                {@"^BC$",new Tuple<byte[], int>(new byte []{0x0B},6)},
                {@"^DE$",new Tuple<byte[], int>(new byte []{0x1B},6)},
                {@"^HL$",new Tuple<byte[], int>(new byte []{0x2B},6)},
                {@"^SP$",new Tuple<byte[], int>(new byte []{0x3B},6)},
                {@"^IX$",new Tuple<byte[], int>(new byte []{0xDD,0x2B},10)},
                {@"^IY$",new Tuple<byte[], int>(new byte []{0xFD,0x2B},10)},

                // not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x25},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x2D},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x25},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x2D},8)},
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> DecInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x35},23)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x35},23)},
            };
    }
}
