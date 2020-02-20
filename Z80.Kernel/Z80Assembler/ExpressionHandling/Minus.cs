namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Minus : Operator
    {
        public Minus(bool isUnary = false) : base(ExpressionConstans.Operators.Minus, isUnary, isUnary ? 1 : 2, isUnary ? 6 : 2)
        {
        }

        public override ushort Calculate()
        {
            if (IsUnary)
            {
                ushort op = Operands.Pop();
                return (ushort) -op;
            }

            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort)(op1 - op2);
        }
    }
}
