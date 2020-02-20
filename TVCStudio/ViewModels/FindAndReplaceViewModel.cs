using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;

namespace TVCStudio.ViewModels
{
    internal sealed class FindAndReplaceViewModel : ViewModelBase
    {
        public FindAndReplaceViewModel(MainViewModel mainviewModel)
        {
            m_MainViewModel = mainviewModel;
            FindNextCommand = new RelayCommand(FindNext);
            ReplaceCommand = new RelayCommand(Replace);
            ReplaceAllCommand = new RelayCommand(ReplaceAll);
            FindNormal = true;
        }

        public ICommand FindNextCommand { get; }

        public ICommand ReplaceCommand { get; }

        public ICommand ReplaceAllCommand { get; }

        public bool WholeWord
        {
            get => m_WholeWord;
            set
            {
                if (m_WholeWord != value)
                {
                    m_WholeWord = value;
                    OnPropertyChanged(nameof(m_WholeWord));
                }
            }
        }

        public bool CaseSensitive
        {
            get => m_CaseSensitive;
            set
            {
                if (m_CaseSensitive != value)
                {
                    m_CaseSensitive = value;
                    OnPropertyChanged(nameof(CaseSensitive));
                }
            }
        }

        public bool FindNormal
        {
            get => m_FindNormal;
            set
            {
                if (m_FindNormal != value)
                {
                    m_FindNormal = value;
                    OnPropertyChanged(nameof(FindNormal));
                }
            }
        }

        public string TextToFind
        {
            get => m_TextToFind;
            set
            {
                if (m_TextToFind != value)
                {
                    m_TextToFind = value;
                    OnPropertyChanged(nameof(TextToFind));
                }
            }
        }

        public string ReplaceWith
        {
            get => m_ReplaceWith;
            set
            {
                if (m_ReplaceWith != value)
                {
                    m_ReplaceWith = value;
                    OnPropertyChanged(nameof(ReplaceWith));
                }
            }
        }

        private void FindNext(object obj)
        {
            if (m_MainViewModel.ActiveDocument == null)
            {
                return;
            }

            TextEditor editor = m_MainViewModel.ActiveDocument.Editor;
            Regex regex = GetRegEx(m_TextToFind);
            int start = regex.Options.HasFlag(RegexOptions.RightToLeft) ?
                editor.SelectionStart : editor.SelectionStart + editor.SelectionLength;
            Match match = regex.Match(editor.Text, start);

            if (!match.Success)  // start again from beginning or end
            {
                match = regex.Match(editor.Text, regex.Options.HasFlag(RegexOptions.RightToLeft) ? editor.Text.Length : 0);
            }

            if (match.Success)
            {
                editor.Select(match.Index, match.Length);
                TextLocation loc = editor.Document.GetLocation(match.Index);
                editor.ScrollTo(loc.Line, loc.Column);
            }
        }

        private void Replace(object obj)
        {
            if (m_MainViewModel.ActiveDocument == null)
            {
                return;
            }

            TextEditor editor = m_MainViewModel.ActiveDocument.Editor;
            Regex regex = GetRegEx(TextToFind);
            string input = editor.Text.Substring(editor.SelectionStart, editor.SelectionLength);
            Match match = regex.Match(input);
            if (match.Success && match.Index == 0 && match.Length == input.Length)
            {
                editor.Document.Replace(editor.SelectionStart, editor.SelectionLength, ReplaceWith);
            }

            FindNext(null);
        }

        private void ReplaceAll(object obj)
        {
            if (m_MainViewModel.ActiveDocument == null)
            {
                return;
            }

            if (MessageBox.Show($"Biztos benne, hogy lecseréli a(z) '{TextToFind}' összes előfordulását erre '{ReplaceWith}'?",
                    "Összes cseréje", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Regex regex = GetRegEx(TextToFind);
                int offset = 0;
                TextEditor editor = m_MainViewModel.ActiveDocument.Editor;
                editor.BeginChange();
                int findCount = 0;
                foreach (Match match in regex.Matches(editor.Text))
                {
                    editor.Document.Replace(offset + match.Index, match.Length, ReplaceWith);
                    offset += ReplaceWith.Length - match.Length;
                    findCount++;
                }
                editor.EndChange();
                MessageBox.Show($"{findCount} számú találat lecserélve a szövegben!");
            }
        }

        private Regex GetRegEx(string textToFind)
        {
            RegexOptions options = RegexOptions.None;
            if (!CaseSensitive)
            {
                options |= RegexOptions.IgnoreCase;
            }

            if (!FindNormal)
            {
                return new Regex(textToFind, options);
            }

            string pattern = Regex.Escape(textToFind);
            if (WholeWord)
            {
                pattern = "\\b" + pattern + "\\b";
            }

            return new Regex(pattern, options);
        }

        private readonly MainViewModel m_MainViewModel;
        private bool m_WholeWord;
        private bool m_CaseSensitive;
        private bool m_FindNormal;
        private string m_TextToFind;
        private string m_ReplaceWith;
    }
}
