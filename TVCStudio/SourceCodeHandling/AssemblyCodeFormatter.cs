using TVCStudio.Settings;
using Z80.Kernel.Z80Assembler;

namespace TVCStudio.SourceCodeHandling
{
    internal class AssemblyCodeFormatter
    {
        public AssemblyCodeFormatter(AssemblyIndentationSettings settings)
        {
            m_Settings = settings;
        }

        public string FormatLine(string inputLine)
        {
            string result = inputLine.TrimStart(' ', '\t');

            // Ignore comment only lines
            if (result.StartsWith(";"))
            {
                return inputLine;
            }

            if (result.StartsWith("#"))
            {
                return result.PadLeft(m_Settings.PreprocessorIndentSize + result.Length);
            }

            try
            {
                AssemblyRowTokenizer tokenizer = new AssemblyRowTokenizer(result, true);
                tokenizer.TokenizeRow();
                if (tokenizer.Tokens.Count == 0)
                {
                    return inputLine;
                }

                AssemblyRowInterpreter interpreter = new AssemblyRowInterpreter(tokenizer.Tokens);
                interpreter.InterpretRow();

                AssemblyRow row = interpreter.InterpretedAssemblyRow;
                string joinedOperands = string.Join(",", row.Operands);
                int labelPaddingSize = row.Label.Length > m_Settings.LabelSectionPaddingSize
                    ? 1
                    : m_Settings.LabelSectionPaddingSize - row.Label.Length;

                int instructionPaddingSize = row.Instruction.Mnemonic.Length > m_Settings.InstructionSectionPaddingSize
                    ? 1
                    : m_Settings
                          .InstructionSectionPaddingSize - row.Instruction.Mnemonic.Length;

                int operandPaddingSize = joinedOperands.Length > m_Settings.OperandSectionPaddingSize
                    ? 1
                    : m_Settings.OperandSectionPaddingSize - joinedOperands.Length;

                result = $"{row.Label.PadRight(labelPaddingSize + row.Label.Length)}" +
                       $"{row.Instruction.Mnemonic.PadRight(instructionPaddingSize + row.Instruction.Mnemonic.Length)}" +
                       $"{joinedOperands.PadRight(operandPaddingSize + joinedOperands.Length)}" +
                       $"{row.Comment}";

                return result.PadLeft(m_Settings.AssemblyRowIndentSize + result.Length);

            }
            catch (Z80AssemblerException)
            {
                return inputLine;
            }
        }

        private readonly AssemblyIndentationSettings m_Settings;
    }
}
