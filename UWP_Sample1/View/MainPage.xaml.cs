using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Sample1.Model;
using UWP_Sample1.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWP_Sample1.View
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel MainPageVM;

        public MainPage()
        {
            this.InitializeComponent();
            MainPageVM = App.Current.Resources["MainPageVM"] as MainPageViewModel;
            var appView = ApplicationView.GetForCurrentView();
            appView.TitleBar.BackgroundColor = Colors.Black;
            appView.TitleBar.ForegroundColor = Colors.White;
            appView.TitleBar.ButtonBackgroundColor = Colors.Black;
            appView.TitleBar.ButtonForegroundColor = Colors.White;
            var pane = MainPageView.Pane as StackPanel;
            pane.DataContext = MainPageVM.PageList;
            foreach (var item in MainPageVM.PageList)
            {
                var itemElement = new ListViewItem() { DataContext = item, ContentTemplate = (DataTemplate)Resources["ListViewItemDataTemplate"], Tag = item.PageTitle };
                itemElement.Tapped += new TappedEventHandler(ListViewItemTapped);
                pane.Children.Add(itemElement);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = base.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested += new EventHandler<BackRequestedEventArgs>(BackRequestHandler);
            System.Diagnostics.Debug.WriteLine(base.Frame.GetNavigationState());
            base.OnNavigatedTo(e);
        }

        private void BackRequestHandler(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= BackRequestHandler;
            
            base.OnNavigatedFrom(e);
        }

        private void ListViewItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var item = (sender as ListViewItem).DataContext as PageModel;
            Frame.Navigate(item.NavigatePageType);
            //switch (item.NavigatePageType.Name)
            //{
            //    case "AdaptiveShellPage":
            //        this.MainPageView.Content = new AdaptiveShellPage();
            //        break;
            //    case "DownloadProgressPage":
            //        this.MainPageView.Content = new DownloadProgressPage();
            //        break;

            //}
            //this.MainPageView.IsPaneOpen = false;
        }
    }
}
