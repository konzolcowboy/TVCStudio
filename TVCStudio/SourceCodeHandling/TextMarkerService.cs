using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;

namespace TVCStudio.SourceCodeHandling
{
    internal sealed class TextMarkerService : DocumentColorizingTransformer, IBackgroundRenderer, ITextMarkerService, ITextViewConnect
    {
        public TextMarkerService(TextDocument document)
        {
            m_Markers = new TextSegmentCollection<TextMarker>(document);
        }

        #region DocumentColorizingTransformer
        protected override void ColorizeLine(DocumentLine line)
        {
            if (m_Markers == null)
            {
                return;
            }

            int lineStart = line.Offset;
            int lineEnd = lineStart + line.Length;
            foreach (TextMarker marker in m_Markers.FindOverlappingSegments(lineStart, line.Length))
            {
                Brush foregroundBrush = null;
                if (marker.ForegroundColor != null)
                {
                    foregroundBrush = new SolidColorBrush(marker.ForegroundColor.Value);
                    foregroundBrush.Freeze();
                }

                ChangeLinePart(
                    Math.Max(marker.StartOffset, lineStart),
                    Math.Min(marker.EndOffset, lineEnd),
                    element =>
                    {
                        if (foregroundBrush != null)
                        {
                            element.TextRunProperties.SetForegroundBrush(foregroundBrush);
                        }

                        Typeface tf = element.TextRunProperties.Typeface;
                        element.TextRunProperties.SetTypeface(new Typeface(
                            tf.FontFamily,
                            marker.FontStyle ?? tf.Style,
                            marker.FontWeight ?? tf.Weight,
                            tf.Stretch
                        ));
                    }
                );
            }
        }

        #endregion

        #region IBackgroundRenderer
        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (m_Markers == null || !textView.VisualLinesValid)
            {
                return;
            }

            var visualLines = textView.VisualLines;
            if (visualLines.Count == 0)
            {
                return;
            }

            int viewStart = visualLines.First().FirstDocumentLine.Offset;
            int viewEnd = visualLines.Last().LastDocumentLine.EndOffset;
            foreach (TextMarker marker in m_Markers.FindOverlappingSegments(viewStart, viewEnd - viewStart))
            {
                if (marker.BackgroundColor != null)
                {
                    BackgroundGeometryBuilder geoBuilder = new BackgroundGeometryBuilder
                    {
                        AlignToWholePixels = true,
                        CornerRadius = 3
                    };
                    geoBuilder.AddSegment(textView, marker);
                    Geometry geometry = geoBuilder.CreateGeometry();
                    if (geometry != null)
                    {
                        Color color = marker.BackgroundColor.Value;
                        SolidColorBrush brush = new SolidColorBrush(color);
                        brush.Freeze();
                        drawingContext.DrawGeometry(brush, null, geometry);
                    }
                }
                var underlineMarkerTypes = TextMarkerTypes.SquigglyUnderline
                    | TextMarkerTypes.NormalUnderline
                    | TextMarkerTypes.DottedUnderline;

                if ((marker.MarkerTypes & underlineMarkerTypes) != 0)
                {
                    foreach (Rect r in BackgroundGeometryBuilder.GetRectsForSegment(textView, marker))
                    {
                        Point startPoint = r.BottomLeft;
                        Point endPoint = r.BottomRight;

                        Brush usedBrush = new SolidColorBrush(marker.MarkerColor);
                        usedBrush.Freeze();
                        if ((marker.MarkerTypes & TextMarkerTypes.SquigglyUnderline) != 0)
                        {
                            double offset = 2.5;

                            int count = Math.Max((int)((endPoint.X - startPoint.X) / offset) + 1, 4);

                            StreamGeometry geometry = new StreamGeometry();

                            using (StreamGeometryContext ctx = geometry.Open())
                            {
                                ctx.BeginFigure(startPoint, false, false);
                                ctx.PolyLineTo(CreatePoints(startPoint, offset, count).ToArray(), true, false);
                            }

                            geometry.Freeze();

                            Pen usedPen = new Pen(usedBrush, 1);
                            usedPen.Freeze();
                            drawingContext.DrawGeometry(Brushes.Transparent, usedPen, geometry);
                        }
                        if ((marker.MarkerTypes & TextMarkerTypes.NormalUnderline) != 0)
                        {
                            Pen usedPen = new Pen(usedBrush, 1);
                            usedPen.Freeze();
                            drawingContext.DrawLine(usedPen, startPoint, endPoint);
                        }
                        if ((marker.MarkerTypes & TextMarkerTypes.DottedUnderline) != 0)
                        {
                            Pen usedPen = new Pen(usedBrush, 1) { DashStyle = DashStyles.Dot };
                            usedPen.Freeze();
                            drawingContext.DrawLine(usedPen, startPoint, endPoint);
                        }
                    }
                }
            }
        }
        private IEnumerable<Point> CreatePoints(Point start, double offset, int count)
        {
            for (int i = 0; i < count; i++)
                yield return new Point(start.X + i * offset, start.Y - ((i + 1) % 2 == 0 ? offset : 0));
        }

        public KnownLayer Layer => KnownLayer.Selection;
        #endregion

        #region ITextMarkerService
        public ITextMarker Create(int startOffset, int length, string message)
        {
            TextMarker m = new TextMarker(this, startOffset, length, message);
            m_Markers.Add(m);
            return m;
        }

        public IEnumerable<ITextMarker> TextMarkers => m_Markers ?? Enumerable.Empty<ITextMarker>();
        public void Remove(ITextMarker marker)
        {
            TextMarker m = marker as TextMarker;
            if (m != null)
            {
                if (m_Markers != null && m_Markers.Remove(m))
                {
                    Redraw(m);
                    m.OnDeleted();
                }
            }
        }

        /// <summary>
        /// Redraws the specified text segment.
        /// </summary>
        internal void Redraw(ISegment segment)
        {
            foreach (var view in m_TextViews)
            {
                view.Redraw(segment);
            }

            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RedrawRequested;


        public void RemoveAll(Predicate<ITextMarker> predicate)
        {
            if (m_Markers != null)
            {
                foreach (TextMarker m in m_Markers.ToArray())
                {
                    if (predicate != null && predicate(m))
                        Remove(m);
                }
            }
        }

        public IEnumerable<ITextMarker> GetMarkersAtOffset(int offset)
        {
            if (m_Markers != null)
            {
                return m_Markers.FindSegmentsContaining(offset);
            }

            return Enumerable.Empty<ITextMarker>();
        }
        #endregion

        #region ITextViewConnect

        void ITextViewConnect.AddToTextView(TextView textView)
        {
            if (textView != null && !m_TextViews.Contains(textView))
            {
                m_TextViews.Add(textView);
            }
        }

        void ITextViewConnect.RemoveFromTextView(TextView textView)
        {
            if (textView != null)
            {
                m_TextViews.Remove(textView);
            }
        }
        #endregion

        private readonly TextSegmentCollection<TextMarker> m_Markers;
        private readonly List<TextView> m_TextViews = new List<TextView>();
    }
}
