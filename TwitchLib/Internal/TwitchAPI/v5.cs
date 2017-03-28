using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class v5
    {
        public static class Bits
        {
            public static Models.API.v5.Bits.Action[] GetCheermotes(string channelId = null)
            {
                if(channelId == null)
                    return Requests.Get<Models.API.v5.Bits.Action[]>("https://api.twitch.tv/kraken/bits/actions");
                else
                    return Requests.Get<Models.API.v5.Bits.Action[]>($"https://api.twitch.tv/kraken/bits/actions?channel_id={channelId}");
            }
        }

        public static class ChannelFeed
        {

        }

        public static class Chat
        {

        }

        public static class Collections
        {

        }

        public static class Communities
        {

        }

        public static class Games
        {

        }

        public static class Ingests
        {

        }

        public static class Search
        {

        }

        public static class Streams
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
