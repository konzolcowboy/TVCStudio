using System.Windows.Media;
using System.Xml.Serialization;

namespace TVCStudio.Settings
{
    public class BasicEditorSettings
    {
        public BasicEditorSettings()
        {
            AutoLineNumbering = true;
            StartRowNumber = 10;
            RowNumberIncrement = 10;
            BackgroundColor = new TvcStudioColor(Colors.White);
            ForegroundColor = new TvcStudioColor(Colors.Black);
            KeywordColor = new TvcStudioColor(Colors.Blue);
            StringColor = new TvcStudioColor(Colors.Red);
            NumberColor = new TvcStudioColor(Colors.Magenta);
            CommentColor = new TvcStudioColor(0xff, 0x00, 0x80, 0x00); //green
            UserMethodColor = new TvcStudioColor(0xff, 0x00, 0x80, 0x80); // teal
            EditorFontName = "Consolas";
            EditorFontSize = 18;
        }

        [XmlElement]
        public bool AutoLineNumbering { get; set; }

        [XmlElement]
        public bool RemoveSpacesBeforeBuild { get; set; }

        [XmlElement]
        public int StartRowNumber { get; set; }

        [XmlElement]
        public int RowNumberIncrement { get; set; }

        [XmlElement]
        public TvcStudioColor BackgroundColor { get; set; }

        [XmlElement]
        public TvcStudioColor ForegroundColor { get; set; }

        [XmlElement]
        public TvcStudioColor KeywordColor { get; set; }

        [XmlElement]
        public TvcStudioColor StringColor { get; set; }

        [XmlElement]
        public TvcStudioColor NumberColor { get; set; }

        [XmlElement]
        public string EditorFontName { get; set; }

        [XmlElement]
        public int EditorFontSize { get; set; }

        [XmlElement]
        public TvcStudioColor CommentColor { get; set; }

        [XmlElement]
        public TvcStudioColor UserMethodColor { get; set; }

    }
}
