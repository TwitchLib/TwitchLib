using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     Search for channels, streams or games with queries.
    /// </summary>
    public class Search : ApiBase
    {
        /// <summary>
        ///     Execute a search query on Twitch to find a list of channels.
        /// </summary>
        /// <param name="query">A url-encoded search query.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <returns>A list of TwitchChannel objects matching the query.</returns>
        public static async Task<List<TwitchChannel>> SearchChannels(string query, int limit = 25, int offset = 0)
        {
            var returnedChannels = new List<TwitchChannel>();

            try
            {
                var args = $"?query={query}&limit={limit}&offset={offset}";
                var resp = await MakeGetRequest($"{KrakenBaseUrl}/search/channels{args}");

                var json = JObject.Parse(resp);
                if (json.SelectToken("_total").ToString() == "0") return returnedChannels;
                returnedChannels.AddRange(
                    json.SelectToken("channels").Select(channelToken => new TwitchChannel((JObject) channelToken)));
                return returnedChannels;
            }
            catch
            {
                return returnedChannels;
            }
        }
    }
}