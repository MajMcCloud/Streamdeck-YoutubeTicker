using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTicker
{
    [DebuggerDisplay("{ChannelUrl}")]
    public class StreamDeckTileConfig
    {
        public double? RefreshInterval { get; set; }

        public String ChannelUrl { get; set; }


        public String Preview { get; set; } = "all";

        public String Icon { get; set; } = "channel";

        public String Event { get; set; } = "openlatest";


        public StreamDeckTileConfig()
        {


        }


        public StreamDeckTileConfig(JObject cfg) : this()
        {
            if (cfg == null)
                return;


            double d = 0;
            if (cfg["refreshInterval"] != null && double.TryParse(cfg["refreshInterval"].ToString(), out d))
                RefreshInterval = d;

            if (cfg["channelUrl"] != null)
                ChannelUrl = cfg["channelUrl"].ToString();

            if (cfg["preview"] != null)
                Preview = cfg["preview"].ToString();

            if (cfg["icon"] != null)
                Icon = cfg["icon"].ToString();

            if (cfg["event"] != null)
                Event = cfg["event"].ToString();
        }

        public bool IsSet()
        {
            if (ChannelUrl == null || ChannelUrl.Trim() == "")
                return false;

            if (Preview == null || Preview.Trim() == "")
                return false;

            //if (Icon == null || Icon.Trim() == "")
            //    return false;

            //if (ChannelUrl == null || ChannelUrl.Trim() == "")
            //    return false;

            return true;
        }

        public JObject ToJObject()
        {
            var obj = new JObject();

            obj["channelUrl"] = ChannelUrl;

            if (RefreshInterval != null)
                obj["refreshInterval"] = RefreshInterval?.ToString("0");

            obj["preview"] = Preview;

            obj["icon"] = Icon;

            obj["event"] = Event;

            return obj;
        }



        public static explicit operator StreamDeckTileConfig(JObject obj)
        {
            return new StreamDeckTileConfig(obj);
        }

        public static StreamDeckTileConfig FromJObject(JObject obj)
        {
            return new StreamDeckTileConfig(obj);
        }
    }
}
