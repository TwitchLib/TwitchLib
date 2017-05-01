using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TwitchLib.Exceptions.API;

namespace TwitchLib
{
    /// <summary>
    /// Fully featured Twitch API wrapper.
    /// </summary>
    public static class TwitchApi
    {
        /// <summary>
        /// Twitch API calls relating to Twitch channels.
        /// </summary>
        public static class Channels
        {
            /// <summary>
            /// [SYNC] Retrieves all active events for a specific channel (by channel id)
            /// </summary>
            /// <param name="channelId">The channel id to fetch channel events from.</param>
            /// <returns>ChannelEventsResponse</returns>
            public static Models.API.Channel.ChannelEventsResponse GetChannelEvents(string channelId) => Task.Run(() => Internal.TwitchApi.GetChannelEvents(channelId)).Result;
            /// <summary>
            /// [ASYNC] Retrieves all active events for a specific channel (by channel id)
            /// </summary>
            /// <param name="channelId">The channel id to fetch channel events from.</param>
            /// <returns>ChannelEventsResponse</returns>
            public static async Task<Models.API.Channel.ChannelEventsResponse> GetChannelEventsAsync(string channelId) => await Internal.TwitchApi.GetChannelEvents(channelId);

            /// <summary>
            /// [SYNC] Retrieves a Channels object regarding a specific channel.
            /// </summary>
            /// <param name="channel">The channel to fetch Channels object about.</param>
            /// <returns>Channels object.</returns>
            public static Models.API.Channel.Channels GetChannelsObject(string channel) => Task.Run(() => Internal.TwitchApi.GetChannelsObject(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a Channels object regarding a specific channel.
            /// </summary>
            /// <param name="channel">The channel to fetch Channels object about.</param>
            /// <returns>Channels object.</returns>
            public static async Task<Models.API.Channel.Channels> GetChannelsObjectAsync(string channel) => await Internal.TwitchApi.GetChannelsObject(channel);

            /// <summary>
            /// [SYNC] Retrieves a channel's list of available chat badges.
            /// </summary>
            /// <param name="channel">The channel to fetch available badges from.</param>
            /// <returns>BadgeResponse object containing list of available badges.</returns>
            public static Models.API.Badge.BadgeResponse GetChannelBadges(string channel) => Task.Run(() => Internal.TwitchApi.GetChannelBadges(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a channel's list of available chat badges.
            /// </summary>
            /// <param name="channel">The channel to fetch available badges from.</param>
            /// <returns>BadgeResponse object containing list of available badges.</returns>
            public static async Task<Models.API.Badge.BadgeResponse> GetChannelBadgesAsync(string channel) => await Internal.TwitchApi.GetChannelBadges(channel);

            /// <summary>
            /// [SYNC] Retrieves a string list of channel editor users.
            /// <para>Authenticated, required scope: <code>channel_read</code></para>
            /// </summary>
            /// <param name="channel">The channel to fetch editors from.</param>
            /// <param name="accessToken">An access token with the required scope.</param>
            /// <returns>A list of User objects that are channel editors.</returns>
            public static List<Models.API.User.User> GetChannelEditors(string channel, string accessToken) => Task.Run(() => Internal.TwitchApi.GetChannelEditors(channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a string list of channel editor users.
            /// <para>Authenticated, required scope: <code>channel_read</code></para>
            /// </summary>
            /// <param name="channel">The channel to fetch editors from.</param>
            /// <param name="accessToken">An access token with the required scope.</param>
            /// <returns>A list of User objects that are channel editors.</returns>
            public static async Task<List<Models.API.User.User>> GetChannelEditorsAsync(string channel, string accessToken) => await Internal.TwitchApi.GetChannelEditors(channel, accessToken);

            /// <summary>
            /// [SYNC] Retrieves a string list of channels hosting a specified channel.
            /// <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed. Additionally, this makes 2 API calls so limited use is recommended.</para>
            /// </summary>
            /// <param name="channel">The name of the channel to search for.</param>
            /// <returns>A list of all channels that are currently hosting the specified channel.</returns>
            public static List<string> GetChannelHosts(string channel) => Task.Run(() => Internal.TwitchApi.GetChannelHosts(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a string list of channels hosting a specified channel.
            /// <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed. Additionally, this makes 2 API calls so limited use is recommended.</para>
            /// </summary>
            /// <param name="channel">The name of the channel to search for.</param>
            /// <returns>A list of all channels that are currently hosting the specified channel.</returns>
            public static async Task<List<string>> GetChannelHostsAsync(string channel) => await Internal.TwitchApi.GetChannelHosts(channel);

            /// <summary>
            /// [SYNC] Retrieves a TwitchStream object containing API data related to a stream.
            /// </summary>
            /// <param name="channel">The name of the channel to search for.</param>
            /// <returns>A TwitchStream object containing API data related to a stream.</returns>
            public static Models.API.Channel.Channel GetChannel(string channel) => Task.Run(() => Internal.TwitchApi.GetChannel(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a TwitchStream object containing API data related to a stream.
            /// </summary>
            /// <param name="channel">The name of the channel to search for.</param>
            /// <returns>A TwitchStream object containing API data related to a stream.</returns>
            public static async Task<Models.API.Channel.Channel> GetChannelAsync(string channel) => await Internal.TwitchApi.GetChannel(channel);

            /// <summary>
            /// [SYNC] Execute a search query on Twitch to find a list of channels.
            /// </summary>
            /// <param name="query">A url-encoded search query.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
            /// <param name="offset">Object offset for pagination. Default is 0.</param>
            /// <returns>A list of Channel objects matching the query.</returns>
            public static List<Models.API.Channel.Channel> SearchChannels(string query, int limit = 25, int offset = 0) => Task.Run(() => Internal.TwitchApi.SearchChannels(query, limit, offset)).Result;
            /// <summary>
            /// [ASYNC] Execute a search query on Twitch to find a list of channels.
            /// </summary>
            /// <param name="query">A url-encoded search query.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
            /// <param name="offset">Object offset for pagination. Default is 0.</param>
            /// <returns>A list of Channel objects matching the query.</returns>
            public static async Task<List<Models.API.Channel.Channel>> SearchChannelsAsync(string query, int limit = 25, int offset = 0) => await Internal.TwitchApi.SearchChannels(query, limit, offset);

            /// <summary>
            /// [SYNC] Retrieves channel feed posts.
            /// </summary>
            /// <param name="channel">Channel to fetch feed posts from.</param>
            /// <param name="limit">Applied limit (default 10, max 100)</param>
            /// <param name="cursor">Used for pagination.</param>
            /// <returns></returns>
            public static Models.API.Feed.FeedResponse GetChannelFeed(string channel, int limit = 10, string cursor = null) => Task.Run(() => Internal.TwitchApi.GetChannelFeed(channel, limit, cursor)).Result;
            /// <summary>
            /// [ASYNC] Retrieves channel feed posts.
            /// </summary>
            /// <param name="channel">Channel to fetch feed posts from.</param>
            /// <param name="limit">Applied limit (default 10, max 100)</param>
            /// <param name="cursor">Used for pagination.</param>
            /// <returns></returns>
            public static async Task<Models.API.Feed.FeedResponse> GetChannelFeedAsync(string channel, int limit = 10, string cursor = null) => await Internal.TwitchApi.GetChannelFeed(channel, limit, cursor);

            /// <summary>
            /// [SYNC] Posts to a Twitch channel's feed.
            /// </summary>
            /// <param name="content">The content of the message being posted.</param>
            /// <param name="accessToken">OAuth access token with channel_feed_edit scope, not needed if already set.</param>
            /// <param name="channel">Channel to post feed post to.</param>
            /// <param name="share">If set to true, and enabled on account, will tweet out post.</param>
            /// <returns>Returns object with Post object and URL to tweet if available.</returns>
            public static Models.API.Feed.PostToChannelFeedResponse PostToChannelFeed(string content, bool share, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.PostToChannelFeed(content, share, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Posts to a Twitch channel's feed.
            /// </summary>
            /// <param name="content">The content of the message being posted.</param>
            /// <param name="accessToken">OAuth access token with channel_feed_edit scope, not needed if already set.</param>
            /// <param name="channel">Channel to post feed post to.</param>
            /// <param name="share">If set to true, and enabled on account, will tweet out post.</param>
            /// <returns>Returns object with Post object and URL to tweet if available.</returns>
            public static async Task<Models.API.Feed.PostToChannelFeedResponse> PostToChannelFeedAsync(string content, bool share, string channel, string accessToken = null) => await Internal.TwitchApi.PostToChannelFeed(content, share, channel, accessToken);

            /// <summary>
            /// [SYNC] Deletes a post on a Twitch channel's feed.
            /// </summary>
            /// <param name="postId">Integer Id of feed post to delete.</param>
            /// <param name="channel">Channel where the post resides.</param>
            /// <param name="accessToken">OAuth access token with channel_feed_edit scope.</param>
            public static void DeleteChannelFeedPost(string postId, string channel, string accessToken = null) => Internal.TwitchApi.DeleteChannelFeedPost(postId, channel, accessToken);
            /// <summary>
            /// [ASYNC] Deletes a post on a Twitch channel's feed.
            /// </summary>
            /// <param name="postId">Integer Id of feed post to delete.</param>
            /// <param name="channel">Channel where the post resides.</param>
            /// <param name="accessToken">OAuth access token with channel_feed_edit scope.</param>
            public static async void DeleteChannelFeedPostAsync(string postId, string channel, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.DeleteChannelFeedPost(postId, channel, accessToken));

            /// <summary>
            /// [SYNC] Fetches Twitch channel name from a steam Id, if their Steam is connected to their Twitch.
            /// </summary>
            /// <param name="steamId">The steam id of the user whose Twitch channel is requested.</param>
            /// <returns>Returns channel name if available, or null.</returns>
            public static string GetChannelFromSteamId(string steamId) => Task.Run(() => Internal.TwitchApi.GetChannelFromSteamId(steamId)).Result;
            /// <summary>
            /// [ASYNC] Fetches Twitch channel name from a steam Id, if their Steam is connected to their Twitch.
            /// </summary>
            /// <param name="steamId">The steam id of the user whose Twitch channel is requested.</param>
            /// <returns>Returns channel name if available, or null.</returns>
            public static async Task<string> GetChannelFromSteamIdAsync(string steamId) => await Internal.TwitchApi.GetChannelFromSteamId(steamId);

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
        /// Twitch API calls relating to Twitch teams.
        /// </summary>
        public static class Teams
        {
            /// <summary>
            /// [SYNC] Retrieves a TwitchTeamMember list of all members in a Twitch team.
            /// <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed.</para>
            /// </summary>
            /// <param name="teamName">The name of the Twitch team to search for.</param>
            /// <returns>A TwitchTeamMember list of all members in a Twitch team.</returns>
            public static List<Models.API.Team.TeamMember> GetTeamMembers(string teamName) => Task.Run(() => Internal.TwitchApi.GetTeamMembers(teamName)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a TwitchTeamMember list of all members in a Twitch team.
            /// <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed.</para>
            /// </summary>
            /// <param name="teamName">The name of the Twitch team to search for.</param>
            /// <returns>A TwitchTeamMember list of all members in a Twitch team.</returns>
            public static async Task<List<Models.API.Team.TeamMember>> GetTeamMembersAsync(string teamName) => await Internal.TwitchApi.GetTeamMembers(teamName);
        }

        /// <summary>
        /// Twitch API calls relating to Twitch users.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// [SYNC] Retrieves a User object from Twitch Api and returns User object.
            /// </summary>
            /// <param name="username">Name of the user you wish to fetch from Twitch.</param>
            /// <returns>User object containing details about the searched for user. Returns null if invalid user/error.</returns>
            public static Models.API.User.User GetUser(string username) => Task.Run(() => Internal.TwitchApi.GetUser(username)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a User object from Twitch Api and returns User object.
            /// </summary>
            /// <param name="username">Name of the user you wish to fetch from Twitch.</param>
            /// <returns>User object containing details about the searched for user. Returns null if invalid user/error.</returns>
            public static async Task<Models.API.User.User> GetUserAsync(string username) => await Internal.TwitchApi.GetUser(username);

            /// <summary>
            /// [SYNC] Fetches a list of user objects given their usernames.
            /// </summary>
            /// <param name="usernames">List of strings representing usernames.</param>
            /// <returns>List of user objects of the valid usernames.</returns>
            public static List<Models.API.v5.User> GetUsersV5(List<string> usernames) => Task.Run(() => Internal.TwitchApi.GetUsersV5(usernames)).Result;

            /// <summary>
            /// [ASYNC] Fetches a list of user objects given their usernames.
            /// </summary>
            /// <param name="usernames">List of strings representing usernames.</param>
            /// <returns>List of user objects of the valid usernames.</returns>
            public static async Task<List<Models.API.v5.User>> GetUsersV5Async(List<string> usernames) => await Internal.TwitchApi.GetUsersV5(usernames);

            public static Models.API.v5.User GetUserV5ById(string userid) => Task.Run(() => Internal.TwitchApi.GetUserV5ById(userid)).Result;

            public static async Task<Models.API.v5.User> GetUserV5ByIdAsync(string userid) => await Internal.TwitchApi.GetUserV5ById(userid);
        }

        /// <summary>
        /// Twitch API calls relating to Twitch streams.
        /// </summary>
        public static class Streams
        {
            /// <summary>
            /// [SYNC] Retrieves the current uptime of a stream, if it is online.
            /// </summary>
            /// <param name="channel">The channel to retrieve the uptime for.</param>
            /// <returns>A TimeSpan object representing time between creation_at of stream, and now.</returns>
            public static TimeSpan GetUptime(string channel) => Task.Run(() => Internal.TwitchApi.GetUptime(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves the current uptime of a stream, if it is online.
            /// </summary>
            /// <param name="channel">The channel to retrieve the uptime for.</param>
            /// <returns>A TimeSpan object representing time between creation_at of stream, and now.</returns>
            public static async Task<TimeSpan> GetUptimeAsync(string channel) => await Internal.TwitchApi.GetUptime(channel);

            /// <summary>
            /// [SYNC] Retrieves a collection of API data from a stream.
            /// </summary>
            /// <param name="channel">The channel to retrieve the data for.</param>
            /// <exception cref="StreamOfflineException">Throws StreamOfflineException if stream is offline.</exception>
            /// <exception cref="BadResourceException">Throws BadResourceException if the passed channel is invalid.</exception>
            /// <returns>A TwitchStream object containing API data related to a stream.</returns>
            public static Models.API.Stream.Stream GetStream(string channel) => Task.Run(() => Internal.TwitchApi.GetStream(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a collection of API data from a stream.
            /// </summary>
            /// <param name="channel">The channel to retrieve the data for.</param>
            /// <exception cref="StreamOfflineException">Throws StreamOfflineException if stream is offline.</exception>
            /// <exception cref="BadResourceException">Throws BadResourceException if the passed channel is invalid.</exception>
            /// <returns>A TwitchStream object containing API data related to a stream.</returns>
            public static async Task<Models.API.Stream.Stream> GetStreamAsync(string channel) => await Internal.TwitchApi.GetStream(channel);

            /// <summary>
            /// [SYNC] Retrieves a collection of API data from multiple streams
            /// </summary>
            /// <param name="channels">List of channels.</param>
            /// <returns>A list of stream objects for each stream.</returns>
            public static List<Models.API.Stream.Stream> GetStreams(List<string> channels) => Task.Run(() => Internal.TwitchApi.GetStreams(channels)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a collection of API data from multiple streams
            /// </summary>
            /// <param name="channels">List of channels.</param>
            /// <returns>A list of stream objects for each stream.</returns>
            public static async Task<List<Models.API.Stream.Stream>> GetStreamsAsync(List<string> channels) => await Internal.TwitchApi.GetStreams(channels);

            /// <summary>
            /// [SYNC] Retrieves all featured streams.
            /// </summary>
            /// <returns>A list of featured stream objects for each featured stream.</returns>
            public static List<Models.API.Stream.FeaturedStream> GetFeaturedStreams(int limit = 25, int offset = 0) => Task.Run(() => Internal.TwitchApi.GetFeaturedStreams(limit, offset)).Result;
            /// <summary>
            /// [ASYNC] Retrieves all featured streams.
            /// </summary>
            /// <returns>A list of featured stream objects for each featured stream.</returns>
            public static async Task<List<Models.API.Stream.FeaturedStream>> GetFeaturedStreamsAsync(int limit = 25, int offset = 0) => await Internal.TwitchApi.GetFeaturedStreams(limit, offset);

            /// <summary>
            /// [ASYNC] Update the <paramref name="status"/> of a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_editor</code></para>
            /// </summary>
            /// <param name="status">Channel's title.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static Models.API.Channel.Channel UpdateStreamTitle(string status, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UpdateStreamTitle(status, channel, accessToken)).Result;
            /// <summary>
            /// [SYNC] Update the <paramref name="status"/> of a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_editor</code></para>
            /// </summary>
            /// <param name="status">Channel's title.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static async Task<Models.API.Channel.Channel> UpdateStreamTitleAsync(string status, string channel, string accessToken = null) => await Internal.TwitchApi.UpdateStreamTitle(status, channel, accessToken);

            /// <summary>
            /// [SYNC] Update the <paramref name="game"/> the <paramref name="channel"/> is currently playing.
            /// <para>Authenticated, required scope: <code>channel_editor</code></para>
            /// </summary>
            /// <param name="game">Game category to be classified as.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static Models.API.Channel.Channel UpdateStreamGame(string game, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UpdateStreamGame(game, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Update the <paramref name="game"/> the <paramref name="channel"/> is currently playing.
            /// <para>Authenticated, required scope: <code>channel_editor</code></para>
            /// </summary>
            /// <param name="game">Game category to be classified as.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static async Task<Models.API.Channel.Channel> UpdateStreamGameAsync(string game, string channel, string accessToken = null) => await Internal.TwitchApi.UpdateStreamGame(game, channel, accessToken);

            /// <summary>
            /// [SYNC] Update the <paramref name="status"/> and <paramref name="game"/> of a <paramref name="channel"/>.
            /// </summary>
            /// <param name="status">Channel's title.</param>
            /// <param name="game">Game category to be classified as.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static Models.API.Channel.Channel UpdateStreamTitleAndGame(string status, string game, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UpdateStreamTitleAndGame(status, game, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Update the <paramref name="status"/> and <paramref name="game"/> of a <paramref name="channel"/>.
            /// </summary>
            /// <param name="status">Channel's title.</param>
            /// <param name="game">Game category to be classified as.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static async Task<Models.API.Channel.Channel> UpdateStreamTitleAndGameAsync(string status, string game, string channel, string accessToken = null) => await Internal.TwitchApi.UpdateStreamTitleAndGame(status, game, channel, accessToken);

            /// <summary>
            /// [SYNC] Execute a search query on Twitch to find a list of streams.
            /// </summary>
            /// <param name="query">A url-encoded search query.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
            /// <param name="offset">Object offset for pagination. Default is 0.</param>
            /// <param name="hls">If set to true, only returns streams using HLS, if set to false only returns non-HLS streams. Default is null.</param>
            /// <returns>A list of Stream objects matching the query.</returns>
            public static List<Models.API.Stream.Stream> SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null) => Task.Run(() => Internal.TwitchApi.SearchStreams(query, limit, offset, hls)).Result;
            /// <summary>
            /// [ASYNC] Execute a search query on Twitch to find a list of streams.
            /// </summary>
            /// <param name="query">A url-encoded search query.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
            /// <param name="offset">Object offset for pagination. Default is 0.</param>
            /// <param name="hls">If set to true, only returns streams using HLS, if set to false only returns non-HLS streams. Default is null.</param>
            /// <returns>A list of Stream objects matching the query.</returns>
            public static async Task<List<Models.API.Stream.Stream>> SearchStreamsAsync(string query, int limit = 25, int offset = 0, bool? hls = null) => await Internal.TwitchApi.SearchStreams(query, limit, offset, hls);

            /// <summary>
            /// [SYNC] Resets the stream key of the <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_stream</code></para>
            /// </summary>
            /// <param name="channel">The channel to reset the stream key for.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static string ResetStreamKey(string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.ResetStreamKey(channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Resets the stream key of the <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_stream</code></para>
            /// </summary>
            /// <param name="channel">The channel to reset the stream key for.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static async Task<string> ResetStreamKeyAsync(string channel, string accessToken) => await Internal.TwitchApi.ResetStreamKey(channel, accessToken);

            /// <summary>
            /// [SYNC] Updates the <paramref name="delay"/> of a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_editor</code></para>
            /// </summary>
            /// <param name="delay">Channel delay in seconds.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">The channel owner's access token and the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static string UpdateStreamDelay(int delay, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.UpdateStreamDelay(delay, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Updates the <paramref name="delay"/> of a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_editor</code></para>
            /// </summary>
            /// <param name="delay">Channel delay in seconds.</param>
            /// <param name="channel">The channel to update.</param>
            /// <param name="accessToken">The channel owner's access token and the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static async Task<string> UpdateStreamDelayAsync(int delay, string channel, string accessToken = null) => await Internal.TwitchApi.UpdateStreamDelay(delay, channel, accessToken);

            /// <summary>
            /// [SYNC] Start a commercial on <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_commercial</code></para>
            /// </summary>
            /// <param name="length">Length of commercial break in seconds. Default value is 30. You can only trigger a commercial once every 8 minutes.</param>
            /// <param name="channel">The channel to start a commercial on.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static string RunCommercial(Enums.CommercialLength length, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.RunCommercial(length, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Start a commercial on <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_commercial</code></para>
            /// </summary>
            /// <param name="length">Length of commercial break in seconds. Default value is 30. You can only trigger a commercial once every 8 minutes.</param>
            /// <param name="channel">The channel to start a commercial on.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>The response of the request.</returns>
            public static async Task<string> RunCommercialAsync(Enums.CommercialLength length, string channel, string accessToken = null) => await Internal.TwitchApi.RunCommercial(length, channel, accessToken);

            /// <summary>
            /// [SYNC] Retrieves a list of all people currently chatting in a channel's chat.
            /// </summary>
            /// <param name="channel">The channel to retrieve the chatting people for.</param>
            /// <returns>A list of Chatter objects detailing each chatter in a channel.</returns>
            public static List<Models.API.Chat.Chatter> GetChatters(string channel) => Task.Run(() => Internal.TwitchApi.GetChatters(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a list of all people currently chatting in a channel's chat.
            /// </summary>
            /// <param name="channel">The channel to retrieve the chatting people for.</param>
            /// <returns>A list of Chatter objects detailing each chatter in a channel.</returns>
            public static async Task<List<Models.API.Chat.Chatter>> GetChattersAsync(string channel) => await Internal.TwitchApi.GetChatters(channel);

            /// <summary>
            /// [SYNC, DEPRECATED] Checks if a stream is live or not.
            /// </summary>
            /// <param name="channel">The channel to retrieve live status for.</param>
            /// <returns>Boolean representing if a stream is live or not.</returns>
            public static bool StreamIsLive(string channel) => Task.Run(() => Internal.TwitchApi.StreamIsLive(channel)).Result;
            /// <summary>
            /// [ASYNC, DEPRECATED] Checks if a stream is live or not.
            /// </summary>
            /// <param name="channel">The channel to retrieve live status for.</param>
            /// <returns>Boolean representing if a stream is live or not.</returns>
            public static async Task<bool> StreamIsLiveAsync(string channel) => await Internal.TwitchApi.StreamIsLive(channel);

            /// <summary>
            /// [SYNC] Retrieves the current status of the broadcaster.
            /// </summary>
            /// <param name="channel">The name of the broadcaster to check.</param>
            /// <returns>True if the broadcaster is online, false otherwise.</returns>
            public static bool BroadcasterOnline(string channel) => Task.Run(() => Internal.TwitchApi.BroadcasterOnline(channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves the current status of the broadcaster.
            /// </summary>
            /// <param name="channel">The name of the broadcaster to check.</param>
            /// <returns>True if the broadcaster is online, false otherwise.</returns>
            public static async Task<bool> BroadcasterOnlineAsync(string channel) => await Internal.TwitchApi.BroadcasterOnline(channel);

            /// <summary>
            /// [SYNC] Retrieves the site wide streams summary (total viewers, total streams) on Twitch.
            /// </summary>
            /// <returns>StreamsSummary object housing total viewers, total streams.</returns>
            public static Models.API.Stream.StreamsSummary GetStreamsSummary() => Task.Run(() => Internal.TwitchApi.GetStreamsSummary()).Result;
            /// <summary>
            /// [ASYNC] Retrieves the site wide streams summary (total viewers, total streams) on Twitch.
            /// </summary>
            /// <returns>StreamsSummary object housing total viewers, total streams.</returns>
            public static async Task<Models.API.Stream.StreamsSummary> GetStreamsSummaryAsync() => await Internal.TwitchApi.GetStreamsSummary();

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
        /// Twitch API calls relating to Twitch games.
        /// </summary>
        public static class Games
        {
            /// <summary>
            /// [SYNC] Execute a query to return the games with the most current viewers.
            /// </summary>
            /// <param name="limit">The number of listings to return, default to 10.</param>
            /// <param name="offset">The number of listings to offset the returned listings, default to 0.</param>
            /// <returns>A list of Game objects matching the query.</returns>
            public static List<Models.API.Game.GameByPopularityListing> GetGamesByPopularity(int limit = 10, int offset = 0) => Task.Run(() => Internal.TwitchApi.GetGamesByPopularity(limit, offset)).Result;
            /// <summary>
            /// [ASYNC] Execute a query to return the games with the most current viewers.
            /// </summary>
            /// <param name="limit">The number of listings to return, default to 10.</param>
            /// <param name="offset">The number of listings to offset the returned listings, default to 0.</param>
            /// <returns>A list of Game objects matching the query.</returns>
            public static async Task<List<Models.API.Game.GameByPopularityListing>> GetGamesByPopularityAsync(int limit = 10, int offset = 0) => await Internal.TwitchApi.GetGamesByPopularity(limit, offset);

            /// <summary>
            /// [SYNC] Execute a search query on Twitch to find a list of games.
            /// </summary>
            /// <param name="query">A url-encoded search query.</param>
            /// <param name="live">If set to true, only games with active streams will be found.</param>
            /// <returns>A list of Game objects matching the query.</returns>
            public static List<Models.API.Game.Game> SearchGames(string query, bool live = false) => Task.Run(() => Internal.TwitchApi.SearchGames(query, live)).Result;
            /// <summary>
            /// [ASYNC] Execute a search query on Twitch to find a list of games.
            /// </summary>
            /// <param name="query">A url-encoded search query.</param>
            /// <param name="live">If set to true, only games with active streams will be found.</param>
            /// <returns>A list of Game objects matching the query.</returns>
            public static async Task<List<Models.API.Game.Game>> SearchGamesAsync(string query, bool live = false) => await Internal.TwitchApi.SearchGames(query, live);
        }

        /// <summary>
        /// Twitch API calls relating Twitch's blocking system.
        /// </summary>
        public static class Blocks
        {
            /// <summary>
            /// [SYNC] Retrieves a list of blocked users a specific user has.
            /// <para>Authenticated, required scope: <code>user_blocks_read</code></para>
            /// </summary>
            /// <param name="username">Username of user to fetch blocked list of.</param>
            /// <param name="accessToken">This call requires an access token.</param>
            /// <param name="limit">Limit output from Twitch Api. Default 25, max 100.</param>
            /// <param name="offset">Offset out from Twitch Api. Default 0.</param>
            /// <returns>List of Block objects.</returns>
            public static List<Models.API.Block.Block> GetBlockedList(string username, int limit = 25, int offset = 0, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetBlockedList(username, accessToken, limit, offset)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a list of blocked users a specific user has.
            /// <para>Authenticated, required scope: <code>user_blocks_read</code></para>
            /// </summary>
            /// <param name="username">Username of user to fetch blocked list of.</param>
            /// <param name="accessToken">This call requires an access token.</param>
            /// <param name="limit">Limit output from Twitch Api. Default 25, max 100.</param>
            /// <param name="offset">Offset out from Twitch Api. Default 0.</param>
            /// <returns>List of Block objects.</returns>
            public static async Task<List<Models.API.Block.Block>> GetBlockedListAsync(string username, int limit = 25, int offset = 0, string accessToken = null) => await Internal.TwitchApi.GetBlockedList(username, accessToken, limit, offset);

            /// <summary>
            /// [SYNC] Blocks a user.
            /// <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
            /// </summary>
            /// <param name="username">User who's blocked list to add to.</param>
            /// <param name="blockedUsername">User to block.</param>
            /// <param name="accessToken">This call requires an access token.</param>
            /// <returns>Block object.</returns>
            public static Models.API.Block.Block BlockUser(string username, string blockedUsername, string accessToken = null) => Task.Run(() => Internal.TwitchApi.BlockUser(username, blockedUsername, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Blocks a user.
            /// <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
            /// </summary>
            /// <param name="username">User who's blocked list to add to.</param>
            /// <param name="blockedUsername">User to block.</param>
            /// <param name="accessToken">This call requires an access token.</param>
            /// <returns>Block object.</returns>
            public static async Task<Models.API.Block.Block> BlockUserAsync(string username, string blockedUsername, string accessToken = null) => await Internal.TwitchApi.BlockUser(username, blockedUsername, accessToken);

            /// <summary>
            /// [SYNC] Unblocks a user.
            /// <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
            /// </summary>
            /// <param name="username">User who's blocked list to unblock from.</param>
            /// <param name="blockedUsername">User to unblock.</param>
            /// <param name="accessToken">This call requires an access token.</param>
            public static void UnblockUser(string username, string blockedUsername, string accessToken = null) => Internal.TwitchApi.UnblockUser(username, blockedUsername, accessToken);
            /// <summary>
            /// [ASYNC] Unblocks a user.
            /// <para>Authenticated, required scope: <code>user_blocks_edit</code></para>
            /// </summary>
            /// <param name="username">User who's blocked list to unblock from.</param>
            /// <param name="blockedUsername">User to unblock.</param>
            /// <param name="accessToken">This call requires an access token.</param>
            public static async void UnblockUserAsync(string username, string blockedUsername, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.UnblockUser(username, blockedUsername, accessToken));
        }

        /// <summary>
        /// Twitch API calls relating to Twitch's follow system.
        /// </summary>
        public static class Follows
        {
            /// <summary>
            /// [SYNC] Retrieves whether a specified user is following the specified user.
            /// </summary>
            /// <param name="username">The user to check the follow status of.</param>
            /// <param name="channel">The channel to check against.</param>
            /// <returns>Returns Follow object representing follow relationship.</returns>
            public static Models.API.Follow.Follow UserFollowsChannel(string username, string channel) => Task.Run(() => Internal.TwitchApi.UserFollowsChannel(username, channel)).Result;
            /// <summary>
            /// [ASYNC] Retrieves whether a specified user is following the specified user.
            /// </summary>
            /// <param name="username">The user to check the follow status of.</param>
            /// <param name="channel">The channel to check against.</param>
            /// <returns>Returns Follow object representing follow relationship.</returns>
            public static async Task<Models.API.Follow.Follow> UserFollowsChannelAsync(string username, string channel) => await Internal.TwitchApi.UserFollowsChannel(username, channel);

            /// <summary>
            /// [SYNC] Retrieves an ascending or descending list of followers from a specific channel.
            /// </summary>
            /// <param name="channel">The channel to retrieve the followers from.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
            /// <param name="cursor">Twitch uses cursoring to paginate long lists of followers. Check <code>_cursor</code> in response body and set <code>cursor</code> to this value to get the next page of results, or use <code>_links.next</code> to navigate to the next page of results.</param>
            /// <param name="direction">Creation date sorting direction.</param>
            /// <returns>A list of TwitchFollower objects.</returns>
            public static Models.API.Follow.FollowersResponse GetFollowers(string channel, int limit = 25, string cursor = null, Enums.SortDirection direction = Enums.SortDirection.Descending) => Task.Run(() => Internal.TwitchApi.GetTwitchFollowers(channel, limit, cursor, direction)).Result;
            /// <summary>
            /// [ASYNC] Retrieves an ascending or descending list of followers from a specific channel.
            /// </summary>
            /// <param name="channel">The channel to retrieve the followers from.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 25. Maximum is 100.</param>
            /// <param name="cursor">Twitch uses cursoring to paginate long lists of followers. Check <code>_cursor</code> in response body and set <code>cursor</code> to this value to get the next page of results, or use <code>_links.next</code> to navigate to the next page of results.</param>
            /// <param name="direction">Creation date sorting direction.</param>
            /// <returns>A list of TwitchFollower objects.</returns>
            public static async Task<Models.API.Follow.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, string cursor = null, Enums.SortDirection direction = Enums.SortDirection.Descending) => await Internal.TwitchApi.GetTwitchFollowers(channel, limit, cursor, direction);

            /// <summary>
            /// [SYNC] Retrieves a list of followed users a specific user has.
            /// </summary>
            /// <param name="channel">Channel to fetch followed users</param>
            /// <param name="limit">Default is 25, max is 100, min is 0</param>
            /// <param name="offset">Integer representing list offset</param>
            /// <param name="sortKey">Enum representing sort order.</param>
            /// <returns>FollowedUsersResponse object.</returns>
            public static Models.API.Follow.FollowedUsersResponse GetFollowedUsers(string channel, int limit = 25, int offset = 0, Enums.SortKey sortKey = Enums.SortKey.CreatedAt) => Task.Run(() => Internal.TwitchApi.GetFollowedUsers(channel, limit, offset, sortKey)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a list of followed users a specific user has.
            /// </summary>
            /// <param name="channel">Channel to fetch followed users</param>
            /// <param name="limit">Default is 25, max is 100, min is 0</param>
            /// <param name="offset">Integer representing list offset</param>
            /// <param name="sortKey">Enum representing sort order.</param>
            /// <returns>FollowedUsersResponse object.</returns>
            public static async Task<Models.API.Follow.FollowedUsersResponse> GetFollowedUsersAsync(string channel, int limit = 25, int offset = 0, Enums.SortKey sortKey = Enums.SortKey.CreatedAt) => await Internal.TwitchApi.GetFollowedUsers(channel, limit, offset, sortKey);

            /// <summary>
            /// [SYNC] Follows a channel given by <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>user_follows_edit</code></para>
            /// </summary>
            /// <param name="username">The username of the user trying to follow the given channel.</param>
            /// <param name="channel">The channel to follow.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>A follow object representing the follow action.</returns>
            public static Models.API.Follow.Follow FollowChannel(string username, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.FollowChannel(username, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Follows a channel given by <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>user_follows_edit</code></para>
            /// </summary>
            /// <param name="username">The username of the user trying to follow the given channel.</param>
            /// <param name="channel">The channel to follow.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>A follow object representing the follow action.</returns>
            public static async Task<Models.API.Follow.Follow> FollowChannelAsync(string username, string channel, string accessToken = null) => await Internal.TwitchApi.FollowChannel(username, channel, accessToken);

            /// <summary>
            /// [SYNC] Unfollows a channel given by <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>user_follows_edit</code></para>
            /// </summary>
            /// <param name="username">The username of the user trying to follow the given channel.</param>
            /// <param name="channel">The channel to unfollow.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            public static void UnfollowChannel(string username, string channel, string accessToken = null) => Internal.TwitchApi.UnfollowChannel(username, channel, accessToken);
            /// <summary>
            /// [ASYNC] Unfollows a channel given by <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>user_follows_edit</code></para>
            /// </summary>
            /// <param name="username">The username of the user trying to follow the given channel.</param>
            /// <param name="channel">The channel to unfollow.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            public static async void UnfollowChannelAsync(string username, string channel, string accessToken = null) => await Task.Run(() => Internal.TwitchApi.UnfollowChannel(username, channel, accessToken));
        }

        /// <summary>
        /// Twitch API calls relating to Twitch subscriptions.
        /// </summary>
        public static class Subscriptions
        {
            /// <summary>
            /// [SYNC] Returns the amount of subscribers <paramref name="channel"/> has.
            /// <para>Authenticated, required scope: <code>channel_subscriptions</code></para>
            /// </summary>
            /// <param name="channel">The channel to retrieve the subscriptions from.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>An integer of the total subscription count.</returns>
            public static int GetSubscriberCount(string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetSubscriberCount(channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Returns the amount of subscribers <paramref name="channel"/> has.
            /// <para>Authenticated, required scope: <code>channel_subscriptions</code></para>
            /// </summary>
            /// <param name="channel">The channel to retrieve the subscriptions from.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>An integer of the total subscription count.</returns>
            public static async Task<int> GetSubscriberCountAsync(string channel, string accessToken = null) => await Internal.TwitchApi.GetSubscriberCount(channel, accessToken);

            /// <summary>
            /// [SYNC] Retrieves whether a <paramref name="username"/> is subscribed to a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_check_subscription</code></para>
            /// </summary>
            /// <param name="username">The user to check subscription status for.</param>
            /// <param name="channel">The channel to check against.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>True if the user is subscribed to the channel, false otherwise.</returns>
            public static Models.API.Channel.ChannelHasUserSubscribedResponse ChannelHasUserSubscribed(string username, string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.ChannelHasUserSubscribed(username, channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Retrieves whether a <paramref name="username"/> is subscribed to a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: <code>channel_check_subscription</code></para>
            /// </summary>
            /// <param name="username">The user to check subscription status for.</param>
            /// <param name="channel">The channel to check against.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>True if the user is subscribed to the channel, false otherwise.</returns>
            public static async Task<Models.API.Channel.ChannelHasUserSubscribedResponse> ChannelHasUserSubscribedAsync(string username, string channel, string accessToken = null) => await Internal.TwitchApi.ChannelHasUserSubscribed(username, channel, accessToken);

            /// <summary>
            /// [SYNC] Retrieves subscriber list for a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: channel_subscriptions</para>
            /// </summary>
            /// <param name="channel">The channel to check against.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>List of channel subscribers</returns>
            public static Models.API.Subscriber.SubscribersResponse GetChannelSubscribers(string channel, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetAllSubscribers(channel, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Retrieves subscriber list for a <paramref name="channel"/>.
            /// <para>Authenticated, required scope: channel_subscriptions</para>
            /// </summary>
            /// <param name="channel">The channel to check against.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>List of channel subscribers</returns>
            public static async Task<Models.API.Subscriber.SubscribersResponse> GetChannelSubscribersAsync(string channel, string accessToken = null) => await Internal.TwitchApi.GetAllSubscribers(channel, accessToken);

            public static Models.API.Subscriber.SubscribersResponsev5 GetChannelSubscribersv5(string channelId, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetAllSubscribersv5(channelId, accessToken)).Result;

            public static async Task<Models.API.Subscriber.SubscribersResponsev5> GetChannelSubscribersv5Async(string channelId, string accessToken = null) => await Internal.TwitchApi.GetAllSubscribersv5(channelId, accessToken);

            /// <summary>
            /// [SYNC] Retrieves channel subscribers from Twitch using limit and offset
            /// </summary>
            /// <param name="channel">Channel to pull subscribers from</param>
            /// <param name="limit">Limit the number of subscriptions returned. Max 100, default: 25</param>
            /// <param name="offset">Access the subscriber list at a specific offset. Default 0</param>
            /// <param name="direction">Direction of which the subscribers should be returned. Default Ascending</param>
            /// <param name="accessToken">Optional access token used if you haven't set the access token.</param>
            /// <returns>SubscribersResponse housing all subscribers and total number</returns>
            public static Models.API.Subscriber.SubscribersResponse GetSubscribers(string channel, int limit = 25, int offset = 0, Enums.SortDirection direction = Enums.SortDirection.Ascending, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetSubscribers(channel, limit, offset, direction, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Retrieves channel subscribers from Twitch using limit and offset
            /// </summary>
            /// <param name="channel">Channel to pull subscribers from</param>
            /// <param name="limit">Limit the number of subscriptions returned. Max 100, default: 25</param>
            /// <param name="offset">Access the subscriber list at a specific offset. Default 0</param>
            /// <param name="direction">Direction of which the subscribers should be returned. Default Ascending</param>
            /// <param name="accessToken">Optional access token used if you haven't set the access token.</param>
            /// <returns>SubscribersResponse housing all subscribers and total number</returns>
            public static async Task<Models.API.Subscriber.SubscribersResponse> GetSubscribersAsync(string channel, int limit = 25, int offset = 0, Enums.SortDirection direction = Enums.SortDirection.Ascending, string accessToken = null) => await Internal.TwitchApi.GetSubscribers(channel, limit, offset, direction, accessToken);
        }

        /// <summary>
        /// Twitch API calls relating to Twitch videos.
        /// </summary>
        public static class Videos
        {
            /// <summary>
            /// [SYNC] Returns a list of videos ordered by time of creation, starting with the most recent.
            /// </summary>
            /// <param name="channel">The channel to retrieve the list of videos from.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 10. Maximum is 100.</param>
            /// <param name="offset">Object offset for pagination. Default is 0.</param>
            /// <param name="onlyBroadcasts">Returns only broadcasts when true. Otherwise only highlights are returned. Default is false.</param>
            /// <param name="onlyHls">Returns only HLS VoDs when true. Otherwise only non-HLS VoDs are returned. Default is false.</param>
            /// <returns>A list of TwitchVideo objects the channel has available.</returns>
            public static List<Models.API.Video.Video> GetChannelVideos(string channel, int limit = 10, int offset = 0, bool onlyBroadcasts = false, bool onlyHls = false) => Task.Run(() => Internal.TwitchApi.GetChannelVideos(channel, limit, offset, onlyBroadcasts, onlyHls)).Result;
            /// <summary>
            /// [ASYNC] Returns a list of videos ordered by time of creation, starting with the most recent.
            /// </summary>
            /// <param name="channel">The channel to retrieve the list of videos from.</param>
            /// <param name="limit">Maximum number of objects in array. Default is 10. Maximum is 100.</param>
            /// <param name="offset">Object offset for pagination. Default is 0.</param>
            /// <param name="onlyBroadcasts">Returns only broadcasts when true. Otherwise only highlights are returned. Default is false.</param>
            /// <param name="onlyHls">Returns only HLS VoDs when true. Otherwise only non-HLS VoDs are returned. Default is false.</param>
            /// <returns>A list of TwitchVideo objects the channel has available.</returns>
            public static async Task<List<Models.API.Video.Video>> GetChannelVideosAsync(string channel, int limit = 10, int offset = 0, bool onlyBroadcasts = false, bool onlyHls = false) => await Internal.TwitchApi.GetChannelVideos(channel, limit, offset, onlyBroadcasts, onlyHls);
        }

        /// <summary>
        /// Twitch API calls relating to Twitch clips system.
        /// </summary>
        public static class Clips
        {
            /// <summary>
            /// [SYNC] Retrieves a list of top clips given specific (or no) parameters.
            /// </summary>
            /// <param name="channels">List of channels to get top clips from. Limit is 10.</param>
            /// <param name="games">List of games to get top clips from. Limit is 10.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10.</param>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="period">Period enum used to specify a date range. Default is Day.</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <returns>ClipsResponse object containing cursor string as well as List of Clip objects.</returns>
            public static Models.API.Clip.ClipsResponse GetTopClips(List<string> channels = null, List<string> games = null, int limit = 10, string cursor = null, Enums.Period period = Enums.Period.Day, bool trending = false) => Task.Run(() => Internal.TwitchApi.GetTopClips(channels, games, limit, cursor, period, trending)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a list of top clips given specific (or no) parameters.
            /// </summary>
            /// <param name="channels">List of channels to get top clips from. Limit is 10.</param>
            /// <param name="games">List of games to get top clips from. Limit is 10.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10.</param>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="period">Period enum used to specify a date range. Default is Day.</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <returns>ClipsResponse object containing cursor string as well as List of Clip objects.</returns>
            public static async Task<Models.API.Clip.ClipsResponse> GetTopClipsAsync(List<string> channels = null, List<string> games = null, int limit = 10, string cursor = null, Enums.Period period = Enums.Period.Day, bool trending = false) => await Internal.TwitchApi.GetTopClips(channels, games, limit, cursor, period, trending);

            /// <summary>
            /// [SYNC] Retrieves detailed information regarding a specific clip.
            /// </summary>
            /// <param name="slug">The string of words that identifies the clip.</param>
            /// <returns>Clip object.</returns>
            public static Models.API.Clip.Clip GetClipInformation(string slug) => Task.Run(() => Internal.TwitchApi.GetClipInformation(slug)).Result;
            /// <summary>
            /// [ASYNC] Retrieves detailed information regarding a specific clip.
            /// </summary>
            /// <param name="slug">The string of words that identifies the clip.</param>
            /// <returns>Clip object.</returns>
            public static async Task<Models.API.Clip.Clip> GetClipInformationAsync(string slug) => await Internal.TwitchApi.GetClipInformation(slug);

            /// <summary>
            /// [SYNC] Gets the top Clips for a user's followed games. Required scope: user_read
            /// </summary>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>ClipsResponse object.</returns>
            public static Models.API.Clip.ClipsResponse GetFollowedClips(string cursor = "0", int limit = 10, bool trending = false, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetFollowedClips(cursor, limit, trending, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Gets the top Clips for a user's followed games. Required scope: user_read
            /// </summary>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>ClipsResponse object.</returns>
            public static async Task<Models.API.Clip.ClipsResponse> GetFollowedClipsAsync(string cursor = "0", int limit = 10, bool trending = false, string accessToken = null) => await Internal.TwitchApi.GetFollowedClips(cursor, limit, trending, accessToken);
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

        #region Twitch API Global Functions
        /// <summary>
        /// [SYNC] Sets ClientId, which is required for all API calls. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// <param name="disableClientIdValidation">Forcefully disables Client-Id validation.</param>
        /// </summary>
        public static void SetClientId(string clientId, bool disableClientIdValidation = false) => Internal.TwitchApi.SetClientId(clientId, disableClientIdValidation);
        /// <summary>
        /// [ASYNC] Sets ClientId, which is required for all API calls. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// <param name="disableClientIdValidation">Forcefully disables Client-Id validation.</param>
        /// </summary>
        public static async void SetClientIdAsync(string clientId, bool disableClientIdValidation = false) => await Task.Run(() => Internal.TwitchApi.SetClientId(clientId, disableClientIdValidation));

        /// <summary>
        /// [SYNC] Sets Access Token, which is saved in memory. This is not necessary, as tokens can be passed into Api calls.
        /// </summary>
        /// <param name="accessToken">Twitch account OAuth token to store in memory.</param>
        public static void SetAccessToken(string accessToken) => Internal.TwitchApi.SetAccessToken(accessToken);
        /// <summary>
        /// [ASYNC] Sets Access Token, which is saved in memory. This is not necessary, as tokens can be passed into Api calls.
        /// </summary>
        /// <param name="accessToken">Twitch account OAuth token to store in memory.</param>
        public static async void SetAccessTokenAsync(string accessToken) => await Task.Run(() => Internal.TwitchApi.SetAccessToken(accessToken));

        /// <summary>
        /// [SYNC] Validates a Client-Id and optionally updates it.
        /// </summary>
        /// <param name="clientId">Client-Id string to be validated.</param>
        /// <param name="updateClientIdOnSuccess">Updates Client-Id if passed Client-Id is valid.</param>
        /// <returns>True or false depending on the validity of the Client-Id.</returns>
        public static bool ValidClientId(string clientId, bool updateClientIdOnSuccess = true) => Task.Run(() => Internal.TwitchApi.ValidClientId(clientId, updateClientIdOnSuccess)).Result;
        /// <summary>
        /// [ASYNC] Validates a Client-Id and optionally updates it.
        /// </summary>
        /// <param name="clientId">Client-Id string to be validated.</param>
        /// <param name="updateClientIdOnSuccess">Updates Client-Id if passed Client-Id is valid.</param>
        /// <returns>True or false depending on the validity of the Client-Id.</returns>
        public static async Task<bool> ValidClientIdAsync(string clientId, bool updateClientIdOnSuccess = true) => await Internal.TwitchApi.ValidClientId(clientId, updateClientIdOnSuccess);

        /// <summary>
        /// [SYNC] Calls Kraken API base endpoint and returns client ID and access token details.
        /// </summary>
        /// <param name="accessToken">You may provide an access token or not. If not, AUthorization model will not be set.</param>
        /// <returns>ValidationResponse model.</returns>
        public static Models.API.Other.Validate.ValidationResponse ValidationAPIRequest(string accessToken = null) => Task.Run(() => Internal.TwitchApi.ValidationAPIRequest(accessToken)).Result;
        /// <summary>
        /// [ASYNC] Calls Kraken API base endpoint and returns client ID and access token details.
        /// </summary>
        /// <param name="accessToken">You may provide an access token or not. If not, AUthorization model will not be set.</param>
        /// <returns>ValidationResponse model.</returns>
        public static async Task<Models.API.Other.Validate.ValidationResponse> ValidationAPIRequestAsync(string accessToken = null) => await Internal.TwitchApi.ValidationAPIRequest(accessToken);
        #endregion

        /// <summary>
        /// Class representing calls to third party (non-official API) sites.
        /// </summary>
        public static class ThirdParty
        {
            /// <summary>
            /// [SYNC] Gets any recorded username changes courtesy of https://twitch-tools.rootonline.de/
            /// Direct call: https://twitch-tools.rootonline.de/username_changelogs_search.php?q={username}&format=json
            /// </summary>            
            /// <param name="username">Username to get a history of.</param>
            /// <returns>List of name changes.</returns>
            public static List<Models.API.ThirdParty.UsernameChangeListing> GetUsernameChanges(string username) => Task.Run(() => Internal.TwitchApi.GetUsernameChanges(username)).Result;

            /// <summary>
            /// [SYNC] Gets any recorded username changes courtesy of https://twitch-tools.rootonline.de/
            /// Direct call: https://twitch-tools.rootonline.de/username_changelogs_search.php?q={username}&format=json
            /// </summary>            
            /// <param name="username">Username to get a history of.</param>
            /// <returns>List of name changes.</returns>
            public static async Task<List<Models.API.ThirdParty.UsernameChangeListing>> GetUsernameChangesAsync(string username) => await Internal.TwitchApi.GetUsernameChanges(username);
        }
    }
}
