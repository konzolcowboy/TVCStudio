using System;
using System.IO;
using System.Windows;
using TVCStudio.ViewModels.Document;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for AssemblyDocumentView.xaml
    /// </summary>
    public partial class AssemblyDocumentView
    {
        private static readonly string XmlFileName = @"AssemblyLayout.tvcset";
        private static readonly string SettingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"TVCStudio");

        private static readonly string LayoutPath = Path.Combine(SettingsDirectory, XmlFileName);

        public AssemblyDocumentView()
        {
            InitializeComponent();
            if (File.Exists(LayoutPath))
            {
                XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(AssemblyDockingManager);
                using (var reader = new StreamReader(LayoutPath))
                {
                    layoutSerializer.Deserialize(reader);
                }
            }

            Loaded += DocumentView_Loaded;
        }

        private void DocumentView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Loaded -= DocumentView_Loaded;
            var model = DataContext as DocumentViewModel;
            model?.InitializeTextArea(CodeEditor);
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }

                XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(AssemblyDockingManager);
                using (var writer = new StreamWriter(LayoutPath))
                {
                    layoutSerializer.Serialize(writer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A layout file mentése sikertelen:{ex.Message}", @"Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
