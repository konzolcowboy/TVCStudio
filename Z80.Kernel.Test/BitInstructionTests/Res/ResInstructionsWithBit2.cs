using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.BitInstructionTests.Res
{
    [TestClass]
    public class ResInstructionsWithBit2
    {
        [TestMethod]
        public void ResWithBit2AndHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x96);
        }

        [TestMethod]
        public void ResWithBit2AndA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x97);
        }
        [TestMethod]
        public void ResWithBit2AndB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x90);
        }

        [TestMethod]
        public void ResWithBit2AndC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x91);
        }
        [TestMethod]
        public void ResWithBit2AndD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x92);
        }
        [TestMethod]
        public void ResWithBit2AndE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x93);
        }
        [TestMethod]
        public void ResWithBit2AndH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x94);
        }
        [TestMethod]
        public void ResWithBit2AndL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RES 2,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x95);
        }
        [DataTestMethod]
        [DataRow("RES 2,(IX+$5D)")]
        [DataRow("RES 2,(IX+93)")]
        public void ResWithBit2AndIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x96);
        }

        [DataTestMethod]
        [DataRow("RES 2,(IY+$5D)")]
        [DataRow("RES 2,(IY+93)")]
        public void ResWithBit2AndIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x96);
        }

    }
}
