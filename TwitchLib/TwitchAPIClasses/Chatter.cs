using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
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
        
        public string Username { get { return _username; } }
        public UType UserType { get { return _userType; } }

        public Chatter(string username, UType userType)
        {
            this._username = username;
            this._userType = userType;
        }
    }
}
