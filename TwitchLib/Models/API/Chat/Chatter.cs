using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Chat
{
    /// <summary>Class representing a chatter as returned by the Twitch Api.</summary>
    public class Chatter
    {
        /// <summary>Property representing the username of a chatter.</summary>
        public string Username { get; protected set; }
        /// <summary>Property representing the user type of a chatter.</summary>
        public Enums.UserType UserType { get; protected set; }

        /// <summary>Constructor of Chatter.</summary>
        public Chatter(string username, Enums.UserType userType)
        {
            Username = username;
            UserType = userType;
        }
    }
}