using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using TVCStudio.Settings;
using Xceed.Wpf.AvalonDock.Themes;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace TVCStudio.ViewModels
{
    public class ThemeData
    {
        public string Name { get; set; }

        public Theme Theme { get; set; }
    }

    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(IReadOnlyDictionary<string, Theme> themes, TvcStudioSettings settings)
        {
            Settings = settings;
            ThemeList = new ObservableCollection<ThemeData>(themes.Select(kvp => new ThemeData { Name = kvp.Key, Theme = kvp.Value }));
            Brushes = new List<SolidColorBrush>(GetSolidBrushes());
            ResetSettingsCommand = new RelayCommand(ResetSettings);
            SelectEmulatorCommand = new RelayCommand(SelectEmulator);

            ApplySettings();
        }

        public ObservableCollection<ThemeData> ThemeList { get; }

        public TvcStudioSettings Settings { get; }

        public ThemeData SelectedThemeData
        {
            get => m_SelectedThemeData;
            set
            {
                if (m_SelectedThemeData != value)
                {
                    m_SelectedThemeData = value;
                    Settings.SelectedTheme = m_SelectedThemeData.Name;
                    OnPropertyChanged(nameof(SelectedThemeData));
                }
            }
        }

        public List<SolidColorBrush> Brushes { get; }

        public SolidColorBrush BackgroundColor
        {
            get => m_BackgroundColor;
            set
            {
                if (m_BackgroundColor == null || !m_BackgroundColor.Equals(value))
                {
                    m_BackgroundColor = value;
                    Settings.AssemblyEditorSettings.BackgroundColor.Color = m_BackgroundColor.Color;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }
        public SolidColorBrush ForegroundColor
        {
            get => m_ForegroundColor;
            set
            {
                if (m_ForegroundColor == null || !m_ForegroundColor.Equals(value))
                {
                    m_ForegroundColor = value;
                    Settings.AssemblyEditorSettings.ForegroundColor.Color = m_ForegroundColor.Color;
                    OnPropertyChanged(nameof(ForegroundColor));
                }
            }
        }

        public SolidColorBrush StringColor
        {
            get => m_StringColor;
            set
            {
                if (m_StringColor == null || !m_StringColor.Equals(value))
                {
                    m_StringColor = value;
                    Settings.AssemblyEditorSettings.StringColor.Color = m_StringColor.Color;
                    OnPropertyChanged(nameof(StringColor));
                }
            }
        }

        public SolidColorBrush CommentColor
        {
            get => m_CommentColor;
            set
            {
                if (m_CommentColor == null || !m_CommentColor.Equals(value))
                {
                    m_CommentColor = value;
                    Settings.AssemblyEditorSettings.CommentColor.Color = m_CommentColor.Color;
                    OnPropertyChanged(nameof(CommentColor));
                }
            }
        }

        public SolidColorBrush ProcessorInstruction
        {
            get => m_ProcessorInstruction;
            set
            {
                if (m_ProcessorInstruction == null || !m_ProcessorInstruction.Equals(value))
                {
                    m_ProcessorInstruction = value;
                    Settings.AssemblyEditorSettings.ProcessorInstruction.Color = m_ProcessorInstruction.Color;
                    OnPropertyChanged(nameof(ProcessorInstruction));
                }
            }
        }

        public SolidColorBrush ClockCycleColor
        {
            get => m_ClockCycleColor;
            set
            {
                if (m_ClockCycleColor == null || !m_ClockCycleColor.Equals(value))
                {
                    m_ClockCycleColor = value;
                    Settings.AssemblyEditorSettings.ClockCycleColor.Color = m_ClockCycleColor.Color;
                    OnPropertyChanged(nameof(ClockCycleColor));
                }
            }
        }

        public SolidColorBrush RegisterColor
        {
            get => m_RegisterColor;
            set
            {
                if (m_RegisterColor == null || !m_RegisterColor.Equals(value))
                {
                    m_RegisterColor = value;
                    Settings.AssemblyEditorSettings.RegisterColor.Color = m_RegisterColor.Color;
                    OnPropertyChanged(nameof(RegisterColor));
                }
            }
        }


        public SolidColorBrush AssemblerInstruction
        {
            get => m_AssemblerInstruction;
            set
            {
                if (m_AssemblerInstruction == null || !m_AssemblerInstruction.Equals(value))
                {
                    m_AssemblerInstruction = value;
                    Settings.AssemblyEditorSettings.AssemblerInstruction.Color = m_AssemblerInstruction.Color;
                    OnPropertyChanged(nameof(AssemblerInstruction));
                }
            }
        }

        public SolidColorBrush PreprocessorInstructionColor
        {
            get => m_PreprocessorInstructionColor;
            set
            {
                if (m_PreprocessorInstructionColor == null || !m_PreprocessorInstructionColor.Equals(value))
                {
                    m_PreprocessorInstructionColor = value;
                    Settings.AssemblyEditorSettings.PreprocessorInstructionColor.Color = m_PreprocessorInstructionColor.Color;
                    OnPropertyChanged(nameof(PreprocessorInstructionColor));
                }
            }
        }

        public SolidColorBrush ExpressionColor
        {
            get => m_ExpressionColor;
            set
            {
                if (m_ExpressionColor == null || !m_ExpressionColor.Equals(value))
                {
                    m_ExpressionColor = value;
                    Settings.AssemblyEditorSettings.ExpressionColor.Color = m_ExpressionColor.Color;
                    OnPropertyChanged(nameof(ExpressionColor));
                }
            }
        }

        public SolidColorBrush InactiveCodeColor
        {
            get => m_InactiveCodeColor;
            set
            {
                if (m_InactiveCodeColor == null || !m_InactiveCodeColor.Equals(value))
                {
                    m_InactiveCodeColor = value;
                    Settings.AssemblyEditorSettings.InactiveCodeColor.Color = m_InactiveCodeColor.Color;
                    OnPropertyChanged(nameof(InactiveCodeColor));
                }
            }
        }

        public SolidColorBrush WrongCodeBackgroundColor
        {
            get => m_WrongCodeBackgroundColor;
            set
            {
                if (m_WrongCodeBackgroundColor == null || !m_WrongCodeBackgroundColor.Equals(value))
                {
                    m_WrongCodeBackgroundColor = value;
                    Settings.AssemblyEditorSettings.WrongCodeBackgroundColor.Color = m_WrongCodeBackgroundColor.Color;
                    OnPropertyChanged(nameof(WrongCodeBackgroundColor));
                }
            }
        }

        public bool AutoCodeFormating
        {
            get => Settings.AssemblyEditorSettings.AutoCodeFormating;
            set
            {
                if (Settings.AssemblyEditorSettings.AutoCodeFormating != value)
                {
                    Settings.AssemblyEditorSettings.AutoCodeFormating = value;
                    OnPropertyChanged(nameof(AutoCodeFormating));
                }
            }
        }

        public bool ShowClockCycles
        {
            get => Settings.AssemblyEditorSettings.ShowClockCycles;
            set
            {
                if (Settings.AssemblyEditorSettings.ShowClockCycles != value)
                {
                    Settings.AssemblyEditorSettings.ShowClockCycles = value;
                    OnPropertyChanged(nameof(ShowClockCycles));
                }
            }
        }

        public bool RunAfterSucessfulBuild
        {
            get => Settings.RunAfterSucessfulBuild;
            set
            {
                if (Settings.RunAfterSucessfulBuild != value)
                {
                    Settings.RunAfterSucessfulBuild = value;
                    OnPropertyChanged(nameof(RunAfterSucessfulBuild));
                }
            }
        }

        public bool MarkInactiveCode
        {
            get => Settings.AssemblyEditorSettings.MarkInactiveCode;
            set
            {
                if (Settings.AssemblyEditorSettings.MarkInactiveCode != value)
                {
                    Settings.AssemblyEditorSettings.MarkInactiveCode = value;
                    OnPropertyChanged(nameof(MarkInactiveCode));
                }
            }
        }

        public bool AutoCodeAnalization
        {
            get => Settings.AssemblyEditorSettings.AutoCodeAnalization;
            set
            {
                if (Settings.AssemblyEditorSettings.AutoCodeAnalization != value)
                {
                    Settings.AssemblyEditorSettings.AutoCodeAnalization = value;
                    OnPropertyChanged(nameof(AutoCodeAnalization));
                }
            }
        }

        public bool CodeFolding
        {
            get => Settings.AssemblyEditorSettings.CodeFolding;
            set
            {
                if (Settings.AssemblyEditorSettings.CodeFolding != value)
                {
                    Settings.AssemblyEditorSettings.CodeFolding = value;
                    OnPropertyChanged(nameof(CodeFolding));
                }
            }
        }

        public int PreprocessorIndentSize
        {
            get => Settings.AssemblyIndentationSettings.PreprocessorIndentSize;
            set
            {
                if (Settings.AssemblyIndentationSettings.PreprocessorIndentSize != value)
                {
                    Settings.AssemblyIndentationSettings.PreprocessorIndentSize = value;
                    OnPropertyChanged(nameof(PreprocessorIndentSize));
                }
            }
        }

        public int AssemblyRowIndentSize
        {
            get => Settings.AssemblyIndentationSettings.AssemblyRowIndentSize;
            set
            {
                if (Settings.AssemblyIndentationSettings.AssemblyRowIndentSize != value)
                {
                    Settings.AssemblyIndentationSettings.AssemblyRowIndentSize = value;
                    OnPropertyChanged(nameof(AssemblyRowIndentSize));
                }
            }
        }

        public int LabelSectionPaddingSize
        {
            get => Settings.AssemblyIndentationSettings.LabelSectionPaddingSize;
            set
            {
                if (Settings.AssemblyIndentationSettings.LabelSectionPaddingSize != value)
                {
                    Settings.AssemblyIndentationSettings.LabelSectionPaddingSize = value;
                    OnPropertyChanged(nameof(LabelSectionPaddingSize));
                }
            }
        }

        public int InstructionSectionPaddingSize
        {
            get => Settings.AssemblyIndentationSettings.InstructionSectionPaddingSize;
            set
            {
                if (Settings.AssemblyIndentationSettings.InstructionSectionPaddingSize != value)
                {
                    Settings.AssemblyIndentationSettings.InstructionSectionPaddingSize = value;
                    OnPropertyChanged(nameof(InstructionSectionPaddingSize));
                }
            }
        }

        public int OperandSectionPaddingSize
        {
            get => Settings.AssemblyIndentationSettings.OperandSectionPaddingSize;
            set
            {
                if (Settings.AssemblyIndentationSettings.OperandSectionPaddingSize != value)
                {
                    Settings.AssemblyIndentationSettings.OperandSectionPaddingSize = value;
                    OnPropertyChanged(nameof(OperandSectionPaddingSize));
                }
            }
        }

        public FontFamily EditorFont
        {
            get => m_EditorFontFamily;
            set
            {
                if (m_EditorFontFamily == null || !m_EditorFontFamily.Equals(value))
                {
                    m_EditorFontFamily = value;
                    Settings.AssemblyEditorSettings.EditorFontName = m_EditorFontFamily.Source;
                    OnPropertyChanged(nameof(EditorFont));
                }
            }
        }

        public int EditorFontSize
        {
            get => Settings.AssemblyEditorSettings.EditorFontSize;
            set
            {
                if (Settings.AssemblyEditorSettings.EditorFontSize != value)
                {
                    Settings.AssemblyEditorSettings.EditorFontSize = value;
                    OnPropertyChanged(nameof(EditorFontSize));
                }
            }
        }

        public SolidColorBrush BasicBackgroundColor
        {
            get => m_BasicBackgroundColor;
            set
            {
                if (m_BasicBackgroundColor == null || !m_BasicBackgroundColor.Equals(value))
                {
                    m_BasicBackgroundColor = value;
                    Settings.BasicEditorSettings.BackgroundColor.Color = m_BasicBackgroundColor.Color;
                    OnPropertyChanged(nameof(BasicBackgroundColor));
                }
            }
        }

        public SolidColorBrush BasicForegroundColor
        {
            get => m_BasicForegroundColor;
            set
            {
                if (m_BasicForegroundColor == null || !m_BasicForegroundColor.Equals(value))
                {
                    m_BasicForegroundColor = value;
                    Settings.BasicEditorSettings.ForegroundColor.Color = m_BasicForegroundColor.Color;
                    OnPropertyChanged(nameof(BasicForegroundColor));
                }
            }
        }

        public SolidColorBrush BasicStringColor
        {
            get => m_BasicStringColor;
            set
            {
                if (m_BasicStringColor == null || !m_BasicStringColor.Equals(value))
                {
                    m_BasicStringColor = value;
                    Settings.BasicEditorSettings.StringColor.Color = m_BasicStringColor.Color;
                    OnPropertyChanged(nameof(BasicStringColor));
                }
            }
        }

        public SolidColorBrush BasicKeywordColor
        {
            get => m_BasicKeywordColor;
            set
            {
                if (m_BasicKeywordColor == null || !m_BasicKeywordColor.Equals(value))
                {
                    m_BasicKeywordColor = value;
                    Settings.BasicEditorSettings.KeywordColor.Color = m_BasicKeywordColor.Color;
                    OnPropertyChanged(nameof(BasicKeywordColor));
                }
            }
        }

        public SolidColorBrush BasicNumberColor
        {
            get => m_BasicNumberColor;
            set
            {
                if (m_BasicNumberColor == null || !m_BasicNumberColor.Equals(value))
                {
                    m_BasicNumberColor = value;
                    Settings.BasicEditorSettings.NumberColor.Color = m_BasicNumberColor.Color;
                    OnPropertyChanged(nameof(BasicNumberColor));
                }
            }
        }

        public SolidColorBrush BasicCommentColor
        {
            get => m_BasicCommentColor;
            set
            {
                if (m_BasicCommentColor == null || !m_BasicCommentColor.Equals(value))
                {
                    m_BasicCommentColor = value;
                    Settings.BasicEditorSettings.CommentColor.Color = m_BasicCommentColor.Color;
                    OnPropertyChanged(nameof(BasicCommentColor));
                }
            }
        }

        public SolidColorBrush BasicUserMethodColor
        {
            get => m_BasicUserMethodColor;
            set
            {
                if (m_BasicUserMethodColor == null || !m_BasicUserMethodColor.Equals(value))
                {
                    m_BasicUserMethodColor = value;
                    Settings.BasicEditorSettings.UserMethodColor.Color = m_BasicUserMethodColor.Color;
                    OnPropertyChanged(nameof(BasicUserMethodColor));
                }
            }
        }

        public FontFamily BasicEditorFont
        {
            get => m_BasicEditorFontFamily;
            set
            {
                if (m_BasicEditorFontFamily == null || !m_BasicEditorFontFamily.Equals(value))
                {
                    m_BasicEditorFontFamily = value;
                    Settings.BasicEditorSettings.EditorFontName = m_BasicEditorFontFamily.Source;
                    OnPropertyChanged(nameof(BasicEditorFont));
                }
            }
        }

        public int BasicEditorFontSize
        {
            get => Settings.BasicEditorSettings.EditorFontSize;
            set
            {
                if (Settings.BasicEditorSettings.EditorFontSize != value)
                {
                    Settings.BasicEditorSettings.EditorFontSize = value;
                    OnPropertyChanged(nameof(BasicEditorFontSize));
                }
            }
        }

        public string EmulatorPath
        {
            get => Settings.EmulatorPath;
            set
            {
                if (Settings.EmulatorPath != value)
                {
                    Settings.EmulatorPath = value;
                    OnPropertyChanged(nameof(EmulatorPath));
                }
            }
        }

        public string EmulatorArguments
        {
            get => Settings.EmulatorArguments;
            set
            {
                if (Settings.EmulatorArguments != value)
                {
                    Settings.EmulatorArguments = value;
                    OnPropertyChanged(nameof(EmulatorArguments));
                }
            }
        }

        public bool AutoIntellisence
        {
            get => Settings.AutoIntellisence;
            set
            {
                if (Settings.AutoIntellisence != value)
                {
                    Settings.AutoIntellisence = value;
                    OnPropertyChanged(nameof(AutoIntellisence));
                }
            }
        }

        public bool ClearOutputBeforeBuild
        {
            get => Settings.ClearOutputBeforeBuild;
            set
            {
                if (Settings.ClearOutputBeforeBuild != value)
                {
                    Settings.ClearOutputBeforeBuild = value;
                    OnPropertyChanged(nameof(ClearOutputBeforeBuild));
                }
            }
        }

        public ICommand SelectEmulatorCommand { get; }

        public ICommand ResetSettingsCommand { get; }

        private IEnumerable<SolidColorBrush> GetSolidBrushes()
        {
            return typeof(Colors)
                .GetProperties()
                .Where(prop =>
                    typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop => new SolidColorBrush((Color)prop.GetValue(null)));
        }

        private void SelectEmulator(object obj)
        {
            OpenFileDialog openDialog = new OpenFileDialog {Filter = "Futtatható állomány(*.exe)| *.exe"};

            if (openDialog.ShowDialog() ?? true)
            {
                EmulatorPath = openDialog.FileName;
            }

        }

        private void ApplySettings()
        {
            SelectedThemeData = ThemeList.Where(td => td.Name.Equals(Settings.SelectedTheme)).Select(td => td).First();

            BackgroundColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.BackgroundColor.Color)).Select(c => c).First();

            ForegroundColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.ForegroundColor.Color)).Select(c => c).First();

            StringColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.StringColor.Color)).Select(c => c).First();

            CommentColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.CommentColor.Color)).Select(c => c).First();

            ProcessorInstruction = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.ProcessorInstruction.Color)).Select(c => c)
                .First();

            ClockCycleColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.ClockCycleColor.Color)).Select(c => c)
                .First();

            RegisterColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.RegisterColor.Color)).Select(c => c)
                .First();

            AssemblerInstruction = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.AssemblerInstruction.Color)).Select(c => c)
                .First();

            PreprocessorInstructionColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.PreprocessorInstructionColor.Color)).Select(c => c)
                .First();

            ExpressionColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.ExpressionColor.Color)).Select(c => c).First();

            InactiveCodeColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.InactiveCodeColor.Color)).Select(c => c).First();

            WrongCodeBackgroundColor = Brushes
                .Where(b => b.Color.Equals(Settings.AssemblyEditorSettings.WrongCodeBackgroundColor.Color)).Select(c => c).First();

            AutoCodeFormating = Settings.AssemblyEditorSettings.AutoCodeFormating;
            AutoCodeAnalization = Settings.AssemblyEditorSettings.AutoCodeAnalization;
            CodeFolding = Settings.AssemblyEditorSettings.CodeFolding;

            EditorFont = new FontFamily(Settings.AssemblyEditorSettings.EditorFontName);

            BasicBackgroundColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.BackgroundColor.Color))
                .Select(c => c).First();
            BasicForegroundColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.ForegroundColor.Color))
                .Select(c => c).First();
            BasicEditorFont = new FontFamily(Settings.BasicEditorSettings.EditorFontName);
            BasicEditorFontSize = Settings.BasicEditorSettings.EditorFontSize;
            BasicKeywordColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.KeywordColor.Color))
                .Select(c => c).First();
            BasicNumberColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.NumberColor.Color))
                .Select(c => c).First();
            BasicStringColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.StringColor.Color))
                .Select(c => c).First();
            BasicCommentColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.CommentColor.Color))
                .Select(c => c).First();
            BasicUserMethodColor = Brushes.Where(b => b.Color.Equals(Settings.BasicEditorSettings.UserMethodColor.Color))
                .Select(c => c).First();

            PreprocessorIndentSize = Settings.AssemblyIndentationSettings.PreprocessorIndentSize;
            AssemblyRowIndentSize = Settings.AssemblyIndentationSettings.AssemblyRowIndentSize;
            LabelSectionPaddingSize = Settings.AssemblyIndentationSettings.LabelSectionPaddingSize;
            InstructionSectionPaddingSize = Settings.AssemblyIndentationSettings.LabelSectionPaddingSize;
            OperandSectionPaddingSize = Settings.AssemblyIndentationSettings.OperandSectionPaddingSize;
        }

        private void ResetSettings(object obj)
        {
            if (MessageBox.Show(null, "Biztos benne, hogy mindent alapértelmezettre állít?", "Megerősítés", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Settings.Reset();
                ApplySettings();
            }
        }

        private ThemeData m_SelectedThemeData;
        private SolidColorBrush m_BackgroundColor;
        private SolidColorBrush m_ForegroundColor;
        private SolidColorBrush m_StringColor;
        private SolidColorBrush m_CommentColor;
        private SolidColorBrush m_ProcessorInstruction;
        private SolidColorBrush m_AssemblerInstruction;
        private SolidColorBrush m_PreprocessorInstructionColor;
        private SolidColorBrush m_ExpressionColor;
        private SolidColorBrush m_InactiveCodeColor;
        private SolidColorBrush m_WrongCodeBackgroundColor;
        private FontFamily m_EditorFontFamily;
        private SolidColorBrush m_BasicBackgroundColor;
        private SolidColorBrush m_BasicForegroundColor;
        private SolidColorBrush m_BasicStringColor;
        private SolidColorBrush m_BasicKeywordColor;
        private SolidColorBrush m_BasicNumberColor;
        private SolidColorBrush m_BasicCommentColor;
        private SolidColorBrush m_BasicUserMethodColor;
        private FontFamily m_BasicEditorFontFamily;
        private SolidColorBrush m_ClockCycleColor;
        private SolidColorBrush m_RegisterColor;
    }
}
