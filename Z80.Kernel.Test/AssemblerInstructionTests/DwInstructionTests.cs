using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Test.AssemblerInstructionTests
{
    [TestClass]
    public class DwInstructionTests
    {
        [TestMethod]
        public void DwWithSimpleWordCouldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DW $FDA9" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$FDA9", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 2,true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0xA9, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0xFD, true);
        }
        [TestMethod]
        public void DwThreeWordsCouldBeAssembled()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DW $FDA9,$FF,$AB00" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 3, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$FDA9", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$FF", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "$AB00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 6, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0xA9, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0xFD, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0xFF, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x00, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x00, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0xAB, true);
        }
        [TestMethod]
        public void DwThreeWordsCouldBeAssembledWithExtraCommaAtTheEnd()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DW $FDA9,$FF,$AB00," });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 3, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "$FDA9", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "$FF", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Byte, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "$AB00", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Word, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 6, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0xA9, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0xFD, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0xFF, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x00, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x00, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0xAB, true);
        }
        [TestMethod]
        public void DwThreeWordsCouldNotBeAssembledWithExtraCommaAtTheBeginning()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DW ,$FDA9,$FF,$AB00" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Szintaxis hiba: helytelen karakter:','!"), true);
        }
        [TestMethod]
        public void DwThreeWordsCouldNotBeAssembledWithLiteralAtTheEnd()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DW $FDA9,$FF,$AB00,'Literal not supported!'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Az operandus adat típusa(Literal) a DW utasításban nem támogatott!"), true);
        }
        [TestMethod]
        public void DwThreeWordsWithThreeExpression()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZOVEG DW abs(-1500),8*1000,(4500*2)" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 2, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Label == "SZOVEG", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands.Count == 3, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].ToString() == "ABS(-1500)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[0].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].ToString() == "8*1000", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[1].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].ToString() == "(4500*2)", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[1].Operands[2].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 6, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0xDC, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x05, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x40, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x1f, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[4] == 0x28, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[5] == 0x23, true);
        }

        [TestMethod]
        public void DwWithSymbolAddress()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DB 1","SECONDBYTE DB 2", "DW SECONDBYTE" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 4, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[3].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[3].Operands[0].ToString() == "SECONDBYTE", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[3].Operands[0].Info.DataType == OperandType.Unknown, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 4, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x01, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x02, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x01, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x30, true);
        }

        [TestMethod]
        public void DwWithSymbolExpression()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "DB 1", "SECONDBYTE DB 2", "DW SECONDBYTE+$10" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows.Count == 4, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[3].Operands.Count == 1, true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[3].Operands[0].ToString() == "SECONDBYTE+$10", true);
            Assert.AreEqual(assembler.InterPretedAssemblyRows[3].Operands[0].Info.DataType == OperandType.Expression, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length == 4, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[0] == 0x01, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[1] == 0x02, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[2] == 0x11, true);
            Assert.AreEqual(assembler.AssembledProgramBytes[3] == 0x30, true);
        }
    }
}
