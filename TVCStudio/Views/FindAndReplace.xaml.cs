using System.Windows;
using TVCStudio.ViewModels;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for FindAndReplace.xaml
    /// </summary>
    public partial class FindAndReplace : Window
    {
        public FindAndReplace(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new FindAndReplaceViewModel(mainViewModel);
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
