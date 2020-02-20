using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.BitInstructionTests.Bit
{
    [TestClass]
    public class BitInstructionsWithBit6
    {
            [TestMethod]
            public void BitWithBit6AndHlPointer()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,(HL)" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x76);
            }

            [TestMethod]
            public void BitWithBit6AndA()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,A" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x77);
            }
            [TestMethod]
            public void BitWithBit6AndB()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,B" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x70);
            }

            [TestMethod]
            public void BitWithBit6AndC()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,C" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x71);
            }
            [TestMethod]
            public void BitWithBit6AndD()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,D" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x72);
            }
            [TestMethod]
            public void BitWithBit6AndE()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,E" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x73);
            }
            [TestMethod]
            public void BitWithBit6AndH()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,H" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x74);
            }
            [TestMethod]
            public void BitWithBit6AndL()
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BIT 6,L" });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x75);
            }
            [DataTestMethod]
            [DataRow("BIT 6,(IX+$5D)")]
            [DataRow("BIT 6,(IX+93)")]
            public void BitWithBit6AndIxWithOffset(string row)
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
                Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x76);
            }

            [DataTestMethod]
            [DataRow("BIT 6,(IY+$5D)")]
            [DataRow("BIT 6,(IY+93)")]
            public void BitWithBit6AndIyWithOffset(string row)
            {
                var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
                bool result = assembler.BuildProgram();
                Assert.AreEqual(result, true);
                Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
                Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
                Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
                Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
                Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x76);
            }


        }
    }
