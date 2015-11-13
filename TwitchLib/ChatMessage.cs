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
        private bool subscriber, turbo;
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

        //@color=#CC00C9;display-name=astickgamer;emotes=70803:6-11;sent-ts=1447446917994;subscriber=1;tmi-sent-ts=1447446957359;turbo=0;user-id=24549902;user-type= :astickgamer!astickgamer@astickgamer.tmi.twitch.tv PRIVMSG #cohhcarnage :cjb2, cohhHi
        public ChatMessage(string IRCString)
        {
            //@color=asd;display-name=Swiftyspiffyv4;emotes=;subscriber=0;turbo=0;user-id=103325214;user-type=asd :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #burkeblack :this is a test lol
            int TSFinalBoost = 0;
            int TSSubBoost = 0;
            if (IRCString.Split(';').Count() == 9)
            {
                TSSubBoost = 1;
                TSFinalBoost = 2;
                Console.WriteLine("TSSubBoost: " + TSSubBoost + "TSFinal: " + TSFinalBoost.ToString());
            }
            colorHEX = IRCString.Split(';')[0].Split('=')[1];
            username = IRCString.Split('@')[2].Split('.')[0];
            displayName = IRCString.Split(';')[1].Split('=')[1];
            emoteSet = IRCString.Split(';')[2].Split('=')[1];
            string subscriberStr = IRCString.Split(';')[3 + TSSubBoost].Split('=')[1];
            string turboStr = IRCString.Split(';')[4 + TSFinalBoost].Split('=')[1];
            string userIDStr = IRCString.Split(';')[5 + TSFinalBoost].Split('=')[1];
            string userTypeStr = IRCString.Split(';')[6 + TSFinalBoost].Split(':')[0].Split('=')[1].Replace(" ", String.Empty);
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
