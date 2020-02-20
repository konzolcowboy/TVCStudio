using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using TVCStudio.Settings;
using TVCStudio.Trace;
using TVCStudio.ViewModels.Document;
using TVCStudio.ViewModels.Program;
using TVCStudio.Views;
using Xceed.Wpf.AvalonDock.Themes;

namespace TVCStudio.ViewModels
{
    internal static class ThemeNames
    {
        public const string Aero = @"Aero";
        public const string Metro = @"Metro";
        public const string Generic = @"Generikus";
        public const string Vs2010 = @"VS2010";
    }

    public sealed class MainViewModel : ViewModelBase, ITraceClient
    {
        public MainViewModel()
        {
            m_OutPut = new StringBuilder();
            CreateProgramCommand = new RelayCommand(CreateProgram);
            OpenProgramCommand = new RelayCommand(OpenProgram);
            SaveProgramCommand = new RelayCommand(SaveProgram, o => ActiveDocument != null && ActiveDocument.IsDirty);
            BuildProgramCommand = new RelayCommand(BuildProgram, o => SelectedProgram != null);
            ApplicationCloseCommand = new RelayCommand(ApplicationClose);
            HelpCommand = new RelayCommand(ShowHelp);
            AboutCommand = new RelayCommand(ShowAbout);
            OpenProgramFromRecentListCommand = new RelayCommand(OpenProgramFromRecentList);
            SettingsCommand = new RelayCommand(OpenSettings);
            FormatCodeCommand = new RelayCommand(FormatCode);
            FindAndReplaceCommand = new RelayCommand(Replace);
            ClearOutPutCommand = new RelayCommand(o =>
            {
                m_OutPut.Clear();
                OnPropertyChanged(nameof(OutPut));
            }, o => m_OutPut.Length > 0);

            QuickSearchCommand = new RelayCommand(o =>
            {
                ActiveDocument?.QuickSearchCommand.Execute(o);
            });

            m_DocumentHandler = DocumentHandler.DeSerialize() ?? new DocumentHandler();
            m_DocumentHandler.DocumentOpened += DocumentOpened;
            SelectedTheme = Themes[m_DocumentHandler.TvcStudioSettings.SelectedTheme];
            TraceEngine.Subscribe(this);
        }

        public string Title => $"TVCStudio {Assembly.GetExecutingAssembly().GetName().Version}";

        public ObservableCollection<DocumentViewModel> OpenedPrograms => m_DocumentHandler.OpenedDocuments;

        public ObservableCollection<ProgramViewModel> RecentPrograms => m_DocumentHandler.RecentPrograms;

        public DocumentViewModel ActiveDocument
        {
            get => m_ActiveDocument;
            set
            {
                if (m_ActiveDocument != value)
                {
                    m_ActiveDocument = value;
                    if (m_ActiveDocument?.Program != null)
                    {
                        SelectedProgram = m_ActiveDocument.Program;
                    }

                    OnPropertyChanged(nameof(ActiveDocument));
                    OnPropertyChanged(nameof(SaveCommandHeader));
                }
            }
        }

        public ProgramViewModel SelectedProgram
        {
            get => m_SelectedProgram;

            set
            {
                if (m_SelectedProgram != value)
                {
                    if (m_SelectedProgram != null)
                    {
                        m_SelectedProgram.BeforeBuild -= BeforeProgramBuild;
                    }

                    m_SelectedProgram = value;
                    if (m_SelectedProgram != null)
                    {
                        m_SelectedProgram.BeforeBuild += BeforeProgramBuild;
                    }

                    OnPropertyChanged(nameof(SelectedProgram));
                }
            }
        }

        public Theme SelectedTheme
        {
            get => m_SelectedTheme;

            set
            {
                if (m_SelectedTheme == null || !m_SelectedTheme.Equals(value))
                {
                    m_SelectedTheme = value;
                    OnPropertyChanged(nameof(SelectedTheme));
                }
            }
        }

        public ICommand CreateProgramCommand { get; }

        public ICommand OpenProgramCommand { get; }

        public ICommand SaveProgramCommand { get; }

        public ICommand BuildProgramCommand { get; }

        public ICommand ApplicationCloseCommand { get; }

