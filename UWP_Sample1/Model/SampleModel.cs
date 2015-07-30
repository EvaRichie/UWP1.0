using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace UWP_Sample1.Model
{
    public class SampleModel : BaseModel
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; NotifyChanged(); }
        }

        private string _SubTitle;

        public string SubTitle
        {
            get { return _SubTitle; }
            set { _SubTitle = value; NotifyChanged(); }
        }

        private string _ContentText;

        public string ContentTex
        {
            get { return _ContentText; }
            set { _ContentText = value; NotifyChanged(); }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyChanged(); }
        }

        private string _ImageUriStr;

        public string ImageUriStr
        {
            get { return _ImageUriStr; }
            set { _ImageUriStr = value; NotifyChanged(); }
        }

        private Uri _ImageUri;

        public Uri ImageUri
        {
            get { return _ImageUri; }
            set { _ImageUri = value; NotifyChanged(); }
        }

        private Uri _ItemUri;

        public Uri ItemUri
        {
            get { return _ItemUri; }
            set { _ItemUri = value; NotifyChanged(); }
        }

        private double _DownloadProgress;

        public double DownloadProgress
        {
            get { return _DownloadProgress; }
            set { _DownloadProgress = value; NotifyChanged(); }
        }

        private DownloadOperation _Operation;
        public DownloadOperation Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        public async Task<IStorageFile> CreateLocalFileAsync()
        {
            var localStorage = ApplicationData.Current.LocalFolder;
            return await localStorage.CreateFileAsync(_Title, CreationCollisionOption.OpenIfExists);
        }

        public async Task<IStorageFile> CreateLocalFileAsync(CreationCollisionOption options)
        {
            var localStorage = ApplicationData.Current.LocalFolder;
            return await localStorage.CreateFileAsync(_Title, options);
        }
    }
}
