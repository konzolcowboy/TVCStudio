namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Multiply : Operator
    {
        public Multiply() : base(ExpressionConstans.Operators.Multiply, false, 2, 4)
        {
        }

        public override ushort Calculate()
        {
            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort)(op1 * op2);
        }
    }
}
