using System.IO;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;
using Syncfusion.Presentation;
using System.Text.RegularExpressions;

namespace EssentialPresentation
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public sealed partial class PPTXToMarkdown : Page
    {
        #region Fields
        readonly Assembly assembly = typeof(PPTXToMarkdown).GetTypeInfo().Assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public PPTXToMarkdown()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Convert Presentation file to Markdown file.
        /// </summary>
        private void Button_Click(System.Object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
			
            string path = "Syncfusion.PresentationDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Presentation.";
#endif
            string resourcePath = path + "Assets.Presentation.PPTXToMarkdown.pptx";
           
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            using MemoryStream ms = new();
            //Saves the Markdown to the memory stream.
            presentation.Save(ms, FormatType.Markdown);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveAndLaunch.Save("PPTXToMarkdown.md", ms);
        }
        /// <summary>
        /// Opens the input PowerPoint file.
        /// </summary>
        private void ButtonView_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string path = "Syncfusion.PresentationDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Presentation.";
#endif
            //Gets the input PowerPoint document.
            string resourcePath = path + "Assets.Presentation.PPTXToMarkdown.pptx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
			//Saves the memory stream as file.
            SaveAndLaunch.Save("PPTXToMarkdown.pptx", ms);
        }
        #endregion
    }
}
