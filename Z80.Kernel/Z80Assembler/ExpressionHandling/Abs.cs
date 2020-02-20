using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z80.Kernel.Z80Assembler.ExpressionHandling
{
    internal class Abs : Operator
    {
        public Abs() : base(ExpressionConstans.Operators.Abs, true, 1, 18)
        {
        }

        public override ushort Calculate()
        {
            short operand = (short) Operands.Pop();

            return (ushort)Math.Abs(operand);
        }
    }
}
