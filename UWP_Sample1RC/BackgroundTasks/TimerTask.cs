using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Sample1RC.Helpers;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace UWP_Sample1RC.BackgroundTasks
{
    public sealed class TimerTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled += TaskInstance_Canceled;
#if DEBUG
            //ShowToast("WTF");
            ShowAdaptiveToast();
#endif
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {

        }

        private void ShowToast(String toastMessage)
        {
            var toastTemplate = ToastTemplateType.ToastText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(toastMessage));
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void ShowAdaptiveToast()
        {
            var toastVisual = new ToastVisualElement()
            {
                version = 3,
                bindingElement = new ToastBindingElement()
                {
                    template = AdapiveToastBindingEnum.ToastGeneric,
                    ChildElements = new IChildElement[]
                    {
                        new ToastTextElement() { text="title" },
                        new ToastTextElement() { text="content" },
                        new ToastImageElement() { imageSource = "ms-appx:///Assets/Logo.png", placement= AdaptiveToastImagePlacementEnum.appLogoOverride, hintCrop = AdaptiveToastCropEnum.circle },
                        new ToastImageElement() { imageSource = "ms-appx:///Assets/IC716666.png", placement= AdaptiveToastImagePlacementEnum.inline, hintCrop = AdaptiveToastCropEnum.none, IsAddImageQuery = false, alt = "override" }
                    },
                }
            };
            var toast = new ToastElement()
            {
                visual = toastVisual,
                actions = new ToastActionsElement()
                {
                    ChildActions = new IChildElement[]
                    {
                        //new ToastInputElement() { id= "message", type = AdaptiveToastInputTypeEnum.text, PlaceholderContent="here" },
                        new ToastInputElement() { id = "selections", type= AdaptiveToastInputTypeEnum.selection, defaultInput = "2", InputSelections = new ToastSelectionElement[]
                        {
                            new ToastSelectionElement() { id="1",content="s1" },
                            new ToastSelectionElement() { id="2",content="s2" },
                            new ToastSelectionElement() { id="3",content="s3" }
                        } },
                        new ToastActionElement() { activationType = AdaptiveToastActiivationType.foreground, content="primary", arguments = "first" },
                        new ToastActionElement() { activationType = AdaptiveToastActiivationType.background, content="secondary", arguments = "secondary" }
                    }
                }
            };
            var toastNotification = new ToastNotification(AdaptiveShellHelper.TrySerializeToastTemplateToXml(toast));
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }
    }
}
