using System;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Indentation;

namespace TVCStudio.SourceCodeHandling.Assembly
{
    internal class AssemblyIndentationStrategy : IIndentationStrategy
    {
        public bool AutoIndentationEnabled
        {
            get; set;
        }

        public AssemblyIndentationStrategy(AssemblyCodeFormatter formatter)
        {
            m_Formatter = formatter;
        }
        public void IndentLine(TextDocument document, DocumentLine line)
        {
            if (AutoIndentationEnabled)
            {
                IndentLineInternal(document, line);
            }
        }

        private void IndentLineInternal(TextDocument document, DocumentLine line)
        {
            DocumentLine lineForFormat = line.PreviousLine ?? line;
            int currentLineNumber = line.LineNumber;

            string text = document.GetText(lineForFormat.Offset, lineForFormat.TotalLength);
            var nextLineText = document.GetText(line.Offset, line.TotalLength);

            //ignore comment only lines
            if (text.TrimStart(' ', '\t').StartsWith(";"))
            {
                return;
            }

            string formattedText = m_Formatter.FormatLine(text);
            if (!formattedText.EndsWith(Environment.NewLine))
            {
                formattedText += Environment.NewLine;
            }

            document.Replace(lineForFormat.Offset, lineForFormat.TotalLength, formattedText);


            //ignore comment only lines
            if (nextLineText.TrimStart(' ', '\t').StartsWith(";"))
            {
                return;
            }

            line = document.GetLineByNumber(currentLineNumber);
            ISegment indentationSegment = TextUtilities.GetWhitespaceAfter(document, lineForFormat.Offset);
            string indentation = document.GetText(indentationSegment);
            indentationSegment = TextUtilities.GetWhitespaceAfter(document, line.Offset);
            document.Replace(indentationSegment.Offset, indentationSegment.Length, indentation,
                OffsetChangeMappingType.RemoveAndInsert);
        }

        public void IndentLines(TextDocument document, int beginLine, int endLine)
        {
        }

        public void IndentLines(TextDocument document)
        {
            for (int lineNumber = 1; lineNumber < document.LineCount; lineNumber++)
            {
                DocumentLine line = document.GetLineByNumber(lineNumber);
                IndentLineInternal(document, line);
            }
        }

        private readonly AssemblyCodeFormatter m_Formatter;
    }
}
