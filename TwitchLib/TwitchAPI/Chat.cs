using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     Chat is where Twitch users can interact with each other while watching a stream.
    /// </summary>
    public class Chat : ApiBase
    {
        /// <summary>
        ///     Retrieves a list of all people currently chatting in a channel's chat.
        /// </summary>
        /// <param name="channel">The channel to retrieve the chatting people for.</param>
        /// <returns>A list of Chatter objects detailing each chatter in a channel.</returns>
        public static async Task<List<Chatter>> GetChatters(string channel)
        {
            var resp = await MakeGetRequest($"https://tmi.twitch.tv/group/user/{channel}/chatters");
            var chatters = JObject.Parse(resp).SelectToken("chatters");
            var chatterList =
                chatters.SelectToken("moderators")
                    .Select(user => new Chatter(user.ToString(), Chatter.UType.Moderator))
                    .ToList();
            chatterList.AddRange(
                chatters.SelectToken("staff").Select(user => new Chatter(user.ToString(), Chatter.UType.Staff)));
            chatterList.AddRange(
                chatters.SelectToken("admins").Select(user => new Chatter(user.ToString(), Chatter.UType.Admin)));
            chatterList.AddRange(
                chatters.SelectToken("global_mods")
                    .Select(user => new Chatter(user.ToString(), Chatter.UType.GlobalModerator)));
            chatterList.AddRange(
                chatters.SelectToken("viewers").Select(user => new Chatter(user.ToString(), Chatter.UType.Viewer)));
            return chatterList;
        }
    }
}