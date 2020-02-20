namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class OnesComplement : Operator
    {
        public OnesComplement() : base(ExpressionConstans.Operators.OnesComplement, true, 1, 6)
        {
        }

        public override ushort Calculate()
        {
            ushort op = Operands.Pop();
            return (ushort) ~op;
        }
    }
}
