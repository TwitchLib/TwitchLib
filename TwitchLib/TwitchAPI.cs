using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Linq;
using TwitchLib.Exceptions;
using TwitchLib.TwitchAPIClasses;

namespace TwitchLib
{
    public static class TwitchApi
    {
        #region Enums
        /// <summary>
        /// A list of valid commercial lengths.
        /// </summary>
        public enum CommercialLength
        {
            Seconds30 = 30,
            Seconds60 = 60,
            Seconds90 = 90,
            Second120 = 120,
            Seconds150 = 150,
            Seconds180 = 180
        }

        /// <summary>
        /// A list of valid sorting directions.
        /// </summary>
        public enum SortDirection
        {
            Descending,
            Ascending
        }
        #endregion

        #region Get Objects
        /// <summary>
        /// Retrieves a string list of channel editor users.
        /// <para>Authenticated, required scope: <code>channel_read</code></para>
        /// </summary>
        /// <param name="channel">The channel to fetch editors from.</param>
        /// <param name="accessToken">An access token with the required scope.</param>
        /// <returns>A list of User objects that are channel editors.</returns>
        public static async Task<List<User>> GetChannelEditors(string channel, string accessToken)
        {
            var json = JObject.Parse(await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/editors", accessToken));
            List<User> editors = new List<User>();
            foreach (JToken editor in json.SelectToken("users"))
                editors.Add(new User(editor.ToString()));
            return editors;
        }

        /// <summary>
        /// Retrieves a string list of channels hosting a specified channel.
        /// <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed. Additionally, this makes 2 API calls so limited use is recommended.</para>
        /// </summary>
        /// <param name="channel">The name of the channel to search for.</param>
        /// <returns>A list of all channels that are currently hosting the specified channel.</returns>
        public static async Task<List<string>> GetChannelHosts(string channel)
        {
            var hosts = new List<string>();
            var client = new WebClient();
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{channel}");
            var json = JObject.Parse(resp);
            if (json.SelectToken("_id") == null) return hosts;
            resp = await MakeGetRequest($"http://tmi.twitch.tv/hosts?include_logins=1&target={json.SelectToken("_id")}");
            json = JObject.Parse(resp);
            hosts.AddRange(json.SelectToken("hosts").Select(host => host.SelectToken("host_login").ToString()));
            return hosts;
        }

        /// <summary>
        /// Retrieves a TwitchTeamMember list of all members in a Twitch team.
        /// <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed.</para>
        /// </summary>
        /// <param name="teamName">The name of the Twitch team to search for.</param>
        /// <returns>A TwitchTeamMember list of all members in a Twitch team.</returns>
        public static async Task<List<TeamMember>> GetTeamMembers(string teamName)
        {
            var resp = await MakeGetRequest($"http://api.twitch.tv/api/team/{teamName}/all_channels.json");
            var json = JObject.Parse(resp);
            return
                json.SelectToken("channels")
                    .Select(member => new TeamMember(member.SelectToken("channel")))
                    .ToList();
        }

        /// <summary>
        /// Retrieves a TwitchStream object containing API data related to a stream.
        /// </summary>
        /// <param name="channel">The name of the channel to search for.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<Channel> GetTwitchChannel(string channel)
        {
            var resp = "";
            try
            {
                resp = await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}");
            }
            catch
            {
                throw new InvalidChannelException(resp);
            }
            var json = JObject.Parse(resp);
            if (json.SelectToken("error") != null) throw new InvalidChannelException(resp);
            return new Channel(json);
        }

