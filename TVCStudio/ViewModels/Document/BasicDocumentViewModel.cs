using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using TVCStudio.Settings;
using TVCStudio.SourceCodeHandling;
using TVCStudio.SourceCodeHandling.Basic;
using TVCStudio.ViewModels.Program;

namespace TVCStudio.ViewModels.Document
{
    public enum BasicDocumentEditMode
    {
        UseRowNumbers,
        UseLabels
    }

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

        public override ProgramViewModel Program { get; }

        public ICommand RenumberCodeCommand { get; }

        public BasicDocumentEditMode EditMode
        {
            get => m_EditMode;
            set
            {
                if (m_EditMode != value)
                {
                    m_EditMode = value;
                    OnPropertyChanged(nameof(EditMode));
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

            if (m_BasicCodeHandler == null)
            {
                m_BasicCodeHandler = new BasicCodeHandler(Document, Settings);
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
            m_CodeAnalyzer.Dispose();
        }

        private void RenumberCode(object obj)
        {
            m_BasicCodeHandler?.RenumberCode();
        }

        private void ChangeEditMode()
        {
            if(EditMode == BasicDocumentEditMode.UseRowNumbers)
            {
                m_BasicCodeHandler.UseRowNumbers();
            }
            else
            {
                m_BasicCodeHandler.UseLabels();
            }
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
        }

        private BasicCodeAnalyzer m_CodeAnalyzer;
        private BasicCodeHandler m_BasicCodeHandler;
        private BasicDocumentEditMode m_EditMode;
    }
}
