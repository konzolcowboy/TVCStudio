using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class Bit8SbcInstructionTests
    {
        [TestMethod]
        public void SbcHlPointerToA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x9E);
        }
        [DataTestMethod]
        [DataRow("SBC A,(IX+$5D)")]
        [DataRow("SBC A,(IX+93)")]
        public void SbcIxWithOffsetToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x9E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("SBC A,(IY+$5D)")]
        [DataRow("SBC A,(IY+93)")]
        public void SbcIyWithOffsetToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x9E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);

        }
        [TestMethod]
        public void SbcAtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x9F);
        }
        [TestMethod]
        public void SbcBtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x98);
        }
        [TestMethod]
        public void SbcCtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x99);
        }
        [TestMethod]
        public void SbcDtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x9A);
        }
        [TestMethod]
        public void SbcEtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x9B);
        }
        [TestMethod]
        public void SbcHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x9C);
        }
        [TestMethod]
        public void SbcLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SBC A,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x9D);
        }
        [DataTestMethod]
        [DataRow("SBC A,$5D")]
        [DataRow("SBC A,93")]
        public void SbcConstanttoA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDE);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }

    }
}
