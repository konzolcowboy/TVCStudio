using System.Collections.Generic;
using TVC.Basic;

namespace TVC.Computer
{
    public class BasicBuilder : ProgramBuilder
    {
        public BasicBuilder(BuilderSettings settings) : base(settings)
        {
            m_TokenisedBytes = new List<byte>();
        }

        public override void Build()
        {
            if (!OpenInputFile())
            {
                return;
            }

            OnBuildMessageSent($"'{BuildSettings.ProgramPath}' tokenizálása.");
            if (!TokeniseBasicRows())
            {
                return;
            }

            if (BuildSettings.GenerateCasFile)
            {
                OnBuildMessageSent($"CAS file létrehozása ({BuildSettings.CasFilePath})...");
                if (!CreateCasFile(m_TokenisedBytes.ToArray(), CasFileContent.BasicProgram))
                {
                    return;
                }
            }

            if (BuildSettings.GenerateWavFile)
            {
                OnBuildMessageSent($"WAV file létrehozása ({BuildSettings.WavFilePath})...");
                CreateWavFile(m_TokenisedBytes.ToArray());
            }
        }

        private bool TokeniseBasicRows()
        {
            try
            {
                foreach (string programLine in ProgramLines)
                {
                    if (!string.IsNullOrEmpty(programLine))
                    {
                        var basicRow = new TvcBasicRow(programLine);
                        m_TokenisedBytes.AddRange(basicRow.TokenizedBytes);
                    }
                }

                m_TokenisedBytes.Add(0x00);
                return true;
            }
            catch (TvcBasicException e)
            {
                OnBuildMessageSent($"'{BuildSettings.ProgramPath}' tokenizálási hiba:{e.Message}");
                return false;
            }
        }

        private readonly List<byte> m_TokenisedBytes;
    }
}
