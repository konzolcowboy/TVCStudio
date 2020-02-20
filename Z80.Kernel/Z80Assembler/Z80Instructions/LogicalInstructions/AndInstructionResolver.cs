using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LogicalInstructions
{
    internal class AndInstructionResolver : Z80InstructionResolver
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

            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in GetRegexesWithOneByteConstant())
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

        protected virtual Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes()
        {
            return AndInstructionsSingleRegister;
        }

        protected virtual Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters()
        {
            return AndInstructionsWithIndexRegisters;
        }

        protected virtual Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstant()
        {
            return AndInstructionsWithOneByteConstants;
        }

        private static readonly Dictionary<string, Tuple<byte[], int>> AndInstructionsSingleRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(HL\)$",new Tuple<byte[],int>(new byte []{0xA6},7)},
                {@"^A$",new Tuple<byte[], int>(new byte []{0xA7},4)},
                {@"^B$",new Tuple<byte[], int>(new byte []{0xA0},4)},
                {@"^C$",new Tuple<byte[], int>(new byte []{0xA1},4)},
                {@"^D$",new Tuple<byte[], int>(new byte []{0xA2},4)},
                {@"^E$",new Tuple<byte[], int>(new byte []{0xA3},4)},
                {@"^H$",new Tuple<byte[], int>(new byte []{0xA4},4)},
                {@"^L$",new Tuple<byte[], int>(new byte []{0xA5},4)},

                //not published instructions
                {@"^IXH$",new Tuple<byte[], int>(new byte []{0xDD,0xA4},8)},
                {@"^IXL$",new Tuple<byte[], int>(new byte []{0xDD,0xA5},8)},
                {@"^IYH$",new Tuple<byte[], int>(new byte []{0xFD,0xA4},8)},
                {@"^IYL$",new Tuple<byte[], int>(new byte []{0xFD,0xA5},8)},
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> AndInstructionsWithIndexRegisters =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xDD,0xA6},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\)$",new Tuple<byte[], int>(new byte []{0xFD,0xA6},19)}
            };

        private static readonly Dictionary<string, Tuple<byte[], int>> AndInstructionsWithOneByteConstants =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xE6},7)}
            };
    }
}
