using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Syncfusion.DemosCommon.WinUI
{
    /// <summary>
    /// Represent a template selector class for navigation item
    /// </summary>
    public class NavigationItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets a value of header template to display the control name.
        /// </summary>
        public DataTemplate HeaderTemplate { get; set; }

        public DataTemplate ControlItemTemplate { get; set; }
        public DataTemplate DemoItemTemplate { get; set; }

        public DataTemplate CategoryItemTemplate { get; set; }
        /// <summary>
        /// Gets or sets a value of AllControlsButtonTemplate to display the All Controls Button.
        /// </summary>
        public DataTemplate AllControlsButtonTemplate { get; set; }
        /// <summary>
        /// A method to select the navigation item template.
        /// </summary>
        /// <param name="item">Browser model</param>
        /// <returns>Data template</returns>
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if(item is ControlInfo)
            {
                return ControlItemTemplate;
            }
            if(item is DemoInfo)
            {
                return DemoItemTemplate;
            }
            if(item is CategoryGroup)
            {
                return CategoryItemTemplate;
            }
            if (item is BrowserModel model)
            {
                return HeaderTemplate;
            }
            if (item is AllControlsButtonInfo)
            {
                return AllControlsButtonTemplate;
            }
            throw new NotImplementedException();
        }
    }
}
