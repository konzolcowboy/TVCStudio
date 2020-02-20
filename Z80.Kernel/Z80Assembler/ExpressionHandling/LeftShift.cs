namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class LeftShift : Operator
    {
        public LeftShift() : base(ExpressionConstans.Operators.LeftShift, false, 2, 14)
        {
        }

        public override ushort Calculate()
        {
            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort)(op1 << op2);
        }
    }
}
