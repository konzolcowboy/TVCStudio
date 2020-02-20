using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.AssemblyProgramTests
{
    [TestClass]
    public class AssemblyProgramWithExpressionTests
    {
        [DataTestMethod]
        [DataRow("LD (IX+((200+60)-5)),175")]
        [DataRow("LD (IX+(($C8+$3C)-$5)),$AF")]
        public void LdConstantIntoIndexedAddressWithIxAndExpression(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x36);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xAF);
        }
        [DataTestMethod]
        [DataRow("LD (IY+(3*20)),A")]
        [DataRow("LD (IY+($3*$14)),A")]
        public void LdIntoIndexedAddressWithIyFromAWithExpression(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x77);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }

        [TestMethod]
        public void OneSimpleInstructionSymbolWithExpressionPlus()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,$41",
                "        LD     (($3E06+$1A)),A",
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
        public void OneSimpleInstructionSymbolWithExpressionPlusAndEqu()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,$41",
                "        LD     (($3E06+Operand)),A",
                "Hurok   JP     Hurok",
                "Operand EQU    $1A",
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
        public void OneSimpleInstructionSymbolWithExpressionPlusAndEqu2()
        {
            List<string> inputProgram = new List<string>
            {
                "        ORG    $7000",
                "        LD     A,$41",
                "        LD     ((Abs123+$3E06)),A",
                "Hurok   JP     Hurok",
                "Abs123  EQU    $1A",
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
        public void OneSimpleInstructionSymbolWithExpressionPlusAndChainedEqu()
        {
            List<string> inputProgram = new List<string>
            {
                "         ORG    $7000",
                "         LD     A,$41",
                "         LD     (($3E06+Operand)),A",
                "Hurok    JP     Hurok",
                "Operand  EQU    $19+operand2",
                "Operand2 EQU    $1",
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
    }
}
