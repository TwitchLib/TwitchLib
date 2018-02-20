using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api
{
    public class Streams
    {
        public Streams(TwitchAPI api)
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
            #region GetStream
            public async Task<Models.v3.Streams.StreamResponse> GetStreamAsync(string channel)
            {
                return await Api.GetGenericAsync<Models.v3.Streams.StreamResponse>($"https://api.twitch.tv/kraken/streams/{channel}", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetStreams
            public async Task<Models.v3.Streams.StreamsResponse> GetStreamsAsync(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, StreamType streamType = StreamType.All, string language = "en")
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("offset", offset.ToString())
                };
                if (game != null)
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (channel != null)
                    getParams.Add(new KeyValuePair<string, string>("channel", channel));
                if (clientId != null)
                    getParams.Add(new KeyValuePair<string, string>("client_id", clientId));
                if (language != null)
                    getParams.Add(new KeyValuePair<string, string>("language", language));
                switch (streamType)
                {
                    case StreamType.All:
                        getParams.Add(new KeyValuePair<string, string>("stream_type", "all"));
                        break;
                    case StreamType.Live:
                        getParams.Add(new KeyValuePair<string, string>("stream_type", "live"));
                        break;
                    case StreamType.Playlist:
                        getParams.Add(new KeyValuePair<string, string>("stream_type", "playlist"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(streamType), streamType, null);
                }

                return await Api.GetGenericAsync<Models.v3.Streams.StreamsResponse>("https://api.twitch.tv/kraken/streams", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFeaturedStreams
            public async Task<Models.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreamsAsync(int limit = 25, int offset = 0)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("offset", offset.ToString())
                };
                return await Api.GetGenericAsync<Models.v3.Streams.FeaturedStreamsResponse>("https://api.twitch.tv/kraken/streams/featured", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetStreamsSummary
            public async Task<Models.v3.Streams.Summary> GetStreamsSummaryAsync()
            {
                return await Api.GetGenericAsync<Models.v3.Streams.Summary>("https://api.twitch.tv/kraken/streams/summary", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetStreamByUser
            public async Task<Models.v5.Streams.StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for fetching streams. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                {
                    getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));
                }
                return await Api.GetGenericAsync<Models.v5.Streams.StreamByUser>($"https://api.twitch.tv/kraken/streams/{channelId}", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetLiveStreams
            public async Task<Models.v5.Streams.LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (channelList != null && channelList.Count > 0)
                    getParams.Add(new KeyValuePair<string, string>("channel", string.Join(",", channelList)));
                if (!string.IsNullOrWhiteSpace(game))
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    getParams.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return await Api.GetGenericAsync<Models.v5.Streams.LiveStreams>("https://api.twitch.tv/kraken/streams/", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetStreamsSummary
            public async Task<Models.v5.Streams.StreamsSummary> GetStreamsSummaryAsync(string game = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (game != null)
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                return await Api.GetGenericAsync<Models.v5.Streams.StreamsSummary>("https://api.twitch.tv/kraken/streams/summary", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetFeaturedStreams
            public async Task<Models.v5.Streams.FeaturedStreams> GetFeaturedStreamAsync(int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return await Api.GetGenericAsync<Models.v5.Streams.FeaturedStreams>("https://api.twitch.tv/kraken/streams/featured", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedStreams
            public async Task<Models.v5.Streams.FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Read, authToken);
                var getParams = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                    getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                return await Api.GetGenericAsync<Models.v5.Streams.FollowedStreams>("https://api.twitch.tv/kraken/streams/followed", getParams, authToken).ConfigureAwait(false);
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

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            public async Task<Models.Helix.Streams.GetStreams.GetStreamsResponse> GetStreams(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("first", first.ToString()), new KeyValuePair<string, string>("type", type) };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (communityIds != null && communityIds.Count > 0)
                    foreach (var communityId in communityIds)
                        getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
                if (languages != null && languages.Count > 0)
                    foreach (var language in languages)
                        getParams.Add(new KeyValuePair<string, string>("language", language));
                if (userIds != null && userIds.Count > 0)
                    foreach (var userId in userIds)
                        getParams.Add(new KeyValuePair<string, string>("user_id", userId));
                if (userLogins != null && userLogins.Count > 0)
                    foreach (var userLogin in userLogins)
                        getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));

                return await Api.GetGenericAsync<Models.Helix.Streams.GetStreams.GetStreamsResponse>("https://api.twitch.tv/helix/streams", getParams, api: ApiVersion.Helix).ConfigureAwait(false);
            }

            public async Task<Models.Helix.StreamsMetadata.GetStreamsMetadataResponse> GetStreamsMetadata(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("first", first.ToString()), new KeyValuePair<string, string>("type", type) };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (communityIds != null && communityIds.Count > 0)
                    foreach (var communityId in communityIds)
                        getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
                if (gameIds != null && gameIds.Count > 0)
                    foreach (var gameId in gameIds)
                        getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
                if (languages != null && languages.Count > 0)
                    foreach (var language in languages)
                        getParams.Add(new KeyValuePair<string, string>("language", language));
                if (userIds != null && userIds.Count > 0)
                    foreach (var userId in userIds)
                        getParams.Add(new KeyValuePair<string, string>("user_id", userId));
                if (userLogins != null && userLogins.Count > 0)
                    foreach (var userLogin in userLogins)
                        getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));

                return await Api.GetGenericAsync<Models.Helix.StreamsMetadata.GetStreamsMetadataResponse>("https://api.twitch.tv/helix/streams/metadata", getParams, api: ApiVersion.Helix).ConfigureAwait(false);
            }
        }
    }
}