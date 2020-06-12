using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using TVC.Basic;
using TVCStudio.Settings;
using TVCStudio.SourceCodeHandling;
using TVCStudio.SourceCodeHandling.Basic;
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
            RenumberCodeCommand = new RelayCommand(RenumberCode);
        }

        private void Document_TextChanged(object sender, System.EventArgs e)
        {
            DetectSimplifiedCode();
        }

        public override ProgramViewModel Program { get; }

        public ICommand RenumberCodeCommand { get; }

        public bool RenumberingAllowed => true; //!SimplifiedMode;

        public bool SimplifiedMode
        {
            get => m_SimplifiedMode;
            set
            {
                if (m_SimplifiedMode != value)
                {
                    m_SimplifiedMode = value;
                    OnPropertyChanged(nameof(SimplifiedMode));
                    OnPropertyChanged(nameof(RenumberingAllowed));
                }
            }
        }

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

        public bool RemoveSpacesBeforeBuild
        {
            get => Settings.BasicEditorSettings.RemoveSpacesBeforeBuild;
            set
            {
                if (Settings.BasicEditorSettings.RemoveSpacesBeforeBuild != value)
                {
                    Settings.BasicEditorSettings.RemoveSpacesBeforeBuild = value;
                    OnPropertyChanged(nameof(RemoveSpacesBeforeBuild));
                }
            }
        }

        public int StartRowNumber
        {
            get => Settings.BasicEditorSettings.StartRowNumber;
            set
            {
                if (Settings.BasicEditorSettings.StartRowNumber != value)
                {
                    Settings.BasicEditorSettings.StartRowNumber = value;
                    OnPropertyChanged(nameof(StartRowNumber));
                }
            }
        }

        public int RowNumberIncrement
        {
            get => Settings.BasicEditorSettings.RowNumberIncrement;
            set
            {
                if (Settings.BasicEditorSettings.RowNumberIncrement != value)
                {
                    Settings.BasicEditorSettings.RowNumberIncrement = value;
                    OnPropertyChanged(nameof(RowNumberIncrement));
                }
            }
        }


        protected override void TextAreaInitialized()
        {
            if (m_CodeAnalyzer == null)
            {
                m_CodeAnalyzer = new BasicCodeAnalyzer(TextArea, Document, Settings);
            }

            DetectSimplifiedCode();

            Document.TextChanged += Document_TextChanged;

            OnSettingsChanged();
        }

        protected override void OnDocumentActivated()
        {
            DetectSimplifiedCode();
        }

        protected override void OnDocumentDeactivated()
        {
        }

        protected override void OnDocumentTextEntering(string enteredText)
        {
            if (char.IsLetter(enteredText[0]))
            {
                if (CodeIntellisense.Basic.CompletionWindow == null && Settings.AutoIntellisence)
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
            Document.TextChanged -= Document_TextChanged;
            m_CodeAnalyzer.Dispose();
        }

        private void DetectSimplifiedCode()
        {
            IEnumerable<string> basicRows = Document.Lines.Select(dl => Document.GetText(dl.Offset, dl.Length));
            SimplifiedMode = BasicHelper.BasicCodeIsSimplified(basicRows);
        }


        private void RenumberCode(object obj)
        {
            IEnumerable<string> basicRows = Document.Lines.Select(dl => Document.GetText(dl.Offset, dl.Length));

            string renumberedBasicProgram = BasicHelper.RenumberBasicProgram(basicRows,
                Settings.BasicEditorSettings.StartRowNumber,
                Settings.BasicEditorSettings.RowNumberIncrement);

            Document.BeginUpdate();
            Document.Text = renumberedBasicProgram;
            Document.EndUpdate();
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

            color = basicDefinition.GetNamedColor(ColorNames.Comment);
            color.Foreground = new SimpleHighlightingBrush(Settings.BasicEditorSettings.CommentColor.Color);

            color = basicDefinition.GetNamedColor(ColorNames.UserMethods);
            color.Foreground = new SimpleHighlightingBrush(Settings.BasicEditorSettings.UserMethodColor.Color);
            
            color = basicDefinition.GetNamedColor(ColorNames.RowSymbol);
            color.Foreground = new SimpleHighlightingBrush(Settings.BasicEditorSettings.RowSymbolColor.Color);
        }

        private BasicCodeAnalyzer m_CodeAnalyzer;
        private bool m_SimplifiedMode;
    }
}
