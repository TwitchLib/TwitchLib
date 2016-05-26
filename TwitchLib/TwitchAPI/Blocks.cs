using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    /// Stores and updates information about a user's block list.
    /// </summary>
    public class Blocks : ApiBase
    {
        /// <summary>
        ///     Retrieve a list of blocks objects on <paramref name="channel" />'s block list. List sorted by recency, newest
        ///     first.
        ///     <para>Authenticated, required scope: <code>user_blocks_read</code></para>
        /// </summary>
        /// <param name="channel">The channel to retrieve the blocklist for.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
        /// <param name="offset">Object offset for pagination. Default is 0.</param>
        /// <returns>A list containing TwitchUser objects.</returns>
        public static async Task<List<TwitchUser>> GetBlockList(string channel, string accessToken, int limit = 25,
            int offset = 0)
        {
            var blockedChannels = new List<TwitchUser>();
            var args = $"?limit={limit}&offset={offset}";
            var resp = await MakeGetRequest($"https://api.twitch.tv/kraken/users/{channel}/blocks{args}", accessToken);

            var json = JObject.Parse(resp);
            blockedChannels.AddRange(
                json.SelectToken("blocks").Select(channelToken => new TwitchUser((JObject) channelToken)));
            return blockedChannels;
        }

        /// <summary>
        ///     Adds <paramref name="user" /> to <paramref name="channel" />'s block list.
        ///     <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
        /// </summary>
        /// <param name="user">The user to be blocked.</param>
        /// <param name="channel">The authenticated user.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>A blocks object.</returns>
        public static async Task<string> BlockUser(string user, string channel, string accessToken)
        {
            return
                await
                    MakeRestRequest($"https://api.twitch.tv/kraken/users/{channel}/blocks/{user}", "PUT", "",
                        accessToken);
        }

        /// <summary>
        ///     Removes <paramref name="user" /> from <paramref name="channel" />'s block list.
        ///     <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
        /// </summary>
        /// <param name="user">The user to be unblocked.</param>
        /// <param name="channel">The authenticated user.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns><code>204 No Content</code> if successful.</returns>
        public static async Task<string> UnblockUser(string user, string channel, string accessToken)
        {
            return
                await
                    MakeRestRequest($"https://api.twitch.tv/kraken/users/{channel}/blocks/{user}", "DELETE", "",
                        accessToken);
        }
    }
}