using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.BitInstructionTests.Set
{
    [TestClass]
    public class SetInstructionsWithBit4
    {
        [TestMethod]
        public void SetWithBit4AndHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE6);
        }

        [TestMethod]
        public void SetWithBit4AndA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE7);
        }
        [TestMethod]
        public void SetWithBit4AndB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE0);
        }

        [TestMethod]
        public void SetWithBit4AndC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE1);
        }
        [TestMethod]
        public void SetWithBit4AndD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE2);
        }
        [TestMethod]
        public void SetWithBit4AndE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE3);
        }
        [TestMethod]
        public void SetWithBit4AndH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE4);
        }
        [TestMethod]
        public void SetWithBit4AndL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SET 4,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE5);
        }
        [DataTestMethod]
        [DataRow("SET 4,(IX+$5D)")]
        [DataRow("SET 4,(IX+93)")]
        public void SetWithBit4AndIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xE6);
        }

        [DataTestMethod]
        [DataRow("SET 4,(IY+$5D)")]
        [DataRow("SET 4,(IY+93)")]
        public void SetWithBit4AndIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xE6);
        }


    }
}
