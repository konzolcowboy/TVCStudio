using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using TVCStudio.Settings;
using Z80.Kernel.Z80Assembler;

namespace TVCStudio.SourceCodeHandling.Assembly
{
    internal class ClockCycleMargin : AbstractMargin, IDisposable
    {
        private double m_EmSize;
        private readonly AssemblyCodeAnalyzer m_Analyzer;
        private readonly TvcStudioSettings m_Settings;
        private Brush m_TextColor;

        public ClockCycleMargin(AssemblyCodeAnalyzer analyzer, TvcStudioSettings settings)
        {
            m_Analyzer = analyzer;
            m_Settings = settings;
            m_Analyzer.AnalizingFinished += AnalizingFinished;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (TextView != null && TextView.VisualLinesValid && m_Settings.AssemblyEditorSettings.ShowClockCycles)
            {
                if (m_Analyzer.InterPretedAssemblyRows != null)
                {
                    var interpretedz80Instructions = m_Analyzer.InterPretedAssemblyRows
                        .Where(ar => ar.Instruction.Type == Z80.Kernel.Z80Assembler.InstructionType.ProcessorInstruction)
                        .Select(ar => ar);
                    m_TextColor = new SolidColorBrush(m_Settings.AssemblyEditorSettings.ClockCycleColor.Color);

                    foreach (AssemblyRow assemblyRow in interpretedz80Instructions)
                    {
                        VisualLine line = TextView.VisualLines.FirstOrDefault(vl =>
                            vl.FirstDocumentLine.LineNumber == assemblyRow.RowNumber);

                        if (line != null)
                        {
                            FormattedText text = new FormattedText($"[{assemblyRow.ClockCycles}]",
                                CultureInfo.CurrentCulture,
                                FlowDirection.LeftToRight,
                                new Typeface("")
                                ,
                                m_EmSize,
                                m_TextColor);
                            double y = line.GetTextLineVisualYPosition(line.TextLines[0], VisualYPosition.LineTop);
                            double x = line.GetTextLineVisualXPosition(line.TextLines[0],
                                line.GetTextLineVisualStartColumn(line.TextLines[0]));
                            drawingContext.DrawText(text, new Point(x, y - TextView.VerticalOffset));
                        }
                    }
                }
            }
        }
        protected override Size MeasureOverride(Size availableSize)

        {
            m_EmSize = (double)GetValue(TextBlock.FontSizeProperty);
            FormattedText text = new FormattedText("[222]",
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("")
                ,
                m_EmSize,
                m_TextColor);

            return new Size(text.Width, 0);
        }
        protected override void OnTextViewChanged(TextView oldTextView, TextView newTextView)
        {
            if (oldTextView != null)
            {
                oldTextView.VisualLinesChanged -= TextViewVisualLinesChanged;
            }

            base.OnTextViewChanged(oldTextView, newTextView);
            if (newTextView != null)
            {
                newTextView.VisualLinesChanged += TextViewVisualLinesChanged;
            }

            InvalidateVisual();
        }

        void TextViewVisualLinesChanged(object sender, EventArgs e)

        {
            InvalidateVisual();
        }

        void AnalizingFinished(object sender, EventArgs e)
        {
            InvalidateVisual();
        }

        public void Dispose()
        {
            m_Analyzer.AnalizingFinished -= AnalizingFinished;
        }
    }
}
