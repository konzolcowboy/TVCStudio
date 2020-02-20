using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.AssemblyProgramTests
{
    [TestClass]
    public class Mathematic
    {
        [TestMethod]
        public void Multiplication()
        {
            List<string> inputProgram = new List<string>
            {
                "ORG    $3000        ;Program kezdőcíme",
                "LD     A,$1C        ;Egy szorzandó szám",
                "LD     B,A          ;B-be is",
                "ADD    A,A          ;*2    (A+A=2*A)",
                "ADD    A,A          ;*4 Az előbbi 2-szerese",
                "ADD    A,A          ;*8 Az előbbi 2-szerese",
                "SUB    B            ;*7",
                "END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 7);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1C);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x47);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x87);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x87);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0x87);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x90);
        }

        [TestMethod]
        public void Multiplication2()
        {
            List<string> inputProgram = new List<string>
            {
                "ORG    $3000   ",
                "LD     BC,$332C",
                "XOR    A     ",
                "LD     H,A     ",
                "OR     B     ",
                "JR     Z,$300F     ",
                "XOR    A       ",
                "ADD    A,C       ",
                "JR     NC,$300D       ",
                "INC    H       ",
                "DJNZ   $3009       ",
                "LD     L,A       ",
                "END"
            };

            var assembler = new Z80Assembler.Z80Assembler(inputProgram);
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 16);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2C);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x33);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xAF);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x67);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xB0);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x28);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x07);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0xAF);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x81);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x30);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0x24);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x10);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0xFA);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x6F);
        }
    }
}
