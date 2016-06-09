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
        /// Retrieves a list of ALL emotes on Twitch.
        /// <para>Caution: Uses a lot of memory to process the entire list.</para>
        /// </summary>
        /// <returns>A list of TwitchEmote objects.</returns>
        public static async Task<List<TwitchEmote>> GetEmotes()
        {
            var emotes = new List<TwitchEmote>();
            var resp = await MakeGetRequest($"{KrakenBaseUrl}/chat/emoticons");

            var json = JObject.Parse(resp);
            emotes.AddRange(
                json.SelectToken("emoticons").Select(emote => new TwitchEmote(emote)));
            return emotes;
        }

        /// <summary>
        /// Retrieves a list of the emotes that can be used in <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The channel to retrieve the emotes for.</param>
        /// <returns>A list of TwitchEmote objects.</returns>
        public static async Task<List<TwitchEmote>> GetEmotes(string channel)
        {
            var emotes = new List<TwitchEmote>();
            var resp = await MakeGetRequest($"{KrakenBaseUrl}/chat/{channel}/emoticons");

            var json = JObject.Parse(resp);
            emotes.AddRange(
                json.SelectToken("emoticons").Select(emote => new TwitchEmote(emote)));
            return emotes;
        }

        /// <summary>
        ///     Retrieves a list of all people currently chatting in a channel's chat.
        ///     <para>Note: This uses an undocumented API endpoint and reliability is not guaranteed.</para>
        /// </summary>
        /// <param name="channel">The channel to retrieve the chatting people for.</param>
        /// <returns>A list of Chatter objects detailing each chatter in a channel.</returns>
        public static async Task<List<Chatter>> GetChatters(string channel)
        {
            var resp = await MakeGetRequest($"{TmiBaseUrl}/group/user/{channel}/chatters");
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