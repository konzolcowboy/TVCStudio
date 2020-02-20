using System.Collections.Generic;
using TVCStudio.Settings;
using TVCStudio.ViewModels;
using Xceed.Wpf.AvalonDock.Themes;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        public Settings(TvcStudioSettings settings, IReadOnlyDictionary<string, Theme> themes)
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(themes, settings);
        }

        private void SaveClick(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
