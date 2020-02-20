using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.PreProcessorTests
{
    [TestClass]
    public class MacroTests
    {
        [TestMethod]
        public void SimpleMacroDefinitionWithOutParameters()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText",
                "VIDEO   EQU    $3FC0",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     \"EZ EGY KOMOLY PROGRAM\"",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 45);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x17);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xC0);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x3F);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x28);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x77);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x23);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0x23);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0xC3);
            Assert.AreEqual(assembler.AssembledProgramBytes[19], 0x07);
            Assert.AreEqual(assembler.AssembledProgramBytes[20], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[21], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[22], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[23], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[24], 0x5A);
            Assert.AreEqual(assembler.AssembledProgramBytes[25], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[26], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[27], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[28], 0x59);
            Assert.AreEqual(assembler.AssembledProgramBytes[29], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[30], 0x4B);
            Assert.AreEqual(assembler.AssembledProgramBytes[31], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[32], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[33], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[34], 0x4C);
            Assert.AreEqual(assembler.AssembledProgramBytes[35], 0x59);
            Assert.AreEqual(assembler.AssembledProgramBytes[36], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[37], 0x50);
            Assert.AreEqual(assembler.AssembledProgramBytes[38], 0x52);
            Assert.AreEqual(assembler.AssembledProgramBytes[39], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[40], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[41], 0x52);
            Assert.AreEqual(assembler.AssembledProgramBytes[42], 0x41);
            Assert.AreEqual(assembler.AssembledProgramBytes[43], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[44], 0x00);
        }


        [TestMethod]
        public void SimpleMacroDefinitionWithParameters()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText (videoAddress,text)",
                "VIDEO   EQU    videoAddress",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     text",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText($3FC0,\"EZ EGY KOMOLY PROGRAM\")",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 45);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x17);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xC0);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x3F);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x28);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x77);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x23);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0x23);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0xC3);
            Assert.AreEqual(assembler.AssembledProgramBytes[19], 0x07);
            Assert.AreEqual(assembler.AssembledProgramBytes[20], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[21], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[22], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[23], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[24], 0x5A);
            Assert.AreEqual(assembler.AssembledProgramBytes[25], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[26], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[27], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[28], 0x59);
            Assert.AreEqual(assembler.AssembledProgramBytes[29], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[30], 0x4B);
            Assert.AreEqual(assembler.AssembledProgramBytes[31], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[32], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[33], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[34], 0x4C);
            Assert.AreEqual(assembler.AssembledProgramBytes[35], 0x59);
            Assert.AreEqual(assembler.AssembledProgramBytes[36], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[37], 0x50);
            Assert.AreEqual(assembler.AssembledProgramBytes[38], 0x52);
            Assert.AreEqual(assembler.AssembledProgramBytes[39], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[40], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[41], 0x52);
            Assert.AreEqual(assembler.AssembledProgramBytes[42], 0x41);
            Assert.AreEqual(assembler.AssembledProgramBytes[43], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[44], 0x00);
        }

        [TestMethod]
        public void SimpleMacroDefinitionWithParametersWithTabs()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText   (       videoAddress    ,           text)",
                "VIDEO   EQU    videoAddress",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     text",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText  (       $3FC0,      \"EZ EGY KOMOLY PROGRAM\"       )",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 45);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x17);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xC0);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x3F);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x28);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x77);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x23);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0x23);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0xC3);
            Assert.AreEqual(assembler.AssembledProgramBytes[19], 0x07);
            Assert.AreEqual(assembler.AssembledProgramBytes[20], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[21], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[22], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[23], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[24], 0x5A);
            Assert.AreEqual(assembler.AssembledProgramBytes[25], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[26], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[27], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[28], 0x59);
            Assert.AreEqual(assembler.AssembledProgramBytes[29], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[30], 0x4B);
            Assert.AreEqual(assembler.AssembledProgramBytes[31], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[32], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[33], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[34], 0x4C);
            Assert.AreEqual(assembler.AssembledProgramBytes[35], 0x59);
            Assert.AreEqual(assembler.AssembledProgramBytes[36], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[37], 0x50);
            Assert.AreEqual(assembler.AssembledProgramBytes[38], 0x52);
            Assert.AreEqual(assembler.AssembledProgramBytes[39], 0x4F);
            Assert.AreEqual(assembler.AssembledProgramBytes[40], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[41], 0x52);
            Assert.AreEqual(assembler.AssembledProgramBytes[42], 0x41);
            Assert.AreEqual(assembler.AssembledProgramBytes[43], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[44], 0x00);
        }

        [TestMethod]
        public void SimpleMacroDefinitionWithExpressionParameters()
        {
            List<string> inputProgram = new List<string>
            {
                "        #macro  expressions(expression1,expression2)",
                "         LD     A,$41",
                "         LD     (expression1),A",
                "Hurok    JP     Hurok",
                "Operand  EQU    expression2",
                "Operand2 EQU    $1",
                "         #endm",
                "         ORG    $7000",
                "         expressions(($3E06+Operand),$19+operand2)",
                "         END"
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
        public void MacroBodyMustNotContainMacroInstruction()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText   (       videoAddress    ,           text)",
                "VIDEO   EQU    videoAddress",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        #Macro",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        #endm",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     text",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText  (       $3FC0,      \"EZ EGY KOMOLY PROGRAM\"       )",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Makró definíció nem tartalmazhat #MACRO és #INCLUDE utasításokat!"));
        }

        [TestMethod]
        public void MacroBodyMustNotContainIncludeInstruction()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText   (       videoAddress    ,           text)",
                "VIDEO   EQU    videoAddress",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        #include \"valami.tvcasm\"",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        #endm",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     text",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText  (       $3FC0,      \"EZ EGY KOMOLY PROGRAM\"       )",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Makró definíció nem tartalmazhat #MACRO és #INCLUDE utasításokat!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfEmptyParanthesis()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO ()",
                "VIDEO   EQU    videoAddress",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        #include \"valami.tvcasm\"",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        #endm",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     text",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText  (       $3FC0,      \"EZ EGY KOMOLY PROGRAM\"       )",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("A makró neve csak betűvel kezdődhet!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfEmptyParanthesisWithName()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText   ()",
                "VIDEO   EQU    videoAddress",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        #include \"valami.tvcasm\"",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        #endm",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     text",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText  (       $3FC0,      \"EZ EGY KOMOLY PROGRAM\"       )",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Szintaxis hiba a makró definícióban! Hiányzó paraméter név!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfInvalidCharInName()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO Write&Text",
                "VIDEO   EQU    $3FC0",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     \"EZ EGY KOMOLY PROGRAM\"",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Helytelen karakter:'&' a makró nevében!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfInvalidCharInParameterName()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText(param&name)",
                "VIDEO   EQU    $3FC0",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     \"EZ EGY KOMOLY PROGRAM\"",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Helytelen karakter: & a makró paraméter nevében!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfDoubleColon()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText(paramname,,)",
                "VIDEO   EQU    $3FC0",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     \"EZ EGY KOMOLY PROGRAM\"",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Szintaxis hiba a makró definícióban! Hiányzó paraméter név!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfMissingRightParenthesis()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO WriteText(paramname",
                "VIDEO   EQU    $3FC0",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     \"EZ EGY KOMOLY PROGRAM\"",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Szintaxis hiba a makró hívásban! Hiányzió ')' karakter!"));
        }

        [TestMethod]
        public void WrongMacroDefinitionBecauseOfEmpthyMacroDefinition()
        {
            List<string> inputProgram = new List<string>
            {
                "        #MACRO  ",
                "VIDEO   EQU    $3FC0",
                "        LD     HL,SZOVEG",
                "        LD     IX,VIDEO",
                "CIKLUS  LD     A,(HL)",
                "        CP     0",
                "        JR     Z,VEGE",
                "        LD     (IX+0),A",
                "        INC    HL",
                "        INC    IX",
                "        JP     CIKLUS",
                "VEGE    JR     VEGE",
                "SZOVEG  DB     \"EZ EGY KOMOLY PROGRAM\"",
                "        DB     $00",
                "        #ENDM",
                "        ORG    $7000",
                "        WriteText",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("A macro nevét meg kell adni!"));
        }
    }
}
