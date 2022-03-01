
using System;
using System.Collections.Generic;
using System.Drawing;

using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Extensions.NetCore;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    /// <summary>Class representing a resubscriber.</summary>
    public class SubscriberBase
    {
        /// <summary>Property representing list of badges assigned.</summary>
        public List<KeyValuePair<string, string>> Badges { get; }

        /// <summary>Metadata associated with each badge</summary>
        public List<KeyValuePair<string, string>> BadgeInfo { get; }

        /// <summary>Property representing the colorhex of the resubscriber.</summary>
        public string ColorHex { get; }

        /// <summary>Property representing HEX color as a System.Drawing.Color object.</summary>
        public Color Color { get; }

        /// <summary>Property representing resubscriber's customized display name.</summary>
        public string DisplayName { get; }

        /// <summary>Property representing emote set of resubscriber.</summary>
        public string EmoteSet { get; }

        /// <summary>Property representing resub message id</summary>
        public string Id { get; }

        /// <summary>Property representing whether or not the resubscriber is a moderator.</summary>
        public bool IsModerator { get; }

        /// <summary>Property representing whether or not person is a partner.</summary>
        public bool IsPartner { get; }

        /// <summary>Property representing whether or not the resubscriber is a subscriber (YES).</summary>
        public bool IsSubscriber { get; }

        /// <summary>Property representing whether or not the resubscriber is a turbo member.</summary>
        public bool IsTurbo { get; }

        /// <summary>Property representing login of resubscription event.</summary>
        public string Login { get; }

        public string MsgId { get; }

        public string MsgParamCumulativeMonths { get; }

        public bool MsgParamShouldShareStreak { get; }

        public string MsgParamStreakMonths { get; }

        /// <summary>Property representing the raw IRC message (for debugging/customized parsing)</summary>
        public string RawIrc { get; }

        /// <summary>Property representing system message.</summary>
        public string ResubMessage { get; }

        /// <summary>Property representing the room id.</summary>
        public string RoomId { get; }

        /// <summary>Property representing the plan a user is on.</summary>
        public SubscriptionPlan SubscriptionPlan { get; } = SubscriptionPlan.NotSet;

        /// <summary>Property representing the subscription plan name.</summary>
        public string SubscriptionPlanName { get; }

        /// <summary>Property representing internval system message value.</summary>
        public string SystemMessage { get; }

        /// <summary>Property representing internal system message value, parsed.</summary>
        public string SystemMessageParsed { get; }

        /// <summary>Property representing the tmi-sent-ts value.</summary>
        public string TmiSentTs { get; }

        /// <summary>Property representing the user's id.</summary>
        public string UserId { get; }

        /// <summary>Property representing the user type of the resubscriber.</summary>
        public UserType UserType { get; }

        public string Channel { get; }

        // @badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;id=9dasn-asdibas-asdba-as8as;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG

        protected readonly int monthsInternal;

        /// <summary>Subscriber object constructor.</summary>
        protected SubscriberBase(IrcMessage ircMessage)
        {
            RawIrc = ircMessage.ToString();
            ResubMessage = ircMessage.Message;

            foreach (var tag in ircMessage.Tags.Keys)
            {
                var tagValue = ircMessage.Tags[tag];
                switch (tag)
                {
                    case Tags.Badges:
                        Badges = Common.Helpers.ParseBadges(tagValue);
                        // iterate through badges for special circumstances
                        foreach (var badge in Badges)
                        {
                            if (badge.Key == "partner")
                                IsPartner = true;
                        }
                        break;
                    case Tags.BadgeInfo:
                        BadgeInfo = Common.Helpers.ParseBadges(tagValue);
                        break;
                    case Tags.Color:
                        ColorHex = tagValue;
                        if (!string.IsNullOrEmpty(ColorHex))
                            Color = ColorTranslator.FromHtml(ColorHex);
                        break;
                    case Tags.DisplayName:
                        DisplayName = tagValue;
                        break;
                    case Tags.Emotes:
                        EmoteSet = tagValue;
                        break;
                    case Tags.Id:
                        Id = tagValue;
                        break;
                    case Tags.Login:
                        Login = tagValue;
                        break;
                    case Tags.Mod:
                        IsModerator = ConvertToBool(tagValue);
                        break;
                    case Tags.MsgId:
                        MsgId = tagValue;
                        break;
                    case Tags.MsgParamCumulativeMonths:
                        MsgParamCumulativeMonths = tagValue;
                        break;
                    case Tags.MsgParamStreakMonths:
                        MsgParamStreakMonths = tagValue;
                        break;
                    case Tags.MsgParamShouldShareStreak:
                        MsgParamShouldShareStreak = Common.Helpers.ConvertToBool(tagValue);
                        break;
                    case Tags.MsgParamSubPlan:
                        switch (tagValue.ToLower())
                        {
                            case "prime":
                                SubscriptionPlan = SubscriptionPlan.Prime;
                                break;
                            case "1000":
                                SubscriptionPlan = SubscriptionPlan.Tier1;
                                break;
                            case "2000":
                                SubscriptionPlan = SubscriptionPlan.Tier2;
                                break;
                            case "3000":
                                SubscriptionPlan = SubscriptionPlan.Tier3;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(tagValue.ToLower));
                        }
                        break;
                    case Tags.MsgParamSubPlanName:
                        SubscriptionPlanName = tagValue.Replace("\\s", " ");
                        break;
                    case Tags.RoomId:
                        RoomId = tagValue;
                        break;
                    case Tags.Subscriber:
                        IsSubscriber = ConvertToBool(tagValue);
                        break;
                    case Tags.SystemMsg:
                        SystemMessage = tagValue;
                        SystemMessageParsed = tagValue.Replace("\\s", " ");
                        break;
                    case Tags.TmiSentTs:
                        TmiSentTs = tagValue;
                        break;
                    case Tags.Turbo:
                        IsTurbo = ConvertToBool(tagValue);
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

        internal SubscriberBase(
            List<KeyValuePair<string, string>> badges,
            List<KeyValuePair<string, string>> badgeInfo,
            string colorHex,
            Color color,
            string displayName,
            string emoteSet,
            string id,
            string login,
            string systemMessage,
            string msgId,
            string msgParamCumulativeMonths,
            string msgParamStreakMonths,
            bool msgParamShouldShareStreak,
            string systemMessageParsed,
            string resubMessage,
            SubscriptionPlan subscriptionPlan,
            string subscriptionPlanName,
            string roomId,
            string userId,
            bool isModerator,
            bool isTurbo,
            bool isSubscriber,
            bool isPartner,
            string tmiSentTs,
            UserType userType,
            string rawIrc,
            string channel,
            int months)
        {
            Badges = badges;
            BadgeInfo = badgeInfo;
            ColorHex = colorHex;
            Color = color;
            DisplayName = displayName;
            EmoteSet = emoteSet;
            Id = id;
            Login = login;
            MsgId = msgId;
            MsgParamCumulativeMonths = msgParamCumulativeMonths;
            MsgParamStreakMonths = msgParamStreakMonths;
            MsgParamShouldShareStreak = msgParamShouldShareStreak;
            SystemMessage = systemMessage;
            SystemMessageParsed = systemMessageParsed;
            ResubMessage = resubMessage;
            SubscriptionPlan = subscriptionPlan;
            SubscriptionPlanName = subscriptionPlanName;
            RoomId = roomId;
            UserId = UserId;
            IsModerator = isModerator;
            IsTurbo = isTurbo;
            IsSubscriber = isSubscriber;
            IsPartner = isPartner;
            TmiSentTs = tmiSentTs;
            UserType = userType;
            RawIrc = rawIrc;
            monthsInternal = months;
            UserId = userId;
            Channel = channel;
        }

        private static bool ConvertToBool(string data)
        {
            return data == "1";
        }

        /// <summary>Overriden ToString method, prints out all properties related to resub.</summary>
        public override string ToString()
        {
            return $"Badges: {Badges.Count}, color hex: {ColorHex}, display name: {DisplayName}, emote set: {EmoteSet}, login: {Login}, system message: {SystemMessage}, msgId: {MsgId}, msgParamCumulativeMonths: {MsgParamCumulativeMonths}" +
                $"msgParamStreakMonths: {MsgParamStreakMonths}, msgParamShouldShareStreak: {MsgParamShouldShareStreak}, resub message: {ResubMessage}, months: {monthsInternal}, room id: {RoomId}, user id: {UserId}, mod: {IsModerator}, turbo: {IsTurbo}, sub: {IsSubscriber}, user type: {UserType}, raw irc: {RawIrc}";
        }
    }
}
