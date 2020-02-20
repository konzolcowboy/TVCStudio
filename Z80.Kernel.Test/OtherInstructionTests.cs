using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class OtherInstructionTests
    {
        [TestMethod]
        public void Im0()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IM 0" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x46);
        }
        [TestMethod]
        public void Im1()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IM 1" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x56);
        }
        [TestMethod]
        public void Im2()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IM 2" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5E);
        }
        [TestMethod]
        public void Di()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF3);
        }
        [TestMethod]
        public void Ei()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFB);
        }
        [TestMethod]
        public void Halt()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "HALT" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x76);
        }

    }
}
