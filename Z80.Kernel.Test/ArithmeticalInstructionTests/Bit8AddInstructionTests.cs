using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.ArithmeticalInstructionTests
{
    [TestClass]
    public class Bit8AddInstructionTests
    {
        [TestMethod]
        public void AddHlPointerToA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x86);
        }
        [DataTestMethod]
        [DataRow("ADD A,(IX+$5D)")]
        [DataRow("ADD A,(IX+93)")]
        public void AddIxWithOffsetToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x86);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("ADD A,(IY+$5D)")]
        [DataRow("ADD A,(IY+93)")]
        public void AddIyWithOffsetToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x86);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);

        }
        [TestMethod]
        public void AddAtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x87);
        }
        [TestMethod]
        public void AddBtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x80);
        }
        [TestMethod]
        public void AddCtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x81);
        }
        [TestMethod]
        public void AddDtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x82);
        }
        [TestMethod]
        public void AddEtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x83);
        }
        [TestMethod]
        public void AddHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x84);
        }
        [TestMethod]
        public void AddLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x85);
        }
        [DataTestMethod]
        [DataRow("ADD A,$5D")]
        [DataRow("ADD A,93")]
        public void AddConstanttoA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC6);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }

        [TestMethod]
        public void AddIXHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }

        [TestMethod]
        public void AddIXLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x85);
        }
        [TestMethod]
        public void AddIYHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }

        [TestMethod]
        public void AddIYLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADD A,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x85);
        }
    }
}
