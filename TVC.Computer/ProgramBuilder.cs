using System;
using System.Collections.Generic;
using System.IO;
using TVC.Basic;
using TVCTape;

namespace TVC.Computer
{
    public abstract class ProgramBuilder
    {
        protected BuilderSettings BuildSettings { get; }

        public event EventHandler<BuildEventArgs> BuildMessageSent;

        public abstract void Build();

        protected List<string> ProgramLines { get; }


        protected ProgramBuilder(BuilderSettings buildSettings)
        {
            BuildSettings = buildSettings;
            ProgramLines = new List<string>();
        }

        protected bool OpenInputFile()
        {
            try
            {
                FileStream fs = new FileStream(BuildSettings.ProgramPath, FileMode.Open);
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        ProgramLines.Add(sr.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                OnBuildMessageSent($"'{BuildSettings.ProgramPath}' beolvasása sikertelen:{e.Message}");
                return false;
            }

            return true;
        }

        protected bool CreateCasFile(byte[] programbytes, CasFileContent casContent)
        {
            try
            {
                CasFileWriter writer = new CasFileWriter(BuildSettings.CasFilePath, BuildSettings.CopyProtected, CasFileType.ProgramFile, casContent, BuildSettings.AutoRun);
                writer.Write(programbytes);
                return true;
            }
            catch (TvcBasicException e)
            {
                OnBuildMessageSent($"'{BuildSettings.CasFilePath}' létrehozása sikertelen:{e.Message}");
                return false;
            }
        }

        protected void CreateWavFile(byte[] programBytes)
        {
            try
            {
                TvcTape tapeFile = new TvcTape(BuildSettings.CopyProtected, BuildSettings.AutoRun,
                    BuildSettings.WavFilePath, BuildSettings.WavGapLeading,
                    BuildSettings.WavFrequencyOffset, BuildSettings.WavLeadingLength);

                tapeFile.GenerateTvcAudioFile(programBytes);
            }
            catch (Exception e)
            {
                OnBuildMessageSent($"WAV file létrehozása sikertelen:{e.Message}");
            }
        }

        protected void OnBuildMessageSent(string message)
        {
            BuildMessageSent?.Invoke(this, new BuildEventArgs(message));
        }
    }
}
