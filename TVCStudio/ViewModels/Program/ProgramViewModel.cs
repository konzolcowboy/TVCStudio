using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Input;
using TVC.Computer;
using TVCStudio.Settings;
using TVCStudio.Trace;
using TVCStudio.ViewModels.Document;

namespace TVCStudio.ViewModels.Program
{
    public enum ProgramType
    {
        Unknown,
        Assembly,
        Basic
    }

    public enum ProgramState
    {
        Closed,
        Opened,
        NotFound
    }


    public abstract class ProgramViewModel : ViewModelBase
    {

        public string ProgramName => Path.GetFileName(ProgramFullPath);

        public string ProgramDirectory => Path.GetDirectoryName(ProgramFullPath);

        public string ProgramFullPath => BuildSettings.ProgramPath;

        public event EventHandler BeforeBuild;

        public ProgramType ProgramType
        {
            get; protected set;
        }

        public ICommand OpenListFileCommand
        {
            get; protected set;
        }

        public ICommand OpenLoaderFileCommand
        {
            get; protected set;
        }

        public ICommand RemoveFromRecentListCommand
        {
            get;
        }

        public ICommand PlayProgramCommand
        {
            get;
        }

        public ProgramState ProgramState
        {
            get; set;
        }

        public ICommand BuildProgramCommand
        {
            get;
        }

        public ICommand RemoveBuiltFiles
        {
            get;
        }

        public ICommand StartInEmulatorCommand
        {
            get;
        }

        public bool WavFileExists => File.Exists(BuildSettings.WavFilePath);

        public bool LstFileExists => File.Exists(BuildSettings.LstFilePath);

        public bool CasFileExists => File.Exists(BuildSettings.CasFilePath);

        public bool LoaderFileExists => File.Exists(BuildSettings.LoaderPath);

        public bool CopyProtected
        {
            get => BuildSettings.CopyProtected;
            set
            {
                if (BuildSettings.CopyProtected != value)
                {
                    BuildSettings.CopyProtected = value;
                    OnPropertyChanged(nameof(CopyProtected));
                }
            }
        }

        public uint WavGapLeading
        {
            get => BuildSettings.WavGapLeading;
            set
            {
                BuildSettings.WavGapLeading = value;
                OnPropertyChanged(nameof(WavGapLeading));
            }
        }

        public uint WavFrequencyOffset
        {
            get => BuildSettings.WavFrequencyOffset;
            set
            {
                BuildSettings.WavFrequencyOffset = value;
                OnPropertyChanged(nameof(WavFrequencyOffset));
            }
        }

        public uint WavLeadingLength
        {
            get => BuildSettings.WavLeadingLength;
            set
            {
                BuildSettings.WavLeadingLength = value;
                OnPropertyChanged(nameof(WavLeadingLength));
            }
        }

        public ushort StartAddress
        {
            get
            {
                if (BuildSettings.StartAddress.HasValue)
                {
                    return BuildSettings.StartAddress.Value;
                }

                return 0;
            }
            set
            {
                BuildSettings.StartAddress = value;
                OnPropertyChanged(nameof(StartAddress));
            }
        }

        public bool AutoRun
        {
            get => BuildSettings.AutoRun;
            set
            {
                if (BuildSettings.AutoRun != value)
                {
                    BuildSettings.AutoRun = value;
                    OnPropertyChanged(nameof(AutoRun));
                }
            }
        }

        public bool GenerateLoader
        {
            get => BuildSettings.GenerateLoader;
            set
            {
                if (BuildSettings.GenerateLoader != value)
                {
                    BuildSettings.GenerateLoader = value;
                    OnPropertyChanged(nameof(GenerateLoader));
                }
            }
        }
        public bool GenerateWavFile
        {
            get => BuildSettings.GenerateWavFile;
            set
            {
                if (BuildSettings.GenerateWavFile != value)
                {
                    BuildSettings.GenerateWavFile = value;
                    OnPropertyChanged(nameof(GenerateWavFile));
                }
            }
        }

        public bool GenerateListFile
        {
            get => BuildSettings.GenerateListFile;
            set
            {
                if (BuildSettings.GenerateListFile != value)
                {
                    BuildSettings.GenerateListFile = value;
                    OnPropertyChanged(nameof(GenerateListFile));
                }
            }
        }

        public bool GenerateCasFile
        {
            get => BuildSettings.GenerateCasFile;
            set
            {
                if (BuildSettings.GenerateCasFile != value)
                {
                    BuildSettings.GenerateCasFile = value;
                    OnPropertyChanged(nameof(GenerateCasFile));
                }
            }
        }

        public bool GenerateNativeFile
        {
            get => BuildSettings.GenerateNativeFile;
            set
            {
                if (BuildSettings.GenerateNativeFile != value)
                {
                    BuildSettings.GenerateNativeFile = value;
                    OnPropertyChanged(nameof(GenerateNativeFile));
                }
            }
        }

        public abstract DocumentViewModel GetDocumentViewModel();

