using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.AvalonDock.Themes;

namespace TVCStudio.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static readonly Dictionary<string, Theme> Themes = new Dictionary<string, Theme>
        {
            {ThemeNames.Aero, new AeroTheme()},
            {ThemeNames.Metro, new MetroTheme()},
            {ThemeNames.Generic, new GenericTheme()},
            {ThemeNames.Vs2010,new VS2010Theme() }
        };

    }
}
