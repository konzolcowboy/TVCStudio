using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using TVCStudio.Views;

namespace TVCStudio.SourceCodeHandling
{
    public enum InstructionType
    {
        Assembler,
        Processor,
        Basic,
        PreProcessor
    }

    public class CompletionData : ICompletionData
    {
        public CompletionData()
        {
            Text = string.Empty;
            Priority = 0f;
        }

        public CompletionData(string text)
        {
            Text = text;
            Priority = 0f;
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, Text);
        }

        [XmlIgnore]
        public ImageSource Image
        {
            get
            {
                if (m_Image == null)
                {
                    switch (InstructionType)
                    {
                        case InstructionType.Assembler:
                            m_Image = new BitmapImage(new Uri("pack://application:,,,/Views/Images/Assembler.png"));
                            break;
                        case InstructionType.Processor:
                            m_Image = new BitmapImage(new Uri("pack://application:,,,/Views/Images/CPU.png"));
                            break;
                        default:
                            m_Image = new BitmapImage(new Uri("pack://application:,,,/Views/Images/puzzle.png"));
                            break;
                    }
                }
                return m_Image;
            }
        }

        [XmlElement]
        public string Text { get; set; }
        [XmlIgnore]
        public object Content => Text;

        [XmlIgnore]
        public object Description => DescriptionText;
        [XmlIgnore]
        public double Priority { get; }
        [XmlElement]
        public InstructionType InstructionType { get; set; }
        [XmlElement]
        public string DescriptionText { get; set; }

        private ImageSource m_Image;
    }

    [XmlRoot]
    public class CodeIntellisense
    {
        public CodeIntellisense()
        {
            CompletionList = new List<CompletionData>();
        }

        static CodeIntellisense()
        {
            Assembly = Deserialize(@"TVCStudio.Resources.IntellisenseAsm.xml");
            Basic = Deserialize(@"TVCStudio.Resources.IntellisenseBas.xml");
        }

        public void Show(TextArea textArea, List<SymbolData> symbols)
        {
            CompletionWindow = new CompletionWindow(textArea);
            IList<ICompletionData> data = CompletionWindow.CompletionList.CompletionData;
            CompletionList.ForEach(cd => data.Add(cd));
            CompletionWindow.Closed += delegate
            {
                CompletionWindow = null;
            };
            symbols.ForEach(s => data.Add(new CompletionData(s.SymbolName) { DescriptionText = s.SymbolText, InstructionType = InstructionType.Basic }));
            CompletionWindow.Show();
        }

        [XmlIgnore]
        public CompletionWindow CompletionWindow { get; private set; }

        [XmlArray("CompletionList")]
        [XmlArrayItem("CompletionData", typeof(CompletionData))]
        public List<CompletionData> CompletionList { get; set; }

        public static CodeIntellisense Assembly { get; }
        public static CodeIntellisense Basic { get; }

        private static CodeIntellisense Deserialize(string definitionName)
        {
            Stream s = typeof(MainWindow).Assembly.GetManifestResourceStream(definitionName);
            if (s != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CodeIntellisense));
                CodeIntellisense result = (CodeIntellisense)serializer.Deserialize(s);
                return result;
            }

            return new CodeIntellisense();
        }
    }
}
