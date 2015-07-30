using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Sample1.Model
{
    public class PageModel : BaseModel
    {
        private string _PageTitle;

        public string PageTitle
        {
            get { return _PageTitle; }
            set { _PageTitle = value; }
        }

        private string _PageSubTitle;

        public string PageSubTitle
        {
            get { return _PageSubTitle; }
            set { _PageSubTitle = value; }
        }

        private Type _NavigatePageType;

        public Type NavigatePageType
        {
            get { return _NavigatePageType; }
            set { _NavigatePageType = value; }
        }

        public PageModel(string pageTitle, Type pageType)
        {
            this._PageTitle = pageTitle;
            this._NavigatePageType = pageType;
        }

        public PageModel(string pageTitle, string pageSubTitle, Type pageType)
        {
            this._PageTitle = pageTitle;
            this._PageSubTitle = pageSubTitle;
            this._NavigatePageType = pageType;
        }
    }
}
