using System.Xml.Serialization;

namespace TVCStudio.Settings
{
    public class AssemblyEditorSettings
    {
        public AssemblyEditorSettings()
        {
            AutoCodeFormating = true;
            AutoCodeAnalization = true;
            CodeFolding = true;
            MarkInactiveCode = true;
            BackgroundColor = new TvcStudioColor(255, 255, 255, 255); // white
            ForegroundColor = new TvcStudioColor(255, 0, 0, 0); //black
            ClockCycleColor = new TvcStudioColor(0xff, 0xff, 0, 0); //red
            CommentColor = new TvcStudioColor(0xff, 0x00, 0x80, 0x00); //green
            StringColor = new TvcStudioColor(0xff, 0xff, 0x00, 0xff); // magenta
            ProcessorInstruction = new TvcStudioColor(0xff, 0x00, 0x80, 0x80); // teal
            RegisterColor = ProcessorInstruction;
            AssemblerInstruction = new TvcStudioColor(0xff, 0x80, 0x80, 0x00); // olive
            PreprocessorInstructionColor = new TvcStudioColor(0xff, 0xff, 0x00, 0x00); // red
            ExpressionColor = new TvcStudioColor(0xff, 0x00, 0x00, 0xff); // blue
            InactiveCodeColor = new TvcStudioColor(0xff, 0xd3, 0xd3, 0xd3); //light gray
            WrongCodeBackgroundColor = new TvcStudioColor(0xff, 0xd3, 0xd3, 0xd3); //light gray
            EditorFontName = "Consolas";
            EditorFontSize = 18;
        }

        [XmlElement]
        public bool MarkInactiveCode
        {
            get; set;
        }

        [XmlElement]
        public bool ShowClockCycles
        {
            get; set;
        }

        [XmlElement]
        public bool AutoCodeFormating
        {
            get; set;
        }

        [XmlElement]
        public bool AutoCodeAnalization
        {
            get; set;
        }

        [XmlElement]
        public bool CodeFolding
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor BackgroundColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor ForegroundColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor StringColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor CommentColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor ProcessorInstruction
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor AssemblerInstruction
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor PreprocessorInstructionColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor ExpressionColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor InactiveCodeColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor ClockCycleColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor RegisterColor
        {
            get; set;
        }

        [XmlElement]
        public TvcStudioColor WrongCodeBackgroundColor
        {
            get; set;
        }

        [XmlElement]
        public string EditorFontName
        {
            get; set;
        }

        [XmlElement]
        public int EditorFontSize
        {
            get; set;
        }
    }
}
