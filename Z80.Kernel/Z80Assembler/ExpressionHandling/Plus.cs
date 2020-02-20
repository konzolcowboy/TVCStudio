namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Plus : Operator
    {
        public Plus(bool isUnary = false) : base(ExpressionConstans.Operators.Plus, isUnary, isUnary ? 1 : 2, isUnary ? 6 : 2)
        {
        }

        public override ushort Calculate()
        {
            if (IsUnary)
            {
                ushort op = Operands.Pop();
                return op;
            }

            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort) (op1 + op2);
        }
    }
}
