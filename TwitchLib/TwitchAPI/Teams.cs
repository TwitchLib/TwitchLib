using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    public class Teams : ApiBase
    {
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
    }
}