namespace TwitchLib
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Streams
    {
        public Streams(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
            Helix = new helix(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }
        public helix Helix { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetStream
            public async Task<Models.API.v3.Streams.StreamResponse> GetStreamAsync(string channel)
            {
                return await Api.GetGenericAsync<Models.API.v3.Streams.StreamResponse>($"https://api.twitch.tv/kraken/streams/{channel}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetStreams
            public async Task<Models.API.v3.Streams.StreamsResponse> GetStreamsAsync(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                if (game != null)
                    paramsStr += $"&game={game}";
                if (channel != null)
                    paramsStr += $"&channel={channel}";
                if (clientId != null)
                    paramsStr += $"&client_id={clientId}";
                if (language != null)
                    paramsStr += $"&language={language}";
                switch (streamType)
                {
                    case Enums.StreamType.All:
                        break;
                    case Enums.StreamType.Live:
                        break;
                    case Enums.StreamType.Playlist:
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Streams.StreamsResponse>($"https://api.twitch.tv/kraken/streams{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFeaturedStreams
            public async Task<Models.API.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreamsAsync(int limit = 25, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return await Api.GetGenericAsync<Models.API.v3.Streams.FeaturedStreamsResponse>($"https://api.twitch.tv/kraken/streams/featured{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetStreamsSummary
            public async Task<Models.API.v3.Streams.Summary> GetStreamsSummaryAsync()
            {
                return await Api.GetGenericAsync<Models.API.v3.Streams.Summary>("https://api.twitch.tv/kraken/streams/summary", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetStreamByUser
            public async Task<Models.API.v5.Streams.StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching streams. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                {
                    optionalQuery = $"?stream_type={streamType}";
                }
                return await Api.GetGenericAsync<Models.API.v5.Streams.StreamByUser>($"https://api.twitch.tv/kraken/streams/{channelId}{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetLiveStreams
            public async Task<Models.API.v5.Streams.LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (channelList != null && channelList.Count > 0)
                    queryParameters.Add(new KeyValuePair<string, string>("channel", string.Join(",", channelList)));
                if (!string.IsNullOrWhiteSpace(game))
                    queryParameters.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    queryParameters.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    queryParameters.Add(new KeyValuePair<string, string>("stream_type", streamType));
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
                return await Api.GetGenericAsync<Models.API.v5.Streams.LiveStreams>($"https://api.twitch.tv/kraken/streams/{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetStreamsSummary
            public async Task<Models.API.v5.Streams.StreamsSummary> GetStreamsSummaryAsync(string game = null)
            {
                string optionalQuery = (!string.IsNullOrWhiteSpace(game)) ? $"?game={game}" : string.Empty;
                return await Api.GetGenericAsync<Models.API.v5.Streams.StreamsSummary>($"https://api.twitch.tv/kraken/streams/summary{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetFeaturedStreams
            public async Task<Models.API.v5.Streams.FeaturedStreams> GetFeaturedStreamAsync(int? limit = null, int? offset = null)
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
                return await Api.GetGenericAsync<Models.API.v5.Streams.FeaturedStreams>($"https://api.twitch.tv/kraken/streams/featured{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedStreams
            public async Task<Models.API.v5.Streams.FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    queryParameters.Add(new KeyValuePair<string, string>("stream_type", streamType));
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
                return await Api.GetGenericAsync<Models.API.v5.Streams.FollowedStreams>($"https://api.twitch.tv/kraken/streams/followed{optionalQuery}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUptime
            public async Task<TimeSpan?> GetUptimeAsync(string channelId)
            {
                try
                {
                    var stream = await GetStreamByUserAsync(channelId).ConfigureAwait(false);
                    return DateTime.UtcNow - stream.Stream.CreatedAt;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            #endregion
            #region BroadcasterOnline
            public async Task<bool> BroadcasterOnlineAsync(string channelId)
            {
                var res = await GetStreamByUserAsync(channelId).ConfigureAwait(false);
                return res.Stream != null;
            }
            #endregion
        }

        public class helix : ApiSection
        {
            public helix(TwitchAPI api) : base(api)
            {
            }
            public async Task<Models.API.Helix.Streams.GetStreams.GetStreamsResponse> GetStreams(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
            {
                string getParams = $"?first={first}&type={type}";
                if (after != null)
                    getParams += $"&after={after}";
                if (communityIds != null && communityIds.Count > 0)
                    foreach (var communityId in communityIds)
                        getParams += $"&community_id={communityId}";
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        getParams += $"&game_id={gameId}";
                if (languages != null && languages.Count > 0)
                    foreach (var language in languages)
                        getParams += $"&language={language}";
                if (userIds != null && userIds.Count > 0)
                    foreach (var userId in userIds)
                        getParams += $"&user_id={userId}";
                if (userLogins != null && userLogins.Count > 0)
                    foreach (var userLogin in userLogins)
                        getParams += $"&user_login={userLogin}";

                return await Api.GetGenericAsync<Models.API.Helix.Streams.GetStreams.GetStreamsResponse>($"https://api.twitch.tv/helix/streams{getParams}", api: ApiVersion.Helix).ConfigureAwait(false);
            }

            public async Task<Models.API.Helix.StreamsMetadata.GetStreamsMetadataResponse> GetStreamsMetadata(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
            {
                string getParams = $"?first={first}&type={type}";
                if (after != null)
                    getParams += $"&after={after}";
                if (communityIds != null && communityIds.Count > 0)
                    foreach (var communityId in communityIds)
                        getParams += $"&community_id={communityId}";
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        getParams += $"&game_id={gameId}";
                if (languages != null && languages.Count > 0)
                    foreach (var language in languages)
                        getParams += $"&language={language}";
                if (userIds != null && userIds.Count > 0)
                    foreach (var userId in userIds)
                        getParams += $"&user_id={userId}";
                if (userLogins != null && userLogins.Count > 0)
                    foreach (var userLogin in userLogins)
                        getParams += $"&user_login={userLogin}";

                return await Api.GetGenericAsync<Models.API.Helix.StreamsMetadata.GetStreamsMetadataResponse>($"https://api.twitch.tv/helix/streams/metadata{getParams}", api: ApiVersion.Helix).ConfigureAwait(false);
            }
        }
    }
}