using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;

namespace TwitchLib
{
    class utility
    {
        public enum userType
        {
            Viewer,
            Moderator,
            Streamer,
            GlobalModerator,
            Admin,
            Staff
        }

        public static string getChatServerDetails(string _channelName)
        {
            string resp = new WebClient().DownloadString(String.Format("https://api.twitch.tv/api/channels/{0}/chat_properties", _channelName));
            JObject respJson = JObject.Parse(resp);
            return respJson.SelectToken("chat_servers")[0].ToString();
        }

        public static IPEndPoint createIPEndPoint(string inputStr)
        {
            string[] parts = inputStr.Split(':');
            IPAddress ip;
            int port;
            IPAddress.TryParse(parts[0].ToString(), out ip);
            int.TryParse(parts[1].ToString(), out port);
            Console.WriteLine("Connecting chat server @: " + ip + ":" + port);
            return new IPEndPoint(ip, port);
        }

        //@msg-id=room_mods :tmi.twitch.tv NOTICE #swiftyspiffy :The moderators of this room are: nightbot, swiftyspiffy, the_kraken_bot
        public static List<string> parseModListMessage(string _channelName, string _rawIRCMessage)
        {
            List<string> mods = new List<string>();
            string modStr = _rawIRCMessage.Replace(String.Format("@msg-id=room_mods :tmi.twitch.tv NOTICE #{0} :The moderators of this room are: ", _channelName), "").Replace(", ", ",");
            string[] modParts = modStr.Split(',');
            for (int i = 0; i < modParts.Length; i++)
            {
                mods.Add(modParts[i]);
            }
            return mods;
        }

        public static Subscription createSubscription(string _rawIRCMessage)
        {
            string message = _rawIRCMessage.Split(':')[2];
            if (message.Contains("months in a row"))
            {
                int months;
                int.TryParse(message.Split(' ')[3], out months);
                return new Subscription(message.Split(' ')[0], months);
            }
            else
            {
                return new Subscription(message.Split(' ')[0], 0);
            }
        }

        public static string parseJoinMessage(string _rawIRCMessage)
        {
            return _rawIRCMessage.Substring(1, _rawIRCMessage.Length - 1).Split('!')[0];
        }

        public static string parsePartMessage(string _rawIRCMessage)
        {
            return _rawIRCMessage.Substring(1, _rawIRCMessage.Length - 1).Split('!')[0];
        }

        //This function should utilize regex instead of Split
        public static ChatMessage createTwitchMessage(string _channelName, string _rawIRCMessage)
        {
            string colorHEX, username, display_name, emotesStr, subscriberStr, turboStr, userTypeStr, message;
            string[] parts = _rawIRCMessage.Split(';');
            colorHEX = parts[0].Split('=')[1];
            username = _rawIRCMessage.Split(':')[1].Split('!')[0];
            display_name = parts[1].Split('=')[1];
            emotesStr = parts[2].Split('=')[1];
            subscriberStr = parts[3].Split('=')[1];
            turboStr = parts[4].Split('=')[1];
            userTypeStr = parts[5].Split('=')[1].Split(' ')[0];
            message = parts[5].Split(' ')[4];
            message = message.Substring(1, message.Length - 1);

            List<string> emotes = new List<string>();
            if (emotesStr.Length > 0)
            {
                if (emotesStr.Contains(","))
                {
                    for (int i = 0; i < emotesStr.Split(',').Length; i++)
                    {
                        emotes.Add(emotesStr.Split(',')[i]);
                    }
                }
                else
                {
                    emotes.Add(emotesStr);
                }
            }

            userType user_type;
            switch (userTypeStr)
            {
                case "viewer":
                    user_type = utility.userType.Viewer;
                    break;

                case "mod":
                    if (username.ToLower() == _channelName.ToLower())
                    {
                        user_type = utility.userType.Streamer;
                    }
                    else
                    {
                        user_type = utility.userType.Moderator;
                    }
                    break;

                case "admin":
                    user_type = utility.userType.Admin;
                    break;

                case "global_moderator":
                    user_type = utility.userType.GlobalModerator;
                    break;

                case "staff":
                    user_type = utility.userType.Staff;
                    break;

                default:
                    user_type = utility.userType.Viewer;
                    break;
            }

            int subscriberInt, turboInt;
            int.TryParse(subscriberStr, out subscriberInt);
            int.TryParse(turboStr, out turboInt);
            Boolean subscriber, turbo;
            subscriber = Convert.ToBoolean(subscriberInt);
            turbo = Convert.ToBoolean(turboInt);

            return new ChatMessage(username, display_name, colorHEX, emotes, subscriber, turbo, user_type, message);
        }
    }
}
