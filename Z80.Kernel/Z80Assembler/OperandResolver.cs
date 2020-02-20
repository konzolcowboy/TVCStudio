using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler
{
    internal class OperandResolver
    {
        private readonly AssemblyRow m_AssemblyRow;
        private readonly IReadOnlyDictionary<string, Symbol> m_SymbolTable;

        public OperandResolver(AssemblyRow row, IReadOnlyDictionary<string, Symbol> symbolTable)
        {
            m_AssemblyRow = row;
            m_SymbolTable = symbolTable;
        }

        public List<Operand> Resolve()
        {
            List<Operand> resolvedOperand = m_AssemblyRow.Operands.Clone();
            foreach (Operand operand in resolvedOperand)
            {
                switch (operand.Info.DataType)
                {
                    case OperandType.ExpressionWithIxIndexRegister:
                    case OperandType.ExpressionWithIyIndexRegister:
                    case OperandType.Expression:
                        ResolveExpressionOperand(operand);
                        break;
                    case OperandType.Unknown:
                        ResolveSymbolOperand(operand);
                        break;
                    case OperandType.Character:
                        {
                            var bytestring = operand.Value.Replace("'", "").Replace("\"", "");
                            operand.Value = char.Parse(bytestring).ToTvcAscii().ByteToHexa();
                            operand.State = OperandState.Valid;
                        }
                        break;
                    default:
                        operand.State = OperandState.Valid;
                        break;
                }
            }

            return resolvedOperand;
        }

        private void ResolveExpressionOperand(Operand operand)
        {
            string expressionString = operand.Value;

            if (operand.Info.DataType == OperandType.ExpressionWithIxIndexRegister ||
                operand.Info.DataType == OperandType.ExpressionWithIyIndexRegister)
            {
                var regex = new Regex(operand.Info.GetRegexStringForOperandType(),
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operand.Value);
                if (matches.Count > 0)
                {
                    expressionString = matches[0].Groups[1].Value;
                }
            }

            var expressionParser = new ExpressionParser(expressionString, m_SymbolTable);
            var result = expressionParser.Parse();
            if (result.ResultCode == ParseResultCode.Error)
            {
                throw new Z80AssemblerException(result.Message);
            }

            operand.State = result.ResultCode == ParseResultCode.Ok ? OperandState.Valid : OperandState.FutureSymbol;

            if (operand.Info.DataType == OperandType.ExpressionWithIxIndexRegister ||
                operand.Info.DataType == OperandType.ExpressionWithIyIndexRegister)
            {
                string operandValueWithIndexRegister = operand.Value.Substring(0, 4); // (IX+ or (IY+
                operand.Value = operandValueWithIndexRegister + result + ")";
            }
            else
            {
                operand.Value = result.ToString();
            }
        }

        private void ResolveSymbolOperand(Operand operand)
        {
            Symbol foundSymbol;
            if (TryGetSymbol(operand.Value, out foundSymbol))
            {
                operand.Value = operand.Value.EnclosedWithBracket() ? $"({foundSymbol})" : foundSymbol.ToString();
                operand.State = foundSymbol.State == SymbolState.Resolved ? OperandState.Valid : OperandState.FutureSymbol;
            }
            else
            {
                throw new Z80AssemblerException($"A(z) {operand} operandus ismeretlen!");
            }
        }
        private bool TryGetSymbol(string symbolName, out Symbol symbol)
        {
            if (symbolName.EnclosedWithBracket())
            {
                symbolName = symbolName.Replace("(", "").Replace(")", "");
            }

            symbol = null;
            var result = m_SymbolTable.Where(kvp => kvp.Key == symbolName).Select(kvp => kvp.Value).FirstOrDefault();
            if (result != null)
            {
                symbol = result;
                return true;
            }

            return false;
        }
    }
}
