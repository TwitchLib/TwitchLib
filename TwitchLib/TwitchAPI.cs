namespace TwitchLib
{
    #region using directives
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Internal.TwitchAPI;
    #endregion
    /// <summary>Fully featured Twitch API wrapper.</summary>
    public static class TwitchAPI
    {
        public static class Settings
        {
            public static string ClientId { get { return Internal.TwitchAPI.Shared.ClientId; } set { Internal.TwitchAPI.Shared.ClientId = value; } }
            public static string AccessToken { get { return Internal.TwitchAPI.Shared.AccessToken; } set { Internal.TwitchAPI.Shared.AccessToken = value; } }
            public static class Validators
            {
                public static bool SkipClientIdValidation { get; set; } = false;
                public static bool SkipAccessTokenValidation { get; set; } = false;
                public static bool SkipDynamicScopeValidation { get; set; } = false;
            }
            public static List<Enums.AuthScopes> Scopes { get { return Internal.TwitchAPI.Shared.Scopes; } }
        }

        public static class Badges
        {
            #region GetSubscriberBadgesForChannel
            public static Models.API.v5.Badges.ChannelDisplayBadges GetSubscriberBadgesForChannel(string channelId)
            {
                return Internal.TwitchAPI.v5.Badges.GetSubscriberBadgesForChannel(channelId);
            }

            public static async Task<Models.API.v5.Badges.ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Badges.GetSubscriberBadgesForChannel(channelId));
            }
            #endregion
            #region GetGlobalBadges
            public static Models.API.v5.Badges.GlobalBadgesResponse GetGlobalBadges()
            {
                return Internal.TwitchAPI.v5.Badges.GetGlobalBadges();
            }

            public static async Task<Models.API.v5.Badges.GlobalBadgesResponse> GetGlobalBadgesAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Badges.GetGlobalBadges());
            }
            #endregion
        }

        public static class Bits
        {
            #region GetCheermotes
            public static Models.API.v5.Bits.Action[] GetCheermotes(string channelId = null)
            {
                return Internal.TwitchAPI.v5.Bits.GetCheermotes(channelId);
            }

            public static async Task<Models.API.v5.Bits.Action[]> GetCheermotesAsync(string channelId = null)
            {
                return await Task.Run(() => v5.Bits.GetCheermotes(channelId));
            }
            #endregion
        }

        public static class Blocks
        {
            #region GetBlocks
            public static async Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocksAsync(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, accessToken);
                return await Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset, accessToken);
            }
            #endregion
            #region CreateBlock
            public static async Task<Models.API.v3.Blocks.Block> CreateBlock(string channel, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, accessToken);
                return await Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target, accessToken);
            }
            #endregion
            #region RemoveBlock
            public static async void RemoveBlock(string channel, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, accessToken);
                await Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target, accessToken);
            }
            #endregion
        }

        public static class ChannelFeeds
        {
            public static class v3
            {
                #region GetChannelFeedPosts
                public async static Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPosts(string channel, int limit = 25, string cursor = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, accessToken);
                    return await Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor);
                }
                #endregion
                #region CreatePost
                public static async Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePost(string channel, string content, bool share = false, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    return await Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share, accessToken);
                }
                #endregion
                #region GetPostById
                public static async Task<Models.API.v3.ChannelFeeds.Post> GetPostById(string channel, string postId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, accessToken);
                    return await Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId);
                }
                #endregion
                #region RemovePost
                public static async Task RemovePost(string channel, string postId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    await Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId, accessToken);
                }
                #endregion
                #region CreateReaction
                public static async Task CreateReaction(string channel, string postId, string emoteId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    await Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId, accessToken);
                }
                #endregion
                #region RemoveReaction
                public static async Task RemoveReaction(string channel, string postId, string emoteId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    await Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId, accessToken);
                }
                #endregion
            }
            
            public static class v5
            {
                #region GetMultipleFeedPosts
                public static Models.API.v5.ChannelFeed.MultipleFeedPosts GetMultipleFeedPosts(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.GetMultipleFeedPosts(channelId, limit, cursor, comments, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.GetMultipleFeedPosts(channelId, limit, cursor, comments, authToken));
                }
                #endregion
                #region GetFeedPosts
                public static Models.API.v5.ChannelFeed.FeedPost GetFeedPost(string channelId, string postId, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.GetFeedPost(channelId, postId, comments, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.GetFeedPost(channelId, postId, comments, authToken));
                }
                #endregion
                #region CreateFeedPost
                public static Models.API.v5.ChannelFeed.FeedPostCreation CreateFeedPost(string channelId, string content, bool? share = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateFeedPost(channelId, content, share, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateFeedPost(channelId, content, share, authToken));
                }
                #endregion
                #region DeleteFeedPost
                public static Models.API.v5.ChannelFeed.FeedPost DeleteFeedPost(string channelId, string postId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedPost(channelId, postId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedPost(channelId, postId, authToken));
                }
                #endregion
                #region CreateReactionToFeedPost
                public static Models.API.v5.ChannelFeed.FeedPostReactionPost CreateReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedPost(channelId, postId, emoteId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedPost(channelId, postId, emoteId, authToken));
                }
                #endregion
                #region DeleteReactionToFeedPost
                public static void DeleteReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedPost(channelId, postId, emoteId, authToken);
                }

                public static Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedPost(channelId, postId, emoteId, authToken));
                }
                #endregion
                #region GetFeedComments
                public static Models.API.v5.ChannelFeed.FeedPostComments GetFeedComments(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.GetFeedComments(channelId, postId, limit, cursor, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.GetFeedComments(channelId, postId, limit, cursor, authToken));
                }
                #endregion
                #region CreateFeedComment
                public static Models.API.v5.ChannelFeed.FeedPostComment CreateFeedComment(string channelId, string postId, string content, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateFeedComment(channelId, postId, content, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateFeedComment(channelId, postId, content, authToken));
                }
                #endregion
                #region DeleteFeedComment
                public static Models.API.v5.ChannelFeed.FeedPostComment DeleteFeedComment(string channelId, string postId, string commentId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedComment(channelId, postId, commentId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedComment(channelId, postId, commentId, authToken));
                }
                #endregion
                #region CreateReactionToFeedComments
                public static Models.API.v5.ChannelFeed.FeedPostReactionPost CreateReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedComment(channelId, postId, commentId, emoteId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedComment(channelId, postId, commentId, emoteId, authToken));
                }
                #endregion
                #region DeleteReactionToFeedComments
                public static void DeleteReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedComment(channelId, postId, commentId, emoteId, authToken);
                }

                public static Task DeleteReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    return Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedComment(channelId, postId, commentId, emoteId, authToken));
                }
                #endregion
            }
        }

        public static class Channels
        {
            public static class v3
            {
                #region GetChannelByName
                public static async Task<Models.API.v3.Channels.Channel> GetChannelByName(string channel)
                {
                    return await Internal.TwitchAPI.v3.Channels.GetChannelByName(channel);
                }
                #endregion
                #region GetChannel
                public static async Task<Models.API.v3.Channels.Channel> GetChannelAsync()
                {
                    return await Internal.TwitchAPI.v3.Channels.GetChannel();
                }
                #endregion
                #region GetChannelEditors
                public static async Task<Models.API.v3.Channels.GetEditorsResponse> GetChannelEditors(string channel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, accessToken);
                    return await Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel, accessToken);
                }
                #endregion
                #region UpdateChannel
                public static async Task<Models.API.v3.Channels.Channel> UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                    return await Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed, accessToken);
                }
                #endregion
                #region ResetStreamKey
                public static async Task<Models.API.v3.Channels.ResetStreamKeyResponse> ResetStreamKey(string channel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, accessToken);
                    return await Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel, accessToken);
                }
                #endregion
                #region RunCommercial
                public static async Task RunCommercial(string channel, Enums.CommercialLength length, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, accessToken);
                    await Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length, accessToken);
                }
                #endregion
                #region GetTeams
                public static async Task<Models.API.v3.Channels.GetTeamsResponse> GetTeams(string channel)
                {
                    return await Internal.TwitchAPI.v3.Channels.GetTeams(channel);
                }
                #endregion
            }
            public static class v5
            {
                #region GetChannel
                /// <summary>
                /// [SYNC] Gets a channel object based on a specified OAuth token.<para/>
                /// Get Channel returns more data than Get Channel by ID because Get Channel is privileged.<para/>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <returns>A ChannelPrivileged object including all Channel object info plus email and streamkey.</returns>
                public static Models.API.v5.Channels.ChannelAuthed GetChannel(string authToken = null)
                {
                    return Internal.TwitchAPI.v5.Channels.GetChannel(authToken);
                }

                /// <summary>
                /// [ASYNC] Gets a channel object based on a specified OAuth token.<para/>
                /// Get Channel returns more data than Get Channel by ID because Get Channel is privileged.<para/>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <returns>A ChannelPrivileged object including all Channel object info plus email and streamkey.</returns>
                public async static Task<Models.API.v5.Channels.ChannelAuthed> GetChannelAsync(string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannel(authToken));
                }
                #endregion
                #region GetChannelById
                /// <summary>
                /// [SYNC] Gets a speicified channel object.<para/>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A Channel object from the response of the Twitch API.</returns>
                public static Models.API.v5.Channels.Channel GetChannelByID(string channelId)
                {
                    return Internal.TwitchAPI.v5.Channels.GetChannelByID(channelId);
                }

                /// <summary>
                /// [ASYNC] Gets a speicified channel object.<para/>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A Channel object from the response of the Twitch API.</returns>
                public async static Task<Models.API.v5.Channels.Channel> GetChannelByIDAsync(string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelByID(channelId));
                }
                #endregion
                #region UpdateChannel
                /// <summary>
                /// [SYNC] Updates specified properties of a specified channel.<para/>
                /// In the request, the new properties are specified as a JSON object representation.<para/>
                /// <para>Required Authentication Scopes: To update delay or channel_feed_enabled parameter: a channel_editor token from the channel owner. To update other parameters: channel_editor.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="status">Description of the broadcaster’s status, displayed as a title on the channel page.</param>
                /// <param name="game">Name of the game.</param>
                /// <param name="delay">Channel delay, in seconds. This inserts a delay in the live feed. Requires the channel owner’s OAuth token.</param>
                /// <param name="channelFeedEnabled">If true, the channel’s feed is turned on. Requires the channel owner’s OAuth token. Default: false.</param>
                /// <returns>A Channel object with the newly changed properties.</returns>
                public static Models.API.v5.Channels.Channel UpdateChannel(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return Internal.TwitchAPI.v5.Channels.UpdateChannel(channelId, status, game, delay, channelFeedEnabled, authToken);
                }

                /// <summary>
                /// [ASYNC] Updates specified properties of a specified channel.<para/>
                /// In the request, the new properties are specified as a JSON object representation.<para/>
                /// <para>Required Authentication Scopes: To update delay or channel_feed_enabled parameter: a channel_editor token from the channel owner. To update other parameters: channel_editor.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="status">Description of the broadcaster’s status, displayed as a title on the channel page.</param>
                /// <param name="game">Name of the game.</param>
                /// <param name="delay">Channel delay, in seconds. This inserts a delay in the live feed. Requires the channel owner’s OAuth token.</param>
                /// <param name="channelFeedEnabled">If true, the channel’s feed is turned on. Requires the channel owner’s OAuth token. Default: false.</param>
                /// <returns>A Channel object with the newly changed properties.</returns>
                public async static Task<Models.API.v5.Channels.Channel> UpdateChannelAsync(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.UpdateChannel(channelId, status, game, delay, channelFeedEnabled, authToken));
                }
                #endregion
                #region GetChannelEditors
                /// <summary>
                /// <para>[SYNC] Gets a list of users who are editors for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A ChannelEditors object that contains an array of the Users which are Editor of the channel.</returns>
                public static Models.API.v5.Channels.ChannelEditors GetChannelEditors(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, authToken);
                    return Internal.TwitchAPI.v5.Channels.GetChannelEditors(channelId, authToken);
                }

                /// <summary>
                /// <para>[ASYNC] Gets a list of users who are editors for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A ChannelEditors object that contains an array of the Users which are Editor of the channel.</returns>
                public async static Task<Models.API.v5.Channels.ChannelEditors> GetChannelEditorsAsync(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelEditors(channelId, authToken));
                }
                #endregion
                #region GetChannelFollowers
                /// <summary>
                /// <para>[SYNC] Gets a list of users who follow a specified channel, sorted by the date when they started following the channel (newest first, unless specified otherwise).</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="cursor">Tells the server where to start fetching the next set of results, in a multi-page response.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <returns>A ChannelFollowers object that represents the response from the Twitch API.</returns>
                public static Models.API.v5.Channels.ChannelFollowers GetChannelFollowers(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
                {
                    return Internal.TwitchAPI.v5.Channels.GetChannelFollowers(channelId, limit, offset, cursor, direction);
                }

                /// <summary>
                /// <para>[ASYNC] Gets a list of users who follow a specified channel, sorted by the date when they started following the channel (newest first, unless specified otherwise).</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="cursor">Tells the server where to start fetching the next set of results, in a multi-page response.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <returns>A ChannelFollowers object that represents the response from the Twitch API.</returns>
                public async static Task<Models.API.v5.Channels.ChannelFollowers> GetChannelFollowersAsync(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelFollowers(channelId, limit, offset, cursor, direction));
                }
                #endregion
                #region GetChannelTeams
                /// <summary>
                /// <para>[SYNC] Gets a list of teams to which a specified channel belongs.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>An Array of the Teams the Channel belongs to.</returns>
                public static Models.API.v5.Channels.ChannelTeams GetChannelTeams(string channelId)
                {
                    return Internal.TwitchAPI.v5.Channels.GetChannelTeams(channelId);
                }

                /// <summary>
                /// <para>[ASYNC] Gets a list of teams to which a specified channel belongs.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>An Array of the Teams the Channel belongs to.</returns>
                public async static Task<Models.API.v5.Channels.ChannelTeams> GetChannelTeamsAsync(string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelTeams(channelId));
                }
                #endregion
                #region GetChannelSubscribers
                /// <summary>
                /// <para>[SYNC] Gets a list of users subscribed to a specified channel, sorted by the date when they subscribed.</para>
                /// <para>Required Authentication Scope: channel_subscriptions</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <param name="authToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public static Models.API.v5.Channels.ChannelSubscribers GetChannelSubscribers(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, authToken);
                    return Internal.TwitchAPI.v5.Channels.GetChannelSubscribers(channelId, limit, offset, direction, authToken);
                }

                /// <summary>
                /// <para>[ASYNC] Gets a list of users subscribed to a specified channel, sorted by the date when they subscribed.</para>
                /// <para>Required Authentication Scope: channel_subscriptions</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <param name="authToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public async static Task<Models.API.v5.Channels.ChannelSubscribers> GetChannelSubscribersAsync(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelSubscribers(channelId, limit, offset, direction, authToken));
                }
                #endregion
                #region GetAllSubscribers
                /// <summary>
                /// [SYNC] Makes a number of calls to get all subscriber objects belonging to a channel. THIS IS AN EXPENSIVE OPERATION.
                /// </summary>
                /// <param name="channelId">ChannelId indicating channel to get subs from.</param>
                /// <param name="accessToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public static List<Models.API.v5.Subscriptions.Subscription> GetAllSubscribers(string channelId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return Internal.TwitchAPI.v5.Channels.GetAllSubscribers(channelId, accessToken);
                }

                /// <summary>
                /// [ASYNC] Makes a number of calls to get all subscriber objects belonging to a channel. THIS IS AN EXPENSIVE OPERATION.
                /// </summary>
                /// <param name="channelId">ChannelId indicating channel to get subs from.</param>
                /// <param name="accessToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public async static Task<List<Models.API.v5.Subscriptions.Subscription>> GetAllSubscribersAsync(string channelId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetAllSubscribers(channelId, accessToken));
                }
                #endregion
                #region CheckChannelSubscriptionByUser
                /// <summary>
                /// <para>[SYNC] Checks if a specified channel has a specified user subscribed to it. Intended for use by channel owners.</para>
                /// <para>Returns a subscription object which includes the user if that user is subscribed. Requires authentication for the channel.</para>
                /// <para>Required Authentication Scope: channel_check_subscription</para>
                /// </summary>
                /// <param name="channelId">The specified channel to check the subscription on.</param>
                /// <param name="userId">The specified user to check for.</param>
                /// <returns>Returns a subscription object or null if not subscribed.</returns>
                public static Models.API.v5.Subscriptions.Subscription CheckChannelSubscriptionByUser(string channelId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, authToken);
                    return Internal.TwitchAPI.v5.Channels.CheckChannelSubscriptionByUser(channelId, userId, authToken);
                }

                /// <summary>
                /// <para>[ASYNC] Checks if a specified channel has a specified user subscribed to it. Intended for use by channel owners.</para>
                /// <para>Returns a subscription object which includes the user if that user is subscribed. Requires authentication for the channel.</para>
                /// <para>Required Authentication Scope: channel_check_subscription</para>
                /// </summary>
                /// <param name="channelId">The specified channel to check the subscription on.</param>
                /// <param name="userId">The specified user to check for.</param>
                /// <returns>Returns a subscription object or null if not subscribed.</returns>
                public async static Task<Models.API.v5.Subscriptions.Subscription> CheckChannelSubscriptionByUserAsync(string channelId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.CheckChannelSubscriptionByUser(channelId, userId, authToken));
                }
                #endregion
                #region GetChannelVideos
                public static Models.API.v5.Channels.ChannelVideos GetChannelVideos(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return Internal.TwitchAPI.v5.Channels.GetChannelVideos(channelId, limit, offset, broadcastType, language, sort);
                }

                public async static Task<Models.API.v5.Channels.ChannelVideos> GetChannelVideosAsync(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelVideos(channelId, limit, offset, broadcastType, language, sort));
                }
                #endregion
                #region StartChannelCommercial
                public static Models.API.v5.Channels.ChannelCommercial StartChannelCommercial(string channelId, Enums.CommercialLength duration, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, authToken);
                    return Internal.TwitchAPI.v5.Channels.StartChannelCommercial(channelId, duration, authToken);
                }

                public async static Task<Models.API.v5.Channels.ChannelCommercial> StartChannelCommercialAsync(string channelId, Enums.CommercialLength duration, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.StartChannelCommercial(channelId, duration, authToken));
                }
                #endregion
                #region ResetChannelStreamKey
                /// <summary>
                /// <para>[SYNC] Deletes the stream key for a specified channel. Once it is deleted, the stream key is automatically reset.</para>
                /// <para>A stream key (also known as authorization key) uniquely identifies a stream. Each broadcast uses an RTMP URL that includes the stream key. Stream keys are assigned by Twitch.</para>
                /// <para>Required Authentication Scope: channel_stream</para>
                /// </summary>
                /// <param name="channelId">The specified channel to reset the StreamKey on.</param>
                /// <returns>A ChannelPrivileged object that also contains the email and stream key of the channel aside from the normal channel values.</returns>
                public static Models.API.v5.Channels.ChannelAuthed ResetChannelStreamKey(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, authToken);
                    return Internal.TwitchAPI.v5.Channels.ResetChannelStreamKey(channelId, authToken);
                }

                /// <summary>
                /// <para>[ASYNC] Deletes the stream key for a specified channel. Once it is deleted, the stream key is automatically reset.</para>
                /// <para>A stream key (also known as authorization key) uniquely identifies a stream. Each broadcast uses an RTMP URL that includes the stream key. Stream keys are assigned by Twitch.</para>
                /// <para>Required Authentication Scope: channel_stream</para>
                /// </summary>
                /// <param name="channelId">The specified channel to reset the StreamKey on.</param>
                /// <returns>A ChannelPrivileged object that also contains the email and stream key of the channel aside from the normal channel values.</returns>
                public async static Task<Models.API.v5.Channels.ChannelAuthed> ResetChannelStreamKeyAsync(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.ResetChannelStreamKey(channelId, authToken));
                }
                #endregion
                #region Communities
                #region GetChannelCommunity
                /// <summary>
                /// <para>[SYNC] Gets the community for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel ID to get the community from.</param>
                /// <returns>A Community object that represents the community the channel is in.</returns>
                public static Models.API.v5.Communities.Community GetChannelCommunity(string channelId, string authToken)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return Internal.TwitchAPI.v5.Channels.GetChannelCommunity(channelId);
                }

                /// <summary>
                /// <para>[ASYNC] Gets the community for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel ID to get the community from.</param>
                /// <returns>A Community object that represents the community the channel is in.</returns>
                public async static Task<Models.API.v5.Communities.Community> GetChannelCommunityAsync(string channelId, string authToken)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelCommunity(channelId, authToken));
                }
                #endregion
                #region SetChannelCommunity
                /// <summary>
                /// <para>[SYNC] Sets a specified channel to be in a specified community.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel to set the community for.</param>
                /// <param name="communityId">The specified community to set the channel to be a part of.</param>
                public static void SetChannelCommunity(string channelId, string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    Internal.TwitchAPI.v5.Channels.SetChannelCommunity(channelId, communityId, authToken);
                }

                /// <summary>
                /// <para>[ASYNC]Sets a specified channel to be in a specified community.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel to set the community for.</param>
                /// <param name="communityId">The specified community to set the channel to be a part of.</param>
                public async static Task SetChannelCommunityAsync(string channelId, string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Channels.SetChannelCommunity(channelId, communityId, authToken));
                }
                #endregion
                #region DeleteChannelFromCommunity
                /// <summary>
                /// [SYNC] Deletes a specified channel from its community.
                /// </summary>
                /// <param name="channelId">The specified channel to be removed.</param>
                public static void DeleteChannelFromCommunity(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    Internal.TwitchAPI.v5.Channels.DeleteChannelFromCommunity(channelId, authToken);
                }

                /// <summary>
                /// [ASYNC] Deletes a specified channel from its community.
                /// </summary>
                /// <param name="channelId">The specified channel to be removed.</param>
                public async static Task DeleteChannelFromCommunityAsync(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Channels.DeleteChannelFromCommunity(channelId, authToken));
                }
                #endregion
                #endregion
            }
        }

        public static class Chat
        {
            public static class v3
            {
                #region GetBadges
                public static async Task<Models.API.v3.Chat.BadgesResponse> GetBadges(string channel)
                {
                    return await Internal.TwitchAPI.v3.Chat.GetBadges(channel);
                }
                #endregion
                #region GetAllEmoticons
                public static async Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticons()
                {
                    return await Internal.TwitchAPI.v3.Chat.GetAllEmoticons();
                }
                #endregion
                #region GetEmoticonsBySets
                public static async Task<Models.API.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySets(List<int> emotesets)
                {
                    return await Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets);
                }
                #endregion
            }

            public static class v5
            {
                #region GetChatBadgesByChannel
                public static Models.API.v5.Chat.ChannelBadges GetChatBadgesByChannel(string channelId)
                {
                    return Internal.TwitchAPI.v5.Chat.GetChatBadgesByChannel(channelId);
                }

                public async static Task<Models.API.v5.Chat.ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Chat.GetChatBadgesByChannel(channelId));
                }
                #endregion
                #region GetChatEmoticonsBySet
                public static Models.API.v5.Chat.EmoteSet GetChatEmoticonsBySet(List<int> emotesets = null)
                {
                    return Internal.TwitchAPI.v5.Chat.GetChatEmoticonsBySet(emotesets);
                }

                public async static Task<Models.API.v5.Chat.EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Chat.GetChatEmoticonsBySet(emotesets));
                }
                #endregion
                #region GetAllChatEmoticons
                public static Models.API.v5.Chat.AllChatEmotes GetAllChatEmoticons()
                {
                    return Internal.TwitchAPI.v5.Chat.GetAllChatEmoticons();
                }

                public async static Task<Models.API.v5.Chat.AllChatEmotes> GetAllChatEmoticonsAsync()
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Chat.GetAllChatEmoticons());
                }
                #endregion
            }
        }

        public static class Collections
        {
            #region GetCollectionMetadata
            public static Models.API.v5.Collections.CollectionMetadata GetCollectionMetadata(string collectionId)
            {
                return Internal.TwitchAPI.v5.Collections.GetCollectionMetadata(collectionId);
            }

            public async static Task<Models.API.v5.Collections.CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Collections.GetCollectionMetadata(collectionId));
            }
            #endregion
            #region GetCollection
            public static Models.API.v5.Collections.Collection GetCollection(string collectionId, bool? includeAllItems = null)
            {
                return Internal.TwitchAPI.v5.Collections.GetCollection(collectionId, includeAllItems);
            }

            public async static Task<Models.API.v5.Collections.Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Collections.GetCollection(collectionId, includeAllItems));
            }
            #endregion
            #region GetCollectionsByChannel
            public static Models.API.v5.Collections.CollectionsByChannel GetCollectionsByChannel(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                return Internal.TwitchAPI.v5.Collections.GetCollectionsByChannel(channelId, limit, cursor, containingItem);
            }

            public async static Task<Models.API.v5.Collections.CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Collections.GetCollectionsByChannel(channelId, limit, cursor, containingItem));
            }
            #endregion
            #region CreateCollection
            public static Models.API.v5.Collections.CollectionMetadata CreateCollection(string channelId, string collectionTitle, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                return Internal.TwitchAPI.v5.Collections.CreateCollection(channelId, collectionTitle, authToken);
            }

            public async static Task<Models.API.v5.Collections.CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.Collections.CreateCollection(channelId, collectionTitle, authToken));
            }
            #endregion
            #region UpdateCollection
            public static void UpdateCollection(string collectionId, string newCollectionTitle, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                Internal.TwitchAPI.v5.Collections.UpdateCollection(collectionId, newCollectionTitle, authToken);
            }

            public async static Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Collections.UpdateCollection(collectionId, newCollectionTitle, authToken));
            }
            #endregion
            #region CreateCollectionThumbnail
            public static void CreateCollectionThumbnail(string collectionId, string itemId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                Internal.TwitchAPI.v5.Collections.CreateCollectionThumbnail(collectionId, itemId, authToken);
            }

            public async static Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Collections.CreateCollectionThumbnail(collectionId, itemId, authToken));
            }
            #endregion
            #region DeleteCollection
            public static void DeleteCollection(string collectionId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                Internal.TwitchAPI.v5.Collections.DeleteCollection(collectionId, authToken);
            }

            public async static Task DeleteCollectionAsync(string collectionId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Collections.DeleteCollection(collectionId, authToken));
            }
            #endregion
            #region AddItemToCollection
            public static Models.API.v5.Collections.CollectionItem AddItemToCollection(string collectionId, string itemId, string itemType, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                return Internal.TwitchAPI.v5.Collections.AddItemToCollection(collectionId, itemId, itemType, authToken);
            }

            public async static Task<Models.API.v5.Collections.CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.Collections.AddItemToCollection(collectionId, itemId, itemType, authToken));
            }
            #endregion
            #region DeleteItemFromCollection
            public static void DeleteItemFromCollection(string collectionId, string itemId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                Internal.TwitchAPI.v5.Collections.DeleteItemFromCollection(collectionId, itemId, authToken);
            }

            public async static Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Collections.DeleteItemFromCollection(collectionId, itemId, authToken));
            }
            #endregion
            #region MoveItemWithinCollection
            public static void MoveItemWithinCollection(string collectionId, string itemId, int position, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                Internal.TwitchAPI.v5.Collections.MoveItemWithinCollection(collectionId, itemId, position, authToken);
            }

            public async static Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Collections.MoveItemWithinCollection(collectionId, itemId, position, authToken));
            }
            #endregion
        }

        public static class Communities
        {
            #region GetCommunityByName
            public static Models.API.v5.Communities.Community GetCommunityByName(string communityName)
            {
                return Internal.TwitchAPI.v5.Communities.GetCommunityByName(communityName);
            }

            public async static Task<Models.API.v5.Communities.Community> GetCommunityByNameAsync(string communityName)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetCommunityByName(communityName));
            }
            #endregion
            #region GetCommunityByID
            public static Models.API.v5.Communities.Community GetCommunityByID(string communityId)
            {
                return Internal.TwitchAPI.v5.Communities.GetCommunityByID(communityId);
            }

            public async static Task<Models.API.v5.Communities.Community> GetCommunityByIDAsync(string communityId)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetCommunityByID(communityId));
            }
            #endregion
            #region UpdateCommunity
            public static void UpdateCommunity(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.UpdateCommunity(communityId, summary, description, rules, email, authToken);
            }

            public async static Task UpdateCommunityAsync(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.UpdateCommunity(communityId, summary, description, rules, email, authToken));
            }
            #endregion
            #region GetTopCommunities
            public static Models.API.v5.Communities.TopCommunities GetTopCommunities(long? limit = null, string cursor = null)
            {
                return Internal.TwitchAPI.v5.Communities.GetTopCommunities(limit, cursor);
            }

            public async static Task<Models.API.v5.Communities.TopCommunities> GetTopCommunitiesAsync(long? limit = null, string cursor = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetTopCommunities(limit, cursor));
            }
            #endregion
            #region GetCommunityBannedUsers
            public static Models.API.v5.Communities.BannedUsers GetCommunityBannedUsers(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                return Internal.TwitchAPI.v5.Communities.GetCommunityBannedUsers(communityId, limit, cursor, authToken);
            }

            public async static Task<Models.API.v5.Communities.BannedUsers> GetCommunityBannedUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetCommunityBannedUsers(communityId, limit, cursor, authToken));
            }
            #endregion
            #region BanCommunityUser
            public static void BanCommunityUser(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                Internal.TwitchAPI.v5.Communities.BanCommunityUser(communityId, userId, authToken);
            }

            public static void BanCommunityUserAsync(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                Internal.TwitchAPI.v5.Communities.BanCommunityUser(communityId, userId, authToken);
            }
            #endregion
            #region UnBanCommunityUser
            public static void UnBanCommunityUser(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                Internal.TwitchAPI.v5.Communities.UnBanCommunityUser(communityId, userId, authToken);
            }

            public async static Task UnBanCommunityUserAsync(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.UnBanCommunityUser(communityId, userId, authToken));
            }
            #endregion
            #region CreateCommunityAvatarImage
            public static void CreateCommunityAvatarImage(string communityId, string avatarImage, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.CreateCommunityAvatarImage(communityId, avatarImage, authToken);
            }

            public async static Task CreateCommunityAvatarImageAsync(string communityId, string avatarImage, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.CreateCommunityAvatarImage(communityId, avatarImage, authToken));
            }
            #endregion
            #region DeleteCommunityAvatarImage
            public static void DeleteCommunityAvatarImage(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.DeleteCommunityAvatarImage(communityId, authToken);
            }

            public async static Task DeleteCommunityAvatarImageAsync(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.DeleteCommunityAvatarImage(communityId, authToken));
            }
            #endregion
            #region CreateCommunityCoverImage
            public static void CreateCommunityCoverImage(string communityId, string coverImage, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.CreateCommunityCoverImage(communityId, coverImage, authToken);
            }

            public async static Task CreateCommunityCoverImageAsync(string communityId, string coverImage, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.CreateCommunityCoverImage(communityId, coverImage, authToken));
            }
            #endregion
            #region DeleteCommunityCoverImage
            public static void DeleteCommunityCoverImage(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.DeleteCommunityCoverImage(communityId, authToken);
            }

            public async static Task DeleteCommunityCoverImageAsync(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.DeleteCommunityCoverImage(communityId, authToken));
            }
            #endregion
            #region GetCommunityModerators
            public static Models.API.v5.Communities.Moderators GetCommunityModerators(string communityId, string authToken)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                return Internal.TwitchAPI.v5.Communities.GetCommunityModerators(communityId, authToken);
            }

            public async static Task<Models.API.v5.Communities.Moderators> GetCommunityModeratorsAsync(string communityId, string authToken)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetCommunityModerators(communityId, authToken));
            }
            #endregion
            #region AddCommunityModerator
            public static void AddCommunityModerator(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.AddCommunityModerator(communityId, userId, authToken);
            }

            public async static Task AddCommunityModeratorAsync(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.AddCommunityModerator(communityId, userId, authToken));
            }
            #endregion
            #region DeleteCommunityModerator
            public static void DeleteCommunityModerator(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                Internal.TwitchAPI.v5.Communities.DeleteCommunityModerator(communityId, userId, authToken);
            }

            public async static Task DeleteCommunityModeratorAsync(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.DeleteCommunityModerator(communityId, userId, authToken));
            }
            #endregion
            #region GetCommunityPermissions
            public static Dictionary<string, bool> GetCommunityPermissions(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Any, authToken);
                return Internal.TwitchAPI.v5.Communities.GetCommunityPermissions(communityId, authToken);
            }

            public async static Task<Dictionary<string, bool>> GetCommunityPermissionsAsync(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Any, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetCommunityPermissions(communityId, authToken));
            }
            #endregion
            #region ReportCommunityViolation
            public static void ReportCommunityViolation(string communityId, string channelId)
            {
                Internal.TwitchAPI.v5.Communities.ReportCommunityViolation(communityId, channelId);
            }

            public async static Task ReportCommunityViolationAsync(string communityId, string channelId)
            {
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.ReportCommunityViolation(communityId, channelId));
            }
            #endregion
            #region GetCommunityTimedOutUsers
            public static Models.API.v5.Communities.TimedOutUsers GetCommunityTimedOutUsers(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                return Internal.TwitchAPI.v5.Communities.GetCommunityTimedOutUsers(communityId, limit, cursor, authToken);
            }

            public async static Task<Models.API.v5.Communities.TimedOutUsers> GetCommunityTimedOutUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.Communities.GetCommunityTimedOutUsers(communityId, limit, cursor, authToken));
            }
            #endregion
            #region AddCommunityTimedOutUser
            public static void AddCommunityTimedOutUser(string communityId, string userId, int duration, string reason = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                Internal.TwitchAPI.v5.Communities.AddCommunityTimedOutUser(communityId, userId, duration, reason, authToken);
            }

            public async static Task AddCommunityTimedOutUserAsync(string communityId, string userId, int duration, string reason = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.AddCommunityTimedOutUser(communityId, userId, duration, reason, authToken));
            }
            #endregion
            #region DeleteCommunityTimedOutUser
            public static void DeleteCommunityTimedOutUser(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                Internal.TwitchAPI.v5.Communities.DeleteCommunityTimedOutUser(communityId, userId, authToken);
            }

            public async static Task DeleteCommunityTimedOutUserAsync(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await Task.Run(() => Internal.TwitchAPI.v5.Communities.DeleteCommunityTimedOutUser(communityId, userId, authToken));
            }
            #endregion
        }

        public static class Follows
        {
            #region GetFollowers
            public static async Task<Models.API.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                return await v3.Follows.GetFollowers(channel, limit, offset, cursor, direction);
            }
            #endregion
            #region GetFollows
            public static async Task<Models.API.v3.Follows.FollowsResponse> GetFollows(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                return await v3.Follows.GetFollows(channel, limit, offset, direction, sortBy);
            }
            #endregion
            #region GetFollowStatus
            public static async Task<Models.API.v3.Follows.Follows> GetFollowsStatus(string user, string targetChannel)
            {
                return await v3.Follows.GetFollowsStatus(user, targetChannel);
            }
            #endregion
            #region CreateFollow
            public static async Task<Models.API.v3.Follows.Follows> CreateFollow(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                return await v3.Follows.CreateFollow(user, targetChannel, notifications, accessToken);
            }
            #endregion
            #region RemoveFollow
            public static void RemoveFollow(string user, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target, accessToken);
            }

            public static async Task RemoveFollowAsync(string user, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                await Task.Run(() => Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target, accessToken));
            }
            #endregion
        }

        public static class Games
        {
            public static class v3
            {
                #region GetTopGames
                public async static Task<Models.API.v3.Games.TopGamesResponse> GetTopGames(int limit = 10, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Games.GetTopGames(limit, offset);
                }
                #endregion
            }

            public static class v5
            {
                #region GetTopGames
                public static Models.API.v5.Games.TopGames GetTopGames(int? limit = null, int? offset = null)
                {
                    return Internal.TwitchAPI.v5.Games.GetTopGames(limit, offset);
                }

                public async static Task<Models.API.v5.Games.TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Games.GetTopGames(limit, offset));
                }
                #endregion
            }
        }

        public static class Ingests
        {
            public static class v3
            {
                #region GetIngests
                public async static Task<Models.API.v3.Ingests.IngestsResponse> GetIngests()
                {
                    return await Internal.TwitchAPI.v3.Ingests.GetIngests();
                }
                #endregion
            }

            public static class v5
            {
                #region GetIngestServerList
                public static Models.API.v5.Ingests.Ingests GetIngestServerList()
                {
                    return Internal.TwitchAPI.v5.Ingests.GetIngestServerList();
                }

                public static async Task<Models.API.v5.Ingests.Ingests> GetIngestServerListAsync()
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Ingests.GetIngestServerList());
                }
                #endregion
            }
        }

        public static class Root
        {
            public static class v3
            {
                #region GetRoot
                public async static Task<Models.API.v3.Root.RootResponse> GetRoot(string accessToken = null)
                {
                    return await Internal.TwitchAPI.v3.Root.GetRoot(accessToken);
                }
                #endregion
            }

            public static class v5
            {
                #region GetRoot
                public static Models.API.v5.Root.Root GetRoot(string accessToken = null)
                {
                    return Internal.TwitchAPI.v5.Root.GetRoot(accessToken);
                }

                public async static Task<Models.API.v5.Root.Root> GetRootAsync(string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Root.GetRoot(accessToken));
                }
                #endregion
            }
            
        }

        public static class Search
        {
            public static class v3
            {
                #region SearchChannels
                public async static Task<Models.API.v3.Search.SearchChannelsResponse> SearchChannels(string query, int limit = 25, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Search.SearchChannels(query, limit, offset);
                }
                #endregion
                #region SearchStreams
                public async static Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
                {
                    return await Internal.TwitchAPI.v3.Search.SearchStreams(query, limit, offset, hls);
                }
                #endregion
                #region SearchGames
                public async static Task<Models.API.v3.Search.SearchGamesResponse> SearchGames(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
                {
                    return await Internal.TwitchAPI.v3.Search.SearchGames(query, type, live);
                }
                #endregion
            }

            public static class v5
            {
                #region SearchChannels
                public static Models.API.v5.Search.SearchChannels SearchChannels(string encodedSearchQuery, int? limit = null, int? offset = null)
                {
                    return Internal.TwitchAPI.v5.Search.SearchChannels(encodedSearchQuery, limit, offset);
                }

                public async static Task<Models.API.v5.Search.SearchChannels> SearchChannelsAsync(string encodedSearchQuery, int? limit = null, int? offset = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Search.SearchChannels(encodedSearchQuery, limit, offset));
                }
                #endregion
                #region SearchGames
                public static Models.API.v5.Search.SearchGames SearchGames(string encodedSearchQuery, bool? live = null)
                {
                    return Internal.TwitchAPI.v5.Search.SearchGames(encodedSearchQuery, live);
                }
                public async static Task<Models.API.v5.Search.SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Search.SearchGames(encodedSearchQuery, live));
                }
                #endregion
                #region SearchStreams
                public static Models.API.v5.Search.SearchStreams SearchStreams(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
                {
                    return Internal.TwitchAPI.v5.Search.SearchStreams(encodedSearchQuery, limit, offset, hls);
                }

                public async static Task<Models.API.v5.Search.SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Search.SearchStreams(encodedSearchQuery, limit, offset, hls));
                }
                #endregion
            }
        }

        public static class Streams
        {
            public static class v3
            {
                #region GetStream
                public async static Task<Models.API.v3.Streams.StreamResponse> GetStream(string channel)
                {
                    return await Internal.TwitchAPI.v3.Streams.GetStream(channel);
                }
                #endregion
                #region GetStreams
                public async static Task<Models.API.v3.Streams.StreamsResponse> GetStreams(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
                {
                    return await Internal.TwitchAPI.v3.Streams.GetStreams(game, channel, limit, offset, clientId, streamType, language);
                }
                #endregion
                #region GetFeaturedStreams
                public async static Task<Models.API.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreams(int limit = 25, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Streams.GetFeaturedStreams(limit, offset);
                }
                #endregion
                #region GetStreamsSummary
                public async static Task<Models.API.v3.Streams.Summary> GetStreamsSummary()
                {
                    return await Internal.TwitchAPI.v3.Streams.GetStreamsSummary();
                }
                #endregion
            }

            public static class v5
            {
                #region GetStreamByUser
                public static Models.API.v5.Streams.StreamByUser GetStreamByUser(string channelId, string streamType = null)
                {
                    return Internal.TwitchAPI.v5.Streams.GetStreamByUser(channelId, streamType);
                }

                public async static Task<Models.API.v5.Streams.StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.GetStreamByUser(channelId, streamType));
                }
                #endregion
                #region GetLiveStreams
                public static Models.API.v5.Streams.LiveStreams GetLiveStreams(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
                {
                    return Internal.TwitchAPI.v5.Streams.GetLiveStreams(channelList, game, language, streamType, limit, offset);
                }

                public async static Task<Models.API.v5.Streams.LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.GetLiveStreams(channelList, game, language, streamType, limit, offset));
                }
                #endregion
                #region GetStreamsSummary
                public static Models.API.v5.Streams.StreamsSummary GetStreamsSummary(string game = null)
                {
                    return Internal.TwitchAPI.v5.Streams.GetStreamsSummary(game);
                }

                public async static Task<Models.API.v5.Streams.StreamsSummary> GetStreamsSummaryAsync(string game = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.GetStreamsSummary(game));
                }
                #endregion
                #region GetFeaturedStreams
                public static Models.API.v5.Streams.FeaturedStreams GetFeaturedStreams(int? limit = null, int? offset = null)
                {
                    return Internal.TwitchAPI.v5.Streams.GetFeaturedStreams(limit, offset);
                }

                public async static Task<Models.API.v5.Streams.FeaturedStreams> GetFeaturedStreamsAsync(int? limit = null, int? offset = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.GetFeaturedStreams(limit, offset));
                }
                #endregion
                #region GetFollowedStreams
                public static Models.API.v5.Streams.FollowedStreams GetFollowedStreams(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return Internal.TwitchAPI.v5.Streams.GetFollowedStreams(streamType, limit, offset, authToken);
                }

                public async static Task<Models.API.v5.Streams.FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.GetFollowedStreams(streamType, limit, offset, authToken));
                }
                #endregion
                #region GetUptime
                public static TimeSpan? GetUptime(string channelId)
                {
                    return Internal.TwitchAPI.v5.Streams.GetUptime(channelId);
                }

                public async static Task<TimeSpan?> GetUptimeAsync(string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.GetUptime(channelId));
                }
                #endregion
                #region BroadcasterOnline
                public static bool BroadcasterOnline(string channelId)
                {
                    return Internal.TwitchAPI.v5.Streams.BroadcasterOnline(channelId);
                }
                
                public async static Task<bool> BroadcasterOnlineAsync(string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Streams.BroadcasterOnline(channelId));
                }
                #endregion
            }
        }

        public static class Subscriptions
        {
            #region GetSubscribers
            public async static Task<Models.API.v3.Subscriptions.SubscribersResponse> GetSubscribers(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                return await v3.Subscriptions.GetSubscribers(channel, limit, offset, direction, accessToken);
            }
            #endregion
            #region GetAllSubscribers
            public async static Task<List<Models.API.v3.Subscriptions.Subscriber>> GetAllSubscribers(string channel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetAllSubscribers(channel, accessToken));
            }
            #endregion
            #region ChannelHasUserSubscribed
            public async static Task<Models.API.v3.Subscriptions.Subscriber> ChannelHasUserSubscribed(string channel, string targetUser, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, accessToken);
                return await v3.Subscriptions.ChannelHasUserSubscribed(channel, targetUser, accessToken);
            }
            #endregion
            #region UserSubscribedToChannel
            public async static Task<Models.API.v3.Subscriptions.ChannelSubscription> UserSubscribedToChannel(string user, string targetChannel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                return await v3.Subscriptions.UserSubscribedToChannel(user, targetChannel, accessToken);
            }
            #endregion
            #region GetSubscriberCount
            public async static Task<int> GetSubscriberCount(string channel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                return await v3.Subscriptions.GetSubscriberCount(channel, accessToken);
            }
            #endregion
        }

        public static class Teams
        {
            public static class v3
            {
                #region GetTeams
                public async static Task<Models.API.v3.Teams.GetTeamsResponse> GetTeams(int limit = 25, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Teams.GetTeams(limit, offset);
                }
                #endregion
                #region GetTeam
                public async static Task<Models.API.v3.Teams.Team> GetTeam(string teamName)
                {
                    return await Internal.TwitchAPI.v3.Teams.GetTeam(teamName);
                }
                #endregion
            }

            public static class v5
            {
                #region GetAllTeams
                public static Models.API.v5.Teams.AllTeams GetAllTeams(int? limit = null, int? offset = null)
                {
                    return Internal.TwitchAPI.v5.Teams.GetAllTeams(limit, offset);
                }

                public async static Task<Models.API.v5.Teams.AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Teams.GetAllTeams(limit, offset));
                }
                #endregion
                #region GetTeam
                public static Models.API.v5.Teams.Team GetTeam(string teamName)
                {
                    return Internal.TwitchAPI.v5.Teams.GetTeam(teamName);
                }

                public async static Task<Models.API.v5.Teams.Team> GetTeamAsync(string teamName)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Teams.GetTeam(teamName));
                }
                #endregion
            }
        }

        public static class Users
        {
            public static class v3
            {
                #region GetUserFromUsername
                public async static Task<Models.API.v3.Users.User> GetUserFromUsername(string username)
                {
                    return await Internal.TwitchAPI.v3.User.GetUserFromUsername(username);
                }
                #endregion
                #region GetEmotes
                public async static Task<Models.API.v3.Users.UserEmotesResponse> GetEmotes(string username, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetEmotes(username, accessToken);
                }
                #endregion
                #region GetUserFromToken
                public async static Task<Models.API.v3.Users.FullUser> GetUserFromToken(string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetUserFromToken(accessToken);
                }
                #endregion
                #region GetFollowedStreams
                public async static Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreams(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetFollowedStreams(limit, offset, type, accessToken);
                }
                #endregion
                #region GetFollowedVideos
                public async static Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideos(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetFollowedVideos(limit, offset, broadcastType, accessToken);
                }
                #endregion
            }

            public static class v5
            {
                #region GetUsersByName
                public static Models.API.v5.Users.Users GetUsersByName(List<string> usernames)
                {
                    return Internal.TwitchAPI.v5.Users.GetUsersByName(usernames);
                }

                public async static Task<Models.API.v5.Users.Users> GetUsersByNameAsync(List<string> usernames)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUsersByName(usernames));
                }
                #endregion
                #region GetUser
                public static Models.API.v5.Users.UserAuthed GetUser(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return Internal.TwitchAPI.v5.Users.GetUser(authToken);
                }

                public async static Task<Models.API.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUser(authToken));
                }
                #endregion
                #region GetUserByID
                public static Models.API.v5.Users.User GetUserByID(string userId)
                {
                    return Internal.TwitchAPI.v5.Users.GetUserByID(userId);
                }

                public async static Task<Models.API.v5.Users.User> GetUserByIDAsync(string userId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUserByID(userId));
                }
                #endregion
                #region GetUserByName
                public static Models.API.v5.Users.Users GetUserByName(string username)
                {
                    return Internal.TwitchAPI.v5.Users.GetUserByName(username);
                }
                public async static Task<Models.API.v5.Users.Users> GetUserByNameAsync(string username)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUserByName(username));
                }
                #endregion
                #region GetUserEmotes
                public static Models.API.v5.Users.UserEmotes GetUserEmotes(string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return Internal.TwitchAPI.v5.Users.GetUserEmotes(userId, authToken);
                }

                public async static Task<Models.API.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUserEmotes(userId, authToken));
                }
                #endregion
                #region CheckUserSubscriptionByChannel
                public static Models.API.v5.Subscriptions.Subscription CheckUserSubscriptionByChannel(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return Internal.TwitchAPI.v5.Users.CheckUserSubscriptionByChannel(userId, channelId, authToken);
                }

                public async static Task<Models.API.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.CheckUserSubscriptionByChannel(userId, channelId, authToken));
                }
                #endregion
                #region GetUserFollows
                public static Models.API.v5.Users.UserFollows GetUserFollows(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
                {
                    return Internal.TwitchAPI.v5.Users.GetUserFollows(userId, limit, offset, direction, sortby);
                }

                public async static Task<Models.API.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUserFollows(userId, limit, offset, direction, sortby));
                }
                #endregion
                #region CheckUserFollowsByChannel
                public static Models.API.v5.Users.UserFollow CheckUserFollowsByChannel(string userId, string channelId)
                {
                    return Internal.TwitchAPI.v5.Users.CheckUserFollowsByChannel(userId, channelId);
                }

                public async static Task<Models.API.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.CheckUserFollowsByChannel(userId, channelId));
                }
                #endregion
                #region FollowChannel
                public static Models.API.v5.Users.UserFollow FollowChannel(string userId, string channelId, bool? notifications = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    return Internal.TwitchAPI.v5.Users.FollowChannel(userId, channelId, notifications, authToken);
                }

                public async static Task<Models.API.v5.Users.UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.FollowChannel(userId, channelId, notifications, authToken));
                }
                #endregion
                #region UnfollowChannel
                public static void UnfollowChannel(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    Internal.TwitchAPI.v5.Users.UnfollowChannel(userId, channelId, authToken);
                }

                public async static Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Users.UnfollowChannel(userId, channelId, authToken));
                }
                #endregion
                #region GetUserBlockList
                public static Models.API.v5.Users.UserBlocks GetUserBlockList(string userId, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, authToken);
                    return Internal.TwitchAPI.v5.Users.GetUserBlockList(userId, limit, offset, authToken);
                }

                public async static Task<Models.API.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.GetUserBlockList(userId, limit, offset, authToken));
                }
                #endregion
                #region BlockUser
                public static Models.API.v5.Users.UserBlock BlockUser(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    return Internal.TwitchAPI.v5.Users.BlockUser(sourceUserId, targetUserId, authToken);
                }

                public async static Task<Models.API.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.BlockUser(sourceUserId, targetUserId, authToken));
                }
                #endregion
                #region UnblockUser
                public static void UnblockUser(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    Internal.TwitchAPI.v5.Users.UnblockUser(sourceUserId, targetUserId, authToken);
                }

                public async static Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Users.UnblockUser(sourceUserId, targetUserId, authToken));
                }
                #endregion
                #region ViewerHeartbeatService
                #region CreateUserConnectionToViewerHeartbeatService
                public static void CreateUserConnectionToViewerHeartbeatService(string identifier, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    Internal.TwitchAPI.v5.Users.CreateUserConnectionToViewerHeartbeatService(identifier, authToken);
                }

                public async static Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Users.CreateUserConnectionToViewerHeartbeatService(identifier, authToken));
                }
                #endregion
                #region CheckUserConnectionToViewerHeartbeatService
                public static Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck CheckUserConnectionToViewerHeartbeatService(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return Internal.TwitchAPI.v5.Users.CheckUserConnectionToViewerHeartbeatService(authToken);
                }

                public async static Task<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Users.CheckUserConnectionToViewerHeartbeatService(authToken));
                }
                #endregion
                #region DeleteUserConnectionToViewerHeartbeatService
                public static void DeleteUserConnectionToViewerHeartbeatServicechStreams(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    Internal.TwitchAPI.v5.Users.DeleteUserConnectionToViewerHeartbeatServicechStreams(authToken);
                }

                public async static Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Users.DeleteUserConnectionToViewerHeartbeatServicechStreams(authToken));
                }
                #endregion
                #endregion
            }
        }

        public static class Videos
        {
            public static class v3
            {
                #region GetVideo
                public async static Task<Models.API.v3.Videos.Video> GetVideo(string id)
                {
                    return await Internal.TwitchAPI.v3.Videos.GetVideo(id);
                }
                #endregion
                #region GetTopVideos
                public async static Task<Models.API.v3.Videos.TopVideosResponse> GetTopVideos(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
                {
                    return await Internal.TwitchAPI.v3.Videos.GetTopVideos(limit, offset, game, period);
                }
                #endregion
            }

            public static class v5
            {
                #region GetVideo
                public static Models.API.v5.Videos.Video GetVideo(string videoId)
                {
                    return Internal.TwitchAPI.v5.Videos.GetVideo(videoId);
                }

                public async static Task<Models.API.v5.Videos.Video> GetVideoAsync(string videoId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Videos.GetVideo(videoId));
                }
                #endregion
                #region GetTopVideos
                public static Models.API.v5.Videos.TopVideos GetTopVideos(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return Internal.TwitchAPI.v5.Videos.GetTopVideos(limit, offset, game, period, broadcastType, language, sort);
                }

                public async static Task<Models.API.v5.Videos.TopVideos> GetTopVideosAsync(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Videos.GetTopVideos(limit, offset, game, period, broadcastType, language, sort));
                }
                #endregion
                #region GetFollowedVideos
                public static Models.API.v5.Videos.FollowedVideos GetFollowedVideos(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return Internal.TwitchAPI.v5.Videos.GetFollowedVideos(limit, offset, broadcastType, language, sort, authToken);
                }

                public async static Task<Models.API.v5.Videos.FollowedVideos> GetFollowedVideosAsync(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Videos.GetFollowedVideos(limit, offset, broadcastType, language, sort, authToken));
                }
                #endregion
                #region UploadVideo
                public static Models.API.v5.UploadVideo.UploadedVideo UploadVideo(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                    return Internal.TwitchAPI.v5.Videos.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken);
                }

                public async static Task<Models.API.v5.UploadVideo.UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Videos.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken));
                }
                #endregion
                #region UpdateVideo
                public static Models.API.v5.Videos.Video UpdateVideo(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return Internal.TwitchAPI.v5.Videos.UpdateVideo(videoId, description, game, language, tagList, title, authToken);
                }

                public async static Task<Models.API.v5.Videos.Video> UpdateVideoAsync(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Task.Run(() => Internal.TwitchAPI.v5.Videos.UpdateVideo(videoId, description, game, language, tagList, title, authToken));
                }
                #endregion
                #region DeleteVideo
                public static void DeleteVideo(string videoId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    Internal.TwitchAPI.v5.Videos.DeleteVideo(videoId, authToken);
                }

                public async static Task DeleteVideoAsync(string videoId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Task.Run(() => Internal.TwitchAPI.v5.Videos.DeleteVideo(videoId, authToken));
                }
                #endregion
            }
        }

        public static class Clips
        {
            #region GetClip
            public static Models.API.v5.Clips.Clip GetClip(string slug)
            {
                return Internal.TwitchAPI.v5.GetClip(slug);
            }

            public static async Task<Models.API.v5.Clips.Clip> GetClipAsync(string slug)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.GetClip(slug));
            }
            #endregion
            #region GetTopClips
            public static Models.API.v5.Clips.TopClipsResponse GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v5.Clips.Period period = Models.API.v5.Clips.Period.Week, bool trending = false)
            {
                return Internal.TwitchAPI.v5.GetTopClips(channel, cursor, game, limit, period, trending);
            }

            public static async Task<Models.API.v5.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v5.Clips.Period period = Models.API.v5.Clips.Period.Week, bool trending = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v5.GetTopClips(channel, cursor, game, limit, period, trending));
            }
            #endregion
            #region GetFollowedClips
            public static Models.API.v5.Clips.FollowClipsResponse GetFollowedClips(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return Internal.TwitchAPI.v5.GetFollowedClips(limit, cursor, trending, authToken);
            }

            public static async Task<Models.API.v5.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return await Task.Run(() => Internal.TwitchAPI.v5.GetFollowedClips(limit, cursor, trending, authToken));
            }
            #endregion
        }

        /// <summary>These endpoints are pretty cool, but they may stop working at anytime due to changes Twitch makes.</summary>
        public static class Undocumented
        {
            #region GetClipChat
            public static Models.API.Undocumented.ClipChat.GetClipChatResponse GetClipChat(string slug)
            {
                return Internal.TwitchAPI.Undocumented.GetClipChat(slug);
            }

            public static async Task<Models.API.Undocumented.ClipChat.GetClipChatResponse> GetClipChatAsync(string slug)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetClipChat(slug));
            }
            #endregion
            #region GetTwitchPrimeOffers
            public static Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers GetTwitchPrimeOffers()
            {
                return Internal.TwitchAPI.Undocumented.GetTwitchPrimeOffers();
            }

            public static async Task<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers> GetTwitchPrimeOffersAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetTwitchPrimeOffers());
            }
            #endregion
            #region GetChannelHosts
            public static Models.API.Undocumented.Hosting.ChannelHostsResponse GetChannelHosts(string channelId)
            {
                return Internal.TwitchAPI.Undocumented.GetChannelHosts(channelId);
            }

            public static async Task<Models.API.Undocumented.Hosting.ChannelHostsResponse> GetChannelHostsAsync(string channelId)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetChannelHosts(channelId));
            }
            #endregion
            #region GetChatProperties
            public static Models.API.Undocumented.ChatProperties.ChatProperties GetChatProperties(string channelName)
            {
                return Internal.TwitchAPI.Undocumented.GetChatProperties(channelName);
            }

            public static async Task<Models.API.Undocumented.ChatProperties.ChatProperties> GetChatPropertiesAsync(string channelName)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetChatProperties(channelName));
            }
            #endregion
            #region GetChannelPanels
            public static Models.API.Undocumented.ChannelPanels.Panel[] GetChannelPanels(string channelName)
            {
                return Internal.TwitchAPI.Undocumented.GetChannelPanels(channelName);
            }

            public static async Task<Models.API.Undocumented.ChannelPanels.Panel[]> GetChannelPanelsAsync(string channelName)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetChannelPanels(channelName));
            }
            #endregion
            #region GetCSMaps
            public static Models.API.Undocumented.CSMaps.CSMapsResponse GetCSMaps()
            {
                return Internal.TwitchAPI.Undocumented.GetCSMaps();
            }

            public static async Task<Models.API.Undocumented.CSMaps.CSMapsResponse> GetCSMapsAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetCSMaps());
            }
            #endregion
            #region GetRecentMessages
            public static Models.API.Undocumented.RecentMessages.RecentMessagesResponse GetRecentMessages(string channelId)
            {
                return Internal.TwitchAPI.Undocumented.GetRecentMessages(channelId);
            }

            public static async Task<Models.API.Undocumented.RecentMessages.RecentMessagesResponse> GetRecentMessagesAsync(string channelId)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetRecentMessages(channelId));
            }
            #endregion
            #region GetChatters
            public static Models.API.Undocumented.Chatters.ChattersResponse GetChatters(string channelName)
            {
                return Internal.TwitchAPI.Undocumented.GetChatters(channelName);
            }

            public static async Task<Models.API.Undocumented.Chatters.ChattersResponse> GetChattersAsync(string channelName)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetChatters(channelName));
            }
            #endregion
        }

        /// <summary>These endpoints are offered by third party services (NOT TWITCH), but are still pretty cool.</summary>
        public static class ThirdParty
        {
            #region GetUsernameChanges
            public static List<Models.API.ThirdParty.UsernameChangeListing> GetUsernameChanges(string username)
            {
                return Internal.TwitchAPI.ThirdParty.GetUsernameChanges(username);
            }

            public async static Task<List<Models.API.ThirdParty.UsernameChangeListing>> GetUsernameChangesAsync(string username)
            {
                return await Task.Run(() => Internal.TwitchAPI.ThirdParty.GetUsernameChanges(username));
            }
            #endregion
        }

        /// <summary>These methods are intended to aid in developing the library.</summary>
        public static class Debugging
        {
            public static T BuildModel<T>(string data)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        /// <summary>Private methods that are used within the API.</summary>
        private static class APIHelpers
        {
            public static void ValidateScope(Enums.AuthScopes requiredScope, string accessToken = null)
            {
                if (accessToken != null)
                    return;
                if (!Internal.TwitchAPI.Shared.Scopes.Contains(requiredScope))
                    throw new Exceptions.API.InvalidCredentialException($"The call you attempted was blocked because you are missing required scope: {requiredScope.ToString().ToLower()}. You can ignore this protection by using TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = false . You can also generate a new token with the required scope here: https://twitchtokengenerator.com");
            }
        }
    }
}
