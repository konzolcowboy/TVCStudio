using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TVCTape
{
    internal struct TapeBlockHeader
    {
        public byte Zero;                       // always zero byte
        public byte Magic;                  // always 0x6a
        public byte BlockType;          // Block type: Header=0xff, data=0x00
        public byte FileType;               // File type: Buffered: 0x01, non-buffered: 0x11
        public byte CopyProtect;        // Copy protection: not protected=0x00
        public byte SectorsInBlock;    // Number of sectors in this block

        public byte[] ToByteArray(bool withZero = true)
        {
            if (withZero)
            {
                return new[] { Zero, Magic, BlockType, FileType, CopyProtect, SectorsInBlock };
            }

            return new[] { Magic, BlockType, FileType, CopyProtect, SectorsInBlock };
        }

        public TapeBlockHeader(bool copyProtect)
        {
            Zero = 0;
            Magic = 0x6a;
            BlockType = 0x00;
            FileType = 0x11;
            CopyProtect = (byte)(copyProtect ? 0xff : 0x00);
            SectorsInBlock = 0;
        }
    }

    // Tape Sector header
    internal struct TapeSectorHeader
    {
        public byte SectorNumber;
        public byte BytesInSector;      // Sector length in bytes (0=256bytes)

        public byte[] ToByteArray()
        {
            return new[] { SectorNumber, BytesInSector };
        }
    }

    // Tape Sector end
    internal struct TapeSectorEnd
    {
        public byte EofFlag;
        public ushort Crc;

        public byte[] ToByteArray()
        {
            return new[] { EofFlag, (byte)(Crc & 0xff), (byte)(Crc >> 8) };
        }
    }

    internal struct CasProgramFileHeader
    {

        public byte Zero;                           // Zero
        public byte FileType;                   // Program type: 0x01 - ASCII, 0x00 - binary
        public ushort FileLength;            // Length of the file
        public byte Autorun;                    // Autostart: 0xff, no autostart: 0x00
        public byte[] Zeros;              // Zero
        public byte Version;					// Version

        public CasProgramFileHeader(bool autorun)
        {
            Zero = 0x00;
            FileType = 0x01;
            Autorun = (byte)(autorun ? 0xff : 0x00);
            Zeros = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Version = 0x00;
            FileLength = 0x00;
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>
            {
                Zero,
                FileType,
                (byte) (FileLength & 0xFF),
                (byte) (FileLength >> 8),
                Autorun
            };

            result.AddRange(Zeros);
            result.Add(Version);

            return result.ToArray();
        }
    }

    public class TvcTape
    {
        public TvcTape(bool copyProtect, bool autoRun, string filePath, 
            uint gapLeading, uint frequencyOffset, uint leadingLength)
        {
            m_CopyProtect = copyProtect;
            m_CrcGenerator = new TvcCrcGenerator();
            m_MemoryStream = new MemoryStream();
            m_OutputWaves = new BinaryWriter(m_MemoryStream);
            m_FilePath = filePath;
            m_FileName = Path.GetFileNameWithoutExtension(filePath);
            m_GapLeading = gapLeading;
            m_LeadingLength = leadingLength;
            m_FrequencyOffset = frequencyOffset;
            if (string.IsNullOrEmpty(m_FileName))
            {
                m_FileName = "PROGRAMOM";
            }
            else if (m_FileName.Length > 10)
            {
                m_FileName = m_FileName.Substring(0, 10);
            }

            m_CasHeader = new CasProgramFileHeader(autoRun);
        }

        public void GenerateTvcAudioFile(byte[] programbytes)
        {
            GenerateHeaderBlock(programbytes.Length);
            GenerateDataBlock(programbytes);

            m_MemoryStream.Position = 0;
            WavFileCreator wavCreator = new WavFileCreator(m_FilePath, m_MemoryStream.ToArray());

            wavCreator.Save();
        }

        private uint GenerateFrequenyFromOffset(uint frequency)
        {
            if (m_FrequencyOffset == 0)
            {
                return frequency;
            }

            return frequency * (100 + m_FrequencyOffset) / 100;
        }

        private void GenerateHeaderBlock(int programlength)
        {
            // block leading
            EncodeBlockLeading();

            TapeBlockHeader blockHeader = new TapeBlockHeader(m_CopyProtect)
            {
                BlockType = 0xff, // Header block
                SectorsInBlock = 1
            };

            m_CrcGenerator.AddBlock(blockHeader.ToByteArray(false));
            EncodeBlock(blockHeader.ToByteArray());

            TapeSectorHeader sectorHeader = new TapeSectorHeader
            {
                SectorNumber = 0,
                BytesInSector = (byte)(m_FileName.Length + m_CasHeader.ToByteArray().Length + 1)
            };

            m_CrcGenerator.AddBlock(sectorHeader.ToByteArray());
            EncodeBlock(sectorHeader.ToByteArray());

            m_CrcGenerator.AddByte((byte)m_FileName.Length);
            EncodeByte((byte)m_FileName.Length);

            byte[] asciiFileName = Encoding.ASCII.GetBytes(m_FileName);
            m_CrcGenerator.AddBlock(asciiFileName);
            EncodeBlock(asciiFileName);

            m_CasHeader.FileLength = (ushort)programlength;
            m_CrcGenerator.AddBlock(m_CasHeader.ToByteArray());
            EncodeBlock(m_CasHeader.ToByteArray());

            TapeSectorEnd sectorEnd = new TapeSectorEnd
            {
                EofFlag = 0x00,
            };

            m_CrcGenerator.AddByte(sectorEnd.EofFlag);
            sectorEnd.Crc = m_CrcGenerator.GeneratedCrcValue;
            EncodeBlock(sectorEnd.ToByteArray());
            DirectDigitalSyntheser.GenerateSignal(GenerateFrequenyFromOffset(FreqLeading), 5, m_OutputWaves);
        }

        private void GenerateDataBlock(byte[] programbytes)
        {
            byte sectorCount = (byte)((programbytes.Length + 255) / 256);
            byte sectorIndex = 1;
            m_CrcGenerator.GeneratedCrcValue = 0;

            EncodeBlockLeading();

            TapeBlockHeader blockHeader =
                new TapeBlockHeader(m_CopyProtect) { SectorsInBlock = (byte)((programbytes.Length + 255) / 256) };

            m_CrcGenerator.AddBlock(blockHeader.ToByteArray(false));
            EncodeBlock(blockHeader.ToByteArray());

            while (sectorIndex <= sectorCount)
            {
                int sectorSize = programbytes.Length - 256 * (sectorIndex - 1);
                if (sectorSize > 255)
                {
                    sectorSize = 256;
                }

                TapeSectorHeader sectorHeader = new TapeSectorHeader
                {
                    SectorNumber = sectorIndex,
                    BytesInSector = (byte)(sectorSize > 255 ? 0 : (byte)sectorSize)
                };

                m_CrcGenerator.AddBlock(sectorHeader.ToByteArray());
                EncodeBlock(sectorHeader.ToByteArray());
                byte[] sectorData = new byte[sectorSize];
                int sourceIndex = (sectorIndex - 1) * 256;
                Array.Copy(programbytes, sourceIndex, sectorData, 0, sectorSize);

                m_CrcGenerator.AddBlock(sectorData);
                EncodeBlock(sectorData);

                TapeSectorEnd sectorEnd =
                    new TapeSectorEnd { EofFlag = (byte)(sectorIndex == sectorCount ? 0xff : 0x00) };

                m_CrcGenerator.AddByte(sectorEnd.EofFlag);
                sectorEnd.Crc = m_CrcGenerator.GeneratedCrcValue;
                EncodeBlock(sectorEnd.ToByteArray());
                m_CrcGenerator.GeneratedCrcValue = 0;
                sectorIndex++;
            }

            DirectDigitalSyntheser.GenerateSignal(GenerateFrequenyFromOffset(FreqLeading), 5, m_OutputWaves);
            DirectDigitalSyntheser.GenerateSilence(50, m_OutputWaves);
        }

        #region Constans

        private const uint FreqSync = 1359;
        private const uint FreqLeading = 2128;
        private const uint ZeroFreqValue = 1812;
        private const uint OneFreqValue = 2577;
        private readonly uint m_LeadingLength;
        private readonly uint m_GapLeading;
        private readonly uint m_FrequencyOffset;

        #endregion

        private readonly TvcCrcGenerator m_CrcGenerator;
        private readonly BinaryWriter m_OutputWaves;
        private readonly bool m_CopyProtect;
        private CasProgramFileHeader m_CasHeader;
        private readonly string m_FileName;
        private readonly string m_FilePath;
        private readonly MemoryStream m_MemoryStream;

        private void EncodeBlockLeading()
        {
            ushort periodCount = (ushort)((GenerateFrequenyFromOffset(FreqLeading) * m_LeadingLength + 500) / 1000);

            DirectDigitalSyntheser.GenerateSilence(m_GapLeading, m_OutputWaves);
            DirectDigitalSyntheser.GenerateSignal(GenerateFrequenyFromOffset(FreqLeading), periodCount, m_OutputWaves);
            DirectDigitalSyntheser.GenerateSignal(GenerateFrequenyFromOffset(FreqSync), 1, m_OutputWaves);
        }

        private void EncodeByte(byte data)
        {
            for (int i = 0; i < 8; i++)
            {
                DirectDigitalSyntheser.GenerateSignal(
                    (data & 0x01) == 0
                        ? GenerateFrequenyFromOffset(ZeroFreqValue)
                        : GenerateFrequenyFromOffset(OneFreqValue), 1, m_OutputWaves);

                data >>= 1;
            }
        }
        private void EncodeBlock(byte[] buffer)
        {
            foreach (byte b in buffer)
            {
                EncodeByte(b);
            }
        }

    }
}
