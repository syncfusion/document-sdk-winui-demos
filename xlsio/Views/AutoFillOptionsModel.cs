using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.XlsIODemos.WinUI.Views
{
    internal class AutoFillOptionsModel
    {
        public string SelectedAutoFillOption { get; set; }
        public ExcelSeriesBy SeriesBy { get; set; }
        public string StepValue { get; set; }
        public string StopValue { get; set; }
        public bool Trend { get; set; }
    }
}
