using System.Collections.Generic;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class UserStateBuilder : IBuilder<UserState>, IFromIrcMessageBuilder<UserState>
    {
        private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();
        private readonly List<KeyValuePair<string, string>> _badgeInfo = new List<KeyValuePair<string, string>>();
        private string _channel;
        private string _colorHex;
        private string _displayName;
        private string _emoteSet;
        private bool _isModerator;
        private bool _isSubscriber;
        private UserType _userType;

        private UserStateBuilder()
        {
        }

        public UserStateBuilder WithBadges(params KeyValuePair<string, string>[] badges)
        {
            _badges.AddRange(badges);
            return this;
        }

        public UserStateBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
        {
            _badgeInfo.AddRange(badgeInfos);
            return this;
        }

        public UserStateBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public UserStateBuilder WithColorHex(string olorHex)
        {
            _colorHex = olorHex;
            return this;
        }

        public UserStateBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public UserStateBuilder WithEmoteSet(string emoteSet)
        {
            _emoteSet = emoteSet;
            return this;
        }

        public UserStateBuilder WithIsModerator(bool isModerator)
        {
            _isModerator = isModerator;
            return this;
        }

        public UserStateBuilder WithIsSubscriber(bool isSubscriber)
        {
            _isSubscriber = isSubscriber;
            return this;
        }

        public UserStateBuilder WithUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        public static UserStateBuilder Create()
        {
            return new UserStateBuilder();
        }

        public UserState BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new UserState(fromIrcMessageBuilderDataObject.Message);
        }

        public UserState Build()
        {
            return new UserState(
                _badges,
                _badgeInfo,
                _colorHex,
                _displayName,
                _emoteSet,
                _channel,
                _isSubscriber,
                _isModerator,
                _userType);
        }
    }
}
