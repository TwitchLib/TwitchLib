﻿using System;
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

        /// <summary>
        /// Sets ClientId, which is required for all API calls going forward from August 3rd. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// <param name="disableClientIdValidation">Forcefully disables Client-Id validation.</param>
        /// </summary>
        public static void SetClientId(string clientId, bool disableClientIdValidation = false)
        {
            ClientId = clientId;
            if(!disableClientIdValidation)
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
            catch
            {
                return false;
            }
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
        public static async Task<List<TwitchTeamMember>> GetTeamMembers(string teamName)
        {
            var resp = await MakeGetRequest($"http://api.twitch.tv/api/team/{teamName}/all_channels.json");
            var json = JObject.Parse(resp);
            return
                json.SelectToken("channels")
                    .Select(member => new TwitchTeamMember(member.SelectToken("channel")))
                    .ToList();
        }

        /// <summary>
        /// Retrieves a TwitchStream object containing API data related to a stream.
        /// </summary>
        /// <param name="channel">The name of the channel to search for.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<TwitchChannel> GetTwitchChannel(string channel)
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
            return new TwitchChannel(json);
        }

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
        public static async Task<List<TwitchFollower>> GetTwitchFollowers(string channel, int limit = 25,
            int cursor = -1, SortDirection direction = SortDirection.Descending)
        {
            string args = "";

            args += "?limit=" + limit;
            args += cursor != -1 ? $"&cursor={cursor}" : "";
            args += "&direction=" + (direction == SortDirection.Descending ? "desc" : "asc");

            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/follows{args}");
            return JObject.Parse(resp).SelectToken("follows").Select(follower => new TwitchFollower(follower)).ToList();
        }

        /// <summary>
        /// Retrieves the current uptime of a stream, if it is online.
        /// </summary>
        /// <param name="channel">The channel to retrieve the uptime for.</param>
        /// <returns>A TimeSpan object representing time between creation_at of stream, and now.</returns>
        public static async Task<TimeSpan> GetUptime(string channel)
        {
            var stream = await GetTwitchStream(channel);
            var time = Convert.ToDateTime(stream.CreatedAt);
            return DateTime.UtcNow - time;
        }

        /// <summary>
        /// Retrieves a collection of API data from a stream.
        /// </summary>
        /// <param name="channel">The channel to retrieve the data for.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<TwitchStream> GetTwitchStream(string channel)
        {
            try
            {
                var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/streams/{channel}");
                var json = JObject.Parse(resp);
                return json.SelectToken("stream").SelectToken("_id") != null
                    ? new TwitchStream(json.SelectToken("stream"))
                    : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Execute a search query on Twitch to find a list of channels.
        /// </summary>
        /// <param name="query">A url-encoded search query.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <returns>A list of TwitchChannel objects matching the query.</returns>
        public static async Task<List<TwitchChannel>> SearchChannels(string query, int limit = 25, int offset = 0)
        {
            var returnedChannels = new List<TwitchChannel>();
            var args = $"?query={query}&limit={limit}&offset={offset}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/search/channels{args}");

            var json = JObject.Parse(resp);
            if (json.SelectToken("_total").ToString() == "0") return returnedChannels;
            returnedChannels.AddRange(
                json.SelectToken("channels").Select(channelToken => new TwitchChannel((JObject) channelToken)));
            return returnedChannels;
        }

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

        /// <summary>
        /// Returns a list of videos ordered by time of creation, starting with the most recent.
        /// </summary>
        /// <param name="channel">The channel to retrieve the list of videos from.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 10. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <param name="onlyBroadcasts">Returns only broadcasts when true. Otherwise only highlights are returned. Default is false.</param>
        /// <param name="onlyHls">Returns only HLS VoDs when true. Otherwise only non-HLS VoDs are returned. Default is false.</param>
        /// <returns>A list of TwitchVideo objects the channel has available.</returns>
        public static async Task<List<TwitchVideo>> GetChannelVideos(string channel, int limit = 10,
            int offset = 0, bool onlyBroadcasts = false, bool onlyHls = false)
        {
            var args = $"?limit={limit}&offset={offset}&broadcasts={onlyBroadcasts}&hls={onlyHls}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/channels/{channel}/videos{args}");
            var vids = JObject.Parse(resp).SelectToken("videos");

            return vids.Select(vid => new TwitchVideo(vid)).ToList();
        }

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

        private static async Task<string> MakeGetRequest(string url, string accessToken = null)
        {
            if(string.IsNullOrEmpty(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token as of August 3rd. Set Client-Id by using SetClientId()");

            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
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

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
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
    }
}
