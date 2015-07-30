using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Sample1RC.Helpers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Sample1.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdaptiveShellPage : Page
    {
        public AdaptiveShellPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Content.ToString())
            {
                case "Sent tile notification":
                    CreateTileNotification();
                    break;
                case "Sent toast notification":
                    CreateToastNotification();
                    break;
                case "":
                    break;
            }
        }

        private void CreateTileNotification()
        {
            var text1 = new TextElement() { StringData = this.Group1Tbx.Text };
            var text2 = new TextElement() { StringData = this.Group2Tbx.Text };
            var defaultImgPath = "ms-appx:///Assets/Logo.png";
            var imgPath1 = this.Group1ImgTbx.Text;
            if (String.IsNullOrEmpty(imgPath1) || String.IsNullOrWhiteSpace(imgPath1))
                imgPath1 = defaultImgPath;
            var imgPath2 = this.Group2ImgTbx.Text;
            if (String.IsNullOrEmpty(imgPath2) || String.IsNullOrWhiteSpace(imgPath2))
                imgPath2 = defaultImgPath;
            var image1 = new ImageElement() { ImageSource = imgPath1, IsRemoveMargin = true };
            var image2 = new ImageElement() { ImageSource = imgPath2, IsRemoveMargin = true };
            var subgroup1 = new SubGroupElement() { weight = 1, ChildElements = new IChildElement[] { text1, image1 } };
            var subgroup2 = new SubGroupElement() { weight = 1, ChildElements = new IChildElement[] { text2, image2 } };
            var group1 = new GroupElement() { Subgroups = new SubGroupElement[] { subgroup1, subgroup2 } };
            var group2 = new GroupElement() { Subgroups = new SubGroupElement[] { subgroup1, subgroup2 } };
            var visual = new TileVisualElement()
            {
                version = 3,
                BindingElements = new TileBindingElement[]
                {
                    new TileBindingElement() { TileTemplate = AdaptiveTileSizeEnum.TileWide, Group = group1 },
                    new TileBindingElement() { TileTemplate = AdaptiveTileSizeEnum.TileMedium, Group = group2 }
                }
            };
            var tile = new TileElement() { visual = visual };
            var notification = new TileNotification(AdaptiveShellHelper.TrySerializeTileTemplateToXml(tile));
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private void CreateToastNotification()
        {
            var toastVisual = new ToastVisualElement()
            {
                version = 3,
                bindingElement = new ToastBindingElement()
                {
                    template = AdapiveToastBindingEnum.ToastGeneric,
                    ChildElements = new IChildElement[]
                    {
                        new ToastTextElement() { text=this.ToastTitleTbx.Text },
                        new ToastTextElement() { text=this.ToastContentTbx.Text },
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
