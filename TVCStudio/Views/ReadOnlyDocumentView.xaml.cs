using TVCStudio.ViewModels.Document;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for ReadOnlyDocumentView.xaml
    /// </summary>
    public partial class ReadOnlyDocumentView
    {
        public ReadOnlyDocumentView()
        {
            InitializeComponent();
            Loaded += DocumentView_Loaded;
        }
        private void DocumentView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Loaded -= DocumentView_Loaded;
            var model = DataContext as DocumentViewModel;
            model?.InitializeTextArea(CodeEditor);
        }
    }
}

