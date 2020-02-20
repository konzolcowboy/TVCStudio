using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class OrInstructionTests
    {
        [TestMethod]
        public void OrHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB6);
        }
        [DataTestMethod]
        [DataRow("OR (IX+$5D)")]
        [DataRow("OR (IX+93)")]
        public void OrIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB6);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("OR (IY+$5D)")]
        [DataRow("OR (IY+93)")]
        public void OrIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB6);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [TestMethod]
        public void OrA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB7);
        }
        [TestMethod]
        public void OrB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB0);
        }
        [TestMethod]
        public void OrC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB1);
        }
        [TestMethod]
        public void OrD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB2);
        }
        [TestMethod]
        public void OrE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB3);
        }
        [TestMethod]
        public void OrH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB4);
        }
        [TestMethod]
        public void OrL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OR L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB5);
        }
        [DataTestMethod]
        [DataRow("OR $5D")]
        [DataRow("OR 93")]
        public void OrWithConstant(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF6);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }

    }
}
