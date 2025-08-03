using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CommandLine;
using IronSoftware.Drawing;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using streamdeck_client_csharp;
using streamdeck_client_csharp.Events;
using StreamDeckBase;
using YoutubeTicker.lib;
using YoutubeTicker.Model.Youtube;

using SKBitmap = SixLabors.ImageSharp.Image;
using SKRectangle = IronSoftware.Drawing.Rectangle;

namespace YoutubeTicker
{
    class Program
    {

        public static TileManager Manager { get; set; }


        public static StreamDeckConnection connection
        {
            get
            {
                return Manager.connection;
            }
        }

        public static Dictionary<string, JObject> settings
        {
            get
            {
                return Manager.TileSettings;
            }
        }

        public static List<VideoEntry> VideoCache { get; set; } = new List<VideoEntry>();

        public static System.Timers.Timer timer { get; set; }

        public static List<SixLabors.Fonts.Font> Fonts { get; set; } = new List<SixLabors.Fonts.Font>();

        //public static System.Timers.Timer tmInternet { get; set; }

        public static bool Loading { get; set; } = false;

        // StreamDeck launches the plugin with these details
        // -port [number] -pluginUUID [GUID] -registerEvent [string?] -info [json]
        static void Main(string[] args)
        {
            // Uncomment this line of code to allow for debugging
            //while (!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}

            // The command line args parser expects all args to use `--`, so, let's append
            for (int count = 0; count < args.Length; count++)
            {
                if (args[count].StartsWith("-") && !args[count].StartsWith("--"))
                {
                    args[count] = $"-{args[count]}";
                }
            }



            //var b = (AnyBitmap)Resources.nowlive_4;

            //var bytes = b.ToString();

            var set = cache.load();
            if (set != null)
                VideoCache = set.Cache;


            if (!Directory.Exists("images"))
                Directory.CreateDirectory("images");

            Fonts = CollectFonts();

            Parser parser = new Parser((with) =>
            {
                with.EnableDashDash = true;
                with.CaseInsensitiveEnumValues = true;
                with.CaseSensitive = false;
                with.IgnoreUnknownArguments = true;
                with.HelpWriter = Console.Error;
            });

            ParserResult<Options> options = parser.ParseArguments<Options>(args);
            options.WithParsed(o => RunPlugin(o));

        }

        static void RunPlugin(Options options)
        {
            timer = new System.Timers.Timer();

            timer.Elapsed += Timer_Elapsed;

            Manager = new TileManager(options);

            Manager.Start();

            Manager.PageOpened += Manager_PageOpened;

            connection.OnWillAppear += Connection_OnWillAppear;

            connection.OnDidReceiveSettings += Connection_OnDidReceiveSettings;

            connection.OnWillDisappear += Connection_OnWillDisappear;

            connection.OnKeyUp += Connection_OnKeyUp;

            connection.OnSendToPlugin += Connection_OnSendToPlugin;

            Manager.WaitForStop();


            CleanupAndSaveCache();

        }

        private static async void Manager_PageOpened(object sender, EventArgs args)
        {
            if (await CheckEvents(true))
            {
                return;
            }

            //Timer_Elapsed(null, null);

            Debug.WriteLine($"PageOpened");

            var cfg = settings.FirstOrDefault().Value ?? new JObject();

            if (!timer.Enabled && cfg["refreshInterval"] != null)
            {
                timer.Interval = (double)cfg["refreshInterval"] * 1000 * 60;
                timer.Start();
            }
        }

        private static async void Connection_OnWillAppear(object sender, StreamDeckEventReceivedEventArgs<WillAppearEvent> args)
        {
#if DEBUG
            Debug.WriteLine("OnWillAppear " + args.Event.Context);
#endif

            //var cfg = new JObject();

            //    //System.Diagnostics.Debug.WriteLine("Appear " + args.Event.Action);

            settings[args.Event.Context] = args.Event.Payload.Settings;
            if (settings[args.Event.Context] == null)
            {
                settings[args.Event.Context] = new JObject();

            }

            Debug.WriteLine($"Appear: {args.Event.Context}");

            //cfg = settings[args.Event.Context];

            if (await CheckEvents(false))
            {
                return;
            }

            StreamDeckTileConfig cfg = new StreamDeckTileConfig(settings[args.Event.Context]);

            if (!timer.Enabled && cfg.RefreshInterval != null)
            {
                timer.Interval = cfg.RefreshInterval.Value * 1000 * 60;
                timer.Start();
            }

            await RefreshTile(args.Event.Context, cfg);
        }

