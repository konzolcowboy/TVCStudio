namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class High : Operator
    {
        public High() : base(ExpressionConstans.Operators.High, true, 1, 18)
        {
        }

        public override ushort Calculate()
        {
            ushort operand = Operands.Pop();

            return (ushort) (operand >> 0x08);
        }
    }
}
