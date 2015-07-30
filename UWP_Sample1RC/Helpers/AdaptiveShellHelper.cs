using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;

namespace UWP_Sample1RC.Helpers
{
    public sealed class AdaptiveShellHelper
    {
        public static Windows.Data.Xml.Dom.XmlDocument TrySerializeTileTemplateToXml(TileElement tile)
        {
            var docResult = new Windows.Data.Xml.Dom.XmlDocument();
            var stringBuilder = new StringBuilder();
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            try
            {
                var serializer = new XmlSerializer(tile.GetType());
                using (var writer = XmlWriter.Create(stringBuilder, new XmlWriterSettings() { OmitXmlDeclaration = true }))
                {
                    serializer.Serialize(writer, tile, xns);
                }
                docResult.LoadXml(stringBuilder.ToString());
                System.Diagnostics.Debug.WriteLine(docResult.GetXml());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return docResult;
        }

        public static Windows.Data.Xml.Dom.XmlDocument TrySerializeToastTemplateToXml(ToastElement toast)
        {
            var docResult = new Windows.Data.Xml.Dom.XmlDocument();
            var stringBuilder = new StringBuilder();
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            try
            {
                var serializer = new XmlSerializer(toast.GetType());
                using (var writer = XmlWriter.Create(stringBuilder, new XmlWriterSettings() { OmitXmlDeclaration = true }))
                {
                    serializer.Serialize(writer, toast, xns);
                }
                docResult.LoadXml(stringBuilder.ToString());
                System.Diagnostics.Debug.WriteLine(docResult.GetXml());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return docResult;
        }
    }

    public interface IChildElement
    {

    }

    #region Tile Class elements
    public sealed class TextElement : IChildElement
    {
        [XmlText]
        public string StringData { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlAttribute("hint-maxLines")]
        public int MaxLines { get; set; }

        [XmlAttribute("hint-minLines")]
        public int MinLines { get; set; }

        [XmlAttribute("hint-wrap")]
        public bool IsWrap { get; set; }

        [XmlAttribute("hint-align")]
        public AdaptiveTileTextAlignEnum TextAlign { get; set; }

        [XmlAttribute("hint-style")]
        public AdaptiveTileTextHintStyleEnum TextStyle { get; set; }
    }

    public sealed class ImageElement : IChildElement
    {
        [XmlAttribute("src")]
        public string ImageSource { get; set; }

        [XmlAttribute("alt")]
        public string ImageAlt { get; set; }

        [XmlAttribute("addImageQuery")]
        public bool IsAddImageQuery { get; set; }

        [XmlAttribute("hint-removeMargin")]
        public bool IsRemoveMargin { get; set; }

        [XmlAttribute("placement")]
        public AdaptiveTileImagePlacementEnum ImagePlacement { get; set; }

        [XmlAttribute("hint-crop")]
        public AdaptiveTileImageCropEnum ImageCrop { get; set; }

        [XmlAttribute("hint-align")]
        public AdaptiveTileImageAlignEnum ImageAlign { get; set; }
    }

    public sealed class SubGroupElement
    {
        [XmlAttribute("hint-weight")]
        public int weight { get; set; }

        [XmlAttribute("hint-textStacking")]
        public AdaptiveTileSubGroupTextStacking TaxtStacking { get; set; }

        [XmlElement(Type = typeof(ImageElement), ElementName = "image")]
        [XmlElement(Type = typeof(TextElement), ElementName = "text")]
        public object[] ChildElements { get; set; }
    }

    public sealed class GroupElement
    {
        [XmlElement("subgroup")]
        public SubGroupElement[] Subgroups { get; set; }
    }

    public sealed class TileBindingElement
    {
        [XmlAttribute("template")]
        public AdaptiveTileSizeEnum TileTemplate { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        //[XmlAttribute("baseUri")]
        //public Uri BaseUri { get; set; }

        [XmlAttribute("branding")]
        public AdaptiveTileBrandingEnum TileBranding { get; set; }

        [XmlAttribute("addImageQuery")]
        public bool IsAddImageQuery { get; set; }

        //private string _ContentId;
        [XmlAttribute("contentId")]
        public string ContentId { get; set; }

        [XmlAttribute("dislayName")]
        public string DisplayName { get; set; }

        [XmlAttribute("hint-textStacking")]
        public AdaptiveTileSubGroupTextStacking TextStacking { get; set; }

        [XmlAttribute("hint-overlay")]
        public int Overlay { get; set; }

        [XmlElement("group")]
        public GroupElement Group { get; set; }
    }

    public sealed class TileVisualElement
    {
        [XmlAttribute("version")]
        public int version { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlAttribute("baseUri")]
        public string baseUri { get; set; }

        [XmlAttribute("branding")]
        public AdaptiveTileBrandingEnum branding { get; set; }

        [XmlAttribute("addImageQuery")]
        public bool addImageQuery { get; set; }

        [XmlAttribute("contentId")]
        public string contentId { get; set; }

        [XmlAttribute("displayName")]
        public string displayName { get; set; }

        [XmlElement(Type = typeof(TileBindingElement), ElementName = "binding")]
        public object[] BindingElements { get; set; }
    }

    [XmlRoot(ElementName = "tile")]
    public sealed class TileElement
    {
        [XmlElement("visual")]
        public TileVisualElement visual { get; set; }
    }
    #endregion

    #region Toast Class elements
    public sealed class ToastTextElement : IChildElement
    {
        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlText]
        public string text { get; set; }
    }

    public sealed class ToastImageElement : IChildElement
    {
        [XmlAttribute("src")]
        public string imageSource { get; set; }

        [XmlAttribute("placement")]
        public AdaptiveToastImagePlacementEnum placement { get; set; }

        [XmlAttribute("alt")]
        public string alt { get; set; }

        [XmlAttribute("addImageQuery")]
        public bool IsAddImageQuery { get; set; }

        [XmlAttribute("hint-crop")]
        public AdaptiveToastCropEnum hintCrop { get; set; }
    }

    public sealed class ToastAudioElement
    {
        [XmlAttribute("src")]
        public string audioSource { get; set; }

        [XmlAttribute("loop")]
        public bool IsLoop { get; set; }

        [XmlAttribute("silent")]
        public bool IsSilent { get; set; }
    }

    public sealed class ToastSelectionElement
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("content")]
        public string content { get; set; }
    }

