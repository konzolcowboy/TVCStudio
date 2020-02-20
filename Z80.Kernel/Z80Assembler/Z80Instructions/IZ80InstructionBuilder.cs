namespace Z80.Kernel.Z80Assembler.Z80Instructions
{
    internal interface IZ80InstructionBuilder
    {
        byte[] InstructionBytes { get; }

        void Build();

        int ClockCycles { get; }
    }

}
