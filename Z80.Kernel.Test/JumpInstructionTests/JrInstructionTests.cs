using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.JumpInstructionTests
{
    [TestClass]
    public class JrInstructionTests
    {
        [DataTestMethod]
        [DataRow("JR C,$304D")]
        [DataRow("JR C,12365")]
        public void JrWithC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $2FFE", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x38);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JR NC,$304D")]
        [DataRow("JR NC,12365")]
        public void JrWithNc(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $2FFE", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x30);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JR Z,$304D")]
        [DataRow("JR Z,12365")]
        public void JrWithZ(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $2FFE", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x28);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JR NZ,$304D")]
        [DataRow("JR NZ,12365")]
        public void JrWithNz(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $2FFE", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JR $304D")]
        [DataRow("JR 12365")]
        public void JrWithOutCondition(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $2FFE", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("DJNZ $304D")]
        [DataRow("DJNZ 12365")]
        public void DjNz(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $2FFE", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x10);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [DataTestMethod]
        [DataRow("JR C,$3004")]
        [DataRow("JR C,12292")]
        public void JrWithCNegative(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3027", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x38);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDB);
        }
        [DataTestMethod]
        [DataRow("JR NC,$3004")]
        [DataRow("JR NC,12292")]
        public void JrWithNcNegative(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3027", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x30);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDB);
        }
        [DataTestMethod]
        [DataRow("JR Z,$3004")]
        [DataRow("JR Z,12292")]
        public void JrWithZNegative(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3027", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x28);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDB);
        }
        [DataTestMethod]
        [DataRow("JR NZ,$3004")]
        [DataRow("JR NZ,12292")]
        public void JrWithNzNegative(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3027", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x20);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDB);
        }
        [DataTestMethod]
        [DataRow("JR $3004")]
        [DataRow("JR 12292")]
        public void JrWithOutConditionNegative(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3027", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x18);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDB);
        }
        [DataTestMethod]
        [DataRow("DJNZ $3004")]
        [DataRow("DJNZ 12292")]
        public void DjNzNegative(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3027", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x10);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDB);
        }
    }
}
