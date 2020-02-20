using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using TVCStudio.Trace;
using TVCStudio.ViewModels;

namespace TVCStudio.Settings
{
    [XmlRoot]
    public class TvcStudioSettings
    {
        public TvcStudioSettings()
        {
            Reset();
            EmulatorPath = string.Empty;
            EmulatorArguments = string.Empty;
        }

        [XmlElement]
        public string SelectedTheme { get; set; }

        [XmlElement]
        public string EmulatorPath { get; set; }

        [XmlElement]
        public bool RunAfterSucessfulBuild { get; set; }

        [XmlElement]
        public string EmulatorArguments { get; set; }

        [XmlElement]
        public bool AutoIntellisence {get; set; }

        [XmlElement]
        public bool ClearOutputBeforeBuild { get; set; }

        [XmlElement]
        public AssemblyEditorSettings AssemblyEditorSettings { get; set; }

        [XmlElement]
        public BasicEditorSettings BasicEditorSettings { get; set; }

        [XmlElement]
        public AssemblyIndentationSettings AssemblyIndentationSettings { get; set; }

        [XmlIgnore]
        public static string SettingsPath => Path.Combine(SettingsDirectory, XmlFileName);

        [XmlElement]
        public double WindowWidth { get; set; }

        [XmlElement]
        public double WindowHeight{get; set;}

        [XmlElement]
        public WindowState WindowState { get; set; }

        [XmlElement]
        public double WindowLeft { get; set; }

        [XmlElement]
        public double WindowTop { get; set; }

        public static TvcStudioSettings DeSerialize()
        {
            try
            {
                FileStream fs = new FileStream(SettingsPath, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(TvcStudioSettings));
                return (TvcStudioSettings)xs.Deserialize(fs);
            }

            catch (Exception e)
            {
                TraceEngine.TraceError($"{SettingsPath} beolvasása sikertelen:{e.Message}");
                return null;
            }
        }


        public static void Serialize(TvcStudioSettings settings)
        {
            try
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }

                FileStream fs = new FileStream(SettingsPath, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(TvcStudioSettings));
                xs.Serialize(fs, settings, null);

            }

            catch (Exception e)
            {
                TraceEngine.TraceError($"{SettingsPath} kimentése sikertelen:{e.Message}");
            }
        }

        public void Reset()
        {
            AssemblyEditorSettings = new AssemblyEditorSettings();
            AssemblyIndentationSettings = new AssemblyIndentationSettings();
            BasicEditorSettings = new BasicEditorSettings();
            SelectedTheme = ThemeNames.Aero;
            WindowWidth = 768;
            WindowHeight = 1024;
            WindowState = WindowState.Normal;
            WindowLeft = 100;
            WindowTop = 100;
        }

        private static readonly string XmlFileName = $"{nameof(TvcStudioSettings)}.tvcset";
        private static readonly string SettingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"TVCStudio");

    }
}
