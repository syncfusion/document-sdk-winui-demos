using System.IO;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.MarkdownDemos.WinUI.Helpers;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace Markdown
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public sealed partial class MarkdownToPDF : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(MarkdownToPDF).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public MarkdownToPDF()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Converts the Markdown file to Word document.
        /// </summary>
        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path;
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#else
            path = "Syncfusion.MarkdownDemos.WinUI.";
#endif
            //Gets the input Markdown document.
            string resourcePath = path + "Assets.Markdown.MarkdownToWord.md";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Creates a new Word document instance.
            using WordDocument document = new();
            //Opens an existing Markdown document.
            document.Open(fileStream, FormatType.Markdown);

            #region Document Save
            using MemoryStream ms = new();
            string filename = "MarkdownToWord.pdf";
            //Creates a new DocIORenderer instance.
            using DocIORenderer renderer = new();
            //Converts Word document into PDF.
            using PdfDocument pdfDoc = renderer.ConvertToPDF(document);
            //Saves the PDF document to the memory stream.
            pdfDoc.Save(ms);
            PdfDocument.ClearFontCache();
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch(filename, ms);
            #endregion Document Save
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path;
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#else
            path = "Syncfusion.MarkdownDemos.WinUI.";
#endif
            //Gets the input markdown document.
            string resourcePath = path + "Assets.Markdown.MarkdownToWord.md";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("MarkdownToWord.md", ms);
        }
        #endregion
    }
}
