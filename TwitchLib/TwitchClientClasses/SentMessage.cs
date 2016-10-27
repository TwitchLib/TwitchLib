using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchClientClasses
{
    public class SentMessage
    {
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        public string Channel { get; protected set; }
        public string ColorHex { get; protected set; }
        public string DisplayName { get; protected set; }
        public string EmoteSet { get; protected set; }
        public bool IsModerator { get; protected set; }
        public bool IsSubscriber { get; protected set; }
        public Common.UserType UserType { get; protected set; }
        public string Message { get; protected set; }

        public SentMessage(UserState state, string message)
        {
            Badges = state.Badges;
            Channel = state.Channel;
            ColorHex = state.ColorHex;
            DisplayName = state.DisplayName;
            EmoteSet = state.EmoteSet;
            IsModerator = state.Moderator;
            IsSubscriber = state.Subscriber;
            UserType = state.UserType;
            Message = message;
        }
    }
}
