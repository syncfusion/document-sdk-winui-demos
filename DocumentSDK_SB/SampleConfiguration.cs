namespace Syncfusion.SampleBrowser.WinUI
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
#if !DOCUMENT_SDK
            new DataGridDemos.WinUI.SamplesConfiguration();
            new TreeGridDemos.WinUI.SamplesConfiguration();
            new ChartDemos.WinUI.SamplesConfiguration();
            new RadialGaugeDemos.WinUI.SamplesConfiguration();
            new LinearGaugeDemos.WinUI.SamplesConfiguration();
            new BarcodeDemos.WinUI.SamplesConfiguration();
            new RibbonDemos.WinUI.SamplesConfiguration();
            new TreeViewDemos.WinUI.SamplesConfiguration();
            new SchedulerDemos.WinUI.SamplesConfiguration();
            new SliderDemos.WinUI.SamplesConfiguration();
            new AvatarViewDemos.WinUI.SamplesConfiguration();
            new EditorDemos.WinUI.SamplesConfiguration();
            new CalendarDemos.WinUI.SamplesConfiguration();
            new ShadowDemos.WinUI.SamplesConfiguration();
            new ShimmerDemos.WinUI.SamplesConfiguration();
            new ChatDemos.WinUI.SamplesConfiguration();
            new NotificationDemos.WinUI.SamplesConfiguration();
            new KanbanDemos.WinUI.SamplesConfiguration();
#endif
#if !UISuiteSDK
            new DocIODemos.WinUI.SamplesConfiguration();
            new PdfDemos.WinUI.SamplesConfiguration();
            new PresentationDemos.WinUI.SamplesConfiguration();
            new XlsIODemos.WinUI.SamplesConfiguration();
#endif
        }
    }
}
