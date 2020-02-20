using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions
{
    internal abstract class ArithmeticalInstructionResolverBase : Z80InstructionResolver
    {
        public override bool Resolve(List<Operand> operands)
        {
            string joinedoperands = string.Join(",", operands);
            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in GetSingleRegisterRegexes())
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
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
                var matches = regex.Matches(joinedoperands);
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

            foreach (KeyValuePair<string, Tuple<byte[], int>> keyValuePair in GetRegexesWithOneByteConstants())
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(joinedoperands);
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

        protected abstract Dictionary<string, Tuple<byte[], int>> GetSingleRegisterRegexes();
        protected abstract Dictionary<string, Tuple<byte[], int>> GetRegexesWithIndexRegisters();
        protected abstract Dictionary<string, Tuple<byte[], int>> GetRegexesWithOneByteConstants();
    }
}
