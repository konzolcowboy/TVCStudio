using System;
using System.Collections.Generic;
using System.IO;

namespace TVCTape
{
    internal struct RiffHeader
    {
        public uint ChunkId;       // 'RIFF'
        public uint ChunkSize;
        public uint Format;            // 'WAVE'
        public static uint SizeInBytes => 12;

        public RiffHeader(uint chunksize)
        {
            ChunkId = 0x46464952; // 'RIFF'
            Format = 0x45564157; // 'WAVE'
            ChunkSize = chunksize;
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(ChunkId));
            result.AddRange(BitConverter.GetBytes(ChunkSize));
            result.AddRange(BitConverter.GetBytes(Format));

            return result.ToArray();
        }
    }

    internal struct ChunkHeader
    {
        public uint ChunkId;
        public uint ChunkSize;
        public static uint SizeInBytes => 8;

        public ChunkHeader(uint chunkid)
        {
            ChunkId = chunkid;
            ChunkSize = 0;
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(ChunkId));
            result.AddRange(BitConverter.GetBytes(ChunkSize));

            return result.ToArray();
        }
    }

    internal struct FormatChunk
    {

        public ushort AudioFormat;
        public ushort NumChannels;
        public uint SampleRate;
        public uint ByteRate;
        public ushort BlockAlign;
        public ushort BitsPerSample;

        public static uint SizeInBytes => 16;

        public FormatChunk(ushort bitspersample)
        {
            AudioFormat = 1; // PCM
            NumChannels = 1; // MONO
            SampleRate = 44100;
            BitsPerSample = bitspersample;
            BlockAlign = 1;
            ByteRate = SampleRate * NumChannels * BitsPerSample / 8;
        }
        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(AudioFormat));
            result.AddRange(BitConverter.GetBytes(NumChannels));
            result.AddRange(BitConverter.GetBytes(SampleRate));
            result.AddRange(BitConverter.GetBytes(ByteRate));
            result.AddRange(BitConverter.GetBytes(BlockAlign));
            result.AddRange(BitConverter.GetBytes(BitsPerSample));

            return result.ToArray();
        }
    }

    internal class WavFileCreator
    {
        public WavFileCreator(string filePath, byte[] sampleBytes, ushort bitspersample = 8)
        {
            m_FilePath = filePath;
            m_FormatChunk = new FormatChunk(bitspersample);
            m_SampleBytes = sampleBytes;

            uint chunkSize = (uint)(ChunkHeader.SizeInBytes + FormatChunk.SizeInBytes + ChunkHeader.SizeInBytes +
                                     m_SampleBytes.Length + 4);
            m_RiffHeader = new RiffHeader(chunkSize);
            m_ChunkHeader = new ChunkHeader(0x20746d66); // 'fmt '
        }

        public void Save()
        {
            using (FileStream fs = File.Create(m_FilePath))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(m_RiffHeader.ToByteArray());
                    m_ChunkHeader.ChunkSize = FormatChunk.SizeInBytes;
                    bw.Write(m_ChunkHeader.ToByteArray());
                    bw.Write(m_FormatChunk.ToByteArray());
                    m_ChunkHeader.ChunkId = 0x61746164; // 'data'
                    m_ChunkHeader.ChunkSize = (uint)(m_SampleBytes.Length * m_FormatChunk.BitsPerSample / 8);
                    bw.Write(m_ChunkHeader.ToByteArray());
                    bw.Write(m_SampleBytes);
                    bw.Flush();
                }
            }
        }

        private readonly string m_FilePath;
        private FormatChunk m_FormatChunk;
        private RiffHeader m_RiffHeader;
        private ChunkHeader m_ChunkHeader;
        private readonly byte[] m_SampleBytes;
    }
}
