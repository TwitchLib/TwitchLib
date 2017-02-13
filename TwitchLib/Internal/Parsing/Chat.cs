using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Internal.Parsing
{
    internal static class Chat
    {
        /// <summary>Function returning the type of message received from Twitch</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns>Message type (ie NOTICE, PRIVMSG, JOIN, etc)</returns>
        public static string getReadType(string message, string channel)
        {
            if (message.Contains(" "))
            {
                bool found = false;
                foreach (string word in message.Split(' '))
                {
                    if (word[0] == '#')
                    {
                        if (word == $"#{channel}")
                            found = true;
                    }
                }
                if (found)
                {
                    var splitter = Regex.Split(message, $" #{channel}");
                    var readType = splitter[0].Split(' ')[splitter[0].Split(' ').Length - 1];
                    return readType;
                }
                else
                {
                    if (message.Split(' ').Count() > 1 && message.Split(' ')[1] == "NOTICE")
                        return "NOTICE";
                }
            }
            return null;
        }

        /// <summary>
        /// Extracts msg-id property from message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Message id (ie host_on)</returns>
        public static string getMsgId(string message)
        {
            foreach (string part in message.Split(' '))
                if (part.Contains("@msg-id"))
                    return part.Split('=')[1];
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

        /// <summary>Parse function to detect new subscriber</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectNewSubscriber(string message, List<JoinedChannel> channels)
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
                return new DetectionReturn((message.Split('!')[0] == ":twitchnotify" && (message.Contains("just subscribed!") || message.ToLower().Contains("just subscribed with twitch prime!"))), channelRet);
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
        /// <param name="botUsername"></param>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <param name="_channelEmotes"></param>
        /// <param name="WillReplaceEmotes"></param>
        /// <param name="_commandIdentifiers"></param>
        /// <returns></returns>
        public static DetectionReturn detectCommandReceived(string botUsername, string message, List<JoinedChannel> channels, MessageEmoteCollection _channelEmotes, bool WillReplaceEmotes, List<char> _commandIdentifiers)
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
                var chatMessage = new ChatMessage(botUsername, message, ref _channelEmotes, WillReplaceEmotes);
                return new DetectionReturn((_commandIdentifiers.Count != 0 && _commandIdentifiers.Contains(chatMessage.Message[0])), channelRet);
            }
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectUserJoined(string message, List<JoinedChannel> channels)
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
        public static DetectionReturn detectedUserLeft(string message, List<JoinedChannel> channels)
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
        /// <returns></returns>
        public static DetectionReturn detectedIncorrectLogin(string message)
        {
            if (message.Contains(":") && message.Split(':').Count() > 2
                && message.Split(':')[2] == "Login authentication failed")
                return new DetectionReturn(true);
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

        /// <summary>Parse function to detect host leaving.</summary>
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

        /// <summary>Parse function to detect resubscriptions.</summary>
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
                return new DetectionReturn((message.Split(';')[7].Split('=')[1] == "resub"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect PING messages.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn detectedPing(string message)
        {
            return new DetectionReturn(message == "PING :tmi.twitch.tv");
        }

        /// <summary>[Works] Parse function to detect PONG messages.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn detectedPong(string message)
        {
            return new DetectionReturn(message == ":tmi.twitch.tv PONG tmi.twitch.tv :irc.chat.twitch.tv");
        }

        /// <summary>Parse function to detect stopped hosting.</summary>
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
            if (channels.Count == 0)
                return new DetectionReturn(false);
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
        public static DetectionReturn detectedUserTimedout(string message, List<JoinedChannel> channels)
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
        public static DetectionReturn detectedUserBanned(string message, List<JoinedChannel> channels)
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

        /// <summary>Parse function to detect list of moderators was received.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedModeratorsReceived(string message, List<JoinedChannel> channels)
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
                return new DetectionReturn(message.Contains("The moderators of this room are:"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>
        /// Parse function to detect chat color being changed successfully
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedChatColorChanged(string message, List<JoinedChannel> channels)
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
                return new DetectionReturn(message.Contains("Your color has been changed."), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>
        /// Parse function to detect now hosting event.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn detectedNowHosting(string message, List<JoinedChannel> channels)
        {
            //@msg-id=host_on :tmi.twitch.tv NOTICE #burkeblack :Now hosting DjTechlive.
            string id = getMsgId(message);
            if (string.IsNullOrEmpty(id) || id != "host_on")
                return new DetectionReturn(false);
            string channel = "";
            foreach (JoinedChannel channelObj in channels)
                if (channelObj.Channel.ToLower() == message.Split(' ')[3].ToLower().Replace("#", ""))
                    channel = channelObj.Channel;
            return new DetectionReturn(message.Contains(":Now hosting "), channel);
        }

        /// <summary>
        /// Parse function to detect that a 366 has been received indicating completed joining channel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn detectedJoinChannelCompleted(string message)
        {
            //:blubott.tmi.twitch.tv 366 blubott #monstercat :End of /NAMES list
            if (!message.Contains(" ") || message.Split(' ')[1] != "366")
                return new DetectionReturn(false);

            return new DetectionReturn(true, message.Split(' ')[3].Replace("#", ""));
        }

        public static DetectionReturn detectedBeingHosted(string message, List<JoinedChannel> channels)
        {
            //:jtv!jtv@jtv.tmi.twitch.tv PRIVMSG (HOSTED):(HOSTER) is now hosting you for (VIEWERS_TOTAL) viewers.
            //:jtv!jtv@jtv.tmi.twitch.tv PRIVMSG swiftyspiffy :BurkeBlack is now hosting you.
            if (message.Contains(" ") && message.Split(' ')[1] == "PRIVMSG" && message.Contains("jtv!jtv@jtv") && message.Contains("is now hosting you"))
                return new DetectionReturn(true, message.Split(' ')[2]);
            return new DetectionReturn(false);
        }
    }
}
