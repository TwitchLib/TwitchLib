using System.Collections.Generic;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class GiftedSubscriptionBuilder : IBuilder<GiftedSubscription>, IFromIrcMessageBuilder<GiftedSubscription>
    {
        private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();
        private readonly List<KeyValuePair<string, string>> _badgeInfo = new List<KeyValuePair<string, string>>();
        private string _color;
        private string _displayName;
        private string _emotes;
        private string _id;
        private bool _isModerator;
        private bool _isSubscriber;
        private bool _isTurbo;
        private string _login;
        private string _msgId;
        private string _msgParamMonths;
        private string _msgParamRecipientDisplayName;
        private string _msgParamRecipientId;
        private string _msgParamRecipientUserName;
        private string _msgParamSubPlanName;
        private string _msgParamMultiMonthGiftDuration;
        private SubscriptionPlan _msgParamSubPlan;
        private string _roomId;
        private string _systemMsg;
        private string _systemMsgParsed;
        private string _tmiSentTs;
        private string _userId;
        private UserType _userType;

        private GiftedSubscriptionBuilder()
        {
        }

        public GiftedSubscriptionBuilder WithBadges(params KeyValuePair<string, string>[] badges)
        {
            _badges.AddRange(badges);
            return this;
        }

        public GiftedSubscriptionBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
        {
            _badgeInfo.AddRange(badgeInfos);
            return this;
        }

        public GiftedSubscriptionBuilder WithColor(string color)
        {
            _color = color;
            return this;
        }

        public GiftedSubscriptionBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public GiftedSubscriptionBuilder WithEmotes(string emotes)
        {
            _emotes = emotes;
            return this;
        }

        public GiftedSubscriptionBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public GiftedSubscriptionBuilder WithIsModerator(bool isModerator)
        {
            _isModerator = isModerator;
            return this;
        }

        public GiftedSubscriptionBuilder WithIsSubscriber(bool isSubscriber)
        {
            _isSubscriber = isSubscriber;
            return this;
        }

        public GiftedSubscriptionBuilder WithIsTurbo(bool isTurbo)
        {
            _isTurbo = isTurbo;
            return this;
        }

        public GiftedSubscriptionBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }

        public GiftedSubscriptionBuilder WithMessageId(string msgId)
        {
            _msgId = msgId;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamCumulativeMonths(string msgParamMonths)
        {
            _msgParamMonths = msgParamMonths;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamRecipientDisplayName(string msgParamRecipientDisplayName)
        {
            _msgParamRecipientDisplayName = msgParamRecipientDisplayName;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamRecipientId(string msgParamRecipientId)
        {
            _msgParamRecipientId = msgParamRecipientId;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamRecipientUserName(string msgParamRecipientUserName)
        {
            _msgParamRecipientUserName = msgParamRecipientUserName;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamSubPlanName(string msgParamSubPlanName)
        {
            _msgParamSubPlanName = msgParamSubPlanName;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamMultiMonthGiftDuration(string msgParamMultiMonthGiftDuration)
        {
            _msgParamMultiMonthGiftDuration = msgParamMultiMonthGiftDuration;
            return this;
        }

        public GiftedSubscriptionBuilder WithMsgParamSubPlan(SubscriptionPlan msgParamSubPlan)
        {
            _msgParamSubPlan = msgParamSubPlan;
            return this;
        }

        public GiftedSubscriptionBuilder WithRoomId(string roomId)
        {
            _roomId = roomId;
            return this;
        }

        public GiftedSubscriptionBuilder WithSystemMsg(string systemMsg)
        {
            _systemMsg = systemMsg;
            return this;
        }

        public GiftedSubscriptionBuilder WithSystemMsgParsed(string systemMsgParsed)
        {
            _systemMsgParsed = systemMsgParsed;
            return this;
        }

        public GiftedSubscriptionBuilder WithTmiSentTs(string tmiSentTs)
        {
            _tmiSentTs = tmiSentTs;
            return this;
        }

        public GiftedSubscriptionBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public GiftedSubscriptionBuilder WithUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        public static GiftedSubscriptionBuilder Create()
        {
            return new GiftedSubscriptionBuilder();
        }

        public GiftedSubscription Build()
        {
            return new GiftedSubscription(
                _badges,
                _badgeInfo,
                _color,
                _displayName,
                _emotes,
                _id,
                _login,
                _isModerator,
                _msgId,
                _msgParamMonths,
                _msgParamRecipientDisplayName,
                _msgParamRecipientId,
                _msgParamRecipientUserName,
                _msgParamSubPlanName,
                _msgParamMultiMonthGiftDuration,
                _msgParamSubPlan,
                _roomId,
                _isSubscriber,
                _systemMsg,
                _systemMsgParsed,
                _tmiSentTs,
                _isTurbo,
                _userType,
                _userId);
        }

        public GiftedSubscription BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new GiftedSubscription(fromIrcMessageBuilderDataObject.Message);
        }
    }
}