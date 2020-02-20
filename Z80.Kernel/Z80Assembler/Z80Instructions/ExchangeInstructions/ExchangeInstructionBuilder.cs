using System;
using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.Z80Instructions.ExchangeInstructions
{
    internal class ExchangeInstructionBuilder : IZ80InstructionBuilder
    {
        public byte[] InstructionBytes
        {
            get; private set;
        }

        public ExchangeInstructionBuilder(string instruction, List<Operand> operands)
        {
            m_Instruction = instruction;
            m_Operands = operands;

        }
        public void Build()
        {
            if (m_Instruction == "EXX")
            {
                if (m_Operands != null && m_Operands.Count > 0)
                {
                    throw new Z80AssemblerException($"Az '{m_Instruction}' utasításnak nincs szüksége operandusra!");
                }

                InstructionBytes = new byte[] { 0xD9 };
                ClockCycles = 4;
                return;
            }

            if (m_Instruction == "EX")
            {
                string joinedoperands = string.Join(",", m_Operands);
                if (PossibleOperands.ContainsKey(joinedoperands))
                {
                    var kvp = PossibleOperands[joinedoperands];
                    InstructionBytes = kvp.Item1;
                    ClockCycles = kvp.Item2;
                    return;
                }

                throw new Z80AssemblerException($"Hibás operandusok a(z) '{m_Instruction}' utasításnak!");
            }

            throw new Z80AssemblerException($"A(z) '{m_Instruction}' utasítás ismeretlen!");
        }

        public int ClockCycles
        {
            get; private set;
        }

        private static Dictionary<string, Tuple<byte[], int>> PossibleOperands
        {
            get;
        } =
            new Dictionary<string, Tuple<byte[], int>>
            {
                {"(SP),HL", new Tuple<byte[], int>(new byte[] {0xE3},19)},
                {"(SP),IX", new Tuple<byte[], int>(new byte[] {0xDD,0xE3},23)},
                {"(SP),IY", new Tuple<byte[], int>(new byte[] {0xFD,0xE3},23)},
                {"AF,AF'", new Tuple<byte[], int>(new byte[] {0x08},4)},
                {"DE,HL", new Tuple<byte[], int>(new byte[] {0xEB},4)}
            };

        private readonly string m_Instruction;
        private readonly List<Operand> m_Operands;

    }
}
