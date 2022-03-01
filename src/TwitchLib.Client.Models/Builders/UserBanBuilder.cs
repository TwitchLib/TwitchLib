namespace TwitchLib.Client.Models.Builders
{
    public sealed class UserBanBuilder : IBuilder<UserBan>, IFromIrcMessageBuilder<UserBan>
    {
        private string _channelName;
        private string _userName;
        private string _banReason;
        private string _roomId;
        private string _targetUserId;

        public static UserBanBuilder Create()
        {
            return new UserBanBuilder();
        }

        private UserBanBuilder()
        {
        }

        public UserBanBuilder WithChannel(string channel)
        {
            _channelName = channel;
            return this;
        }

        public UserBanBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public UserBanBuilder WithBanReason(string banReason)
        {
            _banReason = banReason;
            return this;
        }

        public UserBanBuilder WithRoomId(string roomId)
        {
            _roomId = roomId;
            return this;
        }

        public UserBanBuilder WithTargetUserId(string targetUserId)
        {
            _targetUserId = targetUserId;
            return this;
        }

        public UserBan BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new UserBan(fromIrcMessageBuilderDataObject.Message);
        }

        public UserBan Build()
        {
            return new UserBan(_channelName, _userName, _banReason, _roomId, _targetUserId);
        }
    }
}
