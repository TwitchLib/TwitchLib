namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Search
    {
        public Search(TwitchAPI api)
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
            #region SearchChannels
            public async Task<Models.API.v3.Search.SearchChannelsResponse> SearchChannelsAsync(string query, int limit = 25, int offset = 0)
            {
                string paramsStr = $"?query={query}&limit={limit}&offset={0}";
                return await Api.GetGenericAsync<Models.API.v3.Search.SearchChannelsResponse>($"https://api.twitch.tv/kraken/search/channels{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region SearchStreams
            public async Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreamsAsync(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                string opHls = "";
                if (hls != null)
                {
                    if ((bool)hls)
                        opHls = "&hls=true";
                    else
                        opHls = "&hls=false";
                }

                string paramsStr = $"?query={query}&limit={limit}&offset={offset}{opHls}";
                return await Api.GetGenericAsync<Models.API.v3.Search.SearchStreamsResponse>($"https://api.twitch.tv/kraken/search/streams{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region SearchGames
            public async Task<Models.API.v3.Search.SearchGamesResponse> SearchGamesAsync(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
            {
                string paramsStr = $"?query={query}&live={live.ToString().ToLower()}";
                switch (type)
                {
                    case Enums.GameSearchType.Suggest:
                        paramsStr += $"&type=suggest";
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Search.SearchGamesResponse>($"https://api.twitch.tv/kraken/search/games{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region SearchChannels
            public async Task<Models.API.v5.Search.SearchChannels> SearchChannelsAsync(string encodedSearchQuery, int? limit = null, int? offset = null)
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
                return await Api.GetGenericAsync<Models.API.v5.Search.SearchChannels>($"https://api.twitch.tv/kraken/search/channels?query={encodedSearchQuery}{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region SearchGames
            public async Task<Models.API.v5.Search.SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
            {
                string optionalQuery = (live != null) ? $"?live={live}" : string.Empty;
                return await Api.GetGenericAsync<Models.API.v5.Search.SearchGames>($"https://api.twitch.tv/kraken/search/games?query={encodedSearchQuery}{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region SearchStreams
            public async Task<Models.API.v5.Search.SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (hls != null)
                    queryParameters.Add(new KeyValuePair<string, string>("hls", hls.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return await Api.GetGenericAsync<Models.API.v5.Search.SearchStreams>($"https://api.twitch.tv/kraken/search/streams?query={encodedSearchQuery}{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
