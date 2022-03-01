using System.Collections.Generic;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class RaidNotificationBuilder : IBuilder<RaidNotification>, IFromIrcMessageBuilder<RaidNotification>
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
        private string _msgParamDisplayName;
        private string _msgParamLogin;
        private string _msgParamViewerCount;
        private string _roomId;
        private string _systemMsg;
        private string _systemMsgParsed;
        private string _tmiSentTs;
        private string _userId;
        private UserType _userType;

        public RaidNotificationBuilder WithBadges(params KeyValuePair<string, string>[] badges)
        {
            _badges.AddRange(badges);
            return this;
        }

        public RaidNotificationBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
        {
            _badgeInfo.AddRange(badgeInfos);
            return this;
        }

        public RaidNotificationBuilder WithColor(string color)
        {
            _color = color;
            return this;
        }

        public RaidNotificationBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public RaidNotificationBuilder WithEmotes(string emotes)
        {
            _emotes = emotes;
            return this;
        }

        public RaidNotificationBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public RaidNotificationBuilder WithIsModerator(bool isModerator)
        {
            _isModerator = isModerator;
            return this;
        }

        public RaidNotificationBuilder WithIsSubscriber(bool isSubscriber)
        {
            _isSubscriber = isSubscriber;
            return this;
        }

        public RaidNotificationBuilder WithIsTurbo(bool isTurbo)
        {
            _isTurbo = isTurbo;
            return this;
        }

        public RaidNotificationBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }

        public RaidNotificationBuilder WithMessageId(string msgId)
        {
            _msgId = msgId;
            return this;
        }

        public RaidNotificationBuilder WithMsgParamDisplayName(string msgParamDisplayName)
        {
            _msgParamDisplayName = msgParamDisplayName;
            return this;
        }

        public RaidNotificationBuilder WithMsgParamLogin(string msgParamLogin)
        {
            _msgParamLogin = msgParamLogin;
            return this;
        }

        public RaidNotificationBuilder WithMsgParamViewerCount(string msgParamViewerCount)
        {
            _msgParamViewerCount = msgParamViewerCount;
            return this;
        }

        public RaidNotificationBuilder WithRoomId(string roomId)
        {
            _roomId = roomId;
            return this;
        }

        public RaidNotificationBuilder WithSystemMsg(string systemMsg)
        {
            _systemMsg = systemMsg;
            return this;
        }

        public RaidNotificationBuilder WithSystemMsgParsed(string systemMsgParsed)
        {
            _systemMsgParsed = systemMsgParsed;
            return this;
        }

        public RaidNotificationBuilder WithTmiSentTs(string tmiSentTs)
        {
            _tmiSentTs = tmiSentTs;
            return this;
        }

        public RaidNotificationBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public RaidNotificationBuilder WithUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        private RaidNotificationBuilder()
        {
        }

        public RaidNotification Build()
        {
            return new RaidNotification(
                _badges,
                _badgeInfo,
                _color,
                _displayName,
                _emotes,
                _id,
                _login,
                _isModerator,
                _msgId,
                _msgParamDisplayName,
                _msgParamLogin,
                _msgParamViewerCount,
                _roomId,
                _isSubscriber,
                _systemMsg,
                _systemMsgParsed,
                _tmiSentTs,
                _isTurbo,
                _userType,
                _userId);
        }

        public RaidNotification BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new RaidNotification(fromIrcMessageBuilderDataObject.Message);
        }
    }
}