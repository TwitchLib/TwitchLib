using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ReSubscriber
    {
        private List<KeyValuePair<string, string>> _badges = new List<KeyValuePair<string, string>>();
        private string _colorHex, _displayName, _emoteSet, _login, _systemMessage, _rawIrc, _channel;
        private string _resubMessage = "";
        private int _months, _roomId, _userId;
        private bool _mod, _turbo, _sub;
        private Common.UserType _userType;
        private List<KeyValuePair<string, string>> _unknownProperties = new List<KeyValuePair<string, string>>();

        public List<KeyValuePair<string, string>> Badges => _badges;
        public string ColorHex => _colorHex;
        public string DisplayName => _displayName;
        public string EmoteSet => _emoteSet;
        public string Login => _login;
        public string SystemMessage => _systemMessage;
        public string ResubMessage => _resubMessage;
        public int Months => _months;
        public int RoomId => _roomId;
        public int UserId => _userId;
        public bool Mod => _mod;
        public bool Turbo => _turbo;
        public bool Sub => _sub;
        public Common.UserType UserType => _userType;
        public string RawIrc => _rawIrc;
        public List<KeyValuePair<string, string>> UnknownProperties => _unknownProperties;
        public string Channel => _channel;

        // @badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG
        public ReSubscriber(string ircString)
        {
            _rawIrc = ircString;
            foreach(string section in ircString.Split(';'))
            {
                if(section.Contains("="))
                {
                    string key = section.Split('=')[0];
                    string value = section.Split('=')[1];
                    switch (key)
                    {
                        case "@badges":
                            foreach (string badgeValue in value.Split(','))
                                _badges.Add(new KeyValuePair<string, string>(badgeValue.Split('/')[0], badgeValue.Split('/')[1]));
                            break;
                        case "color":
                            _colorHex = value;
                            break;
                        case "display-name":
                            _displayName = value;
                            break;
                        case "emotes":
                            _emoteSet = value;
                            break;
                        case "login":
                            _login = value;
                            break;
                        case "mod":
                            _mod = value == "1";
                            break;
                        case "msg-param-months":
                            _months = int.Parse(value);
                            break;
                        case "room-id":
                            _roomId = int.Parse(value);
                            break;
                        case "subscriber":
                            _sub = value == "1";
                            break;
                        case "system-msg":
                            _systemMessage = value;
                            break;
                        case "turbo":
                            _turbo = value == "1";
                            break;
                        case "user-id":
                            _userId = int.Parse(value);
                            break;
                    }
                }
            }
            // Parse user-type
            if(ircString.Contains("=") && ircString.Contains(" "))
            {
                switch (ircString.Split(' ')[0].Split(';')[13].Split('=')[1])
                {
                    case "mod":
                        _userType = Common.UserType.Moderator;
                        break;
                    case "global_mod":
                        _userType = Common.UserType.GlobalModerator;
                        break;
                    case "admin":
                        _userType = Common.UserType.Admin;
                        break;
                    case "staff":
                        _userType = Common.UserType.Staff;
                        break;
                    default:
                        _userType = Common.UserType.Viewer;
                        break;
                }
            }


            // Parse channel
            if (ircString.Contains("#") && ircString.Split('#').Count() > 2)
                if (ircString.Split('#')[2].Contains(" "))
                    _channel = ircString.Split('#')[2].Split(' ')[0];
                else
                    _channel = ircString.Split('#')[2];

            // Parse sub message
            if(ircString.Contains($"{ircString.Split(':')[0]}:{ircString.Split(':')[1]}:"))
                _resubMessage = ircString.Replace($"{ircString.Split(':')[0]}:{ircString.Split(':')[1]}:", String.Empty);
        }

        public override string ToString()
        {
            return $"Badges: {Badges.Count}, color hex: {ColorHex}, display name: {DisplayName}, emote set: {EmoteSet}, login: {Login}, system message: {SystemMessage}, " + 
                $"resub message: {ResubMessage}, months: {Months}, room id: {RoomId}, user id: {UserId}, mod: {Mod}, turbo: {Turbo}, sub: {Sub}, user type: {UserType}, " + 
                $"channel: {Channel}, unknown properties: {UnknownProperties.Count}, raw irc: {RawIrc}";
        }
    }
}
