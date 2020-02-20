using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions
{
    class LoadingInstructionConstantIntoIndexRegisters : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            string joinedoperands = string.Join(",", operands);
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in InstructionsConstantWithIndexRegister)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
                if (matches.Count > 0)
                {
                    byte indexValue = matches[0].Groups[1].Value.ResolveByteConstant();
                    byte constantValue = matches[0].Groups[2].Value.ResolveByteConstant();

                    var resultBytes = new List<byte>();
                    resultBytes.AddRange(keyValuePair.Value.Item1);
                    resultBytes.Add(indexValue);
                    resultBytes.Add(constantValue);
                    InstructionBytes = resultBytes.ToArray();
                    ClockCycles = keyValuePair.Value.Item2;
                    return true;
                }
            }
            return false;

        }
        private static readonly Dictionary<string, Tuple<byte[], int>> InstructionsConstantWithIndexRegister =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {@"^\(IX\+(\${0,1}[0-9A-F]{1,3})\),(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xDD,0x36},19)},
                {@"^\(IY\+(\${0,1}[0-9A-F]{1,3})\),(\${0,1}[0-9A-F]{1,3})$",new Tuple<byte[], int>(new byte []{0xFD,0x36},19)}
            };

    }
}
