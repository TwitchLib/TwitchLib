using System;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class ChannelStateBuilder : IBuilder<ChannelState>, IFromIrcMessageBuilder<ChannelState>
    {
        private string _broadcasterLanguage;
        private string _channel;
        private bool _emoteOnly;
        private TimeSpan _followersOnly;
        private bool _mercury;
        private bool _r9K;
        private bool _rituals;
        private string _roomId;
        private int _slowMode;
        private bool _subOnly;

        private ChannelStateBuilder()
        {
        }

        public ChannelStateBuilder WithBroadcasterLanguage(string broadcasterLanguage)
        {
            _broadcasterLanguage = broadcasterLanguage;
            return this;
        }

        public ChannelStateBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public ChannelStateBuilder WithEmoteOnly(bool emoteOnly)
        {
            _emoteOnly = emoteOnly;
            return this;
        }

        public ChannelStateBuilder WIthFollowersOnly(TimeSpan followersOnly)
        {
            _followersOnly = followersOnly;
            return this;
        }

        public ChannelStateBuilder WithMercury(bool mercury)
        {
            _mercury = mercury;
            return this;
        }

        public ChannelStateBuilder WithR9K(bool r9k)
        {
            _r9K = r9k;
            return this;
        }

        public ChannelStateBuilder WithRituals(bool rituals)
        {
            _rituals = rituals;
            return this;
        }

        public ChannelStateBuilder WithRoomId(string roomId)
        {
            _roomId = roomId;
            return this;
        }

        public ChannelStateBuilder WithSlowMode(int slowMode)
        {
            _slowMode = slowMode;
            return this;
        }

        public ChannelStateBuilder WithSubOnly(bool subOnly)
        {
            _subOnly = subOnly;
            return this;
        }

        public static ChannelStateBuilder Create()
        {
            return new ChannelStateBuilder();
        }

        public ChannelState Build()
        {
            return new ChannelState(
                _r9K,
                _rituals,
                _subOnly,
                _slowMode,
                _emoteOnly,
                _broadcasterLanguage,
                _channel,
                _followersOnly,
                _mercury,
                _roomId);
        }

        public ChannelState BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new ChannelState(fromIrcMessageBuilderDataObject.Message);
        }
    }
}
