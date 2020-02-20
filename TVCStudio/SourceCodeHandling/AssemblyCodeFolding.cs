using System;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Folding;
using Z80.Kernel.Preprocessor;

namespace TVCStudio.SourceCodeHandling
{
    internal sealed class AssemblyCodeFolding : CodeFolding
    {
        public AssemblyCodeFolding(TextArea textArea) : base(textArea)
        {
            m_Document = textArea.Document;
        }

        protected override void UpdateFolding()
        {
            IEnumerable<NewFolding> newFoldings = CreateNewFoldings();
            FoldingManager.UpdateFoldings(newFoldings, -1);
        }

        private IEnumerable<NewFolding> CreateNewFoldings()
        {
            List<NewFolding> newFoldings = new List<NewFolding>();
            Stack<Tuple<int, string>> startOffsets = new Stack<Tuple<int, string>>();
            foreach (Tuple<string, string> foldingString in FoldingStrings)
            {
                foreach (var line in m_Document.Lines)
                {
                    string text = m_Document.GetText(line.Offset, line.TotalLength).ToUpper();
                    text = text.Replace(Environment.NewLine, "");
                    int openingOffset = text.IndexOf(foldingString.Item1, StringComparison.Ordinal);
                    int closingoffset = text.IndexOf(foldingString.Item2, StringComparison.Ordinal);

                    if (openingOffset > -1 && closingoffset > -1)
                    {
                        // The opening and the closing are in the same line
                        continue;
                    }
                    if (openingOffset > -1)
                    {
                        int foldingStart = line.Offset + openingOffset;
                        string foldingName = text.Substring(openingOffset + foldingString.Item1.Length);
                        startOffsets.Push(new Tuple<int, string>(foldingStart, foldingName));
                    }
                    else if (closingoffset > -1 && startOffsets.Count > 0)
                    {
                        Tuple<int, string> tuple = startOffsets.Pop();
                        var folding = new NewFolding(tuple.Item1, line.EndOffset) { Name = tuple.Item2 };
                        newFoldings.Add(folding);
                    }
                }
            }

            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;
        }

        private readonly TextDocument m_Document;
        private const string FoldingOpen = @";{";
        private const string FoldingClose = @";}";
        private static readonly List<Tuple<string, string>> FoldingStrings = new List<Tuple<string, string>>
        {
            new Tuple<string, string>(FoldingOpen,FoldingClose),
            new Tuple<string, string>("#"+PreprocessorConstans.PreprocessorDirectives.IfDef,"#"+PreprocessorConstans.PreprocessorDirectives.EndIf),
            new Tuple<string, string>("#"+PreprocessorConstans.PreprocessorDirectives.Else,"#"+PreprocessorConstans.PreprocessorDirectives.EndIf),
            new Tuple<string, string>("#"+PreprocessorConstans.PreprocessorDirectives.Macro,"#"+PreprocessorConstans.PreprocessorDirectives.Endm)
        };

    }
}
