using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Test.AssemblerInstructionTests
{
    [TestClass]
    public class EquInstructionTests
    {
        [TestMethod]
        public void EquIsWorkingWithByte()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZAM EQU $FA" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 2, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["SZAM"].Type == SymbolType.Constant, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["SZAM"].Value == 0xFA, true);
        }

        [TestMethod]
        public void EquIsWorkingWithAnotherSymbol()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "ELSO = $FA", "SZAM EQU ELSO" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 3, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["ELSO"].Type == SymbolType.Constant, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["ELSO"].Value == 0xFA, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["SZAM"].Type == SymbolType.Constant, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["SZAM"].Value == 0xFA, true);
        }
        [TestMethod]
        public void EquIsWorkingWithByteAndWithEqualSymbol()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "SZAM = $FA" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 2, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["SZAM"].Type == SymbolType.Constant, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["SZAM"].Value == 0xFA, true);
        }

        [TestMethod]
        public void EquIsWorkingCharacter()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "BETU EQU 'A'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 2, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["BETU"].Type == SymbolType.Constant, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["BETU"].Value == 0x41, true);
        }
        [TestMethod]
        public void EquIsWorkingWithWord()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "VIDEO EQU $FA66" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 2, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["VIDEO"].Type == SymbolType.Constant, true);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable["VIDEO"].Value == 0xFA66, true);
        }
        [TestMethod]
        public void EquDoesNotWorkWithLiteral()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "VIDEO EQU 'Literal'" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 2, true);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Az EQU utasítás nem támogatja a megadott operandust"), true);
        }

        [TestMethod]
        public void EquWithReqursiveSymbolDoesNotWork()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "VIDEO EQU (VIDEO+12*sqrt(25))" });
            bool successBuildProgram = assembler.BuildProgram();
            Assert.AreEqual(successBuildProgram, false);
            Assert.AreEqual(assembler.AssembledProgram.SymbolTable.Count == 2, true);
            Assert.AreEqual(assembler.StatusMessage.StartsWith(@"Az EQU utasításban rekurzív szimbólum hivatkozás található"), true);
        }
    }
}
