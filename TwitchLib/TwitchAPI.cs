using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TwitchLib
{
    public class TwitchAPI
    {
        public static async Task<bool> broadcasterOnline(string channel)
        {
            var client = new WebClient();
            string resp = await client.DownloadStringTaskAsync(new Uri(String.Format("https://api.twitch.tv/kraken/streams/{0}", channel)));
            if (resp.Contains("{\"stream\":{\"_id\":"))
                return true;
            return false;
        }
    }
}
