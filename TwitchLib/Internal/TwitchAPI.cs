using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Linq;
using TwitchLib.Exceptions.API;
using TwitchLib.Models.API;

namespace TwitchLib.Internal
{
    /// <summary>Static class with functionality for Twitch API calls.</summary>
    internal static class TwitchApi
    {
        // Internal variables
        private static string ClientId { get; set; }
        private static string AccessToken { get; set; } 

        #region Get Objects
        public static async Task<Channels> GetChannelsObject(string channel)
        {
            return new Channels(JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/api/channels/{channel}")));
        }

        public static async Task<BadgeResponse> GetChannelBadges(string channel)
        {
            return new BadgeResponse(await MakeGetRequest($"https://api.twitch.tv/kraken/chat/{channel}/badges"));
        }

        public static async Task<List<User>> GetChannelEditors(string channel, string accessToken = null)
        {
            var json = JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/editors", accessToken));
            List<User> editors = new List<User>();
            foreach (JToken editor in json.SelectToken("users"))
                editors.Add(new User(editor.ToString()));
            return editors;
        }

        public static async Task<List<string>> GetChannelHosts(string channel)
        {
            var hosts = new List<string>();
            User user = await GetUser(channel);
            var json = JObject.Parse(await MakeGetRequest($"http://tmi.twitch.tv/hosts?include_logins=1&target={user.Id}"));
            hosts.AddRange(json.SelectToken("hosts").Select(host => host.SelectToken("host_login").ToString()));
            return hosts;
        }

        public static async Task<List<TeamMember>> GetTeamMembers(string teamName)
        {
            var json = JObject.Parse(await MakeGetRequest($"http://api.twitch.tv/api/team/{teamName}/all_channels.json"));
            return
                json.SelectToken("channels")
                    .Select(member => new TeamMember(member.SelectToken("channel")))
                    .ToList();
        }

        public static async Task<Channel> GetChannel(string channel)
        {
            return new Channel(JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}")));
        }

        public static async Task<User> GetUser(string username)
        {
            return new User(await MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}"));
        }

        public static async Task<TimeSpan> GetUptime(string channel)
        {
            return (await GetStream(channel)).TimeSinceCreated;
        }

        public static async Task<bool> StreamIsLive(string channel)
        {
            try
            {
                await GetStream(channel);
                return true;
            } catch(StreamOfflineException)
            {
                return false;
            }
        }

        public static async Task<FeedResponse> GetChannelFeed(string channel, int limit = 10, string cursor = null)
        {
            var args = $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor};";
            return new FeedResponse(JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts{args}")));
        }

        public static async Task<Models.API.Stream> GetStream(string channel)
        {
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/streams/{channel}");
            var json = JObject.Parse(resp);
            if (!Common.JsonIsNullOrEmpty(json.SelectToken("error")))
                throw new BadResourceException(json.SelectToken("error").ToString());
            if (Common.JsonIsNullOrEmpty(json.SelectToken("stream")))
                throw new StreamOfflineException();
            return new Models.API.Stream(json.SelectToken("stream"));
        }

        public static async Task<List<Models.API.Stream>> GetStreams(List<string> channels)
        {
            var json = JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/streams?channel={string.Join(",", channels)}"));
            List<Models.API.Stream> streams = new List<Models.API.Stream>();
            streams.AddRange(json.SelectToken("streams").Select(stream => new Models.API.Stream(stream)));
            return streams;
        }

        public static async Task<List<FeaturedStream>> GetFeaturedStreams(int limit = 25, int offset = 0)
        {
            var json = JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/streams/featured?limit={limit}&offset={offset}"));
            List<FeaturedStream> streams = new List<FeaturedStream>();
            streams.AddRange(json.SelectToken("streams").Select(stream => new Models.API.FeaturedStream(stream)));
            return streams;
        }
        
        public static async Task<StreamsSummary> GetStreamsSummary()
        {
            var json = await MakeGetRequest("https://api.twitch.tv/kraken/streams/summary");
            return new StreamsSummary(json);
        }
        #endregion

        #region Searching
        public static async Task<List<Channel>> SearchChannels(string query, int limit = 25, int offset = 0)
        {
            var returnedChannels = new List<Channel>();
            var json = JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/search/channels?query={query}&limit={limit}&offset={offset}"));
            if (json.SelectToken("_total").ToString() == "0") return returnedChannels;
            returnedChannels.AddRange(json.SelectToken("channels").Select(channelToken => new Channel((JObject)channelToken)));
            return returnedChannels;
        }

        
        public static async Task<List<Models.API.Stream>> SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
        {
            var returnedStreams = new List<Models.API.Stream>();
            var hlsStr = "";
            if (hls == true) hlsStr = "&hls=true";
            if (hls == false) hlsStr = "&hls=false";
            var args = $"?query={query}&limit={limit}&offset={offset}{hlsStr}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/search/streams{args}");

            var json = JObject.Parse(resp);
            if (json.SelectToken("_total").ToString() == "0") return returnedStreams;
            returnedStreams.AddRange(
                json.SelectToken("streams").Select(streamToken => new Models.API.Stream((JObject)streamToken)));
            return returnedStreams;
        }

        
        public static async Task<List<Game>> SearchGames(string query, bool live = false)
        {
            var returnedGames = new List<Game>();

            var args = $"?query={query}&type=suggest&live=" + live.ToString();
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/search/games{args}");

            var json = JObject.Parse(resp);
            returnedGames.AddRange(
                json.SelectToken("games").Select(gameToken => new Game((JObject)gameToken)));
            return returnedGames;
        }

        
        public static async Task<List<GameByPopularityListing>> GetGamesByPopularity(int limit = 10, int offset = 0)
        {
            var returnedGames = new List<GameByPopularityListing>();

            var args = $"?limit={limit}&offset={offset}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/games/top{args}");

