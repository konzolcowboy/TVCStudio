using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Z80.Kernel.Z80Assembler
{
    public enum AssemblyRowState
    {
        Created,
        Interpreted,
        ContainsFutureSymbol,
        Assembled
    }

    public enum InstructionType
    {
        Unknown,
        ProcessorInstruction,
        AssemblerInstruction
    }

    public class Instruction
    {
        public Instruction()
        {
            Mnemonic = string.Empty;
            Type = InstructionType.Unknown;
        }

        public string Mnemonic { get; set; }
        public InstructionType Type { get; set; }

    }

    public class AssemblyRow
    {
        public string Label { get; set; }

        public string Comment { get; set; }

        public Instruction Instruction { get; set; }

        public List<Operand> Operands { get; set; }

        public List<byte> InstructionBytes { get; set; }

        public ushort Address { get; set; }

        public AssemblyRowState State { get; set; }

        public int RowNumber { get; set; }

        public int ClockCycles{get; set;}

        public AssemblyRow()
        {
            Label = string.Empty;
            Comment = string.Empty;
            Instruction = new Instruction();
            Operands = new List<Operand>();
            InstructionBytes = new List<byte>();
            Address = 0;
            State = AssemblyRowState.Created;
            ClockCycles = 0;
        }

        public override string ToString()
        {
            var addressString = Instruction.Mnemonic.ToUpper() != "ORG" && Instruction.Mnemonic.ToUpper() != "END" &&
                                Instruction.Mnemonic.ToUpper() != "EQU"
                ? Address.ToString("X4")
                : "";

            if (InstructionBytes.Count == 0)
            {
                return $"{addressString.PadRight(6)}{"".PadRight(10)}{Label.PadRight(15)}{Instruction.Mnemonic.PadRight(5)}{string.Join(",", Operands)}\n";
            }

            var resultString = new StringBuilder();
            var chunkedInstructionArray = SplitInstructionBytesIntoFourByteArrays();

            for (int i = 0; i < chunkedInstructionArray.Length; i++)
            {
                if (i == 0)
                {
                    resultString.AppendLine(
                        Instruction.Type == InstructionType.ProcessorInstruction
                            ? $"{addressString.PadRight(6)}{InstructionBytesToString(chunkedInstructionArray[i]).PadRight(10)}{Label.PadRight(15)}{Instruction.Mnemonic.PadRight(5)}{string.Join(",", Operands).PadRight(15)}[{ClockCycles}]"
                            : $"{addressString.PadRight(6)}{InstructionBytesToString(chunkedInstructionArray[i]).PadRight(10)}{Label.PadRight(15)}{Instruction.Mnemonic.PadRight(5)}{string.Join(",", Operands)}");

                    continue;
                }
                resultString.AppendLine($"{"".PadRight(6)}{InstructionBytesToString(chunkedInstructionArray[i]).PadRight(10)}");
            }
            return resultString.ToString();

        }

        private string InstructionBytesToString(byte[] chunkedInstructionBytes)
        {
            string result = "";
            foreach (byte b in chunkedInstructionBytes)
            {
                result += b.ToString("X2");
            }
            return result;
        }

        private byte[][] SplitInstructionBytesIntoFourByteArrays()
        {
            int chunkSizeCounter = 0;
            const int chunkSize = 4;

            var query = from b in InstructionBytes
                        let num = chunkSizeCounter++
                        group b by num / chunkSize into g
                        select g.ToArray();

            return query.ToArray();
        }
    }
}
