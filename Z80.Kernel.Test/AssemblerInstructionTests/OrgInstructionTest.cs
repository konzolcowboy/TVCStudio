using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.AssemblerInstructionTests
{
    [TestClass]
    public class OrgInstructionTest
    {
        [TestMethod]
        public void OrgIsAssembledWithWord()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $FA66" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.LocationCounter == 0xFA66, true);
        }
        [TestMethod]
        public void OrgIsAssembledWithByte()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $66" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.LocationCounter == 0x0066, true);
        }
        [TestMethod]
        public void OrgIsNotAssembledWithLiteral()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG 'Literal'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Az ORG utasítás nem támogatja a megadott operandust"), true);
        }
        [TestMethod]
        public void OrgIsAssembledWithDecimal()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG 32456" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.LocationCounter == 0x7EC8, true);
        }
        [TestMethod]
        public void OrgNeedsOperand()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG " });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Az ORG utasításnak szüksége van egy operandusa!"), true);
        }
        [TestMethod]
        public void OrgCouldBeAssembledWithLcConstant()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000","RET","ORG .LC","RET" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 4, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections.Count ==2, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections[0].ProgramStartAddress == 0x3000, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections[0].SectionLength ==1, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections[1].ProgramStartAddress == 0x3001, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections[0].SectionLength == 1, true);
        }
        [TestMethod]
        public void OrgIsAssembledWithExpression()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG (32000+400)+56" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.LocationCounter == 0x7EC8, true);
        }
    }
}
