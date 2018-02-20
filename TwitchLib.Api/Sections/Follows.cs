using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api
{
    public class Follows
    {
        public Follows(TwitchAPI api)
        {
            v3 = new V3(api);
        }

        public V3 v3 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetFollowers
            public async Task<Models.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Direction direction = Direction.Descending)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                if (cursor != null)
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                switch (direction)
                {
                    case Direction.Ascending:
                        getParams.Add(new KeyValuePair<string, string>("direction", "asc"));
                        break;
                    case Direction.Descending:
                        getParams.Add(new KeyValuePair<string, string>("direction", "desc"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                return await Api.GetGenericAsync<Models.v3.Follows.FollowersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/follows", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollows
            public async Task<Models.v3.Follows.FollowsResponse> GetFollowsAsync(string channel, int limit = 25, int offset = 0, Direction direction = Direction.Descending, SortBy sortBy = SortBy.CreatedAt)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                switch (direction)
                {
                    case Direction.Ascending:
                        getParams.Add(new KeyValuePair<string, string>("direction", "asc"));
                        break;
                    case Direction.Descending:
                        getParams.Add(new KeyValuePair<string, string>("direction", "desc"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
                switch (sortBy)
                {
                    case SortBy.CreatedAt:
                        getParams.Add(new KeyValuePair<string, string>("sortby", "created_at"));
                        break;
                    case SortBy.LastBroadcast:
                        getParams.Add(new KeyValuePair<string, string>("sortby", "last_broadcast"));
                        break;
                    case SortBy.Login:
                        getParams.Add(new KeyValuePair<string, string>("sortby", "login"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, null);
                }

                return await Api.GetGenericAsync<Models.v3.Follows.FollowsResponse>($"https://api.twitch.tv/kraken/users/{channel}/follows/channels", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowStatus
            public async Task<Models.v3.Follows.Follows> GetFollowsStatusAsync(string user, string targetChannel)
            {
                return await Api.GetGenericAsync<Models.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region CreateFollow
            public async Task<Models.v3.Follows.Follows> CreateFollowAsync(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Follows_Edit, accessToken);
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("notificaitons", notifications.ToString().ToLower()) };
                return await Api.PutGenericAsync<Models.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region RemoveFollow
            public async Task RemoveFollowAsync(string user, string target, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Follows_Edit, accessToken);
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{target}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
