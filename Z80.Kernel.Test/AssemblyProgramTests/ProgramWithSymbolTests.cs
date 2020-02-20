using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.AssemblyProgramTests
{
    [TestClass]
    public class ProgramWithSymbolTests
    {
        [TestMethod]
        public void OneSimpleInstructionSymbol()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,$41",
                "        LD     ($3E20),A",
                "Hurok   JP     Hurok",
                "        END"
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
        public void SimpleAssemblyProgramWithOneSymbolAndDecimalOperand()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,65",
                "        LD     ($3E20),A",
                "Hurok   JP     Hurok",
                "        END"
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
        public void SimpleAssemblyProgramWithCharacterOperand()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,\"A\"",
                "        LD     ($3E20),A",
                "Hurok   JP     Hurok",
                "        END"
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
        public void SimpleDbAssemblerInstruction()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,(BETU)",
                "        LD     ($3E20),A",
                "Hurok   JP     Hurok",
                "BETU    DB     $41",
                "        END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 10);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3A);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0xC3);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x06);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x41);
        }
        [TestMethod]
        public void SimpleEquAssemblerInstruction()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,BETU",
                "        LD     (VIDEO),A",
                "Hurok   JP     Hurok",
                "BETU    EQU    $41",
                "VIDEO   EQU    $3E20",
                "        END"
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
        public void SimpleEquAssemblerProgramWithCharacter()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,BETU",
                "        LD     (VIDEO),A",
                "Hurok   JP     Hurok",
                "BETU    EQU    'A'",
                "VIDEO   EQU    $3E20",
                "        END"
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
        public void AssemblyProgramForWritingToScreen()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
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
        public void AssemblyProgramWithSubRoutine()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,'G'",
                "        CALL   KERES",
                "        JP     NZ,NINCS",
                "        LD     BC,TABLA",
                "        OR     A",
                "        SBC    HL,BC",
                "        DEC    HL",
                "        ADD    HL,HL",
                "        LD     BC,SZUBZ",
                "        ADD    HL,BC",
                "        JP     (HL)",
                "NINCS   JR     NINCS",
                "SZUBZ   JR     SZUBZ",
                "SZUBE   JR     SZUBE",
                "SZUBN   JR     SZUBN",
                "SZUBG   JR     SZUBG",
                "SZUBC   JR     SZUBC",
                "SZUBF   JR     SZUBF",
                "SZUBX   JR     SZUBX",
                "SZUBM   JR     SZUBM",
                "SZUBQ   JR     SZUBQ",
                "",
                "        ;TABLAKERESO SZUBRUTIN",
                "",
                "        ORG    $7E00",
                "KERES   LD     HL,TABLA",
                "        LD     BC,9",
                "        CPIR",
                "        RET",
                "TABLA   DB     'ZENGCFXMQ'",
                "        END",
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections.Count == 2,true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections[0].ProgramStartAddress == 0x7000, true);
            Assert.AreEqual(assembler.AssembledProgram.ProgramSections[1].ProgramStartAddress == 0x7E00, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, assembler.AssembledProgram.ProgramSections[0].SectionLength + assembler.AssembledProgram.ProgramSections[1].SectionLength);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 59);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xCD);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xC2);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x15);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0xB7);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x42);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x2B);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x29);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0x17);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[19], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[20], 0xE9);
            Assert.AreEqual(assembler.AssembledProgramBytes[21], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[22], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[23], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[24], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[25], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[26], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[27], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[28], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[29], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[30], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[31], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[32], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[33], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[34], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[35], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[36], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[37], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[38], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[39], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[40], 0xFE);
            Assert.AreEqual(assembler.AssembledProgramBytes[41], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[42], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[43], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[44], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[45], 0x09);
            Assert.AreEqual(assembler.AssembledProgramBytes[46], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[47], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[48], 0xB1);
            Assert.AreEqual(assembler.AssembledProgramBytes[49], 0xC9);
            Assert.AreEqual(assembler.AssembledProgramBytes[50], 0x5A);
            Assert.AreEqual(assembler.AssembledProgramBytes[51], 0x45);
            Assert.AreEqual(assembler.AssembledProgramBytes[52], 0x4E);
            Assert.AreEqual(assembler.AssembledProgramBytes[53], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[54], 0x43);
            Assert.AreEqual(assembler.AssembledProgramBytes[55], 0x46);
            Assert.AreEqual(assembler.AssembledProgramBytes[56], 0x58);
            Assert.AreEqual(assembler.AssembledProgramBytes[57], 0x4D);
            Assert.AreEqual(assembler.AssembledProgramBytes[58], 0x51);
        }
    }
}
