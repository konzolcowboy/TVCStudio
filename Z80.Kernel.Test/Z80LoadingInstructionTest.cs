using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class Z80LoadingInstructionTest
    {
        [TestMethod]
        public void LdIntoBcFromAPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000","LD (BC),A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length,1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0],0x02);
        }
        [TestMethod]
        public void LdIntoDeFromAPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (DE),A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x12);
        }
        [TestMethod]
        public void LdIntoHlFromAPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x77);
        }
        [TestMethod]
        public void LdIntoHlFromBPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x70);
        }
        [TestMethod]
        public void LdIntoHlFromCPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x71);
        }
        [TestMethod]
        public void LdIntoHlFromDPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x72);
        }
        [TestMethod]
        public void LdIntoHlFromEPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x73);
        }
        [TestMethod]
        public void LdIntoHlFromHPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x74);
        }
        [TestMethod]
        public void LdIntoHlFromLPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD (HL),L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x75);
        }
        [DataTestMethod]
        [DataRow("LD (HL),$AF")]
        [DataRow("LD (HL),175")]
        public void LdIntoHlOneByteConstant(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> {"ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x36);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xAF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),A")]
        [DataRow("LD (IX+$FF),A")]
        public void LdIntoIndexedAddressWithIxFromA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x77);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),B")]
        [DataRow("LD (IX+$FF),B")]
        public void LdIntoIndexedAddressWithIxFromB(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),C")]
        [DataRow("LD (IX+$FF),C")]
        public void LdIntoIndexedAddressWithIxFromC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x71);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),D")]
        [DataRow("LD (IX+$FF),D")]
        public void LdIntoIndexedAddressWithIxFromD(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x72);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),E")]
        [DataRow("LD (IX+$FF),E")]
        public void LdIntoIndexedAddressWithIxFromE(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x73);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),H")]
        [DataRow("LD (IX+$FF),H")]
        public void LdIntoIndexedAddressWithIxFromH(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x74);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),L")]
        [DataRow("LD (IX+$FF),L")]
        public void LdIntoIndexedAddressWithIxFromL(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x75);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD (IX+255),175")]
        [DataRow("LD (IX+$FF),$AF")]
        public void LdConstantIntoIndexedAddressWithIx(string row)
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
        [DataRow("LD (IY+60),A")]
        [DataRow("LD (IY+$3C),A")]
        public void LdIntoIndexedAddressWithIyFromA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x77);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD (IY+60),B")]
        [DataRow("LD (IY+$3C),B")]
        public void LdIntoIndexedAddressWithIyFromB(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD (IY+60),C")]
        [DataRow("LD (IY+$3C),C")]
        public void LdIntoIndexedAddressWithIyFromC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x71);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD (IY+60),D")]
        [DataRow("LD (IY+$3C),D")]
        public void LdIntoIndexedAddressWithIyFromD(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x72);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD (IY+60),E")]
        [DataRow("LD (IY+$3C),E")]
        public void LdIntoIndexedAddressWithIyFromE(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x73);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD (IY+60),H")]
        [DataRow("LD (IY+$3C),H")]
        public void LdIntoIndexedAddressWithIyFromH(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x74);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD (IY+60),L")]
        [DataRow("LD (IY+$3C),L")]
        public void LdIntoIndexedAddressWithIyFromL(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x75);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x3C);
        }
        [DataTestMethod]
        [DataRow("LD ($ACEF),A")]
        [DataRow("LD (44271),A")]
        public void LdInto16BitAddressFromA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xAC);
        }
        [DataTestMethod]
        [DataRow("LD ($12DC),BC")]
        [DataRow("LD (4828),BC")]
        public void LdInto16BitAddressFromBc(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x43);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD ($ACEF),DE")]
        [DataRow("LD (44271),DE")]
        public void LdInto16BitAddressFromDe(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x53);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xAC);
        }
        [DataTestMethod]
        [DataRow("LD ($ACEF),HL")]
        [DataRow("LD (44271),HL")]
        public void LdInto16BitAddressFromHl(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x22);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xAC);
        }
        [DataTestMethod]
        [DataRow("LD ($ACEF),IX")]
        [DataRow("LD (44271),IX")]
        public void LdInto16BitAddressFromIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x22);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xAC);
        }
        [DataTestMethod]
        [DataRow("LD ($ACEF),IY")]
        [DataRow("LD (44271),IY")]
        public void LdInto16BitAddressFromIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x22);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xAC);
        }
        [DataTestMethod]
        [DataRow("LD ($ACEF),SP")]
        [DataRow("LD (44271),SP")]
        public void LdInto16BitAddressFromSp(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x73);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0xAC);
        }
        [TestMethod]
        public void LdIntoAFromBcPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,(BC)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x0A);
        }
        [TestMethod]
        public void LdIntoAFromDePointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,(DE)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x1A);
        }
        [TestMethod]
        public void LdIntoAFromHlPointer()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x7E);
        }
        [DataTestMethod]
        [DataRow("LD A,(IX+255)")]
        [DataRow("LD A,(IX+$FF)")]
        public void LdIntoAFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD A,(IY+255)")]
        [DataRow("LD A,(IY+$FF)")]
        public void LdIntoAFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD A,($ACEF)")]
        [DataRow("LD A,(44271)")]
        public void LdIntoAFrom16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3A);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xEF);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xAC);
        }
        [TestMethod]
        public void LdIntoAFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x7F);
        }
        [TestMethod]
        public void LdIntoAFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x78);
        }
        [TestMethod]
        public void LdIntoAFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x79);
        }
        [TestMethod]
        public void LdIntoAFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x7A);
        }
        [TestMethod]
        public void LdIntoAFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x7B);
        }
        [TestMethod]
        public void LdIntoAFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x7C);
        }
        [TestMethod]
        public void LdIntoAFromI()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,I" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x57);
        }
        [TestMethod]
        public void LdIntoAFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x7D);
        }
        [DataTestMethod]
        [DataRow("LD A,$84")]
        [DataRow("LD A,132")]
        public void Ld8BitConstantIntoA(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [TestMethod]
        public void LdIntoAFromR()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,R" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5F);
        }
        [TestMethod]
        public void LdIntoBFromPointerHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x46);
        }
        [DataTestMethod]
        [DataRow("LD B,(IX+255)")]
        [DataRow("LD B,(IX+$FF)")]
        public void LdIntoBFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x46);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD B,(IY+255)")]
        [DataRow("LD B,(IY+$FF)")]
        public void LdIntoBFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x46);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [TestMethod]
        public void LdIntoBFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x47);
        }
        [TestMethod]
        public void LdIntoBFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x40);
        }
        [TestMethod]
        public void LdIntoBFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x41);
        }
        [TestMethod]
        public void LdIntoBFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x42);
        }
        [TestMethod]
        public void LdIntoBFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x43);
        }
        [TestMethod]
        public void LdIntoBFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x44);
        }
        [TestMethod]
        public void LdIntoBFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x45);
        }
        [DataTestMethod]
        [DataRow("LD B,$84")]
        [DataRow("LD B,132")]
        public void Ld8BitConstantIntoB(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x06);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [DataTestMethod]
        [DataRow("LD BC,($12DC)")]
        [DataRow("LD BC,(4828)")]
        public void LdIntoBFromB16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4B);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD BC,$12DC")]
        [DataRow("LD BC,4828")]
        public void LdI16BitConstantIntoBc(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x12);
        }
        [TestMethod]
        public void LdIntoCFromPointerHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x4E);
        }
        [DataTestMethod]
        [DataRow("LD C,(IX+255)")]
        [DataRow("LD C,(IX+$FF)")]
        public void LdIntoCFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD C,(IY+255)")]
        [DataRow("LD C,(IY+$FF)")]
        public void LdIntoCFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [TestMethod]
        public void LdIntoCFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x4F);
        }
        [TestMethod]
        public void LdIntoCFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x48);
        }
        [TestMethod]
        public void LdIntoCFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x49);
        }
        [TestMethod]
        public void LdIntoCFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x4A);
        }
        [TestMethod]
        public void LdIntoCFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x4B);
        }

        [TestMethod]
        public void LdIntoCFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x4C);
        }


        [TestMethod]
        public void LdIntoCFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x4D);
        }

        [DataTestMethod]
        [DataRow("LD C,$84")]
        [DataRow("LD C,132")]
        public void Ld8BitConstantIntoC(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x0E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [TestMethod]
        public void LdIntoDFromPointerHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x56);
        }
        [DataTestMethod]
        [DataRow("LD D,(IX+255)")]
        [DataRow("LD D,(IX+$FF)")]
        public void LdIntoDFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x56);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD D,(IY+255)")]
        [DataRow("LD D,(IY+$FF)")]
        public void LdIntoDFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x56);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [TestMethod]
        public void LdIntoDFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x57);
        }
        [TestMethod]
        public void LdIntoDFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x50);
        }
        [TestMethod]
        public void LdIntoDFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x51);
        }
        [TestMethod]
        public void LdIntoDFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x52);
        }
        [TestMethod]
        public void LdIntoDFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x53);
        }
        [TestMethod]
        public void LdIntoDFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x54);
        }

        [TestMethod]
        public void LdIntoDFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x55);
        }

        [DataTestMethod]
        [DataRow("LD D,$84")]
        [DataRow("LD D,132")]
        public void Ld8BitConstantIntoD(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x16);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [DataTestMethod]
        [DataRow("LD DE,($12DC)")]
        [DataRow("LD DE,(4828)")]
        public void LdIntoDeFromB16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5B);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD DE,$12DC")]
        [DataRow("LD DE,4828")]
        public void LdI16BitConstantIntoDe(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x11);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x12);
        }
        [TestMethod]
        public void LdIntoEFromPointerHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x5E);
        }
        [DataTestMethod]
        [DataRow("LD E,(IX+255)")]
        [DataRow("LD E,(IX+$FF)")]
        public void LdIntoEFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD E,(IY+255)")]
        [DataRow("LD E,(IY+$FF)")]
        public void LdIntoEFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [TestMethod]
        public void LdIntoEFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x5F);
        }
        [TestMethod]
        public void LdIntoEFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x58);
        }
        [TestMethod]
        public void LdIntoEFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x59);
        }
        [TestMethod]
        public void LdIntoEFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x5A);
        }
        [TestMethod]
        public void LdIntoEFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x5B);
        }
        [TestMethod]
        public void LdIntoEFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x5C);
        }
        [TestMethod]
        public void LdIntoEFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x5D);
        }
        [DataTestMethod]
        [DataRow("LD E,$84")]
        [DataRow("LD E,132")]
        public void Ld8BitConstantIntoE(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x1E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [TestMethod]
        public void LdIntoHFromPointerHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x66);
        }
        [DataTestMethod]
        [DataRow("LD H,(IX+255)")]
        [DataRow("LD H,(IX+$FF)")]
        public void LdIntoHFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x66);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD H,(IY+255)")]
        [DataRow("LD H,(IY+$FF)")]
        public void LdIntoHFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x66);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [TestMethod]
        public void LdIntoHFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x67);
        }
        [TestMethod]
        public void LdIntoHFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x60);
        }
        [TestMethod]
        public void LdIntoHFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x61);
        }
        [TestMethod]
        public void LdIntoHFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x62);
        }
        [TestMethod]
        public void LdIntoHFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x63);
        }
        [TestMethod]
        public void LdIntoHFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x64);
        }
        [TestMethod]
        public void LdIntoHFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD H,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x65);
        }
        [DataTestMethod]
        [DataRow("LD H,$84")]
        [DataRow("LD H,132")]
        public void Ld8BitConstantIntoH(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x26);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [TestMethod]
        public void LdIntoLFromPointerHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,(HL)" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x6E);
        }
        [DataTestMethod]
        [DataRow("LD L,(IX+255)")]
        [DataRow("LD L,(IX+$FF)")]
        public void LdIntoLFromIndexedAddressWithIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [DataTestMethod]
        [DataRow("LD L,(IY+255)")]
        [DataRow("LD L,(IY+$FF)")]
        public void LdIntoLFromIndexedAddressWithIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xFF);
        }
        [TestMethod]
        public void LdIntoLFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x6F);
        }
        [TestMethod]
        public void LdIntoLFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x68);
        }
        [TestMethod]
        public void LdIntoLFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x69);
        }
        [TestMethod]
        public void LdIntoLFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x6A);
        }
        [TestMethod]
        public void LdIntoLFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x6B);
        }
        [TestMethod]
        public void LdIntoLFromH()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,H" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x6C);
        }
        [TestMethod]
        public void LdIntoLFromL()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD L,L" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x6D);
        }
        [DataTestMethod]
        [DataRow("LD L,$84")]
        [DataRow("LD L,132")]
        public void Ld8BitConstantIntoL(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x2E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x84);
        }
        [DataTestMethod]
        [DataRow("LD HL,($12DC)")]
        [DataRow("LD HL,(4828)")]
        public void LdIntoHlFromB16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x2A);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD HL,$12DC")]
        [DataRow("LD HL,4828")]
        public void LdI16BitConstantIntoHl(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x12);
        }
        [TestMethod]
        public void LdIntoIFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD I,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x47);
        }
        [DataTestMethod]
        [DataRow("LD IX,($12DC)")]
        [DataRow("LD IX,(4828)")]
        public void LdIntoIxFromB16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2A);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }

        [DataTestMethod]
        [DataRow("LD IX,$12DC")]
        [DataRow("LD IX,4828")]
        public void LdI16BitConstantIntoIx(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD IY,($12DC)")]
        [DataRow("LD IY,(4828)")]
        public void LdIntoIyFromB16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2A);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD IY,$12DC")]
        [DataRow("LD IY,4828")]
        public void LdI16BitConstantIntoIy(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [TestMethod]
        public void LdIntoRFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD R,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4F);
        }
        [DataTestMethod]
        [DataRow("LD SP,($12DC)")]
        [DataRow("LD SP,(4828)")]
        public void LdIntoSpFromB16BitAddress(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 4);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7B);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x12);
        }
        [DataTestMethod]
        [DataRow("LD SP,$12DC")]
        [DataRow("LD SP,4828")]
        public void LdI16BitConstantIntoSp(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x31);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xDC);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x12);
        }
        [TestMethod]
        public void LdIntoSpFromHl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD SP,HL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 1);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xF9);
        }
        [TestMethod]
        public void LdIntoSpFromIx()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD SP,IX" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xF9);
        }
        [TestMethod]
        public void LdIntoSpFromIy()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD SP,IY" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xF9);
        }
        [TestMethod]
        public void InstructionLdd()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LDD" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA8);
        }
        [TestMethod]
        public void InstructionLdi()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LDI" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xA0);
        }
        [TestMethod]
        public void InstructionLddr()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LDDR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB8);
        }
        [TestMethod]
        public void InstructionLdir()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LDIR" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB0);
        }


        // not published instructions

        [DataTestMethod]
        [DataRow("LD IXH,$DC")]
        [DataRow("LD IXH,220")]
        public void LdI8BitConstantIntoIxh(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x26);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
        }

        [DataTestMethod]
        [DataRow("LD IXL,$DC")]
        [DataRow("LD IXL,220")]
        public void LdI8BitConstantIntoIxl(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
        }
        [DataTestMethod]
        [DataRow("LD IYH,$DC")]
        [DataRow("LD IYH,220")]
        public void LdI8BitConstantIntoIyh(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x26);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
        }

        [DataTestMethod]
        [DataRow("LD IYL,$DC")]
        [DataRow("LD IYL,220")]
        public void LdI8BitConstantIntoIyl(string row)
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", row });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 3);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x2E);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0xDC);
        }
        [TestMethod]
        public void LdIntoAFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7C);
        }
        [TestMethod]
        public void LdIntoAFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7D);
        }
        [TestMethod]
        public void LdIntoAFromIyh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7C);
        }
        [TestMethod]
        public void LdIntoAFromIyl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD A,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x7D);
        }
        [TestMethod]
        public void LdIntoBFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x44);
        }

        [TestMethod]
        public void LdIntoBFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x45);
        }
        [TestMethod]
        public void LdIntoBFromIyh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x44);
        }

        [TestMethod]
        public void LdIntoBFromIyl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD B,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x45);
        }
        [TestMethod]
        public void LdIntoCFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4C);
        }

        [TestMethod]
        public void LdIntoCFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [TestMethod]
        public void LdIntoCFromIyh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4C);
        }

        [TestMethod]
        public void LdIntoCFromIyl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD C,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x4D);
        }
        [TestMethod]
        public void LdIntoDFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x54);
        }

        [TestMethod]
        public void LdIntoDFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x55);
        }
        [TestMethod]
        public void LdIntoDFromIyh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x54);
        }

        [TestMethod]
        public void LdIntoDFromIyl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD D,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x55);
        }
        [TestMethod]
        public void LdIntoEFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5C);
        }

        [TestMethod]
        public void LdIntoEFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }
        [TestMethod]
        public void LdIntoEFromIyh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5C);
        }

        [TestMethod]
        public void LdIntoEFromIyl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD E,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x5D);
        }
        
        //----------------------------------

        [TestMethod]
        public void LdIntoIxhFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x67);
        }
        [TestMethod]
        public void LdIntoIxhFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x60);
        }
        [TestMethod]
        public void LdIntoIxhFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x61);
        }
        [TestMethod]
        public void LdIntoIxhFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x62);
        }
        [TestMethod]
        public void LdIntoIxhFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x63);
        }
        [TestMethod]
        public void LdIntoIxhFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x64);
        }
        [TestMethod]
        public void LdIntoIxhFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXH,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x65);
        }

        //----------------------------------------------------
        [TestMethod]
        public void LdIntoIxlFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6F);
        }
        [TestMethod]
        public void LdIntoIxlFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x68);
        }
        [TestMethod]
        public void LdIntoIxlFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x69);
        }
        [TestMethod]
        public void LdIntoIxlFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6A);
        }
        [TestMethod]
        public void LdIntoIxlFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6B);
        }
        [TestMethod]
        public void LdIntoIxlFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,IXH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6C);
        }

        [TestMethod]
        public void LdIntoIxlFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IXL,IXL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xDD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6D);
        }

        //----------------------------------

        [TestMethod]
        public void LdIntoIyhFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x67);
        }
        [TestMethod]
        public void LdIntoIyhFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x60);
        }
        [TestMethod]
        public void LdIntoIyhFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x61);
        }
        [TestMethod]
        public void LdIntoIyhFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x62);
        }
        [TestMethod]
        public void LdIntoIyhFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x63);
        }
        [TestMethod]
        public void LdIntoIyhFromIxh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x64);
        }
        [TestMethod]
        public void LdIntoIyhFromIxl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYH,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x65);
        }

        //----------------------------------------------------
        [TestMethod]
        public void LdIntoIylFromA()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,A" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6F);
        }
        [TestMethod]
        public void LdIntoIylFromB()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,B" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x68);
        }
        [TestMethod]
        public void LdIntoIylFromC()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,C" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x69);
        }
        [TestMethod]
        public void LdIntoIylFromD()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,D" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6A);
        }
        [TestMethod]
        public void LdIntoIylFromE()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,E" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6B);
        }
        [TestMethod]
        public void LdIntoIylFromIyh()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,IYH" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6C);
        }
        [TestMethod]
        public void LdIntoIylFromIyl()
        {
            var assembler = new Z80Assembler.Z80Assembler(new List<string> { "ORG $3000", "LD IYL,IYL" });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 2);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0xFD);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0x6D);
        }
    }
}
