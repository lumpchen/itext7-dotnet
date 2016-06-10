using System;
using System.IO;
using iTextSharp.IO.Source;
using iTextSharp.Kernel.Pdf;
using iTextSharp.Kernel.Pdf.Action;
using iTextSharp.Test;

namespace iTextSharp.Pdfa
{
    public class PdfA1ActionCheckTest : ExtendedITextTest
    {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext
            .TestDirectory + "/../../resources/itextsharp/pdfa/";

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck01()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.Launch);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck02()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.Hide);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck03()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.Sound);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck04()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.Movie);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck05()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.ResetForm);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck06()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.ImportData);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck07()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.JavaScript);
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck08()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.AddNewPage();
                PdfDictionary openActions = new PdfDictionary();
                openActions.Put(PdfName.S, PdfName.Named);
                openActions.Put(PdfName.N, new PdfName("CustomName"));
                doc.GetCatalog().Put(PdfName.OpenAction, openActions);
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException.NamedActionType1IsNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck09()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                PdfPage page = doc.AddNewPage();
                page.SetAdditionalAction(PdfName.C, PdfAction.CreateJavaScript("js"));
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException._1ActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck10()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                PdfPage page = doc.AddNewPage();
                PdfDictionary action = new PdfDictionary();
                action.Put(PdfName.S, PdfName.SetState);
                page.SetAdditionalAction(PdfName.C, new PdfAction(action));
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException.DeprecatedSetStateAndNoOpActionsAreNotAllowed));
;
        }

        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="iTextSharp.Kernel.XMP.XMPException"/>
        [NUnit.Framework.Test]
        public virtual void ActionCheck11()
        {
            NUnit.Framework.Assert.That(() => 
            {
                PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
                Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open
                    , FileAccess.Read);
                PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
                    ("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
                doc.GetCatalog().SetAdditionalAction(PdfName.C, PdfAction.CreateJavaScript("js"));
                doc.Close();
            }
            , NUnit.Framework.Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException.CatalogDictionaryShallNotContainAAEntry));
;
        }
    }
}
