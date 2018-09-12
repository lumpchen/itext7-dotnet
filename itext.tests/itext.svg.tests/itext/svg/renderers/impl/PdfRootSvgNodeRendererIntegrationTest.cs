using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Svg.Converter;
using iText.Svg.Exceptions;
using iText.Svg.Renderers;

namespace iText.Svg.Renderers.Impl {
    public class PdfRootSvgNodeRendererIntegrationTest : SvgIntegrationTest {
        [NUnit.Framework.Test]
        public virtual void CalculateOutermostViewportTest() {
            Rectangle expected = new Rectangle(0, 0, 600, 600);
            SvgDrawContext context = new SvgDrawContext(null, null);
            PdfDocument document = new PdfDocument(new PdfWriter(new MemoryStream(), new WriterProperties().SetCompressionLevel
                (0)));
            document.AddNewPage();
            PdfFormXObject pdfForm = new PdfFormXObject(expected);
            PdfCanvas canvas = new PdfCanvas(pdfForm, document);
            context.PushCanvas(canvas);
            SvgTagSvgNodeRenderer renderer = new SvgTagSvgNodeRenderer();
            PdfRootSvgNodeRenderer root = new PdfRootSvgNodeRenderer(renderer);
            Rectangle actual = root.CalculateViewPort(context);
            NUnit.Framework.Assert.IsTrue(expected.EqualsWithEpsilon(actual));
        }

        [NUnit.Framework.Test]
        public virtual void CalculateOutermostViewportWithDifferentXYTest() {
            Rectangle expected = new Rectangle(10, 20, 600, 600);
            SvgDrawContext context = new SvgDrawContext(null, null);
            PdfDocument document = new PdfDocument(new PdfWriter(new MemoryStream(), new WriterProperties().SetCompressionLevel
                (0)));
            document.AddNewPage();
            PdfFormXObject pdfForm = new PdfFormXObject(expected);
            PdfCanvas canvas = new PdfCanvas(pdfForm, document);
            context.PushCanvas(canvas);
            SvgTagSvgNodeRenderer renderer = new SvgTagSvgNodeRenderer();
            PdfRootSvgNodeRenderer root = new PdfRootSvgNodeRenderer(renderer);
            Rectangle actual = root.CalculateViewPort(context);
            NUnit.Framework.Assert.IsTrue(expected.EqualsWithEpsilon(actual));
        }

        [NUnit.Framework.Test]
        public virtual void CalculateNestedViewportDifferentFromParentTest() {
            Rectangle expected = new Rectangle(0, 0, 500, 500);
            SvgDrawContext context = new SvgDrawContext(null, null);
            PdfDocument document = new PdfDocument(new PdfWriter(new MemoryStream(), new WriterProperties().SetCompressionLevel
                (0)));
            document.AddNewPage();
            PdfFormXObject pdfForm = new PdfFormXObject(expected);
            PdfCanvas canvas = new PdfCanvas(pdfForm, document);
            context.PushCanvas(canvas);
            context.AddViewPort(expected);
            SvgTagSvgNodeRenderer parent = new SvgTagSvgNodeRenderer();
            SvgTagSvgNodeRenderer renderer = new SvgTagSvgNodeRenderer();
            PdfRootSvgNodeRenderer root = new PdfRootSvgNodeRenderer(parent);
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put("width", "500");
            styles.Put("height", "500");
            renderer.SetAttributesAndStyles(styles);
            renderer.SetParent(parent);
            Rectangle actual = root.CalculateViewPort(context);
            NUnit.Framework.Assert.IsTrue(expected.EqualsWithEpsilon(actual));
        }

        [NUnit.Framework.Test]
        public virtual void NoBoundingBoxOnXObjectTest() {
            NUnit.Framework.Assert.That(() =>  {
                PdfDocument document = new PdfDocument(new PdfWriter(new MemoryStream(), new WriterProperties().SetCompressionLevel
                    (0)));
                document.AddNewPage();
                ISvgNodeRenderer processed = SvgConverter.Process(SvgConverter.Parse("<svg />")).GetRootRenderer();
                PdfRootSvgNodeRenderer root = new PdfRootSvgNodeRenderer(processed);
                PdfFormXObject pdfForm = new PdfFormXObject(new PdfStream());
                PdfCanvas canvas = new PdfCanvas(pdfForm, document);
                SvgDrawContext context = new SvgDrawContext(null, null);
                context.PushCanvas(canvas);
                root.Draw(context);
            }
            , NUnit.Framework.Throws.TypeOf<SvgProcessingException>().With.Message.EqualTo(SvgLogMessageConstant.ROOT_SVG_NO_BBOX));
;
        }

        [NUnit.Framework.Test]
        public virtual void CalculateOutermostTransformation() {
            AffineTransform expected = new AffineTransform(1d, 0d, 0d, -1d, 0d, 600d);
            SvgDrawContext context = new SvgDrawContext(null, null);
            PdfDocument document = new PdfDocument(new PdfWriter(new MemoryStream(), new WriterProperties().SetCompressionLevel
                (0)));
            document.AddNewPage();
            PdfFormXObject pdfForm = new PdfFormXObject(new Rectangle(0, 0, 600, 600));
            PdfCanvas canvas = new PdfCanvas(pdfForm, document);
            context.PushCanvas(canvas);
            SvgTagSvgNodeRenderer renderer = new SvgTagSvgNodeRenderer();
            PdfRootSvgNodeRenderer root = new PdfRootSvgNodeRenderer(renderer);
            context.AddViewPort(root.CalculateViewPort(context));
            AffineTransform actual = root.CalculateTransformation(context);
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void DeepCopyTest() {
            SvgTagSvgNodeRenderer subTree = new SvgTagSvgNodeRenderer();
            subTree.AddChild(new CircleSvgNodeRenderer());
            PdfRootSvgNodeRenderer expected = new PdfRootSvgNodeRenderer(subTree);
            ISvgNodeRenderer actual = expected.CreateDeepCopy();
            expected.Equals(actual);
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }
    }
}