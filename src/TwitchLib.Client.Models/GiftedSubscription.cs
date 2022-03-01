using System;
using System.Collections.Generic;

using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    public class GiftedSubscription
    {
        private const string AnonymousGifterUserId = "274598607";

        public List<KeyValuePair<string, string>> Badges { get; }

        public List<KeyValuePair<string, string>> BadgeInfo { get; }

        public string Color { get; }

        public string DisplayName { get; }

        public string Emotes { get; }

        public string Id { get; }

        public bool IsModerator { get; }

        public bool IsSubscriber { get; }

        public bool IsTurbo { get; }

        public bool IsAnonymous { get; }

        public string Login { get; }

        public string MsgId { get; }

        public string MsgParamMonths { get; }

        public string MsgParamRecipientDisplayName { get; }

        public string MsgParamRecipientId { get; }

        public string MsgParamRecipientUserName { get; }

        public string MsgParamSubPlanName { get; }

        public SubscriptionPlan MsgParamSubPlan { get; }

        public string RoomId { get; }

        public string SystemMsg { get; }

        public string SystemMsgParsed { get; }

        public string TmiSentTs { get; }

        public string UserId { get; }

        public UserType UserType { get; }

        public string MsgParamMultiMonthGiftDuration { get; }

        public GiftedSubscription(IrcMessage ircMessage)
        {
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
                    case Tags.MsgParamMonths:
                        MsgParamMonths = tagValue;
                        break;
                    case Tags.MsgParamRecipientDisplayname:
                        MsgParamRecipientDisplayName = tagValue;
                        break;
                    case Tags.MsgParamRecipientId:
                        MsgParamRecipientId = tagValue;
                        break;
                    case Tags.MsgParamRecipientUsername:
                        MsgParamRecipientUserName = tagValue;
                        break;
                    case Tags.MsgParamSubPlanName:
                        MsgParamSubPlanName = tagValue;
                        break;
                    case Tags.MsgParamSubPlan:
                        switch (tagValue)
                        {
                            case "prime":
                                MsgParamSubPlan = SubscriptionPlan.Prime;
                                break;
                            case "1000":
                                MsgParamSubPlan = SubscriptionPlan.Tier1;
                                break;
                            case "2000":
                                MsgParamSubPlan = SubscriptionPlan.Tier2;
                                break;
                            case "3000":
                                MsgParamSubPlan = SubscriptionPlan.Tier3;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(tagValue.ToLower));
                        }
                        break;
                    case Tags.RoomId:
                        RoomId = tagValue;
                        break;
                    case Tags.Subscriber:
                        IsSubscriber = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.SystemMsg:
                        SystemMsg = tagValue;
                        SystemMsgParsed = tagValue.Replace("\\s", " ").Replace("\\n", "");
                        break;
                    case Tags.TmiSentTs:
                        TmiSentTs = tagValue;
                        break;
                    case Tags.Turbo:
                        IsTurbo = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.UserId:
                        UserId = tagValue;
                        if (UserId == AnonymousGifterUserId)
                        {
                            IsAnonymous = true;
                        }
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
                    case Tags.MsgParamMultiMonthGiftDuration:
                        MsgParamMultiMonthGiftDuration = tagValue;
                        break;
                }
            }
        }

        public GiftedSubscription(
            List<KeyValuePair<string, string>> badges,
            List<KeyValuePair<string, string>> badgeInfo,
            string color,
            string displayName,
            string emotes,
            string id,
            string login,
            bool isModerator,
            string msgId,
            string msgParamMonths,
            string msgParamRecipientDisplayName,
            string msgParamRecipientId,
            string msgParamRecipientUserName,
            string msgParamSubPlanName,
            string msgMultiMonthDuration,
            SubscriptionPlan msgParamSubPlan,
            string roomId,
            bool isSubscriber,
            string systemMsg,
            string systemMsgParsed,
            string tmiSentTs,
            bool isTurbo,
            UserType userType,
            string userId)
        {
            Badges = badges;
            BadgeInfo = badgeInfo;
            Color = color;
            DisplayName = displayName;
            Emotes = emotes;
            Id = id;
            Login = login;
            IsModerator = isModerator;
            MsgId = msgId;
            MsgParamMonths = msgParamMonths;
            MsgParamRecipientDisplayName = msgParamRecipientDisplayName;
            MsgParamRecipientId = msgParamRecipientId;
            MsgParamRecipientUserName = msgParamRecipientUserName;
            MsgParamSubPlanName = msgParamSubPlanName;
            MsgParamSubPlan = msgParamSubPlan;
            MsgParamMultiMonthGiftDuration = msgMultiMonthDuration;
            RoomId = roomId;
            IsSubscriber = isSubscriber;
            SystemMsg = systemMsg;
            SystemMsgParsed = systemMsgParsed;
            TmiSentTs = tmiSentTs;
            IsTurbo = isTurbo;
            UserType = userType;
            UserId = userId;
        }
    }
}
