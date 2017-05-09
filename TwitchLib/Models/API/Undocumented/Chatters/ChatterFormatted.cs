using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.Chatters
{
    public class ChatterFormatted
    {
        public string Username { get; protected set; }
        public Enums.UserType UserType { get; protected set; }

        public ChatterFormatted(string username, Enums.UserType userType)
        {
            Username = username;
            UserType = userType;
        }
    }
}
