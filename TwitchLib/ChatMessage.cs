using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TwitchLib
{
    /// <summary>Class represents ChatMessage in a Twitch channel.</summary>
    public class ChatMessage
    {
        private int _userId;
        private string _username, _displayName, _colorHex, _message, _channel, _emoteSet, _rawIrcMessage, _emoteReplacedMessage;
        private bool _subscriber, _turbo, _modFlag, _meFlag;
        private Common.UType _userType;
        private List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();
        private MessageEmoteCollection _emoteCollection;

        /// <summary>Twitch-unique integer assigned on per account basis.</summary>
        public int UserId => _userId;
        /// <summary>Username of sender of chat message.</summary>
        public string Username => _username;
        /// <summary>Case-sensitive username of sender of chat message.</summary>
        public string DisplayName => _displayName;
        /// <summary>Hex representation of username color in chat.</summary>
        public string ColorHex => _colorHex;
        /// <summary>Twitch chat message contents.</summary>
        public string Message => _message;
        /// <summary>User type can be viewer, moderator, global mod, admin, or staff</summary>
        public Common.UType UserType => _userType;
        /// <summary>Twitch channel message was sent from (useful for multi-channel bots).</summary>
        public string Channel => _channel;
        /// <summary>Channel specific subscriber status.</summary>
        public bool Subscriber => _subscriber;
        /// <summary>Twitch site-wide turbo status.</summary>
        public bool Turbo => _turbo;
        /// <summary>Channel specific moderator status.</summary>
        public bool ModFlag => _modFlag;
        /// <summary>Chat message /me identifier flag.</summary>
        public bool MeFlag => _meFlag;
        /// <summary>Raw IRC-style text received from Twitch.</summary>
        public string RawIrcMessage => _rawIrcMessage;
        /// <summary>Text after emotes have been handled (if desired). Will be null if replaceEmotes is false.</summary>
        public string EmoteReplacedMessage => _emoteReplacedMessage;
        /// <summary>List of key-value pair badges.</summary>
        public List<KeyValuePair<string,string>> Badges => _badges;

        //Example IRC message: @badges=moderator/1,warcraft/alliance;color=;display-name=Swiftyspiffyv4;emotes=;mod=1;room-id=40876073;subscriber=0;turbo=0;user-id=103325214;user-type=mod :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #swiftyspiffy :asd
        /// <summary>Constructor for ChatMessage object.</summary>
        /// <param name="ircString">The raw string received from Twitch to be processed.</param>
        /// <param name="emoteCollection">The <see cref="MessageEmoteCollection"/> to register new emotes on and, if desired, use for emote replacement.</param>
        /// <param name="replaceEmotes">Whether to replace emotes for this chat message. Defaults to false.</param>
        public ChatMessage(string ircString, ref MessageEmoteCollection emoteCollection, bool replaceEmotes = false)
        {
            _rawIrcMessage = ircString;
            _emoteCollection = emoteCollection;
            foreach (var part in ircString.Split(';'))
            {
                if (part.Contains("!"))
                {
                    if (_channel == null)
                        _channel = part.Split('#')[1].Split(' ')[0];
                    if (_username == null)
                        _username = part.Split('!')[1].Split('@')[0];
                }
                else if(part.Contains("@badges="))
                {
                    string badges = part.Split('=')[1];
                    if(badges.Contains('/'))
                    {
                        if (!badges.Contains(","))
                            _badges.Add(new KeyValuePair<string, string>(badges.Split('/')[0], badges.Split('/')[1]));
                        else
                            foreach (string badge in badges.Split(','))
                                _badges.Add(new KeyValuePair<string, string>(badge.Split('/')[0], badge.Split('/')[1]));
                    }
                }
                else if (part.Contains("color="))
                {
                    if (_colorHex == null)
                        _colorHex = part.Split('=')[1];
                }
                else if (part.Contains("display-name"))
                {
                    if (_displayName == null)
                        _displayName = part.Split('=')[1];
                }
                else if (part.Contains("emotes="))
                {
                    if (_emoteSet == null)
                    {
                        _emoteSet = part.Split('=')[1]; ;
                    }

                }
                else if (part.Contains("subscriber="))
                {
                    _subscriber = part.Split('=')[1] == "1";
                }
                else if (part.Contains("turbo="))
                {
                    _turbo = part.Split('=')[1] == "1";
                }
                else if (part.Contains("user-id="))
                {
                    _userId = int.Parse(part.Split('=')[1]);
                }
                else if (part.Contains("user-type="))
                {
                    switch (part.Split('=')[1].Split(' ')[0])
                    {
                        case "mod":
                            _userType = Common.UType.Moderator;
                            break;
                        case "global_mod":
                            _userType = Common.UType.GlobalModerator;
                            break;
                        case "admin":
                            _userType = Common.UType.Admin;
                            break;
                        case "staff":
                            _userType = Common.UType.Staff;
                            break;
                        default:
                            _userType = Common.UType.Viewer;
                            break;
                    }
                }
                else if (part.Contains("mod="))
                {
                    _modFlag = part.Split('=')[1] == "1";
                }
            }
            _message = ircString.Split(new[] {$" PRIVMSG #{_channel} :"}, StringSplitOptions.None)[1];
            if ((byte)_message[0] == 1 && (byte)_message[_message.Length-1] == 1)
            {
              //Actions (/me {action}) are wrapped by byte=1 and prepended with "ACTION "
              //This setup clears all of that leaving just the action's text.
              //If you want to clear just the nonstandard bytes, use:
              //_message = _message.Substring(1, text.Length-2);
              _message = _message.Substring(8, _message.Length-9);
              _meFlag = true;
            }

            //Parse the emoteSet
            if (_emoteSet != null && _message != null)
            {
                string[] uniqueEmotes = _emoteSet.Split('/');
                string id, text;
                int firstColon, firstComma, firstDash, low, high;
                foreach (string emote in uniqueEmotes)
                {
                    firstColon = emote.IndexOf(':');
                    firstComma = emote.IndexOf(',');
                    if (firstComma == -1) firstComma = emote.Length;
                    firstDash = emote.IndexOf('-');
                    if (firstColon > 0 && firstDash > firstColon && firstComma > firstDash)
                    {
                        if (Int32.TryParse(emote.Substring(firstColon + 1, (firstDash - firstColon) - 1), out low) &&
                            Int32.TryParse(emote.Substring(firstDash + 1, (firstComma - firstDash) - 1), out high))
                        {
                            if (low >= 0 && low < high && high < _message.Length)
                            {
                                //Valid emote, let's parse
                                id = emote.Substring(0, firstColon);
                                //Pull the emote text from the message
                                text = _message.Substring(low, (high - low) + 1);
                                _emoteCollection.Add(new MessageEmote(id, text));
                            }
                        }
                    }
                }
                if (replaceEmotes)
                {
                    _emoteReplacedMessage = _emoteCollection.ReplaceEmotes(_message);
                }
            }
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}