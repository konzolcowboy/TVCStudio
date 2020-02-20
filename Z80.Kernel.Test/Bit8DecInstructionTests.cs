using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class Bit8DecInstructionTests
    {
        [TestMethod]
        public void DecHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x35);
        }
        [DataTestMethod]
        [DataRow("DEC (IX+$5D)")]
        [DataRow("DEC (IX+93)")]
        public void DecIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x35);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("DEC (IY+$5D)")]
        [DataRow("DEC (IY+93)")]
        public void DecIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x35);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [TestMethod]
        public void DecA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3D);
        }
        [TestMethod]
        public void DecB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x05);
        }
        [TestMethod]
        public void DecC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x0D);
        }
        [TestMethod]
        public void DecD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x15);
        }
        [TestMethod]
        public void DecE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x1D);
        }
        [TestMethod]
        public void DecH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x25);
        }
        [TestMethod]
        public void DecL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DEC L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x2D);
        }

    }
}
