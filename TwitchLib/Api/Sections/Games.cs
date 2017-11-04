namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion

    public class Games
    {
        public Games(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
            helix = new Helix(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }
        public Helix helix { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetTopGames
            public async Task<Models.API.v3.Games.TopGamesResponse> GetTopGamesAsync(int limit = 10, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return await Api.GetGenericAsync<Models.API.v3.Games.TopGamesResponse>($"https://api.twitch.tv/kraken/games/top{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetTopGames
            public async Task<Models.API.v5.Games.TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
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
                return await Api.GetGenericAsync<Models.API.v5.Games.TopGames>($"https://api.twitch.tv/kraken/games/top{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            #region GetGames
            public async Task<Models.API.v5.Games.TopGames> GetTopGamesAsync(List<string> gameIds = null, List<string> gameNames = null)
            {
                if ((gameIds == null && gameNames == null) ||
                    (gameIds != null && gameIds.Count == 0 && gameNames == null) ||
                    (gameNames != null && gameNames.Count == 0 && gameIds == null))
                    throw new Exceptions.API.BadParameterException("Either gameIds or gameNames must have at least one value");
                if (gameIds != null && gameIds.Count > 100)
                    throw new Exceptions.API.BadParameterException("gameIds list cannot exceed 100 items");
                if (gameNames != null && gameNames.Count > 100)
                    throw new Exceptions.API.BadParameterException("gameNames list cannot exceed 100 items");

                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        queryParameters.Add(new KeyValuePair<string, string>("id", gameId));
                if (gameNames != null && gameNames.Count > 0)
                    foreach (var gameName in gameNames)
                        queryParameters.Add(new KeyValuePair<string, string>("name", gameName));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return await Api.GetGenericAsync<Models.API.v5.Games.TopGames>($"https://api.twitch.tv/helix/games{optionalQuery}", null, ApiVersion.Helix).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
