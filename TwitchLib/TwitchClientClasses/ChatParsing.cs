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
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectNewSubscriber(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach(JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }
                
            if (readType != null && readType == "PRIVMSG")
                return new DetectionReturn((message.Split('!')[0] == ":twitchnotify" && (message.Contains("just subscribed!"))), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new messages.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectMessageReceived(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "PRIVMSG")
                return new DetectionReturn(!(message.Split('!')[0] == ":twitchnotify"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new commands.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <param name="_channelEmotes"></param>
        /// <param name="WillReplaceEmotes"></param>
        /// <param name="_commandIdentifiers"></param>
        /// <returns></returns>
        public static DetectionReturn detectCommandReceived(string message, List<JoinedChannel> channels, MessageEmoteCollection _channelEmotes, bool WillReplaceEmotes, List<char> _commandIdentifiers)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "PRIVMSG")
            {
                var chatMessage = new ChatMessage(message, ref _channelEmotes, WillReplaceEmotes);
                return new DetectionReturn((_commandIdentifiers.Count != 0 && _commandIdentifiers.Contains(chatMessage.Message[0])), channelRet);
            }
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectViewerJoined(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            return new DetectionReturn((readType != null && readType == "JOIN"), channelRet);
        }

        /// <summary>[Works] Parse function to detect leaving viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedViewerLeft(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            return new DetectionReturn((readType != null && readType == "PART"), channelRet);
        }

        /// <summary>[Works] Parse function to detect new moderators.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedModeratorJoined(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "MODE")
                return new DetectionReturn((message.Contains(" ") && message.Split(' ')[3] == "+o"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect leaving moderators.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedModeatorLeft(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "MODE")
                return new DetectionReturn((message.Contains(" ") && message.Split(' ')[3] == "-o"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect failed login.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedIncorrectLogin(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn((message.Contains("Error logging in") || message.Contains("Login authentication failed")), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect malformed oauth error.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedMalformedOAuth(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn(message.Contains("Improperly formatted auth"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Untested] Parse function to detect host leaving.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedHostLeft(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn((message.Contains("has gone offline")), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new channel state.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedChannelStateChanged(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            return new DetectionReturn((readType != null && readType == "ROOMSTATE"), channelRet);
        }

        /// <summary>[Works] Parse function to detect new user states.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedUserStateChanged(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            return new DetectionReturn((readType != null && readType == "USERSTATE"), channelRet);
        }

        /// <summary>[Untested] Parse function to detect resubscriptions.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedReSubscriber(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            if (readType != null && readType == "USERNOTICE")
                return new DetectionReturn((message.Split(';')[6].Split('=')[1] == "resub"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect PING messages.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn detectedPing(string message)
        {
            return new DetectionReturn(message == "PING :tmi.twitch.tv");
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
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedExistingUsers(string message, string username, List<JoinedChannel> channels)
        {
            //This assumes the existing users came from the most recently joined channel. Could be a source of bug(s)
            return new DetectionReturn((message.Split(' ').Count() > 5 && message.Split(' ')[0] == $":{username}.tmi.twitch.tv") && message.Split(' ')[1] == "353" &&
                message.Split(' ')[2] == username, channels[channels.Count - 1].Channel);
        }

        /// <summary>Parse function to detect clearchat.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedClearedChat(string message, List<JoinedChannel> channels)
        {
            foreach (JoinedChannel channel in channels)
                if (message == $":tmi.twitch.tv CLEARCHAT #{channel.Channel.ToLower()}")
                    return new DetectionReturn(true, channel.Channel);

            return new DetectionReturn(false);
        }

        /// <summary>Parse function to detect a viewer was timedout.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedViewerTimedout(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            return new DetectionReturn(readType == "CLEARCHAT" && message.Substring(0, 14) == "@ban-duration=", channelRet);
        }

        /// <summary>Parse function to detect viewer was banned.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedViewerBanned(string message, List<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (JoinedChannel channel in channels)
            {
                readType = getReadType(message, channel.Channel);
                if (readType != null)
                {
                    channelRet = channel.Channel;
                    break;
                }
            }

            return new DetectionReturn(readType == "CLEARCHAT" && message.Substring(0, 12) == "@ban-reason=", channelRet);
        }
    }
}
