using System;

namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Sqrt : Operator
    {
        public Sqrt() : base(ExpressionConstans.Operators.Sqrt, true, 1, 16)
        {
        }

        public override ushort Calculate()
        {
            ushort op = Operands.Pop();
            return (ushort) Math.Sqrt(op);
        }
    }
}
