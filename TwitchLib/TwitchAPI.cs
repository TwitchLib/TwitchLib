using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitchLib
{
    /// <summary>
    /// Fully featured Twitch API wrapper.
    /// </summary>
    public static class TwitchAPI
    {
        public static class Settings
        {
            public static string ClientId { get { return Internal.TwitchAPI.Shared.ClientId; } set { Internal.TwitchAPI.Shared.ClientId = value; } }
            public static string AccessToken { get { return Internal.TwitchAPI.Shared.AccessToken; } set { Internal.TwitchAPI.Shared.AccessToken = value; } }
        }

        public static class Blocks
        {
            public static Models.API.v3.Blocks.GetBlocksResponse GetBlocks(string channel, int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset);
            }

            public static async Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocksAsync(string channel, int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset));
            }

            public static Models.API.v3.Blocks.Block CreateBlock(string channel, string target)
            {
                return Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target);
            }

            public static async Task<Models.API.v3.Blocks.Block> CreateBlockAsync(string channel, string target)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target));
            }

            public static void RemoveBlock(string channel, string target)
            {
                Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target);
            }

            public static async void RemoveBlockAsync(string channel, string target)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target));
            }
        }

        public static class ChannelFeeds
        {
            public static Models.API.v3.ChannelFeeds.ChannelFeedResponse GetChannelFeedPosts(string channel, int limit = 25, string cursor = null)
            {
                return Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor);
            }

            public async static Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPostsAsync(string channel, int limit = 25, string cursor = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor));
            }

            public static Models.API.v3.ChannelFeeds.PostResponse CreatePost(string channel, string content, bool share = false)
            {
                return Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share);
            }

            public static async Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePostAsync(string channel, string content, bool share = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share));
            }

            public static Models.API.v3.ChannelFeeds.Post GetPostById(string channel, string postId)
            {
                return Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId);
            }

            public static async Task<Models.API.v3.ChannelFeeds.Post> GetPostByIdAsync(string channel, string postId)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId));
            }

            public static void RemovePost(string channel, string postId)
            {
                Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId);
            }

            public static async Task RemovePostAsync(string channel, string postId)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId));
            }

            public static void CreateReaction(string channel, string postId, string emoteId)
            {
                Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId);
            }

            public static async Task CreateReactionAsync(string channel, string postId, string emoteId)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId));
            }

            public static void RemoveReaction(string channel, string postId, string emoteId)
            {
                Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId);
            }

            public static async Task RemoveReactionAsync(string channel, string postId, string emoteId)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId));
            }
        }

        public static class Channels
        {
            public static Models.API.v3.Channels.Channel GetChannelByName(string channel)
            {
                return Internal.TwitchAPI.v3.Channels.GetChannelByName(channel);
            }

            public static async Task<Models.API.v3.Channels.Channel> GetChannelByNameAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannelByName(channel));
            }

            public static Models.API.v3.Channels.Channel GetChannel()
            {
                return Internal.TwitchAPI.v3.Channels.GetChannel();
            }

            public static async Task<Models.API.v3.Channels.Channel> GetChannelAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannel());
            }

            public static Models.API.v3.Channels.GetEditorsResponse GetChannelEditors(string channel)
            {
                return Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel);
            }

            public static async Task<Models.API.v3.Channels.GetEditorsResponse> GetChannelEditorsAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel));
            }

            public static Models.API.v3.Channels.Channel UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null)
            {
                return Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed);
            }

            public static async Task<Models.API.v3.Channels.Channel> UpdateChannelAsync(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed));
            }

            public static Models.API.v3.Channels.ResetStreamKeyResponse ResetStreamKey(string channel)
            {
                return Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel);
            }

            public static async Task<Models.API.v3.Channels.ResetStreamKeyResponse> ResetStreamKeyAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel));
            }

            public static void RunCommercial(string channel, Enums.CommercialLength length)
            {
                Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length);
            }

            public static async Task RunCommercialAsync(string channel, Enums.CommercialLength length)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length));
            }

            public static Models.API.v3.Channels.GetTeamsResponse GetTeams(string channel)
            {
                return Internal.TwitchAPI.v3.Channels.GetTeams(channel);
            }

            public static async Task<Models.API.v3.Channels.GetTeamsResponse> GetTeamsAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetTeams(channel));
            }
        }

        public static class Chat
        {
            public static Models.API.v3.Chat.BadgesResponse GetBadges(string channel)
            {
                return Internal.TwitchAPI.v3.Chat.GetBadges(channel);
            }

            public static async Task<Models.API.v3.Chat.BadgesResponse> GetBadgesAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetBadges(channel));
            }

            public static Models.API.v3.Chat.AllEmoticonsResponse GetAllEmoticons()
            {
                return Internal.TwitchAPI.v3.Chat.GetAllEmoticons();
            }

            public static async Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticonsAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetAllEmoticons());
            }

            public static Models.API.v3.Chat.SetEmoticonsResponse GetEmoticonsBySets(List<int> emotesets)
            {
                return Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets);
            }

            public static async Task<Models.API.v3.Chat.SetEmoticonsResponse> GetEmoticonsBySetsAsync(List<int> emotesets)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets));
            }
        }

        public static class Follows
        {
            public static Models.API.v3.Follows.FollowersResponse GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = null, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollowers(channel, limit, offset, cursor, direction);
            }

            public static async Task<Models.API.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollowers(channel, limit, offset, cursor, direction));
            }

            public static Models.API.v3.Follows.FollowsResponse GetFollows(string channel, int limit = 25, int offset = 0, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending, Models.API.v3.Follows.SortBy sortBy = Models.API.v3.Follows.SortBy.CreatedAt)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollows(channel, limit, offset, direction, sortBy);
            }

            public static async Task<Models.API.v3.Follows.FollowsResponse> GetFollowsAsync(string channel, int limit = 25, int offset = 0, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending, Models.API.v3.Follows.SortBy sortBy = Models.API.v3.Follows.SortBy.CreatedAt)
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

            public static Models.API.v3.Follows.Follows CreateFollow(string user, string targetChannel, bool notifications = false)
            {
                return Internal.TwitchAPI.v3.Follows.CreateFollow(user, targetChannel, notifications);
            }

            public static async Task<Models.API.v3.Follows.Follows> CreateFollowAsync(string user, string targetChannel, bool notifications = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.CreateFollow(user, targetChannel, notifications));
            }

            public static void RemoveFollow(string user, string target)
            {
                Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target);
            }

            public static async Task RemoveFollowAsync(string user, string target)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target));
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
            public static Models.API.v3.Root.RootResponse GetRoot()
            {
                return Internal.TwitchAPI.v3.Root.GetRoot();
            }

            public async static Task<Models.API.v3.Root.RootResponse> GetRootAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Root.GetRoot());
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
    }
}
