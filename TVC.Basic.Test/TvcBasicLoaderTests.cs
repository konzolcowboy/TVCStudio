using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace TVC.Basic.Test
{
    [TestClass]
    public class TvcBasicLoaderTests
    {
        [TestMethod]
        public void BasicLoaderTextCouldBeGeneratedForThreeProgramBytes()
        {
            byte[] programbytes = { 211, 2, 201 };
            Z80Program program = new Z80Program();
            program.ProgramBytes.AddRange(programbytes);
            program.ProgramSections.Add(new ProgramSection { ProgramStartAddress = 0x3000, SectionLength = (ushort)programbytes.Length });
            var loader = new TvcBasicLoader(program, 1, 1, 0x3000);

            Assert.AreEqual(loader.GenerateBasicLoader(), true);
            Assert.AreEqual(loader.TvcBasicRows.Count == 3, true);
            Assert.AreEqual(loader.TvcBasicRows[0].RowText == @"1DATA 211,2,201", true);
            Assert.AreEqual(loader.TvcBasicRows[1].RowText == @"2FORI=12288TO12290:READB:POKE I,B:NEXTI", true);
            Assert.AreEqual(loader.TvcBasicRows[2].RowText == @"3S=USR(12288)", true);
        }
        [TestMethod]
        public void BasicLoaderTextCouldBeGeneratedWith230Bytes()
        {
            List<byte> programbytes = new List<byte>();
            for (int i = 1; i <= 230; i++)
            {
                programbytes.Add((byte)i);
            }

            Z80Program program = new Z80Program();
            program.ProgramBytes.AddRange(programbytes);
            program.ProgramSections.Add(new ProgramSection{ProgramStartAddress = 0x3000,SectionLength = (ushort) programbytes.Count});
            var loader = new TvcBasicLoader(program, 1, 1, 0x3000);

            Assert.AreEqual(loader.GenerateBasicLoader(), true);
            Assert.AreEqual(loader.TvcBasicRows.Count == 6, true);
            Assert.AreEqual(loader.TvcBasicRows[0].RowText == @"1DATA 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60", true);
            Assert.AreEqual(loader.TvcBasicRows[1].RowText == @"2DATA 61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120", true);
            Assert.AreEqual(loader.TvcBasicRows[2].RowText == @"3DATA 121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180", true);
            Assert.AreEqual(loader.TvcBasicRows[3].RowText == @"4DATA 181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230", true);
            Assert.AreEqual(loader.TvcBasicRows[4].RowText == @"5FORI=12288TO12517:READB:POKE I,B:NEXTI", true);
            Assert.AreEqual(loader.TvcBasicRows[5].RowText == @"6S=USR(12288)", true);
        }

        [TestMethod]
        public void BasicLoaderForWritingIntoVideoMemoryAssemblyProgram()
        {
            List<string> programrows = new List<string>
            {
                "ORG $3000",
                "LD  A,$50",
                "LD  ($3),A",
                "OUT ($2),A",
                "LD  A,$6B",
                "LD  ($942A),A",
                "LD  A,$70",
                "LD  ($3),A",
                "OUT ($2),A",
                "RET"
            };

            var assembler = new Z80Assembler(programrows);
            Assert.AreEqual(assembler.BuildProgram(),true);
            var loader = new TvcBasicLoader(assembler.AssembledProgram,1,1,0x3000);
            Assert.AreEqual(loader.GenerateBasicLoader(),true);
            Assert.AreEqual(loader.TvcBasicRows.Count == 3, true);
            Assert.AreEqual(loader.TvcBasicRows[0].RowText == @"1DATA 62,80,50,3,0,211,2,62,107,50,42,148,62,112,50,3,0,211,2,201", true);
            Assert.AreEqual(loader.TvcBasicRows[1].RowText == @"2FORI=12288TO12307:READB:POKE I,B:NEXTI", true);
            Assert.AreEqual(loader.TvcBasicRows[2].RowText == @"3S=USR(12288)", true);
        }
        [TestMethod]
        public void BasicLoaderForWritingTextToScreen()
        {
            List<string> programrows = new List<string>
            {
                "ORG $3000",
                "RST $30",
                "DB  $05",
                "LD  BC,$010C",
                "RST $30",
                "DB  $03",
                "LD  DE,SZOVEG",
                "LD  BC,21",
                "RST $30",
                "DB  $02",
                "RET",
                "SZOVEG  DB 'ÁRVÍZTŰRŐTÜKÖRFÚRÓGÉP'"
            };

            var assembler = new Z80Assembler(programrows);
            Assert.AreEqual(assembler.BuildProgram(), true);
            var loader = new TvcBasicLoader(assembler.AssembledProgram, 1, 1, 0x3000);
            Assert.AreEqual(loader.GenerateBasicLoader(), true);
            Assert.AreEqual(loader.TvcBasicRows.Count == 3, true);
            Assert.AreEqual(loader.TvcBasicRows[0].RowText == @"1DATA 247,5,1,12,1,247,3,17,16,48,1,21,0,247,2,201,128,82,86,130,90,84,136,82,133,84,135,75,132,82,70,134,82,131,71,129,80", true);
            Assert.AreEqual(loader.TvcBasicRows[1].RowText == @"2FORI=12288TO12324:READB:POKE I,B:NEXTI", true);
            Assert.AreEqual(loader.TvcBasicRows[2].RowText == @"3S=USR(12288)", true);
        }
    }
}
