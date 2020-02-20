using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class AndInstructionTests
    {
        [TestMethod]
        public void AndHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA6);
        }
        [DataTestMethod]
        [DataRow("AND (IX+$5D)")]
        [DataRow("AND (IX+93)")]
        public void AndIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA6);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("AND (IY+$5D)")]
        [DataRow("AND (IY+93)")]
        public void AndIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA6);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [TestMethod]
        public void AndA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA7);
        }
        [TestMethod]
        public void AndB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA0);
        }
        [TestMethod]
        public void AndC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA1);
        }
        [TestMethod]
        public void AndD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA2);
        }
        [TestMethod]
        public void AndE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA3);
        }
        [TestMethod]
        public void AndH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA4);
        }
        [TestMethod]
        public void AndL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "AND L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xA5);
        }
        [DataTestMethod]
        [DataRow("AND $5D")]
        [DataRow("AND 93")]
        public void AndWithConstant(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE6);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }
        [TestMethod]
        public void Neg()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "NEG" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x44);
        }
        [TestMethod]
        public void Cpl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CPL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x2F);
        }
    }
}