            var json = JObject.Parse(resp);
            returnedGames.AddRange(
                json.SelectToken("top").Select(gameToken => new GameByPopularityListing((JObject)gameToken)));
            return returnedGames;
        }
        #endregion

        #region Chatters
        
        public static async Task<List<Chatter>> GetChatters(string channel)
        {
            var resp = await MakeGetRequest($"https://tmi.twitch.tv/group/user/{channel.ToLower()}/chatters");
            var chatters = JObject.Parse(resp).SelectToken("chatters");
            var chatterList =
                chatters.SelectToken("moderators")
                    .Select(user => new Chatter(user.ToString(), Enums.UserType.Moderator))
                    .ToList();
            chatterList.AddRange(
                chatters.SelectToken("staff").Select(user => new Chatter(user.ToString(), Enums.UserType.Staff)));
            chatterList.AddRange(
                chatters.SelectToken("admins").Select(user => new Chatter(user.ToString(), Enums.UserType.Admin)));
            chatterList.AddRange(
                chatters.SelectToken("global_mods")
                    .Select(user => new Chatter(user.ToString(), Enums.UserType.GlobalModerator)));
            chatterList.AddRange(
                chatters.SelectToken("viewers").Select(user => new Chatter(user.ToString(), Enums.UserType.Viewer)));
            return chatterList;
        }
        #endregion

