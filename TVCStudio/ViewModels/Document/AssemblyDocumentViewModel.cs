using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using TVCStudio.Settings;
using TVCStudio.SourceCodeHandling;
using TVCStudio.ViewModels.Program;
using Xceed.Wpf.AvalonDock.Themes;

namespace TVCStudio.ViewModels.Document
{
    internal static class ColorNames
    {
        public const string Comment = @"Comment";
        public const string StringColor = @"String";
        public const string CharColor = @"Char";
        public const string ProcessorInstructions = @"ProcessorInstructions";
        public const string AssemblerInstructions = @"AssemblerInstructions";
        public const string PreprocessorInstructions = @"PreprocessorInstructions";
        public const string HexaConstans = @"HexaConstans";
        public const string Expressions = @"Expressions";
        public const string BasicInstructions = @"BasicInstructions";
        public const string NumericConstans = @"NumericConstans";
        public const string RegisterColor = @"Registers";
    }
    internal sealed class AssemblyDocumentViewModel : DocumentViewModel
    {
        public AssemblyDocumentViewModel(string programFullPath, ProgramViewModel program, TvcStudioSettings settings) : base(programFullPath, settings)
        {
            Program = program;
            IsReadOny = false;
            CollapseAllCommand = new RelayCommand(o => m_CodeFolding.CollapseAll(),
                o => m_CodeFolding != null && m_CodeFolding.FoldingCount > 0);
            ExpandAllCommand = new RelayCommand(o => m_CodeFolding.ExpandAll(),
                o => m_CodeFolding != null && m_CodeFolding.FoldingCount > 0);

            InterpretedSymbols = new ObservableCollection<SymbolData>();
            WrongLines = new ObservableCollection<WrongLineData>();
        }

        public override ProgramViewModel Program
        {
            get;
        }

        public override void OnSettingsChanged()
        {
            if (!TextAreaLoaded)
            {
                return;
            }

            SelectedTheme = Themes[Settings.SelectedTheme];
            m_AssemblyIndentationStrategy.AutoIndentationEnabled = Settings.AssemblyEditorSettings.AutoCodeFormating;

            if (m_AssemblyIndentationStrategy.AutoIndentationEnabled)
            {
                m_AssemblyIndentationStrategy.IndentLines(Document);
                Save();
            }

            if (Settings.AssemblyEditorSettings.CodeFolding)
            {
                m_CodeFolding.ResumeUpdate();
            }
            else
            {
                m_CodeFolding.SuspendUpdate(true);
            }

            if (Settings.AssemblyEditorSettings.AutoCodeAnalization)
            {
                m_CodeAnalyzer.Trigger = AnalizerTrigger.DocumentChange;
                m_CodeAnalyzer.AnalyzeCode();
            }
            else
            {
                m_CodeAnalyzer.ClearResults();
                InterpretedSymbols.Clear();
                WrongLines.Clear();
                m_CodeAnalyzer.Trigger = AnalizerTrigger.Manual;
            }

            ApplyEditorStyle();
        }

        public override void FormatCode()
        {
            var caretLocation = TextArea.Caret.Location;
            m_AssemblyIndentationStrategy.IndentLines(Document);
            Save();
            TextArea.Caret.Location = caretLocation;
            TextArea.Caret.BringCaretToView();
        }

        public ObservableCollection<SymbolData> InterpretedSymbols
        {
            get;
        }

        public ObservableCollection<WrongLineData> WrongLines
        {
            get;
        }

        public WrongLineData SelectedWrongLine
        {
            get => m_SelectedWrongLine;
            set
            {
                m_SelectedWrongLine = value;
                if (m_SelectedWrongLine != null)
                {
                    TextLocation loc = Document.GetLocation(Document.GetLineByNumber(m_SelectedWrongLine.LineNumber).Offset);
                    Editor.ScrollTo(loc.Line, loc.Column);
                }
            }
        }

        public SymbolData SelectedSymbol
        {
            get => m_SelectedSymbol;
            set
            {
                m_SelectedSymbol = value;
                if (m_SelectedSymbol != null)
                {
                    SelectSymbol();
                }
            }
        }

        public Theme SelectedTheme
        {
            get => m_SelectedTheme;

            set
            {
                if (m_SelectedTheme == null || !m_SelectedTheme.Equals(value))
                {
                    m_SelectedTheme = value;
                    OnPropertyChanged(nameof(SelectedTheme));
                }
            }
        }

        public FontFamily EditorFont
        {
            get; private set;
        }

        public int EditorFontSize
        {
            get; private set;
        }

        protected override void TextAreaInitialized()
        {
            if (m_CodeAnalyzer == null)
            {
                m_CodeAnalyzer = new AssemblyCodeAnalyzer(TextArea, Document, Settings, AnalizerTrigger.DocumentChange, ProgramFullPath);
                m_CodeAnalyzer.AnalizingFinished += M_CodeAnalyzer_AnalizingFinished;
            }

            if (m_CodeFolding == null)
            {
                m_CodeFolding = new AssemblyCodeFolding(TextArea);
            }

            if (m_AssemblyCodeFormatter == null)
            {
                m_AssemblyCodeFormatter = new AssemblyCodeFormatter(Settings.AssemblyIndentationSettings);
            }

            if (m_AssemblyIndentationStrategy == null)
            {
                m_AssemblyIndentationStrategy = new AssemblyIndentationStrategy(m_AssemblyCodeFormatter);
                TextArea.IndentationStrategy = m_AssemblyIndentationStrategy;
            }

            if (m_ClockCycleMargin == null)
            {
                m_ClockCycleMargin = new ClockCycleMargin(m_CodeAnalyzer, Settings);
                TextArea.LeftMargins.Add(m_ClockCycleMargin);
            }

            OnSettingsChanged();
        }

