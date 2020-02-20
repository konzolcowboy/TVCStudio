using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TVC.Basic;
using Z80.Kernel.Z80Assembler;

namespace TVC.Computer
{
    public class AssemblyBuilder : ProgramBuilder
    {
        public override void Build()
        {
            if (!OpenInputFile())
            {
                return;
            }

            OnBuildMessageSent(@"Assembly program fordítása...");

            var fileName = Path.GetFileName(BuildSettings.ProgramPath);
            var programDirectory = Path.GetDirectoryName(BuildSettings.ProgramPath);
            Z80Assembler assembler = new Z80Assembler(ProgramLines, new List<string> { programDirectory }, fileName);
            if (!assembler.BuildProgram())
            {
                OnBuildMessageSent(assembler.StatusMessage);
                return;
            }

            float programSizeInKb = (float)assembler.AssembledProgramBytes.Length / 1024;
            OnBuildMessageSent($"Lefordított assembly program mérete: {programSizeInKb:F3} KB ({assembler.AssembledProgramBytes.Length} byte)");

            if (BuildSettings.GenerateNativeFile)
            {
                OnBuildMessageSent($"Nativ tárgykód létrehozása({BuildSettings.NativePath})...");
                if (!CreateNativeFile(assembler))
                {
                    return;
                }
            }

            if (BuildSettings.GenerateListFile)
            {
                OnBuildMessageSent($"Lista file létrehozása({BuildSettings.LstFilePath})...");
                if (!CreateListFile(assembler))
                {
                    return;
                }
            }

            if (!BuildSettings.GenerateLoader)
            {
                if (BuildSettings.GenerateCasFile)
                {
                    OnBuildMessageSent($"CAS file létrehozása ({BuildSettings.CasFilePath})...");
                    if (!CreateCasFile(assembler.AssembledProgramBytes, CasFileContent.NativProgramCode))
                    {
                        OnBuildMessageSent(@"CAS file létrehozása sikertelen!");
                    }
                }

                if (BuildSettings.GenerateWavFile)
                {
                    OnBuildMessageSent($"WAV file létrehozása ({BuildSettings.WavFilePath})...");
                    CreateWavFile(assembler.AssembledProgramBytes);
                }

                return;
            }

            ushort startAddress = assembler.AssembledProgram.ProgramSections[0].ProgramStartAddress;

            if (BuildSettings.StartAddress.HasValue)
            {
                var selectedprogramSection = assembler.AssembledProgram.ProgramSections
                    .Where(ps => ps.ProgramStartAddress == BuildSettings.StartAddress.Value)
                    .Select(ps => ps).FirstOrDefault();

                if (selectedprogramSection == null)
                {
                    OnBuildMessageSent($"A megadott indítási cím:{BuildSettings.StartAddress.Value:D} (${BuildSettings.StartAddress.Value:X4}) érvénytelen!");
                    var addressList = assembler.AssembledProgram.ProgramSections
                        .Select(ps => $"{ps.ProgramStartAddress}(${ps.ProgramStartAddress:x4})").ToList();

                    OnBuildMessageSent($"A lehetséges indítási címek:{string.Join(",", addressList)}");
                    return;
                }

                startAddress = selectedprogramSection.ProgramStartAddress;
            }

            var loader = new TvcBasicLoader(assembler.AssembledProgram, 1, 1, startAddress);
            OnBuildMessageSent(@"Basic loader generálása...");
            OnBuildMessageSent($"Program indítási cím:{startAddress:D} (${startAddress:X4})");

            if (!loader.GenerateBasicLoader())
            {
                OnBuildMessageSent(loader.StatusMessage);
                return;
            }

            if (!CreateLoaderFile(loader))
            {
                return;
            }

            if (BuildSettings.GenerateCasFile)
            {
                OnBuildMessageSent($"CAS file létrehozása basic betöltővel ({BuildSettings.CasFilePath})...");
                if (!CreateCasFile(loader.BasicLoaderProgramBytes.ToArray(), CasFileContent.BasicProgram))
                {
                    OnBuildMessageSent(@"CAS file létrehozása sikertelen!");
                    return;
                }
            }

            if (BuildSettings.GenerateWavFile)
            {
                OnBuildMessageSent($"WAV file létrehozása basic betöltővel ({BuildSettings.WavFilePath})...");
                CreateWavFile(loader.BasicLoaderProgramBytes.ToArray());
            }
        }

        public AssemblyBuilder(BuilderSettings settings) : base(settings)
        {
        }

        private bool CreateListFile(Z80Assembler assembler)
        {
            try
            {
                FileStream fs = new FileStream(BuildSettings.LstFilePath, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (AssemblyRow assemblyRow in assembler.InterPretedAssemblyRows)
                    {
                        sw.Write(assemblyRow.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                OnBuildMessageSent($"'{BuildSettings.LstFilePath}' létrehozása sikertelen:{e.Message}");
                return false;
            }

            return true;
        }

        private bool CreateLoaderFile(TvcBasicLoader loader)
        {
            try
            {
                FileStream fs = new FileStream(BuildSettings.LoaderPath, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(loader.ToString());
                }
            }
            catch (Exception e)
            {
                OnBuildMessageSent($"'{BuildSettings.LstFilePath}' létrehozása sikertelen:{e.Message}");
                return false;
            }

            return true;
        }

        private bool CreateNativeFile(Z80Assembler assembler)
        {
            try
            {
                FileStream fs = new FileStream(BuildSettings.NativePath, FileMode.Create);
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(assembler.AssembledProgramBytes);
                }
            }
            catch (Exception e)
            {
                OnBuildMessageSent($"'{BuildSettings.NativePath}' létrehozása sikertelen:{e.Message}");
                return false;
            }

            return true;
        }
    }
}
