using System;
using System.Reflection;
using System.Text;

namespace TVCC
{
    internal class CommandLine
    {
        public CommandLine(string[] args)
        {
            m_Args = args;
            GenerateListFile = false;
            GenerateCasFile = false;
            GenerateWavFile = false;
            GenerateNativeFile = false;
            Parse();
        }

        private readonly string[] m_Args;

        public string InputFilePath { get; private set; }
        public string InputType { get; private set; }
        public string StartAddress { get; private set; }

        public bool WithBasicLoader { get; private set; }
        public bool AutoStart { get; private set; }
        public bool GenerateListFile { get; private set; }
        public bool GenerateCasFile { get; private set; }
        public bool GenerateWavFile { get; private set; }

        public bool GenerateNativeFile { get; private set; }

        public static void Usage()
        {
            var sbBuilder = new StringBuilder();

            sbBuilder.AppendLine($"TVCC.exe [Verzió {Assembly.GetExecutingAssembly().GetName().Version}]");
            sbBuilder.AppendLine(@"Készítette: Oravecz István (konzolcowboy)");
            sbBuilder.AppendLine(@"Parancssoros fordító program a Videoton TV computerhez.A program képes a TV computerre írt Z80 assembly, vagy basic file-ok lefordítására.");
            sbBuilder.AppendLine(@"A program használata:");
            sbBuilder.AppendLine(@"TVCC.exe -i bemeneti file -t=Z80,bas [-a] [-bl] [-sa=999999] [-o=cas,wav,lst,obj]");
            sbBuilder.AppendLine(@"");
            sbBuilder.AppendLine(@"Kapcsolók jelentése:");
            sbBuilder.AppendLine("-i:            A bemeneti szöveg file útvonallal együtt. Ha a file elérési útvonalában szóköz is van (Program Files) akkor az útvonalat idézőjelbe kell tenni!");
            sbBuilder.AppendLine("-t=Z80,bas:    A bemeneti file típusa. Értékei: Z80 (assembly program), vagy bas(basic program)");
            sbBuilder.AppendLine("-a:            Az autostart flag-et állítja igazra a cas és wav file-okban. (Opcionális)");
            sbBuilder.AppendLine("-bl:           A megadott paraméterrel a fordító basic betöltőt generál az assembly programhoz(csak a -t=Z80 kapcsolóval együtt használatos), ha ez a paraméter nincs megadva, csak a lefordított gépi kód kerül a kimeneti file-ba.");
            sbBuilder.AppendLine("-sa=999999:    (Csak a -t=Z80 és -bl kapcsolókkal együtt). Több 'ORG' utasítás esetén ezzel a kapcsolóval tudjuk megadni a program indítási címét(tizes számrendszerben) a basic loader számára. Az értéke 0-65535 között lehet. Ha nem adjuk meg, akkor az első ORG utasítás jelenti az indítási címet.");
            sbBuilder.AppendLine("-o=cas,wav,lst,obj: A kapcsoló használatával a kimeneti file adható meg. (lst:lista file, obj:nativ gépi kód, cas: cas file, wav: wav file). Ha nincs megadva, akkor a cas és wav file-ok automatikusan elkészülnek.");
            sbBuilder.AppendLine("");
            sbBuilder.AppendLine("Használati példák:");
            sbBuilder.AppendLine("Basic program automatikus indítással              :TVCC.exe -i c:\\programok\\programom.bas -t=bas -a");
            sbBuilder.AppendLine("Assembly program BASIC betöltővel                 :TVCC.exe -i c:\\programok\\programom.asm -t=Z80 -a -bl");
            sbBuilder.AppendLine("Assembly program BASIC betöltővel a 12456-os címen:TVCC.exe -i c:\\programok\\programom.asm -t=Z80 -a -bl -sa=12456");
            sbBuilder.AppendLine("Assembly program betöltő nélkül                   :TVCC.exe -i c:\\programok\\programom.asm -t=Z80");
            sbBuilder.AppendLine("Assembly lista file készítése                     :TVCC.exe -i c:\\programok\\programom.asm -t=Z80 -o=lst");
            sbBuilder.AppendLine("Assembly cas file készítése                     :TVCC.exe -i c:\\programok\\programom.asm -t=Z80 -o=cas");
            sbBuilder.AppendLine("Assembly wav file készítése                     :TVCC.exe -i c:\\programok\\programom.asm -t=Z80 -o=wav");
            sbBuilder.AppendLine("Assembly cas és wav file készítése              :TVCC.exe -i c:\\programok\\programom.asm -t=Z80");

            Console.WriteLine(sbBuilder.ToString());
        }

        private void Parse()
        {
            for (int i = 0; i < m_Args.Length; i++)
            {
                switch (m_Args[i].ToUpper())
                {
                    case "-I":
                        InputFilePath = m_Args[++i];
                        continue;
                    case "-A":
                        AutoStart = true;
                        continue;
                    case "-BL":
                        WithBasicLoader = true;
                        continue;
                }

                if (m_Args[i].ToUpper().StartsWith("-O"))
                {
                    string[] sOutArgs = m_Args[i].ToUpper().Split('=');
                    if (sOutArgs.Length == 2)
                    {
                        string[] outputFiles = sOutArgs[1].ToUpper().Split(',');
                        foreach (string outputFile in outputFiles)
                        {
                            switch (outputFile)
                            {
                                case "OBJ":
                                    GenerateNativeFile = true;
                                    continue;
                                case "CAS":
                                    GenerateCasFile = true;
                                    continue;
                                case "WAV":
                                    GenerateWavFile = true;
                                    continue;
                                case "LST":
                                    GenerateListFile = true;
                                    continue;
                            }
                        }
                    }
                }

                if (m_Args[i].ToUpper().StartsWith("-T"))
                {
                    var sType = m_Args[i].ToUpper().Split('=');
                    if (sType.Length == 2)
                    {
                        InputType = sType[1];
                    }
                }

                if (m_Args[i].ToUpper().StartsWith("-SA"))
                {
                    var sType = m_Args[i].ToUpper().Split('=');
                    if (sType.Length == 2)
                    {
                        StartAddress = sType[1];
                    }
                }
            }

            if (!GenerateCasFile && !GenerateNativeFile && !GenerateWavFile && !GenerateListFile)
            {
                GenerateWavFile = true;
                GenerateCasFile = true;
            }
        }
    }
}
