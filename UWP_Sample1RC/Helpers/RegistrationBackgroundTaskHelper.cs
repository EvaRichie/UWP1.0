using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace UWP_Sample1RC.Helpers
{
    public sealed class RegistrationBackgroundTaskHelper
    {
        public static async void RegisterBackgroundTasks(String taskName, Type taskType, IBackgroundTrigger triggerInstance)
        {
            if (BackgroundExecutionManager.GetAccessStatus() != BackgroundAccessStatus.Denied)
            {
                var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == taskName)
                    {
                        task.Value.Unregister(true);
                    }
                }
            }

            var backgroundTaskBuilder = new BackgroundTaskBuilder();
            backgroundTaskBuilder.Name = taskName;
            backgroundTaskBuilder.TaskEntryPoint = taskType.FullName;
            backgroundTaskBuilder.SetTrigger(triggerInstance);
            var taskRegistration = backgroundTaskBuilder.Register();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} is registed!", taskRegistration.Name));
        }

        public static async void RegisterBackgroundTasks(String taskName, Type taskType, IBackgroundTrigger triggerInstance, IBackgroundCondition triggerCondiction)
        {
            if (BackgroundExecutionManager.GetAccessStatus() != BackgroundAccessStatus.Denied)
            {
                var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == taskName)
                    {
                        task.Value.Unregister(true);
                    }
                }
            }

            var backgroundTaskBuilder = new BackgroundTaskBuilder();
            backgroundTaskBuilder.Name = taskName;
            backgroundTaskBuilder.TaskEntryPoint = taskType.FullName;
            backgroundTaskBuilder.SetTrigger(triggerInstance);
            backgroundTaskBuilder.AddCondition(triggerCondiction);
            var taskRegistration = backgroundTaskBuilder.Register();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} is registed!", taskRegistration.Name));
        }
    }
}
