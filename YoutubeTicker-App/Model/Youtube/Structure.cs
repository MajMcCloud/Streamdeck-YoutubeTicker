using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace YoutubeTicker.Model.Youtube
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class A11ySkipNavigationButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class Accessibility
    {
        public AccessibilityData accessibilityData { get; set; }
        public string label { get; set; }
    }

    public class AccessibilityData
    {
        public string label { get; set; }
        public AccessibilityData accessibilityData { get; set; }
    }

    //public class Action
    //{
    //    public string clickTrackingParams { get; set; }
    //    public AddToPlaylistCommand addToPlaylistCommand { get; set; }
    //    public SignalAction signalAction { get; set; }
    //    public OpenPopupAction openPopupAction { get; set; }
    //    public string addedVideoId { get; set; }
    //    public string action { get; set; }
    //    public string removedVideoId { get; set; }
    //}

    public class AddToPlaylistCommand
    {
        public bool openMiniplayer { get; set; }
        public string videoId { get; set; }
        public string listType { get; set; }
        public OnCreateListCommand onCreateListCommand { get; set; }
        public List<string> videoIds { get; set; }
    }

    public class AddToPlaylistServiceEndpoint
    {
        public string videoId { get; set; }
    }

    public class Avatar
    {
        public List<Thumbnail> thumbnails { get; set; }
        public Accessibility accessibility { get; set; }
    }

    public class BackButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class Badge
    {
        public MetadataBadgeRenderer metadataBadgeRenderer { get; set; }
    }

    public class Banner
    {
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class BrowseEndpoint
    {
        public string browseId { get; set; }
        public string @params { get; set; }
        public string canonicalBaseUrl { get; set; }
    }

    public class ButtonRenderer
    {
        public string style { get; set; }
        public string size { get; set; }
        public bool isDisabled { get; set; }
        public Icon icon { get; set; }
        public Accessibility accessibility { get; set; }
        public string trackingParams { get; set; }
        public AccessibilityData accessibilityData { get; set; }
        public Text text { get; set; }
        public ServiceEndpoint serviceEndpoint { get; set; }
        public Command command { get; set; }
        public string tooltip { get; set; }
    }

    public class ButtonText
    {
        public List<Run> runs { get; set; }
    }

    //public class C4TabbedHeaderRenderer
    //{
    //    public string channelId { get; set; }
    //    public string title { get; set; }
    //    public NavigationEndpoint navigationEndpoint { get; set; }
    //    public Avatar avatar { get; set; }
    //    public Banner banner { get; set; }
    //    public List<Badge> badges { get; set; }
    //    public HeaderLinks headerLinks { get; set; }
    //    public SubscribeButton subscribeButton { get; set; }
    //    public VisitTracking visitTracking { get; set; }
    //    public SubscriberCountText subscriberCountText { get; set; }
    //    public TvBanner tvBanner { get; set; }
    //    public MobileBanner mobileBanner { get; set; }
    //    public string trackingParams { get; set; }
    //    public ChannelHandleText channelHandleText { get; set; }
    //    public VideosCountText videosCountText { get; set; }
    //}

    public class CancelButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class ChannelHandleText
    {
        public List<Run> runs { get; set; }
    }

    public class ChannelHeaderLinksRenderer
    {
        public List<PrimaryLink> primaryLinks { get; set; }
        public List<SecondaryLink> secondaryLinks { get; set; }
    }

    public class ChannelMetadataRenderer
    {
        public string title { get; set; }
        public string description { get; set; }
        public string rssUrl { get; set; }
        public string channelConversionUrl { get; set; }
        public string externalId { get; set; }
        public string keywords { get; set; }
        public List<string> ownerUrls { get; set; }
        public Avatar avatar { get; set; }
        public string channelUrl { get; set; }
        public bool isFamilySafe { get; set; }
        public string facebookProfileId { get; set; }
        public List<string> availableCountryCodes { get; set; }
        public string androidDeepLink { get; set; }
        public string androidAppindexingLink { get; set; }
        public string iosAppindexingLink { get; set; }
        public string vanityChannelUrl { get; set; }
    }

    public class ChipCloudChipRenderer
    {
        public Text text { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public string trackingParams { get; set; }
        public bool isSelected { get; set; }
    }

    public class ClearButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class Command
    {
        public string clickTrackingParams { get; set; }
        public ShowReloadUiCommand showReloadUiCommand { get; set; }
        public CommandExecutorCommand commandExecutorCommand { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    //public class Command3
    //{
    //    public string clickTrackingParams { get; set; }
    //    public OpenPopupAction openPopupAction { get; set; }
    //}

    public class CommandExecutorCommand
    {
        public List<Command> commands { get; set; }
    }

    public class CommandMetadata
    {
        public WebCommandMetadata webCommandMetadata { get; set; }
    }

    public class CommonConfig
    {
        public string url { get; set; }
    }

    public class CompactLinkRenderer
    {
        public Icon icon { get; set; }
        public Title title { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public string trackingParams { get; set; }
        public string style { get; set; }
    }

    public class Config
    {
        public WebSearchboxConfig webSearchboxConfig { get; set; }
    }

    public class ConfirmButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class ConfirmDialogRenderer
    {
        public string trackingParams { get; set; }
        public List<DialogMessage> dialogMessages { get; set; }
        public ConfirmButton confirmButton { get; set; }
        public CancelButton cancelButton { get; set; }
        public bool primaryIsCancel { get; set; }
    }

    public class ConnectionErrorHeader
    {
        public List<Run> runs { get; set; }
    }

    public class ConnectionErrorMicrophoneLabel
    {
        public List<Run> runs { get; set; }
    }

    public class Content
    {



    }

    public class TabContent
    {
        public RichGridRenderer richGridRenderer { get; set; }
    }

    public class ItemContent
    {
        public RichItemRenderer RichItemRenderer { get; set; }
    }

    public class ItemDetailsContent
    {
        public VideoRenderer videoRenderer { get; set; }
    }

    //public class Content2
    //{
    //    public RichItemRenderer richItemRenderer { get; set; }
    //    public ContinuationItemRenderer continuationItemRenderer { get; set; }
    //    public ChipCloudChipRenderer chipCloudChipRenderer { get; set; }
    //    public TwoColumnBrowseResultsRenderer twoColumnBrowseResultsRenderer { get; set; }
    //}

    public class ContinuationCommand
    {
        public string token { get; set; }
        public string request { get; set; }
        public Command command { get; set; }
    }

    public class ContinuationEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public ContinuationCommand continuationCommand { get; set; }
    }

    public class ContinuationItemRenderer
    {
        public string trigger { get; set; }
        public ContinuationEndpoint continuationEndpoint { get; set; }
    }

    public class CreatePlaylistServiceEndpoint
    {
        public List<string> videoIds { get; set; }
        public string @params { get; set; }
    }

    public class DescriptionSnippet
    {
        public List<Run> runs { get; set; }
    }

    public class DesktopTopbarRenderer
    {
        public Logo logo { get; set; }
        public Searchbox searchbox { get; set; }
        public string trackingParams { get; set; }
        public string countryCode { get; set; }
        public List<TopbarButton> topbarButtons { get; set; }
        public HotkeyDialog hotkeyDialog { get; set; }
        public BackButton backButton { get; set; }
        public ForwardButton forwardButton { get; set; }
        public A11ySkipNavigationButton a11ySkipNavigationButton { get; set; }
        public VoiceSearchButton voiceSearchButton { get; set; }
    }

    public class DialogMessage
    {
        public List<Run> runs { get; set; }
    }

    public class DisabledHeader
    {
        public List<Run> runs { get; set; }
    }

    public class DisabledSubtext
    {
        public List<Run> runs { get; set; }
    }

    public class DismissButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class Endpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public BrowseEndpoint browseEndpoint { get; set; }
    }

    public class EntityBatchUpdate
    {
        public List<Mutation> mutations { get; set; }
        public Timestamp timestamp { get; set; }
    }

    public class ExampleQuery1
    {
        public List<Run> runs { get; set; }
    }

    public class ExampleQuery2
    {
        public List<Run> runs { get; set; }
    }

    public class ExitButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class ExpandableTabRenderer
    {
        public Endpoint endpoint { get; set; }
        public string title { get; set; }
        public bool selected { get; set; }
    }

    public class FeedFilterChipBarRenderer
    {
        public List<Content> contents { get; set; }
        public string trackingParams { get; set; }
        public string styleType { get; set; }
    }

    public class ForwardButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class FrameworkUpdates
    {
        public EntityBatchUpdate entityBatchUpdate { get; set; }
    }

    public class FusionSearchboxRenderer
    {
        public Icon icon { get; set; }
        public PlaceholderText placeholderText { get; set; }
        public Config config { get; set; }
        public string trackingParams { get; set; }
        public SearchEndpoint searchEndpoint { get; set; }
        public ClearButton clearButton { get; set; }
    }

    public class Header
    {
        public FeedFilterChipBarRenderer feedFilterChipBarRenderer { get; set; }
        //public C4TabbedHeaderRenderer c4TabbedHeaderRenderer { get; set; }
    }

    public class HeaderLinks
    {
        public ChannelHeaderLinksRenderer channelHeaderLinksRenderer { get; set; }
    }

    public class HotkeyAccessibilityLabel
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class HotkeyDialog
    {
        public HotkeyDialogRenderer hotkeyDialogRenderer { get; set; }
    }

    public class HotkeyDialogRenderer
    {
        public Title title { get; set; }
        public List<Section> sections { get; set; }
        public DismissButton dismissButton { get; set; }
        public string trackingParams { get; set; }
    }

    public class HotkeyDialogSectionOptionRenderer
    {
        public Label label { get; set; }
        public string hotkey { get; set; }
        public HotkeyAccessibilityLabel hotkeyAccessibilityLabel { get; set; }
    }

    public class HotkeyDialogSectionRenderer
    {
        public Title title { get; set; }
        public List<Option> options { get; set; }
    }

    public class Html5PlaybackOnesieConfig
    {
        public CommonConfig commonConfig { get; set; }
    }

    public class Icon
    {
        public string iconType { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class IconImage
    {
        public string iconType { get; set; }
    }

    public class Item
    {
        public MenuServiceItemRenderer menuServiceItemRenderer { get; set; }
        public CompactLinkRenderer compactLinkRenderer { get; set; }
    }

    public class Label
    {
        public List<Run> runs { get; set; }
    }

    public class LengthText
    {
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class LinkAlternate
    {
        public string hrefUrl { get; set; }
    }

    public class LoadingHeader
    {
        public List<Run> runs { get; set; }
    }

    public class Logo
    {
        public TopbarLogoRenderer topbarLogoRenderer { get; set; }
    }

    public class MainAppWebResponseContext
    {
        public string datasyncId { get; set; }
        public bool loggedOut { get; set; }
    }

    public class Menu
    {
        public MenuRenderer menuRenderer { get; set; }
    }

    public class MenuPopupRenderer
    {
        public List<Item> items { get; set; }
    }

    public class MenuRenderer
    {
        public List<Item> items { get; set; }
        public string trackingParams { get; set; }
        public Accessibility accessibility { get; set; }
        public MultiPageMenuRenderer multiPageMenuRenderer { get; set; }
    }

    public class MenuRequest
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class MenuServiceItemRenderer
    {
        public Text text { get; set; }
        public Icon icon { get; set; }
        public ServiceEndpoint serviceEndpoint { get; set; }
        public string trackingParams { get; set; }
        public bool isSelected { get; set; }
    }

    public class Metadata
    {
        public ChannelMetadataRenderer channelMetadataRenderer { get; set; }
    }

    public class MetadataBadgeRenderer
    {
        public Icon icon { get; set; }
        public string style { get; set; }
        public string tooltip { get; set; }
        public string trackingParams { get; set; }
        public AccessibilityData accessibilityData { get; set; }
    }

    public class Microformat
    {
        public MicroformatDataRenderer microformatDataRenderer { get; set; }
    }

    public class MicroformatDataRenderer
    {
        public string urlCanonical { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string siteName { get; set; }
        public string appName { get; set; }
        public string androidPackage { get; set; }
        public string iosAppStoreId { get; set; }
        public string iosAppArguments { get; set; }
        public string ogType { get; set; }
        public string urlApplinksWeb { get; set; }
        public string urlApplinksIos { get; set; }
        public string urlApplinksAndroid { get; set; }
        public string urlTwitterIos { get; set; }
        public string urlTwitterAndroid { get; set; }
        public string twitterCardType { get; set; }
        public string twitterSiteHandle { get; set; }
        public string schemaDotOrgType { get; set; }
        public bool noindex { get; set; }
        public bool unlisted { get; set; }
        public bool familySafe { get; set; }
        public List<string> availableCountries { get; set; }
        public List<LinkAlternate> linkAlternates { get; set; }
    }

    public class MicrophoneButtonAriaLabel
    {
        public List<Run> runs { get; set; }
    }

    public class MicrophoneOffPromptHeader
    {
        public List<Run> runs { get; set; }
    }

    public class MobileBanner
    {
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class ModifyChannelNotificationPreferenceEndpoint
    {
        public string @params { get; set; }
    }

    public class MovingThumbnailDetails
    {
        public List<Thumbnail> thumbnails { get; set; }
        public bool logAsMovingThumbnail { get; set; }
    }

    public class MovingThumbnailRenderer
    {
        public MovingThumbnailDetails movingThumbnailDetails { get; set; }
        public bool enableHoveredLogging { get; set; }
        public bool enableOverlay { get; set; }
    }

    public class MultiPageMenuRenderer
    {
        public List<Section> sections { get; set; }
        public string trackingParams { get; set; }
        public string style { get; set; }
        public bool showLoadingSpinner { get; set; }
    }

    public class MultiPageMenuSectionRenderer
    {
        public List<Item> items { get; set; }
        public string trackingParams { get; set; }
    }

    public class Mutation
    {
        public string entityKey { get; set; }
        public string type { get; set; }
        public Payload payload { get; set; }
    }

    public class NavigationEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public WatchEndpoint watchEndpoint { get; set; }
        public ContinuationCommand continuationCommand { get; set; }
        public BrowseEndpoint browseEndpoint { get; set; }
        public UrlEndpoint urlEndpoint { get; set; }
        public UploadEndpoint uploadEndpoint { get; set; }
        public SignalNavigationEndpoint signalNavigationEndpoint { get; set; }
    }

    //public class NotificationPreferenceButton
    //{
    //    public SubscriptionNotificationToggleButtonRenderer subscriptionNotificationToggleButtonRenderer { get; set; }
    //}

    public class NotificationTopbarButtonRenderer
    {
        public Icon icon { get; set; }
        public MenuRequest menuRequest { get; set; }
        public string style { get; set; }
        public string trackingParams { get; set; }
        public Accessibility accessibility { get; set; }
        public string tooltip { get; set; }
        public UpdateUnseenCountEndpoint updateUnseenCountEndpoint { get; set; }
        public int notificationCount { get; set; }
        public List<string> handlerDatas { get; set; }
    }

    public class OnCreateListCommand
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public CreatePlaylistServiceEndpoint createPlaylistServiceEndpoint { get; set; }
    }

    public class OnSubscribeEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SubscribeEndpoint subscribeEndpoint { get; set; }
    }

    public class OnUnsubscribeEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    //public class OpenPopupAction
    //{
    //    public Popup popup { get; set; }
    //    public string popupType { get; set; }
    //    public bool beReused { get; set; }
    //}

    public class Option
    {
        public HotkeyDialogSectionOptionRenderer hotkeyDialogSectionOptionRenderer { get; set; }
    }

    public class OwnerBadge
    {
        public MetadataBadgeRenderer metadataBadgeRenderer { get; set; }
    }

    public class Param
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Payload
    {
        public SubscriptionStateEntity subscriptionStateEntity { get; set; }
    }

    public class PermissionsHeader
    {
        public List<Run> runs { get; set; }
    }

    public class PermissionsSubtext
    {
        public List<Run> runs { get; set; }
    }

    public class PlaceholderHeader
    {
        public List<Run> runs { get; set; }
    }

    public class PlaceholderText
    {
        public List<Run> runs { get; set; }
    }

    public class PlaylistEditEndpoint
    {
        public string playlistId { get; set; }
        public List<Action> actions { get; set; }
    }

    //public class Popup
    //{
    //    public MenuPopupRenderer menuPopupRenderer { get; set; }
    //    public ConfirmDialogRenderer confirmDialogRenderer { get; set; }
    //    public MultiPageMenuRenderer multiPageMenuRenderer { get; set; }
    //    public VoiceSearchDialogRenderer voiceSearchDialogRenderer { get; set; }
    //}

    public class PrimaryLink
    {
        public NavigationEndpoint navigationEndpoint { get; set; }
        public Icon icon { get; set; }
        public Title title { get; set; }
    }

    public class PromptHeader
    {
        public List<Run> runs { get; set; }
    }

    public class PromptMicrophoneLabel
    {
        public List<Run> runs { get; set; }
    }

    public class PublishedTimeText
    {
        public string simpleText { get; set; }
    }

    public class ResponseContext
    {
        public List<ServiceTrackingParam> serviceTrackingParams { get; set; }
        public int maxAgeSeconds { get; set; }
        public MainAppWebResponseContext mainAppWebResponseContext { get; set; }
        public WebResponseContextExtensionData webResponseContextExtensionData { get; set; }
    }

    public class RichGridRenderer
    {
        public List<ItemContent> contents { get; set; }
        public string trackingParams { get; set; }
        //public Header header { get; set; }
        public string targetId { get; set; }
        public string style { get; set; }
    }

    public class RichItemRenderer
    {
        public ItemDetailsContent content { get; set; }
        //public string trackingParams { get; set; }
    }

    public class RichThumbnail
    {
        public MovingThumbnailRenderer movingThumbnailRenderer { get; set; }
    }

    //JSON Root
    public class ytInitialData
    {
        //public ResponseContext responseContext { get; set; }

        public Contents contents { get; set; }

        //public Header header { get; set; }
        //public Metadata metadata { get; set; }
        //public string trackingParams { get; set; }
        //public Topbar topbar { get; set; }
        //public Microformat microformat { get; set; }
        //public FrameworkUpdates frameworkUpdates { get; set; }

        /// <summary>
        /// Returns all videos from the videos tab.
        /// </summary>
        /// <returns></returns>
        public List<VideoRenderer> GetVideos()
        {
            if (contents?.twoColumnBrowseResultsRenderer == null)
                return new List<VideoRenderer>();

            if (contents.twoColumnBrowseResultsRenderer.tabs.Count < 2)
                return new List<VideoRenderer>();

            var streaming_tab = contents.twoColumnBrowseResultsRenderer.tabs.FirstOrDefault(a => a.tabRenderer != null && a.tabRenderer.title == "Videos");
            if (streaming_tab == null)
                return new List<VideoRenderer>();

            if (streaming_tab.tabRenderer?.content?.richGridRenderer?.contents == null)
                return new List<VideoRenderer>();

            var list = streaming_tab.tabRenderer.content.richGridRenderer.contents.Where(a => a.RichItemRenderer != null).Select(a => a.RichItemRenderer.content.videoRenderer).ToList();

            return list;
        }

        public List<VideoRenderer> GetLiveStreams()
        {
            if (contents?.twoColumnBrowseResultsRenderer == null)
                return new List<VideoRenderer>();

            if (contents.twoColumnBrowseResultsRenderer.tabs.Count < 3)
                return new List<VideoRenderer>();

            var streaming_tab = contents.twoColumnBrowseResultsRenderer.tabs.FirstOrDefault(a => a.tabRenderer != null && a.tabRenderer.title == "Live");
            if(streaming_tab==null)
                return new List<VideoRenderer>();

            if (streaming_tab.tabRenderer?.content?.richGridRenderer?.contents == null)
                return new List<VideoRenderer>();

            var list = streaming_tab.tabRenderer.content.richGridRenderer.contents.Where(a => a.RichItemRenderer != null).Select(a => a.RichItemRenderer.content.videoRenderer).ToList();

            return list;
        }
    }

    public class Contents
    {
        public TwoColumnBrowseResultsRenderer twoColumnBrowseResultsRenderer { get; set; }

    }

    public class Run
    {
        public string text { get; set; }
    }

    public class Searchbox
    {
        public FusionSearchboxRenderer fusionSearchboxRenderer { get; set; }
    }

    public class SearchEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SearchEndpoint searchEndpoint { get; set; }
        public string query { get; set; }
    }

    public class SecondaryLink
    {
        public NavigationEndpoint navigationEndpoint { get; set; }
        public Icon icon { get; set; }
        public Title title { get; set; }
    }

    public class Section
    {
        public MultiPageMenuSectionRenderer multiPageMenuSectionRenderer { get; set; }
        public HotkeyDialogSectionRenderer hotkeyDialogSectionRenderer { get; set; }
    }

    public class ServiceEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
        public PlaylistEditEndpoint playlistEditEndpoint { get; set; }
        public AddToPlaylistServiceEndpoint addToPlaylistServiceEndpoint { get; set; }
        public ModifyChannelNotificationPreferenceEndpoint modifyChannelNotificationPreferenceEndpoint { get; set; }
        public UnsubscribeEndpoint unsubscribeEndpoint { get; set; }
    }

    public class ServiceTrackingParam
    {
        public string service { get; set; }
        public List<Param> @params { get; set; }
    }

    public class ShortViewCountText
    {
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class ShowReloadUiCommand
    {
        public string targetId { get; set; }
    }

    public class SignalAction
    {
        public string signal { get; set; }
    }

    public class SignalNavigationEndpoint
    {
        public string signal { get; set; }
    }

    public class SignalServiceEndpoint
    {
        public string signal { get; set; }
        public List<Action> actions { get; set; }
    }

    public class State
    {
        public int stateId { get; set; }
        public int nextStateId { get; set; }
        public State state { get; set; }
    }

    public class State2
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    //public class SubscribeAccessibility
    //{
    //    public AccessibilityData accessibilityData { get; set; }
    //}

    //public class SubscribeButton
    //{
    //    public SubscribeButtonRenderer subscribeButtonRenderer { get; set; }
    //}

    //public class SubscribeButtonRenderer
    //{
    //    public ButtonText buttonText { get; set; }
    //    public bool subscribed { get; set; }
    //    public bool enabled { get; set; }
    //    public string type { get; set; }
    //    public string channelId { get; set; }
    //    public bool showPreferences { get; set; }
    //    public SubscribedButtonText subscribedButtonText { get; set; }
    //    public UnsubscribedButtonText unsubscribedButtonText { get; set; }
    //    public string trackingParams { get; set; }
    //    public UnsubscribeButtonText unsubscribeButtonText { get; set; }
    //    public SubscribeAccessibility subscribeAccessibility { get; set; }
    //    public UnsubscribeAccessibility unsubscribeAccessibility { get; set; }
    //    public NotificationPreferenceButton notificationPreferenceButton { get; set; }
    //    public string subscribedEntityKey { get; set; }
    //    public List<OnSubscribeEndpoint> onSubscribeEndpoints { get; set; }
    //    public List<OnUnsubscribeEndpoint> onUnsubscribeEndpoints { get; set; }
    //}

    public class SubscribedButtonText
    {
        public List<Run> runs { get; set; }
    }

    public class SubscribeEndpoint
    {
        public List<string> channelIds { get; set; }
        public string @params { get; set; }
    }

    public class SubscriberCountText
    {
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class SubscriptionNotificationToggleButtonRenderer
    {
        public List<State> states { get; set; }
        public int currentStateId { get; set; }
        public string trackingParams { get; set; }
        public Command command { get; set; }
        public string targetId { get; set; }
    }

    public class SubscriptionStateEntity
    {
        public string key { get; set; }
        public bool subscribed { get; set; }
    }

    [DebuggerDisplay("{tabRenderer.title}")]
    public class Tab
    {
        public TabRenderer tabRenderer { get; set; }

        //public ExpandableTabRenderer expandableTabRenderer { get; set; }
    }

    public class TabRenderer
    {
        //public Endpoint endpoint { get; set; }
        public string title { get; set; }
        public string trackingParams { get; set; }
        public bool? selected { get; set; }
        public TabContent content { get; set; }
    }

    public class Text
    {
        public List<Run> runs { get; set; }
        //public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class Thumbnail
    {
        public List<Thumbnail10> thumbnails { get; set; }
    }

    [DebuggerDisplay("{width}x{height}")]
    public class Thumbnail10
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class ThumbnailOverlay
    {
        public ThumbnailOverlayTimeStatusRenderer thumbnailOverlayTimeStatusRenderer { get; set; }
        //public ThumbnailOverlayToggleButtonRenderer thumbnailOverlayToggleButtonRenderer { get; set; }
        //public ThumbnailOverlayNowPlayingRenderer thumbnailOverlayNowPlayingRenderer { get; set; }
        //public ThumbnailOverlayResumePlaybackRenderer thumbnailOverlayResumePlaybackRenderer { get; set; }
    }

    public class ThumbnailOverlayNowPlayingRenderer
    {
        public Text text { get; set; }
    }

    public class ThumbnailOverlayResumePlaybackRenderer
    {
        public int percentDurationWatched { get; set; }
    }

    public class ThumbnailOverlayTimeStatusRenderer
    {
        public Text text { get; set; }
        public string style { get; set; }
    }

    public class ThumbnailOverlayToggleButtonRenderer
    {
        public bool isToggled { get; set; }
        public UntoggledIcon untoggledIcon { get; set; }
        public ToggledIcon toggledIcon { get; set; }
        public string untoggledTooltip { get; set; }
        public string toggledTooltip { get; set; }
        public UntoggledServiceEndpoint untoggledServiceEndpoint { get; set; }
        public ToggledServiceEndpoint toggledServiceEndpoint { get; set; }
        public UntoggledAccessibility untoggledAccessibility { get; set; }
        public ToggledAccessibility toggledAccessibility { get; set; }
        public string trackingParams { get; set; }
    }

    public class Timestamp
    {
        public string seconds { get; set; }
        public int nanos { get; set; }
    }

    public class Title
    {
        public List<Run> runs { get; set; }
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class ToggledAccessibility
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class ToggledIcon
    {
        public string iconType { get; set; }
    }

    public class ToggledServiceEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public PlaylistEditEndpoint playlistEditEndpoint { get; set; }
    }

    public class TooltipText
    {
        public List<Run> runs { get; set; }
    }

    public class Topbar
    {
        public DesktopTopbarRenderer desktopTopbarRenderer { get; set; }
    }

    public class TopbarButton
    {
        public TopbarMenuButtonRenderer topbarMenuButtonRenderer { get; set; }
        public NotificationTopbarButtonRenderer notificationTopbarButtonRenderer { get; set; }
    }

    public class TopbarLogoRenderer
    {
        public IconImage iconImage { get; set; }
        public TooltipText tooltipText { get; set; }
        public Endpoint endpoint { get; set; }
        public string trackingParams { get; set; }
        public string overrideEntityKey { get; set; }
    }

    public class TopbarMenuButtonRenderer
    {
        public Icon icon { get; set; }
        public MenuRenderer menuRenderer { get; set; }
        public string trackingParams { get; set; }
        public Accessibility accessibility { get; set; }
        public string tooltip { get; set; }
        public string style { get; set; }
        public Avatar avatar { get; set; }
        public MenuRequest menuRequest { get; set; }
    }

    public class TvBanner
    {
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class TwoColumnBrowseResultsRenderer
    {
        public List<Tab> tabs { get; set; }
    }

    public class UnsubscribeAccessibility
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class UnsubscribeButtonText
    {
        public List<Run> runs { get; set; }
    }

    public class UnsubscribedButtonText
    {
        public List<Run> runs { get; set; }
    }

    public class UnsubscribeEndpoint
    {
        public List<string> channelIds { get; set; }
        public string @params { get; set; }
    }

    public class UntoggledAccessibility
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class UntoggledIcon
    {
        public string iconType { get; set; }
    }

    public class UntoggledServiceEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public PlaylistEditEndpoint playlistEditEndpoint { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class UpdateUnseenCountEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class UploadEndpoint
    {
        public bool hack { get; set; }
    }

    public class UrlEndpoint
    {
        public string url { get; set; }
        public string target { get; set; }
        public bool nofollow { get; set; }
    }

    public class VideoRenderer
    {
        public string videoId { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Title title { get; set; }
        //public DescriptionSnippet descriptionSnippet { get; set; }
        //public PublishedTimeText publishedTimeText { get; set; }
        public LengthText lengthText { get; set; }
        public ViewCountText viewCountText { get; set; }
        //public NavigationEndpoint navigationEndpoint { get; set; }
        //public List<OwnerBadge> ownerBadges { get; set; }
        //public string trackingParams { get; set; }
        //public bool showActionMenu { get; set; }
        //public ShortViewCountText shortViewCountText { get; set; }
        //public Menu menu { get; set; }
        public List<ThumbnailOverlay> thumbnailOverlays { get; set; }
        //public RichThumbnail richThumbnail { get; set; }
        //public bool? isWatched { get; set; }

        public override string ToString()
        {
            return videoId + ": " + (title?.simpleText ?? title.runs[0].text);
        }
    }

    public class VideosCountText
    {
        public List<Run> runs { get; set; }
    }

    public class ViewCountText
    {
        public string simpleText { get; set; }
    }

    public class VisitTracking
    {
        public string remarketingPing { get; set; }
    }

    public class VoiceSearchButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class VoiceSearchDialogRenderer
    {
        public PlaceholderHeader placeholderHeader { get; set; }
        public PromptHeader promptHeader { get; set; }
        public ExampleQuery1 exampleQuery1 { get; set; }
        public ExampleQuery2 exampleQuery2 { get; set; }
        public PromptMicrophoneLabel promptMicrophoneLabel { get; set; }
        public LoadingHeader loadingHeader { get; set; }
        public ConnectionErrorHeader connectionErrorHeader { get; set; }
        public ConnectionErrorMicrophoneLabel connectionErrorMicrophoneLabel { get; set; }
        public PermissionsHeader permissionsHeader { get; set; }
        public PermissionsSubtext permissionsSubtext { get; set; }
        public DisabledHeader disabledHeader { get; set; }
        public DisabledSubtext disabledSubtext { get; set; }
        public MicrophoneButtonAriaLabel microphoneButtonAriaLabel { get; set; }
        public ExitButton exitButton { get; set; }
        public string trackingParams { get; set; }
        public MicrophoneOffPromptHeader microphoneOffPromptHeader { get; set; }
    }

    public class WatchEndpoint
    {
        public string videoId { get; set; }
        public WatchEndpointSupportedOnesieConfig watchEndpointSupportedOnesieConfig { get; set; }
    }

    public class WatchEndpointSupportedOnesieConfig
    {
        public Html5PlaybackOnesieConfig html5PlaybackOnesieConfig { get; set; }
    }

    public class WebCommandMetadata
    {
        public string url { get; set; }
        public string webPageType { get; set; }
        public int rootVe { get; set; }
        public string apiUrl { get; set; }
        public bool sendPost { get; set; }
    }

    public class WebResponseContextExtensionData
    {
        public YtConfigData ytConfigData { get; set; }
        public bool hasDecorated { get; set; }
    }

    public class WebSearchboxConfig
    {
        public string requestLanguage { get; set; }
        public string requestDomain { get; set; }
        public bool hasOnscreenKeyboard { get; set; }
        public bool focusSearchbox { get; set; }
    }

    public class YtConfigData
    {
        public string visitorData { get; set; }
        public int sessionIndex { get; set; }
        public int rootVisualElementType { get; set; }
    }


}
