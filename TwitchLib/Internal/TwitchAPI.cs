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
using TwitchLib.Models.API.User;
using TwitchLib.Models.API.Team;
using System.Drawing;

namespace TwitchLib.Internal
{
    /// <summary>Static class with functionality for Twitch API calls.</summary>
    internal static class TwitchApi
    {
        // Internal variables
        internal static string ClientId { get; set; }
        internal static string AccessToken { get; set; } 

        #region Get Objects
        internal static async Task<Models.API.Channel.ChannelEventsResponse> GetChannelEvents(string channelId)
        {
            return new Models.API.Channel.ChannelEventsResponse(JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/v5/channels/{channelId}/events")));
        }

        internal static async Task<Models.API.Channel.Channels> GetChannelsObject(string channel)
        {
            return new Models.API.Channel.Channels(JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/api/channels/{channel}")));
        }

        internal static async Task<Models.API.Badge.BadgeResponse> GetChannelBadges(string channel)
        {
            return new Models.API.Badge.BadgeResponse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/chat/{channel}/badges"));
        }

        internal static async Task<List<User>> GetChannelEditors(string channel, string accessToken = null)
        {
            var json = JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/editors", accessToken));
            List<User> editors = new List<User>();
            foreach (JToken editor in json.SelectToken("users"))
                editors.Add(new User(editor.ToString()));
            return editors;
        }

        internal static async Task<List<string>> GetChannelHosts(string channel)
        {
            var hosts = new List<string>();
            User user = await GetUser(channel);
            var json = JObject.Parse(await Requests.MakeGetRequest($"http://tmi.twitch.tv/hosts?include_logins=1&target={user.Id}"));
            hosts.AddRange(json.SelectToken("hosts").Select(host => host.SelectToken("host_login").ToString()));
            return hosts;
        }

        internal static async Task<List<TeamMember>> GetTeamMembers(string teamName)
        {
            var json = JObject.Parse(await Requests.MakeGetRequest($"http://api.twitch.tv/api/team/{teamName}/all_channels.json"));
            return
                json.SelectToken("channels")
                    .Select(member => new TeamMember(member.SelectToken("channel")))
                    .ToList();
        }

        internal static async Task<Models.API.Channel.Channel> GetChannel(string channel)
        {
            return new Models.API.Channel.Channel(JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}")));
        }

        internal static async Task<User> GetUser(string username)
        {
            return new User(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}"));
        }

        internal static async Task<TimeSpan> GetUptime(string channel)
        {
            return (await GetStream(channel)).TimeSinceCreated;
        }

        internal static async Task<bool> StreamIsLive(string channel)
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

        internal static async Task<Models.API.Feed.FeedResponse> GetChannelFeed(string channel, int limit = 10, string cursor = null)
        {
            var args = $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor};";
            return new Models.API.Feed.FeedResponse(JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts{args}")));
        }

        internal static async Task<Models.API.Stream.Stream> GetStream(string channel)
        {
            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams/{channel}");
            var json = JObject.Parse(resp);
            if (!Common.Helpers.JsonIsNullOrEmpty(json.SelectToken("error")))
                throw new BadResourceException(json.SelectToken("error").ToString());
            if (Common.Helpers.JsonIsNullOrEmpty(json.SelectToken("stream")))
                throw new StreamOfflineException();
            return new Models.API.Stream.Stream(json.SelectToken("stream"));
        }

        internal static async Task<List<Models.API.Stream.Stream>> GetStreams(List<string> channels)
        {
            var json = JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams?channel={string.Join(",", channels)}"));
            List<Models.API.Stream.Stream> streams = new List<Models.API.Stream.Stream>();
            streams.AddRange(json.SelectToken("streams").Select(stream => new Models.API.Stream.Stream(stream)));
            return streams;
        }

        internal static async Task<List<Models.API.Stream.FeaturedStream>> GetFeaturedStreams(int limit = 25, int offset = 0)
        {
            var json = JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams/featured?limit={limit}&offset={offset}"));
            List<Models.API.Stream.FeaturedStream> streams = new List<Models.API.Stream.FeaturedStream>();
            streams.AddRange(json.SelectToken("featured").Select(stream => new Models.API.Stream.FeaturedStream(stream)));
            return streams;
        }
        
        internal static async Task<Models.API.Stream.StreamsSummary> GetStreamsSummary()
        {
            var json = await Requests.MakeGetRequest("https://api.twitch.tv/kraken/streams/summary");
            return new Models.API.Stream.StreamsSummary(json);
        }

        internal static async Task<Models.API.Stream.FollowedStreamsResponse> GetFollowedStreams(Enums.StreamType streamType = Enums.StreamType.Live, int limit = 25, int offset = 0, string accessToken = null)
        {
            if (limit > 100 || limit < 0)
                throw new Exceptions.API.BadParameterException("Limit must be larger than 0 and equal to or smaller than 100");

            string args = $"?limit={limit}&offset={offset}";
            switch(streamType)
            {
                case Enums.StreamType.Live:
                    args += "&stream_type=live";
                    break;
                case Enums.StreamType.Playlist:
                    args += "&stream_type=playlist";
                    break;
                case Enums.StreamType.All:
                    args += "&stream_type=all";
                    break;
            }

            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams/followed{args}", accessToken, 5);
            return new Models.API.Stream.FollowedStreamsResponse(JObject.Parse(resp));
        }
        #endregion

        #region Searching
        internal static async Task<List<Models.API.Channel.Channel>> SearchChannels(string query, int limit = 25, int offset = 0)
        {
            var returnedChannels = new List<Models.API.Channel.Channel>();
            var json = JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/search/channels?query={query}&limit={limit}&offset={offset}"));
            if (json.SelectToken("_total").ToString() == "0") return returnedChannels;
            returnedChannels.AddRange(json.SelectToken("channels").Select(channelToken => new Models.API.Channel.Channel((JObject)channelToken)));
            return returnedChannels;
        }

        
        internal static async Task<List<Models.API.Stream.Stream>> SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
        {
            var returnedStreams = new List<Models.API.Stream.Stream>();
            var hlsStr = "";
            if (hls == true) hlsStr = "&hls=true";
            if (hls == false) hlsStr = "&hls=false";
            var args = $"?query={query}&limit={limit}&offset={offset}{hlsStr}";
            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/search/streams{args}");

            var json = JObject.Parse(resp);
            if (json.SelectToken("_total").ToString() == "0") return returnedStreams;
            returnedStreams.AddRange(
                json.SelectToken("streams").Select(streamToken => new Models.API.Stream.Stream((JObject)streamToken)));
            return returnedStreams;
        }

        
        internal static async Task<List<Models.API.Game.Game>> SearchGames(string query, bool live = false)
        {
            var returnedGames = new List<Models.API.Game.Game>();

            var args = $"?query={query}&type=suggest&live=" + live.ToString();
            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/search/games{args}");

            var json = JObject.Parse(resp);
            returnedGames.AddRange(
                json.SelectToken("games").Select(gameToken => new Models.API.Game.Game((JObject)gameToken)));
            return returnedGames;
        }

        
        internal static async Task<List<Models.API.Game.GameByPopularityListing>> GetGamesByPopularity(int limit = 10, int offset = 0)
        {
            var returnedGames = new List<Models.API.Game.GameByPopularityListing>();

            var args = $"?limit={limit}&offset={offset}";
            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/games/top{args}");

            var json = JObject.Parse(resp);
            returnedGames.AddRange(
                json.SelectToken("top").Select(gameToken => new Models.API.Game.GameByPopularityListing((JObject)gameToken)));
            return returnedGames;
        }
        #endregion

        #region Chatters
        
        internal static async Task<List<Models.API.Chat.Chatter>> GetChatters(string channel)
        {
            var resp = await Requests.MakeGetRequest($"https://tmi.twitch.tv/group/user/{channel.ToLower()}/chatters");
            var chatters = JObject.Parse(resp).SelectToken("chatters");
            var chatterList =
                chatters.SelectToken("moderators")
                    .Select(user => new Models.API.Chat.Chatter(user.ToString(), Enums.UserType.Moderator))
                    .ToList();
            chatterList.AddRange(
                chatters.SelectToken("staff").Select(user => new Models.API.Chat.Chatter(user.ToString(), Enums.UserType.Staff)));
            chatterList.AddRange(
                chatters.SelectToken("admins").Select(user => new Models.API.Chat.Chatter(user.ToString(), Enums.UserType.Admin)));
            chatterList.AddRange(
                chatters.SelectToken("global_mods")
                    .Select(user => new Models.API.Chat.Chatter(user.ToString(), Enums.UserType.GlobalModerator)));
            chatterList.AddRange(
                chatters.SelectToken("viewers").Select(user => new Models.API.Chat.Chatter(user.ToString(), Enums.UserType.Viewer)));
            return chatterList;
        }
        #endregion

        #region TitleAndGameAndCommunity
        internal static async Task<Models.API.Channel.Channel> UpdateStreamTitle(string status, string channel, string accessToken = null)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\"}}";
            return new Models.API.Channel.Channel(JObject.Parse(await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken)));
        }

        
        internal static async Task<Models.API.Channel.Channel> UpdateStreamGame(string game, string channel, string accessToken = null)
        {
            var data = "{\"channel\":{\"game\":\"" + game + "\"}}";
            return new Models.API.Channel.Channel(JObject.Parse(await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken)));
        }

        
        internal static async Task<Models.API.Channel.Channel> UpdateStreamTitleAndGame(string status, string game, string channel,
            string accessToken = null)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\",\"game\":\"" + game + "\"}}";
            return new Models.API.Channel.Channel(JObject.Parse(await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken)));
        }
        #endregion

        #region Streaming
        internal static async Task<string> ResetStreamKey(string channel, string accessToken = null)
        {
            return await
                Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}/streamkey", "DELETE", "", accessToken);
        }

        internal static async Task<string> UpdateStreamDelay(int delay, string channel, string accessToken = null)
        {
            var data = "{\"channel\":{\"delay\":" + delay + "}}";
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }
        #endregion

        #region Blocking
        internal static async Task<List<Models.API.Block.Block>> GetBlockedList(string username, string accessToken = null, int limit = 25, int offset = 0)
        {
            string args = $"?limit={limit}&offset={offset}";
            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}/blocks{args}", accessToken);
            JObject json = JObject.Parse(resp);
            List<Models.API.Block.Block> blocks = new List<Models.API.Block.Block>();
            if (json.SelectToken("blocks") != null)
                foreach (JToken block in json.SelectToken("blocks"))
                    blocks.Add(new Models.API.Block.Block(block));
            return blocks;
        }

        internal static async Task<Models.API.Block.Block> BlockUser(string username, string blockedUsername, string accessToken = null)
        {
            return new Models.API.Block.Block(JObject.Parse(await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/blocks/{blockedUsername}", "PUT", "", accessToken)));
        }

        internal static async void UnblockUser(string username, string blockedUsername, string accessToken = null)
        {
            await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/blocks/{blockedUsername}", "DELETE", "", accessToken);
        }
        #endregion

        #region Follows
        internal static async Task<Models.API.Follow.Follow> UserFollowsChannel(string username, string channel)
        {
            try
            {
                string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}");
                return new Models.API.Follow.Follow(resp);
            }
            catch
            {
                return new Models.API.Follow.Follow(null, false);
            }
        }

        internal static async Task<Models.API.Follow.FollowersResponse> GetTwitchFollowers(string channel, int limit = 25,
            string cursor = "-1", Enums.SortDirection direction = Enums.SortDirection.Descending)
        {
            string args = "";

            args += "?limit=" + limit;
            args += cursor != "-1" ? $"&cursor={cursor}" : "";
            args += "&direction=" + (direction == Enums.SortDirection.Descending ? "desc" : "asc");

            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/follows{args}");
            return new Models.API.Follow.FollowersResponse(resp);
        }

        internal static async Task<Models.API.Follow.FollowedUsersResponse> GetFollowedUsers(string channel, int limit = 25, int offset = 0, Enums.SortKey sortKey = Enums.SortKey.CreatedAt)
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

            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/users/{channel}/follows/channels{args}");
            return new Models.API.Follow.FollowedUsersResponse(resp);
        }

        internal static async Task<Models.API.Follow.Follow> FollowChannel(string username, string channel, string accessToken = null)
        {
            return new Models.API.Follow.Follow(await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}", "PUT", "", accessToken));
        }

        internal static async void UnfollowChannel(string username, string channel, string accessToken = null)
        {
            await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}", "DELETE", "", accessToken);
        }
        #endregion  

        #region Subscriptions
        internal static async Task<int> GetSubscriberCount(string channel, string accessToken = null)
        {
            var resp =
                await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions", accessToken);
            var json = JObject.Parse(resp);
            return int.Parse(json.SelectToken("_total").ToString());
        }

        internal static async Task<Models.API.Subscriber.SubscribersResponse> GetAllSubscribers(string channel, string accessToken = null)
        {
            // initial stuffs
            List<Models.API.Subscriber.Subscription> allSubs = new List<Models.API.Subscriber.Subscription>();
            int totalSubs;
            var firstBatch = await GetSubscribers(channel, 100, 0, Enums.SortDirection.Ascending, accessToken);
            totalSubs = firstBatch.TotalSubscriberCount;
            allSubs.AddRange(firstBatch.Subscribers);

            // math stuff to determine left over and number of requests
            int leftOverSubs = (totalSubs - firstBatch.Subscribers.Count) % 100;
            int requiredRequests = (totalSubs - firstBatch.Subscribers.Count - leftOverSubs) / 100;

            // perform required requests after initial delay
            int currentOffset = firstBatch.Subscribers.Count;
            System.Threading.Thread.Sleep(1000);
            for (int i = 0; i < requiredRequests; i++)
            {
                var requestedSubs = await GetSubscribers(channel, 100, currentOffset, Enums.SortDirection.Ascending, accessToken);
                allSubs.AddRange(requestedSubs.Subscribers);
                currentOffset += requestedSubs.Subscribers.Count;

                // We should wait a second before performing another request per Twitch requirements
                System.Threading.Thread.Sleep(1000);
            }

            // get leftover subs
            var leftOverSubsRequest = await GetSubscribers(channel, leftOverSubs, currentOffset, Enums.SortDirection.Ascending, accessToken);
            allSubs.AddRange(leftOverSubsRequest.Subscribers);

            return new Models.API.Subscriber.SubscribersResponse(allSubs, totalSubs);
        }

        internal static async Task<Models.API.Subscriber.SubscribersResponse> GetSubscribers(string channel, int limit = 25, int offset = 0, Enums.SortDirection direction = Enums.SortDirection.Ascending, string accessToken = null)
        {
            string args = $"?limit={limit}";
            args += $"&offset={offset}";
            args += $"&direction={(direction == Enums.SortDirection.Descending ? "desc" : "asc")}";

            return new Models.API.Subscriber.SubscribersResponse(JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions{args}", accessToken)));
        }

        internal static async Task<Models.API.Channel.ChannelHasUserSubscribedResponse> ChannelHasUserSubscribed(string username, string channel, string accessToken = null)
        {
            try
            {
                return new Models.API.Channel.ChannelHasUserSubscribedResponse(JObject.Parse(await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions/{username}", accessToken)));
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Videos
        internal static async Task<List<Models.API.Video.Video>> GetChannelVideos(string channel, int limit = 10,
            int offset = 0, bool onlyBroadcasts = false, bool onlyHls = false)
        {
            var args = $"?limit={limit}&offset={offset}&broadcasts={onlyBroadcasts.ToString().ToLower()}&hls={onlyHls.ToString().ToLower()}";
            var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/videos{args}");
            var vids = JObject.Parse(resp).SelectToken("videos");

            return vids.Select(vid => new Models.API.Video.Video(vid)).ToList();
        }

        // TODO: 404ing
        internal static async Task<Models.API.Video.UploadVideo.CreateVideoResponse> CreateVideo(string channel, string title, string accessToken = "")
        {
            JObject data = new JObject();
            data.Add("channel_name", channel);
            data.Add("title", title);

            var resp = await Requests.MakeRestRequest("https://api.twitch.tv/kraken/videos", "POST", data.ToString(), accessToken, 4);
            return new Models.API.Video.UploadVideo.CreateVideoResponse(JObject.Parse(resp));
        }

        // TODO: NEEDS TO BE FIXED
        internal static void UploadVideo(string vidId, string uploadToken, string fileName, string accessToken = null)
        {
            long maxUploadSize = 10737418240; // 10GBs
            //long chunkSize = 10485760; // 10MBs
        
            // Check if file exists
            if (!File.Exists(fileName))
                throw new Exceptions.API.UploadVideo.UploadVideoPart.BadPartException("File doesn't appear to exist!");

            // Check if file abides by Twitch's size rule (between 5mb and 25mb)
            FileInfo info = new FileInfo(fileName);
            if (info.Length > maxUploadSize)
                throw new Exceptions.API.UploadVideo.UploadVideoPart.BadPartException("File is larger than 10GBs. Twitch does not allow files this large.");

            // read and upload chunks
            using (var file = File.OpenRead(fileName))
            {
                /*int bytes;
                var buffer = new byte[chunkSize];
                int i = 1;
                while((bytes = file.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await Requests.MakeRestRequest($"https://uploads.twitch.tv/upload/{vidId}?index={i}&upload_token={uploadToken}", "POST", null, accessToken, 4, buffer);
                    i++;
                }*/
            }
        }

        //TODO: Untested
        internal static async void CompleteVideoUpload(string videoId, string uploadToken, string accessToken = null)
        {
            await Requests.MakeRestRequest($"https://uploads.twitch.tv/upload/{videoId}/complete", "POST", $"upload_token={uploadToken}", accessToken, 4);
        }

        //TODO: Untested
        internal static async Task<Models.API.Video.Video> UploadTwitchVideo(string channel, string title, string fileName, string accessToken = null)
        {
            // Create video
            var resp = await CreateVideo(channel, title, accessToken);

            // Upload video parts
            UploadVideo(resp.Video.Id, resp.Upload.Token, fileName);

            // Complete video upload
            CompleteVideoUpload(resp.Video.Id, resp.Upload.Token, accessToken);

            // Return URL to newly uploaded video
            return resp.Video;
        }

        #endregion

        #region Commercials
        internal static async Task<string> RunCommercial(Enums.CommercialLength length, string channel,
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
                Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}/commercial", "POST",
                    $"length={seconds}", accessToken);
        }
        #endregion

        #region API5
        public async static Task<Models.API.v5.User> GetUserV5ById(string userid)
        {
            string response = (await Internal.Requests.MakeGetRequest($"https://api.twitch.tv/kraken/users/{userid}", null, 5));
            return new Models.API.v5.User(JObject.Parse(response));
        }

        public async static Task<List<Models.API.v5.User>> GetUsersV5(List<string> usernames)
        {
            List<Models.API.v5.User> users = new List<Models.API.v5.User>();
            string response = (await Internal.Requests.MakeGetRequest($"https://api.twitch.tv/kraken/users?login={String.Join(",", usernames)}", null, 5));
            JObject json = JObject.Parse(response);
            users.AddRange(json.SelectToken("users").Select(user => new Models.API.v5.User(user)));
            return users;
        }

        public async static Task<List<Models.API.Stream.Stream>> GetAllStreamsV5(string game = null, List<string> channels = null, Enums.StreamType streamType = Enums.StreamType.Live,
            string language = "en", int limit = 25, int offset = 0, string accessToken = null)
        {
            List<Models.API.Stream.Stream> streams = new List<Models.API.Stream.Stream>();
            string requestParams = $"?language={language}&limit={limit}&offset={offset}";
            if (game != null)
                requestParams += $"&game={game}";
            if (channels != null)
                requestParams += $"&channels={string.Join(",", channels)}";

            string response = (await Internal.Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams/{requestParams}", accessToken, 5));
            JObject json = JObject.Parse(response);
            streams.AddRange(json.SelectToken("streams").Select(stream => new Models.API.Stream.Stream(stream)));
            return streams;
        }
        #endregion

        #region Clips
        internal static async Task<Models.API.Clip.ClipsResponse> GetTopClips(List<string> channels = null, List<string> games = null, int limit = 10, string cursor = null, Enums.Period period = Enums.Period.Day, bool trending = false)
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
            string url = $"https://api.twitch.tv/kraken/clips/top?{limitStr}&{periodStr}&{trendingStr}";
            if (channels != null)
                url = $"{url}&{channelsStr}";
            if (games != null)
                url = $"{url}&{gamesStr}";
            if (cursor != null)
                url = $"{url}&{cursorStr}";
            return new Models.API.Clip.ClipsResponse(JObject.Parse(await Requests.MakeGetRequest(url, null, 4)));
        }
        
        internal static async Task<Models.API.Clip.Clip> GetClipInformation(string channel, string slug)
        {
            string url = $"https://api.twitch.tv/kraken/clips/{channel}/{slug}";
            return new Models.API.Clip.Clip(JObject.Parse(await Requests.MakeGetRequest(url, null, 4)));
        }

        internal static async Task<Models.API.Clip.ClipsResponse> GetFollowedClips(string cursor = "0", int limit = 10, bool trending = false, string accessToken = null)
        {
            string cursorStr = $"cursor={cursor}";
            string limitStr = $"limit={limit}";
            string trendingStr = $"trending={trending.ToString().ToLower()}";
            string url = $"https://api.twitch.tv/kraken/clips/followed?{cursorStr}&{limitStr}&{trendingStr}";
            return new Models.API.Clip.ClipsResponse(JObject.Parse(await Requests.MakeRestRequest(url, "POST", null, accessToken, 4)));
        }
        #endregion

        #region Communities
        internal async static Task<Models.API.Community.Community> GetCommunityByName(string communityName)
        {
            string response = (await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/communities?name={communityName}", null, 5));
            JObject json = JObject.Parse(response);
            JToken val;
            if(json.TryGetValue("error", out val))
                if (val.ToString() == "Not Found")
                    throw new BadResourceException(response);

            return new Models.API.Community.Community(json);
        }

        internal async static Task<Models.API.Community.Community> GetCommunityById(string id)
        {
            string response = (await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/communities/{id}", null, 5));
            JObject json = JObject.Parse(response);
            JToken val;
            if (json.TryGetValue("error", out val))
                if (val.ToString() == "Not Found")
                    throw new BadResourceException(response);

            return new Models.API.Community.Community(json);
        }

        internal async static Task<string> CreateCommunity(string name, string summary, string description, string rules, string accessToken = null)
        {
            if (name.Length < 3 || name.Length > 25)
                throw new BadParameterException("Name parameter must be between 3 and 25 characters of length.");
            if (name.Contains(" "))
                throw new BadParameterException("Name parameter cannot contain space characters.");
            if (summary.Length > 160)
                throw new BadParameterException("Summary parameter must be 160 or less characters of length.");
            if (description.Length > 1572864)
                throw new BadParameterException("Description must be 1,572,864 characters or less of length.");
            if (rules.Length > 1572864)
                throw new BadParameterException("Rules must be 1,572,864 characters or less of length.");

            JObject jsonObj = new JObject();
            jsonObj["name"] = name;
            jsonObj["summary"] = summary;
            jsonObj["description"] = description;
            jsonObj["rules"] = rules;

            string response = (await Requests.MakeRestRequest("https://api.twitch.tv/kraken/communities", "POST", jsonObj.ToString(), accessToken, 5));

            var json = JObject.Parse(response);
            JToken id;
            if (json.TryGetValue("_id", out id))
                return id.ToString();
            else
                return null;
        }

        internal async static Task<string> UpdateCommunity(string communityId, string summary = null, string description = null, string rules = null, string email = null, string accessToken = null)
        {
            if (summary != null && summary.Length > 160)
                throw new BadParameterException("Summary parameter must be 160 or less characters of length.");
            if (description != null && description.Length > 1572864)
                throw new BadParameterException("Description must be 1,572,864 characters or less of length.");
            if (rules != null && rules.Length > 1572864)
                throw new BadParameterException("Rules must be 1,572,864 characters or less of length.");

            JObject json = new JObject();
            if (summary != null)
                json["summary"] = summary;
            if (description != null)
                json["description"] = description;
            if (rules != null)
                json["rules"] = rules;
            if (email != null)
                json["email"] = email;

            return (await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}", "PUT", json.ToString(), accessToken, 5));
        }

        internal async static Task<Models.API.Community.TopCommunitiesResponse> GetTopCommunities(long? limit = null, string cursor = null)
        {
            if (limit != null && limit > 100)
                throw new BadParameterException("Limit may not be larger than 100");

            string args = (limit == null) ? "?limit=10" : $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor}";

            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/communities/top{args}", null, 5);
            return new Models.API.Community.TopCommunitiesResponse(JObject.Parse(resp));
        }

        internal async static Task<Models.API.Community.CommunityBannedUsersResponse> GetCommunityBannedUsers(string communityId, long? limit = null, string cursor = null, string accessToken = null)
        {
            if (limit != null && limit > 100)
                throw new BadParameterException("Limit may not be larger than 100");

            string args = (limit == null) ? "?limit=10" : $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor}";

            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/communities/{communityId}/bans{args}", accessToken, 5);
            return new Models.API.Community.CommunityBannedUsersResponse(JObject.Parse(resp));
        }

        internal async static Task<Models.API.Community.StreamsInCommunityResponse> GetStreamsInCommunity(string communityId, long? limit = null, string cursor = null)
        {
            if (limit != null && limit > 100)
                throw new BadParameterException("Limit may not be larger than 100");

            string args = (limit == null) ? "?limit=10" : $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor}";

            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams?community_id={communityId}", null, 5);
            return new Models.API.Community.StreamsInCommunityResponse(JObject.Parse(resp));
        }

        internal async static Task<string> BanCommunityUser(string communityId, string userId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/bans/{userId}", "PUT", null, accessToken, 5);
        }

        internal async static Task<string> UnBanCommunityUser(string communityId, string userId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/bans/{userId}", "DELETE", null, accessToken, 5);
        }

        internal async static Task<string> TimeoutCommunityUser(string communityId, string userId, int durationInHours, string reason = null, string accessToken = null)
        {
            JObject json = new JObject();
            json["duration"] = durationInHours;
            if (reason != null)
                json["reason"] = reason;
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts/{userId}", "PUT", json.ToString(), accessToken, 5);
        }

        internal async static Task<Models.API.Community.CommunityTimedOutUsersResponse> GetTimedOutCommunityUsers(string communityId, long? limit = null, string cursor = null, string accessToken = null)
        {
            if (limit != null && limit > 100)
                throw new BadParameterException("Limit may not be larger than 100");

            string args = (limit == null) ? "?limit=10" : $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor}";

            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts{args}", accessToken, 5);
            return new Models.API.Community.CommunityTimedOutUsersResponse(JObject.Parse(resp));
        }

        internal async static Task<string> UnTimeoutCommunityUser(string communityId, string userId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts/{userId}", "DELETE", null, accessToken, 5);
        }

        internal async static Task<string> AddCommunityModerator(string communityId, string userId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/moderators/{userId}", "PUT", null, accessToken, 5);
        }

        internal async static Task<List<Models.API.Community.CommunityModerator>> GetCommunityModerators(string communityId)
        {
            List<Models.API.Community.CommunityModerator> communityModerators = new List<Models.API.Community.CommunityModerator>();
            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/communities/{communityId}/moderators", null, 5);
            JObject json = JObject.Parse(resp);
            foreach (JToken mod in json.SelectToken("moderators"))
                communityModerators.Add(new Models.API.Community.CommunityModerator(mod));
            return communityModerators;
        }

        internal async static Task<string> RemoveCommunityModerator(string communityId, string userId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/moderators/{userId}", "DELETE", null, accessToken, 5);
        }
        
        internal async static Task<Models.API.Community.Community> GetChannelCommunity(string channelId)
        {
            string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channelId}/community", null, 5);
            return new Models.API.Community.Community(JObject.Parse(resp));
        }

        internal async static Task<string> SetChannelCommunity(string channelId, string communityId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channelId}/community/{communityId}", "PUT", null, accessToken, 5);
        }

        internal async static Task<string> RemoveChannelCommunity(string channelId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channelId}/community", "DELETE", null, accessToken, 5);
        }

        internal async static Task<string> CreateCommunityAvatarImage(string communityId, string base64AvatarImage, string accessToken = null)
        {
            JObject json = new JObject();
            json["avatar_image"] = base64AvatarImage;
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", "POST", json.ToString(), accessToken, 5);
        }

        internal async static Task<string> CreateCommunityAvatarImage(string communityId, Image avatarImage, string accessToken = null)
        {
            JObject json = new JObject();
            json["avatar_image"] = Common.Helpers.ImageToBase64(avatarImage);
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", "POST", json.ToString(), accessToken, 5);
        }

        internal async static Task<string> RemoveCommunityAvatarImage(string communityId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", "DELETE", null, accessToken, 5);
        }

        internal async static Task<string> CreateCommunityCoverImage(string communityId, string base64CoverImage, string accessToken = null)
        {
            JObject json = new JObject();
            json["cover_image"] = base64CoverImage;
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", "POST", json.ToString(), accessToken, 5);
        }

        internal async static Task<string> CreateCommunityCoverImage(string communityId, Image coverImage, string accessToken = null)
        {
            JObject json = new JObject();
            json["cover_image"] = Common.Helpers.ImageToBase64(coverImage);
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", "POST", json.ToString(), accessToken, 5);
        }

        internal async static Task<string> RemoveCommunityCoverImage(string communityId, string accessToken = null)
        {
            return await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", "DELETE", null, accessToken, 5);
        }
        #endregion

        #region Other
        internal static void SetClientId(string clientId, bool disableClientIdValidation = false)
        {
            if (ClientId != null && clientId == ClientId)
                return;
            ClientId = clientId;
            if (!disableClientIdValidation)
                ValidClientId();
        }

        internal static void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                AccessToken = accessToken;
        }

        internal static async Task<bool> ValidClientId(string clientId, bool updateClientIdOnSuccess = true)
        {
            string oldClientId;
            if (!string.IsNullOrEmpty(ClientId))
                oldClientId = ClientId;
            var resp = await Requests.MakeGetRequest("https://api.twitch.tv/kraken");
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

        internal static async Task<Models.API.Other.Validate.ValidationResponse> ValidationAPIRequest(string accessToken = null)
        {
            string resp = await Requests.MakeGetRequest("https://api.twitch.tv/kraken", accessToken, 5);
            return new Models.API.Other.Validate.ValidationResponse(JObject.Parse(resp));
        }
        
        internal static async Task<bool> BroadcasterOnline(string channel)
        {
            try
            {
                var resp = await Requests.MakeGetRequest($"https://api.twitch.tv/kraken/streams/{channel}");
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

        internal static async Task<string> GetChannelFromSteamId(string steamId)
        {
            try
            {
                string resp = await Requests.MakeGetRequest($"https://api.twitch.tv/api/steam/{steamId}");
                return JObject.Parse(resp).SelectToken("name").ToString();
            } catch(Exception)
            {
                return null;
            }
        }
        #endregion

        internal static async Task<Models.API.Feed.PostToChannelFeedResponse> PostToChannelFeed(string content, bool share, string channel, string accessToken = null)
        {
            return new Models.API.Feed.PostToChannelFeedResponse(JObject.Parse(await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts", "POST", $"content={content}&share={(share ? "true" : "false")}", accessToken)));
        }

        internal static async void DeleteChannelFeedPost(string postId, string channel, string accessToken = null)
        {
            await Requests.MakeRestRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", "DELETE", null, accessToken);
        }

        #region Third Party
        internal static async Task<List<Models.API.ThirdParty.UsernameChangeListing>> GetUsernameChanges(string name)
        {
            List<Models.API.ThirdParty.UsernameChangeListing> changes = new List<Models.API.ThirdParty.UsernameChangeListing>();
            string resp = await Requests.MakeGetRequestClean($"https://twitch-tools.rootonline.de/username_changelogs_search.php?q={name}&format=json");
            JObject json = JObject.Parse(resp);
            foreach (var change in json)
                changes.Add(new Models.API.ThirdParty.UsernameChangeListing(change.Value));
            return changes;
        }
        #endregion

    }
}
