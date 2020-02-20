using TVCStudio.ViewModels.Document;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for DocumentView.xaml
    /// </summary>
    public partial class BasicDocumentView
    {
        public BasicDocumentView()
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
