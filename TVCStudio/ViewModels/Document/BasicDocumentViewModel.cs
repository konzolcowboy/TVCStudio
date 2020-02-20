using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using TVCStudio.Settings;
using TVCStudio.SourceCodeHandling;
using TVCStudio.ViewModels.Program;

namespace TVCStudio.ViewModels.Document
{
    internal sealed class BasicDocumentViewModel : DocumentViewModel
    {

        public BasicDocumentViewModel(string programFullPath, ProgramViewModel program, TvcStudioSettings settings) : base(programFullPath, settings)
        {
            Program = program;
            IsReadOny = false;
            CollapseAllCommand = new RelayCommand(o => { }, o => false);
            ExpandAllCommand = new RelayCommand(o => { }, o => false);
        }

        public override ProgramViewModel Program { get; }

        public override void OnSettingsChanged()
        {
            if (!TextAreaLoaded)
            {
                return;
            }

            ApplyEditorStyle();
        }

        public override void FormatCode()
        {
            
        }

        protected override void TextAreaInitialized()
        {
            if (m_CodeAnalyzer == null)
            {
                m_CodeAnalyzer = new BasicCodeAnalyzer(TextArea, Document, Settings);
            }

            OnSettingsChanged();
        }

        protected override void OnDocumentActivated()
        {
        }

        protected override void OnDocumentDeactivated()
        {
        }

        protected override void OnDocumentTextEntering(string enteredText)
        {
            if (char.IsLetter(enteredText[0]))
            {
                if (CodeIntellisense.Basic.CompletionWindow == null)
                {
                    CodeIntellisense.Basic.Show(TextArea, m_CodeAnalyzer.InterpretedSymbols);
                }
            }
        }

        protected override void OnDocumentKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                CodeIntellisense.Basic.Show(TextArea, m_CodeAnalyzer.InterpretedSymbols);
                e.Handled = true;
            }
        }

        protected override void OnTextAreaUninitialized()
        {
            m_CodeAnalyzer.Dispose();
        }

        private void ApplyEditorStyle()
        {
            Editor.Background = new SolidColorBrush(Settings.BasicEditorSettings.BackgroundColor.Color);
            Editor.Foreground = new SolidColorBrush(Settings.BasicEditorSettings.ForegroundColor.Color);
            Editor.FontFamily = new FontFamily(Settings.BasicEditorSettings.EditorFontName);
            Editor.FontSize = Settings.BasicEditorSettings.EditorFontSize;

            IHighlightingDefinition basicDefinition = HighlightingManager.Instance.GetDefinition(StyleNames.Basic);

            var color = basicDefinition.GetNamedColor(ColorNames.StringColor);
            color.Foreground = new SimpleHighlightingBrush(Settings.BasicEditorSettings.StringColor.Color);

            color = basicDefinition.GetNamedColor(ColorNames.BasicInstructions);
            color.Foreground = new SimpleHighlightingBrush(Settings.BasicEditorSettings.KeywordColor.Color);

            color = basicDefinition.GetNamedColor(ColorNames.NumericConstans);
            color.Foreground = new SimpleHighlightingBrush(Settings.BasicEditorSettings.NumberColor.Color);
        }

        private BasicCodeAnalyzer m_CodeAnalyzer;
    }
}
