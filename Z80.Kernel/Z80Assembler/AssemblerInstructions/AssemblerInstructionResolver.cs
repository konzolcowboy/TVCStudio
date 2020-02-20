using System.Collections.Generic;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Z80Assembler.AssemblerInstructions
{
    internal abstract class AssemblerInstructionResolver
    {
        protected AssemblerInstructionResolver(IReadOnlyDictionary<string, Symbol> symbolTable)
        {
            SymbolTable = symbolTable;
        }
        public abstract ParseResult Resolve(AssemblyRow row);
        public List<byte> InstructionBytes { get; protected set; }

        public static AssemblerInstructionResolver Create(string assemblerInstruction,
            IReadOnlyDictionary<string, Symbol> symbolTable,
            IReadOnlyList<string> includeDirectories)
        {
            switch (assemblerInstruction)
            {
                case AssemblerConstans.AssemblerInstructions.Ds:
                    return new DsInstructionResolver(symbolTable);
                case AssemblerConstans.AssemblerInstructions.Dw:
                    return new DwInstructionResolver(symbolTable);
                case AssemblerConstans.AssemblerInstructions.Db:
                    return new DbInstructionResolver(symbolTable);
                case AssemblerConstans.AssemblerInstructions.IncBin:
                    return new IncBinInstructionResolver(symbolTable, includeDirectories);
                default: throw new Z80AssemblerException($"Nem támogatott assembler utasítás: {assemblerInstruction}");
            }
        }

        protected IReadOnlyDictionary<string, Symbol> SymbolTable;
    }
}
