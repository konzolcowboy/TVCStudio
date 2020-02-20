using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal class OrgInstructionParser
    {
        private readonly IReadOnlyDictionary<string, Symbol> m_SymbolTable;
        private readonly AssemblyRow m_Row;

        public OrgInstructionParser(AssemblyRow row, IReadOnlyDictionary<string, Symbol> symbolTable)
        {
            m_SymbolTable = symbolTable;
            m_Row = row;
        }

        public ParseResult Parse()
        {
            if (m_Row.Operands == null || m_Row.Operands.Count != 1)
            {
                throw new Z80AssemblerException($"Az {m_Row.Instruction.Mnemonic} utasításnak szüksége van egy operandusa!");
            }

            var oInfo = m_Row.Operands[0].Info;
            if (oInfo.DataType == OperandType.Literal || oInfo.DataType == OperandType.Register ||
                oInfo.DataType == OperandType.ByteWithIndexRegister || oInfo.DataType == OperandType.DecimalWithIndexRegister ||
                oInfo.DataType == OperandType.JumpCondition)
            {
                throw new Z80AssemblerException($"Az {m_Row.Instruction.Mnemonic} utasítás nem támogatja a megadott operandust:{m_Row.Operands[0].Value}!");
            }

            var expressionParser = new ExpressionParser(m_Row.Operands[0].Value, m_SymbolTable);
            var result = expressionParser.Parse();
            if (result.ResultCode == ParseResultCode.Ok && expressionParser.SymbolsInExpression.Any(s => s.Equals(m_Row.Label.ToUpper())))
            {
                return new ParseResult(ParseResultCode.Error, 0,
                    $"Az {m_Row.Instruction.Mnemonic} utasításban rekurzív szimbólum hivatkozás található:{m_Row.Label}!");
            }

            return result;
        }
    }
}
