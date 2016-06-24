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
        private int _userId;
        private string _username, _displayName, _colorHex, _message, _channel, _emoteSet, _rawIrcMessage;
        private bool _subscriber, _turbo, _modFlag;
        private Common.UType _userType;
        private List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();

        public int UserId => _userId;
        public string Username => _username;
        public string DisplayName => _displayName;
        public string ColorHex => _colorHex;
        public string Message => _message;
        public Common.UType UserType => _userType;
        public string Channel => _channel;
        public bool Subscriber => _subscriber;
        public bool Turbo => _turbo;
        public bool ModFlag => _modFlag;
        public string RawIrcMessage => _rawIrcMessage;
        public List<KeyValuePair<string,string>> Badges => _badges;

        //Example IRC message: @badges=moderator/1,warcraft/alliance;color=;display-name=Swiftyspiffyv4;emotes=;mod=1;room-id=40876073;subscriber=0;turbo=0;user-id=103325214;user-type=mod :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #swiftyspiffy :asd
        public ChatMessage(string ircString)
        {
            _rawIrcMessage = ircString;
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
                        _emoteSet = part.Split('=')[1];
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
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}