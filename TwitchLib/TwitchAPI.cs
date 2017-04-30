namespace TwitchLib
{
    #region using directives
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
                return Internal.TwitchAPI.v5.Bits.GetCheermotes(channelId);
            }
            #endregion
        }

        public static class Blocks
        {
            #region GetBlocks
            public static Models.API.v3.Blocks.GetBlocksResponse GetBlocks(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset, accessToken);
            }

            public static async Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocksAsync(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset, accessToken));
            }
            #endregion
            #region CreateBlock
            public static Models.API.v3.Blocks.Block CreateBlock(string channel, string target, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target, accessToken);
            }

            public static async Task<Models.API.v3.Blocks.Block> CreateBlockAsync(string channel, string target, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target, accessToken));
            }
            #endregion
            #region RemoveBlock
            public static void RemoveBlock(string channel, string target, string accessToken = null)
            {
                Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target, accessToken);
            }

            public static async void RemoveBlockAsync(string channel, string target, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target, accessToken));
            }
            #endregion
        }

        public static class ChannelFeeds
        {
            public static class v3
            {
                #region GetChannelFeedPosts
                public static Models.API.v3.ChannelFeeds.ChannelFeedResponse GetChannelFeedPosts(string channel, int limit = 25, string cursor = null)
                {
                    return Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor);
                }

                public async static Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPostsAsync(string channel, int limit = 25, string cursor = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor));
                }
                #endregion
                #region CreatePost
                public static Models.API.v3.ChannelFeeds.PostResponse CreatePost(string channel, string content, bool share = false, string accessToken = null)
                {
                    return Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share, accessToken);
                }

                public static async Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePostAsync(string channel, string content, bool share = false, string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share, accessToken));
                }
                #endregion
                #region GetPostById
                public static Models.API.v3.ChannelFeeds.Post GetPostById(string channel, string postId)
                {
                    return Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId);
                }

                public static async Task<Models.API.v3.ChannelFeeds.Post> GetPostByIdAsync(string channel, string postId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId));
                }
                #endregion
                #region RemovePost
                public static void RemovePost(string channel, string postId, string accessToken = null)
                {
                    Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId, accessToken);
                }

                public static async Task RemovePostAsync(string channel, string postId, string accessToken = null)
                {
                    await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId, accessToken));
                }
                #endregion
                #region CreateReaction
                public static void CreateReaction(string channel, string postId, string emoteId, string accessToken = null)
                {
                    Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId, accessToken);
                }

                public static async Task CreateReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
                {
                    await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId, accessToken));
                }
                #endregion
                #region RemoveReaction
                public static void RemoveReaction(string channel, string postId, string emoteId, string accessToken = null)
                {
                    Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId, accessToken);
                }

                public static async Task RemoveReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
                {
                    await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId, accessToken));
                }
                #endregion
            }
            
            public static class v5
            {
                #region GetMultipleFeedPosts
                public static Models.API.v5.ChannelFeed.MultipleFeedPosts GetMultipleFeedPosts(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.GetMultipleFeedPosts(channelId, limit, cursor, comments, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.GetMultipleFeedPosts(channelId, limit, cursor, comments, authToken));
                }
                #endregion
                #region GetFeedPosts
                public static Models.API.v5.ChannelFeed.FeedPost GetFeedPost(string channelId, string postId, long? comments = null, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.GetFeedPost(channelId, postId, comments, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.GetFeedPost(channelId, postId, comments, authToken));
                }
                #endregion
                #region CreateFeedPost
                public static Models.API.v5.ChannelFeed.FeedPostCreation CreateFeedPost(string channelId, string content, bool? share = null, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateFeedPost(channelId, content, share, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateFeedPost(channelId, content, share, authToken));
                }
                #endregion
                #region DeleteFeedPost
                public static Models.API.v5.ChannelFeed.FeedPost DeleteFeedPost(string channelId, string postId, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedPost(channelId, postId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedPost(channelId, postId, authToken));
                }
                #endregion
                #region CreateReactionToFeedPost
                public static Models.API.v5.ChannelFeed.FeedPostReactionPost CreateReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedPost(channelId, postId, emoteId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedPost(channelId, postId, emoteId, authToken));
                }
                #endregion
                #region DeleteReactionToFeedPost
                public static void DeleteReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedPost(channelId, postId, emoteId, authToken);
                }

                public static Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
                {
                    return Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedPost(channelId, postId, emoteId, authToken));
                }
                #endregion
                #region GetFeedComments
                public static Models.API.v5.ChannelFeed.FeedPostComments GetFeedComments(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.GetFeedComments(channelId, postId, limit, cursor, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.GetFeedComments(channelId, postId, limit, cursor, authToken));
                }
                #endregion
                #region CreateFeedComment
                public static Models.API.v5.ChannelFeed.FeedPostComment CreateFeedComment(string channelId, string postId, string content, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateFeedComment(channelId, postId, content, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateFeedComment(channelId, postId, content, authToken));
                }
                #endregion
                #region DeleteFeedComment
                public static Models.API.v5.ChannelFeed.FeedPostComment DeleteFeedComment(string channelId, string postId, string commentId, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedComment(channelId, postId, commentId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedComment(channelId, postId, commentId, authToken));
                }
                #endregion
                #region CreateReactionToFeedComments
                public static Models.API.v5.ChannelFeed.FeedPostReactionPost CreateReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    return Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedComment(channelId, postId, commentId, emoteId, authToken);
                }

                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedComment(channelId, postId, commentId, emoteId, authToken));
                }
                #endregion
                #region DeleteReactionToFeedComments
                public static void DeleteReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
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
                public static Models.API.v3.Channels.Channel GetChannelByName(string channel)
                {
                    return Internal.TwitchAPI.v3.Channels.GetChannelByName(channel);
                }

                public static async Task<Models.API.v3.Channels.Channel> GetChannelByNameAsync(string channel)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannelByName(channel));
                }
                #endregion
                #region GetChannel
                public static Models.API.v3.Channels.Channel GetChannel()
                {
                    return Internal.TwitchAPI.v3.Channels.GetChannel();
                }

                public static async Task<Models.API.v3.Channels.Channel> GetChannelAsync()
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannel());
                }
                #endregion
                #region GetChannelEditors
                public static Models.API.v3.Channels.GetEditorsResponse GetChannelEditors(string channel, string accessToken = null)
                {
                    return Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel, accessToken);
                }

                public static async Task<Models.API.v3.Channels.GetEditorsResponse> GetChannelEditorsAsync(string channel, string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel, accessToken));
                }
                #endregion
                #region UpdateChannel
                public static Models.API.v3.Channels.Channel UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null, string accessToken = null)
                {
                    return Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed, accessToken);
                }

                public static async Task<Models.API.v3.Channels.Channel> UpdateChannelAsync(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null, string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed, accessToken));
                }
                #endregion
                #region ResetStreamKey
                public static Models.API.v3.Channels.ResetStreamKeyResponse ResetStreamKey(string channel, string accessToken = null)
                {
                    return Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel, accessToken);
                }

                public static async Task<Models.API.v3.Channels.ResetStreamKeyResponse> ResetStreamKeyAsync(string channel, string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel, accessToken));
                }
                #endregion
                #region RunCommercial
                public static void RunCommercial(string channel, Enums.CommercialLength length, string accessToken = null)
                {
                    Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length, accessToken);
                }

                public static async Task RunCommercialAsync(string channel, Enums.CommercialLength length, string accessToken = null)
                {
                    await Task.Run(() => Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length, accessToken));
                }
                #endregion
                #region GetTeams
                public static Models.API.v3.Channels.GetTeamsResponse GetTeams(string channel)
                {
                    return Internal.TwitchAPI.v3.Channels.GetTeams(channel);
                }

                public static async Task<Models.API.v3.Channels.GetTeamsResponse> GetTeamsAsync(string channel)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetTeams(channel));
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
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelSubscribers(channelId, limit, offset, direction, authToken));
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
                    return Internal.TwitchAPI.v5.Channels.StartChannelCommercial(channelId, duration, authToken);
                }

                public async static Task<Models.API.v5.Channels.ChannelCommercial> StartChannelCommercialAsync(string channelId, Enums.CommercialLength duration, string authToken = null)
                {
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
                public static Models.API.v5.Communities.Community GetChannelCommunity(string channelId)
                {
                    return Internal.TwitchAPI.v5.Channels.GetChannelCommunity(channelId);
                }

                /// <summary>
                /// <para>[ASYNC] Gets the community for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel ID to get the community from.</param>
                /// <returns>A Community object that represents the community the channel is in.</returns>
                public async static Task<Models.API.v5.Communities.Community> GetChannelCommunityAsync(string channelId)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Channels.GetChannelCommunity(channelId));
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
                    Internal.TwitchAPI.v5.Channels.DeleteChannelFromCommunity(channelId, authToken);
                }

                /// <summary>
                /// [ASYNC] Deletes a specified channel from its community.
                /// </summary>
                /// <param name="channelId">The specified channel to be removed.</param>
                public async static Task DeleteChannelFromCommunityAsync(string channelId, string authToken = null)
                {
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
                public static Models.API.v3.Chat.BadgesResponse GetBadges(string channel)
                {
                    return Internal.TwitchAPI.v3.Chat.GetBadges(channel);
                }

                public static async Task<Models.API.v3.Chat.BadgesResponse> GetBadgesAsync(string channel)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetBadges(channel));
                }
                #endregion
                #region GetAllEmoticons
                public static Models.API.v3.Chat.AllEmoticonsResponse GetAllEmoticons()
                {
                    return Internal.TwitchAPI.v3.Chat.GetAllEmoticons();
                }

                public static async Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticonsAsync()
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetAllEmoticons());
                }
                #endregion
                #region GetEmoticonsBySets
                public static Models.API.v3.Chat.EmoticonSetsResponse GetEmoticonsBySets(List<int> emotesets)
                {
                    return Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets);
                }

                public static async Task<Models.API.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySetsAsync(List<int> emotesets)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets));
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

            #endregion
            #region GetCollectionsByChannel

            #endregion
            #region CreateCollection

            #endregion
            #region UpdateCollection

            #endregion
            #region CreateCollectionThumbnail

            #endregion
            #region DeleteCollection

            #endregion
            #region AddItemToCollection

            #endregion
            #region DeleteItemFromCollection

            #endregion
            #region MoveItemWithinCollection

            #endregion
        }

        //TODO: Finish cleaning up and importing v5 endpoints below vvv

        public static class Follows
        {
            public static Models.API.v3.Follows.FollowersResponse GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollowers(channel, limit, offset, cursor, direction);
            }

            public static async Task<Models.API.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollowers(channel, limit, offset, cursor, direction));
            }

            public static Models.API.v3.Follows.FollowsResponse GetFollows(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollows(channel, limit, offset, direction, sortBy);
            }

            public static async Task<Models.API.v3.Follows.FollowsResponse> GetFollowsAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollows(channel, limit, offset, direction, sortBy));
            }

            public static Models.API.v3.Follows.Follows GetFollowsStatus(string user, string targetChannel)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollowsStatus(user, targetChannel);
            }

            public static async Task<Models.API.v3.Follows.Follows> GetFollowsStatusAsync(string user, string targetChannel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollowsStatus(user, targetChannel));
            }

            public static Models.API.v3.Follows.Follows CreateFollow(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Follows.CreateFollow(user, targetChannel, notifications, accessToken);
            }

            public static async Task<Models.API.v3.Follows.Follows> CreateFollowAsync(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.CreateFollow(user, targetChannel, notifications, accessToken));
            }

            public static void RemoveFollow(string user, string target, string accessToken = null)
            {
                Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target, accessToken);
            }

            public static async Task RemoveFollowAsync(string user, string target, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target, accessToken));
            }
        }

        public static class Games
        {
            public static Models.API.v3.Games.TopGamesResponse GetTopGames(int limit = 10, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Games.GetTopGames(limit, offset);
            }

            public async static Task<Models.API.v3.Games.TopGamesResponse> GetTopGamesAsync(int limit = 10, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Games.GetTopGames(limit, offset));
            }
        }

        public static class Ingests
        {
            public static Models.API.v3.Ingests.IngestsResponse GetIngests()
            {
                return Internal.TwitchAPI.v3.Ingests.GetIngests();
            }

            public async static Task<Models.API.v3.Ingests.IngestsResponse> GetIngestsAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Ingests.GetIngests());
            }
        }

        public static class Root
        {
            public static class v3
            {
                public static Models.API.v3.Root.RootResponse GetRoot(string accessToken = null)
                {
                    return Internal.TwitchAPI.v3.Root.GetRoot(accessToken);
                }

                public async static Task<Models.API.v3.Root.RootResponse> GetRootAsync(string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v3.Root.GetRoot(accessToken));
                }
            }

            public static class v5
            {
                public static Models.API.v5.Root.Root GetRoot(string accessToken = null)
                {
                    return Internal.TwitchAPI.v5.Root.GetRoot(accessToken);
                }

                public async static Task<Models.API.v5.Root.Root> GetRootAsync(string accessToken = null)
                {
                    return await Task.Run(() => Internal.TwitchAPI.v5.Root.GetRoot(accessToken));
                }
            }
            
        }

        public static class Search
        {
            public static Models.API.v3.Search.SearchChannelsResponse SearchChannels(string query, int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Search.SearchChannels(query, limit, offset);
            }

            public async static Task<Models.API.v3.Search.SearchChannelsResponse> SearchChannelsAsync(string query, int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Search.SearchChannels(query, limit, offset));
            }

            public static Models.API.v3.Search.SearchStreamsResponse SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                return Internal.TwitchAPI.v3.Search.SearchStreams(query, limit, offset, hls);
            }

            public async static Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreamsAsync(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Search.SearchStreams(query, limit, offset, hls));
            }

            public static Models.API.v3.Search.SearchGamesResponse SearchGames(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
            {
                return Internal.TwitchAPI.v3.Search.SearchGames(query, type, live);
            }

            public async static Task<Models.API.v3.Search.SearchGamesResponse> SearchGamesAsync(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Search.SearchGames(query, type, live));
            }
        }

        public static class Streams
        {
            public static Models.API.v3.Streams.StreamResponse GetStream(string channel)
            {
                return Internal.TwitchAPI.v3.Streams.GetStream(channel);
            }

            public async static Task<Models.API.v3.Streams.StreamResponse> GetStreamAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetStream(channel));
            }

            public static Models.API.v3.Streams.StreamsResponse GetStreams(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
            {
                return Internal.TwitchAPI.v3.Streams.GetStreams(game, channel, limit, offset, clientId, streamType, language);
            }

            public async static Task<Models.API.v3.Streams.StreamsResponse> GetStreamsAsync(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetStreams(game, channel, limit, offset, clientId, streamType, language));
            }

            public static Models.API.v3.Streams.FeaturedStreamsResponse GetFeaturedStreams(int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Streams.GetFeaturedStreams(limit, offset);
            }

            public async static Task<Models.API.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreamsAsync(int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetFeaturedStreams(limit, offset));
            }

            public static Models.API.v3.Streams.Summary GetStreamsSummary()
            {
                return Internal.TwitchAPI.v3.Streams.GetStreamsSummary();
            }

            public async static Task<Models.API.v3.Streams.Summary> GetStreamsSummaryAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetStreamsSummary());
            }
        }

        public static class Subscriptions
        {
            public static Models.API.v3.Subscriptions.SubscribersResponse GetSubscribers(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.GetSubscribers(channel, limit, offset, direction, accessToken);
            }

            public async static Task<Models.API.v3.Subscriptions.SubscribersResponse> GetSubscribersAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetSubscribers(channel, limit, offset, direction, accessToken));
            }

            public static List<Models.API.v3.Subscriptions.Subscriber> GetAllSubscribers(string channel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.GetAllSubscribers(channel, accessToken);
            }

            public async static Task<List<Models.API.v3.Subscriptions.Subscriber>> GetAllSubscribersAsync(string channel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetAllSubscribers(channel, accessToken));
            }

            public static Models.API.v3.Subscriptions.Subscriber ChannelHasUserSubscribed(string channel, string targetUser, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.ChannelHasUserSubscribed(channel, targetUser, accessToken);
            }

            public async static Task<Models.API.v3.Subscriptions.Subscriber> ChannelHasUserSubscribedAsync(string channel, string targetUser, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.ChannelHasUserSubscribed(channel, targetUser, accessToken));
            }

            public static Models.API.v3.Subscriptions.ChannelSubscription UserSubscribedToChannel(string user, string targetChannel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.UserSubscribedToChannel(user, targetChannel, accessToken);
            }

            public async static Task<Models.API.v3.Subscriptions.ChannelSubscription> UserSubscribedToChannelAsync(string user, string targetChannel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.UserSubscribedToChannel(user, targetChannel, accessToken));
            }

            public static int GetSubscriberCount(string channel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.GetSubscriberCount(channel, accessToken);
            }

            public async static Task<int> GetSubscriberCountAsync(string channel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetSubscriberCount(channel, accessToken));
            }
        }

        public static class Teams
        {
            public static Models.API.v3.Teams.GetTeamsResponse GetTeams(int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Teams.GetTeams(limit, offset);
            }

            public async static Task<Models.API.v3.Teams.GetTeamsResponse> GetTeamsAsync(int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Teams.GetTeams(limit, offset));
            }

            public static Models.API.v3.Teams.Team GetTeam(string teamName)
            {
                return Internal.TwitchAPI.v3.Teams.GetTeam(teamName);
            }

            public async static Task<Models.API.v3.Teams.Team> GetTeamAsync(string teamName)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Teams.GetTeam(teamName));
            }
        }

        public static class User
        {
            public static Models.API.v3.Users.User GetUserFromUsername(string username)
            {
                return Internal.TwitchAPI.v3.User.GetUserFromUsername(username);
            }

            public async static Task<Models.API.v3.Users.User> GetUserFromUsernameAsync(string username)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetUserFromUsername(username));
            }

            public static Models.API.v3.Users.UserEmotesResponse GetEmotes(string username, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetEmotes(username, accessToken);
            }

            public async static Task<Models.API.v3.Users.UserEmotesResponse> GetEmotesAsync(string username, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetEmotes(username, accessToken));
            }

            public static Models.API.v3.Users.FullUser GetUserFromToken(string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetUserFromToken(accessToken);
            }

            public async static Task<Models.API.v3.Users.FullUser> GetUserFromTokenAsync(string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetUserFromToken(accessToken));
            }

            public static Models.API.v3.Users.FollowedStreamsResponse GetFollowedStreams(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetFollowedStreams(limit, offset, type, accessToken);
            }

            public async static Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreamsAsync(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetFollowedStreams(limit, offset, type, accessToken));
            }

            public static Models.API.v3.Users.FollowedVideosResponse GetFollowedVideos(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetFollowedVideos(limit, offset, broadcastType, accessToken);
            }

            public async static Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideosAsync(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetFollowedVideos(limit, offset, broadcastType, accessToken));
            }
        }

        public static class Videos
        {
            public static Models.API.v3.Videos.Video GetVideo(string id)
            {
                return Internal.TwitchAPI.v3.Videos.GetVideo(id);
            }

            public async static Task<Models.API.v3.Videos.Video> GetVideoAsync(string id)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Videos.GetVideo(id));
            }

            public static Models.API.v3.Videos.TopVideosResponse GetTopVideos(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
            {
                return Internal.TwitchAPI.v3.Videos.GetTopVideos(limit, offset, game, period);
            }

            public async static Task<Models.API.v3.Videos.TopVideosResponse> GetTopVideosAsync(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Videos.GetTopVideos(limit, offset, game, period));
            }

            public static Models.API.v4.UploadVideo.UploadedVideo UploadVideo(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
            {
                return Internal.TwitchAPI.v4.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken);
            }

            public async static Task<Models.API.v4.UploadVideo.UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken));
            }
        }

        public static class Clips
        {
            public static Models.API.v4.Clips.Clip GetClip(string slug)
            {
                return Internal.TwitchAPI.v4.GetClip(slug);
            }

            public static async Task<Models.API.v4.Clips.Clip> GetClipAsync(string slug)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.GetClip(slug));
            }

            public static Models.API.v4.Clips.TopClipsResponse GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
            {
                return Internal.TwitchAPI.v4.GetTopClips(channel, cursor, game, limit, period, trending);
            }

            public static async Task<Models.API.v4.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.GetTopClips(channel, cursor, game, limit, period, trending));
            }

            public static Models.API.v4.Clips.FollowClipsResponse GetFollowedClips(long limit = 10, string cursor = null, bool trending = false)
            {
                return Internal.TwitchAPI.v4.GetFollowedClips(limit, cursor, trending);
            }

            public static async Task<Models.API.v4.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.GetFollowedClips(limit, cursor, trending));
            }
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
            public static Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffersResponse GetTwitchPrimeOffers()
            {
                return Internal.TwitchAPI.Undocumented.GetTwitchPrimeOffers();
            }

            public static async Task<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffersResponse> GetTwitchPrimeOffersAsync()
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
