using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.SubRoutineInstructionTests
{
    [TestClass]
    public class RetInstructionTests
    {
        [TestMethod]
        public void RetWithC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD8);
        }
        [TestMethod]
        public void RetWithNc()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET NC" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD0);
        }
        [TestMethod]
        public void RetWithZ()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET Z" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC8);
        }
        [TestMethod]
        public void RetWithNz()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET NZ" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC0);
        }
        [TestMethod]
        public void RetWithP()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET P" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF0);
        }
        [TestMethod]
        public void RetWithM()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET M" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF8);
        }
        
        [TestMethod]
        public void RetWithPO()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET PO" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE0);
        }

        [TestMethod]
        public void RetWithP0()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET P0" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE0);
        }
        [TestMethod]
        public void RetWithPe()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET PE" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE8);
        }
        [TestMethod]
        public void RetWithOutCondition()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RET" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC9);
        }
        [TestMethod]
        public void Reti()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RETI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [TestMethod]
        public void Retn()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RETN" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x45);
        }
    }
}
