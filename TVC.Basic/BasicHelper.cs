using System.Collections.Generic;
using System.Text;

namespace TVC.Basic
{
    internal static class BasicHelper
    {
        public static byte ToTvcBasicAscii(this char c)
        {
            if (HungarianTvcBasicAsciiChars.ContainsKey(c))
            {
                return HungarianTvcBasicAsciiChars[c];
            }

            byte convertedByte = Encoding.ASCII.GetBytes(new[] { c })[0];
            if (convertedByte == 0x3F && c != '?')
            {
                throw new TvcBasicException($"A '{c}' karakter nem ASCII karakter!");
            }
            return convertedByte;
        }

        public static bool IsTvcBasicAscii(this char c)
        {
            if (HungarianTvcBasicAsciiChars.ContainsKey(c))
            {
                return true;
            }

            byte convertedByte = Encoding.ASCII.GetBytes(new[] { c })[0];

            // Encoding.ASCII.GetBytes returns with '0x3F' if the input character is greather than 7F
            if (convertedByte == 0x3F && c != '?')
            {
                return false;
            }

            return true;
        }

        public static byte[] ToTvcBasicAsciiBytes(this string s)
        {
            List<byte> result = new List<byte>();
            char[] characters = s.ToCharArray();

            if (s.StartsWith("'") && !s.EndsWith("'") ||
                s.StartsWith("\"") && !s.EndsWith("\""))
            {
                throw new TvcBasicException($"Szintaxis hiba: a karakterlánc nincs lezárva:{s}!");
            }

            for (int i = 0; i < characters.Length; i++)
            {
                if (i == 0 && characters[i] == '\'' || characters[i] == '\"' ||
                    i == characters.Length - 1 && characters[i] == '\'' || characters[i] == '\"')
                {
                    continue;
                }
                result.Add(characters[i].ToTvcBasicAscii());
            }

            return result.ToArray();
        }

        private static Dictionary<char, byte> HungarianTvcBasicAsciiChars { get; } = new Dictionary<char, byte>()
        {
            {'Á',0x00 },
            {'á',0x10 },
            {'É',0x01 },
            {'é',0x11 },
            {'Í',0x02 },
            {'í',0x12 },
            {'Ó',0x03 },
            {'ó',0x13 },
            {'Ö',0x04 },
            {'ö',0x14 },
            {'Ő',0x05 },
            {'ő',0x15 },
            {'Ú',0x06 },
            {'ú',0x16 },
            {'Ü',0x07 },
            {'ü',0x17 },
            {'Ű',0x08 },
            {'ű',0x18 }
        };
    }
}