        public void Build()
        {
            BeforeBuild?.Invoke(this, new EventArgs());
            RemoveFiles();
            BuilderSettings.Serialize(BuildSettings);
            ProgramBuilder builder = CreateProgramBuilder();
            builder.BuildMessageSent += Builder_BuildMessageSent;
            builder.Build();
            builder.BuildMessageSent -= Builder_BuildMessageSent;

            NotifyFileChanges();
            if (StudioSettings.RunAfterSucessfulBuild && CasFileExists)
            {
                StartInEmulatorCommand.Execute(null);
            }
        }

        public static string GetDefaultProgramText(ProgramType programtype, string programname)
        {
            StringBuilder result = new StringBuilder();

            if (programtype == ProgramType.Assembly)
            {
                result.AppendLine(@"                 ORG $3000       ;A program kezdőcíme");
                result.AppendLine(string.Empty);
                result.AppendLine(@"                 END             ;A program vége");
            }

            else if (programtype == ProgramType.Basic)
            {
                result.AppendLine(@"10  REM ***********************************");
                result.AppendLine($"20  REM *{programname}");
                result.AppendLine(@"30  REM ***********************************");
                result.AppendLine(@"40  ");
            }

            return result.ToString();
        }

        public static ProgramViewModel Create(string fullPath, DocumentHandler documentHandler, TvcStudioSettings settings)
        {
            var fileExt = Path.GetExtension(fullPath);
            if (fileExt != null)
            {
                fileExt = fileExt.ToLower();
                switch (fileExt)
                {
                    case FileExtensions.Assembly:
                        return new AssemblyProgramViewModel(fullPath, documentHandler, settings);
                    case FileExtensions.Basic:
                        return new BasicProgramViewModel(fullPath, documentHandler, settings);
                }
            }

            return null;
        }

        protected BuilderSettings BuildSettings
        {
            get;
        }

        protected ProgramViewModel(string fullPath, DocumentHandler documentHandler, TvcStudioSettings studioSettings)
        {
            BuildSettings = BuilderSettings.DeSerialize(fullPath) ?? new BuilderSettings(fullPath);
            StudioSettings = studioSettings;
            ProgramState = File.Exists(fullPath) ? ProgramState.Closed : ProgramState.NotFound;
            DocumentHandler = documentHandler;
            RemoveFromRecentListCommand = new RelayCommand(RemoveFromRecentList);
            BuildProgramCommand = new RelayCommand(o => Build());
            RemoveBuiltFiles = new RelayCommand(o => RemoveFiles());
            PlayProgramCommand = new RelayCommand(PlayProgram);
            StartInEmulatorCommand = new RelayCommand(StartInEmulator, o => !string.IsNullOrEmpty(StudioSettings.EmulatorPath));
        }

        protected readonly DocumentHandler DocumentHandler;

        protected abstract ProgramBuilder CreateProgramBuilder();

        private void Builder_BuildMessageSent(object sender, BuildEventArgs e)
        {
            TraceEngine.TraceInfo(e.Message);
        }

        private TvcStudioSettings StudioSettings
        {
            get;
        }

        private void RemoveFromRecentList(object param)
        {
            DocumentViewModel model = GetDocumentViewModel();
            model?.CloseCommand.Execute(null);
            DocumentHandler.RecentPrograms.Remove(this);
        }

        private void PlayProgram(object obj)
        {
            try
            {
                Process p = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = true,
                        FileName = BuildSettings.WavFilePath
                    }
                };
                p.Start();
            }
            catch (Exception)
            {
                TraceEngine.TraceError($"A hang file:{BuildSettings.WavFilePath} lejátszása sikertelen, a file nem található!");
            }
        }

        private void StartInEmulator(object obj)
        {
            try
            {
                Process p = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        FileName = StudioSettings.EmulatorPath,
                        Arguments = StudioSettings.EmulatorArguments.Replace("%CAS%",BuildSettings.CasFilePath)
                    }
                };
                p.Start();
            }
            catch (Exception e)
            {
                TraceEngine.TraceError($"A CAS file:{BuildSettings.CasFilePath} indítása sikertelen!\n{e.Message}");
            }
        }


        private void RemoveFiles()
        {
            try
            {
                if (File.Exists(BuildSettings.CasFilePath))
                {
                    File.Delete(BuildSettings.CasFilePath);
                }

                if (File.Exists(BuildSettings.LstFilePath))
                {
                    File.Delete(BuildSettings.LstFilePath);
                }

                if (File.Exists(BuildSettings.WavFilePath))
                {
                    File.Delete(BuildSettings.WavFilePath);
                }

                if (File.Exists(BuildSettings.NativePath))
                {
                    File.Delete(BuildSettings.NativePath);
                }

                if (File.Exists(BuildSettings.LoaderPath))
                {
                    File.Delete(BuildSettings.LoaderPath);
                }

                NotifyFileChanges();
            }
            catch (Exception e)
            {
                TraceEngine.TraceError(e.Message);
            }
        }

        private void NotifyFileChanges()
        {
            OnPropertyChanged(nameof(CasFileExists));
            OnPropertyChanged(nameof(WavFileExists));
            OnPropertyChanged(nameof(LstFileExists));
            OnPropertyChanged(nameof(LoaderFileExists));
        }
    }
}
