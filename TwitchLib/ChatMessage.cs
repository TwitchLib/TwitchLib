using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TwitchLib
{
    //Should be fully functional
    public class ChatMessage
    {
        public enum uType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private int userID;
        private string username, displayName, colorHEX, message, channel, emoteSet;
        private bool subscriber, turbo, modFlag;
        private uType userType;

        public int UserID { get { return userID; } }
        public string Username { get { return username; } }
        public string DisplayName { get { return displayName; } }
        public string ColorHEX { get { return colorHEX; } }
        public string Message { get { return message; } }
        public uType UserType { get { return userType; } }
        public string Channel { get { return channel; } }
        public bool Subscriber { get { return subscriber; } }
        public bool Turbo { get { return turbo; } }
        public bool ModFlag { get { return modFlag; } }

        //@color=#CC00C9;display-name=astickgamer;emotes=70803:6-11;sent-ts=1447446917994;subscriber=1;tmi-sent-ts=1447446957359;turbo=0;user-id=24549902;user-type= :astickgamer!astickgamer@astickgamer.tmi.twitch.tv PRIVMSG #cohhcarnage :cjb2, cohhHi
        public ChatMessage(string IRCString)
        {
            string userTypeStr = "";
            //@color=asd;display-name=Swiftyspiffyv4;emotes=;subscriber=0;turbo=0;user-id=103325214;user-type=asd :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #burkeblack :this is a test lol
            foreach(string part in IRCString.Split(';'))
            {
                if(part.Contains("!"))
                {
                    channel = part.Split('#')[1].Split(' ')[0];
                    username = part.Split('!')[1].Split('@')[0];
                    continue;
                }
                if(part.Contains("@color="))
                {
                    colorHEX = part.Split('=')[1];
                    continue;
                }
                if(part.Contains("display-name"))
                {
                    displayName = part.Split('=')[1];
                    continue;
                }
                if(part.Contains("emotes="))
                {
                    emoteSet = part.Split('=')[1];
                    continue;
                }
                if(part.Contains("subscriber="))
                {
                    if (part.Split('=')[1] == "1")
                        subscriber = true;
                    else
                        subscriber = false;
                    continue;
                }
                if(part.Contains("turbo="))
                {
                    if (part.Split('=')[1] == "1")
                        turbo = true;
                    else
                        turbo = false;
                    continue;
                }
                if(part.Contains("user-id="))
                {
                    userID = int.Parse(part.Split('=')[1]);
                    continue;
                }
                if(part.Contains("user-type="))
                {
                    userTypeStr = part.Split('=')[1].Split(' ')[0];
                    switch (part.Split('=')[1].Split(' ')[0])
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
                    continue;
                }
                if (part.Contains("mod="))
                {
                    if (part.Split(';')[1] == "1")
                        modFlag = true;
                    else
                        modFlag = false; 
                    continue;
                }
            }
            message = IRCString.Split(new string[] { string.Format(" PRIVMSG #{0} :", channel) }, StringSplitOptions.None)[1];
        }

        private bool convertToBool(string data)
        {
            if (data == "1")
                return true;
            return false;
        }
    }
}
