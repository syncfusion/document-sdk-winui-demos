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
    /// Interaction logic for MarkdownToExcel.xaml
    /// </summary>
    public partial class MarkdownToExcel : Page
    {
        # region Constructor
        /// <summary>
        /// MarkdownToExcel constructor
        /// </summary>
        public MarkdownToExcel()
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
            string inputPath = path + "Assets.Markdown.MarkdownToExcel.md";

            Assembly assembly = typeof(MarkdownToExcel).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream(inputPath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveHelper.SaveAndLaunch("MarkdownToExcel.md", ms);
        }

        /// <summary>
        /// Convert To Excel file from Markdown file and save the converted file to user defined location.
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
                string inputPath = path + "Assets.Markdown.MarkdownToExcel.md";
                Assembly assembly = typeof(MarkdownToExcel).GetTypeInfo().Assembly;
                excelEngine.Excel.PreserveCSVDataTypes = true;
                using Stream input = assembly.GetManifestResourceStream(inputPath);
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(input, ExcelOpenType.Markdown);
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.UsedRange.AutofitColumns();
                sheet.Calculate();

                string fileName = "MarkdownToExcel.xlsx";
                using MemoryStream stream = new MemoryStream();
                workbook.Version = ExcelVersion.Xlsx;
                workbook.SaveAs(stream);
                stream.Position = 0;
                //Saves the memory stream as file.
                SaveHelper.SaveAndLaunch(fileName, stream);
            }
        }
        #endregion
    }
}