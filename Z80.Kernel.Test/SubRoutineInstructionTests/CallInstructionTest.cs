using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.SubRoutineInstructionTests
{
    [TestClass]
    public class CallInstructionTest
    {
        [DataTestMethod]
        [DataRow("CALL C,$4DFD")]
        [DataRow("CALL C,19965")]
        public void CallWithC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL NC,$4DFD")]
        [DataRow("CALL NC,19965")]
        public void CallWithNc(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xD4);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL Z,$4DFD")]
        [DataRow("CALL Z,19965")]
        public void CallWithZ(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCC);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL NZ,$4DFD")]
        [DataRow("CALL NZ,19965")]
        public void CallWithNz(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xC4);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL P,$4DFD")]
        [DataRow("CALL P,19965")]
        public void CallWithP(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF4);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL M,$4DFD")]
        [DataRow("CALL M,19965")]
        public void CallWithM(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFC);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL PO,$4DFD")]
        [DataRow("CALL PO,19965")]
        public void CallWithPO(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE4);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }

        [DataTestMethod]
        [DataRow("CALL P0,$4DFD")]
        [DataRow("CALL P0,19965")]
        public void CallWithP0(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xE4);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL PE,$4DFD")]
        [DataRow("CALL PE,19965")]
        public void CallWithPe(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xEC);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
        [DataTestMethod]
        [DataRow("CALL $4DFD")]
        [DataRow("CALL 19965")]
        public void CallWithOutCondition(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x4D);
        }
    }
}
