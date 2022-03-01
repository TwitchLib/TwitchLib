using System;

using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    /// <summary>Class representing a channel state as received from Twitch chat.</summary>
    public class ChannelState
    {
        /// <summary>Property representing the current broadcaster language.</summary>
        public string BroadcasterLanguage { get; }

        /// <summary>Property representing the current channel.</summary>
        public string Channel { get; }

        /// <summary>Property representing whether EmoteOnly mode is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? EmoteOnly { get; }

        /// <summary>Property representing how long needed to be following to talk. If null, FollowersOnly is not enabled.</summary>
        public TimeSpan? FollowersOnly { get; } = null;

        /// <summary>Property representing mercury value. Not sure what it's for.</summary>
        public bool Mercury { get; }

        /// <summary>Property representing whether R9K is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? R9K { get; }

        /// <summary>Property representing whether Rituals is enabled or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? Rituals { get; }

        /// <summary>Twitch assignedc room id</summary>
        public string RoomId { get; }

        /// <summary>Property representing whether Slow mode is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public int? SlowMode { get; }

        /// <summary>Property representing whether Sub Mode is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? SubOnly { get; }

        /// <summary>ChannelState object constructor.</summary>
        public ChannelState(IrcMessage ircMessage)
        {
            //@broadcaster-lang=;emote-only=0;r9k=0;slow=0;subs-only=1 :tmi.twitch.tv ROOMSTATE #burkeblack
            foreach (var tag in ircMessage.Tags.Keys)
            {
                var tagValue = ircMessage.Tags[tag];

                switch (tag)
                {
                    case Tags.BroadcasterLang:
                        BroadcasterLanguage = tagValue;
                        break;
                    case Tags.EmoteOnly:
                        EmoteOnly = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.R9K:
                        R9K = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.Rituals:
                        Rituals = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.Slow:
                        var success = int.TryParse(tagValue, out var slowDuration);
                        SlowMode = success ? slowDuration : (int?)null;
                        break;
                    case Tags.SubsOnly:
                        SubOnly = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.FollowersOnly:
                        if(int.TryParse(tagValue, out int minutes) && minutes > -1)
                        {
                            FollowersOnly = TimeSpan.FromMinutes(minutes);
                        }
                        break;
                    case Tags.RoomId:
                        RoomId = tagValue;
                        break;
                    case Tags.Mercury:
                        Mercury = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    default:
                        Console.WriteLine("[TwitchLib][ChannelState] Unaccounted for: " + tag);
                        break;
                }
            }
            Channel = ircMessage.Channel;
        }

        public ChannelState(
            bool r9k,
            bool rituals,
            bool subonly,
            int slowMode,
            bool emoteOnly,
            string broadcasterLanguage,
            string channel,
            TimeSpan followersOnly,
            bool mercury,
            string roomId)
        {
            R9K = r9k;
            Rituals = rituals;
            SubOnly = subonly;
            SlowMode = slowMode;
            EmoteOnly = emoteOnly;
            BroadcasterLanguage = broadcasterLanguage;
            Channel = channel;
            FollowersOnly = followersOnly;
            Mercury = mercury;
            RoomId = roomId;
        }
    }
}
