using System.Collections.Generic;
using System.Text;
using Z80.Kernel.Z80Assembler;

namespace TVC.Basic
{
    /// <summary>
    /// This class has the funtionality to create a basic loader for the assembled Z80 processor code in TVC basic syntax.
    /// </summary>
    public class TvcBasicLoader
    {
        /// <summary>
        /// For instanciating of the basic loader the assembbled programbytes must be given
        /// </summary>
        /// <param name="assembledZ80Program"> The assembled Z80 program created by Z80 assembler</param>
        /// <param name="startBasicLineNumber">The start basic line number for the generated basic program.</param>
        /// <param name="lineNumberDifference">The difference between the basic line numbers.</param>
        /// <param name="subRoutineStartAddress">If this parameter is greather than zero the loader will insert the 'S=USR(subRoutineStartAddress)' row into the program.</param>
        public TvcBasicLoader(Z80Program assembledZ80Program, int startBasicLineNumber = 1, int lineNumberDifference = 1,ushort subRoutineStartAddress = 0 )
        {
            m_LineNumberDifference = lineNumberDifference;
            m_AssembledProgram = assembledZ80Program;
            m_StatusMessage = new StringBuilder();
            TvcBasicRows = new List<TvcBasicRow>();
            m_CurrentBasicRowNumber = startBasicLineNumber;
            m_SubRoutineStartAddress = subRoutineStartAddress;
            BasicLoaderProgramBytes = new List<byte>();
        }

        /// <summary>
        /// Generates the basic loader for the assembled program.
        /// A 'TvcBasicException' is thrown if the generating process is failed!
        /// </summary>
        public bool GenerateBasicLoader()
        {
            try
            {
                GenerateBasicDataRows();
                foreach (ProgramSection programSection in m_AssembledProgram.ProgramSections)
                {
                    GenerateBasicRow($"FORI={programSection.ProgramStartAddress:D}TO{programSection.ProgramStartAddress + (programSection.SectionLength - 1):D}:READB:POKE I,B:NEXTI");
                }
                if (m_SubRoutineStartAddress > 0)
                {
                    GenerateBasicRow($"S=USR({m_SubRoutineStartAddress:D})");
                }

                TvcBasicRows.ForEach(bsRow=>BasicLoaderProgramBytes.AddRange(bsRow.TokenizedBytes));
                BasicLoaderProgramBytes.Add(0x00); //The TVC basic program must be closed with zero byte
            }
            catch (TvcBasicException exception)
            {
                m_StatusMessage.AppendLine(exception.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// After creating the basic loader program the generated program rows can be accessed through this property.
        /// </summary>
        public List<TvcBasicRow> TvcBasicRows { get; }

        public List<byte> BasicLoaderProgramBytes { get; }

        public string StatusMessage => m_StatusMessage.ToString();

        public override string ToString()
        {
            var result = new StringBuilder();
            TvcBasicRows.ForEach(s=>result.AppendLine(s.RowText));
            return result.ToString();
        }

        private void GenerateBasicRow(string rowText)
        {
            if (m_CurrentBasicRowNumber > MaxTvcBasicLineNumber)
            {
                throw new TvcBasicException($" A basic sorok száma túllépte a megengedett maximális({MaxTvcBasicLineNumber}) értéket!");
            }

            TvcBasicRows.Add(new TvcBasicRow($"{m_CurrentBasicRowNumber:D}" + rowText));

            m_CurrentBasicRowNumber += m_LineNumberDifference;
        }

        private void GenerateBasicDataRows()
        {
            int currentbyteCount = 1;
            bool newRow = true;
            string rowText = string.Empty;
            foreach (byte programByte in m_AssembledProgram.ProgramBytes)
            {
                if (currentbyteCount == MaxByteNumberInADataBasicRow)
                {
                    currentbyteCount = 1;
                    GenerateBasicRow(rowText);
                    newRow = true;
                }
                if (newRow)
                {
                    rowText = $"DATA {programByte:D}";
                    newRow = false;
                    continue;
                }

                rowText += $",{programByte:D}";

                currentbyteCount++;
            }

            GenerateBasicRow(rowText);
        }
        private readonly Z80Program m_AssembledProgram;
        private readonly int m_LineNumberDifference;
        private readonly ushort m_SubRoutineStartAddress;
        private int m_CurrentBasicRowNumber;
        /// <summary>
        ///  A basic row on the Videoton TVC can contain maximum 250 characters therefore the byte count in a basic 'DATA' row must be maximalized.
        /// </summary>
        private const byte MaxByteNumberInADataBasicRow = 60;
        private const int MaxTvcBasicLineNumber = 9999;
        private readonly StringBuilder m_StatusMessage;

    }
}
