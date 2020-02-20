using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Z80.Kernel.Z80Assembler
{
    public enum OperandType
    {
        Unknown,
        Byte,
        ByteAddress,
        Word,
        WordAddress,
        Character,
        Literal,
        Decimal,
        DecimalAddress,
        Register,
        JumpCondition,
        ByteWithIndexRegister,
        DecimalWithIndexRegister,
        ExpressionWithIxIndexRegister,
        ExpressionWithIyIndexRegister,
        Expression
    }

    public enum OperandState
    {
        Init,
        Valid,
        FutureSymbol
    }

    public class Operand : ICloneable
    {
        public Operand(string operandString)
        {
            m_OperandString = operandString;
            Info = new OperandInfo(operandString);
            State = OperandState.Init;
        }
        public OperandInfo Info { get; private set; }
        public OperandState State { get; set; }

        public string Value
        {
            get => m_OperandString;
            set
            {
                if (value != m_OperandString)
                {
                    m_OperandString = value;
                    Info = new OperandInfo(m_OperandString);
                }
            }
        }

        public override string ToString()
        {
            return m_OperandString;
        }

        public object Clone()
        {
            return new Operand(m_OperandString);
        }

        private string m_OperandString;
    }

    public class OperandInfo
    {
        public OperandInfo(string operand)
        {
            DataType = GetType(operand);
        }

        public string GetRegexStringForOperandType()
        {
            foreach (KeyValuePair<string, OperandType> valuePair in PossibleOperandRegexes)
            {
                if (valuePair.Value == DataType)
                {
                    return valuePair.Key;
                }
            }

            throw new Z80AssemblerException(@"Nem támogatott operandus típus");
        }

        public OperandType DataType { get; }

        private OperandType GetType(string operand)
        {
            if (OperandIsRegister(operand))
            {
                return OperandType.Register;
            }
            if (OperandIsJumpCondition(operand))
            {
                return OperandType.JumpCondition;
            }

            foreach (KeyValuePair<string, OperandType> keyValuePair in PossibleOperandRegexes)
            {
                var regex = new Regex(keyValuePair.Key, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regex.Matches(operand);
                if (matches.Count > 0)
                {
                    return keyValuePair.Value;
                }
            }

            return OperandType.Unknown;
        }

        private bool OperandIsRegister(string operand)
        {
            string tempS = operand.Replace("(", "").Replace(")","");
            return Registers.Any(s => s == tempS) || ProcessorRegisterPairs.Any(s => s == tempS);
        }
        private static string[] Registers { get; } =
        {
            "A", "F", "B", "C", "D", "E", "H", "L",
            "A'", "F'", "B'", "C'", "D'", "E'", "H'", "L'",
            "IX", "IY", "SP", "I", "R", "PC","IXH","IXL","IYH","IYL"
        };

        private static string[] ProcessorRegisterPairs { get; } =
        {
            "AF","AF'", "BC", "DE", "HL"
        };

        private bool OperandIsJumpCondition(string operand)
        {
            return JumpConditions.Any(s => s == operand);
        }

        private static string[] JumpConditions { get; } =
        {
            "C","NC","Z","NZ","P","M","P0","PO","PE"
        };

        private static IReadOnlyDictionary<string, OperandType> PossibleOperandRegexes { get; } =
            new Dictionary<string, OperandType>
            {
                {@"^(\${1}[0-9A-F]{1,2})$",OperandType.Byte },
                {@"^\((\${1}[0-9A-F]{1,2})\)$",OperandType.ByteAddress },
                {@"^(\${1}[0-9A-F]{3,4})$",OperandType.Word },
                {@"^\((\${1}[0-9A-F]{3,4})\)$",OperandType.WordAddress },
                {@"^\(IX\+(\${1}[0-9A-F]{1,2})\)$",OperandType.ByteWithIndexRegister},
                {@"^\(IY\+(\${1}[0-9A-F]{1,2})\)$",OperandType.ByteWithIndexRegister},
                {@"^\(IX\+([0-9]{1,3})\)$",OperandType.DecimalWithIndexRegister},
                {@"^\(IY\+([0-9]{1,3})\)$",OperandType.DecimalWithIndexRegister},
                {@"^(\-{0,1}[0-9]{1,5})$",OperandType.Decimal },
                {@"^\((\-{0,1}[0-9]{1,5})\)$",OperandType.DecimalAddress },
                {"^\"([ -~áÁíÍéÉóÓöÖőŐúÚüÜűŰ]{1})\"$",OperandType.Character },
                {@"^\'([ -~áÁíÍéÉóÓöÖőŐúÚüÜűŰ]{1})\'$",OperandType.Character },
                {"^\"([ -~áÁíÍéÉóÓöÖőŐúÚüÜűŰ]{2,})\"$",OperandType.Literal },
                {@"^\'([ -~áÁíÍéÉóÓöÖőŐúÚüÜűŰ]{2,})\'$",OperandType.Literal },
                { @"^\(IX\+\((.{1,})\)\)$",OperandType.ExpressionWithIxIndexRegister},
                { @"^\(IY\+\((.{1,})\)\)$",OperandType.ExpressionWithIyIndexRegister},
                { @"[-+\/*<>&|~^]|sqrt\({1}|abs\({1}|high\({1}|low\({1}",OperandType.Expression}
            };
    }
}
