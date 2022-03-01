using System;
using System.Collections.Generic;

using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    public class ContinuedGiftedSubscription
    {
        //@badge-info=subscriber/11;badges=subscriber/9;color=#DAA520;display-name=Varanid;emotes=;flags=;id=a2d384c1-c30a-409e-8001-9e7d8f9c784d;login=varanid;mod=0;msg-id=giftpaidupgrade;msg-param-sender-login=cletusbueford;msg-param-sender-name=CletusBueford;room-id=44338537;subscriber=1;system-msg=Varanid\sis\scontinuing\sthe\sGift\sSub\sthey\sgot\sfrom\sCletusBueford!;tmi-sent-ts=1612497386372;user-id=67505836;user-type= :tmi.twitch.tv USERNOTICE #burkeblack 

        public List<KeyValuePair<string, string>> Badges { get; }

        public List<KeyValuePair<string, string>> BadgeInfo { get; }

        public string Color { get; }

        public string DisplayName { get; }

        public string Emotes { get; }

        public string Flags { get; }

        public string Id { get; }

        public string Login { get; }

        public bool IsModerator { get; }

        public string MsgId { get; }

        public string MsgParamSenderLogin { get; }

        public string MsgParamSenderName { get; }

        public string RoomId { get; }

        public bool IsSubscriber { get; }

        public string SystemMsg { get; }

        public string TmiSentTs { get; }

        public string UserId { get; }

        public UserType UserType { get; }

        public ContinuedGiftedSubscription(IrcMessage ircMessage)
        {
            foreach (var tag in ircMessage.Tags.Keys)
            {
                var tagValue = ircMessage.Tags[tag];

                switch (tag)
                {
                    case Tags.SystemMsg:
                        SystemMsg = tagValue;
                        break;
                    case Tags.Flags:
                        Flags = tagValue;
                        break;
                    case Tags.MsgParamSenderLogin:
                        MsgParamSenderLogin = tagValue;
                        break;
                    case Tags.MsgParamSenderName:
                        MsgParamSenderName = tagValue;
                        break;
                    case Tags.Badges:
                        Badges = Common.Helpers.ParseBadges(tagValue);
                        break;
                    case Tags.BadgeInfo:
                        BadgeInfo = Common.Helpers.ParseBadges(tagValue);
                        break;
                    case Tags.Color:
                        Color = tagValue;
                        break;
                    case Tags.DisplayName:
                        DisplayName = tagValue;
                        break;
                    case Tags.Emotes:
                        Emotes = tagValue;
                        break;
                    case Tags.Id:
                        Id = tagValue;
                        break;
                    case Tags.Login:
                        Login = tagValue;
                        break;
                    case Tags.Mod:
                        IsModerator = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.MsgId:
                        MsgId = tagValue;
                        break;
                    case Tags.RoomId:
                        RoomId = tagValue;
                        break;
                    case Tags.Subscriber:
                        IsSubscriber = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.TmiSentTs:
                        TmiSentTs = tagValue;
                        break;
                    case Tags.UserId:
                        UserId = tagValue;
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
                }
            }
        }
    }
}
