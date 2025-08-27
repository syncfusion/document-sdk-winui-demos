using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Syncfusion.DemosCommon.WinUI
{
    public sealed partial class TileGallery : Page
    {
        public TileGallery()
        {
            this.InitializeComponent();
        }

        public string DocsLink => UIStringsProvider.DocsLink;
        public string DocsDesc => UIStringsProvider.DocsDesc;
        public string GitHubLink => UIStringsProvider.GitHubLink;
        public string GitHubDesc => UIStringsProvider.GitHubDesc;
        public string FeatureLink => UIStringsProvider.FeatureLink;
        public string FeatureDesc => UIStringsProvider.FeatureDesc;

        private void OnScrollerViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            if (e.FinalView.HorizontalOffset < 1)
            {
                ScrollBackButton.Visibility = Visibility.Collapsed;
            }
            else if (e.FinalView.HorizontalOffset > 1)
            {
                ScrollBackButton.Visibility = Visibility.Visible;
            }

            if (e.FinalView.HorizontalOffset > Scroller.ScrollableWidth - 1)
            {
                ScrollForwardButton.Visibility = Visibility.Collapsed;
            }
            else if (e.FinalView.HorizontalOffset < Scroller.ScrollableWidth - 1)
            {
                ScrollForwardButton.Visibility = Visibility.Visible;
            }
        }

        private void OnScrollBackButtonClick(object sender, RoutedEventArgs e)
        {
            Scroller.ChangeView(Scroller.HorizontalOffset - Scroller.ViewportWidth, null, null);
        }

        private void OnScrollForwardButtonClick(object sender, RoutedEventArgs e)
        {
            Scroller.ChangeView(Scroller.HorizontalOffset + Scroller.ViewportWidth, null, null);
        }

        private void OnScrollerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateScrollButtonsVisibility();
        }

        private void UpdateScrollButtonsVisibility()
        {
            if (Scroller.ScrollableWidth > 0)
            {
                ScrollForwardButton.Visibility = Visibility.Visible;
            }
            else
            {
                ScrollForwardButton.Visibility = Visibility.Collapsed;
            }
        }
    }

}
