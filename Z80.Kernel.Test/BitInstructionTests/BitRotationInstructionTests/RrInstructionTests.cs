using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.BitInstructionTests.BitRotationInstructionTests
{
    [TestClass]
    public class RrInstructionTests
    {
        [TestMethod]
        public void RrWithHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR (HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1E);

        }
        [TestMethod]
        public void RrWithA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1F);
        }
        [TestMethod]
        public void RrWithB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x18);
        }

        [TestMethod]
        public void RrWithC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x19);
        }
        [TestMethod]
        public void RrWithD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1A);
        }
        [TestMethod]
        public void RrWithE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1B);
        }
        [TestMethod]
        public void RrWithH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1C);
        }
        [TestMethod]
        public void RrWithL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "RR L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x1D);
        }
        [DataTestMethod]
        [DataRow("RR (IX+$5D)")]
        [DataRow("RR (IX+93)")]
        public void RrWithIxWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1E);
        }

        [DataTestMethod]
        [DataRow("RR (IY+$5D)")]
        [DataRow("RR (IY+93)")]
        public void RrWithIyWithOffset(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1E);
        }

        // not published instructions

        [DataTestMethod]
        [DataRow("RR (IX+$5D),A")]
        [DataRow("RR (IX+93),A")]
        public void RrWithIxAndOffsetIntoA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1F);
        }

        [DataTestMethod]
        [DataRow("RR (IX+$5D),B")]
        [DataRow("RR (IX+93),B")]
        public void RrWithIxAndOffsetIntoB(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x18);
        }
        [DataTestMethod]
        [DataRow("RR (IX+$5D),C")]
        [DataRow("RR (IX+93),C")]
        public void RrWithIxAndOffsetIntoC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x19);
        }
        [DataTestMethod]
        [DataRow("RR (IX+$5D),D")]
        [DataRow("RR (IX+93),D")]
        public void RrWithIxAndOffsetIntoD(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1A);
        }
        [DataTestMethod]
        [DataRow("RR (IX+$5D),E")]
        [DataRow("RR (IX+93),E")]
        public void RrWithIxAndOffsetIntoE(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1B);
        }
        [DataTestMethod]
        [DataRow("RR (IX+$5D),H")]
        [DataRow("RR (IX+93),H")]
        public void RrWithIxAndOffsetIntoH(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1C);
        }
        [DataTestMethod]
        [DataRow("RR (IX+$5D),L")]
        [DataRow("RR (IX+93),L")]
        public void RrWithIxAndOffsetIntoL(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1D);
        }

        [DataTestMethod]
        [DataRow("RR (IY+$5D),A")]
        [DataRow("RR (IY+93),A")]
        public void RrWithIyAndOffsetIntoA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1F);
        }

        [DataTestMethod]
        [DataRow("RR (IY+$5D),B")]
        [DataRow("RR (IY+93),B")]
        public void RrWithIyAndOffsetIntoB(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x18);
        }
        [DataTestMethod]
        [DataRow("RR (IY+$5D),C")]
        [DataRow("RR (IY+93),C")]
        public void RrWithIyAndOffsetIntoC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x19);
        }
        [DataTestMethod]
        [DataRow("RR (IY+$5D),D")]
        [DataRow("RR (IY+93),D")]
        public void RrWithIyAndOffsetIntoD(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1A);
        }
        [DataTestMethod]
        [DataRow("RR (IY+$5D),E")]
        [DataRow("RR (IY+93),E")]
        public void RrWithIyAndOffsetIntoE(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1B);
        }
        [DataTestMethod]
        [DataRow("RR (IY+$5D),H")]
        [DataRow("RR (IY+93),H")]
        public void RrWithIyAndOffsetIntoH(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1C);
        }
        [DataTestMethod]
        [DataRow("RR (IY+$5D),L")]
        [DataRow("RR (IY+93),L")]
        public void RrWithIyAndOffsetIntoL(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xCB);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x5D);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x1D);
        }

    }
}
