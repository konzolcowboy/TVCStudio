using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using TVCStudio.Settings;

namespace TVCStudio.SourceCodeHandling.Basic
{
    internal sealed class BasicCodeAnalyzer : CodeAnalyzer
    {
        public BasicCodeAnalyzer(TextArea area, TextDocument document, TvcStudioSettings settings, AnalizerTrigger trigger = AnalizerTrigger.Manual) : base(area, document, settings, trigger)
        {
        }

        public override void AnalyzeCode()
        {

        }
    }
}
