using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.BitInstructionTests.Bit
{
    [TestClass]
    public class BitInstructionsWithBit1
    {
        [TestMethod]
        public void BitWithBit1AndHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4E);
        }

        [TestMethod]
        public void BitWithBit1AndA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4F);
        }
        [TestMethod]
        public void BitWithBit1AndB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x48);
        }

        [TestMethod]
        public void BitWithBit1AndC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x49);
        }
        [TestMethod]
        public void BitWithBit1AndD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4A);
        }
        [TestMethod]
        public void BitWithBit1AndE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4B);
        }
        [TestMethod]
        public void BitWithBit1AndH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4C);
        }
        [TestMethod]
        public void BitWithBit1AndL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 1,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("BIT 1,(IX+$5D)")]
        [DataRow("BIT 1,(IX+93)")]
        public void BitWithBit1AndIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x4E);
        }

        [DataTestMethod]
        [DataRow("BIT 1,(IY+$5D)")]
        [DataRow("BIT 1,(IY+93)")]
        public void BitWithBit1AndIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x4E);
        }

    }
}
