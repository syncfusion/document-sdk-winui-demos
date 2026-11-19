#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.IO;
using System.Reflection;
using System.Text;
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
    public sealed partial class DigitalSignatureValidation : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(DigitalSignatureValidation).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public DigitalSignatureValidation()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Validates the digital signatures in the Word document.
        /// </summary>
        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path;
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.DocIO.";
#else
            path = "Syncfusion.DocIODemos.WinUI.";
#endif
            //Gets the input Word document.
            using Stream fileStream = assembly.GetManifestResourceStream(path + "Assets.DocIO.DigitalSignatureInput.docx");
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            StringBuilder builder = new();
            if (document.DigitalSignatures.Count > 0)
            {
                builder.AppendLine("The following digital signatures were found in the Word document");
                int index = 1;
                foreach (OfficeDigitalSignature signature in document.DigitalSignatures)
                {
                    builder.AppendLine();
                    builder.AppendLine($"{index:D3}:");
                    if (signature.IsValid)
                        builder.AppendLine("The signature is valid");
                    else
                        builder.AppendLine("The signature is invalid");
                    //Signature details
                    builder.AppendLine("Signed at : " + signature.SigningTime);
                    builder.AppendLine("Subject name : " + signature.Certificate.Subject);
                    builder.AppendLine("Issuer name : " + signature.Certificate.Issuer);
                    builder.AppendLine("Valid From : " + signature.Certificate.ValidFrom);
                    builder.AppendLine("Valid To : " + signature.Certificate.ValidTo);
                    index++;
                }
            }
            else
            {
                builder.AppendLine("The document has no digital signatures.");
            }
            //Displays the validation result in the read-only text box.
            resultEditor.Text = builder.ToString();
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path;
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.DocIO.";
#else
            path = "Syncfusion.DocIODemos.WinUI.";
#endif
            //Gets the input Word document.
            using Stream fileStream = assembly.GetManifestResourceStream(path + "Assets.DocIO.DigitalSignatureInput.docx");
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("DigitalSignatureInput.docx", ms);
        }
        #endregion
    }
}
