using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YoutubeTicker
{
    public class cache
    {
        public List<VideoEntry> Cache { get; set; }


        public cache() { }

        public cache(List<VideoEntry> cache)
        {
            Cache = cache;
        }

        public void save()
        {
            save("cache.js");
        }

        public void save(String path)
        {
            try
            {
                var s = Newtonsoft.Json.JsonConvert.SerializeObject(this);

                if (File.Exists(path))
                    File.Delete(path);

                File.WriteAllText(path, s, Encoding.UTF8);

            }
            catch(Exception ex)
            {
                
            }

        }

        public static cache load()
        {
            return load("cache.js");
        }

        public static cache load(String path)
        {
            try
            {
                var s = File.ReadAllText(path);

                var set = Newtonsoft.Json.JsonConvert.DeserializeObject<cache>(s) as cache;

                return set;
            }
            catch
            {

            }

            return null;
        }

    }
}
