using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    internal static class v3
    {
        public static class Blocks
        {
            public static List<Models.API.v3.Blocks.Block> GetBlocks(string channel, int limit = 25, int offset = 0, string token = null)
            {

            }

            public static Models.API.v3.Blocks.Block CreateBlock(string channel, string target, string token = null)
            {

            }

            public static void RemoveBlock(string channel, string target, string token = null)
            {

            }
        }

        public static class ChannelFeed
        {
            public static Models.API.v3.ChannelFeeds.ChannelFeedResponse GetChannelFeedPosts(string channel, int limit = 25, int offset = 0, string token = null)
            {

            }

            public static Models.API.v3.ChannelFeeds.PostResponse CreatePost(string channel, string content, bool share = false, string token = null)
            {

            }

            public static Models.API.v3.ChannelFeeds.PostReactionResponse CreateReaction(string channel, string postId, string emoteId, string token = null)
            {

            }

            public static void RemoveReaction(string channel, string postId, string emoteId, string token = null)
            {

            }
        }

        public static class Channels
        {
            public static Models.API.v3.Channels.Channel GetChannel(string channel)
            {

            }

            public static Models.API.v3.Channels.Channel GetChannel(string token = null)
            {

            }

            public static List<Models.API.v3.Users.User> GetChannelEditors(string channel, string token = null)
            {

            }

            public static Models.API.v3.Users.User UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string token = null)
            {

            }

            public static Models.API.v3.Users.User ResetStreamKey(string channel, string token = null)
            {

            }

            public static void RunCommercial(string channel, int length = 30, string token = null)
            {

            }

            public static Models.API.v3.Teams.Team GetTeams(string channel)
            {

            }
        }

        public static class Chat
        {
            public static Models.API.v3.Chat.BadgesResponse GetBadges(string channel)
            {

            }

            public static List<Models.API.v3.Chat.Emoticon> GetEmoticons()
            {

            }

            public static List<Models.API.v3.Chat.EmoticonImage> GetEmoticonImages(string emotesets = null)
            {

            }
        }

        public static class Follows
        {
            public static Models.API.v3.Follows.FollowersResponse GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = "", Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending)
            {

            }

            public static Models.API.v3.Follows.FollowsResponse GetFollows(string channel, int limit = 25, int offset = 0, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending, Models.API.v3.Follows.SortBy sortBy = Models.API.v3.Follows.SortBy.CreatedAt)
            {

            }

            public static Models.API.v3.Follows.Follows GetFollowsStatus(string channel, string targetChannel)
            {

            }

            public static Models.API.v3.Follows.Follows CreateFollow(string channel, string targetChannel, bool notifications = false, string token = null)
            {

            }

            public static void RemoveFollow(string channel, string target, string token = null)
            {

            }
        }

        public static class Games
        {
            public static Models.API.v3.Games.TopGamesResponse GetTopGames(int limit = 10, int offset = 0)
            {

            }
        }

        public static class Ingests
        {
            public static List<Models.API.v3.Ingests.Ingest> GetIngests()
            {

            }
        }

        public static class Root
        {
            public static Models.API.v3.Root.Token GetRoot(string token = null)
            {

            }
        }

        public static class Search
        {
            public static Models.API.v3.Search.SearchChannelsResponse SearchChannels(string query, int limit = 25, int offset = 0)
            {

            }

            public static Models.API.v3.Search.SearchStreamsResponse SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
            {

            }

            public static Models.API.v3.Search.SearchGamesResponse SearchGames(string query, Models.API.v3.Search.GameSearchType type, bool live = false)
            {

            }
        }

        public static class Streams
        {

        }

        public static class Subscriptions
        {

        }

        public static class Teams
        {

        }

        public static class Users
        {

        }

        public static class Videos
        {

        }
    }
}
