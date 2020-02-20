namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitInstructions
{
    internal class BitInstructionResolver : BitInstructionResolverBase
    {
        protected override byte GetIndexByte(byte bitNumber)
        {
            switch (bitNumber)
            {
                case 0:
                case 1:
                    return 0x40;
                case 2:
                case 3:
                    return 0x50;
                case 4:
                case 5:
                    return 0x60;
                case 6:
                case 7:
                    return 0x70;
                default:
                    throw new Z80AssemblerException("A 'BIT' utasítás első operandusának 0 és 7 közé kell esnie!");
            }
        }

        protected override int GetClockCycles(string registerString)
        {
            if (registerString.StartsWith(@"(IX+") || registerString.StartsWith(@"(IY+"))
            {
                return 20;
            }

            return registerString.ToUpper().Equals(@"(HL)") ? 12 : 8;
        }

        public BitInstructionResolver() : base("BIT")
        {
        }
    }
}
