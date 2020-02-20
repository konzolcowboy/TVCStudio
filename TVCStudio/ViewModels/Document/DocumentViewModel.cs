using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using TVCStudio.Settings;
using TVCStudio.SourceCodeHandling.Search;
using TVCStudio.Trace;
using TVCStudio.ViewModels.Program;

namespace TVCStudio.ViewModels.Document
{
    public abstract class DocumentViewModel : ViewModelBase
    {

        public bool ShowLineNumbers
        {
            get => m_ShowLineNumbers;
            set
            {
                if (m_ShowLineNumbers != value)
                {
                    m_ShowLineNumbers = value;
                    OnPropertyChanged(nameof(ShowLineNumbers));
                }
            }
        }
        public bool IsActive
        {
            get => m_IsActive;
            set
            {
                if (m_IsActive != value)
                {
                    m_IsActive = value;
                    OnPropertyChanged(nameof(IsActive));
                    if (m_IsActive)
                    {
                        OnDocumentActivated();
                    }
                    else
                    {
                        OnDocumentDeactivated();
                    }
                }
            }
        }
        public bool IsDirty
        {
            get => m_IsDirty;
            set
            {
                if (m_IsDirty != value)
                {
                    m_IsDirty = value;
                    OnPropertyChanged(nameof(IsDirty));
                    Title = m_IsDirty ? $"{Title}*" : Title.Replace("*", "");
                }
            }
        }
        public string Title
        {
            get => m_Title;
            set
            {
                if (m_Title != value)
                {
                    m_Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public ICommand CloseCommand
        {
            get; set;
        }

        public ICommand CollapseAllCommand
        {
            get; set;
        }

        public ICommand QuickSearchCommand { get; }

        public ICommand ExpandAllCommand
        {
            get; set;
        }

        public event EventHandler DocumentClosedEvent;

        public TextDocument Document
        {
            get; private set;
        }

        public TvcStudioSettings Settings
        {
            get;
        }

        public string ProgramFullPath
        {
            get;
        }

        public IHighlightingDefinition SyntaxHighlighting
        {
            get;
        }

        public bool IsReadOny
        {
            get; protected set;
        }

        public abstract ProgramViewModel Program
        {
            get;
        }

        protected bool TextAreaLoaded
        {
            get; private set;
        }

        public abstract void OnSettingsChanged();

        public abstract void FormatCode();

        public void Save()
        {
            try
            {
                FileStream fileStream = new FileStream(ProgramFullPath, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    Document.WriteTextTo(writer);
                    writer.Flush();
                }

                IsDirty = false;

            }
            catch (Exception e)
            {
                TraceEngine.TraceError($"'{ProgramFullPath}' mentése sikertelen:{e.Message}");
            }
        }

        public void InitializeTextArea(TextEditor editor)
        {
            Editor = editor;
            TextArea = editor.TextArea;
            TextArea.KeyDown += OnKeyDown;
            TextArea.TextEntering += OnTextEntering;
            m_SearchPanel = SearchPanel.Install(TextArea);
            m_SearchPanel.Localization = new HunLocalization();
            TextAreaLoaded = true;
            TextAreaInitialized();
        }

        protected DocumentViewModel(string programFullPath, TvcStudioSettings settings)
        {
            Settings = settings;
            ProgramFullPath = programFullPath;
            SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(ProgramFullPath));
            Title = Path.GetFileName(ProgramFullPath);
            Document = new TextDocument();
            Load();
            Document.TextChanged += OnTextChanged;
            ShowLineNumbers = true;
            CloseCommand = new RelayCommand(Close);
            QuickSearchCommand = new RelayCommand(QuickSearch);
        }

        protected TextArea TextArea
        {
            get; set;
        }

        public TextEditor Editor
        {
            get; set;
        }

        protected abstract void TextAreaInitialized();

        protected abstract void OnDocumentActivated();

        protected abstract void OnDocumentDeactivated();

        protected abstract void OnDocumentTextEntering(string enteredText);

        protected abstract void OnDocumentKeyDown(KeyEventArgs e);

        protected abstract void OnTextAreaUninitialized();

        protected ISegment GetTextUnderCaret()
        {
            int startoffset = TextArea.Caret.Offset;
            int endoffset = startoffset + 1;

            bool symbolStartFound = false, symbolEndFound = false;
            while (!(symbolStartFound && symbolEndFound))
            {
                string text;

                if (!symbolStartFound)
                {
                    text = Document.GetText(startoffset, 1);
                    if (!char.IsLetterOrDigit(text[0]) && text[0] != '_')
                    {
                        symbolStartFound = true;
                    }
                    else
                    {
                        startoffset--;
                    }
                }

                if (!symbolEndFound)
                {
                    text = Document.GetText(endoffset, 1);
                    if (!char.IsLetterOrDigit(text[0]) && text[0] != '_')
                    {
                        symbolEndFound = true;
                    }
                    else
                    {
                        endoffset++;
                    }
                }
            }

            return new TextSegment { StartOffset = ++startoffset, EndOffset = endoffset };
        }

        private void OnTextEntering(object sender, TextCompositionEventArgs e)
        {
            OnDocumentTextEntering(e.Text);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Add && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (Editor.FontSize < 60.0)
                {
                    Editor.FontSize++;
                }
            }

            if (e.Key == Key.Subtract && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (Editor.FontSize > 6.0)
                {
                    Editor.FontSize--;
                }
            }

            OnDocumentKeyDown(e);
        }

        private void Close(object param)
        {
            if (IsDirty)
            {
                var result = MessageBox.Show($"{Title} tartalma megváltozott.\nKívánja menteni a módosítást?",
                    @"Dokumentum bezárása", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Save();
                }
            }

            if (TextAreaLoaded)
            {
                TextArea.KeyDown -= OnKeyDown;
                TextArea.TextEntering -= OnTextEntering;
                Document.TextChanged -= OnTextChanged;
                m_SearchPanel.Uninstall();
                TextAreaLoaded = false;
                OnTextAreaUninitialized();
                //DataObject.RemovePastingHandler(Editor, EditorPaste);
            }

            DocumentClosedEvent?.Invoke(this, new EventArgs());
            TextArea = null;
            Document = null;
            m_SearchPanel = null;
        }

        private void QuickSearch(object obj)
        {
            m_SearchPanel?.Open();
            m_SearchPanel?.Reactivate();
        }


        private void OnTextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void Load()
        {
            try
            {
                FileStream fileStream = new FileStream(ProgramFullPath, FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    Document.Text = reader.ReadToEnd();
                }
            }

            catch (Exception e)
            {
                TraceEngine.TraceError($"'{ProgramFullPath}' beolvasása sikertelen:{e.Message}");
            }
        }

        private string m_Title;
        private bool m_ShowLineNumbers;
        private bool m_IsDirty;
        private bool m_IsActive;
        private SearchPanel m_SearchPanel;
    }
}
