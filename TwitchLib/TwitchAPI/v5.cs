using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    /// Fully featured Twitch API wrapper (for Twitch v5 endpoints).
    /// </summary>
    public static class v5
    {
        /// <summary>
        /// Twitch API calls relating to Twitch channels.
        /// </summary>
        public static class Channels
        {
            /// <summary>
            /// [SYNC] Fetches the community that a channel is currently in.
            /// </summary>
            /// <param name="channelId">The channel ID to fetch the community of.</param>
            /// <returns>Returns Communnity object.</returns>
            public static Models.API.Community.Community GetChannelCommunity(string channelId) => Task.Run(() => Internal.TwitchApi.GetChannelCommunity(channelId)).Result;

            /// <summary>
            /// [ASYNC] Fetches the community that a channel is currently in.
            /// </summary>
            /// <param name="channelId">The channel ID to fetch the community of.</param>
            /// <returns>Returns Communnity object.</returns>
            public static async Task<Models.API.Community.Community> GetChannelCommunityAsync(string channelId) => await Internal.TwitchApi.GetChannelCommunity(channelId);

            /// <summary>
            /// [SYNC] Attempts to set the community of a channel.
            /// </summary>
            /// <param name="channelId">The channel ID to apply the community to.</param>
            /// <param name="communityId">The community ID to be applied to channel.</param>
            /// <param name="accessToken">If access token is not yet set, you must set it here.</param>
            public static void SetChannelCommunity(string channelId, string communityId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.SetChannelCommunity(channelId, communityId, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to set the community of a channel.
            /// </summary>
            /// <param name="channelId">The channel ID to apply the community to.</param>
            /// <param name="communityId">The community ID to be applied to channel.</param>
            /// <param name="accessToken">If access token is not yet set, you must set it here.</param>
            public static async void SetChannelCommunityAsync(string channelId, string communityId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.SetChannelCommunity(channelId, communityId, accessToken));

            /// <summary>
            /// [SYNC] Attempts to remove a community from a channel.
            /// </summary>
            /// <param name="channelId">The Id of the channel to remove the community from.</param>
            /// <param name="accessToken">If access token is not yet set, you must set it here.</param>
            public static void RemoveChannelCommunity(string channelId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.RemoveChannelCommunity(channelId, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to remove a community from a channel.
            /// </summary>
            /// <param name="channelId">The Id of the channel to remove the community from.</param>
            /// <param name="accessToken">If access token is not yet set, you must set it here.</param>
            public static async void RemoveChannelCommunityAsync(string channelId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.RemoveChannelCommunity(channelId, accessToken));
        }

        /// <summary>
        /// Twitch API calls relating to Twitch users.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// [SYNC] Fetches a list of user objects given their usernames.
            /// </summary>
            /// <param name="usernames">List of strings representing usernames.</param>
            /// <returns>List of user objects of the valid usernames.</returns>
            public static List<Models.API.v5.User> GetUsers(List<string> usernames) => Task.Run(() => Internal.TwitchApi.GetUsersV5(usernames)).Result;

            /// <summary>
            /// [ASYNC] Fetches a list of user objects given their usernames.
            /// </summary>
            /// <param name="usernames">List of strings representing usernames.</param>
            /// <returns>List of user objects of the valid usernames.</returns>
            public static async Task<List<Models.API.v5.User>> GetUsersAsync(List<string> usernames) => await Internal.TwitchApi.GetUsersV5(usernames);
        }

        /// <summary>
        /// Twitch API calls relating to Twitch streams.
        /// </summary>
        public static class Streams
        {
            /// <summary>
            /// [SYNC] Fetches streams based on the given parameters.
            /// </summary>
            /// <param name="game"></param>
            /// <param name="channels"></param>
            /// <param name="streamType"></param>
            /// <param name="language"></param>
            /// <param name="limit"></param>
            /// <param name="offset"></param>
            /// <param name="accessToken"></param>
            /// <returns>List of Stream objects.</returns>
            public static List<Models.API.Stream.Stream> GetAllStreams(string game = null, List<string> channels = null, Enums.StreamType streamType = Enums.StreamType.Live,
                string language = "en", int limit = 25, int offset = 0, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetAllStreamsV5(game, channels, streamType, language, limit, offset, accessToken)).Result;

            /// <summary>
            /// [ASYNC] Fetches streams based on the given parameters.
            /// </summary>
            /// <param name="game"></param>
            /// <param name="channels"></param>
            /// <param name="streamType"></param>
            /// <param name="language"></param>
            /// <param name="limit"></param>
            /// <param name="offset"></param>
            /// <param name="accessToken"></param>
            /// <returns>List of Stream objects.</returns>
            public static async Task<List<Models.API.Stream.Stream>> GetAllStreamsAsync(string game = null, List<string> channels = null, Enums.StreamType streamType = Enums.StreamType.Live,
                string language = "en", int limit = 25, int offset = 0, string accessToken = null) => await Internal.TwitchApi.GetAllStreamsV5(game, channels, streamType, language, limit, offset, accessToken);

            /// <summary>
            /// [SYNC] Retrieves a list of followed streams.
            /// </summary>
            /// <param name="streamType">Stream type can be live, playlist, or all.</param>
            /// <param name="limit">Limit must be larger than 0 and smaller than or equal to 100.</param>
            /// <param name="offset">Offset used for pagination of results.</param>
            /// <param name="accessToken">If accessToken not previously set, you must set it here.</param>
            /// <returns>FollowedStreamsResponse housing total followed streams and a list of (up to 100) followed stream objects.</returns>
            public static Models.API.Stream.FollowedStreamsResponse GetFollowedStreams(Enums.StreamType streamType = Enums.StreamType.Live, int limit = 25, int offset = 0, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetFollowedStreams(streamType, limit, offset, accessToken)).Result;

            /// <summary>
            /// [ASYNC] Retrieves a list of followed streams.
            /// </summary>
            /// <param name="streamType">Stream type can be live, playlist, or all.</param>
            /// <param name="limit">Limit must be larger than 0 and smaller than or equal to 100.</param>
            /// <param name="offset">Offset used for pagination of results.</param>
            /// <param name="accessToken">If accessToken not previously set, you must set it here.</param>
            /// <returns>FollowedStreamsResponse housing total followed streams and a list of (up to 100) followed stream objects.</returns>
            public static async Task<Models.API.Stream.FollowedStreamsResponse> GetFollowedStreamsAsync(Enums.StreamType streamType = Enums.StreamType.Live, int limit = 25, int offset = 0, string accessToken = null) => await Internal.TwitchApi.GetFollowedStreams(streamType, limit, offset, accessToken);
        }

        /// <summary>
        /// Twitch API calls relating to Twitch communities.
        /// </summary>
        public static class Communities
        {
            /// <summary>
            /// [SYNC] Retrieves a community object representing a Twitch community by the name.
            /// </summary>
            /// <param name="communityName">Name of the community to fetch.</param>
            /// <returns>Community object.</returns>
            public static Models.API.Community.Community GetCommunityByName(string communityName) => Task.Run(() => Internal.TwitchApi.GetCommunityByName(communityName)).Result;

            /// <summary>
            /// [ASYNC] Retrieves a community object representing a Twitch community by the name.
            /// </summary>
            /// <param name="communityName">Name of the community to fetch.</param>
            /// <returns>Community object.</returns>
            public static async Task<Models.API.Community.Community> GetCommunityByNameAsync(string communityName) => await Internal.TwitchApi.GetCommunityByName(communityName);

            /// <summary>
            /// [SYNC] Retrieves a community object representing a Twitch community by the id.
            /// </summary>
            /// <param name="communityId">Id of Twitch community to fetch.</param>
            /// <returns>Community object.</returns>
            public static Models.API.Community.Community GetCommunityById(string communityId) => Task.Run(() => Internal.TwitchApi.GetCommunityById(communityId)).Result;

            /// <summary>
            /// [ASYNC] Retrieves a community object representing a Twitch community by the id.
            /// </summary>
            /// <param name="communityId">Id of Twitch community to fetch.</param>
            /// <returns>Community object.</returns>
            public static async Task<Models.API.Community.Community> GetCommunityByIdAsync(string communityId) => await Internal.TwitchApi.GetCommunityById(communityId);

            /// <summary>
            /// [SYNC] Sends request to create a Twitch Community.
            /// </summary>
            /// <param name="name">Name of the Twitch Community. 3-25 characters. No spaces.</param>
            /// <param name="summary">Summary of the Twitch Community. 160 characters max.</param>
            /// <param name="description">Description of the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="rules">Rules for the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="accessToken">If an access token is not set, set this param.</param>
            /// <returns>String ID of the new community.</returns>
            public static string CreateCommunity(string name, string summary, string description, string rules, string accessToken = null) => Task.Run(() => Internal.TwitchApi.CreateCommunity(name, summary, description, rules, accessToken)).Result;

            /// <summary>
            /// [ASYNC] Sends request to create a Twitch Community.
            /// </summary>
            /// <param name="name">Name of the Twitch Community. 3-25 characters. No spaces.</param>
            /// <param name="summary">Summary of the Twitch Community. 160 characters max.</param>
            /// <param name="description">Description of the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="rules">Rules for the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="accessToken">If an access token is not set, set this param.</param>
            /// <returns>String ID of the new community.</returns>
            public static async Task<string> CreateCommunityAsync(string name, string summary, string description, string rules, string accessToken = null) => await Internal.TwitchApi.CreateCommunity(name, summary, description, rules, accessToken);

            /// <summary>
            /// [SYNC] Attempts to update details regarding an existing Twitch community.
            /// </summary>
            /// <param name="communityId">Unique Twitch community identifier.</param>
            /// <param name="summary">Summary of the Twitch Community. 160 characters max.</param>
            /// <param name="description">Description of the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="rules">Rules for the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="email">Contact email for the community.</param>
            /// <param name="accessToken">If an access token is not set, set this param.</param>
            /// <returns>String ID of the new community.</returns>
            public static void UpdateCommunity(string communityId, string summary = null, string description = null, string rules = null, string email = null, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UpdateCommunity(communityId, summary, description, rules, email, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to update details regarding an existing Twitch community.
            /// </summary>
            /// <param name="communityId">Unique Twitch community identifier.</param>
            /// <param name="summary">Summary of the Twitch Community. 160 characters max.</param>
            /// <param name="description">Description of the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="rules">Rules for the Twitch Community. Max of 1,572,864 characters.</param>
            /// <param name="email">Contact email for the community.</param>
            /// <param name="accessToken">If an access token is not set, set this param.</param>
            /// <returns>String ID of the new community.</returns>
            public static async void UpdateCommunityAsync(string communityId, string summary = null, string description = null, string rules = null, string email = null, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.UpdateCommunity(communityId, summary, description, rules, email, accessToken));

            /// <summary>
            /// [SYNC] Fetches the top communities on Twitch currently by viewer count.
            /// </summary>
            /// <param name="limit">Limit the number of results. Maximum possible is 100.</param>
            /// <param name="cursor">Used to tell server where to start fetching results.</param>
            /// <returns>TopCommunitiesResponse housing total, cursor, and list of communities.</returns>
            public static Models.API.Community.TopCommunitiesResponse GetTopCommunities(long? limit = null, string cursor = null) => Task.Run(() => Internal.TwitchApi.GetTopCommunities(limit, cursor)).Result;

            /// <summary>
            /// [ASYNC] Fetches the top communities on Twitch currently by viewer count.
            /// </summary>
            /// <param name="limit">Limit the number of results. Maximum possible is 100.</param>
            /// <param name="cursor">Used to tell server where to start fetching results.</param>
            /// <returns>TopCommunitiesResponse housing total, cursor, and list of communities.</returns>
            public static async Task<Models.API.Community.TopCommunitiesResponse> GetTopCommunitiesAsync(long? limit = null, string cursor = null) => await Internal.TwitchApi.GetTopCommunities(limit, cursor);

            /// <summary>
            /// [SYNC] Fetches the banned users in a specific community.
            /// </summary>
            /// <param name="communityId">Unique ID of the Twitch community.</param>
            /// <param name="limit">Limit the number of results. Maximum possible is 100.</param>
            /// <param name="cursor">Used to tell server where to start fetching results.</param>
            /// <param name="accessToken">If not set, you must set this param.</param>
            /// <returns>CommunityBannedUsersResponse housing cursor and banned users.</returns>
            public static Models.API.Community.CommunityBannedUsersResponse GetCommunityBannedUsers(string communityId, long? limit = null, string cursor = null, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetCommunityBannedUsers(communityId, limit, cursor, accessToken)).Result;

            /// <summary>
            /// [ASYNC] Fetches the banned users in a specific community.
            /// </summary>
            /// <param name="communityId">Unique ID of the Twitch community.</param>
            /// <param name="limit">Limit the number of results. Maximum possible is 100.</param>
            /// <param name="cursor">Used to tell server where to start fetching results.</param>
            /// <param name="accessToken">If not set, you must set this param.</param>
            /// <returns>CommunityBannedUsersResponse housing cursor and banned users.</returns>
            public static async Task<Models.API.Community.CommunityBannedUsersResponse> GetCommunityBannedUsersAsync(string communityId, long? limit = null, string cursor = null, string accessToken = null) => await Internal.TwitchApi.GetCommunityBannedUsers(communityId, limit, cursor, accessToken);

            /// <summary>
            /// [SYNC] Gets all streams currently in a community.
            /// </summary>
            /// <param name="communityId">Unique ID of the Twitch community.</param>
            /// <param name="limit">Limit the number of results. Maximum possible is 100.</param>
            /// <param name="cursor">Used to tell server where to start fetching results.</param>
            /// <returns>StreamsInCommunityResponse houses total streams and list of streams.</returns>
            public static Models.API.Community.StreamsInCommunityResponse GetStreamersInCommunity(string communityId, long? limit = null, string cursor = null) => Task.Run(() => Internal.TwitchApi.GetStreamsInCommunity(communityId, limit, cursor)).Result;

            /// <summary>
            /// [ASYNC] Gets all streams currently in a community.
            /// </summary>
            /// <param name="communityId">Unique ID of the Twitch community.</param>
            /// <param name="limit">Limit the number of results. Maximum possible is 100.</param>
            /// <param name="cursor">Used to tell server where to start fetching results.</param>
            /// <returns>StreamsInCommunityResponse houses total streams and list of streams.</returns>
            public static async Task<Models.API.Community.StreamsInCommunityResponse> GetStreamersInCommunityAsync(string communityId, long? limit = null, string cursor = null) => await Internal.TwitchApi.GetStreamsInCommunity(communityId, limit, cursor);

            /// <summary>
            /// [SYNC] Sends request to ban a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user banned from.</param>
            /// <param name="userId">The ID of the user to be banned.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void BanCommunityMember(string communityId, string userId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.BanCommunityUser(communityId, userId, accessToken));

            /// <summary>
            /// [ASYNC] Sends request to ban a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user banned from.</param>
            /// <param name="userId">The ID of the user to be banned.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void BanCommunityMemberAsync(string communityId, string userId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.BanCommunityUser(communityId, userId, accessToken));

            /// <summary>
            /// [SYNC] Sends request to unban a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user unbanned from.</param>
            /// <param name="userId">The ID of the user to be unbanned.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void UnBanCommunityMember(string communityId, string userId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UnBanCommunityUser(communityId, userId, accessToken));

            /// <summary>
            /// [ASYNC] Sends request to unban a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user unbanned from.</param>
            /// <param name="userId">The ID of the user to be unbanned.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void UnBanCommunityMemberAsync(string communityId, string userId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.UnBanCommunityUser(communityId, userId, accessToken));

            /// <summary>
            /// [SYNC] Sends request to timeout a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user timedout from.</param>
            /// <param name="userId">The ID of the user to be timedout.</param>
            /// <param name="durationInHours">THe number of hours the member will be timed out for.</param>
            /// <param name="reason">Optional parameter to provide a reason why they were timed out.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void TimeoutCommunityMember(string communityId, string userId, int durationInHours, string reason = null, string accessToken = null) => Task.Run(() => Internal.TwitchApi.TimeoutCommunityUser(communityId, userId, durationInHours, reason, accessToken));

            /// <summary>
            /// [ASYNC] Sends request to timeout a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user timedout from.</param>
            /// <param name="userId">The ID of the user to be timedout.</param>
            /// <param name="durationInHours">THe number of hours the member will be timed out for.</param>
            /// <param name="reason">Optional parameter to provide a reason why they were timed out.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void TimeoutCommunityMemberAsync(string communityId, string userId, int durationInHours, string reason = null, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.TimeoutCommunityUser(communityId, userId, durationInHours, reason, accessToken));

            /// <summary>
            /// [SYNC] Sends request to untimeout a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user untimedout from.</param>
            /// <param name="userId">The ID of the user to be untimedout.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void UnTimeoutCommunityMember(string communityId, string userId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UnTimeoutCommunityUser(communityId, userId, accessToken));

            /// <summary>
            /// [ASYNC] Sends request to untimeout a member in a Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community that will have the user untimedout from.</param>
            /// <param name="userId">The ID of the user to be untimedout.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void UnTimeoutCommunityMemberAsync(string communityId, string userId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.UnTimeoutCommunityUser(communityId, userId, accessToken));

            /// <summary>
            /// [SYNC] Fetches the moderators that exist in a specific Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community to fetch moderators from.</param>
            /// <returns>List of CommunityModerator objects.</returns>
            public static List<Models.API.Community.CommunityModerator> GetCommunityModerators(string communityId) => Task.Run(() => Internal.TwitchApi.GetCommunityModerators(communityId)).Result;

            /// <summary>
            /// [ASYNC] Fetches the moderators that exist in a specific Twitch community.
            /// </summary>
            /// <param name="communityId">The ID of the community to fetch moderators from.</param>
            /// <returns>List of CommunityModerator objects.</returns>
            public static async Task<List<Models.API.Community.CommunityModerator>> GetCommunityModeratorsAsync(string communityId) => await Internal.TwitchApi.GetCommunityModerators(communityId);

            /// <summary>
            /// [SYNC] Adds a new moderator to a specific community
            /// </summary>
            /// <param name="communityId">The ID of the community to fetch moderators from.</param>
            /// <param name="userId">The ID of the new moderator.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void AddCommunityModerator(string communityId, string userId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.AddCommunityModerator(communityId, userId, accessToken));

            /// <summary>
            /// [ASYNC] Adds a new moderator to a specific community
            /// </summary>
            /// <param name="communityId">The ID of the community to add the moderator to.</param>
            /// <param name="userId">The ID of the new moderator.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void AddCommunityModeratorAsync(string communityId, string userId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.AddCommunityModerator(communityId, userId, accessToken));

            /// <summary>
            /// [SYNC] Removes an existing moderator from a specific community
            /// </summary>
            /// <param name="communityId">The ID of the community to fetch moderators from.</param>
            /// <param name="userId">The ID of the moderator to remove..</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void RemoveCommunityModerator(string communityId, string userId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.RemoveCommunityModerator(communityId, userId, accessToken));

            /// <summary>
            /// [ASYNC] Removes an existing moderator from a specific community
            /// </summary>
            /// <param name="communityId">The ID of the community to fetch moderators from.</param>
            /// <param name="userId">The ID of the moderator to remove..</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void RemoveCommunityModeratorAsync(string communityId, string userId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.RemoveCommunityModerator(communityId, userId, accessToken));

            /// <summary>
            /// [SYNC] Attempts to create an avatar image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="base64AvatarImage">Base64 encoded image as a string</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void CreateCommunityAvatarImage(string communityId, string base64AvatarImage, string accessToken = null) => Task.Run(() => Internal.TwitchApi.CreateCommunityAvatarImage(communityId, base64AvatarImage, accessToken));

            /// <summary>
            /// [SYNC] Attempts to create an avatar image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="avatarImage">Image object representing the avatar image.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void CreateCommunityAvatarImage(string communityId, Image avatarImage, string accessToken = null) => Task.Run(() => Internal.TwitchApi.CreateCommunityAvatarImage(communityId, avatarImage, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to create an avatar image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="base64AvatarImage">Base64 encoded image as a string</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void CreateCommunityAvatarImageAsync(string communityId, string base64AvatarImage, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.CreateCommunityAvatarImage(communityId, base64AvatarImage, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to create an avatar image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="avatarImage">Image object representing the avatar image.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void CreateCommunityAvatarImageAsync(string communityId, Image avatarImage, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.CreateCommunityAvatarImage(communityId, avatarImage, accessToken));

            /// <summary>
            /// [SYNC] Attempts to remove a community avatar image.
            /// </summary>
            /// <param name="communityId">The ID of the community to remove the avatar image from.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void RemoveCommunityAvatarImage(string communityId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.RemoveCommunityAvatarImage(communityId, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to remove a community avatar image.
            /// </summary>
            /// <param name="communityId">The ID of the community to remove the avatar image from.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void RemoveCommunityAvatarImageAsync(string communityId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.RemoveCommunityAvatarImage(communityId, accessToken));


            /// <summary>
            /// [SYNC] Attempts to create an Cover image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="base64CoverImage">Base64 encoded image as a string</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void CreateCommunityCoverImage(string communityId, string base64CoverImage, string accessToken = null) => Task.Run(() => Internal.TwitchApi.CreateCommunityCoverImage(communityId, base64CoverImage, accessToken));

            /// <summary>
            /// [SYNC] Attempts to create an Cover image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="CoverImage">Image object representing the Cover image.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void CreateCommunityCoverImage(string communityId, Image CoverImage, string accessToken = null) => Task.Run(() => Internal.TwitchApi.CreateCommunityCoverImage(communityId, CoverImage, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to create an Cover image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="base64CoverImage">Base64 encoded image as a string</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void CreateCommunityCoverImageAsync(string communityId, string base64CoverImage, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.CreateCommunityCoverImage(communityId, base64CoverImage, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to create an Cover image for a community.
            /// </summary>
            /// <param name="communityId">The ID of the community to assign the image to.</param>
            /// <param name="CoverImage">Image object representing the Cover image.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void CreateCommunityCoverImageAsync(string communityId, Image CoverImage, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.CreateCommunityCoverImage(communityId, CoverImage, accessToken));

            /// <summary>
            /// [SYNC] Attempts to remove a community Cover image.
            /// </summary>
            /// <param name="communityId">The ID of the community to remove the Cover image from.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static void RemoveCommunityCoverImage(string communityId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.RemoveCommunityCoverImage(communityId, accessToken));

            /// <summary>
            /// [ASYNC] Attempts to remove a community Cover image.
            /// </summary>
            /// <param name="communityId">The ID of the community to remove the Cover image from.</param>
            /// <param name="accessToken">If access token was not previously set, you must set it here.</param>
            public static async void RemoveCommunityCoverImageAsync(string communityId, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.RemoveCommunityCoverImage(communityId, accessToken));
        }
    }
}
