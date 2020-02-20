using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal class AddInstructionResolver : ArithmeticalInstructionResolverBase
    {

        private static readonly Dictionary<string, Tuple<byte[], int>> AddInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,\(HL\)$",new Tuple<byte[], int>(new byte []{0x86},7)},
                {@"^A,A$",new Tuple<byte[], int>(new byte []{0x87},4)},
                {@"^A,B$",new Tuple<byte[], int>(new byte []{0x80},4)},
                {@"^A,C$",new Tuple<byte[], int>(new byte []{0x81},4)},
                {@"^A,D$",new Tuple<byte[], int>(new byte []{0x82},4)},
                {@"^A,E$",new Tuple<byte[], int>(new byte []{0x83},4)},
                {@"^A,H$",new Tuple<byte[], int>(new byte []{0x84},4)},
                {@"^A,L$",new Tuple<byte[], int>(new byte []{0x85},4)},
                {@"^HL,BC$",new Tuple<byte[], int>(new byte []{0x09},11)},
                {@"^HL,DE$",new Tuple<byte[], int>(new byte []{0x19},11)},
                {@"^HL,HL$",new Tuple<byte[], int>(new byte []{0x29},11)},
                {@"^HL,SP$",new Tuple<byte[], int>(new byte []{0x39},11)},
                {@"^IX,BC$",new Tuple<byte[], int>(new byte []{0xDD,0x09},15)},
                {@"^IX,DE$",new Tuple<byte[], int>(new byte []{0xDD,0x19},15)},
                {@"^IX,IX$",new Tuple<byte[], int>(new byte []{0xDD,0x29},15)},
                {@"^IX,SP$",new Tuple<byte[], int>(new byte []{0xDD,0x39},15)},
                {@"^IY,BC$",new Tuple<byte[], int>(new byte []{0xFD,0x09},15)},
                {@"^IY,DE$",new Tuple<byte[], int>(new byte []{0xFD,0x19},15)},
                {@"^IY,IY$",new Tuple<byte[], int>(new byte []{0xFD,0x29},15)},
                {@"^IY,SP$",new Tuple<byte[], int>(new byte []{0xFD,0x39},15)},

                //not published instructions
                {@"^A,IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x84},8)},
                {@"^A,IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x85},8)},
                {@"^A,IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x84},8)},
                {@"^A,IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x85},8)},


            };

        private static readonly Dictionary<string, Tuple<byte[], int>> AddInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x86},19)},
                {@"^A,\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x86},19)}
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> AddInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^A,(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xC6},7)}
            };

        protected override Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return AddInstructionsSingleRegister;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return AddInstructionsWithIndexRegisters;
        }

        protected override Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstants()
        {
            return AddInstructionsWithOneByteConstants;
        }
    }
}
