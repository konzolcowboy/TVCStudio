using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.ArithmeticalInstructionTests
{
    [TestClass]
    public class Bit8AdcInstructionTests
    {
        [TestMethod]
        public void AdcHlPointerToA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x8E);
        }
        [DataTestMethod]
        [DataRow("ADC A,(IX+$5D)")]
        [DataRow("ADC A,(IX+93)")]
        public void AdcIxWithOffsetToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x8E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
        }
        [DataTestMethod]
        [DataRow("ADC A,(IY+$5D)")]
        [DataRow("ADC A,(IY+93)")]
        public void AdcIyWithOffsetToA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x8E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);

        }
        [TestMethod]
        public void AdcAtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x8F);
        }
        [TestMethod]
        public void AdcBtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x88);
        }
        [TestMethod]
        public void AdcCtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x89);
        }
        [TestMethod]
        public void AdcDtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x8A);
        }
        [TestMethod]
        public void AdcEtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x8B);
        }
        [TestMethod]
        public void AdcHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x8C);
        }
        [TestMethod]
        public void AdcLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x8D);
        }
        [DataTestMethod]
        [DataRow("ADC A,$5D")]
        [DataRow("ADC A,93")]
        public void AdcConstanttoA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCE);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }
        [DataTestMethod]
        [DataRow("ADC A,(IX+$KD)")]
        [DataRow("ADC A,(IX+930000000000)")]
        public void AdcIxWithOffsetToAThrowsException(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();

            Assert.AreEqual(result,false);
        }

        [TestMethod]
        public void AdcIXHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x8C);
        }
        [TestMethod]
        public void AdcIXLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x8D);
        }
        [TestMethod]
        public void AdcIYHtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x8C);
        }
        [TestMethod]
        public void AdcIYLtoA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ADC A,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x8D);
        }
    }
}
