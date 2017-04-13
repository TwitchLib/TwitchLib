using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TwitchLib.Models.Client
{
    /// <summary>
    /// Class representing a received whisper from TwitchWhisperClient
    /// </summary>
    public class WhisperMessage
    {
        /// <summary>Property representing dynamic badges assigned to message.</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        /// <summary>Property representing HEX representation of color of username.</summary>
        public string ColorHex { get; protected set; }
        /// <summary>Property representing HEX color as a System.Drawing.Color object.</summary>
        public Color Color { get; protected set; }
        /// <summary>Property representing sender Username.</summary>
        public string Username { get; protected set; }
        /// <summary>Property representing sender DisplayName (can be null/blank).</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing list of string emotes in message.</summary>
        public EmoteSet EmoteSet { get; protected set; }
        /// <summary>Property representing identifier of the message thread.</summary>
        public string ThreadId { get; protected set; }
        /// <summary>Property representing message identifier.</summary>
        public long MessageId { get; protected set; }
        /// <summary>Property representing sender identifier.</summary>
        public long UserId { get; protected set; }
        /// <summary>Property representing whether or not sender has Turbo.</summary>
        public bool Turbo { get; protected set; }
        /// <summary>Property representing bot's username.</summary>
        public string BotUsername { get; protected set; }
        /// <summary>Property representing message contents.</summary>
        public string Message { get; protected set; }
        /// <summary>Property representing user type of sender.</summary>
        public Enums.UserType UserType { get; protected set; }

        /// <summary>
        /// WhisperMessage constructor.
        /// </summary>
        /// <param name="ircString">Received IRC string from Twitch server.</param>
        /// <param name="botUsername">Active bot username receiving message.</param>
        public WhisperMessage(string ircString, string botUsername)
        {
            Username = ircString.Split('!')[1].Split('@')[0];
            BotUsername = botUsername;
            Message = ircString.Replace($"{ircString.Split('!')[0]}!{Username}@{Username}.tmi.twitch.tv WHISPER {botUsername.ToLower()} :", "");

            foreach(string part in ircString.Split(';'))
            {
                string key = part.Split('=')[0];
                string value = part.Split('=')[1];
                switch(key)
                {
                    
                    case "@badges":
                        Badges = new List<KeyValuePair<string, string>>();
                        if (value.Contains('/'))
                        {
                            if (!value.Contains(","))
                                Badges.Add(new KeyValuePair<string, string>(value.Split('/')[0], value.Split('/')[1]));
                            else
                                foreach (string badge in value.Split(','))
                                    Badges.Add(new KeyValuePair<string, string>(badge.Split('/')[0], badge.Split('/')[1]));
                        }
                        break;

                    case "color":
                        ColorHex = value;
                        if (!string.IsNullOrEmpty(ColorHex))
                            Color = ColorTranslator.FromHtml(ColorHex);
                        break;

                    case "display-name":
                        DisplayName = value;
                        break;

                    case "emotes":
                        EmoteSet = new EmoteSet(value, Message);
                        break;

                    case "message-id":
                        MessageId = long.Parse(value);
                        break;

                    case "thread-id":
                        ThreadId = value;
                        break;

                    case "turbo":
                        if (value == "1")
                            Turbo = true;
                        break;

                    case "user-id":
                        UserId = long.Parse(value);
                        break;

                    case "user-type":
                        switch (part.Split('=')[1].Split(' ')[0])
                        {
                            case "global_mod":
                                UserType = Enums.UserType.GlobalModerator;
                                break;
                            case "admin":
                                UserType = Enums.UserType.Admin;
                                break;
                            case "staff":
                                UserType = Enums.UserType.Staff;
                                break;
                            default:
                                UserType = Enums.UserType.Viewer;
                                break;
                        }
                        break;
                }
            }
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}