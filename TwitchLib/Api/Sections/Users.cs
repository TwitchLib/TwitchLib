namespace TwitchLib
{
    #region using directives
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Users
    {
        public Users(TwitchAPI api)
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
            #region GetUserFromUsername
            public async Task<Models.API.v3.Users.User> GetUserFromUsernameAsync(string username)
            {
                return await Api.GetGenericAsync<Models.API.v3.Users.User>($"https://api.twitch.tv/kraken/users/{username}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetEmotes
            public async Task<Models.API.v3.Users.UserEmotesResponse> GetEmotesAsync(string username, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                return await Api.GetGenericAsync<Models.API.v3.Users.UserEmotesResponse>($"https://api.twitch.tv/kraken/users/{username}/emotes", accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetUserFromToken
            public async Task<Models.API.v3.Users.FullUser> GetUserFromTokenAsync(string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                return await Api.GetGenericAsync<Models.API.v3.Users.FullUser>("https://api.twitch.tv/kraken/user", accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedStreams
            public async Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreamsAsync(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                string paramsStr = $"?limit={offset}&offset={offset}";
                switch (type)
                {
                    case Enums.StreamType.All:
                        paramsStr += "&stream_type=all";
                        break;
                    case Enums.StreamType.Live:
                        paramsStr += "&stream_type=live";
                        break;
                    case Enums.StreamType.Playlist:
                        paramsStr += "&stream_type=playlist";
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Users.FollowedStreamsResponse>($"https://api.twitch.tv/kraken/streams/followed{paramsStr}", accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedVideos
            public async Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideosAsync(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (broadcastType)
                {
                    case Enums.BroadcastType.All:
                        paramsStr += "&broadcast_type=all";
                        break;
                    case Enums.BroadcastType.Archive:
                        paramsStr += "&broadcast_type=archive";
                        break;
                    case Enums.BroadcastType.Highlight:
                        paramsStr += "&broadcast_type=highlight";
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Users.FollowedVideosResponse>($"https://api.twitch.tv/kraken/videos/followed{paramsStr}", accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetUsersByName
            public async Task<Models.API.v5.Users.Users> GetUsersByNameAsync(List<string> usernames)
            {
                if (usernames == null || usernames.Count == 0) { throw new Exceptions.API.BadParameterException("The username list is not valid. It is not allowed to be null or empty."); }
                string payload = "?login=" + string.Join(",", usernames);
                return await Api.GetGenericAsync<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users{payload}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUser
            public async Task<Models.API.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return await Api.GetGenericAsync<Models.API.v5.Users.UserAuthed>("https://api.twitch.tv/kraken/user", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserByID
            public async Task<Models.API.v5.Users.User> GetUserByIDAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.User>($"https://api.twitch.tv/kraken/users/{userId}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserByName
            public async Task<Models.API.v5.Users.Users> GetUserByNameAsync(string username)
            {
                if (string.IsNullOrEmpty(username)) { throw new Exceptions.API.BadParameterException("The username is not valid."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users?login={username}");
            }
            #endregion
            #region GetUserEmotes
            public async Task<Models.API.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.UserEmotes>($"https://api.twitch.tv/kraken/users/{userId}/emotes", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public async Task<Models.API.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/users/{userId}/subscriptions/{channelId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserFollows
            public async Task<Models.API.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    queryParameters.Add(new KeyValuePair<string, string>("direction", direction));
                if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                    queryParameters.Add(new KeyValuePair<string, string>("sortby", sortby));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return await Api.GetGenericAsync<Models.API.v5.Users.UserFollows>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserFollowsByChannel
            public async Task<Models.API.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UserFollowsChannel
            public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                try
                {
                    await Api.GetGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, ApiVersion.v5).ConfigureAwait(false);
                    return true;
                }
                catch (Exceptions.API.BadResourceException)
                {
                    return false;
                }
            }
            #endregion
            #region FollowChannel
            public async Task<Models.API.v5.Users.UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalRequestBody = (notifications != null) ? "{\"notifications\": " + notifications + "}" : null;
                return await Api.PutGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", optionalRequestBody, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UnfollowChannel
            public async Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserBlockList
            public async Task<Models.API.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
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
                return await Api.GetGenericAsync<Models.API.v5.Users.UserBlocks>($"https://api.twitch.tv/kraken/users/{userId}/blocks{optionalQuery}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region BlockUser
            public async Task<Models.API.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.PutGenericAsync<Models.API.v5.Users.UserBlock>($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UnblockUser
            public async Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public async Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                if (string.IsNullOrWhiteSpace(identifier)) { throw new Exceptions.API.BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync("https://api.twitch.tv/kraken/user/vhs", "{\"identifier\": \"" + identifier + "\"}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public async Task<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return await Api.GetGenericAsync<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck>("https://api.twitch.tv/kraken/user/vhs", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService

            public async Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                await Api.DeleteAsync("https://api.twitch.tv/kraken/user/vhs", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #endregion
        }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }

            public async Task<Models.API.Helix.Users.GetUsers.GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
            {
                string getParams = "";
                if (ids != null && ids.Count > 0)
                {
                    string idParams = string.Join("&login=", ids);
                    getParams = $"?id={idParams}";
                }
                if (logins != null && logins.Count > 0)
                {
                    string loginParams = string.Join("&id=", logins);
                    if (getParams == "")
                        getParams = $"?login={loginParams}";
                    else
                        getParams += $"&login={loginParams}";
                }
                return await Api.GetGenericAsync<Models.API.Helix.Users.GetUsers.GetUsersResponse>($"https://api.twitch.tv/helix/users{getParams}", accessToken, ApiVersion.Helix).ConfigureAwait(false);
            }

            public async Task<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse> GetUsersFollows(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
            {
                string getParams = $"?first={first}";
                if (after != null)
                    getParams += $"&after={after}";
                if (before != null)
                    getParams += $"&before={before}";
                if (fromId != null)
                    getParams += $"&from_id={fromId}";
                if (toId != null)
                    getParams += $"&to_id={toId}";

                return await Api.GetGenericAsync<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse>($"https://api.twitch.tv/helix/users/follows{getParams}", api: ApiVersion.Helix).ConfigureAwait(false);
            }

            public async Task PutUsers(string description, string accessToken = null)
            {
                await Api.PutAsync($"https://api.twitch.tv/helix/users?description={description}", null, accessToken, ApiVersion.Helix).ConfigureAwait(false);
            }
        }
    }
}