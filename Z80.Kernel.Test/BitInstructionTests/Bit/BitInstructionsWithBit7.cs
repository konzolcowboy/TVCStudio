using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.BitInstructionTests.Bit
{
    [TestClass]
    public class BitInstructionsWithBit7
    {
        [TestMethod]
        public void BitWithBit7AndHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7E);
        }

        [TestMethod]
        public void BitWithBit7AndA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7F);
        }
        [TestMethod]
        public void BitWithBit7AndB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x78);
        }

        [TestMethod]
        public void BitWithBit7AndC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x79);
        }
        [TestMethod]
        public void BitWithBit7AndD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7A);
        }
        [TestMethod]
        public void BitWithBit7AndE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7B);
        }
        [TestMethod]
        public void BitWithBit7AndH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7C);
        }
        [TestMethod]
        public void BitWithBit7AndL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 7,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7D);
        }
        [DataTestMethod]
        [DataRow("BIT 7,(IX+$5D)")]
        [DataRow("BIT 7,(IX+93)")]
        public void BitWithBit7AndIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x7E);
        }

        [DataTestMethod]
        [DataRow("BIT 7,(IY+$5D)")]
        [DataRow("BIT 7,(IY+93)")]
        public void BitWithBit7AndIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x7E);
        }


    }
}
