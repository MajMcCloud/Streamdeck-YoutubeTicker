using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YoutubeTicker.lib
{
    internal class Html
    {
        static Regex KeyValueEx = new Regex("(?<key>\\w+)=[\"\"'](?<value>.*?)[\"\"']");


        public static String ExtractProperty(String html, String TagName, String ConditionName, String ConditionValue, String propertyToExtractName)
        {
            return lib.Html.ExtractProperty(html, TagName, a => a.Any(a => a.Groups["key"].Value == ConditionName && a.Groups["value"].Value == ConditionValue), propertyToExtractName);
        }

        public static String ExtractProperty(String html, String TagName, Func<MatchCollection, bool> condition, String propertyName)
        {
            string LinkPattern = $@"<{TagName}\s+(?<attributes>[^>]*?)>";

            MatchCollection matches = Regex.Matches(html, LinkPattern, RegexOptions.IgnoreCase);

            

            foreach (var href in matches)
            {
                if (!(href is Match match))
                    continue;

                var properties = KeyValueEx.Matches(match.Groups["attributes"].Value);

                if (!condition(properties))
                    continue;

                var value = properties.FirstOrDefault(a => a.Groups["key"].Value == propertyName)?.Groups["value"].Value;

                return value;
            }

            return null;
        }

        // Hilfsfunktion: Entfernt Doctype und andere ungültige DTD-Deklarationen
        static string RemoveDoctype(string html)
        {
            int doctypeIndex = html.IndexOf("<!DOCTYPE");
            if (doctypeIndex >= 0)
            {
                int endIndex = html.IndexOf(">", doctypeIndex);
                if (endIndex >= 0)
                {
                    // Entferne die Doctype-Deklaration
                    html = html.Remove(doctypeIndex, endIndex - doctypeIndex + 1);
                }
            }
            return html;
        }

    }
}
