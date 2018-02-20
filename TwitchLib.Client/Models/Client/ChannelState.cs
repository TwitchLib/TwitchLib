using System;

namespace TwitchLib.Models.Client
{
    /// <summary>Class representing a channel state as received from Twitch chat.</summary>
    public class ChannelState
    {
        /// <summary>Property representing whether R9K is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? R9K { get; protected set; }
        /// <summary>Property representing whether Rituals is enabled or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? Rituals { get; protected set; }
        /// <summary>Property representing whether Sub Mode is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? SubOnly { get; protected set; }
        /// <summary>Property representing whether Slow mode is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? SlowMode { get; protected set; }
        /// <summary>Property representing whether EmoteOnly mode is being applied to chat or not. WILL BE NULL IF VALUE NOT PRESENT.</summary>
        public bool? EmoteOnly { get; protected set; }
        /// <summary>Property representing the current broadcaster language.</summary>
        public string BroadcasterLanguage { get; protected set; }
        /// <summary>Property representing the current channel.</summary>
        public string Channel { get; protected set; }
        /// <summary>Property representing how long needed to be following to talk </summary>
        public TimeSpan FollowersOnly { get; protected set; }
        /// <summary>Property representing mercury value. Not sure what it's for.</summary>
        public bool Mercury { get; protected set; }
        /// <summary>Twitch assignedc room id</summary>
        public string RoomId { get; protected set; }

        /// <summary>ChannelState object constructor.</summary>
        public ChannelState(string ircString)
        {
            //@broadcaster-lang=;emote-only=0;r9k=0;slow=0;subs-only=1 :tmi.twitch.tv ROOMSTATE #burkeblack
            var propertyStrig = ircString.Split(' ')[0];
            foreach(var part in propertyStrig.Split(';'))
            {
                switch(part.Split('=')[0].Replace("@", ""))
                {
                    case "broadcaster-lang":
                        BroadcasterLanguage = part.Split('=')[1];
                        break;
                    case "emote-only":
                        EmoteOnly = ConvertToBool(part.Split('=')[1]);
                        break;
                    case "r9k":
                        R9K = ConvertToBool(part.Split('=')[1]);
                        break;
                    case "rituals":
                        Rituals = ConvertToBool(part.Split('=')[1]);
                        break;
                    case "slow":
                        SlowMode = ConvertToBool(part.Split('=')[1]);
                        break;
                    case "subs-only":
                        SubOnly = ConvertToBool(part.Split('=')[1]);
                        break;
                    case "followers-only":
                        var minutes = int.Parse(part.Split('=')[1]);
                        FollowersOnly = TimeSpan.FromMinutes(minutes == -1 ? 0 : minutes);
                        break;
                    case "room-id":
                        RoomId = part.Split('=')[1];
                        break;
                    case "mercury":
                        Mercury = (part.Split('=')[1] == "1");
                        break;
                    default:
                        Console.WriteLine("[TwitchLib][ChannelState] Unaccounted for: " + part);
                        break;
                }
            }
            Channel = ircString.Split('#')[1];
        }

        private static bool? ConvertToBool(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            return data == "1";
        }
    }
}