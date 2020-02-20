namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class LeftParent:Operator
    {
        public LeftParent() : base(ExpressionConstans.Operators.LeftParent, false, 0, 0)
        {
        }

        public override ushort Calculate()
        {
            return 0;
        }
    }
}
