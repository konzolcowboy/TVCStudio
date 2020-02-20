using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.AssemblyProgramTests
{
    [TestClass]
    public class RamTests
    {
        [TestMethod]
        public void WritingIntoTheVideoRam()
        {
            List<string> inputProgram = new List<string>
            {
                "ORG $3000",
                "LD A,$50",
                "LD ($3),A",
                "OUT ($2),A",
                "LD A,$6B",
                "LD ($942A),A",
                "LD A,$70",
                "LD ($3),A",
                "OUT ($2),A",
                "END"
            };
            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 19);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x50);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x03);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x02);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0x6B);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x2A);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x94);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x03);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0x02);
        }

        [TestMethod]
        public void CopyIntoTheU3Ram()
        {
            List<string> inputProgram = new List<string>
            {
                "ORG $3000",
                "LD A,$B0     ;U3 RAM behozatala a 3.lapra",
                "LD ($3),A",
                "OUT ($2),A",
                "LD HL,$3000",
                "LD DE,$C000",
                "LD BC,$14",
                "LDIR          ;Program átmásolása $3000-tól $C000-ra",
                "LD A,$70",
                "LD ($3),A",
                "OUT ($2),A",
                "END"
            };
            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 25);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB0);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x03);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x02);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x30);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x11);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0xC0);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x14);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0xB0);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[19], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[20], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[21], 0x03);
            Assert.AreEqual(assembler.AssembledProgramBytes[22], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[23], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[24], 0x02);

        }
    }
}
