using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.ArithmeticalInstructionTests
{
    [TestClass]
    public class Bit16IncInstructionTests
    {
        [TestMethod]
        public void IncBc()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC BC" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x03);
        }
        [TestMethod]
        public void IncDe()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC DE" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x13);
        }
        [TestMethod]
        public void IncHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x23);
        }
        [TestMethod]
        public void IncIx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC IX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x23);
        }
        [TestMethod]
        public void IncIy()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC IY" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x23);
        }
        [TestMethod]
        public void IncSp()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC SP" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x33);
        }
    }
}
