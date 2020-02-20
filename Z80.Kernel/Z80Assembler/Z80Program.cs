using System.Collections.Generic;
using Z80.Kernel.Z80Assembler.AssemblerInstructions;

namespace Z80.Kernel.Z80Assembler
{
    public enum SymbolType
    {
        Address,
        Constant
    }

    public enum SymbolState
    {
        Unresolved,
        Resolved
    }

    public class Symbol
    {
        public string Name { get; set; }

        public SymbolType Type { get; set; }

        public SymbolState State { get; private set; }

        public int LineNumber { get; set; }

        private ushort m_Value;
        public ushort Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                State = SymbolState.Resolved;
            }
        }

        public Symbol()
        {
            Type = SymbolType.Address;
            Name = string.Empty;
            State = SymbolState.Unresolved;
        }
        public override string ToString()
        {
            if (Value <= 0xff)
            {
                byte byteresult = (byte)Value;
                return byteresult.ByteToHexa();
            }

            return Value.UshortToHexa();
        }

    }

    /// <summary>
    /// This class contains the program session data.
    /// A section starts from the 'ORG' assembler instruction until the 'END' instruction, the next 'ORG' isntruction or till the last row of the input file
    /// </summary>
    public class ProgramSection
    {
        public ushort ProgramStartAddress { get; set; }
        public ushort SectionLength { get; set; }
    }

    public class Z80Program
    {
        public Dictionary<string, Symbol> SymbolTable { get; }
        public List<byte> ProgramBytes { get; }
        public List<ProgramSection> ProgramSections { get; }

        public Z80Program()
        {
            SymbolTable = new Dictionary<string, Symbol>
            {
                {
                    AssemblerConstans.LocationCounterSymbol,
                    new Symbol
                    {
                        Name = AssemblerConstans.LocationCounterSymbol,
                        Type = SymbolType.Address,
                    }
                }
            };
            ProgramBytes = new List<byte>();
            ProgramSections = new List<ProgramSection>();
        }
    }
}