        /// <summary>
        /// Retrieves a User object from Twitch Api and returns User object.
        /// </summary>
        /// <param name="username">Name of the user you wish to fetch from Twitch.</param>
        /// <returns>User object containing details about the searched for user. Returns null if invalid user/error.</returns>
        public static async Task<User> GetUser(string username)
        {
            try
            {
                var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{username}");
                JObject j = JObject.Parse(resp);
                if (j.SelectToken("error") != null)
                    return null;
                return new User(resp);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the current uptime of a stream, if it is online.
        /// </summary>
        /// <param name="channel">The channel to retrieve the uptime for.</param>
        /// <returns>A TimeSpan object representing time between creation_at of stream, and now.</returns>
        public static async Task<TimeSpan> GetUptime(string channel)
        {
            var stream = await GetTwitchStream(channel);
            if (stream == null)
                return TimeSpan.Zero;
            var time = Convert.ToDateTime(stream.CreatedAt);
            return DateTime.UtcNow - time;
        }

        /// <summary>
        /// Retrieves channel feed posts.
        /// </summary>
        /// <param name="channel">Channel to fetch feed posts from.</param>
        /// <param name="limit">Applied limit (default 10, max 100)</param>
        /// <param name="cursor">Used for pagination.</param>
        /// <returns></returns>
        public static async Task<FeedResponse> GetChannelFeed(string channel, int limit = 10, string cursor = null)
        {
            var args = $"?limit={limit}";
            if (cursor != null)
                args += $"&cursor={cursor};";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/feed/{channel}/posts{args}");
            return new FeedResponse(JObject.Parse(resp));
        }

        /// <summary>
        /// Retrieves a collection of API data from a stream.
        /// </summary>
        /// <param name="channel">The channel to retrieve the data for.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<TwitchAPIClasses.Stream> GetTwitchStream(string channel)
        {
            try
            {
                var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/streams/{channel}");
                var json = JObject.Parse(resp);
                return json.SelectToken("stream").SelectToken("_id") != null
                    ? new TwitchAPIClasses.Stream(json.SelectToken("stream"))
                    : null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Searching
        /// <summary>
        /// Execute a search query on Twitch to find a list of channels.
        /// </summary>
        /// <param name="query">A url-encoded search query.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <returns>A list of Channel objects matching the query.</returns>
        public static async Task<List<Channel>> SearchChannels(string query, int limit = 25, int offset = 0)
        {
            var returnedChannels = new List<Channel>();
            var args = $"?query={query}&limit={limit}&offset={offset}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/search/channels{args}");

            var json = JObject.Parse(resp);
            if (json.SelectToken("_total").ToString() == "0") return returnedChannels;
            returnedChannels.AddRange(
                json.SelectToken("channels").Select(channelToken => new Channel((JObject)channelToken)));
            return returnedChannels;
        }

        /// <summary>
        /// Execute a search query on Twitch to find a list of streams.
        /// </summary>
        /// <param name="query">A url-encoded search query.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <param name="hls">If set to true, only returns streams using HLS, if set to false only returns non-HLS streams. Default is null.</param>
        /// <returns>A list of Stream objects matching the query.</returns>
        public static async Task<List<TwitchAPIClasses.Stream>> SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
        {
            var returnedStreams = new List<TwitchAPIClasses.Stream>();
            var hlsStr = "";
            if (hls == true) hlsStr = "&hls=true";
            if (hls == false) hlsStr = "&hls=false";
            var args = $"?query={query}&limit={limit}&offset={offset}{hlsStr}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/search/streams{args}");

            var json = JObject.Parse(resp);
            if (json.SelectToken("_total").ToString() == "0") return returnedStreams;
            returnedStreams.AddRange(
                json.SelectToken("streams").Select(streamToken => new TwitchAPIClasses.Stream((JObject)streamToken)));
            return returnedStreams;
        }

        /// <summary>
        /// Execute a search query on Twitch to find a list of games.
        /// </summary>
        /// <param name="query">A url-encoded search query.</param>
        /// <param name="live">If set to true, only games with active streams will be found.</param>
        /// <returns>A list of Game objects matching the query.</returns>
        public static async Task<List<TwitchAPIClasses.Game>> SearchGames(string query, bool live = false)
        {
            var returnedGames = new List<TwitchAPIClasses.Game>();

            var args = $"?query={query}&type=suggest&live=" + live.ToString();
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/search/games{args}");

            var json = JObject.Parse(resp);
            returnedGames.AddRange(
                json.SelectToken("games").Select(gameToken => new TwitchAPIClasses.Game((JObject)gameToken)));
            return returnedGames;
        }
        #endregion

        #region Chatters
        /// <summary>
        /// Retrieves a list of all people currently chatting in a channel's chat.
        /// </summary>
        /// <param name="channel">The channel to retrieve the chatting people for.</param>
        /// <returns>A list of Chatter objects detailing each chatter in a channel.</returns>
        public static async Task<List<Chatter>> GetChatters(string channel)
        {
            var resp = await MakeGetRequest($"https://tmi.twitch.tv/group/user/{channel}/chatters");
            var chatters = JObject.Parse(resp).SelectToken("chatters");
            var chatterList =
                chatters.SelectToken("moderators")
                    .Select(user => new Chatter(user.ToString(), Chatter.UType.Moderator))
                    .ToList();
            chatterList.AddRange(
                chatters.SelectToken("staff").Select(user => new Chatter(user.ToString(), Chatter.UType.Staff)));
            chatterList.AddRange(
                chatters.SelectToken("admins").Select(user => new Chatter(user.ToString(), Chatter.UType.Admin)));
            chatterList.AddRange(
                chatters.SelectToken("global_mods")
                    .Select(user => new Chatter(user.ToString(), Chatter.UType.GlobalModerator)));
            chatterList.AddRange(
                chatters.SelectToken("viewers").Select(user => new Chatter(user.ToString(), Chatter.UType.Viewer)));
            return chatterList;
        }
        #endregion

        #region TitleAndGame
        /// <summary>
        /// Update the <paramref name="status"/> of a <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="status">Channel's title.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamTitle(string status, string channel, string accessToken)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\"}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }

        /// <summary>
        /// Update the <paramref name="game"/> the <paramref name="channel"/> is currently playing.
        /// <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="game">Game category to be classified as.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamGame(string game, string channel, string accessToken)
        {
            var data = "{\"channel\":{\"game\":\"" + game + "\"}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }

        /// <summary>
        /// Update the <paramref name="status"/> and <paramref name="game"/> of a <paramref name="channel"/>.
        /// </summary>
        /// <param name="status">Channel's title.</param>
        /// <param name="game">Game category to be classified as.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamTitleAndGame(string status, string game, string channel,
            string accessToken)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\",\"game\":\"" + game + "\"}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }
        #endregion

        #region Streaming
        /// <summary>
        /// Resets the stream key of the <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>channel_stream</code></para>
        /// </summary>
        /// <param name="channel">The channel to reset the stream key for.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> ResetStreamKey(string channel, string accessToken)
        {
            return await
                MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}/streamkey", "DELETE", "", accessToken);
        }

        /// <summary>
        /// Updates the <paramref name="delay"/> of a <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="delay">Channel delay in seconds.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">The channel owner's access token and the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamDelay(int delay, string channel, string accessToken)
        {
            var data = "{\"channel\":{\"delay\":" + delay + "}}";
            return await MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}", "PUT", data, accessToken);
        }
        #endregion

