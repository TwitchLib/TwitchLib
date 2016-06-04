using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
            try
            {
                var resp = await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}");
                var json = JObject.Parse(resp);
                return json.SelectToken("error") != null ? new TwitchChannel() : new TwitchChannel(json);
            }
            catch
            {
                return new TwitchChannel();
            }
        }

        /// <summary>
        ///     Retrieves a channel object of authenticated user. Channel object includes stream key.
        ///     <para>Authenticated, required scope: <code>channel_read</code></para>
        /// </summary>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>A TwitchStream object containing API data related to a stream.</returns>
        public static async Task<TwitchChannel> GetOwnTwitchChannel(string accessToken)
        {
            try
            {
                var resp = await MakeGetRequest($"{KrakenBaseUrl}/channels", accessToken);
                var json = JObject.Parse(resp);
                return json.SelectToken("error") != null ? new TwitchChannel() : new TwitchChannel(json);
            }
            catch
            {
                return new TwitchChannel();
            }
        }

        /// <summary>
        ///     Returns a list of user objects who are editors of <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_read</code></para>
        /// </summary>
        /// <param name="channel">The channel to retrieve the editors from.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>A list of TwitchUser objects whom are channel editors.</returns>
        public static async Task<List<TwitchUser>> GetChannelEditors(string channel, string accessToken)
        {
            var resp = await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}/editors", accessToken);
            var editors = JObject.Parse(resp).SelectToken("users");

            return editors.Select(editor => new TwitchUser(editor)).ToList();
        }

        /// <summary>
        ///     Updates the <paramref name="delay" /> of a <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_editor</code></para>
        /// </summary>
        /// <param name="delay">Channel delay in seconds. Requires the channel owner's OAuth token.</param>
        /// <param name="channel">The channel to update.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
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
        public static async Task<string> UpdateStreamStatus(string status, string channel, string accessToken)
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
        public static async Task<string> UpdateStreamStatusAndGame(string status, string game, string channel,
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