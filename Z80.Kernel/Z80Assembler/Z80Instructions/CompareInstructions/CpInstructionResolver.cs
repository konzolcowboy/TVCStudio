using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.CompareInstructions
{
    internal class CpInstructionResolver : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in CpInstructionsSingleRegister)
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

            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in CpInstructionsWithIndexRegisters)
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

            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in CpInstructionsWithOneByteConstants)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operands[0].ToString());
                if (matches.Count > 0)
                {
                    byte constantValue = matches[0].Groups[1].Value.ResolveByteConstant();
                    var resultBytes = new List<byte>();
                    resultBytes.AddRange(keyValuePair.Value.Item1);
                    resultBytes.Add(constantValue);
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }

            return false;
        }

        private static readonly Dictionary<string, Tuple<byte[], int>> CpInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[], int>(new byte []{0xBE},7)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0xBF},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0xB8},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0xB9},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0xBA},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0xBB},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0xBC},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0xBD},4)},

                //not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0xBC},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0xBD},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0xBC},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0xBD},8)},

            };

        private static readonly Dictionary<string, Tuple<byte[], int>> CpInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0xBE},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0xBE},19)}
            };
        private static readonly Dictionary<string, Tuple<byte[], int>> CpInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xFE},7)}
            };
    }
}
