namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class RightShift : Operator
    {
        public RightShift() : base(ExpressionConstans.Operators.RightShift, false, 2, 14)
        {
        }

        public override ushort Calculate()
        {
            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort)(op1 >> op2);
        }
    }
}
