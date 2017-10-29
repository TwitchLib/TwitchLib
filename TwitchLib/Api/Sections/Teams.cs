namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
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
            public async Task<Models.API.v3.Teams.GetTeamsResponse> GetTeamsAsync(int limit = 25, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return await Api.GetGenericAsync<Models.API.v3.Teams.GetTeamsResponse>($"https://api.twitch.tv/kraken/teams{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetTeam
            public async Task<Models.API.v3.Teams.Team> GetTeamAsync(string teamName)
            {
                return await Api.GetGenericAsync<Models.API.v3.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetAllTeams
            public async Task<Models.API.v5.Teams.AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return await Api.GetGenericAsync<Models.API.v5.Teams.AllTeams>($"https://api.twitch.tv/kraken/teams{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetTeam
            public async Task<Models.API.v5.Teams.Team> GetTeamAsync(string teamName)
            {
                if (string.IsNullOrWhiteSpace(teamName)) { throw new Exceptions.API.BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }
    }
}