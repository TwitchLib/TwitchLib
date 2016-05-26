using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    public class Streams : ApiBase
    {
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
    }
}