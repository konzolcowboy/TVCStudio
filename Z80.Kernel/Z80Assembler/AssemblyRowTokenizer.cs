using System.Linq;

namespace Z80.Kernel.Z80Assembler
{
    public class AssemblyRowTokenizer : RowTokenizer
    {
        public override void TokenizeRow()
        {
            string token = string.Empty;
            var st = TokenStateMachine.Init;
            foreach (char t in RowForTokenising)
            {
                switch (st)
                {
                    case TokenStateMachine.Init:
                        if (char.IsLetterOrDigit(t))
                        {
                            token += t;
                            st = TokenStateMachine.Token;
                        }
                        else if (t == ' ' || t == '\t')
                        {
                            st = TokenStateMachine.Separator;
                        }
                        else if (t == ';')
                        {
                            if (m_KeepComments)
                            {
                                token += t;
                                st = TokenStateMachine.Comment;
                            }
                            else
                            {
                                st = TokenStateMachine.End;
                            }
                        }
                        else if (t == '\n' || t == '\r')
                        {
                            st = TokenStateMachine.End;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"Szintaxis hiba: helytelen karakter:'{t}'. A címke csak betűket vagy számot tartalmazhat!");
                        }
                        continue;
                    case TokenStateMachine.Token:
                        if (char.IsLetterOrDigit(t) || ValidCharacters.Any(c => c == t) || (m_InLiteral && t.IsTvcAscii()))
                        {
                            if (t == '\'' || t == '\"')
                            {
                                m_InLiteral = !m_InLiteral;
                            }
                            token += t;
                        }
                        else if (t == ' ' || t == ':' || t == '\t')
                        {
                            if (m_InLiteral)
                            {
                                token += t;
                            }
                            else
                            {
                                Tokens.Add(token);
                                token = "";
                                st = TokenStateMachine.Separator;
                            }
                        }
                        else if (t == ';')
                        {
                            if (m_InLiteral)
                            {
                                token += t;
                            }
                            else
                            {
                                Tokens.Add(token);
                                if (m_KeepComments)
                                {
                                    token = ";";
                                    st = TokenStateMachine.Comment;
                                }
                                else
                                {
                                    token = "";
                                    st = TokenStateMachine.End;
                                }
                            }
                        }
                        else if (t == '\n' || t == '\r')
                        {
                            st = TokenStateMachine.End;
                        }
                        else
                        {
                            throw new Z80AssemblerException($"Szintaxis hiba: helytelen karakter:'{t}'!");
                        }
                        continue;
                    case TokenStateMachine.Separator:
                        if (char.IsLetterOrDigit(t) || t == '(' || t == '$' || t == '\'' || t == '\"' || t == '.' || t == '=')
                        {
                            if (t == '\'' || t == '\"')
                            {
                                m_InLiteral = !m_InLiteral;
                            }

                            token += t;
                            st = TokenStateMachine.Token;
                        }
                        else if (t == ';')
                        {
                            if (m_KeepComments)
                            {
                                token += t;
                                st = TokenStateMachine.Comment;
                            }
                            else
                            {
                                st = TokenStateMachine.End;
                            }
                        }
                        else if (t == '\n' || t == '\r')
                        {
                            st = TokenStateMachine.End;
                        }
                        else if (t != ' ' && t != '\t')
                        {
                            throw new Z80AssemblerException($"Szintaxis hiba: helytelen karakter:'{t}'!");
                        }
                        continue;
                    case TokenStateMachine.Comment:
                        {
                            if (t == '\n' || t == '\r')
                            {
                                st = TokenStateMachine.End;
                            }
                            else
                            {
                                token += t;
                            }
                        }
                        continue;

                    case TokenStateMachine.End:

                        continue;
                }
            }
            if (token != "")
            {
                Tokens.Add(token);
            }
        }

        public AssemblyRowTokenizer(string row, bool keepComments = false) : base(row)
        {
            m_InLiteral = false;
            m_KeepComments = keepComments;
        }

        private enum TokenStateMachine
        {
            Init,
            Token,
            Separator,
            Comment,
            End
        }

        private bool m_InLiteral;
        private readonly bool m_KeepComments;

        private static char[] ValidCharacters { get; } = { '=', '(', ')', '+', '-', '*', '/', ',', '\'', '$', '\"', '<', '>', '&', '|', '~', '^', '_' };


    }
}
