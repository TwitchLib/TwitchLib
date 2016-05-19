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
    public class TwitchApi
    {
        public enum ValidCommercialLengths
        {
            Seconds30,
            Seconds60,
            Seconds90,
            Second120,
            Seconds150,
            Seconds180
        }

        public static async Task<bool> BroadcasterOnline(string channel)
        {
            try
            {
                var client = new WebClient();
                string resp = await client.DownloadStringTaskAsync(new Uri(
                    $"https://api.twitch.tv/kraken/streams/{channel}"));
                if (resp.Contains("{\"stream\":{\"_id\":"))
                    return true;
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        //Undocumented API endpoint (reliability is shoddy); requires two API calls, try not to use often
        public static async Task<List<string>> GetChannelHosts(string channel)
        {
            List<string> hosts = new List<string>();
            var client = new WebClient();
            string resp = await client.DownloadStringTaskAsync(new Uri($"https://api.twitch.tv/kraken/users/{channel}"));
            JObject json = JObject.Parse(resp);
            if (json.SelectToken("_id") == null) return hosts;
            resp =
                await
                    client.DownloadStringTaskAsync(
                        new Uri(string.Format("http://tmi.twitch.tv/hosts?include_logins=1&target={0}",
                            json.SelectToken("_id").ToString())));
            json = JObject.Parse(resp);
            foreach (JToken host in json.SelectToken("hosts"))
            {
                hosts.Add(host.SelectToken("host_login").ToString());
            }
            return hosts;
        }

        //Undocumented API endpoint (reliability is shoddy)
        public static async Task<List<TwitchAPIClasses.TwitchTeamMember>> GetTeamMembers(string teamName)
        {
            List<TwitchAPIClasses.TwitchTeamMember> members = new List<TwitchAPIClasses.TwitchTeamMember>();
            var client = new WebClient();
            string resp = await client.DownloadStringTaskAsync(new Uri(
                $"http://api.twitch.tv/api/team/{teamName}/all_channels.json"));
            JObject json = JObject.Parse(resp);
            foreach (JToken member in json.SelectToken("channels"))
            {
                members.Add(new TwitchAPIClasses.TwitchTeamMember(member.SelectToken("channel")));
            }
            return members;
        }

        public static async Task<TwitchChannel> GetTwitchChannel(string channel)
        {
            var client = new WebClient();
            string resp = null;
            try
            {
                resp = await client.DownloadStringTaskAsync(new Uri($"https://api.twitch.tv/kraken/channels/{channel}"));
            }
            catch (Exception)
            {
                throw new Exceptions.InvalidChannelException(resp);
            }
            JObject json = JObject.Parse(resp);
            if (json.SelectToken("error") != null)
                throw new Exceptions.InvalidChannelException(resp);
            return new TwitchChannel(json);
        }

        public static async Task<bool> UserFollowsChannel(string username, string channel)
        {
            var client = new WebClient();
            try
            {
                string resp = await client.DownloadStringTaskAsync(new Uri(
                    $"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}"));
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static List<TwitchAPIClasses.TwitchFollower> GetTwitchFollowers(string channel, int limit = 25,
            int cursor = -1, string direction = "desc")
        {
            List<TwitchAPIClasses.TwitchFollower> followers = new List<TwitchAPIClasses.TwitchFollower>();
            var args = cursor == -1
                ? $"?limit={limit}&direction{direction}"
                : $"?limit={limit}&cursor={cursor}&direction={direction}";
            var client = new WebClient();
            string json = client.DownloadString(string.Format(
                $"https://api.twitch.tv/kraken/channels/{channel}/follows{args}"));
            foreach (JToken follower in JObject.Parse(json).SelectToken("follows"))
            {
                followers.Add(new TwitchAPIClasses.TwitchFollower(follower));
            }
            return followers;
        }

        public static TimeSpan GetUptime(string channel)
        {
            TwitchAPIClasses.TwitchStream stream = GetTwitchStream(channel);
            DateTime time = Convert.ToDateTime(stream.CreatedAt);
            return DateTime.UtcNow - time;
        }

        public static TwitchAPIClasses.TwitchStream GetTwitchStream(string channel)
        {
            try
            {
                var client = new WebClient();
                string resp = client.DownloadString($"https://api.twitch.tv/kraken/streams/{channel}");
                JObject json = JObject.Parse(resp);
                if (json.SelectToken("stream").SelectToken("_id") != null)
                    return new TwitchAPIClasses.TwitchStream(json.SelectToken("stream"));
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<TwitchChannel> SearchChannels(string query)
        {
            var client = new WebClient();
            List<TwitchChannel> returnedChannels = new List<TwitchChannel>();
            string resp = client.DownloadString(new Uri($"https://api.twitch.tv/kraken/search/channels?q={query}"));
            JObject respO = JObject.Parse(resp);
            if (respO.SelectToken("_total").ToString() == "0") return returnedChannels;
            foreach (JToken channelToken in respO.SelectToken("channels"))
                returnedChannels.Add(new TwitchChannel((JObject) channelToken));
            return returnedChannels;
        }

        public static async Task<List<Chatter>> GetChatters(string channel)
        {
            var client = new WebClient();
            string resp = await client.DownloadStringTaskAsync(new Uri(
                $"https://tmi.twitch.tv/group/user/{channel}/chatters"));
            JToken chatters = JObject.Parse(resp).SelectToken("chatters");
            List<Chatter> chatterList = new List<Chatter>();
            foreach (JToken user in chatters.SelectToken("moderators"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.UType.Moderator));
            }
            foreach (JToken user in chatters.SelectToken("staff"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.UType.Staff));
            }
            foreach (JToken user in chatters.SelectToken("admins"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.UType.Admin));
            }
            foreach (JToken user in chatters.SelectToken("global_mods"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.UType.GlobalModerator));
            }
            foreach (JToken user in chatters.SelectToken("viewers"))
            {
                chatterList.Add(new Chatter(user.ToString(), Chatter.UType.Viewer));
            }
            return chatterList;
        }

        // Required scope: channel_subscriptions
        public static int GetSubscriberCount(string username, string accessToken)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Accept", "application/vnd.twitchtv.v3+json");
            wc.Headers.Add("Authorization", $"OAuth {accessToken}");
            string cnts = wc.DownloadString("https://api.twitch.tv/kraken/channels/" + username + "/subscriptions");
            JObject json = JObject.Parse(cnts);
            return int.Parse(json.SelectToken("_total").ToString());
        }

        // Required scope: channel_editor
        public static async void UpdateStreamDelay(int delay, string username, string accessToken)
        {
            string data = "{\"channel\":{\"delay\":" + delay + "}}";
            Console.WriteLine(data);
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", $"OAuth {accessToken}");
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse) request.GetResponse();
        }

        //Required scope: channel_editor
        public static async void UpdateStreamTitle(string status, string username, string accessToken)
        {
            string data = "{\"channel\":{\"status\":\"" + status + "\"}}";
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", $"OAuth {accessToken}");
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse) request.GetResponse();
        }

        //Required scope: channel_editor
        public static async void UpdateStreamGame(string game, string username, string accessToken)
        {
            string data = "{\"channel\":{\"game\":\"" + game + "\"}}";
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", $"OAuth {accessToken}");
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse) request.GetResponse();
        }

        //Required scope: channel_editor
        public static async void UpdateStreamTitleAndGame(string status, string game, string username,
            string accessToken)
        {
            string data = "{\"channel\":{\"status\":\"" + status + "\",\"game\":\"" + game + "\"}}";
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username);
            request.Method = "PUT";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", $"OAuth {accessToken}");
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse) request.GetResponse();
        }

        //Required scope: channel_stream
        public static async void ResetStreamKey(string username, string accessToken)
        {
            string data = "";
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username + "/streamkey");
            request.Method = "DELETE";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", $"OAuth {accessToken}");
            request.ContentType = "application/text";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse) request.GetResponse();
        }

        //Required scope: channel_check_subscription
        public static bool ChannelHasUserSubscribed(string username, string channel, string accessToken)
        {
            try
            {
                HttpWebRequest request =
                    (HttpWebRequest)
                        WebRequest.Create("https://api.twitch.tv/kraken/channels/" + channel + "/subscriptions/" +
                                          username);
                request.Method = "GET";
                request.Accept = "application/vnd.twitchtv.v3+json";
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
                request.ContentType = "application/text";
                HttpWebResponse errorResponse = request.GetResponse() as HttpWebResponse;
                return errorResponse.StatusCode != HttpStatusCode.NotFound;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<List<TwitchAPIClasses.TwitchVideo>> GetChannelVideos(string channel, int limit = 10,
            int offset = 0,
            bool onlyBroadcasts = false, bool onlyHls = false)
        {
            List<TwitchAPIClasses.TwitchVideo> videos = new List<TwitchAPIClasses.TwitchVideo>();
            var client = new WebClient();
            string limitStr = "?limit=" + limit;
            string offsetStr = "&offset=" + offset;
            string broadcastStr = "&broadcasts=false";
            if (onlyBroadcasts == true)
            {
                broadcastStr = "&broadcasts=true";
            }
            string hlsStr = "&hls=false";
            if (onlyHls == true)
            {
                hlsStr = "&hls=true";
            }
            string resp = await client.DownloadStringTaskAsync(new Uri(
                $"https://api.twitch.tv/kraken/channels/{channel}/videos?{limitStr}{offsetStr}{broadcastStr}{hlsStr}"));
            JToken vids = JObject.Parse(resp).SelectToken("videos");
            foreach (JToken vid in vids)
            {
                TwitchAPIClasses.TwitchVideo video = new TwitchAPIClasses.TwitchVideo(vid);
                videos.Add(video);
            }

            return videos;
        }

        //Required scope: channel_commercial
        public static async void RunCommercial(ValidCommercialLengths length, string username, string accessToken)
        {
            string data;
            switch (length)
            {
                case ValidCommercialLengths.Seconds30:
                    data = "length=30";
                    break;
                case ValidCommercialLengths.Seconds60:
                    data = "length=60";
                    break;
                case ValidCommercialLengths.Seconds90:
                    data = "length=90";
                    break;
                case ValidCommercialLengths.Second120:
                    data = "length=120";
                    break;
                case ValidCommercialLengths.Seconds150:
                    data = "length=150";
                    break;
                case ValidCommercialLengths.Seconds180:
                    data = "length=180";
                    break;
                default:
                    data = "length=30";
                    break;
            }
            HttpWebRequest request =
                (HttpWebRequest) WebRequest.Create("https://api.twitch.tv/kraken/channels/" + username + "/commercial");
            request.Method = "POST";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.Headers.Add("Authorization", $"OAuth {accessToken}");
            request.ContentType = "application/text";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] utfBytes = encoding.GetBytes(data);
            using (Stream dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(utfBytes, 0, utfBytes.Length);
            }
            var response = (HttpWebResponse) request.GetResponse();
        }
    }
}