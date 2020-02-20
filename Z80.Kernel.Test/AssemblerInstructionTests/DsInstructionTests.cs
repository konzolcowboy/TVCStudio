using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Test.AssemblerInstructionTests
{
    [TestClass]
    public class DsInstructionTests
    {
        [TestMethod]
        public void DsWithOneSimpleOperand()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string>{ "ORG $3000", "LABEL    DS   5" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "LABEL", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "DS", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Value == "5", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Decimal, true);
        }
        [TestMethod]
        public void DsWithTwoOperands()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LABEL    DS   5,$FF" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "LABEL", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Instruction.Mnemonic == "DS", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Value == "5", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Decimal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Value == "$FF", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
        }
        [TestMethod]
        public void DsWithTwoOperandsCouldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LABEL    DS   5,$FF" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 5, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0xFF, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0xFF, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0xFF, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0xFF, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0xFF, true);
        }
        [TestMethod]
        public void DsWithCharacterOperandCouldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LABEL    DS   5,'A'" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 5, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x41, true);
        }
        [TestMethod]
        public void DsWithLiteralOperandCouldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LABEL    DS   $3,'ABA'" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 9, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x42, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x42, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[6] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[7] == 0x42, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[8] == 0x41, true);
        }
        [TestMethod]
        public void DsCouldNotBeAssembledWithFirstCharacterOperand()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LABEL    DS   'A','ABA'" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"A(z) 'A' operandus típusa(Character) a DS utasításban nem támogatott!"), true);
        }
        [TestMethod]
        public void DsWithTwoExpressions()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LABEL    DS   (3205-3200),($40+$1)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 5, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x41, true);
        }
    }
}
