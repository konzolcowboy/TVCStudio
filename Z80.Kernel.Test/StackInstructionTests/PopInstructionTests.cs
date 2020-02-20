using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.StackInstructionTests
{
    [TestClass]
    public class PopInstructionTests
    {
        [TestMethod]
        public void PopAf()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "POP AF" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF1);
        }
        [TestMethod]
        public void PopBc()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "POP BC" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC1);
        }
        [TestMethod]
        public void PopDe()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "POP DE" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD1);
        }
        [TestMethod]
        public void PopHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "POP HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE1);
        }
        [TestMethod]
        public void PopIx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "POP IX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE1);
        }
        [TestMethod]
        public void PopIy()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "POP IY" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE1);
        }

    }
}
