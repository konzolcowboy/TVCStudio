using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class Z80AssemberTest
    {
        [DataTestMethod]
        [DataRow("")]
        [DataRow(";This is comment")]
        [DataRow(";")]
        public void AssemblerNotInterpretComments(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(!result && assembler.InterPretedAssemblyRows.Count == 0,true);
        }

        [DataTestMethod]
        [DataRow("FGHJKL:")]
        [DataRow("  FGHJKL:   ")]
        [DataRow("  FGHJKL: ; This is comment  ")]
        public void AssemblerIsNotInterpretLabelWithoutInstruction(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Cimke nem állhat utasítás nélkül"), true);
        }

        [DataTestMethod]
        [DataRow("FGHJKL: ORG $3000  ;This is comment")]
        [DataRow(" FGHJKL: ORG $3000    ;  This is comment")]
        [DataRow("FGHJKL: ORG $3000   ")]
        public void AssemblerInterpretLabelAndInstruction(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Label == "FGHJKL", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Mnemonic == "ORG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Type == InstructionType.AssemblerInstruction, true);

        }

        [DataTestMethod]
        [DataRow("\t\tFGHJKL:\t\tORG\t\t$3000\t\t;\tThis is comment")]
        [DataRow(" \tFGHJKL:\tORG     \t$3000    ;\t This is comment")]
        [DataRow("FGHJKL: ORG\t$3000\t\t")]
        public void AssemblerInterpretLabelAndInstructionWithTabs(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Label == "FGHJKL", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Mnemonic == "ORG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Type == InstructionType.AssemblerInstruction, true);

        }

        [DataTestMethod]
        [DataRow("FGHJKL_1: ORG $3000  ;This is comment")]
        [DataRow(" FGHJKL_1: ORG $3000    ;  This is comment")]
        [DataRow("FGHJKL_1: ORG $3000   ")]
        public void AssemblerInterpretLabelWithUnderScoreAndInstruction(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Label == "FGHJKL_1", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Mnemonic == "ORG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Type == InstructionType.AssemblerInstruction, true);

        }

        [DataTestMethod]
        [DataRow("FGH_JKL_1: ORG $3000  ;This is comment")]
        [DataRow(" FGH_JKL_1: ORG $3000    ;  This is comment")]
        [DataRow("FGH_JKL_1: ORG $3000   ")]
        public void AssemblerInterpretLabelWithDoubleUnderScoreAndInstruction(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Label == "FGH_JKL_1", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Mnemonic == "ORG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[0].Instruction.Type == InstructionType.AssemblerInstruction, true);

        }

        [DataTestMethod]
        [DataRow("  RET     ;This is comment")]
        [DataRow("RET;This is comment")]
        public void AssemblerInterpretOperandlessInstructionwithComment(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 0, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "RET", true);
        }

        [DataTestMethod]
        [DataRow("1FGHJKL: ORG $3000  ;This is comment")]
        [DataRow(" 2FGHJKL: ORG $3000    ;  This is comment")]
        [DataRow("345FGHJKL: ORG $3000   ")]
        public void AssemblerDoesNotInterpretLabelStartsWithNumberOrOtherCharacter(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("A cimke csak betűvel kezdődhet"));
        }

        [DataTestMethod]
        [DataRow("LD D,0  ;This is comment")]
        [DataRow(" LD   D,0  ;This is comment")]
        [DataRow("LD   D,0  ;This is comment")]
        public void AssemblerInterpretInstructionAndOperands(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> {"ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == string.Empty, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "D", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Register, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "0", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Decimal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "LD", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Type == InstructionType.ProcessorInstruction, true);
        }
        [DataTestMethod]
        [DataRow("START: LD D,0  ;This is comment")]
        [DataRow("START: LD D,0  ;This is comment")]
        [DataRow("START: LD D,0")]
        [DataRow("   START:   LD D,0   ")]
        [DataRow("START:LD D,0")]
        [DataRow("START LD D,0  ;This is comment")]
        [DataRow("START LD D,0  ;This is comment")]
        [DataRow("START LD D,0")]
        [DataRow("   START   LD D,0   ")]
        public void AssemblerInterpretLabelInstructionAndOperands(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "START", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "D", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Register, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "0", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Decimal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "LD", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Type == InstructionType.ProcessorInstruction, true);
        }
        [DataTestMethod]
        [DataRow("START LD  A,(IX+3)")]
        [DataRow("START LD   A,(IX+3)  ;  ")]
        [DataRow("START LD   A,(IX+3)  ;This is comment")]
        [DataRow("  START LD A,(IX+3)")]
        public void AssemblerInterpretLabelInstructionAndOperands_2(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "START", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "A", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Register, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "(IX+3)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.DecimalWithIndexRegister, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "LD", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Type == InstructionType.ProcessorInstruction, true);
        }

        [DataTestMethod]
        [DataRow("START LD  (IX+3),H")]
        [DataRow("START LD   (IX+3),H  ;  ")]
        [DataRow("START LD   (IX+3),H  ;This is comment")]
        [DataRow("  START LD (IX+3),H")]
        public void AssemblerInterpretLabelInstructionAndOperands_3(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "START", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "H", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Register, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "(IX+3)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.DecimalWithIndexRegister, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "LD", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Type == InstructionType.ProcessorInstruction, true);
        }

        [DataTestMethod]
        [DataRow("START LD  HL,($FFAD)")]
        [DataRow("START LD   HL,($FFAD)  ;  ")]
        [DataRow("START LD   HL,($FFAD)  ;This is comment")]
        [DataRow("  START LD HL,($FFAD)")]
        public void AssemblerInterpretLabelInstructionAndHexaOperand(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "START", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "HL", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Register, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "($FFAD)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.WordAddress, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "LD", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Type == InstructionType.ProcessorInstruction, true);
        }

        [DataTestMethod]
        [DataRow("START LD  (HL),123")]
        [DataRow("START LD   (HL),123  ;  ")]
        [DataRow("START LD   (HL),123  ;This is comment")]
        [DataRow("  START LD (HL),123")]

        public void AssemblerInterpretLabelInstructionAndDecimalOperand(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool successBuildProgram = assembler.BuildProgram();

            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "START", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "(HL)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Register, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "123", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Decimal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "LD", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Type == InstructionType.ProcessorInstruction, true);
        }

        [DataTestMethod]
        [DataRow("START# L,D  A,(IX+3)")]
        [DataRow("    START L,D ,; A,(IX+3)")]
        [DataRow("START@ L,D ,; A,(IX+3)")]
        [DataRow("IX+6,HL")]
        [DataRow("  IX+6,HL:   ")]
        [DataRow("  IX+6,,HL: ; This is comment  ")]
        [DataRow("    START: L,D: ; A,(IX+3)")]
        [DataRow("  START LD$FFAD,HL")]
        public void AssemblerCannotInterpretTheRowBecauseOfInvalidCharacteersAndSyntax(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
        }
        [DataTestMethod]
        [DataRow("LD LD A,$5")]
        [DataRow("    START LD LD A,$5")]
        public void DoubleInstructionCouldNotBeBuilt(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { row });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
        }
    }
}
