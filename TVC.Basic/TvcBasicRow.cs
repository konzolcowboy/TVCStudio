using System;
using System.Collections.Generic;
using System.Linq;

namespace TVC.Basic
{
    public class TvcBasicRow
    {
        public TvcBasicRow(string basicRowText, bool removeSpaces = false)
        {
            RowText = removeSpaces ? GetBasicRowTextWithoutSpaces(basicRowText) : basicRowText;
            ResolveRowNumber();
            TokenizeRow();
        }

        public const byte TvcBasicRowTerminator = 0xFF;

        public ushort RowNumber { get; private set; }

        public string RowText { get; }

        public byte[] TokenizedBytes { get; private set; }

        private string GetBasicRowTextWithoutSpaces(string rowText)
        {
            string resultString = "";
            bool inLiteral = false;
            foreach (char  c in rowText)
            {
                if(c == '\"')
                {
                    inLiteral = !inLiteral;
                }

                // The spaces must not be removed from literals.
                if(c != ' ' || (c ==' ' &&  inLiteral))
                {
                    resultString += c;
                }
            }

            return resultString;
        }


        private void ResolveRowNumber()
        {
            if (string.IsNullOrEmpty(RowText))
            {
                throw new TvcBasicException(@" A basic sor üres sor!");
            }

            string numberString = "";
            for (int i = 0; i < RowText.Length; i++)
            {
                if (i == 0 && !char.IsNumber(RowText[i]))
                {
                    throw new TvcBasicException(@" A basic sornak számmal kell kezdődnie!");
                }

                if (!char.IsDigit(RowText[i]))
                {
                    break;
                }
                numberString += RowText[i];
            }

            RowNumber = ushort.Parse(numberString);
        }


        private int SkipRowNumberAndFirstSpaces()
        {
            int result = 0;

            foreach (char t in RowText)
            {
                if (char.IsLetter(t))
                {
                    break;
                }
                result++;
            }
            return result;
        }

        [Flags]
        private enum TokenizingState
        {
            CharMustTokenized = 0,
            CharInLiteral = 1,
            CharInDataOrCommentRow = 2
        }

        private void FindToken(int currentCharIndex, out int lengthOfFoundToken, out byte tokenByte)
        {
            bool endOfSearch = false;
            int lengthofSearch = 1;
            lengthOfFoundToken = -1;
            tokenByte = 0x00;

            Dictionary<string, byte> filteredTokenDic = TvcBasicTokenLibrary.BasicTokens;
            while (!endOfSearch && (currentCharIndex + lengthofSearch) <= RowText.Length)
            {
                string tokenStringForSearch = RowText.Substring(currentCharIndex, lengthofSearch++).ToUpper();
                var tokens = filteredTokenDic
                    .Where(kvp => kvp.Key.StartsWith(tokenStringForSearch, StringComparison.InvariantCultureIgnoreCase))
                    .Select(kvp => kvp);
                if (tokens.Count() == 1)
                {
                    KeyValuePair<string, byte> pair = tokens.First();
                    if (pair.Key.Equals(tokenStringForSearch))
                    {
                        endOfSearch = true;
                        lengthOfFoundToken = pair.Key.Length;
                        tokenByte = pair.Value;
                    }
                }
                else if (tokens.Count() > 1)
                {
                    KeyValuePair<string, byte> pair = tokens.FirstOrDefault(kvp => kvp.Key == tokenStringForSearch);
                    if (pair.Key != null)
                    {
                        lengthOfFoundToken = pair.Key.Length;
                        tokenByte = pair.Value;
                    }
                    filteredTokenDic = tokens.ToDictionary(kvp => kvp.Key, KeyValuePair => KeyValuePair.Value);
                }
                else
                {
                    endOfSearch = true;
                }
            }
        }

        private void TokenizeRow()
        {
            if (string.IsNullOrEmpty(RowText))
            {
                throw new TvcBasicException(@" A basic sor üres sor!");
            }

            byte highByte = (byte)(RowNumber >> 8);
            byte lowByte = (byte)(RowNumber & 0xFF);
            List<byte> tokBytes = new List<byte> { 0x00, lowByte, highByte };

            TokenizingState charState = TokenizingState.CharMustTokenized;


            for (int currentCharIndex = SkipRowNumberAndFirstSpaces(); currentCharIndex < RowText.Length;)
            {
                char currentChar = RowText[currentCharIndex];

                // The numbers and spaces must not be tokenised. They appear with their ASCII code in tokenised Tvc basic Row
                if (charState == TokenizingState.CharMustTokenized && currentChar != ' ' && !char.IsNumber(currentChar))
                {
                    FindToken(currentCharIndex, out int lengthOfFoundToken, out byte tokenByte);
                    if (lengthOfFoundToken > 0)
                    {
                        if (tokenByte == 0xFC ||
                            tokenByte == 0xFE ||
                            tokenByte == 0xFB)
                        {
                            // If the found token is 'DATA', '!', or 'REM', the following characters must not be tokenised
                            charState |= TokenizingState.CharInDataOrCommentRow;
                        }

                        tokBytes.Add(tokenByte);
                        currentCharIndex += lengthOfFoundToken;
                    }
                    else
                    {
                        // if the current character is not part of a basic token 
                        if (currentChar == '"')
                        {
                            charState ^= TokenizingState.CharInLiteral;
                        }
                        // In literal the character must not be converted into upper case
                        currentChar = charState.HasFlag(TokenizingState.CharInLiteral) ? currentChar : char.ToUpper(currentChar);
                        tokBytes.Add(currentChar.ToTvcBasicAscii());
                        currentCharIndex++;
                    }
                }
                else
                {
                    if (currentChar == '"')
                    {
                        charState ^= TokenizingState.CharInLiteral;
                    }
                    if (currentChar == ':' && !charState.HasFlag(TokenizingState.CharInLiteral))
                    {
                        tokBytes.Add(0xFD);
                        charState = TokenizingState.CharMustTokenized;
                        currentCharIndex++;
                    }
                    else
                    {
                        currentChar = charState.HasFlag(TokenizingState.CharInLiteral) ? currentChar : char.ToUpper(currentChar);
                        tokBytes.Add(currentChar.ToTvcBasicAscii());
                        currentCharIndex++;
                    }
                }
            }

            tokBytes.Add(0xFF);
            tokBytes[0] = (byte)tokBytes.Count;
            TokenizedBytes = tokBytes.ToArray();
        }
    }
}
