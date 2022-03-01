using System.Collections.Generic;
using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Internal.Parsing
{
    /// <summary>
    /// Class IrcParser.
    /// </summary>
    internal class IrcParser
    {

        /// <summary>
        /// Builds an IrcMessage from a raw string
        /// </summary>
        /// <param name="raw">Raw IRC message</param>
        /// <returns>IrcMessage object</returns>
        public IrcMessage ParseIrcMessage(string raw)
        {
            Dictionary<string, string> tagDict = new Dictionary<string, string>();

            ParserState state = ParserState.STATE_NONE;
            int[] starts = new[] { 0, 0, 0, 0, 0, 0 };
            int[] lens = new[] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < raw.Length; ++i)
            {
                lens[(int)state] = i - starts[(int)state] - 1;
                if (state == ParserState.STATE_NONE && raw[i] == '@')
                {
                    state = ParserState.STATE_V3;
                    starts[(int)state] = ++i;

                    int start = i;
                    string key = null;
                    for (; i < raw.Length; ++i)
                    {
                        if (raw[i] == '=')
                        {
                            key = raw.Substring(start, i - start);
                            start = i + 1;
                        }
                        else if (raw[i] == ';')
                        {
                            if (key == null)
                                tagDict[raw.Substring(start, i - start)] = "1";
                            else
                                tagDict[key] = raw.Substring(start, i - start);
                            start = i + 1;
                        }
                        else if (raw[i] == ' ')
                        {
                            if (key == null)
                                tagDict[raw.Substring(start, i - start)] = "1";
                            else
                                tagDict[key] = raw.Substring(start, i - start);
                            break;
                        }
                    }
                }
                else if (state < ParserState.STATE_PREFIX && raw[i] == ':')
                {
                    state = ParserState.STATE_PREFIX;
                    starts[(int)state] = ++i;
                }
                else if (state < ParserState.STATE_COMMAND)
                {
                    state = ParserState.STATE_COMMAND;
                    starts[(int)state] = i;
                }
                else if (state < ParserState.STATE_TRAILING && raw[i] == ':')
                {
                    state = ParserState.STATE_TRAILING;
                    starts[(int)state] = ++i;
                    break;
                }
                else if (state < ParserState.STATE_TRAILING && raw[i] == '+' || state < ParserState.STATE_TRAILING && raw[i] == '-')
                {
                    state = ParserState.STATE_TRAILING;
                    starts[(int)state] = i;
                    break;
                }
                else if (state == ParserState.STATE_COMMAND)
                {
                    state = ParserState.STATE_PARAM;
                    starts[(int)state] = i;
                }

                while (i < raw.Length && raw[i] != ' ')
                    ++i;
            }

            lens[(int)state] = raw.Length - starts[(int)state];
            string cmd = raw.Substring(starts[(int)ParserState.STATE_COMMAND],
                lens[(int)ParserState.STATE_COMMAND]);

            IrcCommand command = IrcCommand.Unknown;
            switch (cmd)
            {
                case "PRIVMSG":
                    command = IrcCommand.PrivMsg;
                    break;
                case "NOTICE":
                    command = IrcCommand.Notice;
                    break;
                case "PING":
                    command = IrcCommand.Ping;
                    break;
                case "PONG":
                    command = IrcCommand.Pong;
                    break;
                case "HOSTTARGET":
                    command = IrcCommand.HostTarget;
                    break;
                case "CLEARCHAT":
                    command = IrcCommand.ClearChat;
                    break;
                case "CLEARMSG":
                    command = IrcCommand.ClearMsg;
                    break;
                case "USERSTATE":
                    command = IrcCommand.UserState;
                    break;
                case "GLOBALUSERSTATE":
                    command = IrcCommand.GlobalUserState;
                    break;
                case "NICK":
                    command = IrcCommand.Nick;
                    break;
                case "JOIN":
                    command = IrcCommand.Join;
                    break;
                case "PART":
                    command = IrcCommand.Part;
                    break;
                case "PASS":
                    command = IrcCommand.Pass;
                    break;
                case "CAP":
                    command = IrcCommand.Cap;
                    break;
                case "001":
                    command = IrcCommand.RPL_001;
                    break;
                case "002":
                    command = IrcCommand.RPL_002;
                    break;
                case "003":
                    command = IrcCommand.RPL_003;
                    break;
                case "004":
                    command = IrcCommand.RPL_004;
                    break;
                case "353":
                    command = IrcCommand.RPL_353;
                    break;
                case "366":
                    command = IrcCommand.RPL_366;
                    break;
                case "372":
                    command = IrcCommand.RPL_372;
                    break;
                case "375":
                    command = IrcCommand.RPL_375;
                    break;
                case "376":
                    command = IrcCommand.RPL_376;
                    break;
                case "WHISPER":
                    command = IrcCommand.Whisper;
                    break;
                case "SERVERCHANGE":
                    command = IrcCommand.ServerChange;
                    break;
                case "RECONNECT":
                    command = IrcCommand.Reconnect;
                    break;
                case "ROOMSTATE":
                    command = IrcCommand.RoomState;
                    break;
                case "USERNOTICE":
                    command = IrcCommand.UserNotice;
                    break;
                case "MODE":
                    command = IrcCommand.Mode;
                    break;
            }

            string parameters = raw.Substring(starts[(int)ParserState.STATE_PARAM],
                lens[(int)ParserState.STATE_PARAM]);
            string message = raw.Substring(starts[(int)ParserState.STATE_TRAILING],
                lens[(int)ParserState.STATE_TRAILING]);
            string hostmask = raw.Substring(starts[(int)ParserState.STATE_PREFIX],
                lens[(int)ParserState.STATE_PREFIX]);
            return new IrcMessage(command, new[] { parameters, message }, hostmask, tagDict);
        }

        /// <summary>
        /// Enum ParserState
        /// </summary>
        private enum ParserState
        {
            /// <summary>
            /// The state none
            /// </summary>
            STATE_NONE,
            /// <summary>
            /// The state v3
            /// </summary>
            STATE_V3,
            /// <summary>
            /// The state prefix
            /// </summary>
            STATE_PREFIX,
            /// <summary>
            /// The state command
            /// </summary>
            STATE_COMMAND,
            /// <summary>
            /// The state parameter
            /// </summary>
            STATE_PARAM,
            /// <summary>
            /// The state trailing
            /// </summary>
            STATE_TRAILING
        };
    }
}
