using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

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

        public static async Task<TwitchChannel> getTwitchChannel(string channel)
        {
            var client = new WebClient();
            string resp = await client.DownloadStringTaskAsync(new Uri(String.Format("https://api.twitch.tv/kraken/channels/{0}", channel)));
            JObject json = JObject.Parse(resp);
            if (json.SelectToken("status").ToString() != "404" && json.SelectToken("status").ToString() != "422")
                return new TwitchChannel(resp);
            throw new Exceptions.InvalidChannelException(resp);
        }

        public static async Task<bool> userFollowsChannel(string username, string channel)
        {
            var client = new WebClient();
            try
            {
                string resp = await client.DownloadStringTaskAsync(new Uri(String.Format("https://api.twitch.tv/kraken/users/{0}/follows/channels/{1}", username, channel)));
                return true;
            } catch (WebException)
            {
                return false;
            }
        }

        public static async Task<List<Chatter>> getChatters(string channel)
        {
            var client = new WebClient();
            string resp = await client.DownloadStringTaskAsync(new Uri(String.Format("https://tmi.twitch.tv/group/user/{0}/chatters", channel)));
            JToken chatters = JObject.Parse(resp).SelectToken("chatters");
            List<Chatter> chatterList = new List<Chatter>();
            foreach(JToken user in chatters.SelectToken("moderators"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.uType.Moderator));
            }
            foreach(JToken user in chatters.SelectToken("staff"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.uType.Staff));
            }
            foreach(JToken user in chatters.SelectToken("admins"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.uType.Admin));
            }
            foreach(JToken user in chatters.SelectToken("global_mods"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.uType.Global_Moderator));
            }
            foreach(JToken user in chatters.SelectToken("viewers"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.uType.Viewer));
            }
            return chatterList;
        }
    }
}
