using System;
using System.Collections.Generic;

using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    /// <summary>Class representing state of a specific user.</summary>
    public class UserState
    {
        /// <summary>Properrty representing the chat badges a specific user has.</summary>
        public List<KeyValuePair<string, string>> Badges { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>Metadata associated badgest</summary>
        public List<KeyValuePair<string, string>> BadgeInfo { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>Property representing channel.</summary>
        public string Channel { get; }

        /// <summary>Properrty representing HEX user's name.</summary>
        public string ColorHex { get; }

        /// <summary>Property representing user's display name.</summary>
        public string DisplayName { get; }

        /// <summary>Property representing emote sets available to user.</summary>
        public string EmoteSet { get; }

        /// <summary>Property representing Turbo status.</summary>
        public bool IsModerator { get; }

        /// <summary>Property representing subscriber status.</summary>
        public bool IsSubscriber { get; }

        /// <summary>Property representing returned user type of user.</summary>
        public UserType UserType { get; }

        /// <summary>
        /// Constructor for UserState.
        /// </summary>
        /// <param name="ircMessage"></param>
        public UserState(IrcMessage ircMessage)
        {
            Channel = ircMessage.Channel;

            foreach (var tag in ircMessage.Tags.Keys)
            {
                var tagValue = ircMessage.Tags[tag];
                switch (tag)
                {
                    case Tags.Badges:
                        Badges = Common.Helpers.ParseBadges(tagValue);
                        break;
                    case Tags.BadgeInfo:
                        BadgeInfo = Common.Helpers.ParseBadges(tagValue);
                        break;
                    case Tags.Color:
                        ColorHex = tagValue;
                        break;
                    case Tags.DisplayName:
                        DisplayName = tagValue;
                        break;
                    case Tags.EmotesSets:
                        EmoteSet = tagValue;
                        break;
                    case Tags.Mod:
                        IsModerator = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.Subscriber:
                        IsSubscriber = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.UserType:
                        switch (tagValue)
                        {
                            case "mod":
                                UserType = UserType.Moderator;
                                break;
                            case "global_mod":
                                UserType = UserType.GlobalModerator;
                                break;
                            case "admin":
                                UserType = UserType.Admin;
                                break;
                            case "staff":
                                UserType = UserType.Staff;
                                break;
                            default:
                                UserType = UserType.Viewer;
                                break;
                        }
                        break;
                    default:
                        // This should never happen, unless Twitch changes their shit
                        Console.WriteLine($"Unaccounted for [UserState]: {tag}");
                        break;
                }
            }

            if (string.Equals(ircMessage.User, Channel, StringComparison.InvariantCultureIgnoreCase))
                UserType = UserType.Broadcaster;
        }

        public UserState(
            List<KeyValuePair<string, string>> badges,
            List<KeyValuePair<string, string>> badgeInfo,
            string colorHex,
            string displayName,
            string emoteSet,
            string channel,
            bool isSubscriber,
            bool isModerator,
            UserType userType)
        {
            Badges = badges;
            BadgeInfo = badgeInfo;
            ColorHex = colorHex;
            DisplayName = displayName;
            EmoteSet = emoteSet;
            Channel = channel;
            IsSubscriber = isSubscriber;
            IsModerator = isModerator;
            UserType = userType;
        }
    }
}
