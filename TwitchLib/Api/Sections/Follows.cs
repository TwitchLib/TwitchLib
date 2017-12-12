namespace TwitchLib
{
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
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
            public async Task<Models.API.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                if (cursor != null)
                    paramsStr += $"&cursor={cursor}";
                switch (direction)
                {
                    case Enums.Direction.Ascending:
                        paramsStr += $"&direction=asc";
                        break;
                    case Enums.Direction.Descending:
                        paramsStr += $"&direction=desc";
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Follows.FollowersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/follows{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollows
            public async Task<Models.API.v3.Follows.FollowsResponse> GetFollowsAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (direction)
                {
                    case Enums.Direction.Ascending:
                        paramsStr += $"&direction=asc";
                        break;
                    case Enums.Direction.Descending:
                        paramsStr += $"&direction=desc";
                        break;
                }
                switch (sortBy)
                {
                    case Enums.SortBy.CreatedAt:
                        paramsStr += $"&sortby=created_at";
                        break;
                    case Enums.SortBy.LastBroadcast:
                        paramsStr += $"&sortby=last_broadcast";
                        break;
                    case Enums.SortBy.Login:
                        paramsStr += $"&sortby=login";
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Follows.FollowsResponse>($"https://api.twitch.tv/kraken/users/{channel}/follows/channels{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowStatus
            public async Task<Models.API.v3.Follows.Follows> GetFollowsStatusAsync(string user, string targetChannel)
            {
                return await Api.GetGenericAsync<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region CreateFollow
            public async Task<Models.API.v3.Follows.Follows> CreateFollowAsync(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                string paramsStr = $"?notifications={notifications.ToString().ToLower()}";
                return await Api.PutGenericAsync<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}{paramsStr}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region RemoveFollow
            public async Task RemoveFollowAsync(string user, string target, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{target}", accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
