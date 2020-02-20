using System;
using System.IO;
using TVC.Computer;

namespace TVCC
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                CommandLine.Usage();
                return;
            }

            CommandLine cmd = new CommandLine(args);

            if (!File.Exists(cmd.InputFilePath))
            {
                Console.WriteLine(
                    $"A megadott file: {cmd.InputFilePath} nem létezik! Ellenőrizze, hogy helyesen adta-e meg!");
                return;
            }
            if (cmd.InputType == null || cmd.InputType.ToUpper() != "BAS" && cmd.InputType.ToUpper() != "Z80")
            {
                Console.WriteLine(
                    $"A megadott bemeneti file típus: {cmd.InputType} érvénytelen! A típus csak 'Z80', vagy 'bas' lehet!");
                return;
            }

            ProgramBuilder builder = CreateBuilder(cmd);

            if (builder == null)
            {
                return;
            }

            builder.BuildMessageSent += Builder_BuildMessageSent;
            builder.Build();
            builder.BuildMessageSent -= Builder_BuildMessageSent;
        }

        private static void Builder_BuildMessageSent(object sender, BuildEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static ProgramBuilder CreateBuilder(CommandLine cmd)
        {
            ushort? startAddress = null;
            if (!string.IsNullOrEmpty(cmd.StartAddress))
            {
                if (!ushort.TryParse(cmd.StartAddress, out var value))
                {
                    Console.WriteLine($"Az indítási cím hibásan lett megadva:{cmd.StartAddress}");
                    return null;
                }

                startAddress = value;
            }

            BuilderSettings settings = new BuilderSettings(cmd.InputFilePath)
            {
                AutoRun = cmd.AutoStart,
                GenerateLoader = cmd.WithBasicLoader,
                GenerateListFile = cmd.GenerateListFile,
                GenerateCasFile = cmd.GenerateCasFile,
                GenerateWavFile = cmd.GenerateWavFile,
                GenerateNativeFile = cmd.GenerateNativeFile,
                StartAddress = startAddress
            };

            switch (cmd.InputType.ToUpper())
            {
                case "BAS":
                    return new BasicBuilder(settings);
                case "Z80":
                    return new AssemblyBuilder(settings);
                default: return null;
            }
        }
    }
}
