using System.Collections.Generic;

namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal abstract class Operator
    {
        public int Priority { get; }
        public int OperandCount { get; }
        public bool IsUnary { get; }

        public string Token { get; }

        public void AddOperand(ushort operand)
        {
            Operands.Push(operand);
        }

        public static Operator Create(string token, bool unary = false)
        {
            switch (token)
            {
                case ExpressionConstans.Operators.LeftParent: return new LeftParent();
                case ExpressionConstans.Operators.RightParent: return new RightParent();
                case ExpressionConstans.Operators.Plus: return new Plus(unary);
                case ExpressionConstans.Operators.Minus: return new Minus(unary);
                case ExpressionConstans.Operators.Multiply: return new Multiply();
                case ExpressionConstans.Operators.Divide: return new Divide();
                case ExpressionConstans.Operators.And: return new And();
                case ExpressionConstans.Operators.Or: return new Or();
                case ExpressionConstans.Operators.Xor: return new Xor();
                case ExpressionConstans.Operators.LeftShift: return new LeftShift();
                case ExpressionConstans.Operators.RightShift: return new RightShift();
                case ExpressionConstans.Operators.OnesComplement: return new OnesComplement();
                case ExpressionConstans.Operators.Sqrt: return new Sqrt();
                case ExpressionConstans.Operators.Abs: return new Abs();
                case ExpressionConstans.Operators.High: return new High();
                case ExpressionConstans.Operators.Low: return new Low();
                default: throw new Z80AssemblerException($"Nem támogatott operátor:{token}");
            }
        }

        public static bool operator <=(Operator op1, Operator op2)
        {
            return op1.Priority <= op2.Priority;
        }

        public static bool operator >=(Operator op1, Operator op2)
        {
            return op1.Priority >= op2.Priority;
        }

        public abstract ushort Calculate();

        protected Operator(string token, bool isUnary, int operandCount, int priority)
        {
            Operands = new Stack<ushort>();
            IsUnary = isUnary;
            OperandCount = operandCount;
            Priority = priority;
            Token = token;
        }

        protected readonly Stack<ushort> Operands;
    }
}
