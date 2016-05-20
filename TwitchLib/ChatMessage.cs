using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TwitchLib
{
    //Should be fully functional
    public class ChatMessage
    {
        public enum UType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private int _userId;
        private string _username, _displayName, _colorHex, _message, _channel, _emoteSet, _rawIrcMessage;
        private bool _subscriber, _turbo, _modFlag;
        private UType _userType;

        public int UserId => _userId;
        public string Username => _username;
        public string DisplayName => _displayName;
        public string ColorHex => _colorHex;
        public string Message => _message;
        public UType UserType => _userType;
        public string Channel => _channel;
        public bool Subscriber => _subscriber;
        public bool Turbo => _turbo;
        public bool ModFlag => _modFlag;
        public string RawIrcMessage => _rawIrcMessage;

        //@color=#CC00C9;display-name=astickgamer;emotes=70803:6-11;sent-ts=1447446917994;subscriber=1;tmi-sent-ts=1447446957359;turbo=0;user-id=24549902;user-type= :astickgamer!astickgamer@astickgamer.tmi.twitch.tv PRIVMSG #cohhcarnage :cjb2, cohhHi
        public ChatMessage(string ircString)
        {
            _rawIrcMessage = ircString;
            //
            //@color=asd;display-name=Swiftyspiffyv4;emotes=;subscriber=0;turbo=0;user-id=103325214;user-type=asd :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #burkeblack :this is a test lol
            foreach (string part in ircString.Split(';'))
            {
                if (part.Contains("!"))
                {
                    if (_channel == null)
                        _channel = part.Split('#')[1].Split(' ')[0];
                    if (_username == null)
                        _username = part.Split('!')[1].Split('@')[0];
                    continue;
                }
                if (part.Contains("@color="))
                {
                    if (_colorHex == null)
                        _colorHex = part.Split('=')[1];
                    continue;
                }
                if (part.Contains("display-name"))
                {
                    if (_displayName == null)
                        _displayName = part.Split('=')[1];
                    continue;
                }
                if (part.Contains("emotes="))
                {
                    if (_emoteSet == null)
                        _emoteSet = part.Split('=')[1];
                    continue;
                }
                if (part.Contains("subscriber="))
                {
                    _subscriber = part.Split('=')[1] == "1";
                    continue;
                }
                if (part.Contains("turbo="))
                {
                    _turbo = part.Split('=')[1] == "1";
                    continue;
                }
                if (part.Contains("user-id="))
                {
                    _userId = int.Parse(part.Split('=')[1]);
                    continue;
                }
                if (part.Contains("user-type="))
                {
                    switch (part.Split('=')[1].Split(' ')[0])
                    {
                        case "mod":
                            _userType = UType.Moderator;
                            break;
                        case "global_mod":
                            _userType = UType.GlobalModerator;
                            break;
                        case "admin":
                            _userType = UType.Admin;
                            break;
                        case "staff":
                            _userType = UType.Staff;
                            break;
                        default:
                            _userType = UType.Viewer;
                            break;
                    }
                    continue;
                }
                if (part.Contains("mod="))
                {
                    _modFlag = part.Split('=')[1] == "1";
                    continue;
                }
            }
            _message = ircString.Split(new string[] {$" PRIVMSG #{_channel} :"}, StringSplitOptions.None)[1];
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}