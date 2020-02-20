using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class Bit8IncInstructionTests
    {
        [TestMethod]
        public void IncHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x34);
        }
        [DataTestMethod]
        [DataRow("INC (IX+$5D)")]
        [DataRow("INC (IX+93)")]
        public void IncIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x34);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("INC (IY+$5D)")]
        [DataRow("INC (IY+93)")]
        public void IncIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x34);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [TestMethod]
        public void IncA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3C);
        }
        [TestMethod]
        public void IncB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x04);
        }
        [TestMethod]
        public void IncC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x0C);
        }
        [TestMethod]
        public void IncD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x14);
        }
        [TestMethod]
        public void IncE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x1C);
        }
        [TestMethod]
        public void IncH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x24);
        }
        [TestMethod]
        public void IncL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INC L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x2C);
        }
    }
}
