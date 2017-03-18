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

        }

        public static class Games
        {

        }

        public static class Ingests
        {

        }

        public static class Root
        {

        }

        public static class Search
        {

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
