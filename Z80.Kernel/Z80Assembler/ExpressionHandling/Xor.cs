namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Xor : Operator
    {
        public Xor() : base(ExpressionConstans.Operators.Xor, false, 2, 10)
        {
        }

        public override ushort Calculate()
        {
            ushort op2 = Operands.Pop();
            ushort op1 = Operands.Pop();

            return (ushort)(op1 ^ op2);
        }
    }
}
