using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.ArithmeticalInstructionTests
{
    [TestClass]
    public class Bit16DecInstructionTests
    {
        [TestMethod]
        public void DecBc()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC BC" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x0B);
        }
        [TestMethod]
        public void DecDe()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC DE" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x1B);
        }
        [TestMethod]
        public void DecHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x2B);
        }
        [TestMethod]
        public void DecIx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC IX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2B);
        }
        [TestMethod]
        public void DecIy()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC IY" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2B);
        }
        [TestMethod]
        public void DecSp()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC SP" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3B);
        }

    }
}
