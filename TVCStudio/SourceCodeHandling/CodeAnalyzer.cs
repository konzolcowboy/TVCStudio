using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using TVCStudio.Settings;

namespace TVCStudio.SourceCodeHandling
{
    public class SymbolData
    {
        public string SymbolName { get; set; }

        public string SymbolText { get; set; }

        public int LineNumber { get; set; }

        public string SymbolType { get; set; }
    }

    internal enum AnalizerTrigger
    {
        Manual,
        DocumentChange
    }

    internal class WrongLineData
    {
        public string ErrorText { get; set; }

        public int LineNumber { get; set; }
    }

    internal abstract class CodeAnalyzer : IDisposable
    {
        public List<SymbolData> InterpretedSymbols { get; }

        public AnalizerTrigger Trigger
        {
            get => m_Trigger;
            set
            {
                if (m_Trigger != value)
                {
                    m_Trigger = value;
                    if (m_Trigger == AnalizerTrigger.DocumentChange)
                    {
                        Document.Changed += OnDocumentChanged;
                    }
                    else
                    {
                        Document.Changed -= OnDocumentChanged;
                        MarkerService.RemoveAll(m => true);
                        InterpretedSymbols.Clear();
                    }
                }
            }
        }

        public void Dispose()
        {
            Document.Changed -= OnDocumentChanged;
            Area.TextView.MouseHover -= MouseHover;
            Area.TextView.MouseHoverStopped -= TextEditorMouseHoverStopped;
            Area.TextView.VisualLinesChanged -= VisualLinesChanged;
            IServiceContainer services = (IServiceContainer)Document.ServiceProvider.GetService(typeof(IServiceContainer));
            services?.RemoveService(typeof(ITextMarkerService));
        }

        public abstract void AnalyzeCode();

        protected TextArea Area { get; }

        protected TextDocument Document { get; }

        protected TvcStudioSettings Settings { get; }

        protected ITextMarkerService MarkerService { get; }

        protected void MarkLineAsWrong(DocumentLine line, string message)
        {
            if (line.TotalLength <= 0)
            {
                return;
            }

            ITextMarker marker = MarkerService.Create(line.Offset, line.TotalLength - 1, message);
            marker.MarkerTypes = TextMarkerTypes.CircleInScrollBar | TextMarkerTypes.SquigglyUnderline;
            marker.MarkerColor = Colors.Red;
            marker.BackgroundColor = Settings.AssemblyEditorSettings.WrongCodeBackgroundColor.Color;
        }

        protected void MarkLineAsInactive(DocumentLine line, string message)
        {
            if (line.TotalLength <= 0)
            {
                return;
            }

            ITextMarker marker = MarkerService.Create(line.Offset, line.TotalLength - 1, message);
            marker.MarkerTypes = TextMarkerTypes.None;
            marker.ForegroundColor = Settings.AssemblyEditorSettings.InactiveCodeColor.Color;
        }

        protected CodeAnalyzer(TextArea area, TextDocument document, TvcStudioSettings settings, AnalizerTrigger trigger)
        {
            InterpretedSymbols = new List<SymbolData>();
            Area = area;
            Document = document;
            Settings = settings;
            var textMarkerService = new TextMarkerService(Document);
            Area.TextView.BackgroundRenderers.Add(textMarkerService);
            Area.TextView.LineTransformers.Add(textMarkerService);
            IServiceContainer services = (IServiceContainer)Document.ServiceProvider.GetService(typeof(IServiceContainer));
            services?.AddService(typeof(ITextMarkerService), textMarkerService);
            MarkerService = textMarkerService;
            Trigger = trigger;
            Area.TextView.MouseHover += MouseHover;
            Area.TextView.MouseHoverStopped += TextEditorMouseHoverStopped;
            Area.TextView.VisualLinesChanged += VisualLinesChanged;
            m_AnalizeDelayTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(AnalizeDelayTimeInSec) };
            m_AnalizeDelayTimer.Tick += M_AnalizeDelayTimer_Tick;
        }

        private void M_AnalizeDelayTimer_Tick(object sender, EventArgs e)
        {
            m_AnalizeDelayTimer.Stop();
            AnalyzeCode();
        }

        private void VisualLinesChanged(object sender, EventArgs e)
        {
            if (m_ToolTip != null)
            {
                m_ToolTip.IsOpen = false;
            }
        }

        private void TextEditorMouseHoverStopped(object sender, MouseEventArgs e)
        {
            if (m_ToolTip != null)
            {
                m_ToolTip.IsOpen = false;
                e.Handled = true;
            }
        }

        private void MouseHover(object sender, MouseEventArgs e)
        {
            var pos = Area.TextView.GetPositionFloor(e.GetPosition(Area.TextView) + Area.TextView.ScrollOffset);
            bool inDocument = pos.HasValue;
            if (inDocument)
            {
                TextLocation logicalPosition = pos.Value.Location;
                int offset = Document.GetOffset(logicalPosition);

                var markerWithToolTip = MarkerService.GetMarkersAtOffset(offset).FirstOrDefault();

                if (markerWithToolTip != null)
                {
                    if (m_ToolTip == null)
                    {
                        m_ToolTip = new ToolTip();
                        m_ToolTip.Closed += ToolTipClosed;
                        m_ToolTip.PlacementTarget = Area.TextView;
                        m_ToolTip.Content = new TextBlock
                        {
                            Text = markerWithToolTip.Message,
                            TextWrapping = TextWrapping.Wrap
                        };

                        m_ToolTip.IsOpen = true;
                        e.Handled = true;
                    }
                }
            }
        }

        private void ToolTipClosed(object sender, RoutedEventArgs e)
        {
            m_ToolTip.Closed -= ToolTipClosed;
            m_ToolTip = null;
        }

        private void OnDocumentChanged(object sender, DocumentChangeEventArgs e)
        {
            m_AnalizeDelayTimer.Stop();
            m_AnalizeDelayTimer.Interval = TimeSpan.FromSeconds(AnalizeDelayTimeInSec);
            m_AnalizeDelayTimer.Start();
        }

        private ToolTip m_ToolTip;
        private const double AnalizeDelayTimeInSec = 2.0f;
        private readonly DispatcherTimer m_AnalizeDelayTimer;
        private AnalizerTrigger m_Trigger;
    }
}
