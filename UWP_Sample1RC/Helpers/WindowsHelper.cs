using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP_Sample1RC.Helpers
{
    public sealed class WindowsHelper
    {
        public static async void CreateStandAloneWindowAsync(Type pageType)
        {
            var newViewInstance = Windows.ApplicationModel.Core.CoreApplication.CreateNewView();
            var viewId = 0;
            await newViewInstance.Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var rootFrame = new Frame();
                rootFrame.Navigate(pageType);
                Window.Current.Content = rootFrame;
                viewId = ApplicationView.GetForCurrentView().Id;
                newViewInstance.CoreWindow.Activate();
            });
            var tryShow = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }

        public static async void ReplaceCurrentWindowAsync(Type pageType)
        {
            var newViewInstance = Windows.ApplicationModel.Core.CoreApplication.CreateNewView();
            var viewId = 0;
            await newViewInstance.Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var rootFrame = new Frame();
                rootFrame.Navigate(pageType);
                Window.Current.Content = rootFrame;
                viewId = ApplicationView.GetForCurrentView().Id;
                newViewInstance.CoreWindow.Activate();
            });
            await ApplicationViewSwitcher.SwitchAsync(viewId);
        }
    }
}
