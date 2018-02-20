using System.Collections.Generic;
using System.Linq;
using TwitchLib.Models.Client;

namespace TwitchLib.Internal.Parsing
{
    internal static class Chat
    {
        /// <summary>Function returning the type of message received from Twitch</summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <returns>Message type (ie NOTICE, PRIVMSG, JOIN, etc)</returns>
        private static string GetReadType(string message, string channel)
        {
            var splitByChannel = message.Split(new[] { $" #{channel}" }, System.StringSplitOptions.None);
            if (splitByChannel.Length > 1) // If the message contains $" #{channel}"
                return splitByChannel[0].Split(' ').Last();

            var splitBySpace = message.Split(' ');
            if (splitBySpace.Length > 1 && splitBySpace[1] == "NOTICE")
                return "NOTICE";

            return null;
        }

        /// <summary>
        /// Extracts msg-id property from message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Message id (ie host_on)</returns>
        private static string GetMsgId(string message)
        {
            return (from part in message.Split(' ') where part.Contains("@msg-id") select part.Split('=')[1]).FirstOrDefault();
        }

        /// <summary>[Works] Parse function to detect connected successfully</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool DetectConnected(string message)
        {
            if (message.Split(':').Length <= 2) return false;

            return message.Split(':')[2] == "You are in a maze of twisty passages, all alike.";
        }

        /// <summary>Parse function to detect new subscriber</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectNewSubscriber(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "USERNOTICE")
                return new DetectionReturn(message.Split(';')[7].Split('=')[1] == "sub", channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new messages.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectMessageReceived(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "PRIVMSG")
                return new DetectionReturn(message.Split('!')[0] != ":twitchnotify", channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new commands.</summary>
        /// <param name="botUsername"></param>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <param name="channelEmotes"></param>
        /// <param name="willReplaceEmotes"></param>
        /// <param name="commandIdentifiers"></param>
        /// <returns></returns>
        public static DetectionReturn DetectCommandReceived(string botUsername, string message, IEnumerable<JoinedChannel> channels, MessageEmoteCollection channelEmotes, bool willReplaceEmotes, HashSet<char> commandIdentifiers)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType == null || readType != "PRIVMSG") return new DetectionReturn(false);

            var chatMessage = new ChatMessage(botUsername, message, ref channelEmotes, willReplaceEmotes);
            if (commandIdentifiers != null && commandIdentifiers.Count != 0 && !string.IsNullOrEmpty(chatMessage.Message))
                return new DetectionReturn(commandIdentifiers.Contains(chatMessage.Message[0]), channelRet);

            return new DetectionReturn(false, channelRet);
        }

        /// <summary>[Works] Parse function to detect new viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectUserJoined(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            return new DetectionReturn(readType != null && readType == "JOIN", channelRet);
        }

