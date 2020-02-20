using System.Collections.Generic;
using System.Linq;
using Z80.Kernel.Z80Assembler.Z80Instructions.ArithmeticalInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.BitInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.BitRotationInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.BitshiftingInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.CompareInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.ExchangeInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.JumpInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.LoadingInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.LogicalInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.OtherInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.PortInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.StackInstructions;
using Z80.Kernel.Z80Assembler.Z80Instructions.SubRoutineInstructions;

namespace Z80.Kernel.Z80Assembler.Z80Instructions
{
    internal sealed class Z80InstructionBuilderFactory
    {
        public IZ80InstructionBuilder GetInstructionBuilder(string mnemonic, List<Operand> operands, ushort actualInstructionAddress = 0)
        {
            if (LoadingInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new LoadingInstructionBuilder(mnemonic, operands);
            }
            if (ArithmeticalInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new ArithmeticalInstructionBuilder(mnemonic, operands);
            }
            if (LogicalInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new LogicalInstructionBuilder(mnemonic, operands);
            }
            if (CompareInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new CompareInstructionBuilder(mnemonic, operands);
            }
            if (BitInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new BitInstructionBuilder(mnemonic, operands);
            }
            if (BitShiftingInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new BitShiftingInstructionBuilder(mnemonic, operands);
            }
            if (BitRotationInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new BitRotationInstructionBuilder(mnemonic, operands);
            }
            if (PortInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new PortInstructionBuilder(mnemonic, operands);
            }
            if (SubRoutineInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new SubRoutineInstructionBuilder(mnemonic, operands);
            }
            if (StackInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new StackInstructionBuilder(mnemonic, operands);
            }
            if (ExchangeInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new ExchangeInstructionBuilder(mnemonic, operands);
            }
            if (OtherInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new OtherInstructionBuilder(mnemonic, operands);
            }
            if (RelativeJumpInstructions.Any(inst => inst.Equals(mnemonic)))
            {
                return new RelativeJumpInstructionBuilder(mnemonic, operands,actualInstructionAddress);
            }
            if (mnemonic == JumpInstruction)
            {
                return new JumpInstructionBuilder(mnemonic, operands);
            }
            return null;
        }
        private static string[] LoadingInstructions { get; } = { "LD", "LDIR", "LDDR", "LDI", "LDD" };
        private static string[] ArithmeticalInstructions { get; } = { "ADC", "ADD", "SBC", "SUB", "INC", "DEC" };
        private static string[] LogicalInstructions { get; } = { "AND", "OR", "XOR", "NEG", "CPL" };
        private static string[] CompareInstructions { get; } = { "CP", "CPD", "CPI", "CPDR", "CPIR", "DAA" };
        private static string[] BitInstructions { get; } = { "BIT", "SET", "RES" };
        private static string[] BitShiftingInstructions { get; } = { "SLA", "SRA", "SRL","SLL" };
        private static string[] BitRotationInstructions { get; } = { "RL", "RLC", "RR", "RRC", "RLA", "RLCA", "RRA", "RRCA", "RLD", "RRD", "CCF", "SCF" };
        private static string[] PortInstructions { get; } = { "IN", "OUT", "IND", "INDR", "INI", "INIR", "OUTD", "OTDR", "OUTI", "OTIR" };
        private static string[] SubRoutineInstructions { get; } = { "CALL", "RST", "RET", "RETI", "RETN" };
        private static string[] StackInstructions { get; } = { "PUSH", "POP" };
        private static string[] ExchangeInstructions { get; } = { "EX", "EXX" };
        private static string[] OtherInstructions { get; } = { "IM", "DI","EI","HALT","NOP" };
        private static string[] RelativeJumpInstructions { get; } = { "JR", "DJNZ" };
        private static string JumpInstruction { get; } = "JP";
    }
}
