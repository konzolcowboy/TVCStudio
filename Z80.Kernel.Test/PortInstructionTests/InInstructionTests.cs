using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.PortInstructionTests
{
    [TestClass]
    public class InInstructionTests
    {
        [TestMethod]
        public void InFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN (C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x70);
        }

        [TestMethod]
        public void InAFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN A,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x78);
        }

        [TestMethod]
        public void InBFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN B,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x40);
        }
        [TestMethod]
        public void InCFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN C,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x48);
        }
        [TestMethod]
        public void InDFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN D,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x50);
        }
        [TestMethod]
        public void InEFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN E,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x58);
        }
        [TestMethod]
        public void InHFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN H,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x60);
        }
        [TestMethod]
        public void InLFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IN L,(C)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x68);
        }
        [DataTestMethod]
        [DataRow("IN A,($4D)")]
        [DataRow("IN A,(77)")]
        public void InFromPointerToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [TestMethod]
        public void Ind()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "IND" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAA);
        }
        [TestMethod]
        public void Indr()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INDR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBA);
        }
        [TestMethod]
        public void Ini()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA2);
        }
        [TestMethod]
        public void Inir()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "INIR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB2);
        }
    }
}
