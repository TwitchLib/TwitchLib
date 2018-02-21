using System.Collections.Generic;

namespace TwitchLib.Client.Models.Client
{
    /// <summary>Model representing a sent message.</summary>
    public class SentMessage
    {
        /// <summary>Badges the sender has</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        /// <summary>Channel the sent message was sent from.</summary>
        public string Channel { get; protected set; }
        /// <summary>Sender's name color.</summary>
        public string ColorHex { get; protected set; }
        /// <summary>Display name of the sender.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Emotes that appear in the sent message.</summary>
        public string EmoteSet { get; protected set; }
        /// <summary>Whether or not the sender is a moderator.</summary>
        public bool IsModerator { get; protected set; }
        /// <summary>Whether or not the sender is a subscriber.</summary>
        public bool IsSubscriber { get; protected set; }
        /// <summary>The type of user (admin, broadcaster, viewer, moderator)</summary>
        public Enums.UserType UserType { get; protected set; }
        /// <summary>The message contents.</summary>
        public string Message { get; protected set; }

        /// <summary>Model constructor.</summary>
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
