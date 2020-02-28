using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TVC.Basic.Test
{
    [TestClass]
    public class TvcBasicRowTests
    {
        [TestMethod]
        public void BasicDataRowWithComment()
        {
            TvcBasicRow row = new TvcBasicRow(@"1DATA 1,2,3!KOMMENT");
            Assert.AreEqual(row.TokenizedBytes.Length == 19, true);
            Assert.AreEqual(row.TokenizedBytes[0] == 0x13, true);
            Assert.AreEqual(row.TokenizedBytes[1] == 0x01, true);
            Assert.AreEqual(row.TokenizedBytes[2] == 0x00, true);
            Assert.AreEqual(row.TokenizedBytes[3] == 0xFB, true);
            Assert.AreEqual(row.TokenizedBytes[4] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[5] == 0x31, true);
            Assert.AreEqual(row.TokenizedBytes[6] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[7] == 0x32, true);
            Assert.AreEqual(row.TokenizedBytes[8] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[9] == 0x33, true);
            Assert.AreEqual(row.TokenizedBytes[10] == 0x21, true);
            Assert.AreEqual(row.TokenizedBytes[11] == 0x4B, true);
            Assert.AreEqual(row.TokenizedBytes[12] == 0x4F, true);
            Assert.AreEqual(row.TokenizedBytes[13] == 0x4D, true);
            Assert.AreEqual(row.TokenizedBytes[14] == 0x4D, true);
            Assert.AreEqual(row.TokenizedBytes[15] == 0x45, true);
            Assert.AreEqual(row.TokenizedBytes[16] == 0x4E, true);
            Assert.AreEqual(row.TokenizedBytes[17] == 0x54, true);
            Assert.AreEqual(row.TokenizedBytes[18] == 0xFF, true);
        }
        [TestMethod]
        public void BasicDataRowWithFor()
        {
            TvcBasicRow row = new TvcBasicRow(@"1 FOR I=1 TO 5:NEXT I:DATA1,2:LET A=2");
            Assert.AreEqual(row.TokenizedBytes.Length == 28, true);
            Assert.AreEqual(row.TokenizedBytes[0] == 0x1C, true);
            Assert.AreEqual(row.TokenizedBytes[1] == 0x01, true);
            Assert.AreEqual(row.TokenizedBytes[2] == 0x00, true);
            Assert.AreEqual(row.TokenizedBytes[3] == 0xF2, true);
            Assert.AreEqual(row.TokenizedBytes[4] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[5] == 0x49, true);
            Assert.AreEqual(row.TokenizedBytes[6] == 0x9A, true);
            Assert.AreEqual(row.TokenizedBytes[7] == 0x31, true);
            Assert.AreEqual(row.TokenizedBytes[8] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[9] == 0xB4, true);
            Assert.AreEqual(row.TokenizedBytes[10] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[11] == 0x35, true);
            Assert.AreEqual(row.TokenizedBytes[12] == 0xFD, true);
            Assert.AreEqual(row.TokenizedBytes[13] == 0xE5, true);
            Assert.AreEqual(row.TokenizedBytes[14] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[15] == 0x49, true);
            Assert.AreEqual(row.TokenizedBytes[16] == 0xFD, true);
            Assert.AreEqual(row.TokenizedBytes[17] == 0xFB, true);
            Assert.AreEqual(row.TokenizedBytes[18] == 0x31, true);
            Assert.AreEqual(row.TokenizedBytes[19] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[20] == 0x32, true);
            Assert.AreEqual(row.TokenizedBytes[21] == 0xFD, true);
            Assert.AreEqual(row.TokenizedBytes[22] == 0xEB, true);
            Assert.AreEqual(row.TokenizedBytes[23] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[24] == 0x41, true);
            Assert.AreEqual(row.TokenizedBytes[25] == 0x9A, true);
            Assert.AreEqual(row.TokenizedBytes[26] == 0x32, true);
            Assert.AreEqual(row.TokenizedBytes[27] == 0xFF, true);
        }
        [TestMethod]
        public void BasicDataRowWithLiteral()
        {
            TvcBasicRow row = new TvcBasicRow("1data 1,2,\"T:,\",3");
            Assert.AreEqual(row.TokenizedBytes.Length == 17, true);
            Assert.AreEqual(row.TokenizedBytes[0] == 0x11, true);
            Assert.AreEqual(row.TokenizedBytes[1] == 0x01, true);
            Assert.AreEqual(row.TokenizedBytes[2] == 0x00, true);
            Assert.AreEqual(row.TokenizedBytes[3] == 0xFB, true);
            Assert.AreEqual(row.TokenizedBytes[4] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[5] == 0x31, true);
            Assert.AreEqual(row.TokenizedBytes[6] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[7] == 0x32, true);
            Assert.AreEqual(row.TokenizedBytes[8] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[9] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[10] == 0x54, true);
            Assert.AreEqual(row.TokenizedBytes[11] == 0x3A, true);
            Assert.AreEqual(row.TokenizedBytes[12] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[13] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[14] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[15] == 0x33, true);
            Assert.AreEqual(row.TokenizedBytes[16] == 0xFF, true);
        }
        [TestMethod]
        public void BasicRowWithDataAndStringVariable()
        {
            TvcBasicRow row = new TvcBasicRow("1dATa 1,2,\":\",3:leta$=\"hElLo\"");
            Assert.AreEqual(row.TokenizedBytes.Length == 27, true);
            Assert.AreEqual(row.TokenizedBytes[0] == 0x1B, true);
            Assert.AreEqual(row.TokenizedBytes[1] == 0x01, true);
            Assert.AreEqual(row.TokenizedBytes[2] == 0x00, true);
            Assert.AreEqual(row.TokenizedBytes[3] == 0xFB, true);
            Assert.AreEqual(row.TokenizedBytes[4] == 0x20, true);
            Assert.AreEqual(row.TokenizedBytes[5] == 0x31, true);
            Assert.AreEqual(row.TokenizedBytes[6] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[7] == 0x32, true);
            Assert.AreEqual(row.TokenizedBytes[8] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[9] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[10] == 0x3A, true);
            Assert.AreEqual(row.TokenizedBytes[11] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[12] == 0x2C, true);
            Assert.AreEqual(row.TokenizedBytes[13] == 0x33, true);
            Assert.AreEqual(row.TokenizedBytes[14] == 0xFD, true);
            Assert.AreEqual(row.TokenizedBytes[15] == 0xEB, true);
            Assert.AreEqual(row.TokenizedBytes[16] == 0x41, true);
            Assert.AreEqual(row.TokenizedBytes[17] == 0x24, true);
            Assert.AreEqual(row.TokenizedBytes[18] == 0x9A, true);
            Assert.AreEqual(row.TokenizedBytes[19] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[20] == 0x68, true);
            Assert.AreEqual(row.TokenizedBytes[21] == 0x45, true);
            Assert.AreEqual(row.TokenizedBytes[22] == 0x6C, true);
            Assert.AreEqual(row.TokenizedBytes[23] == 0x4C, true);
            Assert.AreEqual(row.TokenizedBytes[24] == 0x6F, true);
            Assert.AreEqual(row.TokenizedBytes[25] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[26] == 0xFF, true);
        }
        [TestMethod]
        public void BasicRowCanTokeniseHungarianCharacters()
        {
            TvcBasicRow row = new TvcBasicRow("1print\"árvÍztűrőtűkörfúrógép\"");
            Assert.AreEqual(row.TokenizedBytes.Length == 28, true);
            Assert.AreEqual(row.TokenizedBytes[0] == 0x1C, true);
            Assert.AreEqual(row.TokenizedBytes[1] == 0x01, true);
            Assert.AreEqual(row.TokenizedBytes[2] == 0x00, true);
            Assert.AreEqual(row.TokenizedBytes[3] == 0xDD, true);
            Assert.AreEqual(row.TokenizedBytes[4] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[5] == 0x10, true);
            Assert.AreEqual(row.TokenizedBytes[6] == 0x72, true);
            Assert.AreEqual(row.TokenizedBytes[7] == 0x76, true);
            Assert.AreEqual(row.TokenizedBytes[8] == 0x02, true);
            Assert.AreEqual(row.TokenizedBytes[9] == 0x7A, true);
            Assert.AreEqual(row.TokenizedBytes[10] == 0x74, true);
            Assert.AreEqual(row.TokenizedBytes[11] == 0x18, true);
            Assert.AreEqual(row.TokenizedBytes[12] == 0x72, true);
            Assert.AreEqual(row.TokenizedBytes[13] == 0x15, true);
            Assert.AreEqual(row.TokenizedBytes[14] == 0x74, true);
            Assert.AreEqual(row.TokenizedBytes[15] == 0x18, true);
            Assert.AreEqual(row.TokenizedBytes[16] == 0x6B, true);
            Assert.AreEqual(row.TokenizedBytes[17] == 0x14, true);
            Assert.AreEqual(row.TokenizedBytes[18] == 0x72, true);
            Assert.AreEqual(row.TokenizedBytes[19] == 0x66, true);
            Assert.AreEqual(row.TokenizedBytes[20] == 0x16, true);
            Assert.AreEqual(row.TokenizedBytes[21] == 0x72, true);
            Assert.AreEqual(row.TokenizedBytes[22] == 0x13, true);
            Assert.AreEqual(row.TokenizedBytes[23] == 0x67, true);
            Assert.AreEqual(row.TokenizedBytes[24] == 0x11, true);
            Assert.AreEqual(row.TokenizedBytes[25] == 0x70, true);
            Assert.AreEqual(row.TokenizedBytes[26] == 0x22, true);
            Assert.AreEqual(row.TokenizedBytes[27] == 0xFF, true);
        }

        [TestMethod]
        public void SpacesAreRemovedFromBasicLine()
        {
            var basicRow = new TvcBasicRow(@"10 A=5 : if A<>5 then goto 5", true);

            Assert.IsTrue(basicRow.RowText.Equals(@"10A=5:ifA<>5thengoto5"));
        }

        [TestMethod]
        public void SpacesAreRemovedFromBasicLineButNotFromLiterals()
        {
            var basicRow = new TvcBasicRow("10 A=5 : if A=5 then print \"hello word of TVC\"", true);

            Assert.IsTrue(basicRow.RowText.Equals("10A=5:ifA=5thenprint\"hello word of TVC\""));
        }


        [TestMethod]
        public void TokeniserThrowsForEmptyRow()
        {
            Assert.ThrowsException<TvcBasicException>(() =>
            {
                TvcBasicRow dummy = new TvcBasicRow(string.Empty);
            });
        }

        [TestMethod]
        public void TokeniserThrowsForNull()
        {
            Assert.ThrowsException<TvcBasicException>(() =>
            {
                TvcBasicRow dummy = new TvcBasicRow(null);
            });
        }
    }
}
