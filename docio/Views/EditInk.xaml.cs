using Microsoft.UI.Xaml.Controls;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIODemos.WinUI.Helpers;
using Syncfusion.DocIORenderer;
using Syncfusion.Drawing;
using Syncfusion.Office;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public sealed partial class EditInk : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(EditInk).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EditInk()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates a Word document.
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
            string resourcePath = path + "Assets.DocIO.EditInkInput.docx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Creates a new Word document instance.
            using WordDocument document = new();
            //Opens an existing word document.
            document.Open(fileStream, FormatType.Docx);
            // Access the first section of the document.
            WSection section = document.Sections[0];

            // Access the first ink and customize its trace points.
            WInk firstInk = section.Paragraphs[0].ChildEntities[0] as WInk;
            // Move the ink vertically.
            firstInk.VerticalPosition = 25f;
            // Copy existing points into the new array.
            int oldTracePointsLength = firstInk.Traces[0].Points.Length;
            int newTracePointsLength = oldTracePointsLength + 3;
            PointF[] newTracePoints = new PointF[newTracePointsLength];
            PointF[] oldTracePoints = firstInk.Traces[0].Points;
            Array.Copy(oldTracePoints, newTracePoints, oldTracePointsLength);
            newTracePoints[newTracePoints.Length - 3] = new PointF(oldTracePoints[3].X, 0);
            newTracePoints[newTracePoints.Length - 2] = new PointF(oldTracePoints[0].X, 0);
            newTracePoints[newTracePoints.Length - 1] = new PointF(oldTracePoints[0].X, oldTracePoints[0].Y);
            // Update the trace points of the first ink with the new array.
            firstInk.Traces[0].Points = newTracePoints;

            // Access the second ink and customize its container width.
            WInk secondInk = (WInk)section.Paragraphs[1].ChildEntities[0];
            IOfficeInkTrace secondInkTrace = secondInk.Traces[0];
            // Set the ink size (thickness) to 1 point.
            secondInkTrace.Brush.Size = new SizeF(1f, 1f);

            // Access the third ink and customize its container width.
            WInk thirdInk = (WInk)section.Paragraphs[2].ChildEntities[0];
            // Set the width of the ink container to 130 points.
            thirdInk.Width = 130f;

            // Access the fourth ink and customize its brush color.
            WParagraph paragraph = (WParagraph)section.Tables[0].Rows[0].Cells[0].ChildEntities[0];
            WInk fourthInk = (WInk)paragraph.ChildEntities[0];
            IOfficeInkTrace fourthInkTrace = fourthInk.Traces[0];
            // Set the color of the ink stroke to Yellow.
            fourthInkTrace.Brush.Color = Color.Yellow;

            #region Document SaveOption
            using MemoryStream ms = new();
            string filename = string.Empty;
            //Saves as .docx format.
            if (worddocx.IsChecked == true)
            {
                filename = "EditInk.docx";
                //Saves the Word document to the memory stream.
                document.Save(ms, FormatType.Docx);
            }
            //Saves as .pdf format.
            else if (pdf.IsChecked == true)
            {
                filename = "EditInk.pdf";
                //Creates a new DocIORenderer instance.
                using DocIORenderer renderer = new();
                //Converts Word document into PDF.
                using PdfDocument pdfDoc = renderer.ConvertToPDF(document);
                //Saves the PDF document to the memory stream.
                pdfDoc.Save(ms);
            }
            PdfDocument.ClearFontCache();
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch(filename, ms);
            #endregion Document SaveOption
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
            string resourcePath = path + "Assets.DocIO.EditInkInput.docx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("EditInkInput.docx", ms);
        }
        #endregion
    }
}
