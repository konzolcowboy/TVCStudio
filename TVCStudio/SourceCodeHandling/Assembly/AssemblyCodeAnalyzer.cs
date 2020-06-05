using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using TVCStudio.Settings;
using Z80.Kernel.Z80Assembler;

namespace TVCStudio.SourceCodeHandling.Assembly
{
    internal sealed class AssemblyCodeAnalyzer : CodeAnalyzer
    {
        public AssemblyCodeAnalyzer(TextArea area, TextDocument document, TvcStudioSettings settings,
            AnalizerTrigger trigger, string programFullPath) : base(area, document, settings, trigger)
        {
            m_ProgramFullPath = programFullPath;
            WrongLine = new List<Tuple<string, int>>();
        }

        public List<Tuple<string, int>> WrongLine
        {
            get;
        }

        public List<AssemblyRow> InterPretedAssemblyRows => m_Assembler?.InterPretedAssemblyRows;

        public event EventHandler AnalizingFinished;

        public void ClearResults()
        {
            MarkerService.RemoveAll(m => true);
            InterpretedSymbols.Clear();
            WrongLine.Clear();
        }

        public override void AnalyzeCode()
        {
            ClearResults();

            var programLines = Document.Lines.Select(dl => Document.GetText(dl.Offset, dl.TotalLength)
                .TrimStart(' ', '\t').Replace(Environment.NewLine, "").ToUpper()).ToList();

            var fileName = Path.GetFileName(m_ProgramFullPath);
            var programDirectory = Path.GetDirectoryName(m_ProgramFullPath);
            m_Assembler = new Z80Assembler(programLines, new List<string> { programDirectory }, fileName);

            if (!m_Assembler.BuildProgram())
            {
                MarkLineAsWrong(Document.GetLineByNumber(m_Assembler.WrongLineNumber), m_Assembler.StatusMessage);
                WrongLine.Add(new Tuple<string, int>(m_Assembler.StatusMessage, m_Assembler.WrongLineNumber));
                AnalizingFinished?.Invoke(this, new EventArgs());
                return;
            }

            InterpretedSymbols.AddRange(m_Assembler.AssembledProgram.SymbolTable.Where(sd => sd.Key != @".LC").Select(kvp => new SymbolData
            {
                SymbolName = kvp.Key,
                SymbolText = kvp.Value.ToString(),
                LineNumber = kvp.Value.LineNumber,
                SymbolType = kvp.Value.Type == SymbolType.Address ? @"Cím" : "Konstans"
            }));

            if (Settings.AssemblyEditorSettings.MarkInactiveCode)
            {
                MarkSkippedLines(m_Assembler.SkippedLineNumbers);
            }

            AnalizingFinished?.Invoke(this, new EventArgs());
        }

        private void MarkSkippedLines(IReadOnlyList<int> skippedLineNumbers)
        {
            foreach (int skippedLineNumber in skippedLineNumbers)
            {
                DocumentLine line = Document.GetLineByNumber(skippedLineNumber);
                MarkLineAsInactive(line, @" A sor figyelmen kívül hagyva a fordítási feltétel miatt.");
            }
        }

        private Z80Assembler m_Assembler;
        private readonly string m_ProgramFullPath;
    }
}