        public ICommand OpenProgramFromRecentListCommand { get; }

        public ICommand HelpCommand { get; }

        public ICommand AboutCommand { get; }

        public ICommand ClearOutPutCommand { get; }

        public ICommand SettingsCommand { get; }

        public ICommand FormatCodeCommand { get; }

        public ICommand FindAndReplaceCommand { get; }

        public ICommand QuickSearchCommand { get; }

        public double WindowWidth
        {
            get => m_DocumentHandler.TvcStudioSettings.WindowWidth;
            set
            {
                if (Math.Abs(m_DocumentHandler.TvcStudioSettings.WindowWidth - value) > 1.0f)
                {
                    m_DocumentHandler.TvcStudioSettings.WindowWidth = value;
                    OnPropertyChanged(nameof(WindowWidth));
                }
            }
        }

        public double WindowHeight
        {
            get => m_DocumentHandler.TvcStudioSettings.WindowHeight;
            set
            {
                if (Math.Abs(m_DocumentHandler.TvcStudioSettings.WindowHeight - value) > 1.0f)
                {
                    m_DocumentHandler.TvcStudioSettings.WindowHeight = value;
                    OnPropertyChanged(nameof(WindowHeight));
                }
            }
        }

        public double WindowLeft
        {
            get => m_DocumentHandler.TvcStudioSettings.WindowLeft;
            set
            {
                if (Math.Abs(m_DocumentHandler.TvcStudioSettings.WindowLeft - value) > 1.0f)
                {
                    m_DocumentHandler.TvcStudioSettings.WindowLeft = value;
                    OnPropertyChanged(nameof(WindowLeft));
                }
            }
        }

        public double WindowTop
        {
            get => m_DocumentHandler.TvcStudioSettings.WindowTop;
            set
            {
                if (Math.Abs(m_DocumentHandler.TvcStudioSettings.WindowTop - value) > 1.0f)
                {
                    m_DocumentHandler.TvcStudioSettings.WindowTop = value;
                    OnPropertyChanged(nameof(WindowTop));
                }
            }
        }

        public WindowState WindowState
        {
            get => m_DocumentHandler.TvcStudioSettings.WindowState;
            set
            {
                if (m_DocumentHandler.TvcStudioSettings.WindowState != value)
                {
                    m_DocumentHandler.TvcStudioSettings.WindowState = value;
                    OnPropertyChanged(nameof(WindowState));
                }
            }
        }



        public string OutPut => m_OutPut.ToString();
        public string SaveCommandHeader => ActiveDocument == null ? @"Mentés" : $"Mentés '{ActiveDocument.Title}'";

        public void TraceMessageRecieved(TraceMessage newMessage)
        {
            m_OutPut.Append(newMessage);
            OnPropertyChanged(nameof(OutPut));
        }

        private void FormatCode(object obj)
        {
            ActiveDocument?.FormatCode();
        }

        private void OpenSettings(object param)
        {
            Views.Settings settings = new Views.Settings(m_DocumentHandler.TvcStudioSettings, Themes);

            bool? showDialog = settings.ShowDialog();
            if (showDialog != null && showDialog.Value)
            {
                SelectedTheme = Themes[m_DocumentHandler.TvcStudioSettings.SelectedTheme];
                foreach (DocumentViewModel documentViewModel in m_DocumentHandler.OpenedDocuments)
                {
                    documentViewModel.OnSettingsChanged();
                }
            }
        }

        private void DocumentOpened(object sender, DocumentOpenEventArgs e)
        {
            ActiveDocument = e.OpenedDocument;
        }

        private void CreateProgram(object param)
        {
            var filterString = param.ToString() == @"ASM"
                ? $"TVC assembly program (*{FileExtensions.Assembly})|*{FileExtensions.Assembly}"
                : $"TVC basic program (1.2/2.2)(*{FileExtensions.Basic})|*{FileExtensions.Basic}";

            var dialog = new SaveFileDialog
            {
                Filter = filterString
            };

            if (dialog.ShowDialog() ?? true)
            {
                try
                {
                    ProgramType pType = Path.GetExtension(dialog.FileName) == FileExtensions.Assembly
                        ? ProgramType.Assembly
                        : ProgramType.Basic;

                    var fileStream = File.CreateText(dialog.FileName);
                    fileStream.Write(ProgramViewModel.GetDefaultProgramText(pType, Path.GetFileNameWithoutExtension(dialog.FileName)));
                    fileStream.Close();

                    m_DocumentHandler.OpenProgram(dialog.FileName);
                }

                catch (Exception e)
                {
                    TraceEngine.TraceError($"A file '{dialog.FileName}' létrehozása sikertelen:{e.Message}");
                }
            }
        }

