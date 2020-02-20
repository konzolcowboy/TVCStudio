using System.Windows.Input;
using TVCStudio.Settings;
using TVCStudio.ViewModels.Program;

namespace TVCStudio.ViewModels.Document
{
    internal sealed class ReadOnlyDocumentViewModel : DocumentViewModel
    {
        public ReadOnlyDocumentViewModel(string programFullPath, TvcStudioSettings settings) : base(programFullPath, settings)
        {
            IsReadOny = true;
            CollapseAllCommand = new RelayCommand(o => { }, o => false);
            ExpandAllCommand = new RelayCommand(o => { }, o => false);
            Program = null;
        }

        public override ProgramViewModel Program { get; }
        public override void OnSettingsChanged()
        {
        }

        public override void FormatCode()
        {
            
        }

        protected override void TextAreaInitialized()
        {
        }

        protected override void OnDocumentActivated()
        {
        }

        protected override void OnDocumentDeactivated()
        {
        }

        protected override void OnDocumentTextEntering(string enteredText)
        {
        }

        protected override void OnDocumentKeyDown(KeyEventArgs e)
        {
        }

        protected override void OnTextAreaUninitialized()
        {
        }
    }
}
