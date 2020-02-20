using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class ExchangeInstructionTests
    {
        [TestMethod]
        public void ExIntoSpPointerFromHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EX (SP),HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE3);
        }
        [TestMethod]
        public void ExIntoSpPointerFromIx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EX (SP),IX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE3);
        }
        [TestMethod]
        public void ExIntoSpPointerFromIy()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EX (SP),IY" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE3);
        }
        [TestMethod]
        public void ExIntoAfFromTheirPair()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EX AF,AF'" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x08);
        }
        [TestMethod]
        public void ExIntoDeFromHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EX DE,HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xEB);
        }
        [TestMethod]
        public void Exx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "EXX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD9);
        }
    }
}
