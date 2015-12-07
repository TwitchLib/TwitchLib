using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.IO;

namespace TwitchLib
{
    public class TwitchAPI
    {
        public enum Valid_Commercial_Lengths
        {
            SECONDS_30,
            SECONDS_60,
            SECONDS_90,
            SECOND_120,
            SECONDS_150,
            SECONDS_180
        }

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

        //Required scope: channel_editor
        public static async void updateStreamTitle(string status, string username, string access_token)
        {
            string data = "{\"channel\":{\"status\":\"" + status + "\"}}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", string.Format("OAuth {0}", access_token));
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        //Required scope: channel_editor
        public static async void updateStreamGame(string game, string username, string access_token)
        {
            string data = "{\"channel\":{\"game\":\"" + game + "\"}}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", string.Format("OAuth {0}", access_token));
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        //Required scope: channel_editor
        public static async void updateStreamTitleAndGame(string status, string game, string username, string access_token)
        {
            string data = "{\"channel\":{\"status\":\"" + status + "\",\"game\":\"" + game + "\"}}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", string.Format("OAuth {0}", access_token));
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }

        //Required scope: channel_commercial
        public static async void runCommerciale(Valid_Commercial_Lengths length, string username, string access_token)
        {
            string data;
            switch(length)
            {
                case Valid_Commercial_Lengths.SECONDS_30:
                    data = string.Format("length={0}", "30");
                    break;
                case Valid_Commercial_Lengths.SECONDS_60:
                    data = string.Format("length={0}", "60");
                    break;
                case Valid_Commercial_Lengths.SECONDS_90:
                    data = string.Format("length={0}", "90");
                    break;
                case Valid_Commercial_Lengths.SECOND_120:
                    data = string.Format("length={0}", "120");
                    break;
                case Valid_Commercial_Lengths.SECONDS_150:
                    data = string.Format("length={0}", "150");
                    break;
                case Valid_Commercial_Lengths.SECONDS_180:
                    data = string.Format("length={0}", "180");
                    break;
                default:
                    data = string.Format("length={0}", "30");
                    break;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username + "/commercial");
            request.Method = "POST";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", string.Format("OAuth {0}", access_token));
            request.ContentType = "application/text";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
        }
    }
}
