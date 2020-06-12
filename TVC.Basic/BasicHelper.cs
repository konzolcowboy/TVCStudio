using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Z80.Kernel.Z80Assembler;

namespace TVC.Basic
{
    public static class BasicHelper
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

        /// <summary>
        /// If the BASIC program contains row without row number, the method returns with true 
        /// </summary>
        /// <returns>true if the BASIC program contains row without row number</returns>

        public static bool BasicCodeIsSimplified(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string currentRowNumberString = DecodeBasicRowNumber(line);
                    if (string.IsNullOrEmpty(currentRowNumberString))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string DecodeBasicRowNumber(string inputline)
        {
            if (string.IsNullOrEmpty(inputline))
            {
                return string.Empty;
            }

            string numberString = "";
            for (int i = 0; i < inputline.Length; i++)
            {
                if (i == 0 && !char.IsDigit(inputline[i]))
                {
                    return string.Empty;
                }

                if (!char.IsDigit(inputline[i]))
                {
                    break;
                }

                numberString += inputline[i];
            }

            return numberString;
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

        public static string RenumberBasicProgram(IEnumerable<string> rows, int startrowNumber, int rowNumberIncrement)
        {
            var result = new List<string>(rows);

            int newRowNumber = startrowNumber;

            for (int i = 0; i < result.Count; i++)
            {
                string newRowNumberString = newRowNumber.ToString();
                string currentRowNumberString = DecodeBasicRowNumber(result[i]);
                if (!string.IsNullOrEmpty(currentRowNumberString))
                {
                    for (int j = 0; j < result.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(result[j]))
                        {
                            result[j] = ReplaceRowNumberInJumpInstructions(result[j], currentRowNumberString, newRowNumberString);
                        }
                    }

                    int removeIndex = result[i].IndexOf(currentRowNumberString);
                    result[i] = result[i].Remove(removeIndex, currentRowNumberString.Length);
                    result[i] = result[i].Insert(removeIndex, newRowNumberString);
                    newRowNumber += rowNumberIncrement;
                }
                else
                {
                    string rowSymbol = DecodeBasicRowSymbol(result[i]);
                    if(!string.IsNullOrEmpty(rowSymbol))
                    {
                        for (int j = 0; j < result.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(result[j]))
                            {
                                result[j] = ReplaceSymbolInBasicProgram(result[j], rowSymbol, newRowNumberString);
                            }
                        }
                    }
                    else
                    {
                        result[i] = result[i].Insert(0, newRowNumberString);
                    }
                    
                    newRowNumber += rowNumberIncrement;
                }
            }

            var resultString = new StringBuilder();
            result.ForEach(s => resultString.AppendLine(s));
            return resultString.ToString();
        }

        private static string ReplaceSymbolInBasicProgram(string sourceString, string symbolString, string replaceFor)
        {
            var regexForSymbol = new Regex(symbolString, RegexOptions.IgnoreCase);
            return regexForSymbol.Replace(sourceString, replaceFor);

        }

        // The basic row symbol must start and end with @ character
        private static string DecodeBasicRowSymbol(string inputline)
        {
            if (string.IsNullOrEmpty(inputline))
            {
                return string.Empty;
            }

            string symbolString = "";
            bool inSymbol = false;
            for (int i = 0; i < inputline.Length; i++)
            {
                if (i == 0 && inputline[i] == '@')
                {
                    symbolString += '@';
                    inSymbol = !inSymbol;
                    continue;
                }

                if (inputline[i] == '@' && inSymbol)
                {
                    symbolString += '@';
                    break;
                }
                
                if(inSymbol)
                {
                    symbolString += inputline[i];
                }
            }

            return symbolString;
        }

        private static string ReplaceRowNumberInJumpInstructions(string sourceString, string rowNumberString, string replaceFor)
        {
            StringBuilder resultString = new StringBuilder(sourceString);
            string regexPatternForInstructionsWithOneRowNumber = $"THEN[ ]{{0,}}(?<rownumber>{rowNumberString})|ELSE[ ]{{0,}}(?<rownumber>{rowNumberString})|CONTINUE[ ]{{0,}}(?<rownumber>{rowNumberString})|RUN[ ]{{0,}}(?<rownumber>{rowNumberString})";
            string gotoAndGosubpattern = @"(?<match>(?<token>(goto|gosub)[ ]{0,})(?<rownumber>([ ]{0,}[0-9]{1,4})|[ ]{0,}[,]{1}[ ]{0,}(\b[0-9]{1,4}))+)";

            var regexForInstructionsWithOneRowNumber = new Regex(regexPatternForInstructionsWithOneRowNumber, RegexOptions.IgnoreCase);

            foreach (Match match in regexForInstructionsWithOneRowNumber.Matches(resultString.ToString()))
            {
                resultString.Replace(match.Groups["rownumber"].Value, replaceFor, match.Groups["rownumber"].Index, match.Groups["rownumber"].Length);
            }

            var regexForGotoAndGosub = new Regex(gotoAndGosubpattern, RegexOptions.IgnoreCase);

            int offset = 0;
            foreach (Match m in regexForGotoAndGosub.Matches(resultString.ToString()))
            {
                foreach (Capture capture in m.Groups["rownumber"].Captures)
                {
                    // decoding the whole rownumber from the capture value
                    // to avoid the substring issue. For example the rownumberstring is 5
                    // and the capture contains 35. In this case the 5 is replaced with the new value

                    var numberRegex = new Regex(@"[0-9]{1,}");
                    var rownumberMatch = numberRegex.Match(capture.Value);
                    if (rownumberMatch.Success && rownumberMatch.Value.Equals(rowNumberString))
                    {
                        int offSetOfRownumber = capture.Value.IndexOf(rowNumberString, System.StringComparison.OrdinalIgnoreCase);
                        if (offSetOfRownumber > -1)
                        {
                            offSetOfRownumber += capture.Index;
                            resultString.Replace(rowNumberString, replaceFor, offset + offSetOfRownumber, rowNumberString.Length);
                            offset += replaceFor.Length - rowNumberString.Length;
                        }
                    }
                }
            }

            return resultString.ToString();
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
