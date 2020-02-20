using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Test.AssemblerInstructionTests
{
    [TestClass]
    public class IncBinInstructionTests
    {
        [TestMethod]
        public void IncBinOnlyWithFileName()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"testdata\\incbintest.bin\"" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "ADAT", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "\"testdata\\incbintest.bin\"", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 15, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x01, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x02, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x03, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x04, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x05, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x06, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[6] == 0x07, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[7] == 0x08, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[8] == 0x09, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[9] == 0x0a, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[10] == 0x0b, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[11] == 0x0c, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[12] == 0x0d, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[13] == 0x0e, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[14] == 0x0f, true);
        }

        [TestMethod]
        public void IncBinWithTwoOperands()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"testdata\\incbintest.bin\",7" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 8, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x08, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x09, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x0a, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x0b, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x0c, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x0d, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[6] == 0x0e, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[7] == 0x0f, true);
        }
        [TestMethod]
        public void IncBinWithThreeOperands()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"testdata\\incbintest.bin\",12,3" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 3, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x0d, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x0e, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x0f, true);
        }

        [TestMethod]
        public void IncBinForTheFirstFourBytes()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"testdata\\incbintest.bin\",0,4" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 4, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x01, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x02, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x03, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x04, true);
        }

        [TestMethod]
        public void IncBinOnlyWithWrongFileName()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"testdata\\incbintest2.bin\"" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Nem találom a megadott file-t"));
        }

        [TestMethod]
        public void IncBinWithWrongSecondOperands()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"TESTDATA\\INCBINTEST.BIN\",A,3" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az utasítás 'INCBIN' második operandusa csak szám lehet!"));
        }

        [TestMethod]
        public void IncBinWithWrongThirdOperands()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"TESTDATA\\INCBINTEST.BIN\",3,IX(" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az utasítás 'INCBIN' harmadik operandusa csak szám lehet!"));
        }
        [TestMethod]
        public void IncBinWithNegativeSecondOperands()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"TESTDATA\\INCBINTEST.BIN\",-2,3" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az utasítás 'INCBIN' második operandusa nem lehet kisebb nullánál"));
        }

        [TestMethod]
        public void IncBinWithNegativeThirdOperands()
        {
            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { @"ORG $3000", "ADAT INCBIN  \"TESTDATA\\INCBINTEST.BIN\",3,-3" }, new List<string> { includePath });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az utasítás 'INCBIN' harmadik operandusa nem lehet kisebb egynél"));
        }
    }
}
