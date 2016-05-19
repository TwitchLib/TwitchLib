using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class UserState
    {
        public enum UType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private string _colorHex, _displayName, _emoteSet, _channel;
        private bool _subscriber = false;
        private bool _turbo = false;
        private UType _userType;

        // Reversing issue noticed by SimpleVar
        public string ColorHex => _colorHex;
        public string DisplayName => _displayName;
        public string EmoteSet => _emoteSet;
        public string Channel => _channel;
        public bool Subscriber => _subscriber;
        public bool Turbo => _turbo;
        public UType UserType => _userType;

        public UserState(string ircString)
        {
            _colorHex = ircString.Split(';')[0].Contains("#") ? ircString.Split(';')[0].Split('#')[1] : "";
            _displayName = ircString.Split(';')[1].Split('=')[1];
            _emoteSet = ircString.Split(';')[2].Split('=')[1];
            if (ircString.Split(';')[3].Split('=')[1] == "1")
                _subscriber = true;
            if (ircString.Split(';')[4].Split('=')[1] == "1")
                _turbo = true;
            switch(ircString.Split('=')[6].Split(' ')[0])
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
            _channel = ircString.Split(' ')[3].Replace("#", "");
        }
    }
}
