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
            #region GetUserFromUsername
            public async Task<Models.API.v3.Users.User> GetUserFromUsernameAsync(string username)
            {
                return await Api.GetGenericAsync<Models.API.v3.Users.User>($"https://api.twitch.tv/kraken/users/{username}", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetEmotes
            public async Task<Models.API.v3.Users.UserEmotesResponse> GetEmotesAsync(string username, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                return await Api.GetGenericAsync<Models.API.v3.Users.UserEmotesResponse>($"https://api.twitch.tv/kraken/users/{username}/emotes", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetUserFromToken
            public async Task<Models.API.v3.Users.FullUser> GetUserFromTokenAsync(string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                return await Api.GetGenericAsync<Models.API.v3.Users.FullUser>("https://api.twitch.tv/kraken/user", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedStreams
            public async Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreamsAsync(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                switch (type)
                {
                    case Enums.StreamType.All:
                        getParams.Add(new KeyValuePair<string, string>("stream_type", "all"));
                        break;
                    case Enums.StreamType.Live:
                        getParams.Add(new KeyValuePair<string, string>("stream_type", "live"));
                        break;
                    case Enums.StreamType.Playlist:
                        getParams.Add(new KeyValuePair<string, string>("stream_type", "playlist"));
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Users.FollowedStreamsResponse>($"https://api.twitch.tv/kraken/streams/followed", getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedVideos
            public async Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideosAsync(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                switch (broadcastType)
                {
                    case Enums.BroadcastType.All:
                        getParams.Add(new KeyValuePair<string, string>("broadcast_type", "all"));
                        break;
                    case Enums.BroadcastType.Archive:
                        getParams.Add(new KeyValuePair<string, string>("broadcast_type", "archive"));
                        break;
                    case Enums.BroadcastType.Highlight:
                        getParams.Add(new KeyValuePair<string, string>("broadcast_type", "highlight"));
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Users.FollowedVideosResponse>($"https://api.twitch.tv/kraken/videos/followed", getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
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
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("login", string.Join(",", usernames)) };
                return await Api.GetGenericAsync<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users", getParams, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUser
            public async Task<Models.API.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return await Api.GetGenericAsync<Models.API.v5.Users.UserAuthed>("https://api.twitch.tv/kraken/user", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserByID
            public async Task<Models.API.v5.Users.User> GetUserByIDAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.User>($"https://api.twitch.tv/kraken/users/{userId}", null, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserByName
            public async Task<Models.API.v5.Users.Users> GetUserByNameAsync(string username)
            {
                if (string.IsNullOrEmpty(username)) { throw new Exceptions.API.BadParameterException("The username is not valid."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("login", username) };
                return await Api.GetGenericAsync<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users", getParams);
            }
            #endregion
            #region GetUserEmotes
            public async Task<Models.API.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.UserEmotes>($"https://api.twitch.tv/kraken/users/{userId}/emotes", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public async Task<Models.API.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/users/{userId}/subscriptions/{channelId}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserFollows
            public async Task<Models.API.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    getParams.Add(new KeyValuePair<string, string>("direction", direction));
                if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                    getParams.Add(new KeyValuePair<string, string>("sortby", sortby));

                return await Api.GetGenericAsync<Models.API.v5.Users.UserFollows>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels", getParams, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserFollowsByChannel
            public async Task<Models.API.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UserFollowsChannel
            public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                try
                {
                    await Api.GetGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, null, ApiVersion.v5).ConfigureAwait(false);
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
                return await Api.PutGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", optionalRequestBody, null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UnfollowChannel
            public async Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserBlockList
            public async Task<Models.API.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                return await Api.GetGenericAsync<Models.API.v5.Users.UserBlocks>($"https://api.twitch.tv/kraken/users/{userId}/blocks", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region BlockUser
            public async Task<Models.API.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.PutGenericAsync<Models.API.v5.Users.UserBlock>($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", null, null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UnblockUser
            public async Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public async Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                if (string.IsNullOrWhiteSpace(identifier)) { throw new Exceptions.API.BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync("https://api.twitch.tv/kraken/user/vhs", "{\"identifier\": \"" + identifier + "\"}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public async Task<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return await Api.GetGenericAsync<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck>("https://api.twitch.tv/kraken/user/vhs", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService

            public async Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                await Api.DeleteAsync("https://api.twitch.tv/kraken/user/vhs", null, authToken, ApiVersion.v5).ConfigureAwait(false);
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
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (ids != null && ids.Count > 0)
                {
                    foreach (var id in ids)
                        getParams.Add(new KeyValuePair<string, string>("id", id));
                }
                if (logins != null && logins.Count > 0)
                {
                    foreach (var login in logins)
                        getParams.Add(new KeyValuePair<string, string>("login", login));
                }
                return await Api.GetGenericAsync<Models.API.Helix.Users.GetUsers.GetUsersResponse>($"https://api.twitch.tv/helix/users", getParams, accessToken, ApiVersion.Helix).ConfigureAwait(false);
            }

            public async Task<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse> GetUsersFollows(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("first", first.ToString()) };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (before != null)
                    getParams.Add(new KeyValuePair<string, string>("before", before));
                if (fromId != null)
                    getParams.Add(new KeyValuePair<string, string>("from_id", fromId));
                if (toId != null)
                    getParams.Add(new KeyValuePair<string, string>("to_id", toId));

                return await Api.GetGenericAsync<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse>($"https://api.twitch.tv/helix/users/follows{getParams}", api: ApiVersion.Helix).ConfigureAwait(false);
            }

            public async Task PutUsers(string description, string accessToken = null)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("description", description) };
                await Api.PutAsync($"https://api.twitch.tv/helix/users", null, getParams, accessToken, ApiVersion.Helix).ConfigureAwait(false);
            }
        }
    }
}