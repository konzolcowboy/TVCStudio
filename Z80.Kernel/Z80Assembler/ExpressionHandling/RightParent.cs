namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class RightParent: Operator
    {
        public RightParent() : base(ExpressionConstans.Operators.RightParent, false, 0, 0)
        {
        }

        public override ushort Calculate()
        {
            return 0;
        }
    }
}
