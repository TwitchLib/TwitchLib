using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitchLib.TwitchClientClasses
{
    /// <summary>Static parsing class handling all chat message parsing</summary>
    public static class ChatParsing
    {
        /// <summary>Function returning the type of message received from Twitch</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns>Message type (ie NOTICE, PRIVMSG, JOIN, etc)</returns>
        public static string getReadType(string message, string channel)
        {
            if (message.Contains($"#{channel}"))
            {
                var splitter = Regex.Split(message, $" #{channel}");
                var readType = splitter[0].Split(' ')[splitter[0].Split(' ').Length - 1];
                return readType;
            } else
            {
                // Other cases
                if (message.Split(' ').Count() > 1 && message.Split(' ')[1] == "NOTICE")
                    return "NOTICE";
            }

            return null;
        }

        /// <summary>[Works] Parse function to detect connected successfully</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool detectConnected(string message)
        {
            if (message.Split(':').Length > 2)
                if (message.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                    return true;
            return false;
        }

        /// <summary>[Untested] Parse function to detect new subscriber</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectNewSubscriber(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "PRIVMSG")
                return (message.Split('!')[0] == ":twitchnotify" && (message.Contains("just subscribed!")));
            return false; 
        }

        /// <summary>[Works] Parse function to detect new messages.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectMessageReceived(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "PRIVMSG")
                return !(message.Split('!')[0] == ":twitchnotify");
            return false;
        }

        /// <summary>[Works] Parse function to detect new commands.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <param name="_channelEmotes"></param>
        /// <param name="WillReplaceEmotes"></param>
        /// <param name="_commandIdentifiers"></param>
        /// <returns></returns>
        public static bool detectCommandReceived(string message, string channel, MessageEmoteCollection _channelEmotes, bool WillReplaceEmotes, List<char> _commandIdentifiers)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "PRIVMSG")
            {
                var chatMessage = new ChatMessage(message, ref _channelEmotes, WillReplaceEmotes);
                return (_commandIdentifiers.Count != 0 && _commandIdentifiers.Contains(chatMessage.Message[0]));
            }
            return false;
        }

        /// <summary>[Works] Parse function to detect new viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectViewerJoined(string message, string channel)
        {
            var readType = getReadType(message, channel);
            return (readType != null && readType == "JOIN");
        }

        /// <summary>[Works] Parse function to detect leaving viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedViewerLeft(string message, string channel)
        {
            var readType = getReadType(message, channel);
            return (readType != null && readType == "PART");
        }

        /// <summary>[Works] Parse function to detect new moderators.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedModeratorJoined(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "MODE")
                return (message.Contains(" ") && message.Split(' ')[3] == "+o");
            return false;
        }

        /// <summary>[Works] Parse function to detect leaving moderators.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedModeatorLeft(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "MODE")
                return (message.Contains(" ") && message.Split(' ')[3] == "-o");
            return false;
        }

        /// <summary>[Works] Parse function to detect failed login.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedIncorrectLogin(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "NOTICE")
                return (message.Contains("Error logging in") || message.Contains("Login authentication failed"));
            return false;
        }

        /// <summary>[Untested] Parse function to detect host leaving.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedHostLeft(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "NOTICE")
                return (message.Contains("has gone offline"));
            return false;
        }

        /// <summary>[Works] Parse function to detect new channel state.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedChannelStateChanged(string message, string channel)
        {
            var readType = getReadType(message, channel);
            return (readType != null && readType == "ROOMSTATE");
        }

        /// <summary>[Works] Parse function to detect new user states.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedUserStateChanged(string message, string channel)
        {
            var readType = getReadType(message, channel);
            return (readType != null && readType == "USERSTATE");
        }

        /// <summary>[Untested] Parse function to detect resubscriptions.</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool detectedReSubscriber(string message, string channel)
        {
            var readType = getReadType(message, channel);
            if (readType != null && readType == "USERNOTICE")
                return (message.Split(';')[6].Split('=')[1] == "resub");
            return false;
        }

        /// <summary>[Works] Parse function to detect PING messages.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool detectedPing(string message)
        {
            return message == "PING :tmi.twitch.tv";
        }

        /// <summary>[Untested] Parse function to stopped hosting.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool detectedHostingStopped(string message)
        {
            if (message.Split(' ')[1] == "HOSTTARGET")
                return (message.Split(' ')[3] == ":-");
            return false;
        }

        /// <summary>[Works] Parse function to detect started hosting.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool detectedHostingStarted(string message)
        {
            if (message.Split(' ')[1] == "HOSTTARGET")
                return !(message.Split(' ')[3] == ":-");
            return false;
        }

        /// <summary>[Works] Parse function to detect existing user messages.</summary>
        /// <param name="message"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool detectedExistingUsers(string message, string username)
        {
            return (message.Split(' ').Count() > 5 && message.Split(' ')[0] == $":{username}.tmi.twitch.tv") && message.Split(' ')[1] == "353" &&
                message.Split(' ')[2] == username;
        }
    }
}
