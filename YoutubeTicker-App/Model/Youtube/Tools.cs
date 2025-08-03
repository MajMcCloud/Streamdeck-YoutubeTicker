using IronSoftware.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace YoutubeTicker.Model.Youtube
{
    public static class Tools
    {
        public static String ExtractScript(String html)
        {
            String t = "";

            int start = html.IndexOf("ytInitialData");

            start = html.IndexOf("{", start);

            int end = html.IndexOf("</script>", start + 1);

            var sub = html.Substring(start, end - start);

            while (sub[sub.Length - 1] != '}')
            {
                sub = sub.Substring(0, sub.Length - 1);
            }

            return sub;
        }

    }
}
