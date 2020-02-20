using System.Collections.Generic;

namespace Z80.Kernel
{
    public abstract class RowTokenizer
    {
        public List<string> Tokens { get; }

        public abstract void TokenizeRow();
        protected RowTokenizer(string row)
        {
            Tokens = new List<string>();
            RowForTokenising = row;
        }

        protected readonly string RowForTokenising;

    }
}
