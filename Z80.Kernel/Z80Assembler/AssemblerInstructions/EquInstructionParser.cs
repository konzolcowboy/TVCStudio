using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal class EquInstructionParser
    {
        private readonly IReadOnlyDictionary<string, Symbol> m_SymbolTable;
        private readonly AssemblyRow m_Row;

        public EquInstructionParser(AssemblyRow row, IReadOnlyDictionary<string, Symbol> symbolTable)
        {
            m_SymbolTable = symbolTable;
            m_Row = row;
        }

        public ParseResult Parse()
        {
            if(string.IsNullOrEmpty(m_Row.Label))
            {
                return new ParseResult(ParseResultCode.Error, 0, $"Az {m_Row.Instruction.Mnemonic} utasításnak szüksége van cimkére!");
            }

            if (m_Row.Operands == null || m_Row.Operands.Count == 0)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"Az {m_Row.Instruction.Mnemonic} utasításnak szüksége van operandusra!");
            }

            if (m_Row.Operands.Count > 1)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"Az {m_Row.Instruction.Mnemonic} utasításnak csak egy operandusa lehet!");
            }

            var oInfo = m_Row.Operands[0].Info;
            if (oInfo.DataType == OperandType.Literal || oInfo.DataType == OperandType.Register ||
                oInfo.DataType == OperandType.ByteWithIndexRegister || oInfo.DataType == OperandType.DecimalWithIndexRegister ||
                oInfo.DataType == OperandType.JumpCondition)
            {
                return new ParseResult(ParseResultCode.Error, 0, $"Az {m_Row.Instruction.Mnemonic} utasítás nem támogatja a megadott operandust:{m_Row.Operands[0].Value}!");
            }

            if (oInfo.DataType == OperandType.Character)
            {
                var bytestring = m_Row.Operands[0].Value.Replace("'", "").Replace("\"", "");
                return new ParseResult(ParseResultCode.Ok, char.Parse(bytestring).ToTvcAscii());
            }

            var expressionParser = new ExpressionParser(m_Row.Operands[0].Value, m_SymbolTable);

            var result = expressionParser.Parse();

            if (expressionParser.SymbolsInExpression.Any(s => s.Equals(m_Row.Label.ToUpper())))
            {
                return new ParseResult(ParseResultCode.Error, 0, $"Az {m_Row.Instruction.Mnemonic} utasításban rekurzív szimbólum hivatkozás található:{m_Row.Label}!");
            }

            return result;
        }
    }
}
