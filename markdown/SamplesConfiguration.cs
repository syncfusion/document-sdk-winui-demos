﻿using Markdown;
using Syncfusion.DemosCommon.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.MarkdownDemos.WinUI
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            DemoInfo createMarkdown = new()
            {
                Name = "Create Markdown",
                Category = "Getting Started",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to create a Markdown document with various elements such as headings, text, images, lists, tables, hyperlinks, and code blocks using .NET Markdown library.",
                DemoView = typeof(Markdown.CreateMarkdown),
                ShowInfoPanel = true
            };
            List<Documentation> selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Getting started", Uri = new Uri("https://help.syncfusion.com/document-processing/markdown/markdown-library/net/getting-started#creating-a-new-markdown-document-from-scratch-with-basic-elements") }
            };
            createMarkdown.Documentation.AddRange(selectionDocumentations);

            DemoInfo wordToMd = new()
            {
                Name = "Word To Markdown",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert the Word document to Markdown using .NET Word (DocIO) and .NET Markdown libraries.",
                DemoView = typeof(WordToMarkdown),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Word document to Markdown conversion", Uri = new Uri("https://help.syncfusion.com/document-processing/word/conversions/word-to-markdown-conversion") }
            };
            wordToMd.Documentation.AddRange(selectionDocumentations);

            DemoInfo mdToWord = new()
            {
                Name = "Markdown To Word",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert a Markdown file to a Word document using .NET Word (DocIO) and .NET Markdown libraries.",
                DemoView = typeof(Markdown.MarkdownToWord),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Markdown to Word conversion", Uri = new Uri("https://help.syncfusion.com/document-processing/word/conversions/markdown-to-word-conversion") }
            };
            mdToWord.Documentation.AddRange(selectionDocumentations);

            DemoInfo mdToHTML = new()
            {
                Name = "Markdown To HTML",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert a Markdown file to HTML using .NET Word (DocIO) and .NET Markdown libraries.",
                DemoView = typeof(Markdown.MarkdownToHTML),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Markdown to HTML conversion", Uri = new Uri("https://www.syncfusion.com/blogs/post/markdown-to-html-csharp-word-library") }
            };
            mdToHTML.Documentation.AddRange(selectionDocumentations);

            DemoInfo mdToPDF = new()
            {
                Name = "Markdown To PDF",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert a Markdown file to PDF using .NET Word (DocIO), .NET Markdown, and .NET PDF libraries.",
                DemoView = typeof(Markdown.MarkdownToPDF),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Markdown to PDF conversion", Uri = new Uri("https://www.syncfusion.com/blogs/post/markdown-to-pdf-in-csharp-word-library") }
            };
            mdToPDF.Documentation.AddRange(selectionDocumentations);

            DemoInfo pptxToMd = new()
            {
                Name = "PPTX To Markdown",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert the PowerPoint presentation to Markdown using .NET PowerPoint (Presentation) and .NET Markdown libraries.",
                DemoView = typeof(Markdown.PPTXToMarkdown),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  PPTX to Markdown conversion", Uri = new Uri("https://help.syncfusion.com/document-processing/powerpoint/conversions/powerpoint-to-markdown") }
            };
            pptxToMd.Documentation.AddRange(selectionDocumentations);

            DemoInfo mdToPPTX = new()
            {
                Name = "Markdown To PPTX",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert the Markdown file to PowerPoint presentation using .NET PowerPoint (Presentation) and .NET Markdown libraries.",
                DemoView = typeof(Markdown.MarkdownToPPTX),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Markdown to PPTX conversion", Uri = new Uri("https://help.syncfusion.com/document-processing/powerpoint/conversions/markdown-to-powerpoint") }
            };
            mdToPPTX.Documentation.AddRange(selectionDocumentations);

            DemoInfo excelToMd = new()
            {
                Name = "Excel To Markdown",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert an Excel file to a Markdown file using .NET Excel (XlsIO) and .NET Markdown libraries.",
                DemoView = typeof(Markdown.ExcelToMarkdown),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Excel to Markdown conversion", Uri = new Uri("https://help.syncfusion.com/document-processing/excel/conversions/excel-to-markdown/overview") }
            };
            excelToMd.Documentation.AddRange(selectionDocumentations);

            DemoInfo mdToExcel = new()
            {
                Name = "Markdown To Excel",
                Category = "Conversions",
                DemoType = DemoTypes.New,
                Description = "This sample demonstrates how to convert a Markdown file to an Excel worksheet using .NET Excel (XlsIO) and .NET Markdown libraries.",
                DemoView = typeof(Markdown.MarkdownToExcel),
                ShowInfoPanel = true
            };
            selectionDocumentations = new List<Documentation>
            {
                new Documentation() { Content = "Markdown library -  Markdown to Excel conversion", Uri = new Uri("https://help.syncfusion.com/document-processing/excel/conversions/markdown-to-excel/overview") }
            };
            mdToExcel.Documentation.AddRange(selectionDocumentations);

            var demos = new List<DemoInfo>()
            {
                createMarkdown,
                wordToMd,
                mdToWord,
                mdToHTML,
                mdToPDF,
                pptxToMd,
                mdToPPTX,
                excelToMd,
                mdToExcel,
            };

            var controlInfo = new ControlInfo()
            {
                Control = DemoControl.EssentialMarkdown,
                ControlBadge=ControlBadge.New,
                ControlCategory = ControlCategory.FileFormat,
                Description = "A .NET Markdown library to create, read, and edit Markdown documents programmatically.",
                Glyph = "\uE72a",
                ImageSource = "MarkdownViewer.png"
            };

            controlInfo.Demos.AddRange(demos);
            DemoHelper.ControlInfos.Add(controlInfo);
        }
    }
}
