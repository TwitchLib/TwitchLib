using System.Collections.Generic;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class ChatMessageBuilder : IBuilder<ChatMessage>
    {
        private TwitchLibMessage _twitchLibMessage;
        private readonly List<KeyValuePair<string, string>> BadgeInfo = new List<KeyValuePair<string, string>>();
        private int _bits;
        private double _bitsInDollars;
        private string _channel;
        private CheerBadge _cheerBadge;
        private string _emoteReplacedMessage;
        private string _id;
        private bool _isBroadcaster;
        private bool _isMe;
        private bool _isModerator;
        private bool _isSubscriber;
        private bool _isVip;
        private bool _isStaff;
        private bool _isPartner;
        private string _message;
        private Noisy _noisy;
        private string _rawIrcMessage;
        private string _roomId;
        private int _subscribedMonthCount;

        private ChatMessageBuilder()
        {
            _twitchLibMessage = TwitchLibMessageBuilder.Create().Build();
        }

        public ChatMessageBuilder WithTwitchLibMessage(TwitchLibMessage twitchLibMessage)
        {
            _twitchLibMessage = twitchLibMessage;
            return this;
        }

        public ChatMessageBuilder WithBadgeInfos(params KeyValuePair<string, string>[] badgeInfos)
        {
            BadgeInfo.AddRange(badgeInfos);
            return this;
        }

        public ChatMessageBuilder WithBits(int bits)
        {
            _bits = bits;
            return this;
        }

        public ChatMessageBuilder WithBitsInDollars(double bitsInDollars)
        {
            _bitsInDollars = bitsInDollars;
            return this;
        }

        public ChatMessageBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public ChatMessageBuilder WithCheerBadge(CheerBadge cheerBadge)
        {
            _cheerBadge = cheerBadge;
            return this;
        }

        public ChatMessageBuilder WithEmoteReplaceMessage(string emoteReplaceMessage)
        {
            _emoteReplacedMessage = emoteReplaceMessage;
            return this;
        }

        public ChatMessageBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public ChatMessageBuilder WithIsBroadcaster(bool isBroadcaster)
        {
            _isBroadcaster = isBroadcaster;
            return this;
        }

        public ChatMessageBuilder WithIsMe(bool isMe)
        {
            _isMe = isMe;
            return this;
        }

        public ChatMessageBuilder WithIsModerator(bool isModerator)
        {
            _isModerator = isModerator;
            return this;
        }

        public ChatMessageBuilder WithIsSubscriber(bool isSubscriber)
        {
            _isSubscriber = isSubscriber;
            return this;
        }
        public ChatMessageBuilder WithIsVip(bool isVip)
        {
            _isVip = isVip;
            return this;
        }
        public ChatMessageBuilder WithIsStaff(bool isStaff)
        {
            _isStaff = isStaff;
            return this;
        }

        public ChatMessageBuilder WithIsPartner(bool isPartner)
        {
            _isPartner = isPartner;
            return this;
        }
        public ChatMessageBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public ChatMessageBuilder WithNoisy(Noisy noisy)
        {
            _noisy = noisy;
            return this;
        }

        public ChatMessageBuilder WithRawIrcMessage(string rawIrcMessage)
        {
            _rawIrcMessage = rawIrcMessage;
            return this;
        }

        public ChatMessageBuilder WithRoomId(string roomId)
        {
            _roomId = roomId;
            return this;
        }

        public ChatMessageBuilder WithSubscribedMonthCount(int subscribedMonthCount)
        {
            _subscribedMonthCount = subscribedMonthCount;
            return this;
        }

        public static ChatMessageBuilder Create()
        {
            return new ChatMessageBuilder();
        }

        public ChatMessage Build()
        {
            return new ChatMessage(
                _twitchLibMessage.BotUsername,
                _twitchLibMessage.UserId,
                _twitchLibMessage.Username,
                _twitchLibMessage.DisplayName,
                _twitchLibMessage.ColorHex,
                _twitchLibMessage.Color,
                _twitchLibMessage.EmoteSet,
                _message,
                _twitchLibMessage.UserType,
                _channel,
                _id,
                _isSubscriber,
                _subscribedMonthCount,
                _roomId,
                _twitchLibMessage.IsTurbo,
                _isModerator,
                _isMe,
                _isBroadcaster,
                _isVip,
                _isPartner,
                _isStaff,
                _noisy,
                _rawIrcMessage,
                _emoteReplacedMessage,
                _twitchLibMessage.Badges,
                _cheerBadge,
                _bits,
                _bitsInDollars);
        }
    }
}
