using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.PortInstructionTests
{
    [TestClass]
    public class OutInstructionTests
    {
        [TestMethod]
        public void OutZeroToPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),0" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x71);
        }
        [TestMethod]
        public void OutAFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x79);
        }
        [TestMethod]
        public void OutBFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x41);
        }
        [TestMethod]
        public void OutCFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x49);
        }
        [TestMethod]
        public void OutDFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x51);
        }
        [TestMethod]
        public void OutEFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x59);
        }
        [TestMethod]
        public void OutHFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x61);
        }
        [TestMethod]
        public void OutLFromPointerC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUT (C),L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x69);
        }
        [DataTestMethod]
        [DataRow("OUT ($4D),A")]
        [DataRow("OUT (77),A")]
        public void OutToPointerFromA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [TestMethod]
        public void Outd()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUTD" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAB);
        }
        [TestMethod]
        public void Otdr()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OTDR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xBB);
        }
        [TestMethod]
        public void Outi()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OUTI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA3);
        }
        [TestMethod]
        public void Otir()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "OTIR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB3);
        }

    }
}
