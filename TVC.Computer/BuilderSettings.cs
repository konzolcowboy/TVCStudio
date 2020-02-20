using System;
using System.IO;
using System.Xml.Serialization;

namespace TVC.Computer
{
    [XmlRoot]
    public class BuilderSettings
    {
        public BuilderSettings()
        {
            WavGapLeading = 1000;
            WavFrequencyOffset = 0;
            WavLeadingLength = 4812;
        }

        public BuilderSettings(string programFullPath, bool autoRun = true, bool copyProtected = false) : this()
        {
            ProgramPath = programFullPath;
            CasFilePath = Path.ChangeExtension(ProgramPath, @".cas");
            NativePath = Path.ChangeExtension(ProgramPath, @".obj");
            LstFilePath = Path.ChangeExtension(ProgramPath, @".lst");
            WavFilePath = Path.ChangeExtension(ProgramPath, @".wav");
            LoaderPath = Path.ChangeExtension(ProgramPath, @".ldr");
            AutoRun = autoRun;
            CopyProtected = copyProtected;
        }

        [XmlElement]
        public string ProgramPath
        {
            get; set;
        }

        [XmlElement]
        public string CasFilePath
        {
            get; set;
        }

        [XmlElement]
        public string NativePath
        {
            get; set;
        }

        [XmlElement]
        public string LstFilePath
        {
            get; set;
        }

        [XmlElement]
        public string WavFilePath
        {
            get; set;
        }

        [XmlElement]
        public uint WavGapLeading
        {
            get; set;
        }

        [XmlElement]
        public uint WavFrequencyOffset
        {
            get; set;
        }

        [XmlElement]
        public uint WavLeadingLength
        {
            get; set;
        }

        [XmlElement]
        public string LoaderPath
        {
            get; set;
        }

        [XmlElement]
        public bool AutoRun
        {
            get; set;
        }

        [XmlElement]
        public bool CopyProtected
        {
            get; set;
        }

        [XmlElement]
        public bool GenerateLoader
        {
            get; set;
        }

        [XmlElement]
        public bool GenerateListFile
        {
            get; set;
        }

        [XmlElement]
        public bool GenerateWavFile
        {
            get; set;
        }

        [XmlElement]
        public bool GenerateCasFile
        {
            get; set;
        }

        [XmlElement]
        public bool GenerateNativeFile
        {
            get; set;
        }

        [XmlElement]
        public ushort? StartAddress
        {
            get; set;
        }

        public static BuilderSettings DeSerialize(string programFullPath)
        {
            try
            {
                var fileName = programFullPath.GetHashCode().ToString();
                var settingsPath = Path.Combine(SettingsDirectory, fileName + @".buildsettings");
                if (!File.Exists(settingsPath))
                {
                    return null;
                }

                FileStream fs = new FileStream(settingsPath, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(BuilderSettings));
                return (BuilderSettings)xs.Deserialize(fs);
            }

            catch (Exception)
            {
                return null;
            }
        }


        public static void Serialize(BuilderSettings settings)
        {
            try
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }

                var fileName = settings.ProgramPath.GetHashCode().ToString();
                var settingsPath = Path.Combine(SettingsDirectory, fileName + @".buildsettings");
                FileStream fs = new FileStream(settingsPath, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(BuilderSettings));
                xs.Serialize(fs, settings, null);

            }

            catch (Exception)
            {
                // ignored
            }
        }


        private static readonly string SettingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"TVCStudio", @"BuilderSettings");

    }
}
