using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class Chatter
    {
        public enum uType
        {
            Viewer,
            Moderator,
            Global_Moderator,
            Admin,
            Staff
        }

        private string username;
        private uType userType;
        
        public string Username { get { return username; } }
        public uType UserType { get { return userType; } }

        public Chatter(string username, uType userType)
        {
            this.username = username;
            this.userType = userType;
        }
    }
}
