using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal enum ParseResultCode
    {
        Ok,
        ContainsFutureSymbol,
        Error
    }

    internal class ParseResult
    {
        public ParseResult(ParseResultCode resultCode, ushort resultValue = 0, string message = "", bool isPointer = false)
        {
            ResultCode = resultCode;
            Message = message;
            ResultValue = resultValue;
            IsAddress = isPointer;
        }

        public override string ToString()
        {
            if (ResultValue <= 0xff)
            {
                byte byteresult = (byte)ResultValue;
                return IsAddress ? $"({byteresult.ByteToHexa()})" : byteresult.ByteToHexa();
            }

            return IsAddress ? $"({ResultValue.UshortToHexa()})" : ResultValue.UshortToHexa();
        }

        public ushort ResultValue { get; }

        public bool IsAddress { get; set; }

        public string Message { get; }

        public ParseResultCode ResultCode { get; }
    }

    internal class ExpressionParser
    {
        public bool IsAddress { get; }
        public IReadOnlyList<string> SymbolsInExpression => m_SymbolsInExpression;
        public static IReadOnlyDictionary<string, Operator> SupportedOperators => SSupportedOperators;
        public ExpressionParser(string expression, IReadOnlyDictionary<string, Symbol> symbolTable = null)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new Z80AssemblerException("Üres operátor kifejezés!");
            }

            m_SymbolsInExpression = new List<string>();
            m_Expression = expression;
            m_SymbolTable = symbolTable ?? new Dictionary<string, Symbol>(0);

            IsAddress = expression.StartsWith("((") && expression.EndsWith("))");
        }
        public ParseResult Parse()
        {
            try
            {
                string rpnString = ConvertToRpn(FormatString(m_Expression));
                return Calculate(rpnString);
            }
            catch (Z80AssemblerException e)
            {
                return new ParseResult(ParseResultCode.Error, 0, e.Message);
            }
        }

        #region Private Method(s)

        private string FormatString(string expression)
        {

            StringBuilder formattedString = new StringBuilder();
            int balanceOfParenth = 0; // Check number of parenthesis

            // Format string in one iteration and check number of parenthesis
            foreach (char ch in expression)
            {
                switch (ch)
                {
                    case ' ': continue;
                    case '(':
                        balanceOfParenth++;
                        formattedString.Append(char.IsUpper(ch) ? ch : char.ToUpper(ch));
                        break;
                    case ')':
                        balanceOfParenth--;
                        formattedString.Append(char.IsUpper(ch) ? ch : char.ToUpper(ch));
                        break;
                    default:
                        formattedString.Append(char.IsUpper(ch) ? ch : char.ToUpper(ch));
                        break;
                }
            }

            if (balanceOfParenth != 0)
            {
                throw new Z80AssemblerException($"Hibás zárójelezés a következő kifejezésben : '{m_Expression}'!");
            }

            return formattedString.ToString();
        }

        private string ConvertToRpn(string expression)
        {
            int pos = 0; // Current position of lexical analysis
            StringBuilder outputString = new StringBuilder();
            Stack<string> stack = new Stack<string>();

            // While there is unhandled char in expression
            while (pos < expression.Length)
            {
                string token = GetTokenFromInfixNotation(expression, ref pos);
                InterpretToken(token, outputString, stack);
            }

            // Pop all elements from stack to output string            
            while (stack.Count > 0)
            {
                // There should be only operators
                if (stack.Peek()[0] == OperatorMarker[0])
                {
                    outputString.Append(stack.Pop());
                }
                else
                {
                    throw new Z80AssemblerException($"Függvény hívás zárójelek nélkül:{stack.Peek()}");
                }
            }

            return outputString.ToString();
        }


        private string GetTokenFromInfixNotation(string expression, ref int pos)
        {
            // Receive first char
            StringBuilder token = new StringBuilder();
            token.Append(expression[pos]);

            // If it is an operator
            if (SupportedOperators.ContainsKey(token.ToString()))
            {
                // Determine if it is unary or binary operator
                bool isUnary = pos == 0 || expression[pos - 1] == ExpressionConstans.Operators.LeftParent[0];
                pos++;

                switch (token.ToString())
                {
                    case ExpressionConstans.Operators.Plus:
                        return isUnary
                            ? OperatorMarker + ExpressionConstans.Operators.UnaryPlus
                            : OperatorMarker + ExpressionConstans.Operators.Plus;
                    case ExpressionConstans.Operators.Minus:
                        return isUnary
                            ? OperatorMarker + ExpressionConstans.Operators.UnaryMinus
                            : OperatorMarker + ExpressionConstans.Operators.Minus;
                    case ExpressionConstans.Operators.LeftParent:
                    case ExpressionConstans.Operators.RightParent:
                        return token.ToString();
                    default:
                        return OperatorMarker + token;
                }
            }

            if (char.IsDigit(token[0]))
            {
                // Read the whole part of number
                while (++pos < expression.Length
                       && char.IsDigit(expression[pos]))
                {
                    token.Append(expression[pos]);
                }

                return NumberMaker + token;
            }

            if (token.ToString().Equals(HexaNumberMarker))
            {
                while (++pos < expression.Length
                       && expression[pos].IsHexa())
                {
                    token.Append(expression[pos]);
                }

                return token.ToString();
            }

            if (IsSymbolChar(token[0]))
            {
                // Read function or smybol name. 

                while (++pos < expression.Length
                       && IsSymbolChar(expression[pos]))
                {
                    token.Append(expression[pos]);
                }

                // The token is a function or operand
                if (SupportedOperators.ContainsKey(token.ToString()))
                {
                    return OperatorMarker + SupportedOperators[token.ToString()].Token;
                }

                // The token is a symbol
                if (m_SymbolTable.ContainsKey(token.ToString()))
                {
                    m_SymbolsInExpression.Add(token.ToString());
                    return SymbolMarker + token;
                }

                throw new Z80AssemblerException($"Ismeretlen szimbólum:'{token}' a kifejezésben:'{m_Expression}'");
            }


            throw new Z80AssemblerException($"A karakter:{token[0]} helytelen a kifejezésben:{m_Expression}");
        }

        private bool IsSymbolChar(char c)
        {
            var query = from s in SupportedOperators.Keys
                        where s.Length == 1
                        select s[0];

            return query.All(ch => ch != c);
        }

        private void InterpretToken(string token, StringBuilder outputString, Stack<string> stack)
        {
            // If it's a number or symbol just put to string            
            if (token[0] == NumberMaker[0] ||
                token[0] == HexaNumberMarker[0] ||
                token[0] == SymbolMarker[0])
            {
                outputString.Append(token);
            }
            else if (token == ExpressionConstans.Operators.LeftParent)
            {
                stack.Push(token);
            }
            else if (token == ExpressionConstans.Operators.RightParent)
            {
                // If its ')' pop elements from stack to output string
                // until find the ')'

                string elem;
                while ((elem = stack.Pop()) != ExpressionConstans.Operators.LeftParent)
                {
                    outputString.Append(elem);
                }
            }
            else
            {
                // the token is an operator
                // all higher priority operator must be removed from stack
                while (stack.Count > 0 &&
                       SupportedOperators[token.Replace(OperatorMarker, "")]
                       <= SupportedOperators[stack.Peek().Replace(OperatorMarker, "")])
                {
                    outputString.Append(stack.Pop());
                }

                // insert the operator at the stack with lowest priority
                stack.Push(token);
            }
        }

        private ParseResult Calculate(string expression)
        {
            try
            {
                int pos = 0; // Current position of lexical analysis
                var stack = new Stack<ushort>(); // Contains operands

                // Analyse entire expression
                while (pos < expression.Length)
                {
                    string token = LexicalAnalysisRPN(expression, ref pos);
                    var resultCode = SyntaxAnalysisRpn(stack, token);
                    if (resultCode == ParseResultCode.ContainsFutureSymbol)
                    {
                        return new ParseResult(resultCode, 0, string.Empty, IsAddress);
                    }
                }

                // At end of analysis in stack should be only one operand (result)
                if (stack.Count > 1)
                {
                    throw new Z80AssemblerException($"Felesleges operandus a kifejezésben:{m_Expression}");
                }

                return new ParseResult(ParseResultCode.Ok, stack.Pop(), string.Empty, IsAddress);
            }
            catch (Z80AssemblerException e)
            {
                return new ParseResult(ParseResultCode.Error, 0, e.Message);
            }
        }

        private string LexicalAnalysisRPN(string expression, ref int pos)
        {
            StringBuilder token = new StringBuilder();

            // Read token from marker to next marker

            token.Append(expression[pos++]);

            while (pos < expression.Length && expression[pos] != NumberMaker[0]
                   && expression[pos] != OperatorMarker[0]
                   && expression[pos] != HexaNumberMarker[0]
                   && expression[pos] != SymbolMarker[0])
            {
                token.Append(expression[pos++]);
            }

            return token.ToString();
        }
        private ParseResultCode SyntaxAnalysisRpn(Stack<ushort> stack, string token)
        {
            // if it's operand then just push it to stack
            if (token[0] == NumberMaker[0])
            {
                stack.Push(token.Remove(0, 1).ResolveUshortConstant());
            }
            else if (token[0] == HexaNumberMarker[0])
            {
                stack.Push(token.ResolveUshortConstant());
            }
            else if (token[0] == SymbolMarker[0])
            {
                Symbol s = m_SymbolTable[token.Remove(0, 1)];
                if (s.State == SymbolState.Unresolved)
                {
                    return ParseResultCode.ContainsFutureSymbol;
                }
                stack.Push(s.Value.ToString().ResolveUshortConstant());
            }

            // Otherwise apply operator or function to elements in stack
            else
            {
                Operator op = SupportedOperators[token.Remove(0, 1)];

                if (stack.Count < op.OperandCount)
                {
                    throw new Z80AssemblerException($"Hibás operátor használat, vagy hiányzó zárójel a következő kifejezésben:{m_Expression}");
                }
                for (int i = 0; i < op.OperandCount; i++)
                {
                    op.AddOperand(stack.Pop());
                }

                stack.Push(op.Calculate());
            }

            return ParseResultCode.Ok;
        }
        #endregion

        #region Constans
        private const string NumberMaker = "#";
        private const string HexaNumberMarker = "$";
        private const string OperatorMarker = "?";
        private const string SymbolMarker = "@";
        #endregion

        private static readonly Dictionary<string, Operator> SSupportedOperators =
            new Dictionary<string, Operator>
            {
                { ExpressionConstans.Operators.Plus,Operator.Create(ExpressionConstans.Operators.Plus)},
                { ExpressionConstans.Operators.UnaryPlus,Operator.Create(ExpressionConstans.Operators.Plus,true)},
                { ExpressionConstans.Operators.Minus,Operator.Create(ExpressionConstans.Operators.Minus)},
                { ExpressionConstans.Operators.UnaryMinus,Operator.Create(ExpressionConstans.Operators.Minus,true)},
                { ExpressionConstans.Operators.Multiply,Operator.Create(ExpressionConstans.Operators.Multiply)},
                { ExpressionConstans.Operators.Divide,Operator.Create(ExpressionConstans.Operators.Divide)},
                { ExpressionConstans.Operators.And,Operator.Create(ExpressionConstans.Operators.And)},
                { ExpressionConstans.Operators.Or,Operator.Create(ExpressionConstans.Operators.Or)},
                { ExpressionConstans.Operators.Xor,Operator.Create(ExpressionConstans.Operators.Xor)},
                { ExpressionConstans.Operators.LeftShift,Operator.Create(ExpressionConstans.Operators.LeftShift)},
                { ExpressionConstans.Operators.RightShift,Operator.Create(ExpressionConstans.Operators.RightShift)},
                { ExpressionConstans.Operators.OnesComplement,Operator.Create(ExpressionConstans.Operators.OnesComplement)},
                { ExpressionConstans.Operators.Sqrt,Operator.Create(ExpressionConstans.Operators.Sqrt)},
                { ExpressionConstans.Operators.Abs,Operator.Create(ExpressionConstans.Operators.Abs)},
                { ExpressionConstans.Operators.High,Operator.Create(ExpressionConstans.Operators.High)},
                { ExpressionConstans.Operators.Low,Operator.Create(ExpressionConstans.Operators.Low)},
                { ExpressionConstans.Operators.LeftParent,Operator.Create(ExpressionConstans.Operators.LeftParent)},
                { ExpressionConstans.Operators.RightParent,Operator.Create(ExpressionConstans.Operators.RightParent)}
            };

        private readonly string m_Expression;
        private readonly IReadOnlyDictionary<string, Symbol> m_SymbolTable;
        private readonly List<string> m_SymbolsInExpression;
    }
}
