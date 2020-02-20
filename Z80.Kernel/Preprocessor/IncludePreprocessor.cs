using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Z80.Kernel.Z80Assembler;

namespace Z80.Kernel.Preprocessor
{
    internal class IncludePreprocessor
    {
        public string StatusMessage { get; private set; }

        public List<AssemblyRow> PreprocessedProgramLines { get; }

        public IncludePreprocessor(IReadOnlyList<string> includeDirectories, Dictionary<string, string> includedFiles, string fileName, Dictionary<string, string> defines)
        {
            m_IncludeDirectories = includeDirectories;
            m_IncludedFiles = includedFiles;
            m_FileName = fileName;
            m_Defines = defines;
            PreprocessedProgramLines = new List<AssemblyRow>();
        }

        public bool Preprocess(PreprocessorRowTokenizer tokenizer)
        {
            if (tokenizer.Tokens.Count < 2)
            {
                throw new Z80AssemblerException($"Az {tokenizer.Tokens[0]} utasításnak meg kell adni a file nevét!");
            }

            string fileName = tokenizer.Tokens[1].Replace("\"", "").Replace("'", "");
            List<string> includedProgramLines = new List<string>();

            foreach (string includeDirectory in m_IncludeDirectories)
            {
                var filePath = Path.Combine(includeDirectory, fileName);

                if (File.Exists(filePath))
                {
                    if (m_IncludedFiles.Any(kvp => kvp.Key.Equals(filePath)))
                    {
                        throw new Z80AssemblerException($"Rekurzív file hivatkozás az #INCLUDE utasításban! A file {filePath} már beágyazásra került a(z) {m_IncludedFiles[filePath]} fileban!");
                    }

                    try
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                while (!sr.EndOfStream)
                                {
                                    includedProgramLines.Add(sr.ReadLine());
                                }
                            }
                        }

                        if (includedProgramLines.Count == 0)
                        {
                            throw new Z80AssemblerException($"A hivatkozott file:{filePath} üres!");
                        }

                        Z80Preprocessor preprocessor = new Z80Preprocessor(includedProgramLines, m_IncludeDirectories, filePath, m_Defines);
                        if (!preprocessor.Preprocess())
                        {
                            throw new Z80AssemblerException(preprocessor.StatusMessage);
                        }

                        PreprocessedProgramLines.AddRange(preprocessor.PreprocessedProgramLines);
                        m_IncludedFiles.Add(filePath, m_FileName);
                        return true;
                    }
                    catch (Exception e)
                    {
                        StatusMessage = $"A '{fileName}' file feldolgozásakor a következő hiba lépett fel:{e.Message}";
                        return false;
                    }
                }

            }

            throw new Z80AssemblerException($"Az #INCLUDE utasításban megadott file:{fileName} nem található a megadott útvonalak egyikén sem!");

        }

        private readonly IReadOnlyList<string> m_IncludeDirectories;
        private readonly Dictionary<string, string> m_IncludedFiles;
        private readonly string m_FileName;
        private readonly Dictionary<string, string> m_Defines;

    }
}