        private static async void Connection_OnDidReceiveSettings(object sender, StreamDeckEventReceivedEventArgs<DidReceiveSettingsEvent> args)
        {
#if DEBUG
            Debug.WriteLine("Settings " + args.Event.Context);
#endif

            //var cfg = new JObject();

            //System.Diagnostics.Debug.WriteLine("Settings " + args.Event.Action);


            settings[args.Event.Context] = args.Event.Payload.Settings;
            if (settings[args.Event.Context] == null)
            {
                settings[args.Event.Context] = new JObject();
            }

            //cfg = settings[args.Event.Context];

            StreamDeckTileConfig cfg = new StreamDeckTileConfig(settings[args.Event.Context]);

            timer.Stop();

            timer.Interval = cfg.RefreshInterval.Value * 1000 * 60;

            timer.Start();

            if (cfg.ChannelUrl.StartsWith("https://www.youtube.com/watch?v"))
            {
                await connection.ShowAlertAsync(args.Event.Context);
                return;
            }

            var entry = VideoCache.GetVideoEntry(args.Event.Context, cfg.ChannelUrl);

            if (entry != null)
            {
                if (entry.ChannelUrl != cfg.ChannelUrl)
                {
                    entry.ResetImageCache();

                    VideoCache.Remove(entry);
                }
                if (entry.PreviewType != cfg.Preview)
                {
                    await RefreshTile(args.Event.Context, cfg, true);
                    return;
                }
            }

            await RefreshTile(args.Event.Context, cfg);
        }

        private static void Connection_OnWillDisappear(object sender, StreamDeckEventReceivedEventArgs<WillDisappearEvent> args)
        {
#if DEBUG
            Debug.WriteLine("OnWillDisappear " + args.Event.Context);
#endif

            lock (settings)
            {
                if (settings.ContainsKey(args.Event.Context))
                {
                    settings.Remove(args.Event.Context);
                }
            }


            timer.Stop();
        }

