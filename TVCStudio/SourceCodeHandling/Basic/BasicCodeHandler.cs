using ICSharpCode.AvalonEdit.Document;
using System.Text.RegularExpressions;
using TVCStudio.Settings;

namespace TVCStudio.SourceCodeHandling.Basic
{
    internal class BasicCodeHandler
    {
        public BasicCodeHandler(TextDocument document, TvcStudioSettings settings)
        {
            m_Document = document;
            m_Settings = settings;
        }

        public void RenumberCode()
        {
            m_Document.BeginUpdate();
            int newRowNumber = m_Settings.BasicEditorSettings.StartRowNumber;

            for (int lineNumber = 1; lineNumber <= m_Document.LineCount; lineNumber++)
            {
                string newRowNumberString = newRowNumber.ToString();
                DocumentLine line = m_Document.GetLineByNumber(lineNumber);
                string currentRowNumberString = ResolveRowNumber(m_Document.GetText(line.Offset, line.Length));
                if (!string.IsNullOrEmpty(currentRowNumberString))
                {
                    ReplaceRowNumberInJumpInstructions(currentRowNumberString, newRowNumberString);
                    line = m_Document.GetLineByNumber(lineNumber);
                    string linetext = m_Document.GetText(line.Offset, line.Length);
                    m_Document.Replace(line.Offset + linetext.IndexOf(currentRowNumberString), currentRowNumberString.Length, newRowNumberString);
                    newRowNumber += m_Settings.BasicEditorSettings.RowNumberIncrement;
                }
            }

            m_Document.EndUpdate();
        }

        public void UseRowNumbers()
        {

        }

        public void UseLabels()
        {

        }

        private void ReplaceRowNumberInJumpInstructions(string rowNumberString, string replaceFor)
        {
            string regexPatternForInstructionsWithOneRowNumber = $"THEN[ ]{{0,}}(?<rownumber>{rowNumberString})|ELSE[ ]{{0,}}(?<rownumber>{rowNumberString})|CONTINUE[ ]{{0,}}(?<rownumber>{rowNumberString})|RUN[ ]{{0,}}(?<rownumber>{rowNumberString})";
            string gotoAndGosubpattern = @"(?<match>(?<token>(goto|gosub)[ ]{0,})(?<rownumber>([ ]{0,}[0-9]{1,4})|[ ]{0,}[,]{1}[ ]{0,}(\b[0-9]{1,4}))+)";

            var regexForInstructionsWithOneRowNumber = new Regex(regexPatternForInstructionsWithOneRowNumber, RegexOptions.IgnoreCase);

            int offset = 0;
            foreach (Match match in regexForInstructionsWithOneRowNumber.Matches(m_Document.Text))
            {
                m_Document.Replace(match.Groups["rownumber"].Index, match.Groups["rownumber"].Length, replaceFor);
                offset += replaceFor.Length - match.Groups["rownumber"].Length;
            }

            var regexForGotoAndGosub = new Regex(gotoAndGosubpattern, RegexOptions.IgnoreCase);

            offset = 0;
            foreach (Match m in regexForGotoAndGosub.Matches(m_Document.Text))
            {
                foreach (Capture capture in m.Groups["rownumber"].Captures)
                {
                    int offSetOfRownumber = capture.Value.IndexOf(rowNumberString, System.StringComparison.OrdinalIgnoreCase);
                    if (offSetOfRownumber > -1)
                    {
                        offSetOfRownumber += capture.Index;
                        m_Document.Replace(offset + offSetOfRownumber, rowNumberString.Length, replaceFor);
                        offset += replaceFor.Length - rowNumberString.Length;
                    }
                }
            }
        }

        private string ResolveRowNumber(string inputline)
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

        private readonly TextDocument m_Document;
        private readonly TvcStudioSettings m_Settings;
    }
}
