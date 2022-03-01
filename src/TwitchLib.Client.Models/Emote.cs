namespace TwitchLib.Client.Models
{
    /// <summary>
    /// Object representing an emote in an EmoteSet in a chat message.
    /// </summary>
    public class Emote
    {
        /// <summary>Twitch-assigned emote Id.</summary>
        public string Id { get; }

        /// <summary>The name of the emote. For example, if the message was "This is Kappa test.", the name would be 'Kappa'.</summary>
        public string Name { get; }

        /// <summary>Character starting index. For example, if the message was "This is Kappa test.", the start index would be 8 for 'Kappa'.</summary>
        public int StartIndex { get; }

        /// <summary>Character ending index. For example, if the message was "This is Kappa test.", the start index would be 12 for 'Kappa'.</summary>
        public int EndIndex { get; }

        /// <summary>URL to Twitch hosted emote image.</summary>
        public string ImageUrl { get; }

        /// <summary>
        /// Emote constructor.
        /// </summary>
        /// <param name="emoteId"></param>
        /// <param name="name"></param>
        /// <param name="emoteStartIndex"></param>
        /// <param name="emoteEndIndex"></param>
        public Emote(
            string emoteId,
            string name,
            int emoteStartIndex,
            int emoteEndIndex)
        {
            Id = emoteId;
            Name = name;
            StartIndex = emoteStartIndex;
            EndIndex = emoteEndIndex;
            ImageUrl = $"https://static-cdn.jtvnw.net/emoticons/v1/{emoteId}/1.0";
        }
    }
}
