using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TwitchLib
{

    class ChatMessage
    {
        public enum uType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private bool parsedCorrectly = false;

        private int userID;
        private string username, displayName, colorHEX, message, channel, emoteSet;
        private bool subscriber, turbo;
        private uType userType;

        public bool ParsedCorrectly { get { return parsedCorrectly; } }
        public int UserID { get { return userID; } }
        public string Username { get { return username; } }
        public string DisplayName { get { return displayName; } }
        public string ColorHEX { get { return colorHEX; } }
        public string Message { get { return message; } }
        public uType UserType { get { return userType; } }
        public string Channel { get { return channel; } }

        public ChatMessage(string IRCString)
        {
            //@color=asd;display-name=Swiftyspiffyv4;emotes=;subscriber=0;turbo=0;user-id=103325214;user-type=asd :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #burkeblack :this is a test lol
            colorHEX = IRCString.Split(';')[0].Split('=')[1];
            username = IRCString.Split('@')[2].Split('.')[0];
            displayName = IRCString.Split(';')[1].Split('=')[1];
            emoteSet = IRCString.Split(';')[2].Split('=')[1];
            string subscriberStr = IRCString.Split(';')[3].Split('=')[1];
            string turboStr = IRCString.Split(';')[4].Split('=')[1];
            string userIDStr = IRCString.Split(';')[5].Split('=')[1];
            string userTypeStr = IRCString.Split(';')[6].Split(':')[0].Split('=')[1].Replace(" ", String.Empty);
            if (IRCString.Split('#').Count() == 3)
            {
                channel = IRCString.Split('#')[2].Split(' ')[0];
                message = IRCString.Replace(IRCString.Split('#')[0] + "#" + IRCString.Split('#')[1] + "#" + channel + " :", "");
            }
            else
            {
                channel = IRCString.Split('#')[1].Split(' ')[0];
                message = IRCString.Replace(IRCString.Split('#')[0] + "#" + channel + " :", "");
            }
            subscriber = convertToBool(subscriberStr);
            turbo = convertToBool(turboStr);

            userID = int.Parse(userIDStr);

            switch (userTypeStr)
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
        }

        private bool convertToBool(string data)
        {
            if (data == "1")
                return true;
            return false;
        }
    }
}
