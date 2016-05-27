using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TwitchLib.Exceptions;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     Channels serve as the home location for a user's content. Channels have a stream, can run commercials, store
    ///     videos, display information and status, and have a customized page including banners and backgrounds.
    /// </summary>
    public class Channels : ApiBase
    {
        /// <summary>
        ///     Retrieves a TwitchStream object containing API data related to a stream.
        /// </summary>
        /// <param name="channel">The name of the channel to search for.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<TwitchChannel> GetTwitchChannel(string channel)
        {
            var resp = "";
            try
            {
                resp = await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}");
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
        ///     Retrieves a channel object of authenticated user. Channel object includes stream key.
        ///     <para>Authenticated, required scope: <code>channel_read</code></para>
        /// </summary>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<TwitchChannel> GetOwnTwitchChannel(string accessToken)
        {
            var resp = "";
            try
            {
                resp = await MakeGetRequest($"{KrakenBaseUrl}/channels", accessToken);
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
        ///     Retrieves an ascending or descending list of followers from a specific channel.
        /// </summary>
        /// <param name="channel">The channel to retrieve the followers from.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="cursor">
        ///     Twitch uses cursoring to paginate long lists of followers. Check <code>_cursor</code> in response
        ///     body and set <code>cursor</code> to this value to get the next page of results, or use <code>_links.next</code> to
        ///     navigate to the next page of results.
        /// </param>
        /// <param name="direction">Creation date sorting direction.</param>
        /// <returns>A list of TwitchFollower objects.</returns>
        public static async Task<List<TwitchFollower>> GetTwitchFollowers(string channel, int limit = 25,
            int cursor = -1, SortDirection direction = SortDirection.Descending)
        {
            var args = "";

            args += "?limit=" + limit;
            args += cursor != -1 ? $"&cursor={cursor}" : "";
            args += "&direction=" + (direction == SortDirection.Descending ? "desc" : "asc");

            var resp = await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}/follows{args}");
            return JObject.Parse(resp).SelectToken("follows").Select(follower => new TwitchFollower(follower)).ToList();
        }

        /// <summary>
        ///     Updates the <paramref name="delay" /> of a <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="delay">Channel delay in seconds.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">The channel owner's access token and the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamDelay(int delay, string channel, string accessToken)
        {
            var data = "{\"channel\":{\"delay\":" + delay + "}}";
            return await MakeRestRequest($"{KrakenBaseUrl}/channels/{channel}", "PUT", data, accessToken);
        }

        /// <summary>
        ///     Update the <paramref name="status" /> of a <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="status">Channel's title.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamTitle(string status, string channel, string accessToken)
        {
            var data = "{\"channel\":{\"status\":\"" + status + "\"}}";
            return await MakeRestRequest($"{KrakenBaseUrl}/channels/{channel}", "PUT", data, accessToken);
        }

        /// <summary>
        ///     Update the <paramref name="game" /> the <paramref name="channel" /> is currently playing.
        ///     <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="game">Game category to be classified as.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> UpdateStreamGame(string game, string channel, string accessToken)
        {
            var data = "{\"channel\":{\"game\":\"" + game + "\"}}";
            return await MakeRestRequest($"{KrakenBaseUrl}/channels/{channel}", "PUT", data, accessToken);
        }

        /// <summary>
        ///     Update the <paramref name="status" /> and <paramref name="game" /> of a <paramref name="channel" />.
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
            return await MakeRestRequest($"{KrakenBaseUrl}/channels/{channel}", "PUT", data, accessToken);
        }

        /// <summary>
        ///     Resets the stream key of the <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_stream</code></para>
        /// </summary>
        /// <param name="channel">The channel to reset the stream key for.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> ResetStreamKey(string channel, string accessToken)
        {
            return await
                MakeRestRequest($"{KrakenBaseUrl}/channels/{channel}/streamkey", "DELETE", "", accessToken);
        }

        /// <summary>
        ///     Returns a list of videos ordered by time of creation, starting with the most recent.
        /// </summary>
        /// <param name="channel">The channel to retrieve the list of videos from.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 10. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <param name="onlyBroadcasts">
        ///     Returns only broadcasts when true. Otherwise only highlights are returned. Default is
        ///     false.
        /// </param>
        /// <param name="onlyHls">Returns only HLS VoDs when true. Otherwise only non-HLS VoDs are returned. Default is false.</param>
        /// <returns>A list of TwitchVideo objects the channel has available.</returns>
        public static async Task<List<TwitchVideo>> GetChannelVideos(string channel, int limit = 10,
            int offset = 0, bool onlyBroadcasts = false, bool onlyHls = false)
        {
            var args = $"?limit={limit}&offset={offset}&broadcasts={onlyBroadcasts}&hls={onlyHls}";
            var resp = await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}/videos{args}");
            var vids = JObject.Parse(resp).SelectToken("videos");

            return vids.Select(vid => new TwitchVideo(vid)).ToList();
        }

        /// <summary>
        ///     Start a commercial on <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_commercial</code></para>
        /// </summary>
        /// <param name="length">
        ///     Length of commercial break in seconds. Default value is 30. You can only trigger a commercial once
        ///     every 8 minutes.
        /// </param>
        /// <param name="channel">The channel to start a commercial on.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> RunCommercial(CommercialLength length, string channel,
            string accessToken)
        {
            return await
                MakeRestRequest($"{KrakenBaseUrl}/channels/{channel}/commercial", "POST",
                    $"length={length}", accessToken);
        }
    }
}