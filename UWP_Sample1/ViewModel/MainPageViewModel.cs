using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UWP_Sample1.Command;
using UWP_Sample1.Model;
using UWP_Sample1.View;
using Windows.Networking.BackgroundTransfer;
using Windows.Web.Http;

namespace UWP_Sample1.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private BackgroundDownloader _Downloader;

        private CancellationTokenSource _Cts;

        private ObservableCollection<PageModel> _PageList;
        public ObservableCollection<PageModel> PageList
        {
            get { return _PageList; }
        }

        private ObservableCollection<SampleModel> _SampleModelCollection;
        public ObservableCollection<SampleModel> SampleModelCollection
        { get { return _SampleModelCollection; } }

        public ICommand SpritViewPaneCommand { get; private set; }

        public MainPageViewModel()
        {
            SpritViewPaneCommand = new SpritViewPaneCommand();
            _PageList = new ObservableCollection<PageModel>();
            _PageList.Add(new PageModel("Adative tile and toast sample", typeof(AdaptiveShellPage)));
            _PageList.Add(new PageModel("Network downloader", typeof(DownloadProgressPage)));
            _SampleModelCollection = new ObservableCollection<SampleModel>();
        }

        public async Task HttpDownloadAsync(Uri uri)
        {
            try
            {
                using (var client = new HttpClient())
                using (var response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    var responseStr = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(responseStr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async void BackgroundDownloadAsync(string DownloadGroupIdOrName)
        {
            _Downloader = new BackgroundDownloader();
            _Downloader.TransferGroup = BackgroundTransferGroup.CreateGroup(DownloadGroupIdOrName);
            _Cts = new CancellationTokenSource();
            try
            {
                foreach (var dItem in SampleModelCollection)
                {
                    var exitDownload = await BackgroundDownloader.GetCurrentDownloadsAsync();
                    var isActiveItem = exitDownload.Where(d => d.ResultFile.Name == dItem.Title).FirstOrDefault();
                    if (isActiveItem != null)
                    {
                        await HandleDownloadAsync(isActiveItem, false);
                    }
                    else
                    {
                        var downloadAction = _Downloader.CreateDownload(dItem.ItemUri, await dItem.CreateLocalFileAsync());
                        await HandleDownloadAsync(downloadAction, true);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private async Task HandleDownloadAsync(DownloadOperation download, bool start)
        {
            try
            {
                Progress<DownloadOperation> progressCallback = new Progress<DownloadOperation>(downloadProgress);
                if (start)
                {
                    await download.StartAsync().AsTask(_Cts.Token, progressCallback);
                }
                else
                {
                    await download.AttachAsync().AsTask(_Cts.Token, progressCallback);
                }
                var response = download.GetResponseInformation();
            }
            catch (TaskCanceledException tEx)
            {
                System.Diagnostics.Debug.WriteLine(tEx.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private async void downloadProgress(DownloadOperation download)
        {
            double progressPercent = 100;
            if (download.Progress.TotalBytesToReceive > 0)
            {
                progressPercent = download.Progress.BytesReceived * 100 / download.Progress.TotalBytesToReceive;
            }
            await Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var downloadItem = SampleModelCollection.Where(item => item.Title == download.ResultFile.Name).FirstOrDefault();
                if (downloadItem != null)
                    downloadItem.DownloadProgress = progressPercent;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
