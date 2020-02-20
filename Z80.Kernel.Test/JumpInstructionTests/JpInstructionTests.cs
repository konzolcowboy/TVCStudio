using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.JumpInstructionTests
{
    [TestClass]
    public class JpInstructionTests
    {
        [DataTestMethod]
        [DataRow("JP C,$4DFD")]
        [DataRow("JP C,19965")]
        public void JpWithC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDA);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP NC,$4DFD")]
        [DataRow("JP NC,19965")]
        public void JpWithNc(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD2);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP Z,$4DFD")]
        [DataRow("JP Z,19965")]
        public void JpWithZ(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCA);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP NZ,$4DFD")]
        [DataRow("JP NZ,19965")]
        public void JpWithNz(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC2);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP P,$4DFD")]
        [DataRow("JP P,19965")]
        public void JpWithP(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF2);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP M,$4DFD")]
        [DataRow("JP M,19965")]
        public void JpWithM(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFA);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP P0,$4DFD")]
        [DataRow("JP P0,19965")]
        public void JpWithP0(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE2);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }

        [DataTestMethod]
        [DataRow("JP PO,$4DFD")]
        [DataRow("JP PO,19965")]
        public void JpWithPo(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE2);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        
        [DataTestMethod]
        [DataRow("JP PE,$4DFD")]
        [DataRow("JP PE,19965")]
        public void JpWithPe(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xEA);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JP $4DFD")]
        [DataRow("JP 19965")]
        public void JpWithOutCondition(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC3);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [TestMethod]
        public void JpWithHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "JP (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE9);
        }
        [TestMethod]
        public void JpWithIxPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "JP (IX)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE9);
        }
        [TestMethod]
        public void JpWithIyPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "JP (IY)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xE9);
        }

    }
}
