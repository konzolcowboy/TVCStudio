using System;
using System.Windows;
using System.Windows.Media;

namespace TVCStudio.SourceCodeHandling
{
    [Flags]
    public enum TextMarkerTypes
    {
        /// <summary>
        /// Use no marker
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Use squiggly underline marker
        /// </summary>
        SquigglyUnderline = 0x001,
        /// <summary>
        /// Normal underline.
        /// </summary>
        NormalUnderline = 0x002,
        /// <summary>
        /// Dotted underline.
        /// </summary>
        DottedUnderline = 0x004,

        /// <summary>
        /// Horizontal line in the scroll bar.
        /// </summary>
        LineInScrollBar = 0x0100,
        /// <summary>
        /// Small triangle in the scroll bar, pointing to the right.
        /// </summary>
        ScrollBarRightTriangle = 0x0400,
        /// <summary>
        /// Small triangle in the scroll bar, pointing to the left.
        /// </summary>
        ScrollBarLeftTriangle = 0x0800,
        /// <summary>
        /// Small circle in the scroll bar.
        /// </summary>
        CircleInScrollBar = 0x1000
    }

    public interface ITextMarker
    {
        /// <summary>
        /// Gets the start offset of the marked text region.
        /// </summary>
        int StartOffset { get; }

        /// <summary>
        /// Gets the end offset of the marked text region.
        /// </summary>
        int EndOffset { get; }

        /// <summary>
        /// Gets the length of the marked region.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Deletes the text marker.
        /// </summary>
        void Delete();

        /// <summary>
        /// Gets whether the text marker was deleted.
        /// </summary>
        bool IsDeleted { get; }

        /// <summary>
        /// Event that occurs when the text marker is deleted.
        /// </summary>
        event EventHandler Deleted;

        /// <summary>
        /// Gets/Sets the background color.
        /// </summary>
        Color? BackgroundColor { get; set; }

        /// <summary>
        /// Gets/Sets the foreground color.
        /// </summary>
        Color? ForegroundColor { get; set; }

        /// <summary>
        /// Gets/Sets the font weight.
        /// </summary>
        FontWeight? FontWeight { get; set; }

        /// <summary>
        /// Gets/Sets the font style.
        /// </summary>
        FontStyle? FontStyle { get; set; }

        /// <summary>
        /// Gets/Sets the type of the marker. Use TextMarkerType.None for normal markers.
        /// </summary>
        TextMarkerTypes MarkerTypes { get; set; }

        /// <summary>
        /// Gets/Sets the color of the marker.
        /// </summary>
        Color MarkerColor { get; set; }

        /// <summary>
        /// Gets/Sets the message of this marker
        /// </summary>
        string Message { get; }
    }
}
