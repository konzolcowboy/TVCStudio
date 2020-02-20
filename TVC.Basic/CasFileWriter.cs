using System;
using System.IO;
namespace TVC.Basic
{
    public enum CasFileBufferingType : byte
    {
        Puffered = 0x01,
        NotPuffered = 0x11
    }

    public enum CasFileType : byte
    {
        DataFile = 0x00,
        ProgramFile = 0x01,
    }

    public enum CasFileContent : byte
    {
        BasicProgram = 0x01,
        NativProgramCode = 0x02, // assembled Z80 program without basic loader
        BinaryData = 0x03 // for instance screen, sprite, music ...
    }
    public class CasFileWriter
    {
        public CasFileWriter(string filePath, bool copyProtected = false,
            CasFileType type = CasFileType.ProgramFile, CasFileContent content = CasFileContent.BasicProgram, bool autoRun = false)
        {
            FilePath = filePath;
            CopyProtected = copyProtected;
            FileType = type;
            AutoRun = autoRun;
            BufferingType = CasFileBufferingType.NotPuffered;
            FileContent = content;
            m_UpmHeader = new byte[UpmHeaderLength];
            m_UpmHeader[0] = (byte)BufferingType;
            m_UpmHeader[1] = CopyProtected ? (byte)0x01 : (byte)0x00;
            m_NotPufferedProgramHeader = new byte[16];
            m_NotPufferedProgramHeader[1] = (byte)FileType;
            m_NotPufferedProgramHeader[4] = AutoRun ? (byte)0xFF : (byte)0x00;
            m_NotPufferedProgramHeader[15] = (byte)FileContent;

        }

        public void Write(byte[] programBytes)
        {
            CalculateLengthsAndBlockNumber(programBytes);
            
            try
            {
                FileStream fileStream = new FileStream(FilePath, FileMode.Create);
                using (BinaryWriter bw = new BinaryWriter(fileStream))
                {
                    bw.Write(m_UpmHeader);
                    bw.Write(m_NotPufferedProgramHeader);
                    bw.Write(programBytes);
                }
            }
            catch (Exception exception)
            {
                throw new TvcBasicException($"A {FilePath} file létrehozása sikertelen. A hiba oka:'{exception.Message}'");
            }
        }

        private void CalculateLengthsAndBlockNumber(byte[] programBytes)
        {
            ushort fileLengthInBytes = (ushort) (programBytes.Length + m_NotPufferedProgramHeader.Length + UpmHeaderLength);
            ushort blockNumber = (ushort) (fileLengthInBytes / 128);
            m_UpmHeader[2] = (byte) (blockNumber & 0xFF); // low byte of blocknumber
            m_UpmHeader[3] = (byte) (blockNumber >> 8); // high byte of blocknumber

            byte numberOfUsableBytesInLast128BytesProgramBlock = (byte) (fileLengthInBytes - blockNumber * 128);
            m_UpmHeader[4] = numberOfUsableBytesInLast128BytesProgramBlock;

            ushort programLength = (ushort) programBytes.Length;
            m_NotPufferedProgramHeader[2] = (byte) (programLength & 0xFF);
            m_NotPufferedProgramHeader[3] = (byte) (programLength >> 8);
        }

        public string FilePath { get; }
        public bool CopyProtected { get; }
        public CasFileType FileType { get; }
        public CasFileBufferingType BufferingType { get; }
        public CasFileContent FileContent { get; }
        public bool AutoRun { get; }
        private readonly byte[] m_UpmHeader;
        private readonly byte[] m_NotPufferedProgramHeader;
        private const int UpmHeaderLength = 128;
    }
}
