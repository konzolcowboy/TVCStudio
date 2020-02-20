using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.PreProcessorTests
{
    [TestClass]
    public class ConditionalGroupTests
    {
        [TestMethod]
        public void IfDef()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifdef   TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
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
        public void IfnDef()
        {
            List<string> inputProgram = new List<string>
            {
                "#ifndef    TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
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
        public void IfnDefWithUndef()
        {
            List<string> inputProgram = new List<string>
            {
                "  #define  TVC",
                "   #undef TVC",
                "#ifndef    TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
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
        public void IfDefWithElse()
        {
            List<string> inputProgram = new List<string>
            {
                "#ifdef  SPECTRUM",
                "RLCA",
                "RLCA",
                "NOP",
                "#else",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
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
        public void IfnDefWithElse()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifndef  TVC",
                "RLCA",
                "RLCA",
                "NOP",
                "#else",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
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
        public void IfnDefWithElseWithIdefAndElse()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifndef  TVC",
                "RLCA",
                "RLCA",
                "NOP",
                "#else",
                "#Ifndef    spectrum",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#else",
                "LD A,B",
                "call specci",
                "#endif",
                "#endif",
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
        public void IfnDefWithElseWithEmbeddedIfdefAndElse()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifndef  TVC",
                "RLCA",
                "RLCA",
                "NOP",
                "#else",
                "#Ifndef    spectrum",
                "#ifdef     TVC",
                "#ifdef     TVC",
                "#ifdef     TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
                "#endif",
                "#endif",
                "#else",
                "LD A,B",
                "call specci",
                "#endif",
                "#endif",
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
        public void IfnDefWithDoubleElse()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifndef  TVC",
                "RLCA",
                "RLCA",
                "NOP",
                "#else",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "#else",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
                "           END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Egy feltételes szekció csak egy #ELSE utasítást tartalmazhat!"));
        }
        [TestMethod]
        public void IfDefWithMoreEndIf()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifdef   TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
                "#endif",
                "           END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az #ENDIF utasítást #IFDEF vagy #IFNDEF utasításoknak kell megelőznie"));
        }
        [TestMethod]
        public void EmbeddedIfDef()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
                "#endif",
                "#endif",
                "#endif",
                "#endif",
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
        public void EmbeddedIfDefWithLessEndIf()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "#ifdef   TVC",
                "           ORG    $7000",
                "           LD     A,$41",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
                "#endif",
                "#endif",
                "           END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az IFDEF TVC utasítás nincs lezárva #ENDIF utasítással!"));
        }
        [TestMethod]
        public void EndmWithoutMacro()
        {
            List<string> inputProgram = new List<string>
            {
                "#define  TVC",
                "#ifndef  TVC",
                "RLCA",
                "RLCA",
                "NOP",
                "#else",
                "           ORG    $7000",
                "           LD     A,$41",
                "#endm",
                "           LD     (($3E06+Operand)),A",
                "  Hurok    JP     Hurok",
                "#else",
                "  Operand  EQU    $19+operand2",
                "  Operand2 EQU    $1",
                "#endif",
                "           END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az #ENDM utasítást a #MACRO utasításnak kell megelőznie!"));
        }
    }
}
