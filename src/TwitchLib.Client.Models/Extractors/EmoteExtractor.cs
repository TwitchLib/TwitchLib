using System.Collections.Generic;

using TwitchLib.Client.Models.Builders;

namespace TwitchLib.Client.Models.Extractors
{
    public class EmoteExtractor
    {
        public IEnumerable<Emote> Extract(string rawEmoteSetString, string message)
        {
            if (string.IsNullOrEmpty(rawEmoteSetString)
               || string.IsNullOrEmpty(message))
            {
                yield break;
            }

            if (rawEmoteSetString.Contains("/"))
            {
                // Message contains multiple different emotes, first parse by unique emotes: 28087:15-21/25:5-9,28-32
                foreach (var emoteData in rawEmoteSetString.Split('/'))
                {
                    var emoteId = emoteData.Split(':')[0];
                    if (emoteData.Contains(","))
                    {
                        // Multiple copies of a single emote: 25:5-9,28-32
                        foreach (var emote in emoteData.Replace($"{emoteId}:", "").Split(','))
                            yield return GetEmote(emote, emoteId, message);
                    }
                    else
                    {
                        // Single copy of single emote: 25:5-9/28087:16-22
                        yield return GetEmote(emoteData, emoteId, message, true);
                    }
                }
            }
            else
            {
                var emoteId = rawEmoteSetString.Split(':')[0];
                // Message contains a single, or multiple of the same emote
                if (rawEmoteSetString.Contains(","))
                {
                    // Multiple copies of a single emote: 25:5-9,28-32
                    foreach (var emote in rawEmoteSetString.Replace($"{emoteId}:", "").Split(','))
                        yield return GetEmote(emote, emoteId, message);
                }
                else
                {
                    // Single copy of single emote: 25:5-9
                    yield return GetEmote(rawEmoteSetString, emoteId, message, true);
                }
            }
        }

        private Emote GetEmote(string emoteData, string emoteId, string message, bool single = false)
        {
            int startIndex = -1;
            int endIndex = -1;

            if (single)
            {
                startIndex = int.Parse(emoteData.Split(':')[1].Split('-')[0]);
                endIndex = int.Parse(emoteData.Split(':')[1].Split('-')[1]);
            }
            else
            {
                startIndex = int.Parse(emoteData.Split('-')[0]);
                endIndex = int.Parse(emoteData.Split('-')[1]);
            }

            string name = message.Substring(startIndex, (endIndex - startIndex) + 1);

            EmoteBuilder emoteBuilder = EmoteBuilder.Create()
                                                    .WithId(emoteId)
                                                    .WithName(name)
                                                    .WithStartIndex(startIndex)
                                                    .WithEndIndex(endIndex);

            return emoteBuilder.Build();
        }
    }
}
