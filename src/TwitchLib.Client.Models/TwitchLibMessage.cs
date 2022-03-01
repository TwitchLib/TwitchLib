using System.Collections.Generic;
using System.Drawing;

using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models
{
    /// <summary>Class represents Message.</summary>
    public abstract class TwitchLibMessage
    {
        /// <summary>List of key-value pair badges.</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; }

        /// <summary>Twitch username of the bot that received the message.</summary>
        public string BotUsername { get; protected set; }

        /// <summary>Property representing HEX color as a System.Drawing.Color object.</summary>
        public Color Color { get; protected set; }

        /// <summary>Hex representation of username color in chat (THIS CAN BE NULL IF VIEWER HASN'T SET COLOR).</summary>
        public string ColorHex { get; protected set; }

        /// <summary>Case-sensitive username of sender of chat message.</summary>
        public string DisplayName { get; protected set; }

        /// <summary>Emote Ids that exist in message.</summary>
        public EmoteSet EmoteSet { get; protected set; }

        /// <summary>Twitch site-wide turbo status.</summary>
        public bool IsTurbo { get; protected set; }

        /// <summary>Twitch-unique integer assigned on per account basis.</summary>
        public string UserId { get; protected set; }

        /// <summary>Username of sender of chat message.</summary>
        public string Username { get; protected set; }

        /// <summary>User type can be viewer, moderator, global mod, admin, or staff</summary>
        public UserType UserType { get; protected set; }
        
        /// <summary>Raw IRC-style text received from Twitch.</summary>
        public string RawIrcMessage { get; protected set; }
    }
}