        #region TitleAndGame
        public static async Task<string> UpdateStreamTitle(string status, string channel, string accessToken = null)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\"}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }

        
        public static async Task<string> UpdateStreamGame(string game, string channel, string accessToken = null)
        {
            var data = "{\"channel\":{\"game\":\"" + game + "\"}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }

        
        public static async Task<Channel> UpdateStreamTitleAndGame(string status, string game, string channel,
            string accessToken = null)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\",\"game\":\"" + game + "\"}}";
            return new Channel(JObject.Parse(await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken)));
        }
        #endregion

        #region Streaming
        public static async Task<string> ResetStreamKey(string channel, string accessToken = null)
        {
            return await
                MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}/streamkey", "DELETE", "", accessToken);
        }

        public static async Task<string> UpdateStreamDelay(int delay, string channel, string accessToken = null)
        {
            var data = "{\"channel\":{\"delay\":" + delay + "}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }
        #endregion

        #region Blocking
        public static async Task<List<Block>> GetBlockedList(string username, string accessToken = null, int limit = 25, int offset = 0)
        {
            string args = $"?limit={limit}&offset={offset}";
            string resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}/blocks{args}", accessToken);
            JObject json = JObject.Parse(resp);
            List<Block> blocks = new List<Block>();
            if (json.SelectToken("blocks") != null)
                foreach (JToken block in json.SelectToken("blocks"))
                    blocks.Add(new Block(block));
            return blocks;
        }

        public static async Task<Block> BlockUser(string username, string blockedUsername, string accessToken = null)
        {
            return new Block(JObject.Parse(await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/blocks/{blockedUsername}", "PUT", "", accessToken)));
        }

        public static async void UnblockUser(string username, string blockedUsername, string accessToken = null)
        {
            await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/blocks/{blockedUsername}", "DELETE", "", accessToken);
        }
        #endregion

        #region Follows
        public static async Task<Follow> UserFollowsChannel(string username, string channel)
        {
            try
            {
                string resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}");
                return new Follow(resp);
            }
            catch
            {
                return new Follow(null, false);
            }
        }

        public static async Task<FollowersResponse> GetTwitchFollowers(string channel, int limit = 25,
            string cursor = "-1", Enums.SortDirection direction = Enums.SortDirection.Descending)
        {
            string args = "";

            args += "?limit=" + limit;
            args += cursor != "-1" ? $"&cursor={cursor}" : "";
            args += "&direction=" + (direction == Enums.SortDirection.Descending ? "desc" : "asc");

            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/follows{args}");
            return new FollowersResponse(resp);
        }

        public static async Task<FollowedUsersResponse> GetFollowedUsers(string channel, int limit = 25, int offset = 0, Enums.SortKey sortKey = Enums.SortKey.CreatedAt)
        {
            string args = "";
            args += "?limit=" + limit;
            args += "&offset=" + offset;
            switch (sortKey)
            {
                case Enums.SortKey.CreatedAt:
                    args += "&sortby=created_at";
                    break;
                case Enums.SortKey.LastBroadcaster:
                    args += "&sortby=last_broadcast";
                    break;
                case Enums.SortKey.Login:
                    args += "&sortby=login";
                    break;
            }

            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{channel}/follows/channels{args}");
            return new FollowedUsersResponse(resp);
        }

        public static async Task<Follow> FollowChannel(string username, string channel, string accessToken = null)
        {
            return new Follow(await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}", "PUT", "", accessToken));
        }

        public static async void UnfollowChannel(string username, string channel, string accessToken = null)
        {
            await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}", "DELETE", "", accessToken);
        }
        #endregion  

        #region Subscriptions
        public static async Task<int> GetSubscriberCount(string channel, string accessToken = null)
        {
            var resp =
                await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions", accessToken);
            var json = JObject.Parse(resp);
            return int.Parse(json.SelectToken("_total").ToString());
        }

        public static async Task<ChannelHasUserSubscribedResponse> ChannelHasUserSubscribed(string username, string channel, string accessToken = null)
        {
            try
            {
                return new ChannelHasUserSubscribedResponse(JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions/{username}", accessToken)));
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Videos
        public static async Task<List<Video>> GetChannelVideos(string channel, int limit = 10,
            int offset = 0, bool onlyBroadcasts = false, bool onlyHls = false)
        {
            var args = $"?limit={limit}&offset={offset}&broadcasts={onlyBroadcasts}&hls={onlyHls}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/videos{args}");
            var vids = JObject.Parse(resp).SelectToken("videos");

            return vids.Select(vid => new Video(vid)).ToList();
        }
        #endregion

        #region Commercials
        public static async Task<string> RunCommercial(Enums.CommercialLength length, string channel,
            string accessToken = null)
        {
            // Default to 30 seconds?
            int seconds = 30;
            switch(length)
            {
                case Enums.CommercialLength.Seconds30:
                    seconds = 30;
                    break;
                case Enums.CommercialLength.Seconds60:
                    seconds = 60;
                    break;
                case Enums.CommercialLength.Seconds90:
                    seconds = 90;
                    break;
                case Enums.CommercialLength.Seconds120:
                    seconds = 120;
                    break;
                case Enums.CommercialLength.Seconds150:
                    seconds = 150;
                    break;
                case Enums.CommercialLength.Seconds180:
                    seconds = 180;
                    break;
            }
            return await
                MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}/commercial", "POST",
                    $"length={seconds}", accessToken);
        }
        #endregion

        #region Clips
        public static async Task<ClipsResponse> GetTopClips(List<string> channels = null, List<string> games = null, int limit = 10, string cursor = null, Enums.Period period = Enums.Period.Day, bool trending = false)
        {
            string channelsStr = (channels != null) ? $"channel={string.Join(",", channels)}" : null;
            string gamesStr = (games != null) ? $"game={string.Join(",", games)}" : null;
            string limitStr = $"limit={limit}";
            string cursorStr = (cursor != null) ? $"cursor={cursor}" : null;
            string periodStr = "";
            switch(period)
            {
                case Enums.Period.All:
                    periodStr = "period=all";
                    break;
                case Enums.Period.Day:
                    periodStr = "period=day";
                    break;
                case Enums.Period.Month:
                    periodStr = "period=month";
                    break;
                case Enums.Period.Week:
                    periodStr = "period=week";
                    break;
            }
            string trendingStr = (trending) ? "trending=true" : "trending=false";
            string url = $"https://api.twitch.tv/kraken/clips/top?{limitStr}&{periodStr}";
            if (channels != null)
                url = $"{url}&{channelsStr}";
            if (games != null)
                url = $"{url}&{gamesStr}";
            if (cursor != null)
                url = $"{url}&{cursorStr}";
            return new ClipsResponse(JObject.Parse(await MakeGetRequest(url, null, 4)));
        }
        
        public static async Task<Clip> GetClipInformation(string channel, string slug)
        {
            string url = $"https://api.twitch.tv/kraken/clips/{channel}/{slug}";
            return new Clip(JObject.Parse(await MakeGetRequest(url, null, 4)));
        }

        public static async Task<ClipsResponse> GetFollowedClips(string cursor = "0", int limit = 10, bool trending = false, string accessToken = null)
        {
            string cursorStr = $"cursor={cursor}";
            string limitStr = $"limit={limit}";
            string trendingStr = $"trending={trending.ToString().ToLower()}";
            string url = $"https://api.twitch.tv/kraken/clips/followed?{cursorStr}&{limitStr}&{trendingStr}";
            return new ClipsResponse(JObject.Parse(await MakeRestRequest(url, "POST", null, accessToken, 4)));
        }
        #endregion

        #region Other
        public static void SetClientId(string clientId, bool disableClientIdValidation = false)
        {
            if (ClientId != null && clientId == ClientId)
                return;
            ClientId = clientId;
            if (!disableClientIdValidation)
                ValidClientId();
        }

        public static void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                AccessToken = accessToken;
        }

        public static async Task<bool> ValidClientId(string clientId, bool updateClientIdOnSuccess = true)
        {
            string oldClientId;
            if (!string.IsNullOrEmpty(ClientId))
                oldClientId = ClientId;
            var resp = await MakeGetRequest("https://api.twitch.tv/kraken");
            var json = JObject.Parse(resp);
            if (json.SelectToken("identified") != null && (bool)json.SelectToken("identified") == true)
                return true;
            return false;
        }

        private static async void ValidClientId()
        {
            if (await ValidClientId(ClientId, false) == false)
                throw new InvalidCredentialException("The provided Client-Id is invalid. Create an application here and obtain a Client-Id from it here: https://www.twitch.tv/settings/connections");
        }

        
        public static async Task<bool> BroadcasterOnline(string channel)
        {
            try
            {
                var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/streams/{channel}");
                return resp.Contains("{\"stream\":{\"_id\":");
            }
            catch (InvalidCredentialException badCredentials)
            {
                throw badCredentials;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<string> GetChannelFromSteamId(string steamId)
        {
            try
            {
                string resp = await MakeGetRequest($"https://api.twitch.tv/api/steam/{steamId}");
                return JObject.Parse(resp).SelectToken("name").ToString();
            } catch(Exception)
            {
                return null;
            }
        }
        #endregion

        public static async Task<PostToChannelFeedResponse> PostToChannelFeed(string content, bool share, string channel, string accessToken = null)
        {
            return new PostToChannelFeedResponse(JObject.Parse(await MakeRestRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts", "POST", $"content={content}&share={(share ? "true" : "false")}", accessToken)));
        }

        public static async void DeleteChannelFeedPost(string postId, string channel, string accessToken = null)
        {
            await MakeRestRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", "DELETE", null, accessToken);
        }

        #region Internal Calls
        private static async Task<string> MakeGetRequest(string url, string accessToken = null, int apiVersion = 3)
        {
            if (string.IsNullOrEmpty(ClientId) && string.IsNullOrWhiteSpace(accessToken) && string.IsNullOrWhiteSpace(AccessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token. Set Client-Id by using SetClientId(\"client_id_here\")");

            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            // If the URL already has GET parameters, we cannot use the GET parameter initializer '?'
            HttpWebRequest request = url.Contains("?")
                ? (HttpWebRequest)WebRequest.Create(new Uri($"{url}&client_id={ClientId}"))
                : (HttpWebRequest)WebRequest.Create(new Uri($"{url}?client_id={ClientId}"));

            request.Method = "GET";
            request.Accept = $"application/vnd.twitchtv.v{apiVersion}+json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
            else if (!string.IsNullOrEmpty(AccessToken))
                request.Headers.Add("Authorization", $"OAuth {AccessToken}");

            try
            {
                using (var responseStream = await request.GetResponseAsync())
                {
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
                }
            } catch(WebException e) { handleWebException(e); return null; }
            
        }

        private static async Task<string> MakeRestRequest(string url, string method, string requestData = null,
            string accessToken = null, int apiVersion = 3)
        {
            if (string.IsNullOrWhiteSpace(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token.");

            var data = new UTF8Encoding().GetBytes(requestData ?? "");
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest)WebRequest.Create(new Uri($"{url}?client_id={ClientId}"));
            request.Method = method;
            request.Accept = $"application/vnd.twitchtv.v{apiVersion}+json";
            request.ContentType = method == "POST"
                ? "application/x-www-form-urlencoded"
                : "application/json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
            else if (!string.IsNullOrWhiteSpace(AccessToken))
                request.Headers.Add("Authorization", $"OAuth {AccessToken}");

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(data, 0, data.Length);
            }

            try
            {
                using (var responseStream = await request.GetResponseAsync())
                {
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
                }
            } catch(WebException e) { handleWebException(e); return null; }
            
        }

        private static void handleWebException(WebException e)
        {
            HttpWebResponse errorResp = e.Response as HttpWebResponse;
            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new BadScopeException("Your request was blocked due to bad credentials (do you have the right scope for your access token?).");
                case HttpStatusCode.NotFound:
                    throw new BadResourceException("The resource you tried to access was not valid.");
                case (HttpStatusCode)422:
                    throw new NotPartneredException("The resource you requested is only available to channels that have been partnered by Twitch.");
                default:
                    throw e;
            }
        }
        #endregion
    }
}
