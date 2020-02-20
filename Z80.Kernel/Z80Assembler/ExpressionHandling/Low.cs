namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Low : Operator
    {
        public Low() : base(ExpressionConstans.Operators.Low, true, 1, 18)
        {
        }

        public override ushort Calculate()
        {
            ushort operand = Operands.Pop();

            return (ushort)(operand & 0x00ff);
        }
    }
}
