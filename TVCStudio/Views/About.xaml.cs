using System.Reflection;
using System.Windows;
using TVC.Basic;
using TVCTape;
using Z80.Kernel.Z80Assembler;

namespace TVCStudio.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About
    {
        public About()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LblTvcStudio.Content = $"TVCStudio.exe\t{Assembly.GetExecutingAssembly().GetName().Version}";

            var basicAssemblyName = Assembly.GetAssembly(typeof(TvcBasicLoader)).GetName();
            LblBasic.Content = $"{basicAssemblyName.Name}.dll\t{basicAssemblyName.Version}";

            var kernelAssemblyName = Assembly.GetAssembly(typeof(Z80Assembler)).GetName();
            LblKernel.Content = $"{kernelAssemblyName.Name}.dll\t{kernelAssemblyName.Version}";

            var tapeAssemblyName = Assembly.GetAssembly(typeof(TvcTape)).GetName();
            LblTape.Content = $"{tapeAssemblyName.Name}.dll\t{tapeAssemblyName.Version}";
        }
    }
}
