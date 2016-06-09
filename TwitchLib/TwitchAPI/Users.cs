using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     These are members of the Twitch community who have a Twitch account. If broadcasting, they can own a stream that
    ///     they can broadcast on their channel. If mainly viewing, they might follow or subscribe to channels.
    /// </summary>
    public class Users : ApiBase
    {
        /// <summary>
        ///     Retrieves a string list of channels hosting a specified channel.
        ///     <para>
        ///         Note: This uses an undocumented API endpoint and reliability is not guaranteed.
        ///         Additionally, this makes 2 API calls so limited use is recommended.
        ///     </para>
        /// </summary>
        /// <param name="channel">The name of the channel to search for.</param>
        /// <returns>A list of all channels that are currently hosting the specified channel.</returns>
        public static async Task<List<string>> GetChannelHosts(string channel)
        {
            var hosts = new List<string>();
            var resp = await MakeGetRequest($"{KrakenBaseUrl}/users/{channel}");
            var json = JObject.Parse(resp);
            if (json.SelectToken("_id") == null) return hosts;
            resp = await MakeGetRequest($"{TmiBaseUrl}/hosts?include_logins=1&target={json.SelectToken("_id")}");
            json = JObject.Parse(resp);
            hosts.AddRange(json.SelectToken("hosts").Select(host => host.SelectToken("host_login").ToString()));
            return hosts;
        }

        /// <summary>
        ///     Retrieves whether a specified user is following the specified user.
        /// </summary>
        /// <param name="username">The user to check the follow status of.</param>
        /// <param name="channel">The channel to check against.</param>
        /// <returns>True if the user is following the channel, false otherwise.</returns>
        public static async Task<bool> UserFollowsChannel(string username, string channel)
        {
            try
            {
                await MakeGetRequest($"{KrakenBaseUrl}/users/{username}/follows/channels/{channel}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}