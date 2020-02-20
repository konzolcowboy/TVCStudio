using System.Windows;

namespace TVCStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += AppGlobalDispatcherUnhandledException;
        }

        private void AppGlobalDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Because of Xceed toolkit bug it is necessary
            e.Handled = true;
        }
    }
}
