using System.Collections.Generic;
using System.Text;

using TwitchLib.Client.Enums.Internal;

namespace TwitchLib.Client.Models.Internal
{
    public class IrcMessage
    {
        /// <summary>
        /// The channel the message was sent in
        /// </summary>
        public string Channel => Params.StartsWith("#") ? Params.Remove(0, 1) : Params;

        public string Params => _parameters != null && _parameters.Length > 0 ? _parameters[0] : "";

        /// <summary>
        /// Message itself
        /// </summary>
        public string Message => Trailing;

        public string Trailing => _parameters != null && _parameters.Length > 1 ? _parameters[_parameters.Length - 1] : "";

        /// <summary>
        /// Command parameters
        /// </summary>
        private readonly string[] _parameters;

        /// <summary>
        /// The user whose message it is
        /// </summary>
        public readonly string User;

        /// <summary>
        /// Hostmask of the user
        /// </summary>
        public readonly string Hostmask;

        /// <summary>
        /// Raw Command
        /// </summary>
        public readonly IrcCommand Command;

        /// <summary>
        /// IRCv3 tags
        /// </summary>
        public readonly Dictionary<string, string> Tags;

        /// <summary>
        /// Create an INCOMPLETE IrcMessage only carrying username
        /// </summary>
        /// <param name="user"></param>
        public IrcMessage(string user)
        {
            _parameters = null;
            User = user;
            Hostmask = null;
            Command = IrcCommand.Unknown;
            Tags = null;
        }

        /// <summary>
        /// Create an IrcMessage
        /// </summary>
        /// <param name="command">IRC Command</param>
        /// <param name="parameters">Command params</param>
        /// <param name="hostmask">User</param>
        /// <param name="tags">IRCv3 tags</param>
        public IrcMessage(
            IrcCommand command,
            string[] parameters,
            string hostmask,
            Dictionary<string, string> tags = null)
        {
            var idx = hostmask.IndexOf('!');
            User = idx != -1 ? hostmask.Substring(0, idx) : hostmask;
            Hostmask = hostmask;
            _parameters = parameters;
            Command = command;
            Tags = tags;
        }

        public new string ToString()
        {
            var raw = new StringBuilder(32);
            if (Tags != null)
            {
                var tags = new string[Tags.Count];
                var i = 0;
                foreach (var tag in Tags)
                {
                    tags[i] = tag.Key + "=" + tag.Value;
                    ++i;
                }

                if (tags.Length > 0)
                {
                    raw.Append("@").Append(string.Join(";", tags)).Append(" ");
                }
            }

            if (!string.IsNullOrEmpty(Hostmask))
            {
                raw.Append(":").Append(Hostmask).Append(" ");
            }

            raw.Append(Command.ToString().ToUpper().Replace("RPL_", ""));
            if (_parameters.Length <= 0)
                return raw.ToString();

            if (_parameters[0] != null && _parameters[0].Length > 0)
            {
                raw.Append(" ").Append(_parameters[0]);
            }

            if (_parameters.Length > 1 && _parameters[1] != null && _parameters[1].Length > 0)
            {
                raw.Append(" :").Append(_parameters[1]);
            }

            return raw.ToString();
        }
    }
}