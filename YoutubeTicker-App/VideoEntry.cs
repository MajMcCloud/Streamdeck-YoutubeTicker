using IronSoftware.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace YoutubeTicker
{
    [DebuggerDisplay("{ChannelTitle}: {VideoLength} {(IsLive ? \"LIVE\" : \"\")}")]
    public class VideoEntry
    {
        public String SDContext { get; set; }

        [JsonIgnore]
        public StreamDeckTileConfig TileConfig { get; set; }

        public String VideoUrl { get; set; }

        public String VideoLength { get; set; }

        /// <summary>
        /// YouTube Video ID
        /// </summary>
        public String VideoId { get; set; }

        /// <summary>
        /// Last video clicked.
        /// </summary>
        public String LastVideo { get; set; }

        public bool IsLive { get; set; }

        public String ChannelTitle { get; set; }
        public String ChannelUrl { get; set; }

        [JsonIgnore]
        public String ChannelId
        {
            get
            {
                if (ChannelUrl.Contains("/user/"))
                {
                    return ChannelUrl?.Split('/')[4];
                }
                else
                {
                    return ChannelUrl?.Substring(ChannelUrl.LastIndexOf('/') + 1);
                }


            }
        }

        [JsonIgnore]
        public String ChannelImageFile
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "images\\" + this.ChannelId + ".png";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "images/" + this.ChannelId + ".png";
                }
                else
                {
                    return "images/" + this.ChannelId + ".png";
                }

            }
        }

        [JsonIgnore]
        public String LatestVideoThumbnailImageFile
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "images\\" + this.ChannelId + "_thumbnail.png";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "images/" + this.ChannelId + "_thumbnail.png";
                }
                else
                {
                    return "images/" + this.ChannelId + "_thumbnail.png";
                }

            }
        }

        public String LatestVideoThumbnailUrl { get; set; }

        public String IconType { get; set; } = "channel";

        public String PreviewType { get; set; } = "all";

        public DateTime LastUpdate { get; set; }

        public DateTime? VideoSeen { get; set; }

        public AnyBitmap LoadChannelIconFromDisk()
        {
            if (!Directory.Exists("images"))
            {
                return StaticImages.No_Internet;
            }

            if (!File.Exists(ChannelImageFile))
            {
                return null;
            }

            try
            {
                var b = new AnyBitmap(ChannelImageFile);

                var b2 = b.Clone();

                b.Dispose();
                b = null;

                return b2;
            }
            catch
            {

            }

            return null;
        }

        public AnyBitmap LoadThumbnailIconFromDisk()
        {
            if (!Directory.Exists("images"))
                return StaticImages.No_Internet;

            if (!File.Exists(LatestVideoThumbnailImageFile))
            {
                return null;
            }

            var b = new AnyBitmap(LatestVideoThumbnailImageFile);
            var b2 = b.Clone() as AnyBitmap;

            b.Dispose();
            b = null;

            return b2;
        }


        public bool IsChannelIconCached()
        {
            return File.Exists(ChannelImageFile);
        }

        public bool ShouldRenderLengthPreview()
        {
            if (string.IsNullOrEmpty(VideoLength))
            {
                return false;
            }

            if (PreviewType == "live")
            {
                return false;
            }

            return true;
        }

        public void ResetImageCache()
        {
            if (File.Exists(ChannelImageFile))
            {
                try
                {
                    File.Delete(ChannelImageFile);
                }
                catch
                {

                }
            }

            if (File.Exists(LatestVideoThumbnailImageFile))
            {
                try
                {
                    File.Delete(LatestVideoThumbnailImageFile);
                }
                catch
                {

                }
            }



        }

    }
}
