﻿using System.IO;

namespace TVCTape
{
    // Sine table

    internal static class DirectDigitalSyntheser
    {
        private static readonly byte[] SynTable =
        {
            0x80, 0x83, 0x86, 0x89, 0x8C, 0x8F, 0x92, 0x95,
            0x97, 0x9A, 0x9D, 0xA0, 0xA3, 0xA6, 0xA8, 0xAB,
            0xAE, 0xB1, 0xB3, 0xB6, 0xB9, 0xBB, 0xBE, 0xC0,
            0xC3, 0xC5, 0xC7, 0xCA, 0xCC, 0xCE, 0xD1, 0xD3,
            0xD5, 0xD7, 0xD9, 0xDB, 0xDD, 0xDF, 0xE0, 0xE2,
            0xE4, 0xE5, 0xE7, 0xE8, 0xEA, 0xEB, 0xEC, 0xEE,
            0xEF, 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF4, 0xF5,
            0xF6, 0xF6, 0xF7, 0xF7, 0xF7, 0xF8, 0xF8, 0xF8,
            0xF8, 0xF8, 0xF8, 0xF8, 0xF7, 0xF7, 0xF7, 0xF6,
            0xF6, 0xF5, 0xF4, 0xF4, 0xF3, 0xF2, 0xF1, 0xF0,
            0xEF, 0xEE, 0xEC, 0xEB, 0xEA, 0xE8, 0xE7, 0xE5,
            0xE4, 0xE2, 0xE0, 0xDF, 0xDD, 0xDB, 0xD9, 0xD7,
            0xD5, 0xD3, 0xD1, 0xCE, 0xCC, 0xCA, 0xC7, 0xC5,
            0xC3, 0xC0, 0xBE, 0xBB, 0xB9, 0xB6, 0xB3, 0xB1,
            0xAE, 0xAB, 0xA8, 0xA6, 0xA3, 0xA0, 0x9D, 0x9A,
            0x97, 0x95, 0x92, 0x8F, 0x8C, 0x89, 0x86, 0x83,
            0x80, 0x7D, 0x7A, 0x77, 0x74, 0x71, 0x6E, 0x6B,
            0x69, 0x66, 0x63, 0x60, 0x5D, 0x5A, 0x58, 0x55,
            0x52, 0x4F, 0x4D, 0x4A, 0x47, 0x45, 0x42, 0x40,
            0x3D, 0x3B, 0x39, 0x36, 0x34, 0x32, 0x2F, 0x2D,
            0x2B, 0x29, 0x27, 0x25, 0x23, 0x21, 0x20, 0x1E,
            0x1C, 0x1B, 0x19, 0x18, 0x16, 0x15, 0x14, 0x12,
            0x11, 0x10, 0x0F, 0x0E, 0x0D, 0x0C, 0x0C, 0x0B,
            0x0A, 0x0A, 0x09, 0x09, 0x09, 0x08, 0x08, 0x08,
            0x08, 0x08, 0x08, 0x08, 0x09, 0x09, 0x09, 0x0A,
            0x0A, 0x0B, 0x0C, 0x0C, 0x0D, 0x0E, 0x0F, 0x10,
            0x11, 0x12, 0x14, 0x15, 0x16, 0x18, 0x19, 0x1B,
            0x1C, 0x1E, 0x20, 0x21, 0x23, 0x25, 0x27, 0x29,
            0x2B, 0x2D, 0x2F, 0x32, 0x34, 0x36, 0x39, 0x3B,
            0x3D, 0x40, 0x42, 0x45, 0x47, 0x4A, 0x4D, 0x4F,
            0x52, 0x55, 0x58, 0x5A, 0x5D, 0x60, 0x63, 0x66,
            0x69, 0x6B, 0x6E, 0x71, 0x74, 0x77, 0x7A, 0x7D
        };

        private const uint SampleRate = 44100;
        private const byte ZeroValueByteSample = 0x80;
        private static uint _ddsAccumulator;

        internal static void GenerateSilence(uint lengthInMs, BinaryWriter output)
        {
            uint sampleCount = lengthInMs * SampleRate / 1000;

            while (sampleCount > 0)
            {
                output.Write(ZeroValueByteSample);
                sampleCount--;
            }

            _ddsAccumulator = 0;
        }

        internal static void GenerateSignal(uint frequency, uint cycleCount, BinaryWriter output)
        {
            uint ddsIncrement = (uint) (((frequency * SynTable.Length * 256) / SampleRate) << 16);

            while (cycleCount > 0)
            {
                byte sample = SynTable[(byte)(_ddsAccumulator >> 24)];

                var prevAccumulator = _ddsAccumulator;
                _ddsAccumulator += ddsIncrement;

                if (_ddsAccumulator < prevAccumulator)
                {
                    cycleCount--;
                }

                output.Write(sample);
            }
        }
    }
}
