using System.Collections.Generic;
using System.Drawing;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
    public class SubscriberBaseBuilder : IBuilder<SubscriberBase>
    {
        protected List<KeyValuePair<string, string>> Badges { get; } = new List<KeyValuePair<string, string>>();

        public List<KeyValuePair<string, string>> BadgeInfo { get; } = new List<KeyValuePair<string, string>>();

        protected string ColorHex { get; set; }

        protected Color Color { get; set; }

        protected string DisplayName { get; set; }

        protected string EmoteSet { get; set; }

        protected string Id { get; set; }

        protected bool IsModerator { get; set; }

        protected bool IsPartner { get; set; }

        protected bool IsSubscriber { get; set; }

        protected bool IsTurbo { get; set; }

        protected string Login { get; set; }

        protected string RawIrc { get; set; }

        protected string ResubMessage { get; set; }

        protected string RoomId { get; set; }

        protected SubscriptionPlan SubscriptionPlan { get; set; }

        protected string SubscriptionPlanName { get; set; }

        protected string SystemMessage { get; set; }

        protected string ParsedSystemMessage { get; set; }

        protected string TmiSentTs { get; set; }

        protected string UserId { get; set; }

        protected UserType UserType { get; set; }

        protected string Channel { get; set; }

        protected string MessageId { get; set; }

        protected string MsgParamCumulativeMonths { get; set; }

        protected string MsgParamStreakMonths { get; set; }

        protected bool MsgParamShouldShareStreak { get; set; }

        protected int Months { get; set; }

        protected SubscriberBaseBuilder()
        {
        }

        public static SubscriberBaseBuilder Create()
        {
            return new SubscriberBaseBuilder();
        }

        public SubscriberBaseBuilder WithBadges(params KeyValuePair<string, string>[] badges)
        {
            Badges.AddRange(badges);
            return this;
        }

        public SubscriberBaseBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
        {
            BadgeInfo.AddRange(badgeInfos);
            return this;
        }

        public SubscriberBaseBuilder WithColorHex(string colorHex)
        {
            ColorHex = colorHex;
            return this;
        }

        public SubscriberBaseBuilder WithColor(Color color)
        {
            Color = color;
            return this;
        }

        public SubscriberBaseBuilder WithDisplayName(string displayName)
        {
            DisplayName = displayName;
            return this;
        }

        public SubscriberBaseBuilder WithEmoteSet(string emoteSet)
        {
            EmoteSet = emoteSet;
            return this;
        }

        public SubscriberBaseBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public SubscriberBaseBuilder WithIsModerator(bool isModerator)
        {
            IsModerator = isModerator;
            return this;
        }

        public SubscriberBaseBuilder WithIsPartner(bool isPartner)
        {
            IsPartner = isPartner;
            return this;
        }

        public SubscriberBaseBuilder WithIsSubscriber(bool isSubscriber)
        {
            IsSubscriber = isSubscriber;
            return this;
        }

        public SubscriberBaseBuilder WithIsTurbo(bool isTurbo)
        {
            IsTurbo = isTurbo;
            return this;
        }

        public SubscriberBaseBuilder WithLogin(string login)
        {
            Login = login;
            return this;
        }

        public SubscriberBaseBuilder WithRawIrc(string rawIrc)
        {
            RawIrc = rawIrc;
            return this;
        }

        public SubscriberBaseBuilder WithResubMessage(string resubMessage)
        {
            ResubMessage = resubMessage;
            return this;
        }

        public SubscriberBaseBuilder WithRoomId(string roomId)
        {
            RoomId = roomId;
            return this;
        }

        public SubscriberBaseBuilder WithSubscribtionPlan(SubscriptionPlan subscriptionPlan)
        {
            SubscriptionPlan = subscriptionPlan;
            return this;
        }

        public SubscriberBaseBuilder WithSubscriptionPlanName(string subscriptionPlanName)
        {
            SubscriptionPlanName = subscriptionPlanName;
            return this;
        }

        public SubscriberBaseBuilder WithSystemMessage(string systemMessage)
        {
            SystemMessage = systemMessage;
            return this;
        }

        public SubscriberBaseBuilder WithParsedSystemMessage(string parsedSystemMessage)
        {
            ParsedSystemMessage = parsedSystemMessage;
            return this;
        }

        public SubscriberBaseBuilder WithTmiSentTs(string tmiSentTs)
        {
            TmiSentTs = tmiSentTs;
            return this;
        }

        public SubscriberBaseBuilder WithUserType(UserType userType)
        {
            UserType = userType;
            return this;
        }

        public SubscriberBaseBuilder WithUserId(string userId)
        {
            UserId = userId;
            return this;
        }

        public SubscriberBaseBuilder WithMonths(int months)
        {
            Months = months;
            return this;
        }

        public SubscriberBaseBuilder WithMessageId(string messageId)
        {
            MessageId = messageId;
            return this;
        }

        public SubscriberBaseBuilder WithMsgParamCumulativeMonths(string msgParamCumulativeMonths)
        {
            MsgParamCumulativeMonths = msgParamCumulativeMonths;
            return this;
        }

        public SubscriberBaseBuilder WithMsgParamStreakMonths(string msgParamStreakMonths)
        {
            MsgParamStreakMonths = msgParamStreakMonths;
            return this;
        }

        public SubscriberBaseBuilder WithMsgParamShouldShareStreak(bool msgParamShouldShareStreak)
        {
            MsgParamShouldShareStreak = msgParamShouldShareStreak;
            return this;
        }

        public SubscriberBaseBuilder WithChannel(string channel)
        {
            Channel = channel;
            return this;
        }

        public virtual SubscriberBase Build()
        {
            return new SubscriberBase(
                Badges,
                BadgeInfo,
                ColorHex,
                Color,
                DisplayName,
                EmoteSet,
                Id,
                Login,
                SystemMessage,
                MessageId,
                MsgParamCumulativeMonths,
                MsgParamStreakMonths,
                MsgParamShouldShareStreak,
                ParsedSystemMessage,
                ResubMessage,
                SubscriptionPlan,
                SubscriptionPlanName,
                RoomId,
                UserId,
                IsModerator,
                IsTurbo,
                IsSubscriber,
                IsPartner,
                TmiSentTs,
                UserType,
                RawIrc,
                Channel,
                Months);
        }
    }
}
