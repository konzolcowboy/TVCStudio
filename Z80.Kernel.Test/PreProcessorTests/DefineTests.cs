using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.PreProcessorTests
{
    [TestClass]
    public class DefineTests
    {
        [TestMethod]
        public void SimpleDefine()
        {
            var programlines = new List<string>()
            {
                "#define programstart ORG $3000",
                "#define CPUtasitas CP A",
                "programstart",
                "CPUtasitas"
            };

            var assembler = new Z80Assembler.Z80Assembler(programlines);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBF);
        }

        [TestMethod]
        public void SimpleDefineWithSpaces()
        {
            var programlines = new List<string>()
            {
                "#define      programstart       ORG                  $3000",
                "#define       CPUtasitas      CP       A",
                "programstart",
                "CPUtasitas"
            };

            var assembler = new Z80Assembler.Z80Assembler(programlines);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBF);
        }

        [TestMethod]
        public void MultipleDefines()
        {
            List<string> inputProgram = new List<string>
            {
                "#define expression ((SZIMBOLUM+$3E06))",
                "",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     expression,A",
                "Hurok      JP     Hurok",
                "SZIMBOLUM  EQU    $1A",
                "#define    SZIMBOLUM Abs123",
                "           END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 8);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x41);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xC3);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x05);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x70);
        }

        [TestMethod]
        public void NotSupportedPreprocessorInstruction()
        {
            var programlines = new List<string>()
            {
                "#notsupported      programstart       ORG                  $3000",
                "#define       CPUtasitas      CP       A",
                "programstart",
                "CPUtasitas"
            };

            var assembler = new Z80Assembler.Z80Assembler(programlines);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Érvénytelen előfeldolgozó utasítás: 'NOTSUPPORTED' Sor:1"));
        }
        [TestMethod]
        public void WrongPreprocessorInstruction()
        {
            var programlines = new List<string>()
            {
                "# notsupported      programstart       ORG                  $3000",
                "#define       CPUtasitas      CP       A",
                "programstart",
                "CPUtasitas"
            };

            var assembler = new Z80Assembler.Z80Assembler(programlines);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Üres előfeldolgozó utasítás! A '#' jel után nem állhat szóköz!"));
        }

        [TestMethod]
        public void EmptyDefine()
        {
            var programlines = new List<string>()
            {
                "#define spectrum",
                "#define      programstart       ORG                  $3000",
                "#define       CPUtasitas      CP       A",
                "programstartspectrum",
                "CPUtasitas"
            };

            var assembler = new Z80Assembler.Z80Assembler(programlines);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xBF);
        }
    }
}
