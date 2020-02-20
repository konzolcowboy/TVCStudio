using System.Linq;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Preprocessor
{
    internal class PreprocessorRowTokenizer : RowTokenizer
    {
        public PreprocessorRowTokenizer(string row) : base(row)
        {
        }

        public override void TokenizeRow()
        {
            string token = string.Empty;
            var st = TokenStateMachine.Init;
            foreach (char t in RowForTokenising)
            {
                switch (st)
                {
                    case TokenStateMachine.Init:
                        {
                            if (t == '#')
                            {
                                st = TokenStateMachine.Instruction;
                            }
                        }
                        continue;
                    case TokenStateMachine.Instruction:
                        {
                            if (t == ' ' || t == '\t')
                            {
                                if (string.IsNullOrEmpty(token))
                                {
                                    throw new Z80AssemblerException("Üres előfeldolgozó utasítás! A '#' jel után nem állhat szóköz!");
                                }

                                if (!IsPreprocessorInstruction(token))
                                {
                                    throw new Z80AssemblerException($"Érvénytelen előfeldolgozó utasítás: '{token}'");
                                }

                                Tokens.Add(token);
                                token = string.Empty;
                                st = TokenStateMachine.Separator;
                            }
                            else
                            {
                                token += t;
                            }
                        }
                        continue;
                    case TokenStateMachine.Separator:
                        {
                            if (t != ' ' && t != '\t')
                            {
                                token += t;
                                st = TokenStateMachine.Operand;
                            }
                        }
                        continue;
                    case TokenStateMachine.Operand:
                        {
                            token += t;
                        }
                        continue;
                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                Tokens.Add(token);
            }
        }

        public override string ToString()
        {
            if (Tokens.Count > 0)
            {
                return string.Join(" ", Tokens);
            }

            return base.ToString();
        }

        private enum TokenStateMachine
        {
            Init,
            Instruction,
            Separator,
            Operand
        }

        private bool IsPreprocessorInstruction(string token)
        {
            return Z80Preprocessor.SupportedInstructions.Any(s => s.Equals(token.ToUpper()));
        }
    }
}
