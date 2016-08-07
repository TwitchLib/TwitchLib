using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    /// <summary>Class representing state of a specific user.</summary>
    public class UserState
    {
        /// <summary>Properrty representing HEX user's name.</summary>
        public string ColorHex { get; protected set; }
        /// <summary>Property representing user's display name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing emote sets available to user.</summary>
        public string EmoteSet { get; protected set; }
        /// <summary>Property representing channel.</summary>
        public string Channel { get; protected set; }
        /// <summary>Property representing subscriber status.</summary>
        public bool Subscriber { get; protected set; }
        /// <summary>Property representing Turbo status.</summary>
        public bool Turbo { get; protected set; }
        /// <summary>Property representing returned user type of user.</summary>
        public Common.UserType UserType { get; protected set; }

        /// <summary>
        /// Constructor for UserState.
        /// </summary>
        /// <param name="ircString"></param>
        public UserState(string ircString)
        {
            ColorHex = ircString.Split(';')[0].Contains("#") ? ircString.Split(';')[0].Split('#')[1] : "";
            DisplayName = ircString.Split(';')[1].Split('=')[1];
            EmoteSet = ircString.Split(';')[2].Split('=')[1];
            if (ircString.Split(';')[3].Split('=')[1] == "1")
                Subscriber = true;
            if (ircString.Split(';')[4].Split('=')[1] == "1")
                Turbo = true;
            switch (ircString.Split('=')[6].Split(' ')[0])
            {
                case "mod":
                    UserType = Common.UserType.Moderator;
                    break;

                case "global_mod":
                    UserType = Common.UserType.GlobalModerator;
                    break;

                case "admin":
                    UserType = Common.UserType.Admin;
                    break;

                case "staff":
                    UserType = Common.UserType.Staff;
                    break;

                default:
                    UserType = Common.UserType.Viewer;
                    break;
            }
            Channel = ircString.Split(' ')[3].Replace("#", "");
        }
    }
}