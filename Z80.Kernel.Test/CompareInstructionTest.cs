using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class CompareInstructionTest
    {
        [DataTestMethod]
        [DataRow("CP (IX+$4D)")]
        [DataRow("CP (IX+77)")]
        public void CompareWithIxRegister(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBE);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CP (IY+$4D)")]
        [DataRow("CP (IY+77)")]
        public void CompareWithIyRegister(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBE);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }

        [TestMethod]
        public void CompareWithHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBE);
        }
        [TestMethod]
        public void CompareWithA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBF);
        }
        [TestMethod]
        public void CompareWithB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB8);
        }
        [TestMethod]
        public void CompareWithC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xB9);
        }
        [TestMethod]
        public void CompareWithD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBA);
        }
        [TestMethod]
        public void CompareWithE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBB);
        }
        [TestMethod]
        public void CompareWithH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBC);
        }
        [TestMethod]
        public void CompareWithL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBD);
        }
        [DataTestMethod]
        [DataRow("CP $4D")]
        [DataRow("CP 77")]
        public void CompareWithConstant(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [TestMethod]
        public void Cpd()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CPD" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA9);
        }
        [TestMethod]
        public void Cpi()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CPI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA1);
        }
        [TestMethod]
        public void Cpdr()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CPDR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB9);
        }
        [TestMethod]
        public void Cpir()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CPIR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB1);
        }
        [TestMethod]
        public void Daa()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DAA" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x27);
        }
        [TestMethod]
        public void CompareWithIXH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBC);
        }
        [TestMethod]
        public void CompareWithIXL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBD);
        }
        [TestMethod]
        public void CompareWithIYH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBC);
        }
        [TestMethod]
        public void CompareWithIYL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "CP IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBD);
        }
    }
}
