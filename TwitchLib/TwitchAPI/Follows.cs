using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     Status of follow relationships between users and channels.
    /// </summary>
    public class Follows : ApiBase
    {
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
    }
}