    public sealed class ToastInputElement : IChildElement
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("type")]
        public AdaptiveToastInputTypeEnum type { get; set; }

        [XmlAttribute("title")]
        public string title { get; set; }

        [XmlAttribute("placeHolderContent")]
        public string PlaceholderContent { get; set; }

        [XmlAttribute("defaultInput")]
        public string defaultInput { get; set; }

        [XmlElement("selection")]
        public ToastSelectionElement[] InputSelections { get; set; }
    }

    public sealed class ToastActionElement : IChildElement
    {
        [XmlAttribute("content")]
        public string content { get; set; }

        [XmlAttribute("arguments")]
        public string arguments { get; set; }

        [XmlAttribute("activationType")]
        public AdaptiveToastActiivationType activationType { get; set; }

        [XmlAttribute("imageUri")]
        public string imageUri { get; set; }

        [XmlAttribute("hint-inputId")]
        public string inputId { get; set; }
    }

    public sealed class ToastActionsElement
    {
        [XmlAttribute("hint-systemCommand")]
        public AdaptiveToastSystemCommandEnum systemCommand { get; set; }

        [XmlElement(Type = typeof(ToastActionElement), ElementName = "action")]
        [XmlElement(Type = typeof(ToastInputElement), ElementName = "input")]
        public object[] ChildActions { get; set; }
    }

    public sealed class ToastBindingElement
    {
        [XmlAttribute("template")]
        public AdapiveToastBindingEnum template { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlAttribute("baseUri")]
        public string baseUri { get; set; }

        [XmlAttribute("addImageQuery")]
        public bool addImageQuery { get; set; }


        [XmlElement(Type = typeof(ToastImageElement), ElementName = "image")]
        [XmlElement(Type = typeof(ToastTextElement), ElementName = "text")]
        public object[] ChildElements { get; set; }

        [XmlElement("audio")]
        public ToastAudioElement audio { get; set; }
    }

    public sealed class ToastVisualElement
    {
        [XmlAttribute("version")]
        public int version { get; set; }

        [XmlAttribute("lang")]
        public string Language { get; set; }

        [XmlAttribute("baseUri")]
        public string baseUri { get; set; }

        [XmlAttribute("addImageQuery")]
        public bool addImageQuery { get; set; }

        [XmlElement("binding")]
        public ToastBindingElement bindingElement { get; set; }
    }

    [XmlRoot(ElementName = "toast")]
    public sealed class ToastElement
    {
        [XmlAttribute("launch")]
        public string launch { get; set; }

        [XmlAttribute("duration")]
        public AdaptiveToastDurationEnum duration { get; set; }

        [XmlAttribute("activationType")]
        public AdaptiveToastActiivationType activationType { get; set; }

        [XmlAttribute("scenario")]
        public AdaptiveToastScenario scenario { get; set; }

        [XmlAttribute("hint-people")]
        public string hintPeople { get; set; }

        [XmlElement("visual")]
        public ToastVisualElement visual { get; set; }

        [XmlElement("actions")]
        public ToastActionsElement actions { get; set; }

        [XmlElement("audio")]
        public ToastAudioElement audio { get; set; }
    }
    #endregion

    #region Tile enum region
    public enum AdaptiveTileSizeEnum
    {
        [XmlEnum("TileSmall")]
        TileSmall,
        [XmlEnum("TileMedium")]
        TileMedium,
        [XmlEnum("TileWide")]
        TileWide,
        [XmlEnum("TileLarge")]
        TileLarge
    }

    public enum AdaptiveTileBrandingEnum
    {
        [XmlEnum("none")]
        none,
        [XmlEnum("logo")]
        logo,
        [XmlEnum("name")]
        name,
        [XmlEnum("nameAndLogo")]
        nameAndLogo
    }

    public enum AdaptiveTileTextHintStyleEnum
    {
        [XmlEnum("caption")]
        caption,
        [XmlEnum("captionSubtle")]
        captionSubtle,
        [XmlEnum("body")]
        body,
        [XmlEnum("bodySubtle")]
        bodySubtle,
        [XmlEnum("base")]
        @base,
        [XmlEnum("baseSubtle")]
        baseSubtle,
        [XmlEnum("subtitle")]
        subtitle,
        [XmlEnum("subtitleSubtle")]
        subtitleSubtle,
        [XmlEnum("tile")]
        tile,
        [XmlEnum("tileSubtle")]
        tileSubtle,
        [XmlEnum("tileNumeral")]
        tileNumeral,
        [XmlEnum("subheader")]
        subheader,
        [XmlEnum("subheaderSubtle")]
        subheaderSubtle,
        [XmlEnum("nameAndLogo")]
        subheaderNumeral,
        [XmlEnum("header")]
        header,
        [XmlEnum("headerSubtle")]
        headerSubtle,
        [XmlEnum("headerNumber")]
        headerNumber
    }

    public enum AdaptiveTileTextAlignEnum
    {
        [XmlEnum("left")]
        left,
        [XmlEnum("center")]
        center,
        [XmlEnum("right")]
        right
    }

    public enum AdaptiveTileImagePlacementEnum
    {
        [XmlEnum("inline")]
        inline,
        [XmlEnum("background")]
        background,
        [XmlEnum("peek")]
        peek
    }

    public enum AdaptiveTileImageCropEnum
    {
        [XmlEnum("none")]
        none,
        [XmlEnum("circle")]
        circle
    }

    public enum AdaptiveTileImageAlignEnum
    {
        [XmlEnum("stretch")]
        stretch,
        [XmlEnum("left")]
        left,
        [XmlEnum("center")]
        center,
        [XmlEnum("right")]
        right
    }

    public enum AdaptiveTileSubGroupTextStacking
    {
        [XmlEnum("top")]
        top,
        [XmlEnum("center")]
        center,
        [XmlEnum("bottom")]
        bottom
    }
    #endregion

    #region Toast enum region
    public enum AdaptiveToastSystemCommandEnum
    {
        [XmlEnum("SnoozeAndDismiss")]
        SnoozeAndDismiss
    }

    public enum AdaptiveToastInputTypeEnum
    {
        [XmlEnum("text")]
        text,
        [XmlEnum("selection")]
        selection
    }

    public enum AdaptiveToastImagePlacementEnum
    {
        [XmlEnum("inline")]
        inline,
        [XmlEnum("appLogoOverride")]
        appLogoOverride
    }

    public enum AdaptiveToastCropEnum
    {
        [XmlEnum("none")]
        none,
        [XmlEnum("circle")]
        circle
    }

    public enum AdapiveToastBindingEnum
    {
        [XmlEnum("ToastGeneric")]
        ToastGeneric
    }

    public enum AdaptiveToastDurationEnum
    {
        [XmlEnum("short")]
        @short,
        [XmlEnum("long")]
        @long
    }

    public enum AdaptiveToastActiivationType
    {
        [XmlEnum("foreground")]
        foreground,
        [XmlEnum("background")]
        background,
        [XmlEnum("protocol")]
        protocol
    }

    public enum AdaptiveToastAudioSource
    {
        [XmlEnum("ms-winsoundevent:Notification.Default")]
        @Default,
        [XmlEnum("ms-winsoundevent:Notification.Reminder")]
        Reminder,
        [XmlEnum("ms-winsoundevent:Notification.IM")]
        IM,
        [XmlEnum("ms-winsoundevent:Notification.Mail")]
        Mail,
        [XmlEnum("ms-winsoundevent:Notification.SMS")]
        SMS,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm")]
        Alarm,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm2")]
        Alarm2,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm3")]
        Alarm3,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm4")]
        Alarm4,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm5")]
        Alarm5,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm6")]
        Alarm6,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm7")]
        Alarm7,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm8")]
        Alarm8,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm9")]
        Alarm9,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Alarm10")]
        Alarm10,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call")]
        Call,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call2")]
        Call2,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call3")]
        Call3,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call4")]
        Call4,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call5")]
        Call5,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call6")]
        Call6,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call7")]
        Call7,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call8")]
        Call8,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call9")]
        Call9,
        [XmlEnum("ms-winsoundevent:Notification.Looping.Call10")]
        Call10
    }

    public enum AdaptiveToastScenario
    {
        [XmlEnum("default")]
        @default,
        [XmlEnum("alarm")]
        alarm,
        [XmlEnum("reminder")]
        reminder,
        [XmlEnum("incomingCall")]
        incomingCall,

    }
    #endregion
}
