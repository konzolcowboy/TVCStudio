using System;
using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;

namespace TVCStudio.SourceCodeHandling
{
    internal sealed class TextMarker : TextSegment, ITextMarker
    {
        public TextMarker(TextMarkerService service, int startOffset, int length, string message)
        {
            m_Service = service;
            StartOffset = startOffset;
            Length = length;
            m_MarkerTypes = TextMarkerTypes.None;
            Message = message;
        }

        public void Delete()
        {
            m_Service.Remove(this);
        }
        internal void OnDeleted()
        {
            Deleted?.Invoke(this, EventArgs.Empty);
        }
        void Redraw()
        {
            m_Service.Redraw(this);
        }

        public bool IsDeleted => !IsConnectedToCollection;

        public event EventHandler Deleted;
        Color? m_BackgroundColor;

        public Color? BackgroundColor
        {
            get => m_BackgroundColor;
            set
            {
                if (m_BackgroundColor != value)
                {
                    m_BackgroundColor = value;
                    Redraw();
                }
            }
        }

        Color? m_ForegroundColor;

        public Color? ForegroundColor
        {
            get => m_ForegroundColor;
            set
            {
                if (m_ForegroundColor != value)
                {
                    m_ForegroundColor = value;
                    Redraw();
                }
            }
        }

        FontWeight? m_FontWeight;

        public FontWeight? FontWeight
        {
            get => m_FontWeight;
            set
            {
                if (m_FontWeight != value)
                {
                    m_FontWeight = value;
                    Redraw();
                }
            }
        }

        FontStyle? m_FontStyle;

        public FontStyle? FontStyle
        {
            get => m_FontStyle;
            set
            {
                if (m_FontStyle != value)
                {
                    m_FontStyle = value;
                    Redraw();
                }
            }
        }

        public string Message { get; }

        TextMarkerTypes m_MarkerTypes;

        public TextMarkerTypes MarkerTypes
        {
            get => m_MarkerTypes;
            set
            {
                if (m_MarkerTypes != value)
                {
                    m_MarkerTypes = value;
                    Redraw();
                }
            }
        }

        Color m_MarkerColor;

        public Color MarkerColor
        {
            get => m_MarkerColor;
            set
            {
                if (m_MarkerColor != value)
                {
                    m_MarkerColor = value;
                    Redraw();
                }
            }
        }

        private readonly TextMarkerService m_Service;
    }
}