        protected override void OnDocumentActivated()
        {
            if (Settings.AssemblyEditorSettings.CodeFolding)
            {
                m_CodeFolding?.ResumeUpdate();
            }
        }

        protected override void OnDocumentDeactivated()
        {
            m_CodeFolding?.SuspendUpdate();
        }

        protected override void OnDocumentTextEntering(string enteredText)
        {
            if (char.IsLetter(enteredText[0]))
            {
                if (CodeIntellisense.Assembly.CompletionWindow == null && Settings.AutoIntellisence)
                {
                    CodeIntellisense.Assembly.Show(TextArea, m_CodeAnalyzer.InterpretedSymbols);
                }
            }
        }

        protected override void OnDocumentKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                NavigateToSymbol();
            }
            if (e.Key == Key.Space && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                CodeIntellisense.Assembly.Show(TextArea, m_CodeAnalyzer.InterpretedSymbols);
                e.Handled = true;
            }
        }

        protected override void OnTextAreaUninitialized()
        {
            TextArea.LeftMargins.Remove(m_ClockCycleMargin);
            m_ClockCycleMargin.Dispose();
            m_ClockCycleMargin = null;
            m_CodeFolding.Dispose();
            m_CodeFolding = null;
            m_CodeAnalyzer.AnalizingFinished -= M_CodeAnalyzer_AnalizingFinished;
            m_CodeAnalyzer.Dispose();
            m_CodeAnalyzer = null;
            TextArea.IndentationStrategy = null;
            m_AssemblyIndentationStrategy = null;
        }

        private void M_CodeAnalyzer_AnalizingFinished(object sender, System.EventArgs e)
        {
            InterpretedSymbols.Clear();
            WrongLines.Clear();
            m_CodeAnalyzer.InterpretedSymbols.ForEach(sd => InterpretedSymbols.Add(sd));
            m_CodeAnalyzer.WrongLine.ForEach(wl => WrongLines.Add(new WrongLineData { ErrorText = wl.Item1, LineNumber = wl.Item2 }));
        }

        private void NavigateToSymbol()
        {
            string symbolName = Document.GetText(GetTextUnderCaret());
            string searchpattern = @"^\t{0,}\s{0,}" + symbolName;
            Regex reg = new Regex(searchpattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (DocumentLine line in Document.Lines)
            {
                string text = Document.GetText(line.Offset, line.Length);
                Match m = reg.Match(text, 0, line.Length);
                if (m.Success)
                {
                    int selectionOffset = line.Offset + m.Index;
                    Editor.Select(selectionOffset, m.Length);
                    TextLocation loc = Document.GetLocation(selectionOffset);
                    Editor.ScrollTo(loc.Line, loc.Column);
                }
            }
        }

        private void SelectSymbol()
        {
            if (m_SelectedSymbol != null)
            {
                DocumentLine line = Document.GetLineByNumber(m_SelectedSymbol.LineNumber);
                string text = Document.GetText(line.Offset, line.Length);
                Regex reg = new Regex(m_SelectedSymbol.SymbolName, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = reg.Match(text, 0, line.Length);
                if (m.Success)
                {
                    int selectionOffset = line.Offset + m.Index;
                    Editor.Select(selectionOffset, m.Length);
                    TextLocation loc = Document.GetLocation(selectionOffset);
                    Editor.ScrollTo(loc.Line, loc.Column);
                }
            }
        }

        private void ApplyEditorStyle()
        {
            Editor.Background = new SolidColorBrush(Settings.AssemblyEditorSettings.BackgroundColor.Color);
            Editor.Foreground = new SolidColorBrush(Settings.AssemblyEditorSettings.ForegroundColor.Color);
            IHighlightingDefinition assemblyDefinition = HighlightingManager.Instance.GetDefinition(StyleNames.Assembly);

            var color = assemblyDefinition.GetNamedColor(ColorNames.Comment);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.CommentColor.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.StringColor);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.StringColor.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.CharColor);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.StringColor.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.ProcessorInstructions);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.ProcessorInstruction.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.RegisterColor);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.RegisterColor.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.AssemblerInstructions);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.AssemblerInstruction.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.PreprocessorInstructions);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.PreprocessorInstructionColor.Color);

            color = assemblyDefinition.GetNamedColor(ColorNames.Expressions);
            color.Foreground = new SimpleHighlightingBrush(Settings.AssemblyEditorSettings.ExpressionColor.Color);

            EditorFont = new FontFamily(Settings.AssemblyEditorSettings.EditorFontName);
            EditorFontSize = Settings.AssemblyEditorSettings.EditorFontSize;
            OnPropertyChanged(nameof(EditorFont));
            OnPropertyChanged(nameof(EditorFontSize));
        }

        private AssemblyCodeAnalyzer m_CodeAnalyzer;
        private CodeFolding m_CodeFolding;
        private AssemblyIndentationStrategy m_AssemblyIndentationStrategy;
        private SymbolData m_SelectedSymbol;
        private WrongLineData m_SelectedWrongLine;
        private AssemblyCodeFormatter m_AssemblyCodeFormatter;
        private Theme m_SelectedTheme;
        private ClockCycleMargin m_ClockCycleMargin;
    }
}
