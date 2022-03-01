using System.Collections.Generic;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class SentMessageBuilder : IBuilder<SentMessage>
    {
        private readonly List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();
        private string _channel;
        private string _colorHex;
        private string _displayName;
        private string _emoteSet;
        private bool _isModerator;
        private bool _isSubscriber;
        private string _message;
        private UserType _userType;

        private SentMessageBuilder()
        {
        }

        public SentMessageBuilder WithBadges(params KeyValuePair<string, string>[] badges)
        {
            _badges.AddRange(badges);
            return this;
        }

        public SentMessageBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public SentMessageBuilder WithColorHex(string colorHex)
        {
            _colorHex = colorHex;
            return this;
        }

        public SentMessageBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public SentMessageBuilder WithEmoteSet(string emoteSet)
        {
            _emoteSet = emoteSet;
            return this;
        }

        public SentMessageBuilder WithIsModerator(bool isModerator)
        {
            _isModerator = isModerator;
            return this;
        }

        public SentMessageBuilder WithIsSubscriber(bool isSubscriber)
        {
            _isSubscriber = isSubscriber;
            return this;
        }

        public SentMessageBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public SentMessageBuilder WithUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        public static SentMessageBuilder Create()
        {
            return new SentMessageBuilder();
        }

        public SentMessage BuildFromUserState(UserState userState, string message)
        {
            return new SentMessage(userState, message);
        }

        public SentMessage Build()
        {
            return new SentMessage(
                _badges,
                _channel,
                _colorHex,
                _displayName,
                _emoteSet,
                _isModerator,
                _isSubscriber,
                _userType,
                _message);
        }
    }
}
