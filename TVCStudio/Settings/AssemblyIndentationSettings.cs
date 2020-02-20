using System.Xml.Serialization;

namespace TVCStudio.Settings
{
    public class AssemblyIndentationSettings
    {
        public AssemblyIndentationSettings()
        {
            PreprocessorIndentSize = 10;
            AssemblyRowIndentSize = 15;
            LabelSectionPaddingSize = 20;
            InstructionSectionPaddingSize = 5;
            OperandSectionPaddingSize = 30;
        }

        [XmlElement]
        public int PreprocessorIndentSize { get; set; }

        [XmlElement]
        public int AssemblyRowIndentSize { get; set; }

        [XmlElement]
        public int LabelSectionPaddingSize { get; set; }

        [XmlElement]
        public int InstructionSectionPaddingSize { get; set; }

        [XmlElement]
        public int OperandSectionPaddingSize { get; set; }
    }
}