        private void OpenProgram(object param)
        {
            var dialog = new OpenFileDialog
            {
                Filter =
                    $"TVC assembly program (*{FileExtensions.Assembly})|*{FileExtensions.Assembly}|TVC basic program (1.2/2.2)(*{FileExtensions.Basic})|*{FileExtensions.Basic}"
            };

            if (dialog.ShowDialog() ?? true)
            {
                m_DocumentHandler.OpenProgram(dialog.FileName);
            }
        }

        private void SaveProgram(object param)
        {
            ActiveDocument.Save();
        }

        private void BuildProgram(object obj)
        {
            ClearOutPutCommand.Execute(null);
            ActiveDocument.Save();
            SelectedProgram?.Build();
        }

        private void ShowAbout(object obj)
        {
            About aboutdialog = new About();
            aboutdialog.ShowDialog();
        }

        private void ShowHelp(object obj)
        {
            try
            {
                Process p = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = true,
                        FileName = HelpFilePath
                    }
                };
                p.Start();
            }
            catch (Exception)
            {
                TraceEngine.TraceError($"A súgó file:\n{HelpFilePath}\nmegnyitása sikertelen!");
            }
        }


        private void ApplicationClose(object param)
        {
            if (OpenedPrograms.Any(p => p.IsDirty))
            {
                var changedDocuments = OpenedPrograms.Where(p => p.IsDirty).Select(p => p).ToList();

                var message = new StringBuilder();
                message.AppendLine(@"A következő programok tartalma megváltozott:");
                foreach (var documentViewModel in changedDocuments)
                {
                    message.AppendLine(documentViewModel.Title);
                }

                message.AppendLine(@"Kivánja menteni a módosításokat?");

                var result = MessageBox.Show(message.ToString(), @"Kilépés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    changedDocuments.ForEach(p => p.Save());
                }
            }

            DocumentHandler.Serialize(m_DocumentHandler);
            TvcStudioSettings.Serialize(m_DocumentHandler.TvcStudioSettings);
            Application.Current.Shutdown();
        }

        private void BeforeProgramBuild(object sender, EventArgs e)
        {
            if (m_DocumentHandler.TvcStudioSettings.ClearOutputBeforeBuild)
            {
                ClearOutPutCommand.Execute(null);
            }
        }

        private void Replace(object obj)
        {
            if (m_FindAndReplaceDialog == null)
            {
                m_FindAndReplaceDialog = new FindAndReplace(this);
                m_FindAndReplaceDialog.Closed += FindAndReplaceDialogClosed;
                m_FindAndReplaceDialog.Show();
                return;
            }

            m_FindAndReplaceDialog.Activate();
        }

        private void FindAndReplaceDialogClosed(object sender, EventArgs e)
        {
            m_FindAndReplaceDialog.Closed -= FindAndReplaceDialogClosed;
            m_FindAndReplaceDialog = null;
        }

        private void OpenProgramFromRecentList(object param)
        {
            if (SelectedProgram != null)
            {
                if (!File.Exists(SelectedProgram.ProgramFullPath))
                {
                    TraceEngine.TraceError($"A file {SelectedProgram.ProgramFullPath} nem található!");
                    return;
                }
                m_DocumentHandler.OpenProgram(SelectedProgram.ProgramFullPath);
            }
        }

        private string HelpFilePath
        {
            get
            {
                var helpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Help");
                return Path.Combine(helpPath, @"TVCStudio.chm");
            }
        }

        private DocumentViewModel m_ActiveDocument;
        private readonly StringBuilder m_OutPut;
        private readonly DocumentHandler m_DocumentHandler;
        private ProgramViewModel m_SelectedProgram;
        private Theme m_SelectedTheme;
        private FindAndReplace m_FindAndReplaceDialog;
    }
}
