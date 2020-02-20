using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;

namespace TVC.Basic.Test
{
    /// <summary>
    /// This test class tests the Z80 assembler, the BasicLoader and the cas file generator classes together
    /// </summary>
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void IntegrationTest1()
        {
            List<string> programrows = new List<string>
            {
                "ORG $3000",
                "RST $30",
                "DB  $05",
                "LD  BC,$010C",
                "RST $30",
                "DB  $03",
                "LD  DE,SZOVEG",
                "LD  BC,21",
                "RST $30",
                "DB  $02",
                "RET",
                "SZOVEG  DB 'ÁRVÍZTŰRŐTÜKÖRFÚRÓGÉP'"
            };

            string fileName = @"IntegrationTest1.cas";

            var assembler = new Z80Assembler(programrows);
            var assembled = assembler.BuildProgram();
            Assert.AreEqual(assembled, true);

            var loader = new TvcBasicLoader(assembler.AssembledProgram, 1, 1, 0x3000);
            bool loaderGenerated = loader.GenerateBasicLoader();
            Assert.AreEqual(loaderGenerated,true);

            CasFileWriter writer = new CasFileWriter(fileName);
            writer.Write(loader.BasicLoaderProgramBytes.ToArray());

            var bytesOfCreatedFile = File.ReadAllBytes(fileName);
            var bytesOfBaseFile = File.ReadAllBytes(IntegrationTest1TestFileName);
            Assert.AreEqual(bytesOfCreatedFile.SequenceEqual(bytesOfBaseFile), true);
        }

        private static readonly string IntegrationTest1TestFileName = Path.Combine(@"TestFiles", @"IntegrationTest1_base.cas");
    }
}
