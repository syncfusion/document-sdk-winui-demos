#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using System.Reflection;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIODemos.WinUI.Helpers;
using Syncfusion.Office;

namespace DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public sealed partial class DigitalSignature : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(DigitalSignature).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public DigitalSignature()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Digitally signs the Word document.
        /// </summary>
        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path;
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.DocIO.";
#else
            path = "Syncfusion.DocIODemos.WinUI.";
#endif
            //Creates a new Word document and adds the content.
            using WordDocument document = new();
            AddContentToDocument(document);
            //Loads the .pfx certificate with its password.
            OfficeDigitalSignatureCertificate certificate = CreateCertificate(path);
            if ((bool)invisible.IsChecked)
                SignInvisibleDocument(document, certificate);
            else
            {
                AddSignatureLine(document);
                SignVisibleDocument(document, certificate, path);
            }
            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("DigitalSignature.docx", ms);
            #endregion Document SaveOption
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// OfficeDigitalSignatureCertificate only accepts a file path, so the embedded
        /// certificate is extracted to a temporary file, loaded and then deleted.
        /// </summary>
        private OfficeDigitalSignatureCertificate CreateCertificate(string path)
        {
            string certificatePath = Path.Combine(Path.GetTempPath(), "Certificate.pfx");
            try
            {
                using (Stream certificateStream = assembly.GetManifestResourceStream(path + "Assets.DocIO.Certificate.pfx"))
                using (FileStream certificateFile = new FileStream(certificatePath, FileMode.Create, FileAccess.Write))
                    certificateStream.CopyTo(certificateFile);
                return new OfficeDigitalSignatureCertificate(certificatePath, "password123");
            }
            finally
            {
                File.Delete(certificatePath);
            }
        }

        private void AddContentToDocument(WordDocument document)
        {
            IWSection section = document.AddSection();
            WTextBody body = section.Body;
            IWTextRange textRange = body.AddParagraph().AppendText("INVOICE");
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontSize = 24;
            body.AddParagraph();
            body.AddParagraph().AppendText("This invoice has been generated for the goods and services provided as per our agreement.\r\nWe value your business and look forward to serving you again in the future.");
            body.AddParagraph();
        }

        private void SignInvisibleDocument(WordDocument document, OfficeDigitalSignatureCertificate certificate)
        {
            SignatureSettings settings = new SignatureSettings
            {
                Comments = "Approved",
                //Sets the signature standard.
                XmlDsigLevel = (bool)xades.IsChecked ? XmlDsigLevel.XAdEsEpes : XmlDsigLevel.XmlDSig
            };
            document.AddDigitalSignature(certificate, settings);
        }

        private void SignVisibleDocument(WordDocument document, OfficeDigitalSignatureCertificate certificate, string path)
        {
            using Stream imageFileStream = assembly.GetManifestResourceStream(path + "Assets.DocIO.Signature.png");
            byte[] signatureImage = GetImageBytes(imageFileStream ?? Stream.Null);
            WPicture picture = GetSignatureLinePicture(document);
            if (picture == null)
                throw new Exception("Signature line not found.");
            SignatureSettings settings = new SignatureSettings
            {
                SignatureLineId = picture.SignatureLine.Id,
                SignTime = DateTime.Now,
                SignatureLineImage = signatureImage,
                Comments = "Approved",
                XmlDsigLevel = (bool)xades.IsChecked ? XmlDsigLevel.XAdEsEpes : XmlDsigLevel.XmlDSig
            };
            document.AddDigitalSignature(certificate, settings);
        }

        private void AddSignatureLine(WordDocument document)
        {
            SignatureLineSettings settings = new SignatureLineSettings
            {
                Signer = "John Doe",
                SignerTitle = "Manager",
                Email = "john.doe@company.com",
                ShowDate = true
            };
            document.LastSection
                    .AddParagraph()
                    .AppendSignatureLine(settings, 192, 96);
        }

        private WPicture GetSignatureLinePicture(WordDocument document)
        {
            WPicture picture = null;
            foreach (WSection section in document.Sections)
            {
                foreach (WParagraph paragraph in section.Paragraphs)
                {
                    foreach (Entity entity in paragraph.ChildEntities)
                    {
                        if (entity is WPicture currentPicture &&
                            currentPicture.IsSignatureLine)
                        {
                            picture = currentPicture;
                            break;
                        }
                    }
                    if (picture != null)
                        break;
                }
                if (picture != null)
                    break;
            }
            return picture;
        }

        private byte[] GetImageBytes(Stream image)
        {
            if (image == null)
                return Array.Empty<byte>();
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                return ms.ToArray();
            }
        }
        #endregion
    }
}
