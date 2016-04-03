using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class UserState
    {
        public enum uType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private string colorHEX, displayName, emoteSet, channel;
        private bool subscriber = false;
        private bool turbo = false;
        private uType userType;

        //Revursing issue noticed by SimpleVar
        public string ColorHEX { get { return colorHEX; } }
        public string DisplayName { get { return displayName; } }
        public string EmoteSet { get { return emoteSet; } }
        public string Channel { get { return channel; } }
        public bool Subscriber { get { return subscriber; } }
        public bool Turbo { get { return turbo; } }
        public uType UserType { get { return userType; } }

        public UserState(string IRCString)
        {
            if (IRCString.Split(';')[0].Contains("#"))
                colorHEX = IRCString.Split(';')[0].Split('#')[1];
            else
                colorHEX = "";
            displayName = IRCString.Split(';')[1].Split('=')[1];
            emoteSet = IRCString.Split(';')[2].Split('=')[1];
            if (IRCString.Split(';')[3].Split('=')[1] == "1")
                subscriber = true;
            if (IRCString.Split(';')[4].Split('=')[1] == "1")
                turbo = true;
            switch(IRCString.Split('=')[6].Split(' ')[0])
            {
                case "mod":
                    userType = uType.Moderator;
                    break;

                case "global_mod":
                    userType = uType.GlobalModerator;
                    break;

                case "admin":
                    userType = uType.Admin;
                    break;

                case "staff":
                    userType = uType.Staff;
                    break;

                default:
                    userType = uType.Viewer;
                    break;
            }
            channel = IRCString.Split(' ')[3].Replace("#", "");
        }
    }
}
