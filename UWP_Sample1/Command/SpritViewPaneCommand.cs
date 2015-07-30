using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP_Sample1.Command
{
    public class SpritViewPaneCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
            var rootFrame = Window.Current.Content as Frame;
            var currentPage = rootFrame.Content is Page ? (Page)rootFrame.Content : null;
            if (currentPage != null)
            {
                var splitViewCtrl = currentPage.FindName(parameter.ToString()) as SplitView;
                if (splitViewCtrl != null)
                {
                    splitViewCtrl.IsPaneOpen = !splitViewCtrl.IsPaneOpen;
                }
            }
        }
    }
}
