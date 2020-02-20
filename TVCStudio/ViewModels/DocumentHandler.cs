using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TVCStudio.Settings;
using TVCStudio.Trace;
using TVCStudio.ViewModels.Document;
using TVCStudio.ViewModels.Program;

namespace TVCStudio.ViewModels
{
    public class DocumentOpenEventArgs : EventArgs
    {
        public DocumentViewModel OpenedDocument { get; }

        public DocumentOpenEventArgs(DocumentViewModel openedDocument)
        {
            OpenedDocument = openedDocument;
        }
    }

    public class DocumentHandler : IXmlSerializable
    {
        public DocumentHandler()
        {
            RecentPrograms = new ObservableCollection<ProgramViewModel>();
            OpenedDocuments = new ObservableCollection<DocumentViewModel>();
            TvcStudioSettings = TvcStudioSettings.DeSerialize() ?? new TvcStudioSettings();
        }
        public ObservableCollection<ProgramViewModel> RecentPrograms { get; }

        public ObservableCollection<DocumentViewModel> OpenedDocuments { get; }

        public TvcStudioSettings TvcStudioSettings { get; }

        public event EventHandler<DocumentOpenEventArgs> DocumentOpened;

        public void OpenReadonlyDocument(string fullPath)
        {
            var openedProgram = OpenedDocuments.FirstOrDefault(op => op.ProgramFullPath == fullPath);
            if (openedProgram != null)
            {
                DocumentOpened?.Invoke(this,new DocumentOpenEventArgs(openedProgram));
                return;
            }

            openedProgram = new ReadOnlyDocumentViewModel(fullPath, TvcStudioSettings);
            openedProgram.DocumentClosedEvent += OnDocumentClose;
            OpenedDocuments.Add(openedProgram);
            DocumentOpened?.Invoke(this, new DocumentOpenEventArgs(openedProgram));
        }

        public void OpenProgram(string fullpath)
        {
            var openedProgram = OpenedDocuments.FirstOrDefault(op => op.ProgramFullPath == fullpath);
            if (openedProgram != null)
            {
                DocumentOpened?.Invoke(this, new DocumentOpenEventArgs(openedProgram));
                return;
            }

            var programforOpening =
                RecentPrograms.FirstOrDefault(p => p.ProgramFullPath == fullpath);

            if (programforOpening == null)
            {
                programforOpening = ProgramViewModel.Create(fullpath, this, TvcStudioSettings);
                RecentPrograms.Add(programforOpening);
            }

            if (programforOpening.ProgramState == ProgramState.NotFound)
            {
                TraceEngine.TraceError($"{programforOpening.ProgramFullPath} beolvasása sikertelen, a file nem található!");
                return;
            }

            openedProgram = programforOpening.GetDocumentViewModel();
            openedProgram.DocumentClosedEvent += OnDocumentClose;
            OpenedDocuments.Add(openedProgram);
            programforOpening.ProgramState = ProgramState.Opened;
            DocumentOpened?.Invoke(this, new DocumentOpenEventArgs(openedProgram));
        }

        public static DocumentHandler DeSerialize()
        {
            try
            {
                FileStream fs = new FileStream(SettingsPath, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(DocumentHandler));
                return (DocumentHandler)xs.Deserialize(fs);
            }
            catch (Exception e)
            {
                TraceEngine.TraceError($"{SettingsPath} beolvasása sikertelen:{e.Message}");
                return null;
            }
        }

        public static void Serialize(DocumentHandler settings)
        {
            try
            {
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }

                FileStream fs = new FileStream(SettingsPath, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(DocumentHandler));
                xs.Serialize(fs, settings);

            }
            catch (Exception e)
            {
                TraceEngine.TraceError($"{SettingsPath} kimentése sikertelen:{e.Message}");
            }
        }

        private static string SettingsPath => Path.Combine(SettingsDirectory, XmlFileName);

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.ReadToDescendant(@"RecentPrograms"))
            {
                reader.ReadStartElement();
                while (reader.Name == @"TVCProgram")
                {
                    string fullpath = reader.GetAttribute(@"ProgramPath");
                    string isOpenedString = reader.GetAttribute(@"ProgramIsOpened");
                    if (!string.IsNullOrEmpty(fullpath) && !string.IsNullOrEmpty(isOpenedString))
                    {
                        bool wasOpened = bool.Parse(isOpenedString);

                        if (wasOpened)
                        {
                            OpenProgram(fullpath);
                        }
                        else
                        {
                            ProgramViewModel program = ProgramViewModel.Create(fullpath, this, TvcStudioSettings);
                            RecentPrograms.Add(program);
                        }
                    }
                    reader.ReadStartElement();
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(@"RecentPrograms");
            foreach (ProgramViewModel program in RecentPrograms)
            {
                writer.WriteStartElement(@"TVCProgram");
                writer.WriteAttributeString(@"ProgramPath", program.ProgramFullPath);
                writer.WriteAttributeString(@"ProgramIsOpened", (program.ProgramState == ProgramState.Opened).ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
        private void OnDocumentClose(object sender, EventArgs e)
        {
            if (sender is DocumentViewModel document)
            {
                document.DocumentClosedEvent -= OnDocumentClose;
                OpenedDocuments.Remove(document);
            }
        }

        private static readonly string XmlFileName = $"{nameof(DocumentHandler)}.tvcset";
        private static readonly string SettingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"TVCStudio");
    }
}
