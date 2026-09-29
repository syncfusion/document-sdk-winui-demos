using System.IO;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.DocIORenderer;
using Syncfusion.MarkdownDemos.WinUI.Helpers;

namespace Markdown
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public sealed partial class WordToMarkdown : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(WordToMarkdown).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public WordToMarkdown()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Converts Word document to Markdown.
        /// </summary>
        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path;
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#else
            path = "Syncfusion.MarkdownDemos.WinUI.";
#endif
            //Gets the input Word document.
            string resourcePath = path+"Assets.Markdown.WordtoMD.docx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            // Initialize DocIORenderer to preserve drawing elements such as charts, text boxes, shapes, ink elements, MathML equations, SmartArt, group shapes, and canvas objects.
            using DocIORenderer render = new DocIORenderer();
            using MemoryStream ms = new();
            //Saves the Markdown document to the memory stream.
            document.Save(ms, FormatType.Markdown);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("WordtoMD.md", ms);
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
            //Gets the input Word document.
            string resourcePath = path + "Assets.Markdown.WordtoMD.docx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("WordtoMD.docx", ms);
        }
        #endregion
    }
}
