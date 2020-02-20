using System.Windows.Media;
using System.Xml.Serialization;

namespace TVCStudio.Settings
{
    public class TvcStudioColor
    {
        public TvcStudioColor()
        {

        }

        public TvcStudioColor(Color color)
        {
            Color = color;
        }

        public TvcStudioColor(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        [XmlAttribute]
        public byte A { get; set; }

        [XmlAttribute]
        public byte R { get; set; }

        [XmlAttribute]
        public byte G { get; set; }

        [XmlAttribute]
        public byte B { get; set; }

        [XmlIgnore]
        public Color Color
        {
            get => Color.FromArgb(A, R, G, B);
            set
            {
                A = value.A;
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

    }
}
