using System.Collections.Generic;

namespace TVCStudio.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public IList<MenuItemViewModel> Items { get; }

        public MenuViewModel()
        {

            var newMenuViewModel = new MenuItemViewModel(Operations.New, @"Új", new RelayCommand(null));
            newMenuViewModel.Items.Add(new MenuItemViewModel(Operations.NewProject, @"Project", new RelayCommand(null)));
            newMenuViewModel.Items.Add(new MenuItemViewModel(Operations.NewFile, @"File", new RelayCommand(null)));

            var openMenuViewModel = new MenuItemViewModel(Operations.Open, @"Megnyitás", new RelayCommand(null));
            openMenuViewModel.Items.Add(new MenuItemViewModel(Operations.OpenProject, @"Project", new RelayCommand(null)));
            openMenuViewModel.Items.Add(new MenuItemViewModel(Operations.OpenFile, @"File", new RelayCommand(null)));

            var saveMenuViewModel = new MenuItemViewModel(Operations.Save, @"Mentés", new RelayCommand(null));
            var projectSaveMenuViewModel = new MenuItemViewModel(Operations.SaveProject, @"Project mentése", new RelayCommand(null));

            var closeMenuViewModel = new MenuItemViewModel(Operations.Close, @"Bezárás", new RelayCommand(null));
            var projectCloseMenuViewModel = new MenuItemViewModel(Operations.SaveProject, @"Project bezárása", new RelayCommand(null));

            var exitMenuViewModel = new MenuItemViewModel(Operations.Exit, @"Kilépés", new RelayCommand(null));

            var fileMenuViewModel = new MenuItemViewModel(Operations.File, @"File", new RelayCommand(null));

            fileMenuViewModel.Items.Add(newMenuViewModel);
            fileMenuViewModel.Items.Add(openMenuViewModel);
            fileMenuViewModel.Items.Add(new MenuItemViewModel(Operations.Separator, string.Empty, new RelayCommand(null), true));
            fileMenuViewModel.Items.Add(saveMenuViewModel);
            fileMenuViewModel.Items.Add(projectSaveMenuViewModel);
            fileMenuViewModel.Items.Add(new MenuItemViewModel(Operations.Separator, string.Empty, new RelayCommand(null), true));
            fileMenuViewModel.Items.Add(closeMenuViewModel);
            fileMenuViewModel.Items.Add(projectCloseMenuViewModel);
            fileMenuViewModel.Items.Add(new MenuItemViewModel(Operations.Separator, string.Empty, new RelayCommand(null), true));
            fileMenuViewModel.Items.Add(exitMenuViewModel);

            var editMenuItem = new MenuItemViewModel(Operations.Edit, @"Szerkesztés", new RelayCommand(null));
            var viewMenuItem = new MenuItemViewModel(Operations.View, @"Nézet", new RelayCommand(null));

            Items = new List<MenuItemViewModel> { fileMenuViewModel, editMenuItem, viewMenuItem };

        }
    }
}
