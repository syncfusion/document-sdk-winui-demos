#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

namespace Syncfusion.PdfDemos.WinUI
{
    public sealed partial class LinearizedPdf : Page
    {
        private Stream selectedDocumentStream;
        private string selectedFileName = "pdf_succinctly.pdf";

        public LinearizedPdf()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // FILE SIZE LIMIT (10 MB)
            const long MAX_FILE_SIZE = 10 * 1024 * 1024;
            if (selectedDocumentStream != null && selectedDocumentStream.Length > MAX_FILE_SIZE)
            {
                var dialog = new Microsoft.UI.Xaml.Controls.ContentDialog
                {
                    Title = "File too large",
                    Content = string.Format("The selected PDF file exceeds the maximum allowed size of 10 MB. File size: {0:F2} MB.", selectedDocumentStream.Length / (1024.0 * 1024.0)),
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
                return;
            }

            string path = "Syncfusion.PdfDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Pdf.";
#endif
            //Load the selected PDF or the embedded default PDF.
            Stream documentStream = selectedDocumentStream ?? typeof(LinearizedPdf).GetTypeInfo().Assembly.GetManifestResourceStream(path + "Assets.pdf_succinctly.pdf");
             
            // Load the PDF document
            PdfLoadedDocument document = new PdfLoadedDocument(documentStream);

            // Enable linearization for web optimization
            document.FileStructure.IsLinearized = true;

            //Creating the stream object.
            using (MemoryStream stream = new MemoryStream())
            {
                //Save the linearized document into stream.
                document.Save(stream);
                //Close the PDF document
                document.Close(true);
                stream.Position = 0;
                //Save the Output stream as a file using file picker.
                PdfUtil.Save(selectedFileName, stream);
            }

            documentStream.Dispose();
            selectedDocumentStream = null;
        }

        private async void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                ViewMode = PickerViewMode.List
            };
            openPicker.FileTypeFilter.Add(".pdf");

            IntPtr windowHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, windowHandle);

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                selectedDocumentStream?.Dispose();
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                selectedDocumentStream = fileStream.AsStreamForRead();
                selectedFileName = file.Name;
                SourcePdf.Text = selectedFileName;
            }
        }
    }
}

