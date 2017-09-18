using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class Helix
    {
        public static class Users
        {
            public async static Task<Models.API.Helix.Users.GetUsers.GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
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
                return await Requests.GetGenericAsync<Models.API.Helix.Users.GetUsers.GetUsersResponse>($"https://api.twitch.tv/helix/users{getParams}", accessToken, Requests.API.Helix);
            }

            public async static Task<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse> GetUsersFollows(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
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

                return await Requests.GetGenericAsync<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse>($"https://api.twitch.tv/helix/users/follows{getParams}", api: Requests.API.Helix);
            }

            public async static Task PutUsers(string description, string accessToken = null)
            {
                await Requests.PutAsync($"https://api.twitch.tv/helix/users?description={description}", null, accessToken, Requests.API.Helix);
            }
        }

        public static class Streams
        {
            public async static Task<Models.API.Helix.Streams.GetStreams.GetStreamsResponse> GetStreams(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
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

                return await Requests.GetGenericAsync<Models.API.Helix.Streams.GetStreams.GetStreamsResponse>($"https://api.twitch.tv/helix/streams{getParams}", api: Requests.API.Helix);
            }

            public async static Task<Models.API.Helix.StreamsMetadata.GetStreamsMetadataResponse> GetStreamsMetadata(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
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

                return await Requests.GetGenericAsync<Models.API.Helix.StreamsMetadata.GetStreamsMetadataResponse>($"https://api.twitch.tv/helix/streams/metadata{getParams}", api: Requests.API.Helix);
            }
        }
    }
}
