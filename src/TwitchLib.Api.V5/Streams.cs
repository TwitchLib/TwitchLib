using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Streams;
using LiveStreams = TwitchLib.Api.V5.Models.Streams.LiveStreams;

namespace TwitchLib.Api.V5
{
    public class Streams : ApiBase
    {
        public Streams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetStreamByUser

        public Task<StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
        {
            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid for fetching streams. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));

            return TwitchGetGenericAsync<StreamByUser>($"/streams/{channelId}", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetLiveStreams

        public Task<LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
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

            return TwitchGetGenericAsync<LiveStreams>("/streams", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetStreamsSummary

        public Task<StreamsSummary> GetStreamsSummaryAsync(string game = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (game != null)
                getParams.Add(new KeyValuePair<string, string>("game", game));

            return TwitchGetGenericAsync<StreamsSummary>("/streams/summary", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetFeaturedStreams

        public Task<FeaturedStreams> GetFeaturedStreamAsync(int? limit = null, int? offset = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

            return TwitchGetGenericAsync<FeaturedStreams>("/streams/featured", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetFollowedStreams

        public Task<FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                getParams.Add(new KeyValuePair<string, string>("stream_type", streamType));
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            if (offset != null)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

            return TwitchGetGenericAsync<FollowedStreams>("/streams/followed", ApiVersion.V5, getParams, authToken);
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

}
