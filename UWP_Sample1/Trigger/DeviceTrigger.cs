using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UWP_Sample1.Trigger
{
    public class DeviceFamilyTrigger : StateTriggerBase
    {
        private const string KeyName = "DeviceFamily";

        private string _DeviceFamilyName;

        public string DeviceFamilyName
        {
            get { return _DeviceFamilyName; }
            set
            {
                _DeviceFamilyName = value;
                var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
                if (qualifiers.ContainsKey(KeyName))
                {
                    var res = qualifiers[KeyName].ToString() == DeviceFamilyName;
                    SetActive(res);
                }
            }
        }
    }
}
