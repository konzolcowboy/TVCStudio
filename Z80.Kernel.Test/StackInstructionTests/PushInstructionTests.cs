using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.StackInstructionTests
{
    [TestClass]
    public class PushInstructionTests
    {
        [TestMethod]
        public void PushAf()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "PUSH AF" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF5);
        }
        [TestMethod]
        public void PushBc()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "PUSH BC" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC5);
        }
        [TestMethod]
        public void PushDe()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "PUSH DE" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD5);
        }
        [TestMethod]
        public void PushHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "PUSH HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE5);
        }
        [TestMethod]
        public void PushIx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "PUSH IX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE5);
        }
        [TestMethod]
        public void PushIy()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "PUSH IY" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE5);
        }
    }
}
