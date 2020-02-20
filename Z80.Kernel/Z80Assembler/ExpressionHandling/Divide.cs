namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Divide : Operator
    {
        public Divide() : base(ExpressionConstans.Operators.Divide, false, 2, 4)
        {
        }

        public override ushort Calculate()
        {
            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            if (op2 == 0)
            {
                throw  new Z80AssemblerException("Nullával történő osztás a matematikai kifejezésben!");
            }

            return (ushort)(op1 / op2);
        }
    }
}
