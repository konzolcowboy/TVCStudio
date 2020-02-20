using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Test.AssemblerInstructionTests
{
    [TestClass]
    public class DbInstructionTests
    {
        [TestMethod]
        public void DbWithLiteralAndOneByteWithOutSpaceInLiteral()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> {"ORG $3000", "SZOVEG DB 'AAA',$00" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "'AAA'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
        }
        [TestMethod]
        public void DbWithLiteralAndOneByteWithSpaceInLiteral()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DB 'EZ EGY LITERAL SPACE-EL',$00" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "'EZ EGY LITERAL SPACE-EL'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
        }
        [TestMethod]
        public void DbWithLiteralAndOneByteWithSpaceInLiteralAndByteAsFirst()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DB $00,'EZ EGY LITERAL SPACE-EL'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "'EZ EGY LITERAL SPACE-EL'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Literal, true);
        }

        [TestMethod]
        public void DbWithLiteralAndOneByteWithSpaceInLiteralNotUpperAndByteAsFirst()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "org $3000", "SZOVEG Db $00,'Ez Egy Literal Space-EL'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "'Ez Egy Literal Space-EL'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Literal, true);
        }
        
        [TestMethod]
        public void DbWithThreeBytes()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "HAROMBYTE  DB $01,$02,$03" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "HAROMBYTE", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 3, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$01", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$02", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "$03", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Byte, true);
        }
        [TestMethod]
        public void DbWithThreeBytesAndLiteralAndDecimal()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "TizenKilencByte  DB $01,$02,$03,'EGY EGY LITERAL',123" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "TIZENKILENCBYTE", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 5, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$01", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$02", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "$03", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[3].ToString() == "'EGY EGY LITERAL'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[3].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[4].ToString() == "123", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[4].Info.DataType == OperandType.Decimal, true);
        }
        [TestMethod]
        public void DbWithThreeBytesAndLiteralAndDecimal_2()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "TizenKilencByte  DB $01,$02,$03,\"EGY EGY LITERAL\",123" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "TIZENKILENCBYTE", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 5, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$01", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$02", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "$03", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[3].ToString() == "\"EGY EGY LITERAL\"", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[3].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[4].ToString() == "123", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[4].Info.DataType == OperandType.Decimal, true);
        }
        [TestMethod]
        public void LiteralIsNotClosed()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "TizenKilencByte  DB $01,$02,$03,\"EGY EGY LITERAL,123" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Szintaxis hiba: a karakterlánc nincs lezárva"), true);
        }
        [TestMethod]
        public void DbInstructionCouldNotBeInterpretedBecauseOfBigNumber()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "HIBA DB $FF,$FFAB" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"A(z) $FFAB operandus típusa(Word) a DB utasításban nem támogatott!"), true);
        }
        [TestMethod]
        public void DbInstructionCouldNotBeInterpretedBecauseOfBigDecimalNumber()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "HIBA DB $FF,300" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Helytelen decimális érték '300' a DB utasításban! Az értéknek 0 és 255 közé kell esnie!"), true);
        }

        [TestMethod]
        public void DbWithLiteralAndOneByteCuoldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DB $FA,'A AA',$00" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 3, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$FA", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "'A AA'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "$00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 6, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0xFA, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x20, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x00, true);
        }

        [TestMethod]
        public void DbWithCharacterCuoldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DB 'A'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "'A'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Character, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 1, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
        }

        [TestMethod]
        public void DbWithLiteralAndColonCuoldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DB 'A,A'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "'A,A'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 3, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x2C, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x41, true);
        }

        [TestMethod]
        public void DbTwoLiteralsWithColonCuoldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DB 'A,A','A,A'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "'A,A'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "'A,A'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 6, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x2C, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x2C, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x41, true);
        }

        [TestMethod]
        public void DbWithBytesBetweenLiteralsCuoldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> {"ORG $3000", "SZOVEG DB 'A AA',$00,\"AA\",$FA" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 4, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "'A AA'", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "\"AA\"", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Literal, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 8, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x20, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x00, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[6] == 0x41, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[7] == 0xFA, true);
        }
        [TestMethod]
        public void DbInstructionNeedsOperand()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> {"ORG $3000", "HIBA DB " });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"A DB utasításnak szüksége van operandusa!"), true);
        }
        [TestMethod]
        public void DbWithThreeExpression()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "Harombyte  DB high($ABCD),low($ABCD),((123+45)/2)" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "HAROMBYTE", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 3, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "HIGH($ABCD)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "LOW($ABCD)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "((123+45)/2)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].InstructionBytes.Count, 3);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].InstructionBytes[0], 0xab);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].InstructionBytes[1], 0xcd);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].InstructionBytes[2], (123 + 45) / 2);
        }
        [TestMethod]
        public void DbWithTooBigExpression()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "Harombyte  DB 1500*2000" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"A kifejezés '1500*2000' eredménye nem fért el egy byte-on!"), true);
        }
    }
}
