using Microsoft.UI.Xaml.Controls;
using Syncfusion.MarkdownDemos.WinUI.Helpers;
using Syncfusion.Presentation;
using System.IO;
using System.Reflection;

namespace Markdown
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public sealed partial class MarkdownToPPTX : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(MarkdownToPPTX).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public MarkdownToPPTX()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Convert Markdown file to Presentation file.
        /// </summary>
        private void Button_Click(System.Object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
			
            string path = "Syncfusion.MarkdownDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#endif
            string resourcePath = path + "Assets.Markdown.MarkdownToPPTX.md";
           
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing Markdown file.
            using IPresentation markdownDoc = Syncfusion.Presentation.Presentation.Open(fileStream);        
				
            using MemoryStream ms = new();
            //Saves the Markdown file as presnetation stream.
            markdownDoc.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("MarkdownToPPTX.pptx", ms); 
        }
        /// <summary>
        /// Opens the input Markdown file.
        /// </summary>
        private void ButtonView_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path = "Syncfusion.MarkdownDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#endif
            //Gets the input Markdown document.
            string resourcePath = path + "Assets.Markdown.MarkdownToPPTX.md";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("MarkdownToPPTX.md", ms);
        }
        #endregion
    }
}
