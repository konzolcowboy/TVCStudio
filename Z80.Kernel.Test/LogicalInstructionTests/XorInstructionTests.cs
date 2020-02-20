using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.LogicalInstructionTests
{
    [TestClass]
    public class XorInstructionTests
    {
        [TestMethod]
        public void XorHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xAE);
        }
        [DataTestMethod]
        [DataRow("XOR (IX+$5D)")]
        [DataRow("XOR (IX+93)")]
        public void XorIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAE);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("XOR (IY+$5D)")]
        [DataRow("XOR (IY+93)")]
        public void XorIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAE);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [TestMethod]
        public void XorA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xAF);
        }
        [TestMethod]
        public void XorB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA8);
        }
        [TestMethod]
        public void XorC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA9);
        }
        [TestMethod]
        public void XorD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xAA);
        }
        [TestMethod]
        public void XorE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xAB);
        }
        [TestMethod]
        public void XorH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xAC);
        }
        [TestMethod]
        public void XorL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xAD);
        }
        [DataTestMethod]
        [DataRow("XOR $5D")]
        [DataRow("XOR 93")]
        public void XorWithConstant(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xEE);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }

        [TestMethod]
        public void XorIXH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAC);
        }

        [TestMethod]
        public void XorIXL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAD);
        }
        [TestMethod]
        public void XorIYH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAC);
        }

        [TestMethod]
        public void XorIYL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "XOR IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAD);
        }
    }
}
