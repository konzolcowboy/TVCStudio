using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Z80.Kernel.Z80Assembler
{
    public static class Helper
    {
        private static ushort HexToUshort(this string hexaString)
        {
            string hexaNumber = hexaString.Replace("$", "");

            try
            {
                ushort result = ushort.Parse(hexaNumber, NumberStyles.HexNumber);
                return result;
            }
            catch (OverflowException)
            {
                throw new Z80AssemblerException(
                    $"Helytelen hexadecimális szám '{hexaNumber}'! A megengedett értéktartomány: 0000-FFFF");
            }
            catch (FormatException)
            {
                throw new Z80AssemblerException(
                    $"A hexadecimális szám '{hexaNumber}' helytelen karaktert tartalmaz!");
            }
        }

        private static byte HexToByte(this string hexaString)
        {
            string hexaNumber = hexaString.Replace("$", "");

            try
            {
                byte result = byte.Parse(hexaNumber, NumberStyles.HexNumber);
                return result;
            }
            catch (OverflowException)
            {
                throw new Z80AssemblerException(
                    $"Helytelen hexadecimális szám '{hexaNumber}'! A megengedett értéktartomány: 00-FF");
            }
            catch (FormatException)
            {
                throw new Z80AssemblerException(
                    $"A hexadecimális szám '{hexaNumber}' helytelen karaktert tartalmaz!");
            }
        }

        public static bool IsHexa(this char c)
        {
            return char.IsDigit(c) ||
                   c >= 'a' && c <= 'f' ||
                   c >= 'A' && c <= 'F';

        }

        public static byte ResolveByteConstant(this string numberString)
        {
            if (numberString.StartsWith("$"))
            {
                return numberString.HexToByte();
            }

            byte result;
            bool parsed = byte.TryParse(numberString, out result);
            if (!parsed)
            {
                throw new Z80AssemblerException(
                    $"Helytelen decimális konstans:{numberString}!");
            }
            return result;
        }

        public static ushort ResolveUshortConstant(this string numberString)
        {
            if (numberString.StartsWith("$"))
            {
                return numberString.HexToUshort();
            }

            bool parsed = ushort.TryParse(numberString, out var result);
            if (!parsed)
            {
                throw new Z80AssemblerException(
                    $"Helytelen decimális konstans:{numberString}!");
            }
            return result;
        }

        public static string UshortToHexa(this ushort number)
        {
            return $"${number:x4}";
        }

        public static string ByteToHexa(this byte number)
        {
            return $"${number:x2}";
        }

        public static bool EnclosedWithBracket(this string s)
        {
            return s.StartsWith("(") && s.EndsWith(")");
        }
        public static List<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static byte ToTvcAscii(this char c)
        {
            if (HungarianTvcAsciiChars.ContainsKey(c))
            {
                return HungarianTvcAsciiChars[c];
            }

            byte convertedByte = Encoding.ASCII.GetBytes(new[] { c })[0];
            if (convertedByte == 0x3F && c != '?')
            {
                throw new Z80AssemblerException($"A '{c}' karakter nem ASCII karakter!");
            }
            return convertedByte;
        }

        public static bool IsTvcAscii(this char c)
        {
            if (HungarianTvcAsciiChars.ContainsKey(c))
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

        public static byte[] ToTvcAsciiBytes(this string s)
        {
            List<byte> result = new List<byte>();
            char[] characters = s.ToCharArray();

            if (s.StartsWith("'") && !s.EndsWith("'") ||
                s.StartsWith("\"") && !s.EndsWith("\""))
            {
                throw new Z80AssemblerException($"Szintaxis hiba: a karakterlánc nincs lezárva:{s}!");
            }

            for (int i = 0; i < characters.Length; i++)
            {
                if (i == 0 && characters[i] == '\'' || characters[i] == '\"' ||
                    i == characters.Length - 1 && characters[i] == '\'' || characters[i] == '\"')
                {
                    continue;
                }
                result.Add(characters[i].ToTvcAscii());
            }

            return result.ToArray();
        }

        private static Dictionary<char, byte> HungarianTvcAsciiChars { get; } = new Dictionary<char, byte>()
        {
            {'Á',0x80 },
            {'á',0x90 },
            {'É',0x81 },
            {'é',0x91 },
            {'Í',0x82 },
            {'í',0x92 },
            {'Ó',0x83 },
            {'ó',0x93 },
            {'Ö',0x84 },
            {'ö',0x94 },
            {'Ő',0x85 },
            {'ő',0x95 },
            {'Ú',0x86 },
            {'ú',0x96 },
            {'Ü',0x87 },
            {'ü',0x97 },
            {'Ű',0x88 },
            {'ű',0x98 },
        };
    }
}