        private static async void Connection_OnKeyUp(object sender, StreamDeckEventReceivedEventArgs<KeyUpEvent> args)
        {
            StreamDeckTileConfig cfg = new StreamDeckTileConfig(settings[args.Event.Context]);
            if (!cfg.IsSet())
            {
                return;
            }



            try
            {
                switch (cfg.Event)
                {
                    case "open":

                        OpenUrl(cfg.ChannelUrl);

                        break;

                    case "openlatest":

                        var entry = VideoCache.GetVideoEntry(args.Event.Context, cfg.ChannelUrl); //.FirstOrDefault(a => a.SDContext == args.Event.Context);// settings[args.Event.Context]["details"].ToObject(typeof(VideoEntry)) as VideoEntry;

                        if (entry == null)
                        {
                            return;
                        }

                        (String url, String id, String length, String thumbnailurl)? vid = (entry.VideoUrl, entry.LastVideo, entry.VideoLength, entry.LatestVideoThumbnailUrl);

                        //Search for latest
                        if (vid == null | vid?.url == null || vid?.url == "")
                        {
                            vid = await GetLatestVideo(cfg.ChannelUrl, cfg.Preview, args.Event.Context);
                        }

                        if (vid == null || vid?.url.Trim() == "")
                        {
                            entry.VideoLength = "no video";
                            return;
                        }

                        entry.VideoLength = vid?.length;

                        NameValueCollection nvc = HttpUtility.ParseQueryString(new Uri(vid?.url).Query);

                        //Mark video as viewed
                        if (nvc["v"] != null)
                        {
                            entry.LastVideo = nvc["v"];

                            //cfg["details"] = JToken.FromObject(entry);

                            //await connection.SetSettingsAsync(cfg, args.Event.Context);
                        }

                        await Render(args.Event.Context);

                        OpenUrl(vid?.url);

                        break;

                    case "openvideos":

                        var entry2 = VideoCache.GetVideoEntry(args.Event.Context, cfg.ChannelUrl); //.FirstOrDefault(a => a.SDContext == args.Event.Context);// settings[args.Event.Context]["details"].ToObject(typeof(VideoEntry)) as VideoEntry;

                        if (entry2 == null)
                        {
                            return;
                        }

                        String video_uri = entry2.ChannelUrl;

                        video_uri = video_uri.TrimEnd('/');

                        video_uri += "/videos";

                        OpenUrl(video_uri);

                        break;

                    case "refresh":

                        await LoadChannel(args.Event.Context, cfg, true);

                        break;

                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                Debugger.Break();
            }
        }

        private static async void Connection_OnSendToPlugin(object sender, StreamDeckEventReceivedEventArgs<SendToPluginEvent> args)
        {
            if (args.Event.Payload.Count == 0 || args.Event.Payload.GetValue("mode") == null)
            {
                return;
            }

            switch (args.Event.Payload["mode"].ToString())
            {
                case "cache-clear-image":


                    CleanupImageCache();

                    await ForceRefresh();

                    await connection.ShowOkAsync(args.Event.Context);

                    break;

                case "cache-clear-full":


                    CleanupImageCache();

                    VideoCache.Clear();

                    await ForceRefresh();

                    await connection.ShowOkAsync(args.Event.Context);

                    break;

                case "cache-clear-refresh":

                    await ForceRefresh();

                    break;

                case "openPluginFolder":


                    OpenUrl(Directory.GetCurrentDirectory());

                    break;


            }
        }

        private static async Task<bool> CheckEvents(bool UpdateImage = true)
        {

            if (DateTime.Today.Month == 12 && (DateTime.Today.Day >= 24 && DateTime.Today.Day <= 26))
            {
                if (!UpdateImage)
                    return true;

                await Manager.SetImageForAll(StaticImages.Christmas);

                new Thread(() =>
                {
                    Thread.Sleep(5000);

                    Timer_Elapsed(null, null);

                }).Start();

                return true;
            }

            if (DateTime.Today.Month == 12 && DateTime.Today.Day == 31)
            {
                if (!UpdateImage)
                    return true;

                await Manager.SetImageForAll(StaticImages.Sylvester);

                new Thread(() =>
                {
                    Thread.Sleep(5000);

                    Timer_Elapsed(null, null);

                }).Start();

                return true;
            }

            if (DateTime.Today.Month == 1 && DateTime.Today.Day == 1)
            {
                if (!UpdateImage)
                    return true;

                await Manager.SetImageForAll(StaticImages.NewYears);

                new Thread(() =>
                {
                    Thread.Sleep(5000);

                    Timer_Elapsed(null, null);

                }).Start();

                return true;
            }



            return false;
        }




        private static async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Loading = true;

            foreach (var c in settings.Reverse())
            {
                if (settings[c.Key] == null)
                {
                    continue;
                }

                StreamDeckTileConfig cfg = new StreamDeckTileConfig(settings[c.Key]);

                Task.Factory.StartNew(async () =>
                {
                    await RefreshTile(c.Key, cfg);
                });

                //await Task.Run(async () =>
                //{
                //    await RefreshTile(c.Key, cfg);
                //});
            }

            Loading = false;

            //Refresh cache
            var cache = new cache(VideoCache);
            cache.save();
        }

