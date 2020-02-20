using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal class IncInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in GetSingleRegisterRegexes())
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operands[0].ToString());
                if (matches.Count > 0)
                {
                    InstructionBytes = keyValuePair.Value.Item1;
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }

            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in GetRegexesWithIndexRegisters())
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operands[0].ToString());
                if (matches.Count > 0)
                {
                    byte indexValue = matches[0].Groups[1].Value.ResolveByteConstant();
                    var resultBytes = new List<byte>();
                    resultBytes.AddRange(keyValuePair.Value.Item1);
                    resultBytes.Add(indexValue);
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }
            return false;
        }

        protected virtual Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return IncInstructionsSingleRegister;
        }
        protected virtual Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return IncInstructionsWithIndexRegisters;
        }
        private static readonly Dictionary<string, Tuple<byte[], int>> IncInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[], int>(new byte []{0x34},11)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0x3C},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0x04},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0x0C},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0x14},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0x1C},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0x24},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0x2C},4)},
                {@"^BC$",new Tuple<byte[], int>(new byte []{0x03},6)},
                {@"^DE$",new Tuple<byte[], int>(new byte []{0x13},6)},
                {@"^HL$",new Tuple<byte[], int>(new byte []{0x23},6)},
                {@"^SP$",new Tuple<byte[], int>(new byte []{0x33},6)},
                {@"^IX$",new Tuple<byte[], int>(new byte []{0xDD,0x23},10)},
                {@"^IY$",new Tuple<byte[], int>(new byte []{0xFD,0x23},10)},

                // not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0x24},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0x2C},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0x24},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0x2C},8)},

            };

        private static readonly Dictionary<string, Tuple<byte[], int>> IncInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0x34},23)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0x34},23)}
            };
    }
}
