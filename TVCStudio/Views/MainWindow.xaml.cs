using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using TVCStudio.Settings;
using TVCStudio.ViewModels;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly string XmlFileName = @"MainLayout.tvcset";
        private static readonly string SettingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"TVCStudio");

        private static readonly string LayoutPath = Path.Combine(SettingsDirectory, XmlFileName);

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(LayoutPath))
            {
                XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(DockingManager);
                using (var reader = new StreamReader(LayoutPath))
                {
                    layoutSerializer.Deserialize(reader);
                }
            }
            LoadAndRegisterHighlighting(StyleNames.Assembly, @"TVCStudio.Resources.TVCAssembly.xshd", @".tvcasm");
            LoadAndRegisterHighlighting(StyleNames.Basic, @"TVCStudio.Resources.TVCBasic.xshd", @".tvcbas");
            LoadAndRegisterHighlighting(StyleNames.BasicLoader, @"TVCStudio.Resources.TVCBasic.xshd", @".ldr");
            LoadAndRegisterHighlighting(StyleNames.List, @"TVCStudio.Resources.TVCList.xshd", @".lst");
            DataContext = new MainViewModel();
            OutPut.ToggleAutoHide();
        }

        private void LoadAndRegisterHighlighting(string name, string definitionName, string extension)
        {
            Stream s = typeof(MainWindow).Assembly.GetManifestResourceStream(definitionName);
            if (s != null)
            {
                XmlReader reader = new XmlTextReader(s);

                var customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,
                    HighlightingManager.Instance);

                reader.Close();
                HighlightingManager.Instance.RegisterHighlighting(name, new[] { extension }, customHighlighting);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }

                XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(DockingManager);
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

        private void OutPutText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            OutPutText.ScrollToEnd();
        }
    }
}
