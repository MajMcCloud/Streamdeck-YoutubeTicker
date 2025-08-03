using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoutubeTicker.lib
{
    public static class Extensions
    {


        /// <summary>
        /// Backwards compatibility.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="context"></param>
        /// <param name="channelUrl"></param>
        /// <returns></returns>
        public static VideoEntry GetVideoEntry(this List<VideoEntry> list, String context, String channelUrl)
        {
            VideoEntry entry = null;

            entry = list.FirstOrDefault(a => a.SDContext == context);
            if (entry != null)
                return entry;

            //Backup for migration
            if (list.Any(a => a.SDContext == null))
            {
                entry = list.FirstOrDefault(a => a.ChannelUrl == channelUrl);
            }

            return entry;
        }

        
    }
}
