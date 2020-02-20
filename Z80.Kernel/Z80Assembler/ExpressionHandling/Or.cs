using System;

namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Or : Operator
    {
        public Or() : base(ExpressionConstans.Operators.Or, false, 2, 8)
        {
        }

        public override ushort Calculate()
        {
            ushort op1 = Operands.Pop();
            ushort op2 = Operands.Pop();

            return (ushort)(op1 | op2);
        }
    }
}
