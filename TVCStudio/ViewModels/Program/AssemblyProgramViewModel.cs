using System.IO;
using TVC.Computer;
using TVCStudio.Settings;
using TVCStudio.Trace;
using TVCStudio.ViewModels.Document;

namespace TVCStudio.ViewModels.Program
{
    internal sealed class AssemblyProgramViewModel : ProgramViewModel
    {
        public AssemblyProgramViewModel(string fullPath, DocumentHandler documentHandler, TvcStudioSettings settings) : base(fullPath, documentHandler, settings)
        {
            ProgramType = ProgramType.Assembly;
            OpenListFileCommand = new RelayCommand(OpenListFile);
            OpenLoaderFileCommand = new RelayCommand(OpenLoaderFile);
            m_Settings = settings;
        }

        public override DocumentViewModel GetDocumentViewModel()
        {
            if (m_DocumentViewModel == null)
            {
                m_DocumentViewModel = new AssemblyDocumentViewModel(ProgramFullPath, this, m_Settings);
                m_DocumentViewModel.DocumentClosedEvent += OnDocumentClosed;
            }

            return m_DocumentViewModel;
        }

        private void OnDocumentClosed(object sender, System.EventArgs e)
        {
            m_DocumentViewModel.DocumentClosedEvent -= OnDocumentClosed;
            m_DocumentViewModel = null;
            ProgramState = ProgramState.Closed; 
        }

        protected override ProgramBuilder CreateProgramBuilder()
        {
            return new AssemblyBuilder(BuildSettings);
        }
        private void OpenListFile(object obj)
        {
            if (!File.Exists(BuildSettings.LstFilePath))
            {
                TraceEngine.TraceError($"A file {BuildSettings.LstFilePath} nem található!");
                return;
            }

            DocumentHandler.OpenReadonlyDocument(BuildSettings.LstFilePath);
        }
        private void OpenLoaderFile(object obj)
        {
            if (!File.Exists(BuildSettings.LoaderPath))
            {
                TraceEngine.TraceError($"A file {BuildSettings.LoaderPath} nem található!");
                return;
            }

            DocumentHandler.OpenReadonlyDocument(BuildSettings.LoaderPath);
        }

        private AssemblyDocumentViewModel m_DocumentViewModel;
        private readonly TvcStudioSettings m_Settings;
    }
}
