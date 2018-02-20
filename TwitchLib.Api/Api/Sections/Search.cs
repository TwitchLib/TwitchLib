using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Enums;

namespace TwitchLib
{
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
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("query", query),
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("offset", offset.ToString())
                };
                return await Api.GetGenericAsync<Models.API.v3.Search.SearchChannelsResponse>("https://api.twitch.tv/kraken/search/channels", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region SearchStreams
            public async Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreamsAsync(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    // Checks?
                    new KeyValuePair<string, string>("query", query),
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("offset", offset.ToString())
                };

                if(hls.HasValue)
                {
                    getParams.Add(hls.Value
                        ? new KeyValuePair<string, string>("hls", "true")
                        : new KeyValuePair<string, string>("hls", "false"));
                }
                return await Api.GetGenericAsync<Models.API.v3.Search.SearchStreamsResponse>("https://api.twitch.tv/kraken/search/streams", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region SearchGames
            public async Task<Models.API.v3.Search.SearchGamesResponse> SearchGamesAsync(string query, GameSearchType type = GameSearchType.Suggest, bool live = false)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    // Checks?
                    new KeyValuePair<string, string>("query", query),
                    new KeyValuePair<string, string>("live", live.ToString().ToLower())
                };
                switch (type)
                {
                    case GameSearchType.Suggest:
                        getParams.Add(new KeyValuePair<string, string>("type", "suggest"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                return await Api.GetGenericAsync<Models.API.v3.Search.SearchGamesResponse>("https://api.twitch.tv/kraken/search/games", getParams, null, ApiVersion.v3).ConfigureAwait(false);
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
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("query", encodedSearchQuery) };
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                
                return await Api.GetGenericAsync<Models.API.v5.Search.SearchChannels>("https://api.twitch.tv/kraken/search/channels", getParams).ConfigureAwait(false);
            }
            #endregion
            #region SearchGames
            public async Task<Models.API.v5.Search.SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if(live.HasValue)
                {
                    getParams.Add(live.Value
                        ? new KeyValuePair<string, string>("live", "true")
                        : new KeyValuePair<string, string>("live", "false"));
                }
                return await Api.GetGenericAsync<Models.API.v5.Search.SearchGames>($"https://api.twitch.tv/kraken/search/games?query={encodedSearchQuery}", getParams).ConfigureAwait(false);
            }
            #endregion
            #region SearchStreams
            public async Task<Models.API.v5.Search.SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("query", encodedSearchQuery) };
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (hls.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("hls", hls.Value.ToString()));

                return await Api.GetGenericAsync<Models.API.v5.Search.SearchStreams>("https://api.twitch.tv/kraken/search/streams", getParams).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
