using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Z80.Kernel.Test.PreProcessorTests
{

    [TestClass]
    public class IncludeTests
    {
        [TestMethod]
        public void IncludeOneFile()
        {
            List<string> inputProgram = new List<string>
            {
                "    #define TVC",
                "   #ifdef  TVC",
                "   #include testdata\\includeTest.tvcasm",
                "   #else",
                "    #include testdata\\includeSpecci.tvcasm",
                "    #endif"

            };

            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(inputProgram, new List<string> { includePath });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, true);
            Assert.AreEqual(assembler.AssembledProgramBytes.Length, 25);
            Assert.AreEqual(assembler.AssembledProgramBytes[0], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[1], 0xB0);
            Assert.AreEqual(assembler.AssembledProgramBytes[2], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[3], 0x03);
            Assert.AreEqual(assembler.AssembledProgramBytes[4], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[5], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[6], 0x02);
            Assert.AreEqual(assembler.AssembledProgramBytes[7], 0x21);
            Assert.AreEqual(assembler.AssembledProgramBytes[8], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[9], 0x30);
            Assert.AreEqual(assembler.AssembledProgramBytes[10], 0x11);
            Assert.AreEqual(assembler.AssembledProgramBytes[11], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[12], 0xC0);
            Assert.AreEqual(assembler.AssembledProgramBytes[13], 0x01);
            Assert.AreEqual(assembler.AssembledProgramBytes[14], 0x14);
            Assert.AreEqual(assembler.AssembledProgramBytes[15], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[16], 0xED);
            Assert.AreEqual(assembler.AssembledProgramBytes[17], 0xB0);
            Assert.AreEqual(assembler.AssembledProgramBytes[18], 0x3E);
            Assert.AreEqual(assembler.AssembledProgramBytes[19], 0x70);
            Assert.AreEqual(assembler.AssembledProgramBytes[20], 0x32);
            Assert.AreEqual(assembler.AssembledProgramBytes[21], 0x03);
            Assert.AreEqual(assembler.AssembledProgramBytes[22], 0x00);
            Assert.AreEqual(assembler.AssembledProgramBytes[23], 0xD3);
            Assert.AreEqual(assembler.AssembledProgramBytes[24], 0x02);
        }
        [TestMethod]
        public void IncludeTheSameFileMoreTimesDoesNotWorks()
        {
            List<string> inputProgram = new List<string>
            {
                "   #include testdata\\includeTest.tvcasm",
                "    #define TVC",
                "   #ifdef  TVC",
                "   #include testdata\\includeTest.tvcasm",
                "   #else",
                "    #include testdata\\includeSpecci.tvcasm",
                "    #endif"

            };

            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(inputProgram, new List<string> { includePath }, "UnitTest.tvcasm");
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Rekurzív file hivatkozás az #INCLUDE utasításban!"));
        }

        [TestMethod]
        public void IncludeWithWrongPath()
        {
            List<string> inputProgram = new List<string>
            {
                "    #define TVC",
                "   #ifdef  TVC",
                "   #include testdata23\\includeTest.tvcasm",
                "   #else",
                "    #include testdata\\includeSpecci.tvcasm",
                "    #endif"

            };

            var includePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembler = new Z80Assembler.Z80Assembler(inputProgram, new List<string> { includePath });
            bool result = assembler.BuildProgram();
            Assert.AreEqual(result, false);
            Assert.IsTrue(assembler.StatusMessage.StartsWith("Az #INCLUDE utasításban megadott file:TESTDATA23\\INCLUDETEST.TVCASM nem található a megadott útvonalak egyikén sem!"));
        }
    }
}
