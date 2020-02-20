namespace Z80.Kernel.Preprocessor
{
    public class PreprocessorConstans
    {
        public class PreprocessorDirectives
        {
            public const string Include = @"INCLUDE";
            public const string Define = @"DEFINE";
            public const string IfDef = @"IFDEF";
            public const string IfnDef = @"IFNDEF";
            public const string Else = @"ELSE";
            public const string EndIf = @"ENDIF";
            public const string Macro = @"MACRO";
            public const string Endm = @"ENDM";
            public const string Undef = @"UNDEF";
        }
    }
}
