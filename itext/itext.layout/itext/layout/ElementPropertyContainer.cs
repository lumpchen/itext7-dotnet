/*

This file is part of the iText (R) project.
Copyright (c) 1998-2016 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections;
using System.Collections.Generic;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Borders;
using iText.Layout.Hyphenation;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Splitting;

namespace iText.Layout {
    /// <summary>A generic abstract element that fits in a PDF layout object hierarchy.</summary>
    /// <remarks>
    /// A generic abstract element that fits in a PDF layout object hierarchy.
    /// A superclass of all
    /// <see cref="iText.Layout.Element.IElement">layout object</see>
    /// implementations.
    /// </remarks>
    /// 
    public abstract class ElementPropertyContainer<T> : IPropertyContainer
        where T : IPropertyContainer {
        protected internal IDictionary<int, Object> properties = new Dictionary<int, Object>();

        public virtual void SetProperty(int property, Object value) {
            properties[property] = value;
        }

        public virtual bool HasProperty(int property) {
            return HasOwnProperty(property);
        }

        public virtual bool HasOwnProperty(int property) {
            return properties.ContainsKey(property);
        }

        public virtual void DeleteOwnProperty(int property) {
            properties.JRemove(property);
        }

        public virtual T1 GetProperty<T1>(int property) {
            return (T1)this.GetOwnProperty<T1>(property);
        }

        public virtual T1 GetOwnProperty<T1>(int property) {
            return (T1)properties.Get(property);
        }

        public virtual T1 GetDefaultProperty<T1>(int property) {
            switch (property) {
                case Property.MARGIN_TOP:
                case Property.MARGIN_RIGHT:
                case Property.MARGIN_BOTTOM:
                case Property.MARGIN_LEFT:
                case Property.PADDING_TOP:
                case Property.PADDING_RIGHT:
                case Property.PADDING_BOTTOM:
                case Property.PADDING_LEFT: {
                    return (T1)(Object)0f;
                }

                default: {
                    return (T1)(Object)null;
                }
            }
        }

        /// <summary>Gets the width property of the Element.</summary>
        /// <returns>the width of the element, with a value and a measurement unit.</returns>
        /// <seealso cref="iText.Layout.Properties.UnitValue"/>
        public virtual UnitValue GetWidth() {
            return (UnitValue)this.GetProperty<UnitValue>(Property.WIDTH);
        }

        /// <summary>Sets the width property of the Element, measured in points.</summary>
        /// <param name="width">a value measured in points.</param>
        /// <returns>this Element.</returns>
        public virtual T SetWidth(float width) {
            SetProperty(Property.WIDTH, UnitValue.CreatePointValue(width));
            return (T)(Object)this;
        }

        /// <summary>Sets the width property of the Element, measured in percentage.</summary>
        /// <param name="widthPercent">a value measured in percentage.</param>
        /// <returns>this Element.</returns>
        public virtual T SetWidthPercent(float widthPercent) {
            SetProperty(Property.WIDTH, UnitValue.CreatePercentValue(widthPercent));
            return (T)(Object)this;
        }

        /// <summary>
        /// Sets the width property of the Element with a
        /// <see cref="iText.Layout.Properties.UnitValue"/>
        /// .
        /// </summary>
        /// <param name="width">
        /// a
        /// <see cref="iText.Layout.Properties.UnitValue"/>
        /// object
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetWidth(UnitValue width) {
            SetProperty(Property.WIDTH, width);
            return (T)(Object)this;
        }

        /// <summary>Gets the height property of the Element.</summary>
        /// <returns>the height of the element, as a floating point value.</returns>
        public virtual float? GetHeight() {
            return this.GetProperty<float?>(Property.HEIGHT);
        }

        /// <summary>Sets the height property of the Element.</summary>
        /// <param name="height">a floating point value for the new height</param>
        /// <returns>this Element.</returns>
        public virtual T SetHeight(float height) {
            SetProperty(Property.HEIGHT, height);
            return (T)(Object)this;
        }

        /// <summary>Sets values for a relative repositioning of the Element.</summary>
        /// <remarks>
        /// Sets values for a relative repositioning of the Element. Also has as a
        /// side effect that the Element's
        /// <see cref="iText.Layout.Properties.Property.POSITION"/>
        /// is changed to
        /// <see cref="iText.Layout.Layout.LayoutPosition.RELATIVE">relative</see>
        /// .
        /// The default implementation in
        /// <see cref="iText.Layout.Renderer.AbstractRenderer"/>
        /// treats
        /// <code>left</code> and <code>top</code> as the most important values. Only
        /// if <code>left == 0</code> will <code>right</code> be used for the
        /// calculation; ditto for top vs. bottom.
        /// </remarks>
        /// <param name="left">movement to the left</param>
        /// <param name="top">movement upwards on the page</param>
        /// <param name="right">movement to the right</param>
        /// <param name="bottom">movement downwards on the page</param>
        /// <returns>this Element.</returns>
        /// <seealso cref="iText.Layout.Layout.LayoutPosition.RELATIVE"/>
        public virtual T SetRelativePosition(float left, float top, float right, float bottom) {
            SetProperty(Property.POSITION, LayoutPosition.RELATIVE);
            SetProperty(Property.LEFT, left);
            SetProperty(Property.RIGHT, right);
            SetProperty(Property.TOP, top);
            SetProperty(Property.BOTTOM, bottom);
            return (T)(Object)this;
        }

        /// <summary>Sets values for a absolute repositioning of the Element.</summary>
        /// <remarks>
        /// Sets values for a absolute repositioning of the Element. Also has as a
        /// side effect that the Element's
        /// <see cref="iText.Layout.Properties.Property.POSITION"/>
        /// is changed to
        /// <see cref="iText.Layout.Layout.LayoutPosition.FIXED">fixed</see>
        /// .
        /// </remarks>
        /// <param name="x">horizontal position on the page</param>
        /// <param name="y">vertical position on the page</param>
        /// <param name="width">a floating point value measured in points.</param>
        /// <returns>this Element.</returns>
        public virtual T SetFixedPosition(float x, float y, float width) {
            SetFixedPosition(x, y, UnitValue.CreatePointValue(width));
            return (T)(Object)this;
        }

        /// <summary>Sets values for a absolute repositioning of the Element.</summary>
        /// <remarks>
        /// Sets values for a absolute repositioning of the Element. Also has as a
        /// side effect that the Element's
        /// <see cref="iText.Layout.Properties.Property.POSITION"/>
        /// is changed to
        /// <see cref="iText.Layout.Layout.LayoutPosition.FIXED">fixed</see>
        /// .
        /// </remarks>
        /// <param name="x">horizontal position on the page</param>
        /// <param name="y">vertical position on the page</param>
        /// <param name="width">
        /// a
        /// <see cref="iText.Layout.Properties.UnitValue"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetFixedPosition(float x, float y, UnitValue width) {
            SetProperty(Property.POSITION, LayoutPosition.FIXED);
            SetProperty(Property.X, x);
            SetProperty(Property.Y, y);
            SetProperty(Property.WIDTH, width);
            return (T)(Object)this;
        }

        /// <summary>
        /// Sets values for a absolute repositioning of the Element, on a specific
        /// page.
        /// </summary>
        /// <remarks>
        /// Sets values for a absolute repositioning of the Element, on a specific
        /// page. Also has as a side effect that the Element's
        /// <see cref="iText.Layout.Properties.Property.POSITION"/>
        /// is changed to
        /// <see cref="iText.Layout.Layout.LayoutPosition.FIXED">fixed</see>
        /// .
        /// </remarks>
        /// <param name="pageNumber">the page where the element must be positioned</param>
        /// <param name="x">horizontal position on the page</param>
        /// <param name="y">vertical position on the page</param>
        /// <param name="width">a floating point value measured in points.</param>
        /// <returns>this Element.</returns>
        public virtual T SetFixedPosition(int pageNumber, float x, float y, float width) {
            SetFixedPosition(x, y, width);
            SetProperty(Property.PAGE_NUMBER, pageNumber);
            return (T)(Object)this;
        }

        /// <summary>
        /// Sets values for a absolute repositioning of the Element, on a specific
        /// page.
        /// </summary>
        /// <remarks>
        /// Sets values for a absolute repositioning of the Element, on a specific
        /// page. Also has as a side effect that the Element's
        /// <see cref="iText.Layout.Properties.Property.POSITION"/>
        /// is changed to
        /// <see cref="iText.Layout.Layout.LayoutPosition.FIXED">fixed</see>
        /// .
        /// </remarks>
        /// <param name="pageNumber">the page where the element must be positioned</param>
        /// <param name="x">horizontal position on the page</param>
        /// <param name="y">vertical position on the page</param>
        /// <param name="width">a floating point value measured in points.</param>
        /// <returns>this Element.</returns>
        public virtual T SetFixedPosition(int pageNumber, float x, float y, UnitValue width) {
            SetFixedPosition(x, y, width);
            SetProperty(Property.PAGE_NUMBER, pageNumber);
            return (T)(Object)this;
        }

        /// <summary>Sets the horizontal alignment of this Element.</summary>
        /// <param name="horizontalAlignment">
        /// an enum value of type
        /// <see cref="iText.Layout.Properties.HorizontalAlignment?"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetHorizontalAlignment(HorizontalAlignment? horizontalAlignment) {
            SetProperty(Property.HORIZONTAL_ALIGNMENT, horizontalAlignment);
            return (T)(Object)this;
        }

        /// <summary>Sets the font of this Element.</summary>
        /// <param name="font">
        /// a
        /// <see cref="iText.Kernel.Font.PdfFont">font program</see>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetFont(PdfFont font) {
            SetProperty(Property.FONT, font);
            return (T)(Object)this;
        }

        /// <summary>Sets the font color of this Element.</summary>
        /// <param name="fontColor">
        /// a
        /// <see cref="iText.Kernel.Colors.Color"/>
        /// for the text in this Element.
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetFontColor(Color fontColor) {
            return SetFontColor(fontColor, 1f);
        }

        /// <summary>Sets the font color of this Element and the opacity of the text.</summary>
        /// <param name="fontColor">
        /// a
        /// <see cref="iText.Kernel.Colors.Color"/>
        /// for the text in this Element.
        /// </param>
        /// <param name="opacity">an opacity for the text in this Element.</param>
        /// <returns>this Element.</returns>
        public virtual T SetFontColor(Color fontColor, float opacity) {
            SetProperty(Property.FONT_COLOR, fontColor != null ? new TransparentColor(fontColor, opacity) : null);
            return (T)(Object)this;
        }

        /// <summary>Sets the font size of this Element.</summary>
        /// <param name="fontSize">a floating point value</param>
        /// <returns>this Element.</returns>
        public virtual T SetFontSize(float fontSize) {
            SetProperty(Property.FONT_SIZE, fontSize);
            return (T)(Object)this;
        }

        /// <summary>Sets the text alignment of this Element.</summary>
        /// <param name="alignment">
        /// an enum value of type
        /// <see cref="iText.Layout.Properties.TextAlignment?"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetTextAlignment(TextAlignment? alignment) {
            SetProperty(Property.TEXT_ALIGNMENT, alignment);
            return (T)(Object)this;
        }

        /// <summary>Defines a custom spacing distance between all characters of a textual element.</summary>
        /// <remarks>
        /// Defines a custom spacing distance between all characters of a textual element.
        /// The character-spacing parameter is added to the glyph???s horizontal or vertical displacement (depending on the writing mode).
        /// </remarks>
        /// <param name="charSpacing">a floating point value</param>
        /// <returns>this Element.</returns>
        public virtual T SetCharacterSpacing(float charSpacing) {
            SetProperty(Property.CHARACTER_SPACING, charSpacing);
            return (T)(Object)this;
        }

        /// <summary>Defines a custom spacing distance between words of a textual element.</summary>
        /// <remarks>
        /// Defines a custom spacing distance between words of a textual element.
        /// This value works exactly like the character spacing, but only kicks in at word boundaries.
        /// </remarks>
        /// <param name="wordSpacing">a floating point value</param>
        /// <returns>this Element.</returns>
        public virtual T SetWordSpacing(float wordSpacing) {
            SetProperty(Property.WORD_SPACING, wordSpacing);
            return (T)(Object)this;
        }

        /// <summary>Enable or disable kerning.</summary>
        /// <remarks>
        /// Enable or disable kerning.
        /// Some fonts may specify kern pairs, i.e. pair of glyphs, between which the amount of horizontal space is adjusted.
        /// This adjustment is typically negative, e.g. in "AV" pair the glyphs will typically be moved closer to each other.
        /// </remarks>
        /// <param name="fontKerning">an enum value as a boolean wrapper specifying whether or not to apply kerning</param>
        /// <returns>this Element.</returns>
        public virtual T SetFontKerning(FontKerning fontKerning) {
            SetProperty(Property.FONT_KERNING, fontKerning);
            return (T)(Object)this;
        }

        /// <summary>Specifies a background color for the Element.</summary>
        /// <param name="backgroundColor">the background color</param>
        /// <returns>this Element.</returns>
        public virtual T SetBackgroundColor(Color backgroundColor) {
            return SetBackgroundColor(backgroundColor, 1f);
        }

        /// <summary>Specifies a background color for the Element.</summary>
        /// <param name="backgroundColor">the background color</param>
        /// <param name="opacity">the background color opacity</param>
        /// <returns>this Element.</returns>
        public virtual T SetBackgroundColor(Color backgroundColor, float opacity) {
            return SetBackgroundColor(backgroundColor, opacity, 0, 0, 0, 0);
        }

        /// <summary>
        /// Specifies a background color for the Element, and extra space that
        /// must be counted as part of the background and therefore colored.
        /// </summary>
        /// <param name="backgroundColor">the background color</param>
        /// <param name="extraLeft">extra coloring to the left side</param>
        /// <param name="extraTop">extra coloring at the top</param>
        /// <param name="extraRight">extra coloring to the right side</param>
        /// <param name="extraBottom">extra coloring at the bottom</param>
        /// <returns>this Element.</returns>
        public virtual T SetBackgroundColor(Color backgroundColor, float extraLeft, float extraTop, float extraRight
            , float extraBottom) {
            return SetBackgroundColor(backgroundColor, 1f, extraLeft, extraTop, extraRight, extraBottom);
        }

        /// <summary>
        /// Specifies a background color for the Element, and extra space that
        /// must be counted as part of the background and therefore colored.
        /// </summary>
        /// <param name="backgroundColor">the background color</param>
        /// <param name="opacity">the background color opacity</param>
        /// <param name="extraLeft">extra coloring to the left side</param>
        /// <param name="extraTop">extra coloring at the top</param>
        /// <param name="extraRight">extra coloring to the right side</param>
        /// <param name="extraBottom">extra coloring at the bottom</param>
        /// <returns>this Element.</returns>
        public virtual T SetBackgroundColor(Color backgroundColor, float opacity, float extraLeft, float extraTop, 
            float extraRight, float extraBottom) {
            SetProperty(Property.BACKGROUND, backgroundColor != null ? new Background(backgroundColor, opacity, extraLeft
                , extraTop, extraRight, extraBottom) : null);
            return (T)(Object)this;
        }

        /// <summary>Sets a border for all four edges of this Element with customizable color, width, pattern type.</summary>
        /// <param name="border">
        /// a customized
        /// <see cref="iText.Layout.Borders.Border"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetBorder(Border border) {
            SetProperty(Property.BORDER, border);
            return (T)(Object)this;
        }

        /// <summary>Sets a border for the upper limit of this Element with customizable color, width, pattern type.</summary>
        /// <param name="border">
        /// a customized
        /// <see cref="iText.Layout.Borders.Border"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetBorderTop(Border border) {
            SetProperty(Property.BORDER_TOP, border);
            return (T)(Object)this;
        }

        /// <summary>Sets a border for the right limit of this Element with customizable color, width, pattern type.</summary>
        /// <param name="border">
        /// a customized
        /// <see cref="iText.Layout.Borders.Border"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetBorderRight(Border border) {
            SetProperty(Property.BORDER_RIGHT, border);
            return (T)(Object)this;
        }

        /// <summary>Sets a border for the bottom limit of this Element with customizable color, width, pattern type.</summary>
        /// <param name="border">
        /// a customized
        /// <see cref="iText.Layout.Borders.Border"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetBorderBottom(Border border) {
            SetProperty(Property.BORDER_BOTTOM, border);
            return (T)(Object)this;
        }

        /// <summary>Sets a border for the left limit of this Element with customizable color, width, pattern type.</summary>
        /// <param name="border">
        /// a customized
        /// <see cref="iText.Layout.Borders.Border"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetBorderLeft(Border border) {
            SetProperty(Property.BORDER_LEFT, border);
            return (T)(Object)this;
        }

        /// <summary>Sets a rule for splitting strings when they don't fit into one line.</summary>
        /// <remarks>
        /// Sets a rule for splitting strings when they don't fit into one line.
        /// The default implementation is
        /// <see cref="iText.Layout.Splitting.DefaultSplitCharacters"/>
        /// </remarks>
        /// <param name="splitCharacters">
        /// an implementation of
        /// <see cref="iText.Layout.Splitting.ISplitCharacters"/>
        /// </param>
        /// <returns>this Element.</returns>
        public virtual T SetSplitCharacters(ISplitCharacters splitCharacters) {
            SetProperty(Property.SPLIT_CHARACTERS, splitCharacters);
            return (T)(Object)this;
        }

        /// <summary>Gets a rule for splitting strings when they don't fit into one line.</summary>
        /// <returns>
        /// the current string splitting rule, an implementation of
        /// <see cref="iText.Layout.Splitting.ISplitCharacters"/>
        /// </returns>
        public virtual ISplitCharacters GetSplitCharacters() {
            return this.GetProperty<ISplitCharacters>(Property.SPLIT_CHARACTERS);
        }

        /// <summary>
        /// Gets the text rendering mode, a variable that determines whether showing
        /// text causes glyph outlines to be stroked, filled, used as a clipping
        /// boundary, or some combination of the three.
        /// </summary>
        /// <returns>the current text rendering mode</returns>
        /// <seealso cref="iText.Kernel.Pdf.Canvas.PdfCanvasConstants.TextRenderingMode"/>
        public virtual int? GetTextRenderingMode() {
            return this.GetProperty<int?>(Property.TEXT_RENDERING_MODE);
        }

        /// <summary>
        /// Sets the text rendering mode, a variable that determines whether showing
        /// text causes glyph outlines to be stroked, filled, used as a clipping
        /// boundary, or some combination of the three.
        /// </summary>
        /// <param name="textRenderingMode">an <code>int</code> value</param>
        /// <returns>this Element.</returns>
        /// <seealso cref="iText.Kernel.Pdf.Canvas.PdfCanvasConstants.TextRenderingMode"/>
        public virtual T SetTextRenderingMode(int textRenderingMode) {
            SetProperty(Property.TEXT_RENDERING_MODE, textRenderingMode);
            return (T)(Object)this;
        }

        /// <summary>Gets the stroke color for the current element.</summary>
        /// <remarks>
        /// Gets the stroke color for the current element.
        /// The stroke color is the color of the outlines or edges of a shape.
        /// </remarks>
        /// <returns>the current stroke color</returns>
        public virtual Color GetStrokeColor() {
            return this.GetProperty<Color>(Property.STROKE_COLOR);
        }

        /// <summary>Sets the stroke color for the current element.</summary>
        /// <remarks>
        /// Sets the stroke color for the current element.
        /// The stroke color is the color of the outlines or edges of a shape.
        /// </remarks>
        /// <param name="strokeColor">a new stroke color</param>
        /// <returns>this Element.</returns>
        public virtual T SetStrokeColor(Color strokeColor) {
            SetProperty(Property.STROKE_COLOR, strokeColor);
            return (T)(Object)this;
        }

        /// <summary>Gets the stroke width for the current element.</summary>
        /// <remarks>
        /// Gets the stroke width for the current element.
        /// The stroke width is the width of the outlines or edges of a shape.
        /// </remarks>
        /// <returns>the current stroke width</returns>
        public virtual float? GetStrokeWidth() {
            return this.GetProperty<float?>(Property.STROKE_WIDTH);
        }

        /// <summary>Sets the stroke width for the current element.</summary>
        /// <remarks>
        /// Sets the stroke width for the current element.
        /// The stroke width is the width of the outlines or edges of a shape.
        /// </remarks>
        /// <param name="strokeWidth">a new stroke width</param>
        /// <returns>this Element.</returns>
        public virtual T SetStrokeWidth(float strokeWidth) {
            SetProperty(Property.STROKE_WIDTH, strokeWidth);
            return (T)(Object)this;
        }

        /// <summary>Switch on the simulation of bold style for a font.</summary>
        /// <remarks>
        /// Switch on the simulation of bold style for a font.
        /// Be aware that using correct bold font is highly preferred over this option.
        /// </remarks>
        /// <returns>this element</returns>
        public virtual T SetBold() {
            SetProperty(Property.BOLD_SIMULATION, true);
            return (T)(Object)this;
        }

        /// <summary>Switch on the simulation of italic style for a font.</summary>
        /// <remarks>
        /// Switch on the simulation of italic style for a font.
        /// Be aware that using correct italic (oblique) font is highly preferred over this option.
        /// </remarks>
        /// <returns>this element</returns>
        public virtual T SetItalic() {
            SetProperty(Property.ITALIC_SIMULATION, true);
            return (T)(Object)this;
        }

        /// <summary>Sets default line-through attributes for text.</summary>
        /// <remarks>
        /// Sets default line-through attributes for text.
        /// See
        /// <see cref="ElementPropertyContainer{T}.SetUnderline(iText.Kernel.Colors.Color, float, float, float, float, int)
        ///     "/>
        /// for more fine tuning.
        /// </remarks>
        /// <returns>this element</returns>
        public virtual T SetLineThrough() {
            // 7/24 is the average between default browser behavior(1/4) and iText5 behavior(1/3)
            return SetUnderline(null, .75f, 0, 0, 7 / 24f, PdfCanvasConstants.LineCapStyle.BUTT);
        }

        /// <summary>Sets default underline attributes for text.</summary>
        /// <remarks>
        /// Sets default underline attributes for text.
        /// See other overloads for more fine tuning.
        /// </remarks>
        /// <returns>this element</returns>
        public virtual T SetUnderline() {
            return SetUnderline(null, .75f, 0, 0, -1 / 8f, PdfCanvasConstants.LineCapStyle.BUTT);
        }

        /// <summary>
        /// This attribute specifies the base direction of directionally neutral text
        /// (i.e., text that doesn't have inherent directionality as defined in Unicode)
        /// in an element's content and attribute values.
        /// </summary>
        /// <param name="baseDirection">base direction</param>
        /// <returns>this element</returns>
        [Obsolete("Will be removed in 7.1 in favor of SetBaseDirection(BaseDirection? baseDirection)")]
        public virtual T SetBaseDirection(BaseDirection baseDirection) {
            return SetBaseDirection((BaseDirection?)baseDirection);
        }

        /// <summary>Sets an horizontal line that can be an underline or a strikethrough.</summary>
        /// <remarks>
        /// Sets an horizontal line that can be an underline or a strikethrough.
        /// Actually, the line can be anywhere vertically and has always the text width.
        /// Multiple call to this method will produce multiple lines.
        /// </remarks>
        /// <param name="thickness">the absolute thickness of the line</param>
        /// <param name="yPosition">the absolute y position relative to the baseline</param>
        /// <returns>this element</returns>
        public virtual T SetUnderline(float thickness, float yPosition) {
            return SetUnderline(null, thickness, 0, yPosition, 0, PdfCanvasConstants.LineCapStyle.BUTT);
        }

        /// <summary>Sets an horizontal line that can be an underline or a strikethrough.</summary>
        /// <remarks>
        /// Sets an horizontal line that can be an underline or a strikethrough.
        /// Actually, the line can be anywhere vertically due to position parameter.
        /// Multiple call to this method will produce multiple lines.
        /// <p>
        /// The thickness of the line will be
        /// <c>thickness + thicknessMul * fontSize</c>
        /// .
        /// The position of the line will be
        /// <c>baseLine + yPosition + yPositionMul * fontSize</c>
        /// .
        /// </remarks>
        /// <param name="color">
        /// the color of the line or <CODE>null</CODE> to follow the
        /// text color
        /// </param>
        /// <param name="thickness">the absolute thickness of the line</param>
        /// <param name="thicknessMul">the thickness multiplication factor with the font size</param>
        /// <param name="yPosition">the absolute y position relative to the baseline</param>
        /// <param name="yPositionMul">the position multiplication factor with the font size</param>
        /// <param name="lineCapStyle">
        /// the end line cap style. Allowed values are enumerated in
        /// <see cref="iText.Kernel.Pdf.Canvas.PdfCanvasConstants.LineCapStyle"/>
        /// </param>
        /// <returns>this element</returns>
        public virtual T SetUnderline(Color color, float thickness, float thicknessMul, float yPosition, float yPositionMul
            , int lineCapStyle) {
            return SetUnderline(color, 1f, thickness, thicknessMul, yPosition, yPositionMul, lineCapStyle);
        }

        /// <summary>Sets an horizontal line that can be an underline or a strikethrough.</summary>
        /// <remarks>
        /// Sets an horizontal line that can be an underline or a strikethrough.
        /// Actually, the line can be anywhere vertically due to position parameter.
        /// Multiple call to this method will produce multiple lines.
        /// <p>
        /// The thickness of the line will be
        /// <c>thickness + thicknessMul * fontSize</c>
        /// .
        /// The position of the line will be
        /// <c>baseLine + yPosition + yPositionMul * fontSize</c>
        /// .
        /// </remarks>
        /// <param name="color">
        /// the color of the line or <CODE>null</CODE> to follow the
        /// text color
        /// </param>
        /// <param name="opacity">the opacity of the line</param>
        /// <param name="thickness">the absolute thickness of the line</param>
        /// <param name="thicknessMul">the thickness multiplication factor with the font size</param>
        /// <param name="yPosition">the absolute y position relative to the baseline</param>
        /// <param name="yPositionMul">the position multiplication factor with the font size</param>
        /// <param name="lineCapStyle">
        /// the end line cap style. Allowed values are enumerated in
        /// <see cref="iText.Kernel.Pdf.Canvas.PdfCanvasConstants.LineCapStyle"/>
        /// </param>
        /// <returns>this element</returns>
        public virtual T SetUnderline(Color color, float opacity, float thickness, float thicknessMul, float yPosition
            , float yPositionMul, int lineCapStyle) {
            Underline newUnderline = new Underline(color, opacity, thickness, thicknessMul, yPosition, yPositionMul, lineCapStyle
                );
            Object currentProperty = this.GetProperty<Object>(Property.UNDERLINE);
            if (currentProperty is IList) {
                ((IList)currentProperty).Add(newUnderline);
            }
            else {
                if (currentProperty is Underline) {
                    IList<Underline> mergedUnderlines = new List<Underline>();
                    mergedUnderlines.Add((Underline)currentProperty);
                    mergedUnderlines.Add(newUnderline);
                    SetProperty(Property.UNDERLINE, mergedUnderlines);
                }
                else {
                    SetProperty(Property.UNDERLINE, newUnderline);
                }
            }
            return (T)(Object)this;
        }

        /// <summary>
        /// This attribute specifies the base direction of directionally neutral text
        /// (i.e., text that doesn't have inherent directionality as defined in Unicode)
        /// in an element's content and attribute values.
        /// </summary>
        /// <param name="baseDirection">base direction</param>
        /// <returns>this element</returns>
        public virtual T SetBaseDirection(BaseDirection? baseDirection) {
            SetProperty(Property.BASE_DIRECTION, baseDirection);
            return (T)(Object)this;
        }

        /// <summary>
        /// Sets a custom hyphenation configuration which will hyphenate words automatically accordingly to the
        /// language and country.
        /// </summary>
        /// <param name="hyphenationConfig"/>
        /// <returns>this element</returns>
        public virtual T SetHyphenation(HyphenationConfig hyphenationConfig) {
            SetProperty(Property.HYPHENATION, hyphenationConfig);
            return (T)(Object)this;
        }

        /// <summary>Sets the writing system for this text element.</summary>
        /// <param name="script">a new script type</param>
        /// <returns>this Element.</returns>
        public virtual T SetFontScript(UnicodeScript? script) {
            SetProperty(Property.FONT_SCRIPT, script);
            return (T)(Object)this;
        }

        /// <summary>Sets a destination name that will be created when this element is drawn to content.</summary>
        /// <param name="destination">the destination name to be created</param>
        /// <returns>this Element.</returns>
        public virtual T SetDestination(String destination) {
            SetProperty(Property.DESTINATION, destination);
            return (T)(Object)this;
        }

        public virtual T SetOpacity(float? opacity) {
            SetProperty(Property.OPACITY, opacity);
            return (T)(Object)this;
        }
    }
}
