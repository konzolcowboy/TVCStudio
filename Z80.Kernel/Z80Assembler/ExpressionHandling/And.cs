namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class And: Operator
    {
        public And() : base(ExpressionConstans.Operators.And, false, 2, 12)
        {
        }

        public override ushort Calculate()
        {
            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort)(op1 & op2);
        }
    }
}
