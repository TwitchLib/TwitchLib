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
            public static async Task<Models.API.v5.Badges.ChannelDisplayBadges> GetSubscriberBadgesForChannel(string channelId)
            {
                return await v5.Badges.GetSubscriberBadgesForChannel(channelId);
            }
            #endregion
            #region GetGlobalBadges
            public static async Task<Models.API.v5.Badges.GlobalBadgesResponse> GetGlobalBadges()
            {
                return await v5.Badges.GetGlobalBadges();
            }
            #endregion
        }

        public static class Bits
        {
            #region GetCheermotes
            public static async Task<Models.API.v5.Bits.Action[]> GetCheermotes(string channelId = null)
            {
                return await v5.Bits.GetCheermotes(channelId);
            }
            #endregion
        }

        public static class Blocks
        {
            #region GetBlocks
            public static async Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocks(string channel, int limit = 25, int offset = 0, string accessToken = null)
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
            public static async Task RemoveBlock(string channel, string target, string accessToken = null)
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
                public async static Task<Models.API.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPosts(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.GetMultipleFeedPosts(channelId, limit, cursor, comments, authToken);
                }
                #endregion
                #region GetFeedPosts
                public async static Task<Models.API.v5.ChannelFeed.FeedPost> GetFeedPost(string channelId, string postId, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.GetFeedPost(channelId, postId, comments, authToken);
                }
                #endregion
                #region CreateFeedPost
                public async static Task<Models.API.v5.ChannelFeed.FeedPostCreation> CreateFeedPost(string channelId, string content, bool? share = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateFeedPost(channelId, content, share, authToken);
                }
                #endregion
                #region DeleteFeedPost
                public async static Task<Models.API.v5.ChannelFeed.FeedPost> DeleteFeedPost(string channelId, string postId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedPost(channelId, postId, authToken);
                }
                #endregion
                #region CreateReactionToFeedPost
                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedPost(channelId, postId, emoteId, authToken);
                }
                #endregion
                #region DeleteReactionToFeedPost
                public static Task DeleteReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedPost(channelId, postId, emoteId, authToken);
                }
                #endregion
                #region GetFeedComments
                public async static Task<Models.API.v5.ChannelFeed.FeedPostComments> GetFeedComments(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.GetFeedComments(channelId, postId, limit, cursor, authToken);
                }
                #endregion
                #region CreateFeedComment
                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> CreateFeedComment(string channelId, string postId, string content, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateFeedComment(channelId, postId, content, authToken);
                }
                #endregion
                #region DeleteFeedComment
                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> DeleteFeedComment(string channelId, string postId, string commentId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedComment(channelId, postId, commentId, authToken);
                }
                #endregion
                #region CreateReactionToFeedComments
                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedComment(channelId, postId, commentId, emoteId, authToken);
                }
                #endregion
                #region DeleteReactionToFeedComments
                public async static Task DeleteReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    await Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedComment(channelId, postId, commentId, emoteId, authToken);
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
                public static async Task<Models.API.v3.Channels.Channel> GetChannel(string accessToken = null)
                {
                    return await Internal.TwitchAPI.v3.Channels.GetChannel(accessToken);
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
                /// [ASYNC] Gets a channel object based on a specified OAuth token.<para/>
                /// Get Channel returns more data than Get Channel by ID because Get Channel is privileged.<para/>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <returns>A ChannelPrivileged object including all Channel object info plus email and streamkey.</returns>
                public async static Task<Models.API.v5.Channels.ChannelAuthed> GetChannel(string authToken = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannel(authToken);
                }
                #endregion
                #region GetChannelById
                /// <summary>
                /// [ASYNC] Gets a speicified channel object.<para/>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A Channel object from the response of the Twitch API.</returns>
                public async static Task<Models.API.v5.Channels.Channel> GetChannelByID(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelByID(channelId);
                }
                #endregion
                #region UpdateChannel
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
                public async static Task<Models.API.v5.Channels.Channel> UpdateChannel(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Internal.TwitchAPI.v5.Channels.UpdateChannel(channelId, status, game, delay, channelFeedEnabled, authToken);
                }
                #endregion
                #region GetChannelEditors
                /// <summary>
                /// <para>[ASYNC] Gets a list of users who are editors for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A ChannelEditors object that contains an array of the Users which are Editor of the channel.</returns>
                public async static Task<Models.API.v5.Channels.ChannelEditors> GetChannelEditors(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, authToken);
                    return await Internal.TwitchAPI.v5.Channels.GetChannelEditors(channelId, authToken);
                }
                #endregion
                #region GetChannelFollowers
                /// <summary>
                /// <para>[ASYNC] Gets a list of users who follow a specified channel, sorted by the date when they started following the channel (newest first, unless specified otherwise).</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="cursor">Tells the server where to start fetching the next set of results, in a multi-page response.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <returns>A ChannelFollowers object that represents the response from the Twitch API.</returns>
                public async static Task<Models.API.v5.Channels.ChannelFollowers> GetChannelFollowers(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelFollowers(channelId, limit, offset, cursor, direction);
                }
                #endregion
                #region GetChannelTeams
                /// <summary>
                /// <para>[ASYNC] Gets a list of teams to which a specified channel belongs.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>An Array of the Teams the Channel belongs to.</returns>
                public async static Task<Models.API.v5.Channels.ChannelTeams> GetChannelTeams(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelTeams(channelId);
                }
                #endregion
                #region GetChannelSubscribers
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
                public async static Task<Models.API.v5.Channels.ChannelSubscribers> GetChannelSubscribers(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, authToken);
                    return await Internal.TwitchAPI.v5.Channels.GetChannelSubscribers(channelId, limit, offset, direction, authToken);
                }
                #endregion
                #region GetAllSubscribers
                /// <summary>
                /// [ASYNC] Makes a number of calls to get all subscriber objects belonging to a channel. THIS IS AN EXPENSIVE OPERATION.
                /// </summary>
                /// <param name="channelId">ChannelId indicating channel to get subs from.</param>
                /// <param name="accessToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public async static Task<List<Models.API.v5.Subscriptions.Subscription>> GetAllSubscribers(string channelId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v5.Channels.GetAllSubscribers(channelId, accessToken);
                }
                #endregion
                #region CheckChannelSubscriptionByUser
                /// <summary>
                /// <para>[ASYNC] Checks if a specified channel has a specified user subscribed to it. Intended for use by channel owners.</para>
                /// <para>Returns a subscription object which includes the user if that user is subscribed. Requires authentication for the channel.</para>
                /// <para>Required Authentication Scope: channel_check_subscription</para>
                /// </summary>
                /// <param name="channelId">The specified channel to check the subscription on.</param>
                /// <param name="userId">The specified user to check for.</param>
                /// <returns>Returns a subscription object or null if not subscribed.</returns>
                public async static Task<Models.API.v5.Subscriptions.Subscription> CheckChannelSubscriptionByUser(string channelId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, authToken);
                    return await Internal.TwitchAPI.v5.Channels.CheckChannelSubscriptionByUser(channelId, userId, authToken);
                }
                #endregion
                #region GetChannelVideos
                public async static Task<Models.API.v5.Channels.ChannelVideos> GetChannelVideos(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelVideos(channelId, limit, offset, broadcastType, language, sort);
                }
                #endregion
                #region StartChannelCommercial
                public async static Task<Models.API.v5.Channels.ChannelCommercial> StartChannelCommercial(string channelId, Enums.CommercialLength duration, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, authToken);
                    return await Internal.TwitchAPI.v5.Channels.StartChannelCommercial(channelId, duration, authToken);
                }
                #endregion
                #region ResetChannelStreamKey
                /// <summary>
                /// <para>[ASYNC] Deletes the stream key for a specified channel. Once it is deleted, the stream key is automatically reset.</para>
                /// <para>A stream key (also known as authorization key) uniquely identifies a stream. Each broadcast uses an RTMP URL that includes the stream key. Stream keys are assigned by Twitch.</para>
                /// <para>Required Authentication Scope: channel_stream</para>
                /// </summary>
                /// <param name="channelId">The specified channel to reset the StreamKey on.</param>
                /// <returns>A ChannelPrivileged object that also contains the email and stream key of the channel aside from the normal channel values.</returns>
                public async static Task<Models.API.v5.Channels.ChannelAuthed> ResetChannelStreamKey(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, authToken);
                    return await Internal.TwitchAPI.v5.Channels.ResetChannelStreamKey(channelId, authToken);
                }
                #endregion
                #region Communities
                #region GetChannelCommunity
                /// <summary>
                /// <para>[ASYNC] Gets the community for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel ID to get the community from.</param>
                /// <returns>A Community object that represents the community the channel is in.</returns>
                public async static Task<Models.API.v5.Communities.Community> GetChannelCommunity(string channelId, string authToken)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Internal.TwitchAPI.v5.Channels.GetChannelCommunity(channelId, authToken);
                }
                #endregion
                #region SetChannelCommunity
                /// <summary>
                /// <para>[ASYNC]Sets a specified channel to be in a specified community.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel to set the community for.</param>
                /// <param name="communityId">The specified community to set the channel to be a part of.</param>
                public async static Task SetChannelCommunity(string channelId, string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Channels.SetChannelCommunity(channelId, communityId, authToken);
                }
                #endregion
                #region DeleteChannelFromCommunity
                /// <summary>
                /// [ASYNC] Deletes a specified channel from its community.
                /// </summary>
                /// <param name="channelId">The specified channel to be removed.</param>
                public async static Task DeleteChannelFromCommunity(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Channels.DeleteChannelFromCommunity(channelId, authToken);
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
                public async static Task<Models.API.v5.Chat.ChannelBadges> GetChatBadgesByChannel(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Chat.GetChatBadgesByChannel(channelId);
                }
                #endregion
                #region GetChatEmoticonsBySet
                public async static Task<Models.API.v5.Chat.EmoteSet> GetChatEmoticonsBySet(List<int> emotesets = null)
                {
                    return await Internal.TwitchAPI.v5.Chat.GetChatEmoticonsBySet(emotesets);
                }
                #endregion
                #region GetAllChatEmoticons
                public async static Task<Models.API.v5.Chat.AllChatEmotes> GetAllChatEmoticons()
                {
                    return await Internal.TwitchAPI.v5.Chat.GetAllChatEmoticons();
                }
                #endregion
            }
        }

        public static class Collections
        {
            #region GetCollectionMetadata
            public async static Task<Models.API.v5.Collections.CollectionMetadata> GetCollectionMetadata(string collectionId)
            {
                return await v5.Collections.GetCollectionMetadata(collectionId);
            }
            #endregion
            #region GetCollection
            public async static Task<Models.API.v5.Collections.Collection> GetCollection(string collectionId, bool? includeAllItems = null)
            {
                return await v5.Collections.GetCollection(collectionId, includeAllItems);
            }
            #endregion
            #region GetCollectionsByChannel
            public async static Task<Models.API.v5.Collections.CollectionsByChannel> GetCollectionsByChannel(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                return await v5.Collections.GetCollectionsByChannel(channelId, limit, cursor, containingItem);
            }
            #endregion
            #region CreateCollection
            public async static Task<Models.API.v5.Collections.CollectionMetadata> CreateCollection(string channelId, string collectionTitle, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                return await v5.Collections.CreateCollection(channelId, collectionTitle, authToken);
            }
            #endregion
            #region UpdateCollection
            public async static Task UpdateCollection(string collectionId, string newCollectionTitle, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await v5.Collections.UpdateCollection(collectionId, newCollectionTitle, authToken);
            }
            #endregion
            #region CreateCollectionThumbnail
            public async static Task CreateCollectionThumbnail(string collectionId, string itemId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await v5.Collections.CreateCollectionThumbnail(collectionId, itemId, authToken);
            }
            #endregion
            #region DeleteCollection
            public async static Task DeleteCollection(string collectionId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await v5.Collections.DeleteCollection(collectionId, authToken);
            }
            #endregion
            #region AddItemToCollection
            public async static Task<Models.API.v5.Collections.CollectionItem> AddItemToCollection(string collectionId, string itemId, string itemType, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                return await v5.Collections.AddItemToCollection(collectionId, itemId, itemType, authToken);
            }
            #endregion
            #region DeleteItemFromCollection
            public async static Task DeleteItemFromCollection(string collectionId, string itemId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await v5.Collections.DeleteItemFromCollection(collectionId, itemId, authToken);
            }
            #endregion
            #region MoveItemWithinCollection
            public async static Task MoveItemWithinCollection(string collectionId, string itemId, int position, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                await v5.Collections.MoveItemWithinCollection(collectionId, itemId, position, authToken);
            }
            #endregion
        }

        public static class Communities
        {
            #region GetCommunityByName
            public async static Task<Models.API.v5.Communities.Community> GetCommunityByName(string communityName)
            {
                return await v5.Communities.GetCommunityByName(communityName);
            }
            #endregion
            #region GetCommunityByID
            public async static Task<Models.API.v5.Communities.Community> GetCommunityByID(string communityId)
            {
                return await v5.Communities.GetCommunityByID(communityId);
            }
            #endregion
            #region UpdateCommunity
            public async static Task UpdateCommunity(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.UpdateCommunity(communityId, summary, description, rules, email, authToken);
            }
            #endregion
            #region GetTopCommunities
            public async static Task<Models.API.v5.Communities.TopCommunities> GetTopCommunities(long? limit = null, string cursor = null)
            {
                return await v5.Communities.GetTopCommunities(limit, cursor);
            }
            #endregion
            #region GetCommunityBannedUsers
            public async static Task<Models.API.v5.Communities.BannedUsers> GetCommunityBannedUsers(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                return await v5.Communities.GetCommunityBannedUsers(communityId, limit, cursor, authToken);
            }
            #endregion
            #region BanCommunityUser
            public async static Task BanCommunityUser(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await v5.Communities.BanCommunityUser(communityId, userId, authToken);
            }
            #endregion
            #region UnBanCommunityUser
            public async static Task UnBanCommunityUser(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await v5.Communities.UnBanCommunityUser(communityId, userId, authToken);
            }
            #endregion
            #region CreateCommunityAvatarImage
            public async static Task CreateCommunityAvatarImage(string communityId, string avatarImage, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.CreateCommunityAvatarImage(communityId, avatarImage, authToken);
            }
            #endregion
            #region DeleteCommunityAvatarImage
            public async static Task DeleteCommunityAvatarImage(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.DeleteCommunityAvatarImage(communityId, authToken);
            }
            #endregion
            #region CreateCommunityCoverImage
            public async static Task CreateCommunityCoverImage(string communityId, string coverImage, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.CreateCommunityCoverImage(communityId, coverImage, authToken);
            }
            #endregion
            #region DeleteCommunityCoverImage
            public async static Task DeleteCommunityCoverImage(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.DeleteCommunityCoverImage(communityId, authToken);
            }
            #endregion
            #region GetCommunityModerators
            public async static Task<Models.API.v5.Communities.Moderators> GetCommunityModerators(string communityId, string authToken)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                return await v5.Communities.GetCommunityModerators(communityId, authToken);
            }
            #endregion
            #region AddCommunityModerator
            public async static Task AddCommunityModerator(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.AddCommunityModerator(communityId, userId, authToken);
            }
            #endregion
            #region DeleteCommunityModerator
            public async static Task DeleteCommunityModerator(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                await v5.Communities.DeleteCommunityModerator(communityId, userId, authToken);
            }
            #endregion
            #region GetCommunityPermissions
            public async static Task<Dictionary<string, bool>> GetCommunityPermissions(string communityId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Any, authToken);
                return await v5.Communities.GetCommunityPermissions(communityId, authToken);
            }
            #endregion
            #region ReportCommunityViolation
            public async static Task ReportCommunityViolation(string communityId, string channelId)
            {
                await v5.Communities.ReportCommunityViolation(communityId, channelId);
            }
            #endregion
            #region GetCommunityTimedOutUsers
            public async static Task<Models.API.v5.Communities.TimedOutUsers> GetCommunityTimedOutUsers(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                return await v5.Communities.GetCommunityTimedOutUsers(communityId, limit, cursor, authToken);
            }
            #endregion
            #region AddCommunityTimedOutUser
            public async static Task AddCommunityTimedOutUser(string communityId, string userId, int duration, string reason = null, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await v5.Communities.AddCommunityTimedOutUser(communityId, userId, duration, reason, authToken);
            }
            #endregion
            #region DeleteCommunityTimedOutUser
            public async static Task DeleteCommunityTimedOutUser(string communityId, string userId, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                await v5.Communities.DeleteCommunityTimedOutUser(communityId, userId, authToken);
            }
            #endregion
        }

        public static class Follows
        {
            #region GetFollowers
            public static async Task<Models.API.v3.Follows.FollowersResponse> GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
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
            public static async Task RemoveFollow(string user, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                await v3.Follows.RemoveFollow(user, target, accessToken);
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
                public async static Task<Models.API.v5.Games.TopGames> GetTopGames(int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Games.GetTopGames(limit, offset);
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
                public static async Task<Models.API.v5.Ingests.Ingests> GetIngestServerList()
                {
                    return await Internal.TwitchAPI.v5.Ingests.GetIngestServerList();
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
                public async static Task<Models.API.v5.Root.Root> GetRoot(string accessToken = null)
                {
                    return await Internal.TwitchAPI.v5.Root.GetRoot(accessToken);
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
                public async static Task<Models.API.v5.Search.SearchChannels> SearchChannels(string encodedSearchQuery, int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Search.SearchChannels(encodedSearchQuery, limit, offset);
                }
                #endregion
                #region SearchGames
                public async static Task<Models.API.v5.Search.SearchGames> SearchGames(string encodedSearchQuery, bool? live = null)
                {
                    return await Internal.TwitchAPI.v5.Search.SearchGames(encodedSearchQuery, live);
                }
                #endregion
                #region SearchStreams
                public async static Task<Models.API.v5.Search.SearchStreams> SearchStreams(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
                {
                    return await Internal.TwitchAPI.v5.Search.SearchStreams(encodedSearchQuery, limit, offset, hls);
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
                public async static Task<Models.API.v5.Streams.StreamByUser> GetStreamByUser(string channelId, string streamType = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetStreamByUser(channelId, streamType);
                }
                #endregion
                #region GetLiveStreams
                public async static Task<Models.API.v5.Streams.LiveStreams> GetLiveStreams(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetLiveStreams(channelList, game, language, streamType, limit, offset);
                }
                #endregion
                #region GetStreamsSummary
                public async static Task<Models.API.v5.Streams.StreamsSummary> GetStreamsSummary(string game = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetStreamsSummary(game);
                }
                #endregion
                #region GetFeaturedStreams
                public async static Task<Models.API.v5.Streams.FeaturedStreams> GetFeaturedStream(int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetFeaturedStreams(limit, offset);
                }
                #endregion
                #region GetFollowedStreams
                public async static Task<Models.API.v5.Streams.FollowedStreams> GetFollowedStreams(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Streams.GetFollowedStreams(streamType, limit, offset, authToken);
                }
                #endregion
                #region GetUptime
                public async static Task<TimeSpan?> GetUptime(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetUptime(channelId);
                }
                #endregion
                #region BroadcasterOnline
                public async static Task<bool> BroadcasterOnline(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Streams.BroadcasterOnline(channelId);
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
                return await v3.Subscriptions.GetAllSubscribers(channel, accessToken);
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
                public async static Task<Models.API.v5.Teams.AllTeams> GetAllTeams(int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Teams.GetAllTeams(limit, offset);
                }
                #endregion
                #region GetTeam
                public async static Task<Models.API.v5.Teams.Team> GetTeam(string teamName)
                {
                    return await Internal.TwitchAPI.v5.Teams.GetTeam(teamName);
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
                public async static Task<Models.API.v5.Users.Users> GetUsersByName(List<string> usernames)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUsersByName(usernames);
                }
                #endregion
                #region GetUser
                public async static Task<Models.API.v5.Users.UserAuthed> GetUser(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Users.GetUser(authToken);
                }
                #endregion
                #region GetUserByID
                public async static Task<Models.API.v5.Users.User> GetUserByID(string userId)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUserByID(userId);
                }
                #endregion
                #region GetUserByName
                public async static Task<Models.API.v5.Users.Users> GetUserByName(string username)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUserByName(username);
                }
                #endregion
                #region GetUserEmotes
                public async static Task<Models.API.v5.Users.UserEmotes> GetUserEmotes(string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return await Internal.TwitchAPI.v5.Users.GetUserEmotes(userId, authToken);
                }
                #endregion
                #region CheckUserSubscriptionByChannel
                public async static Task<Models.API.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannel(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return await Internal.TwitchAPI.v5.Users.CheckUserSubscriptionByChannel(userId, channelId, authToken);
                }
                #endregion
                #region GetUserFollows
                public async static Task<Models.API.v5.Users.UserFollows> GetUserFollows(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUserFollows(userId, limit, offset, direction, sortby);
                }
                #endregion
                #region CheckUserFollowsByChannel
                public async static Task<Models.API.v5.Users.UserFollow> CheckUserFollowsByChannel(string userId, string channelId)
                {
                    return await Internal.TwitchAPI.v5.Users.CheckUserFollowsByChannel(userId, channelId);
                }
                #endregion
                #region UserFollowsChannel
                public async static Task<bool> UserFollowsChannel(string userId, string channelId)
                {
                    return await Internal.TwitchAPI.v5.Users.UserFollowsChannel(userId, channelId);
                }
                #endregion
                #region FollowChannel
                public async static Task<Models.API.v5.Users.UserFollow> FollowChannel(string userId, string channelId, bool? notifications = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Users.FollowChannel(userId, channelId, notifications, authToken);
                }
                #endregion
                #region UnfollowChannel
                public async static Task UnfollowChannel(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    await Internal.TwitchAPI.v5.Users.UnfollowChannel(userId, channelId, authToken);
                }
                #endregion
                #region GetUserBlockList
                public async static Task<Models.API.v5.Users.UserBlocks> GetUserBlockList(string userId, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, authToken);
                    return await Internal.TwitchAPI.v5.Users.GetUserBlockList(userId, limit, offset, authToken);
                }
                #endregion
                #region BlockUser
                public async static Task<Models.API.v5.Users.UserBlock> BlockUser(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Users.BlockUser(sourceUserId, targetUserId, authToken);
                }
                #endregion
                #region UnblockUser
                public async static Task UnblockUser(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    await Internal.TwitchAPI.v5.Users.UnblockUser(sourceUserId, targetUserId, authToken);
                }
                #endregion
                #region ViewerHeartbeatService
                #region CreateUserConnectionToViewerHeartbeatService
                public async static Task CreateUserConnectionToViewerHeartbeatService(string identifier, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    await Internal.TwitchAPI.v5.Users.CreateUserConnectionToViewerHeartbeatService(identifier, authToken);
                }
                #endregion
                #region CheckUserConnectionToViewerHeartbeatService
                public async static Task<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatService(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Users.CheckUserConnectionToViewerHeartbeatService(authToken);
                }
                #endregion
                #region DeleteUserConnectionToViewerHeartbeatService

                public async static Task DeleteUserConnectionToViewerHeartbeatServicechStreams(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    await Internal.TwitchAPI.v5.Users.DeleteUserConnectionToViewerHeartbeatServicechStreams(authToken);
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
                public async static Task<Models.API.v5.Videos.Video> GetVideo(string videoId)
                {
                    return await Internal.TwitchAPI.v5.Videos.GetVideo(videoId);
                }
                #endregion
                #region GetTopVideos
                public async static Task<Models.API.v5.Videos.TopVideos> GetTopVideos(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return await Internal.TwitchAPI.v5.Videos.GetTopVideos(limit, offset, game, period, broadcastType, language, sort);
                }
                #endregion
                #region GetFollowedVideos
                public async static Task<Models.API.v5.Videos.FollowedVideos> GetFollowedVideos(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Videos.GetFollowedVideos(limit, offset, broadcastType, language, sort, authToken);
                }
                #endregion
                #region UploadVideo
                public async static Task<Models.API.v5.UploadVideo.UploadedVideo> UploadVideo(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                    return await Internal.TwitchAPI.v5.Videos.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken);
                }
                #endregion
                #region UpdateVideo
                public async static Task<Models.API.v5.Videos.Video> UpdateVideo(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Internal.TwitchAPI.v5.Videos.UpdateVideo(videoId, description, game, language, tagList, title, authToken);
                }
                #endregion
                #region DeleteVideo
                public async static Task DeleteVideo(string videoId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Videos.DeleteVideo(videoId, authToken);
                }
                #endregion
            }
        }

        public static class Clips
        {
            #region GetClip
            public static async Task<Models.API.v5.Clips.Clip> GetClip(string slug)
            {
                return await v5.GetClip(slug);
            }
            #endregion
            #region GetTopClips
            public static async Task<Models.API.v5.Clips.TopClipsResponse> GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v5.Clips.Period period = Models.API.v5.Clips.Period.Week, bool trending = false)
            {
                return await v5.GetTopClips(channel, cursor, game, limit, period, trending);
            }
            #endregion
            #region GetFollowedClips
            public static async Task<Models.API.v5.Clips.FollowClipsResponse> GetFollowedClips(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                return await v5.GetFollowedClips(limit, cursor, trending, authToken);
            }
            #endregion
        }

        /// <summary>These endpoints are pretty cool, but they may stop working at anytime due to changes Twitch makes.</summary>
        public static class Undocumented
        {
            #region GetClipChat
            public static async Task<Models.API.Undocumented.ClipChat.GetClipChatResponse> GetClipChat(string slug)
            {
                return await Internal.TwitchAPI.Undocumented.GetClipChat(slug);
            }
            #endregion
            #region GetTwitchPrimeOffers
            public static async Task<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers> GetTwitchPrimeOffers()
            {
                return await Internal.TwitchAPI.Undocumented.GetTwitchPrimeOffers();
            }
            #endregion
            #region GetChannelHosts
            public static async Task<Models.API.Undocumented.Hosting.ChannelHostsResponse> GetChannelHosts(string channelId)
            {
                return await Internal.TwitchAPI.Undocumented.GetChannelHosts(channelId);
            }
            #endregion
            #region GetChatProperties
            public static async Task<Models.API.Undocumented.ChatProperties.ChatProperties> GetChatProperties(string channelName)
            {
                return await Internal.TwitchAPI.Undocumented.GetChatProperties(channelName);
            }
            #endregion
            #region GetChannelPanels
            public static async Task<Models.API.Undocumented.ChannelPanels.Panel[]> GetChannelPanels(string channelName)
            {
                return await Internal.TwitchAPI.Undocumented.GetChannelPanels(channelName);
            }
            #endregion
            #region GetCSMaps
            public static async Task<Models.API.Undocumented.CSMaps.CSMapsResponse> GetCSMaps()
            {
                return await Internal.TwitchAPI.Undocumented.GetCSMaps();
            }
            #endregion
            #region GetRecentMessages
            public static async Task<Models.API.Undocumented.RecentMessages.RecentMessagesResponse> GetRecentMessages(string channelId)
            {
                return await Internal.TwitchAPI.Undocumented.GetRecentMessages(channelId);
            }
            #endregion
            #region GetChatters
            public static async Task<Models.API.Undocumented.Chatters.ChattersResponse> GetChatters(string channelName)
            {
                return await Internal.TwitchAPI.Undocumented.GetChatters(channelName);
            }
            #endregion

            #region GetRecentChannelEvents
            public async static Task<Models.API.Undocumented.RecentEvents.RecentEvents> GetRecentChannelEvents(string channelId)
            {
                return await Internal.TwitchAPI.Undocumented.GetRecentChannelEvents(channelId);
            }
            #endregion
        }

        /// <summary>These endpoints are offered by third party services (NOT TWITCH), but are still pretty cool.</summary>
        public static class ThirdParty
        {
            #region GetUsernameChanges
            public async static Task<List<Models.API.ThirdParty.UsernameChangeListing>> GetUsernameChanges(string username)
            {
                return await Internal.TwitchAPI.ThirdParty.GetUsernameChanges(username);
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
