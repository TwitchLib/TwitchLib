using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    /// <summary>
    /// Object representing emote set from a chat message.
    /// </summary>
    public class EmoteSet
    {
        /// <summary>
        /// List containing all emotes in the message.
        /// </summary>
        public List<Emote> Emotes { get; protected set; }
        /// <summary>
        /// The raw emote set string obtained from Twitch, for legacy purposes.
        /// </summary>
        public string RawEmoteSetString { get; protected set; }

        /// <summary>
        /// Constructor for ChatEmoteSet object.
        /// </summary>
        /// <param name="emoteSetData"></param>
        /// <param name="message"></param>
        public EmoteSet(string emoteSetData, string message)
        {
            Emotes = new List<Emote>();
            RawEmoteSetString = emoteSetData;
            if (string.IsNullOrEmpty(emoteSetData))
                return;
            if (emoteSetData.Contains("/"))
            {
                // Message contains multiple different emotes, first parse by unique emotes: 28087:15-21/25:5-9,28-32
                foreach (string emoteData in emoteSetData.Split('/'))
                {
                    int emoteId = int.Parse(emoteData.Split(':')[0]);
                    if (emoteData.Contains(","))
                    {
                        // Multiple copies of a single emote: 25:5-9,28-32
                        foreach (string emote in emoteData.Replace($"{emoteId}:", "").Split(','))
                            addEmote(emote, emoteId, message);

                    }
                    else
                    {
                        // Single copy of single emote: 25:5-9/28087:16-22
                        addEmote(emoteData, emoteId, message, true);
                    }
                }
            }
            else
            {
                int emoteId = int.Parse(emoteSetData.Split(':')[0]);
                // Message contains a single, or multiple of the same emote
                if (emoteSetData.Contains(","))
                {
                    // Multiple copies of a single emote: 25:5-9,28-32
                    foreach (string emote in emoteSetData.Replace($"{emoteId}:", "").Split(','))
                        addEmote(emote, emoteId, message);
                } else
                {
                    // Single copy of single emote: 25:5-9
                    addEmote(emoteSetData, emoteId, message, true);
                }
            }
        }

        private void addEmote(string emoteData, int emoteId, string message, bool single = false)
        {
            int startIndex = -1, endIndex = -1;
            if (single)
            {
                startIndex = int.Parse(emoteData.Split(':')[1].Split('-')[0]);
                endIndex = int.Parse(emoteData.Split(':')[1].Split('-')[1]);
            } else
            {
                startIndex = int.Parse(emoteData.Split('-')[0]);
                endIndex = int.Parse(emoteData.Split('-')[1]);
            }
            Emotes.Add(new Emote(emoteId, message.Substring(startIndex, (endIndex - startIndex) + 1), startIndex, endIndex));
        }

        /// <summary>
        /// Object representing an emote in an EmoteSet in a chat message.
        /// </summary>
        public class Emote
        {
            /// <summary>Twitch-assigned emote Id.</summary>
            public int Id { get; protected set; }
            /// <summary>The name of the emote. For example, if the message was "This is Kappa test.", the name would be 'Kappa'.</summary>
            public string Name { get; protected set; }
            /// <summary>Character starting index. For example, if the message was "This is Kappa test.", the start index would be 8 for 'Kappa'.</summary>
            public int StartIndex { get; protected set; }
            /// <summary>Character ending index. For example, if the message was "This is Kappa test.", the start index would be 12 for 'Kappa'.</summary>
            public int EndIndex { get; protected set; }
            /// <summary>URL to Twitch hosted emote image.</summary>
            public string ImageUrl { get; protected set; }

            /// <summary>
            /// Emote constructor.
            /// </summary>
            /// <param name="emoteId"></param>
            /// <param name="name"></param>
            /// <param name="emoteStartIndex"></param>
            /// <param name="emoteEndIndex"></param>
            public Emote(int emoteId, string name, int emoteStartIndex, int emoteEndIndex)
            {
                Id = emoteId;
                Name = name;
                StartIndex = emoteStartIndex;
                EndIndex = emoteEndIndex;
                ImageUrl = $"https://static-cdn.jtvnw.net/emoticons/v1/{emoteId}/1.0";
            }
        }
    }
}
