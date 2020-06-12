using System.Collections.Generic;
using TVC.Basic;

namespace TVC.Computer
{
    public class BasicBuilder : ProgramBuilder
    {
        public BasicBuilder(BuilderSettings settings, bool removeSpaces = false) : base(settings)
        {
            m_TokenisedBytes = new List<byte>();
            m_RemoveSpaces = removeSpaces;
        }

        public override void Build()
        {
            if (!OpenInputFile())
            {
                return;
            }

            if (BasicHelper.BasicCodeIsSimplified(ProgramLines))
            {
                // TODO 
                OnBuildMessageSent($"'{BuildSettings.ProgramPath}' egyszerűsített mód.");
                OnBuildMessageSent($"'{BuildSettings.ProgramPath}' cimkék feloldása, és újraszámozás.");
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
                        var basicRow = new TvcBasicRow(programLine, m_RemoveSpaces);
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
        private readonly bool m_RemoveSpaces;
    }
}
