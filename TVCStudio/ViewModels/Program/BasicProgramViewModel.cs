using TVC.Computer;
using TVCStudio.Settings;
using TVCStudio.ViewModels.Document;

namespace TVCStudio.ViewModels.Program
{
    internal sealed class BasicProgramViewModel : ProgramViewModel
    {
        public BasicProgramViewModel(string fullPath, DocumentHandler documentHandler, TvcStudioSettings settings) : base(fullPath, documentHandler,settings)
        {
            ProgramType = ProgramType.Basic;
            OpenListFileCommand = new RelayCommand(o => { }, o => false);
            OpenLoaderFileCommand = new RelayCommand(o => { }, o => false);
            m_Settings = settings;
        }

        public override DocumentViewModel GetDocumentViewModel()
        {
            if (m_DocumentViewModel == null)
            {
                m_DocumentViewModel = new BasicDocumentViewModel(ProgramFullPath, this,m_Settings);
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
            return new BasicBuilder(BuildSettings);
        }

        private BasicDocumentViewModel m_DocumentViewModel;
        private readonly TvcStudioSettings m_Settings;
    }
}