        private static async Task RefreshTile(String context, StreamDeckTileConfig tileConfig, bool force = false)
        {
            JObject settings = tileConfig.ToJObject();

            if (!tileConfig.IsSet())
                return;



            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await LoadChannel(context, tileConfig, force);
                }
                catch (Exception ex)
                {
                    LogError(ex);

                    Debugger.Break();
                }

            });

        }




        private static void CleanupImageCache()
        {
            foreach (var ve in VideoCache)
            {

                ve.ResetImageCache();

            }

            var base_path = Directory.GetCurrentDirectory();

            //Security
            if (!base_path.Contains("com.dahnandpartners.ytticker.sdPlugin", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (!Directory.Exists("images"))
            {
                return;
            }

            var images = Directory.GetFiles("images");

            foreach (var img in images)
            {
                try
                {
                    File.Delete(img);
                }
                catch
                {

                }
            }

        }

        private static void CleanupAndSaveCache()
        {
            //Remove duplicates and other wrong items
            Cleanup();

            var cache = new cache(VideoCache);
            cache.save();
        }

        private static async Task ForceRefresh()
        {
            foreach (var set in settings)
            {
                StreamDeckTileConfig cfg = new StreamDeckTileConfig(set.Value);

                await RefreshTile(set.Key, cfg, true);
            }
        }


        private static async Task RenderNowLive(String context, int repead_count = 3)
        {

            for (int i = 0; i < repead_count; i++)
            {
                AnyBitmap bmp = StaticImages.Now_Live_1; // Resources.nowlive_1;
                await connection.SetImageAsync(bmp, context, SDKTarget.HardwareAndSoftware, null);

                Thread.Sleep(800);

                bmp = StaticImages.Now_Live_2;
                await connection.SetImageAsync(bmp, context, SDKTarget.HardwareAndSoftware, null);

                Thread.Sleep(800);

                bmp = StaticImages.Now_Live_3;
                await connection.SetImageAsync(bmp, context, SDKTarget.HardwareAndSoftware, null);

                Thread.Sleep(800);

                bmp = StaticImages.Now_Live_4;
                await connection.SetImageAsync(bmp, context, SDKTarget.HardwareAndSoftware, null);

                Thread.Sleep(800);
            }

            Thread.Sleep(2000);

            await Render(context);
        }

        private static void Cleanup()
        {

            var vc = (from v in VideoCache
                      group v by v.SDContext into vg
                      select vg).ToList();


            var cleaned = vc.Select(a => a.LastOrDefault()).ToList();

            cleaned = cleaned.Where(a => a.SDContext != null).ToList();

            VideoCache = cleaned;
        }

        static void OpenUrl(string url)
        {
            try
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    ProcessStartInfo info = new ProcessStartInfo()
                    {
                        FileName = url,
                        UseShellExecute = true
                    };

                    Process.Start(info);

                    //url = url.Replace("&", "^&");
                    //var p = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });

                    //Thread.Sleep(1000);

                    //p.Close();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch (Exception ex)
            {

                LogError(ex);
                throw;
            }
        }

        public static async Task LoadChannel(String Context, StreamDeckTileConfig config, bool force = false)
        {
            try
            {
                var entry = VideoCache.GetVideoEntry(Context, config.ChannelUrl); //.FirstOrDefault(a => a.SDContext == Context);// settings[Context]["details"]?.ToObject(typeof(VideoEntry)) as VideoEntry;

                if (entry == null)
                {
                    await SetLoadingScreen(Context, config.ChannelUrl, entry);
                    //await connection.SetImageAsync(Properties.Resources.loading, Context, SDKTarget.HardwareAndSoftware, null);

                    entry = new VideoEntry();
                    entry.SDContext = Context;
                    entry.LastVideo = "";
                    entry.ChannelUrl = config.ChannelUrl;
                    entry.IconType = config.Icon;
                    entry.PreviewType = config.Preview;
                }
                else if (entry != null && !force && DateTime.Now.Subtract(entry.LastUpdate).TotalMinutes < (timer.Interval / 60 / 1000) && entry.IsChannelIconCached())
                {
                    entry.IconType = config.Icon;
                    entry.PreviewType = config.Preview;

                    await Render(Context);

                    return;
                }

                if (force | DateTime.Now.Subtract(entry.LastUpdate).TotalMinutes > 120)
                {
                    await SetLoadingScreen(Context, config.ChannelUrl, entry);
                    //await connection.SetImageAsync(Properties.Resources.loading, Context, SDKTarget.HardwareAndSoftware, null);
                }

                //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-gb");

                if (!config.ChannelUrl.EndsWith("/videos"))
                {
                    config.ChannelUrl += "/videos";
                }

                short retry = 3;
                (String url, String id, String length, String thumbnailurl)? lastvid = null;

                String t = "";

                do
                {
                    if (config.ChannelUrl.Contains("featured"))
                    {
                        config.ChannelUrl = config.ChannelUrl.Replace("featured/", "");
                    }

                    t = wc.DownloadString(config.ChannelUrl + "?view=0&flow=grid");

                    //doc.LoadHtml(t);

                    retry--;

                    //Letztes Video ?
                    lastvid = await GetLatestVideo(config.ChannelUrl, config.Preview, Context, t);

                    if (lastvid == null && retry > 0)
                    {
                        Thread.Sleep(1000);
                    }

                } while (retry > 0);

                //New Video
                if (entry.VideoId != lastvid?.id)
                {
                    entry.VideoSeen = DateTime.Now;
                    entry.IsLive = false;
                    //entry.LastVideo = lastvid?.id;

                    AnyBitmap LatestVideoThumbnail;
                    if (lastvid != null)
                    {
                        LatestVideoThumbnail = ImageTools.DownloadFromUrl(lastvid?.thumbnailurl);

                        if (LatestVideoThumbnail.Width > 200)
                        {
                            LatestVideoThumbnail = LatestVideoThumbnail.ResizeImage(200); // ImageTools.ResizeImage(LatestVideoThumbnail, 200);
                        }

                        LatestVideoThumbnail.SaveAs(entry.LatestVideoThumbnailImageFile, AnyBitmap.ImageFormat.Png);
                    }

                    //Neuer LiveStream
                    if (lastvid != null && lastvid?.length == "LIVE")
                    {
                        entry.VideoUrl = lastvid?.url;
                        entry.IsLive = true;
                        await RenderNowLive(Context);
                    }
                }

                entry.PreviewType = config.Preview;
                entry.VideoUrl = lastvid?.url;
                entry.LatestVideoThumbnailUrl = lastvid?.thumbnailurl;
                entry.VideoLength = lastvid?.length;
                entry.LastUpdate = DateTime.Now;

                entry.ChannelTitle = lib.Html.ExtractProperty(t, "meta", "property", "og:title", "content");
                // doc.DocumentNode.SelectSingleNode("//meta[@property='og:title']")?.GetAttributeValue("content", "") ?? "";

                //var image = doc.DocumentNode.SelectSingleNode("//img[@class='appbar-nav-avatar']");

                if (lastvid?.url != null)
                {
                    NameValueCollection nvc = HttpUtility.ParseQueryString(new Uri(lastvid?.url).Query);

                    entry.VideoId = nvc["v"];
                }

                AnyBitmap bmp = entry.LoadChannelIconFromDisk();
                //if (image != null && bmp == null)
                //{
                //    String avatar = image.GetAttributeValue("src", "");

                //    if (avatar == "")
                //        return;

                //    ///bmp = entry.ChannelIcon;

                //    bmp = ImageTools.DownloadFromUrl(avatar);
                //    //using (Stream st = wc.OpenRead(avatar))
                //    //{
                //    //    bmp = new Bitmap(st);
                //    //    //entry.ChannelIcon = bmp;
                //    //}

                //}
                //else
                if (bmp == null)
                {


                    //image = doc.DocumentNode.SelectSingleNode("//link[@rel='image_src']");

                    //String avatar = image.GetAttributeValue("href", "");

                    //a.Attribute("rel")?.Value == "image_src"
                    String avatar2 = lib.Html.ExtractProperty(t, "link", "rel", "image_src", "href");

                    if (string.IsNullOrEmpty(avatar2))
                        return;

                    bmp = ImageTools.DownloadFromUrl(avatar2);
                    //using (Stream st = wc.OpenRead(avatar))
                    //{
                    //    bmp = new Bitmap(st);
                    //    //entry.ChannelIcon = bmp.Clone() as Bitmap;
                    //}
                }

                if (bmp == null)
                {
                    bmp = new AnyBitmap(100, 100, IronSoftware.Drawing.Color.Black);
                }

                if (bmp.Width > 100)
                {
                    bmp = bmp.ResizeImage(100, 100); // ImageTools.ResizeImage(bmp, 100, 100);
                }

                if (!File.Exists(entry.ChannelImageFile))
                {
                    bmp.SaveAs(entry.ChannelImageFile);
                    bmp.Dispose();
                    bmp = null;
                }


                VideoCache.Add(entry);


                await Render(Context);
            }
            catch (WebException ex)
            {
                int i = (int)ex.Status;

                //Too many requests
                //if (i == 429)
                //{
                //    await RenderNoConnection(Context);
                //    return;
                //}

                var inx = ex.InnerException;

                var inx2 = inx?.InnerException;

                await RenderNoConnection(Context);

                if (inx != null && typeof(HttpRequestException) == inx.GetType()
                    && inx2 != null && inx2.GetType() == typeof(SocketException))
                {
                    var se = inx2 as SocketException;

                    if (se.SocketErrorCode == SocketError.HostNotFound |
                        se.SocketErrorCode == SocketError.TimedOut |
                        se.SocketErrorCode == SocketError.NetworkReset)
                    {
                        //await RenderNoConnection(Context);
                        return;
                    }
                }



                LogError(ex);

                Debugger.Break();
            }
            catch (Exception ex)
            {
                LogError(ex);

                Debugger.Break();
            }
        }

        private static async Task RenderNoConnection(String context)
        {
            VideoEntry entry = VideoCache.GetVideoEntry(context, settings[context]["channelUrl"].ToString());

            var bmp = entry?.LoadChannelIconFromDisk();

            if (entry == null || bmp == null)
            {
                await connection.SetImageAsync(StaticImages.No_Internet, context, SDKTarget.HardwareAndSoftware, null);
                return;
            }

            SKBitmap f = bmp.Blur(2).MakeGrayscale3(); //ImageTools.MakeGrayscale3(ImageTools.Blur(bmp, 2));

            SKBitmap no = StaticImages.No_Internet;

            var m_w = no.Width / 2;
            var m_h = no.Height / 2;

            no.Mutate(a => a.Resize(m_w, m_h));

            f.Mutate(a => a.DrawImage(no, new SixLabors.ImageSharp.Point(50 - (m_w / 2), 50 - (m_h / 2)), 1));

            AnyBitmap f2 = f;

            await connection.SetImageAsync(f2, context, SDKTarget.HardwareAndSoftware, null);

        }

        private static async Task SetLoadingScreen(string context, string channelUrl, VideoEntry entry)
        {
            var bmp = entry?.LoadChannelIconFromDisk();

            if (entry == null || bmp == null)
            {
                AnyBitmap loading = StaticImages.Loading;
                await connection.SetImageAsync(loading, context, SDKTarget.HardwareAndSoftware, null);
                return;
            }



            SKBitmap f = bmp.Blur(2).MakeGrayscale3();

            SKBitmap loading_transparent = StaticImages.Loading_Transparent_30;

            f.Mutate(a => a.DrawImage(loading_transparent, new SixLabors.ImageSharp.Point(0, 0), 1));

            AnyBitmap f2 = f;

            await connection.SetImageAsync(f2, context, SDKTarget.HardwareAndSoftware, null);
        }

        static async Task Render(String Context)
        {
            if (!settings.ContainsKey(Context))
                return;

            var entry = VideoCache.GetVideoEntry(Context, settings[Context]["channelUrl"].ToString()); //.FirstOrDefault(a => a.SDContext == Context);
            if (entry == null)
                return;

            AnyBitmap bmp_channel_icon = entry.LoadChannelIconFromDisk();



            AnyBitmap bmp = bmp_channel_icon?.Clone();

            SKBitmap img = bmp;




            switch (entry.IconType)
            {
                case "thumbnail":

                    var any_thumbnail = entry.LoadThumbnailIconFromDisk();



                    if (entry.PreviewType != "live" && any_thumbnail != null)
                    {
                        SKBitmap thumbnail = any_thumbnail;

                        img.Mutate(a => a.Fill(SixLabors.ImageSharp.Color.Black, new IronSoftware.Drawing.RectangleF(0, 0, 100, 100)));

                        thumbnail.Mutate(a => a.Resize(new ResizeOptions() { Position = AnchorPositionMode.Center, Mode = ResizeMode.Pad, Size = new SixLabors.ImageSharp.Size(100, 100) }));

                        img.Mutate(a => a.DrawImage(thumbnail, new SKRectangle(0, 0, 100, 100), 1));
                    }
                    else if (entry.PreviewType == "live")
                    {
                        img.Mutate(a => a.Fill(SixLabors.ImageSharp.Color.Black, new IronSoftware.Drawing.RectangleF(0, 0, 100, 100)));

                        AnyBitmap ti = bmp_channel_icon;

                        if (!entry.IsLive)
                        {
                            ti = ti.MakeGrayscale3(); // ImageTools.MakeGrayscale3(ti);
                        }

                        img.Mutate(a => a.DrawImage(ti, new SKRectangle(0, 0, bmp.Width, bmp.Height), 1));
                    }

                    break;

                case "channel":

                    if (entry.PreviewType == "live")
                    {
                        img.Mutate(a => a.Fill(SixLabors.ImageSharp.Color.Black, new IronSoftware.Drawing.RectangleF(0, 0, 100, 100)));

                        AnyBitmap ti = bmp_channel_icon;

                        if (!entry.IsLive)
                        {
                            ti = ti.MakeGrayscale3(); // ImageTools.MakeGrayscale3(ti);
                        }

                        img.Mutate(a => a.DrawImage(ti, new SKRectangle(0, 0, bmp.Width, bmp.Height), 1));
                    }

                    break;

                default:

                    break;

            }



            if (entry.ShouldRenderLengthPreview())
            {
                bool IsDark = bmp.isDark();

                SKBitmap f = null;

                if (IsDark)
                {
                    f = bmp.Blur(2); // ImageTools.Blur(bmp, 2);
                }
                else
                {
                    f = bmp.Blur(5); // ImageTools.Blur(bmp, 5);
                }

                f.Mutate(a => a.Crop(new SKRectangle(0, 100 - 26, 100, 26)));

                img.Mutate(a => a.DrawImage(f, new SixLabors.ImageSharp.Point(0, bmp.Height - 26), 1));

                var layer = new SixLabors.ImageSharp.Color(new SixLabors.ImageSharp.PixelFormats.Argb32(50, 80, 80, 80));

                if (!IsDark)
                {
                    layer = new SixLabors.ImageSharp.Color(new SixLabors.ImageSharp.PixelFormats.Argb32(190, 80, 80, 80));
                }

                img.Mutate(a => a.Fill(new DrawingOptions() { GraphicsOptions = new SixLabors.ImageSharp.GraphicsOptions() { AlphaCompositionMode = SixLabors.ImageSharp.PixelFormats.PixelAlphaCompositionMode.SrcOver } }, layer, new IronSoftware.Drawing.RectangleF(0, 100 - 26, 100, 26)));

                SKBitmap b = null;

                if (entry.VideoLength == "LIVE")
                {
                    b = StaticImages.Status_Hot_2; //(AnyBitmap)Resources.hot
                }
                else if ((entry.LastVideo?.ToString() ?? "") != (entry.VideoId ?? ""))
                {
                    if (DateTime.Now.Subtract(entry.VideoSeen ?? entry.LastUpdate).TotalMinutes < 60)
                    {
                        b = StaticImages.Status_Hot_2; //(AnyBitmap)Resources.hot
                    }
                    else
                    {
                        b = StaticImages.Status_New_2; // (AnyBitmap)Resources._new;
                    }
                }
                else
                {
                    b = StaticImages.Status_Played_2; // (AnyBitmap)Resources.played;
                }

                SKBitmap b_shrinked = b;

                //b_shrinked.Mutate(a => a.Resize(new SixLabors.ImageSharp.Size(b.Width / 2, b.Height / 2)));

                img.Mutate(a => a.DrawImage(b_shrinked, new IronSoftware.Drawing.Point(100 - b_shrinked.Width - 10, 100 - b_shrinked.Height - 4), 1));


                try
                {
                    SixLabors.Fonts.Font ff = Fonts.FirstOrDefault();

                    if (entry.VideoLength.Count(a => a == ':') <= 1)
                    {
                        img.Mutate(a => a.DrawText(new RichTextOptions(ff) { HorizontalAlignment = SixLabors.Fonts.HorizontalAlignment.Center, Origin = new System.Numerics.Vector2((100 - b_shrinked.Width) / 2, 100 - 20) }, entry.VideoLength, SixLabors.ImageSharp.Color.White)); ;
                    }
                    else
                    {
                        SixLabors.Fonts.Font f2 = new SixLabors.Fonts.Font(ff, ff.Size - 1);

                        img.Mutate(a => a.DrawText(new RichTextOptions(f2) { HorizontalAlignment = SixLabors.Fonts.HorizontalAlignment.Center, Origin = new System.Numerics.Vector2((100 - b_shrinked.Width) / 2, 100 - 20) }, entry.VideoLength, SixLabors.ImageSharp.Color.White)); ;
                    }


                }
                catch (Exception ex)
                {

                }
            }


            AnyBitmap bmp2 = img;

            await connection.SetImageAsync(bmp2, Context, SDKTarget.HardwareAndSoftware, null);

        }

        static List<SixLabors.Fonts.Font> CollectFonts()
        {
            List<SixLabors.Fonts.Font> fonts = new List<SixLabors.Fonts.Font>();

            try
            {
                SixLabors.Fonts.Font ff = new IronSoftware.Drawing.Font("Verdana", 14);
                if (ff != null)
                    fonts.Add(ff);
            }
            catch
            {

            }

            try
            {
                SixLabors.Fonts.Font ff = new IronSoftware.Drawing.Font("Times New Roman", 16);
                if (ff != null)
                    fonts.Add(ff);
            }
            catch
            {

            }

            try
            {
                SixLabors.Fonts.Font ff = new IronSoftware.Drawing.Font("Times", 16);
                if (ff != null)
                    fonts.Add(ff);
            }
            catch
            {

            }

            try
            {
                var ff = SixLabors.Fonts.SystemFonts.Families.FirstOrDefault().CreateFont(14);
                if (ff != null)
                    fonts.Add(ff);

            }
            catch
            {

            }

            return fonts;
        }

        static async Task<(String url, String id, String length, String thumbnail)?> GetLatestVideo(String channelUrl, String mode, String Context, String t = null)
        {
            String url = "";
            String id = "";
            String length = "";
            String thumbnailurl = "";
            try
            {
                if (t == null)
                {
                    WebClient wc = new WebClient();
                    wc.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-gb");
                    wc.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36");

                    if (!channelUrl.EndsWith("/videos"))
                    {
                        channelUrl += "/videos";
                    }

                    if (channelUrl.Contains("featured"))
                    {
                        channelUrl = channelUrl.Replace("featured/", "");
                    }

                    t = wc.DownloadString(channelUrl + "?view=0&flow=grid");
                }

                //Prüfe auf LiveStream
                if (mode == "all" | mode == "live")
                {
                    var current_stream = await GetLatestLiveStream(channelUrl, Context);
                    if (current_stream != null)
                    {
                        return current_stream;
                    }
                    else if (mode == "live")
                    {
                        return null;
                    }
                }


                var ytInitialData = Model.Youtube.Tools.ExtractScript(t);

                var ytModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ytInitialData>(ytInitialData);

                var videoList = ytModel.GetVideos();

                var last_video = videoList.FirstOrDefault();

                if (last_video == null)
                {
                    return null;
                }

                id = last_video.videoId;
                url = "https://www.youtube.com/watch?v=" + last_video.videoId;
                length = last_video.lengthText.simpleText;

                thumbnailurl = last_video.thumbnail.thumbnails.OrderBy(a => a.width).FirstOrDefault()?.url;

                await connection.SetTitleAsync("", Context, SDKTarget.HardwareAndSoftware, null);

                return (url, id, length, thumbnailurl);
            }
            catch (Exception ex)
            {
                LogError(ex);

                Debugger.Break();

            }

            return null;
        }

        static async Task<(String url, String id, String length, String thumbnail)?> GetLatestLiveStream(String channelUrl, String Context)
        {
            String url = "";
            String id = "";
            String length = "";
            String thumbnailurl = "";

            try
            {

                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-gb");
                wc.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36");

                if (!channelUrl.EndsWith("/streams"))
                {
                    channelUrl += "/streams";
                }

                if (channelUrl.Contains("featured"))
                {
                    channelUrl = channelUrl.Replace("featured/", "");
                }

                if (channelUrl.Contains("videos"))
                {
                    channelUrl = channelUrl.Replace("videos/", "");
                }

                var t = wc.DownloadString(channelUrl + "?view=0&flow=grid");

                var ytInitialData = Model.Youtube.Tools.ExtractScript(t);

                var ytModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ytInitialData>(ytInitialData);

                var videoList = ytModel.GetLiveStreams();

                var last_video = videoList.Where(a => a.lengthText == null && a.thumbnailOverlays.Any(b => b.thumbnailOverlayTimeStatusRenderer != null && b.thumbnailOverlayTimeStatusRenderer.style == "LIVE")).FirstOrDefault();

                if (last_video == null)
                    return null;

                id = last_video.videoId;
                url = "https://www.youtube.com/watch?v=" + last_video.videoId;
                length = "LIVE";

                thumbnailurl = last_video.thumbnail.thumbnails.OrderBy(a => a.width).FirstOrDefault()?.url;

                await connection.SetTitleAsync("", Context, SDKTarget.HardwareAndSoftware, null);

                return (url, id, length, thumbnailurl);
            }
            catch (Exception ex)
            {
                LogError(ex);

                Debugger.Break();
            }

            return null;
        }


        private static void LogError(Exception ex)
        {
            try
            {
                File.AppendAllText("debug.txt", ex.Message + "\r\n" + ex.StackTrace + "\r\n\r\n");
            }
            catch (Exception ex2)
            {
                Debugger.Break();
            }
        }


    }
}
