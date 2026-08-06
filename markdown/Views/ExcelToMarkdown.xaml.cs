#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.MarkdownDemos.WinUI.Helpers;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace Markdown
{
    /// <summary>
    /// Interaction logic for ExcelToMarkdown.xaml
    /// </summary>
    public partial class ExcelToMarkdown : Page
    {
        # region Constructor
        /// <summary>
        /// ExcelToMarkdown constructor
        /// </summary>
        public ExcelToMarkdown()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Loads the input template
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            string path = "Syncfusion.MarkdownDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#endif
            string inputPath = path + "Assets.Markdown.ExcelToMarkdown.xlsx";

            Assembly assembly = typeof(ExcelToMarkdown).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream(inputPath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("ExcelToMarkdown.xlsx", ms);
        }

        /// <summary>
        /// Convert To Markdown file from Excel file and save the converted file to user defined location.
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            string path = "Syncfusion.MarkdownDemos.WinUI.";
#if Main_SB
            path = "Syncfusion.SampleBrowser.WinUI.Markdown.";
#endif
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                string inputPath = path + "Assets.Markdown.ExcelToMarkdown.xlsx";
                Assembly assembly = typeof(ExcelToMarkdown).GetTypeInfo().Assembly;
                using Stream input = assembly.GetManifestResourceStream(inputPath);
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(input, ExcelOpenType.Automatic);

                string fileName = "ExcelToMarkdown.md";
                using MemoryStream stream = new MemoryStream();

                workbook.SaveAs(stream, ExcelSaveType.Markdown);
                stream.Position = 0;
                //Saves the memory stream as file.
                SaveHelper.SaveAndLaunch(fileName, stream);
            }
        }
        #endregion
    }
}