        #region Blocking
        /// <summary>
        /// Retrieves a list of blocked users a specific user has.
        /// <para>Authenticated, required scope: <code>user_blocks_read</code></para>
        /// </summary>
        /// <param name="username">Username of user to fetch blocked list of.</param>
        /// <param name="accessToken">This call requires an access token.</param>
        /// <param name="limit">Limit output from Twitch Api. Default 25, max 100.</param>
        /// <param name="offset">Offset out from Twitch Api. Default 0.</param>
        /// <returns>List of Block objects.</returns>
        public static async Task<List<Block>> GetBlockedList(string username, string accessToken, int limit = 25, int offset = 0)
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

        /// <summary>
        /// Blocks a user.
        /// <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
        /// </summary>
        /// <param name="username">User who's blocked list to add to.</param>
        /// <param name="blockedUsername">User to block.</param>
        /// <param name="accessToken">This call requires an access token.</param>
        /// <returns>Block object.</returns>
        public static async Task<Block> BlockUser(string username, string blockedUsername, string accessToken)
        {
            return new Block(JObject.Parse(await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/blocks/{blockedUsername}", "PUT", "", accessToken)));
        }

        /// <summary>
        /// Unblocks a user.
        /// <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
        /// </summary>
        /// <param name="username">User who's blocked list to unblock from.</param>
        /// <param name="blockedUsername">User to unblock.</param>
        /// <param name="accessToken">This call requires an access token.</param>
        public static async void UnblockUser(string username, string blockedUsername, string accessToken)
        {
            await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/blocks/{blockedUsername}", "DELETE", "", accessToken);
        }
        #endregion

        #region Follows
        /// <summary>
        /// Retrieves whether a specified user is following the specified user.
        /// </summary>
        /// <param name="username">The user to check the follow status of.</param>
        /// <param name="channel">The channel to check against.</param>
        /// <returns>Returns Follow object representing follow relationship.</returns>
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

        /// <summary>
        /// Retrieves an ascending or descending list of followers from a specific channel.
        /// </summary>
        /// <param name="channel">The channel to retrieve the followers from.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="cursor">Twitch uses cursoring to paginate long lists of followers. Check <code>_cursor</code> in response body and set <code>cursor</code> to this value to get the next page of results, or use <code>_links.next</code> to navigate to the next page of results.</param>
        /// <param name="direction">Creation date sorting direction.</param>
        /// <returns>A list of TwitchFollower objects.</returns>
        public static async Task<FollowersResponse> GetTwitchFollowers(string channel, int limit = 25,
            int cursor = -1, SortDirection direction = SortDirection.Descending)
        {
            string args = "";

            args += "?limit=" + limit;
            args += cursor != -1 ? $"&cursor={cursor}" : "";
            args += "&direction=" + (direction == SortDirection.Descending ? "desc" : "asc");

            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/follows{args}");
            return new FollowersResponse(resp);
        }

        /// <summary>
        /// Retrieves a list of followed users a specific user has.
        /// </summary>
        /// <param name="channel">Channel to fetch followed users</param>
        /// <param name="limit">Default is 25, max is 100, min is 0</param>
        /// <param name="offset">Integer representing list offset</param>
        /// <param name="sortKey">Enum representing sort order.</param>
        /// <returns>FollowedUsersResponse object.</returns>
        public static async Task<FollowedUsersResponse> GetFollowedUsers(string channel, int limit = 25, int offset = 0, Common.SortKey sortKey = Common.SortKey.CreatedAt)
        {
            string args = "";
            args += "?limit=" + limit;
            args += "&offset=" + offset;
            switch (sortKey)
            {
                case Common.SortKey.CreatedAt:
                    args += "&sortby=created_at";
                    break;
                case Common.SortKey.LastBroadcaster:
                    args += "&sortby=last_broadcast";
                    break;
                case Common.SortKey.Login:
                    args += "&sortby=login";
                    break;
            }

            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{channel}/follows/channels{args}");
            return new FollowedUsersResponse(resp);
        }

        /// <summary>
        /// Follows a channel given by <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>user_follows_edit</code></para>
        /// </summary>
        /// <param name="username">The username of the user trying to follow the given channel.</param>
        /// <param name="channel">The channel to follow.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>A follow object representing the follow action.</returns>
        public static async Task<Follow> FollowChannel(string username, string channel, string accessToken)
        {
            return new Follow(await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}", "PUT", "", accessToken));
        }

        /// <summary>
        /// Unfollows a channel given by <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>user_follows_edit</code></para>
        /// </summary>
        /// <param name="username">The username of the user trying to follow the given channel.</param>
        /// <param name="channel">The channel to unfollow.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        public static async void UnfollowChannel(string username, string channel, string accessToken)
        {
            await MakeRestRequest($"https://api.twitch.tv/kraken/users/{username}/follows/channels/{channel}", "DELETE", "", accessToken);
        }
        #endregion  

        #region Subscriptions
        /// <summary>
        /// Returns the amount of subscribers <paramref name="channel"/> has.
        /// <para>Authenticated, required scope: <code>channel_subscriptions</code></para>
        /// </summary>
        /// <param name="channel">The channel to retrieve the subscriptions from.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>An integer of the total subscription count.</returns>
        public static async Task<int> GetSubscriberCount(string channel, string accessToken)
        {
            var resp =
                await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions", accessToken);
            var json = JObject.Parse(resp);
            return int.Parse(json.SelectToken("_total").ToString());
        }

        /// <summary>
        /// Retrieves whether a <paramref name="username"/> is subscribed to a <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>channel_check_subscription</code></para>
        /// </summary>
        /// <param name="username">The user to check subscription status for.</param>
        /// <param name="channel">The channel to check against.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>True if the user is subscribed to the channel, false otherwise.</returns>
        public static async Task<bool> ChannelHasUserSubscribed(string username, string channel, string accessToken)
        {
            try
            {
                await
                    MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions/{username}",
                        accessToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Videos
        /// <summary>
        /// Returns a list of videos ordered by time of creation, starting with the most recent.
        /// </summary>
        /// <param name="channel">The channel to retrieve the list of videos from.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 10. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <param name="onlyBroadcasts">Returns only broadcasts when true. Otherwise only highlights are returned. Default is false.</param>
        /// <param name="onlyHls">Returns only HLS VoDs when true. Otherwise only non-HLS VoDs are returned. Default is false.</param>
        /// <returns>A list of TwitchVideo objects the channel has available.</returns>
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
        /// <summary>
        /// Start a commercial on <paramref name="channel"/>.
        /// <para>Authenticated, required scope: <code>channel_commercial</code></para>
        /// </summary>
        /// <param name="length">Length of commercial break in seconds. Default value is 30. You can only trigger a commercial once every 8 minutes.</param>
        /// <param name="channel">The channel to start a commercial on.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> RunCommercial(CommercialLength length, string channel,
            string accessToken)
        {
            return await
                MakeRestRequest($"https://api.twitch.tv/kraken/channels/{channel}/commercial", "POST",
                    $"length={length}", accessToken);
        }
        #endregion

        #region Other
        /// <summary>
        /// Sets ClientId, which is required for all API calls going forward from August 3rd. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// <param name="disableClientIdValidation">Forcefully disables Client-Id validation.</param>
        /// </summary>
        public static void SetClientId(string clientId, bool disableClientIdValidation = false)
        {
            ClientId = clientId;
            if (!disableClientIdValidation)
                ValidClientId();
        }

        private static string ClientId { get; set; }

        /// <summary>
        /// Validates a Client-Id and optionally updates it.
        /// </summary>
        /// <param name="clientId">Client-Id string to be validated.</param>
        /// <param name="updateClientIdOnSuccess">Updates Client-Id if passed Client-Id is valid.</param>
        /// <returns>True or false depending on the validity of the Client-Id.</returns>
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

        /// <summary>
        /// Retrieves the current status of the broadcaster.
        /// </summary>
        /// <param name="channel">The name of the broadcaster to check.</param>
        /// <returns>True if the broadcaster is online, false otherwise.</returns>
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
        #endregion

        #region Internal Calls
        private static async Task<string> MakeGetRequest(string url, string accessToken = null)
        {
            if (string.IsNullOrEmpty(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token as of August 3rd. Set Client-Id by using SetClientId()");

            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.ContentType = "application/json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");

            using (var responseStream = await request.GetResponseAsync())
            {
                return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
            }
        }

        private static async Task<string> MakeRestRequest(string url, string method, string requestData = null,
            string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token as of August 3rd.");

            var data = new UTF8Encoding().GetBytes(requestData ?? "");
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = method;
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.ContentType = "application/json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(data, 0, data.Length);
            }

            using (var responseStream = await request.GetResponseAsync())
            {
                return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
            }
        }
        #endregion
    }
}
