using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPIClasses
{
    public class Chatter
    {
        public enum UType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private string _username;
        private UType _userType;

        public string Username => _username;
        public UType UserType => _userType;

        public Chatter(string username, UType userType)
        {
            _username = username;
            _userType = userType;
        }
    }
}