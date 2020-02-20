namespace Z80.Kernel.Z80Assembler.Z80Instructions.BitInstructions
{
    internal class ResInstructionResolver : BitInstructionResolverBase
    {
        protected override byte GetIndexByte(byte bitNumber)
        {
            switch (bitNumber)
            {
                case 0:
                case 1:
                    return 0x80;
                case 2:
                case 3:
                    return 0x90;
                case 4:
                case 5:
                    return 0xA0;
                case 6:
                case 7:
                    return 0xB0;
                default:
                    throw new Z80AssemblerException("A 'RES' utasítás első operandusának 0 és 7 közé kell esnie!");
            }

        }

        protected override int GetClockCycles(string registerString)
        {
            if (registerString.StartsWith(@"(IX+") || registerString.StartsWith(@"(IY+"))
            {
                return 23;
            }

            return registerString.ToUpper().Equals(@"(HL)") ? 15 : 8;
        }

        public ResInstructionResolver() : base("RES")
        {
        }
    }
}
