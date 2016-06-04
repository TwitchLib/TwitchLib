using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     Videos are broadcasts or chapters owned by a channel. Broadcasts are unedited videos that are saved after a
    ///     streaming session. Chapters are videos edited from broadcasts by the channel's owner.
    /// </summary>
    public class Videos : ApiBase
    {
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
    }
}