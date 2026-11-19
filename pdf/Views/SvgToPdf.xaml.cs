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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace Syncfusion.PdfDemos.WinUI
{
    public sealed partial class SvgToPdf : Page, IDisposable
    {
        public SvgToPdf()
        {
            this.InitializeComponent();
        }

        //Stream object to hold the file picked from file picker. 
        Stream documentStream = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = "Syncfusion.PdfDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Pdf.";
#endif
            //Load SVG file to stream
            if (documentStream == null)
                documentStream = typeof(PdfWatermark).GetTypeInfo().Assembly.GetManifestResourceStream(path + "Assets.SVGToPDF.svg");

            //Convert the SVG file to PDF template.
            SvgConverter converter = new SvgConverter();
            PdfTemplate temp = converter.Convert(documentStream);

            //Create a new PDF document and draw the PDF template to the page.
            PdfDocument document = new PdfDocument();
            document.PageSettings.Margins.All = 0;
            document.PageSettings.Size = new SizeF(temp.Width, temp.Height);
            PdfPage page = document.Pages.Add();

            //Draw the PDF template to the page.
            page.Graphics.DrawPdfTemplate(temp, new PointF(0, 0), new SizeF(temp.Width, temp.Height));
            
            //Creating the stream object.
            using (MemoryStream stream = new MemoryStream())
            {
                //Save the document into stream.
                document.Save(stream);
                //Close the PDF document
                document.Close(true);
                stream.Position = 0;
                //Save the output stream as a file using file picker.
                PdfUtil.Save("SVGToPDF.pdf", stream);
            }
            Dispose();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            //Create file open picker and set the .svg file type. 
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".svg");

            //Get process windows handle to open the dialog in application process. 
            IntPtr windowHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, windowHandle);

            //Pick the single file.
            StorageFile stFile = await openPicker.PickSingleFileAsync();

            if (stFile != null)
            {
                var properties = await stFile.GetBasicPropertiesAsync();
                if (properties.Size > 10 * 1024 * 1024)
                {
                    PdfUtil.ShowDialog("File size should not exceed 10 MB.", "Error");
                    return;
                }

                //Get the file name and update the source SVG field box. 
                textBox1.Text = stFile.Name;

                //Read the SVG file as file stream.
                IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.Read);
                documentStream = fileStream.AsStreamForRead();
            }
        }

        public void Dispose()
        {
            if (documentStream != null)
            {
                documentStream.Dispose();
                documentStream = null;
            }
        }
    }
}