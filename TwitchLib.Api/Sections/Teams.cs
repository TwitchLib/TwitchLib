using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api
{
    public class Teams
    {
        public Teams(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetTeams
            public async Task<Models.v3.Teams.GetTeamsResponse> GetTeamsAsync(int limit = 25, int offset = 0)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("offset", offset.ToString())
                };
                return await Api.GetGenericAsync<Models.v3.Teams.GetTeamsResponse>("https://api.twitch.tv/kraken/teams", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetTeam
            public async Task<Models.v3.Teams.Team> GetTeamAsync(string teamName)
            {
                return await Api.GetGenericAsync<Models.v3.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetAllTeams
            public async Task<Models.v5.Teams.AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return await Api.GetGenericAsync<Models.v5.Teams.AllTeams>("https://api.twitch.tv/kraken/teams", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetTeam
            public async Task<Models.v5.Teams.Team> GetTeamAsync(string teamName)
            {
                if (string.IsNullOrWhiteSpace(teamName)) { throw new BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.v5.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}").ConfigureAwait(false);
            }
            #endregion
        }
    }
}