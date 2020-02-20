using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Preprocessor
{
    internal class DefinePreprocessor
    {
        public DefinePreprocessor(PreprocessorRowTokenizer tokenizer)
        {
            m_Tokenizer = tokenizer;
        }

        public string DefineName { get; private set; }
        public string DefineValue { get; private set; }


        public void Preprocess()
        {
            if (m_Tokenizer.Tokens.Count < 2)
            {
                throw new Z80AssemblerException($"A {m_Tokenizer.Tokens[0]} utasítás nem állhat üresen!");
            }

            var processingStateMachine = OperandPreProcessState.Init;
            string token = string.Empty;
            foreach (char t in m_Tokenizer.Tokens[1])
            {
                switch (processingStateMachine)
                {
                    case OperandPreProcessState.Init:
                        {
                            if (char.IsLetterOrDigit(t))
                            {
                                token += t;
                                processingStateMachine = OperandPreProcessState.DefineName;
                            }
                        }
                        break;
                    case OperandPreProcessState.DefineName:
                        {
                            if (t == ' ')
                            {
                                DefineName = token;
                                token = string.Empty;
                                processingStateMachine = OperandPreProcessState.DefineValue;
                                continue;
                            }

                            token += t;
                        }
                        break;
                    case OperandPreProcessState.DefineValue:
                        {
                            token += t;
                        }
                        break;
                }
            }

            if (DefineName == null)
            {
                DefineName = token;
            }
            else
            {
                DefineValue = token;
            }

        }

        private enum OperandPreProcessState
        {
            Init,
            DefineName,
            DefineValue
        }

        private readonly PreprocessorRowTokenizer m_Tokenizer;
    }
}