        /// <summary>[Works] Parse function to detect leaving viewers.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedUserLeft(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            return new DetectionReturn(readType != null && readType == "PART", channelRet);
        }

        /// <summary>[Works] Parse function to detect new moderators.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedModeratorJoined(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "MODE")
                return new DetectionReturn(message.Contains(" ") && message.Split(' ')[3] == "+o", channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect leaving moderators.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedModeatorLeft(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "MODE")
                return new DetectionReturn(message.Contains(" ") && message.Split(' ')[3] == "-o", channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect failed login.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedIncorrectLogin(string message)
        {
            if (message.Contains(":") && message.Split(':').Length > 2
                && message.Split(':')[2] == "Login authentication failed")
                return new DetectionReturn(true);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect malformed oauth error.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedMalformedOAuth(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn(message.Contains("Improperly formatted auth"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>Parse function to detect host leaving.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedHostLeft(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn(message.Contains("has gone offline"), channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect new channel state.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedChannelStateChanged(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            return new DetectionReturn(readType != null && readType == "ROOMSTATE", channelRet);
        }

        /// <summary>[Works] Parse function to detect new user states.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedUserStateChanged(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            return new DetectionReturn(readType != null && readType == "USERSTATE", channelRet);
        }

        /// <summary>Parse function to detect resubscriptions.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedReSubscriber(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "USERNOTICE")
                return new DetectionReturn(message.Split(';')[7].Split('=')[1] == "resub", channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>[Works] Parse function to detect PING messages.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedPing(string message)
        {
            return new DetectionReturn(message == "PING :tmi.twitch.tv");
        }

        /// <summary>[Works] Parse function to detect PONG messages.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedPong(string message)
        {
            return new DetectionReturn(message == ":tmi.twitch.tv PONG tmi.twitch.tv :irc.chat.twitch.tv");
        }

        /// <summary>Parse function to detect stopped hosting.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool DetectedHostingStopped(string message)
        {
            if (message.Split(' ')[1] == "HOSTTARGET")
                return message.Split(' ')[3] == ":-";
            return false;
        }

        /// <summary>[Works] Parse function to detect started hosting.</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool DetectedHostingStarted(string message)
        {
            if (message.Split(' ')[1] == "HOSTTARGET")
                return message.Split(' ')[3] != ":-";
            return false;
        }

        /// <summary>[Works] Parse function to detect existing user messages.</summary>
        /// <param name="message"></param>
        /// <param name="username"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedExistingUsers(string message, string username, List<JoinedChannel> channels)
        {
            if (channels.Count == 0)
                return new DetectionReturn(false);
            //This assumes the existing users came from the most recently joined channel. Could be a source of bug(s)
            return new DetectionReturn(message.Split(' ').Length > 5 && message.Split(' ')[0] == $":{username}.tmi.twitch.tv" && message.Split(' ')[1] == "353" &&
                message.Split(' ')[2] == username, channels[channels.Count - 1].Channel);
        }

        /// <summary>Parse function to detect clearchat.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedClearedChat(string message, IEnumerable<JoinedChannel> channels)
        {
            foreach (var channel in channels)
                if (message == $":tmi.twitch.tv CLEARCHAT #{channel.Channel.ToLower()}")
                    return new DetectionReturn(true, channel.Channel);

            return new DetectionReturn(false);
        }

        /// <summary>Parse function to detect a viewer was timedout.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedUserTimedout(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            return new DetectionReturn(readType == "CLEARCHAT" && message.Substring(0, 14) == "@ban-duration=", channelRet);
        }

        /// <summary>Parse function to detect viewer was banned.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedUserBanned(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            return new DetectionReturn(readType == "CLEARCHAT" && message.Substring(0, 12) == "@ban-reason=", channelRet);
        }

        /// <summary>Parse function to detect list of moderators was received.</summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedModeratorsReceived(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "NOTICE")
                if (message.Contains("The moderators of this room are:") || message.Contains("There are no moderators of this room."))
                    return new DetectionReturn(true, channelRet);
            return new DetectionReturn(false);
        }

        /// <summary>
        /// Parse function to detect chat color being changed successfully
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedChatColorChanged(string message, IEnumerable<JoinedChannel> channels)
        {
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
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
        public static DetectionReturn DetectedNowHosting(string message, IEnumerable<JoinedChannel> channels)
        {
            //@msg-id=host_on :tmi.twitch.tv NOTICE #burkeblack :Now hosting DjTechlive.
            var id = GetMsgId(message);
            if (string.IsNullOrEmpty(id) || id != "host_on")
                return new DetectionReturn(false);
            var channel = "";
            foreach (var channelObj in channels)
                if (channelObj.Channel.ToLower() == message.Split(' ')[3].ToLower().Replace("#", ""))
                    channel = channelObj.Channel;
            return new DetectionReturn(message.Contains(":Now hosting "), channel);
        }

        /// <summary>
        /// Parse function to detect that a 366 has been received indicating completed joining channel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DetectionReturn DetectedJoinChannelCompleted(string message)
        {
            //:blubott.tmi.twitch.tv 366 blubott #monstercat :End of /NAMES list
            if (!message.Contains(" ") || message.Split(' ')[1] != "366")
                return new DetectionReturn(false);

            return new DetectionReturn(true, message.Split(' ')[3].Replace("#", ""));
        }

        public static DetectionReturn DetectedBeingHosted(string message)
        {
            //:jtv!jtv@jtv.tmi.twitch.tv PRIVMSG (HOSTED):(HOSTER) is now hosting you for (VIEWERS_TOTAL) viewers.
            //:jtv!jtv@jtv.tmi.twitch.tv PRIVMSG swiftyspiffy :BurkeBlack is now hosting you.
            //:jtv!jtv@jtv.tmi.twitch.tv PRIVMSG annemunition :WhateverChannelNameHere is auto hosting you for up to 100 viewers.
            if (message.Contains(" ") && message.Split(' ')[1] == "PRIVMSG" && message.Substring(0, 26) == ":jtv!jtv@jtv.tmi.twitch.tv" && message.Contains("hosting you"))
                return new DetectionReturn(true, message.Split(' ')[2]);
            return new DetectionReturn(false);
        }

        public static DetectionReturn DetectedRaidNotification(string message, IEnumerable<JoinedChannel> channels)
        {
            //@badges=;color=#FF0000;display-name=Heinki;emotes=;id=4fb7ab2d-aa2c-4886-a286-46e20443f3d6;login=heinki;mod=0;msg-id=raid;msg-param-displayName=Heinki;msg-param-login=heinki;msg-param-viewerCount=4;room-id=27229958;subscriber=0;system-msg=4\sraiders\sfrom\sHeinki\shave\sjoined\n!;tmi-sent-ts=1510249711023;turbo=0;user-id=44110799;user-type= :tmi.twitch.tv USERNOTICE #pandablack
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "USERNOTICE")
                return new DetectionReturn(message.Split(';')[7].Contains("=") && message.Split(';')[7].Split('=')[1] == "raid", channelRet);
            return new DetectionReturn(false);
        }

        public static DetectionReturn DetectedGiftedSubscription(string message, IEnumerable<JoinedChannel> channels)
        {
            //@badges=;color=;display-name=gekkebelg1803;emotes=;id=ad77ae72-83fd-4b46-947b-27b6aae5db41;login=gekkebelg1803;mod=0;msg-id=subgift;msg-param-months=1;msg-param-recipient-display-name=Chewwy94;msg-param-recipient-id=44452165;msg-param-recipient-user-name=chewwy94;msg-param-sub-plan-name=Channel\sSubscription\s(LIRIK);msg-param-sub-plan=1000;room-id=23161357;subscriber=0;system-msg=gekkebelg1803\sgifted\sa\s$4.99\ssub\sto\sChewwy94!;tmi-sent-ts=1510781070702;turbo=0;user-id=127964463;user-type= :tmi.twitch.tv USERNOTICE #lirik
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "USERNOTICE")
                return new DetectionReturn(message.Split(';')[7].Contains("=") && message.Split(';')[7].Split('=')[1] == "subgift", channelRet);
            return new DetectionReturn(false);
        }

        public static DetectionReturn DetectedSelfRaidError(string message, IEnumerable<JoinedChannel> channels)
        {
            // @msg-id=raid_error_self :tmi.twitch.tv NOTICE #swiftyspiffy :A channel cannot raid itself.
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn(message.Contains(" ") && message.Split(' ')[0].Contains("=") && message.Split(' ')[0].Split('=')[1] == "raid_error_self", channelRet);
            return new DetectionReturn(false);
        }

        public static DetectionReturn DetectedNoPermissionError(string message, IEnumerable<JoinedChannel> channels)
        {
            // @msg-id=no_permission :tmi.twitch.tv NOTICE #swiftyspiffy :You don't have permission to perform that action.
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn(message.Contains(" ") && message.Split(' ')[0].Contains("=") && message.Split(' ')[0].Split('=')[1] == "no_permission", channelRet);
            return new DetectionReturn(false);
        }

        public static DetectionReturn DetectedRaidedChannelIsMatureAudience(string message, IEnumerable<JoinedChannel> channels)
        {
            // @msg-id=raid_notice_mature :tmi.twitch.tv NOTICE #swiftyspiffy :This channel is intended for mature audiences.
            string readType = null;
            string channelRet = null;
            foreach (var channel in channels)
            {
                readType = GetReadType(message, channel.Channel);
                if (readType == null)
                    continue;

                channelRet = channel.Channel;
                break;
            }

            if (readType != null && readType == "NOTICE")
                return new DetectionReturn(message.Contains(" ") && message.Split(' ')[0].Contains("=") && message.Split(' ')[0].Split('=')[1] == "raid_notice_mature", channelRet);
            return new DetectionReturn(false);
        }

        // badges=subscriber/0;color=#0000FF;display-name=KittyJinxu;emotes=30259:0-6;id=1154b7c0-8923-464e-a66b-3ef55b1d4e50;
        // login=kittyjinxu;mod=0;msg-id=ritual;msg-param-ritual-name=new_chatter;room-id=35740817;subscriber=1;
        // system-msg=@KittyJinxu\sis\snew\shere.\sSay\shello!;tmi-sent-ts=1514387871555;turbo=0;user-id=187446639;
        // user-type= USERNOTICE #thorlar kittyjinxu > #thorlar: HeyGuys
        public static DetectionReturn DetectedRitualNewChatter(string message)
        {
            if (message.Split(';').Length > 11
                && message.Split(';')[8].Split('=')[1] == "new_chatter"
                && message.Split(' ').Length > 2
                && message.Split(' ')[2] == "USERNOTICE")
                return new DetectionReturn(true, message.Split(' ')[3].Substring(1));

            return new DetectionReturn(false);
        }
    }
}
