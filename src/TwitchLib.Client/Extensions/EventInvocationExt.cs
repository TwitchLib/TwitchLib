using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Class EventInvocationExt.
    /// </summary>
    public static class EventInvocationExt
    {
        /// <summary>
        /// Invokes the on being hosted.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="hostedByChannel">The hosted by channel.</param>
        /// <param name="viewers">The viewers.</param>
        /// <param name="isAutoHosted">if set to <c>true</c> [is automatic hosted].</param>
        public static void InvokeOnBeingHosted(this TwitchClient client, string channel, string botUsername, string hostedByChannel, int viewers, bool isAutoHosted)
        {
            OnBeingHostedArgs model = new OnBeingHostedArgs()
            {
                BeingHostedNotification = new BeingHostedNotification(channel, botUsername, hostedByChannel, viewers, isAutoHosted)
            };
            client.RaiseEvent("OnBeingHosted", model);
        }

        /// <summary>
        /// Invokes the channel state changed.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="r9k">if set to <c>true</c> [R9K].</param>
        /// <param name="rituals">if set to <c>true</c> [rituals].</param>
        /// <param name="subOnly">if set to <c>true</c> [sub only].</param>
        /// <param name="slowMode">The slow mode.</param>
        /// <param name="emoteOnly">if set to <c>true</c> [emote only].</param>
        /// <param name="broadcasterLanguage">The broadcaster language.</param>
        /// <param name="followersOnly">The followers only.</param>
        /// <param name="mercury">if set to <c>true</c> [mercury].</param>
        /// <param name="roomId">The room identifier.</param>
        public static void InvokeChannelStateChanged(this TwitchClient client, string channel, bool r9k, bool rituals,
            bool subOnly, int slowMode, bool emoteOnly, string broadcasterLanguage, TimeSpan followersOnly, bool mercury, string roomId)
        {
            ChannelState state = new ChannelState(r9k, rituals, subOnly, slowMode, emoteOnly, broadcasterLanguage, channel, followersOnly, mercury, roomId);
            OnChannelStateChangedArgs model = new OnChannelStateChangedArgs()
            {
                Channel = channel,
                ChannelState = state
            };
            client.RaiseEvent("OnChannelStateChanged", model);
        }

        /// <summary>
        /// Invokes the chat cleared.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        public static void InvokeChatCleared(this TwitchClient client, string channel)
        {
            OnChatClearedArgs model = new OnChatClearedArgs()
            {
                Channel = channel
            };
            client.RaiseEvent("OnChatCleared", model);
        }

        /// <summary>
        /// Invokes the chat commands received.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="color">The color.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="message">The message.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="subscribedMonthCount">The subscribed month count.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="isMe">if set to <c>true</c> [is me].</param>
        /// <param name="isBroadcaster">if set to <c>true</c> [is broadcaster].</param>
        /// <param name="isVip">if set to <c>true</c> [is VIP].</param>
        /// <param name="isPartner">if set to <c>true</c> [is Partner].</param>
        /// <param name="isStaff">if set to <c>true</c> [is Staff].</param>
        /// <param name="noisy">The noisy.</param>
        /// <param name="rawIrcMessage">The raw irc message.</param>
        /// <param name="emoteReplacedMessage">The emote replaced message.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="cheerBadge">The cheer badge.</param>
        /// <param name="bits">The bits.</param>
        /// <param name="bitsInDollars">The bits in dollars.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="argumentsAsString">The arguments as string.</param>
        /// <param name="argumentsAsList">The arguments as list.</param>
        /// <param name="commandIdentifier">The command identifier.</param>
        public static void InvokeChatCommandsReceived(this TwitchClient client, string botUsername, string userId, string userName, string displayName,
            string colorHex, Color color, EmoteSet emoteSet, string message, UserType userType, string channel, string id, bool isSubscriber, int subscribedMonthCount,
            string roomId, bool isTurbo, bool isModerator, bool isMe, bool isBroadcaster, bool isVip, bool isPartner, bool isStaff, Noisy noisy, string rawIrcMessage, string emoteReplacedMessage,
            List<KeyValuePair<string, string>> badges, CheerBadge cheerBadge, int bits, double bitsInDollars, string commandText, string argumentsAsString,
            List<string> argumentsAsList, char commandIdentifier)
        {
            ChatMessage msg = new ChatMessage(botUsername, userId, userName, displayName, colorHex, color, emoteSet, message, userType, channel, id,
                isSubscriber, subscribedMonthCount, roomId, isTurbo, isModerator, isMe, isBroadcaster, isVip, isPartner, isStaff, noisy, rawIrcMessage, emoteReplacedMessage,
                badges, cheerBadge, bits, bitsInDollars);
            OnChatCommandReceivedArgs model = new OnChatCommandReceivedArgs()
            {
                Command = new ChatCommand(msg, commandText, argumentsAsString, argumentsAsList, commandIdentifier)
            };
            client.RaiseEvent("OnChatCommandReceived", model);
        }

        /// <summary>
        /// Invokes the connected.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="autoJoinChannel">The automatic join channel.</param>
        /// <param name="botUsername">The bot username.</param>
        public static void InvokeConnected(this TwitchClient client, string autoJoinChannel, string botUsername)
        {
            OnConnectedArgs model = new OnConnectedArgs()
            {
                AutoJoinChannel = autoJoinChannel,
                BotUsername = botUsername
            };
            client.RaiseEvent("OnConnected", model);
        }

        /// <summary>
        /// Invokes the connection error.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="errorEvent">The error event.</param>
        public static void InvokeConnectionError(this TwitchClient client, string botUsername, ErrorEvent errorEvent)
        {
            OnConnectionErrorArgs model = new OnConnectionErrorArgs()
            {
                BotUsername = botUsername,
                Error = errorEvent
            };
            client.RaiseEvent("OnConnectionError", model);
        }

        /// <summary>
        /// Invokes the disconnected.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        public static void InvokeDisconnected(this TwitchClient client, string botUsername)
        {
            OnDisconnectedArgs model = new OnDisconnectedArgs()
            {
                BotUsername = botUsername
            };
            client.RaiseEvent("OnDisconnected", model);
        }

        /// <summary>
        /// Invokes the existing users detected.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="users">The users.</param>
        public static void InvokeExistingUsersDetected(this TwitchClient client, string channel, List<string> users)
        {
            OnExistingUsersDetectedArgs model = new OnExistingUsersDetectedArgs()
            {
                Channel = channel,
                Users = users
            };
            client.RaiseEvent("OnExistingUsersDetected", model);
        }

        /// <summary>
        /// Invokes the gifted subscription.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="color">The color.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emotes">The emotes.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="login">The login.</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="msgId">The MSG identifier.</param>
        /// <param name="msgParamMonths">The MSG parameter months.</param>
        /// <param name="msgParamRecipientDisplayName">Display name of the MSG parameter recipient.</param>
        /// <param name="msgParamRecipientId">The MSG parameter recipient identifier.</param>
        /// <param name="msgParamRecipientUserName">Name of the MSG parameter recipient user.</param>
        /// <param name="msgParamSubPlanName">Name of the MSG parameter sub plan.</param>
        /// <param name="msgParamSubPlan">The MSG parameter sub plan.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="systemMsg">The system MSG.</param>
        /// <param name="systemMsgParsed">The system MSG parsed.</param>
        /// <param name="tmiSentTs">The tmi sent ts.</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="userId">Id of the user.</param>
        public static void InvokeGiftedSubscription(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string color, string displayName, string emotes, string id, string login, bool isModerator,
            string msgId, string msgParamMonths, string msgParamRecipientDisplayName, string msgParamRecipientId, string msgParamRecipientUserName,
            string msgParamSubPlanName, string msgMultiMonthGiftDuration, SubscriptionPlan msgParamSubPlan, string roomId, bool isSubscriber, string systemMsg, string systemMsgParsed,
            string tmiSentTs, bool isTurbo, UserType userType, string userId)
        {
            OnGiftedSubscriptionArgs model = new OnGiftedSubscriptionArgs()
            {
                GiftedSubscription = new GiftedSubscription(badges, badgeInfo, color, displayName, emotes, id, login, isModerator, msgId, msgParamMonths, msgParamRecipientDisplayName,
                msgParamRecipientId, msgParamRecipientUserName, msgParamSubPlanName, msgMultiMonthGiftDuration, msgParamSubPlan, roomId, isSubscriber, systemMsg, systemMsgParsed, tmiSentTs, isTurbo,
                userType, userId)
            };
            client.RaiseEvent("OnGiftedSubscription", model);
        }

        /// <summary>
        /// Invokes the on hosting started.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="hostingChannel">The hosting channel.</param>
        /// <param name="targetChannel">The target channel.</param>
        /// <param name="viewers">The viewers.</param>
        public static void InvokeOnHostingStarted(this TwitchClient client, string hostingChannel, string targetChannel, int viewers)
        {
            OnHostingStartedArgs model = new OnHostingStartedArgs()
            {
                HostingStarted = new HostingStarted(hostingChannel, targetChannel, viewers)
            };
            client.RaiseEvent("OnHostingStarted", model);
        }

        /// <summary>
        /// Invokes the on hosting stopped.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="hostingChannel">The hosting channel.</param>
        /// <param name="viewers">The viewers.</param>
        public static void InvokeOnHostingStopped(this TwitchClient client, string hostingChannel, int viewers)
        {
            OnHostingStoppedArgs model = new OnHostingStoppedArgs()
            {
                HostingStopped = new HostingStopped(hostingChannel, viewers)
            };
            client.RaiseEvent("OnHostingStopped", model);
        }

        /// <summary>
        /// Invokes the host left.
        /// </summary>
        /// <param name="client">The client.</param>
        public static void InvokeHostLeft(this TwitchClient client)
        {
            client.RaiseEvent("OnHostLeft");
        }

        /// <summary>
        /// Invokes the incorrect login.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="ex">The ex.</param>
        public static void InvokeIncorrectLogin(this TwitchClient client, Exceptions.ErrorLoggingInException ex)
        {
            OnIncorrectLoginArgs model = new OnIncorrectLoginArgs()
            {
                Exception = ex
            };
            client.RaiseEvent("OnIncorrectLogin", model);
        }

        /// <summary>
        /// Invokes the joined channel.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="channel">The channel.</param>
        public static void InvokeJoinedChannel(this TwitchClient client, string botUsername, string channel)
        {
            OnJoinedChannelArgs model = new OnJoinedChannelArgs()
            {
                BotUsername = botUsername,
                Channel = channel
            };
            client.RaiseEvent("OnJoinedChannel", model);
        }

        /// <summary>
        /// Invokes the left channel.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="channel">The channel.</param>
        public static void InvokeLeftChannel(this TwitchClient client, string botUsername, string channel)
        {
            OnLeftChannelArgs model = new OnLeftChannelArgs()
            {
                BotUsername = botUsername,
                Channel = channel
            };
            client.RaiseEvent("OnLeftChannel", model);
        }

        /// <summary>
        /// Invokes the log.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="data">The data.</param>
        /// <param name="dateTime">The date time.</param>
        public static void InvokeLog(this TwitchClient client, string botUsername, string data, DateTime dateTime)
        {
            OnLogArgs model = new OnLogArgs()
            {
                BotUsername = botUsername,
                Data = data,
                DateTime = dateTime
            };
            client.RaiseEvent("OnLog", model);
        }

        /// <summary>
        /// Invokes the message received.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="color">The color.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="message">The message.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="subscribedMonthCount">The subscribed month count.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="isMe">if set to <c>true</c> [is me].</param>
        /// <param name="isBroadcaster">if set to <c>true</c> [is broadcaster].</param>
        /// <param name="noisy">The noisy.</param>
        /// <param name="rawIrcMessage">The raw irc message.</param>
        /// <param name="emoteReplacedMessage">The emote replaced message.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="cheerBadge">The cheer badge.</param>
        /// <param name="bits">The bits.</param>
        /// <param name="bitsInDollars">The bits in dollars.</param>
        public static void InvokeMessageReceived(this TwitchClient client, string botUsername, string userId, string userName, string displayName, string colorHex,
            Color color, EmoteSet emoteSet, string message, UserType userType, string channel, string id, bool isSubscriber, int subscribedMonthCount, string roomId, bool isTurbo,
            bool isModerator, bool isMe, bool isBroadcaster, bool isVip, bool isPartner, bool isStaff, Noisy noisy, string rawIrcMessage, string emoteReplacedMessage, List<KeyValuePair<string, string>> badges,
            CheerBadge cheerBadge, int bits, double bitsInDollars)
        {
            OnMessageReceivedArgs model = new OnMessageReceivedArgs()
            {
                ChatMessage = new ChatMessage(botUsername, userId, userName, displayName, colorHex, color, emoteSet, message, userType, channel, id, isSubscriber,
                subscribedMonthCount, roomId, isTurbo, isModerator, isMe, isBroadcaster, isVip, isPartner, isStaff, noisy, rawIrcMessage, emoteReplacedMessage, badges, cheerBadge, bits,
                bitsInDollars)
            };
            client.RaiseEvent("OnMessageReceived", model);
        }

        /// <summary>
        /// Invokes the message sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="message">The message.</param>
        public static void InvokeMessageSent(this TwitchClient client, List<KeyValuePair<string, string>> badges, string channel, string colorHex,
            string displayName, string emoteSet, bool isModerator, bool isSubscriber, UserType userType, string message)
        {
            OnMessageSentArgs model = new OnMessageSentArgs()
            {
                SentMessage = new SentMessage(badges, channel, colorHex, displayName, emoteSet, isModerator, isSubscriber, userType, message)
            };
            client.RaiseEvent("OnMessageSent", model);
        }

        /// <summary>
        /// Invokes the moderator joined.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="username">The username.</param>
        public static void InvokeModeratorJoined(this TwitchClient client, string channel, string username)
        {
            OnModeratorJoinedArgs model = new OnModeratorJoinedArgs()
            {
                Channel = channel,
                Username = username
            };
            client.RaiseEvent("OnModeratorJoined", model);
        }

        /// <summary>
        /// Invokes the moderator left.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="username">The username.</param>
        public static void InvokeModeratorLeft(this TwitchClient client, string channel, string username)
        {
            OnModeratorLeftArgs model = new OnModeratorLeftArgs()
            {
                Channel = channel,
                Username = username
            };
            client.RaiseEvent("OnModeratorLeft", model);
        }

        /// <summary>
        /// Invokes the moderators received.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="moderators">The moderators.</param>
        public static void InvokeModeratorsReceived(this TwitchClient client, string channel, List<string> moderators)
        {
            OnModeratorsReceivedArgs model = new OnModeratorsReceivedArgs()
            {
                Channel = channel,
                Moderators = moderators
            };
            client.RaiseEvent("OnModeratorsReceived", model);
        }

        /// <summary>
        /// Invokes the new subscriber.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="color">The color.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="login">The login.</param>
        /// <param name="systemMessage">The system message.</param>
        /// <param name="systemMessageParsed">The system message parsed.</param>
        /// <param name="resubMessage">The resub message.</param>
        /// <param name="subscriptionPlan">The subscription plan.</param>
        /// <param name="subscriptionPlanName">Name of the subscription plan.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="isPartner">if set to <c>true</c> [is partner].</param>
        /// <param name="tmiSentTs">The tmi sent ts.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="rawIrc">The raw irc.</param>
        /// <param name="channel">The channel.</param>
        public static void InvokeNewSubscriber(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, Color color, string displayName,
            string emoteSet, string id, string login, string systemMessage, string msgId, string msgParamCumulativeMonths, string msgParamStreakMonths, bool msgParamShouldShareStreak, string systemMessageParsed, string resubMessage, SubscriptionPlan subscriptionPlan,
            string subscriptionPlanName, string roomId, string userId, bool isModerator, bool isTurbo, bool isSubscriber, bool isPartner, string tmiSentTs,
            UserType userType, string rawIrc, string channel)
        {
            OnNewSubscriberArgs model = new OnNewSubscriberArgs()
            {
                Subscriber = new Subscriber(badges, badgeInfo, colorHex, color, displayName, emoteSet, id, login, systemMessage, msgId, msgParamCumulativeMonths, msgParamStreakMonths, msgParamShouldShareStreak, systemMessageParsed, resubMessage,
                subscriptionPlan, subscriptionPlanName, roomId, userId, isModerator, isTurbo, isSubscriber, isPartner, tmiSentTs, userType, rawIrc, channel)
            };
            client.RaiseEvent("OnNewSubscriber", model);
        }

        /// <summary>
        /// Invokes the now hosting.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="hostedChannel">The hosted channel.</param>
        public static void InvokeNowHosting(this TwitchClient client, string channel, string hostedChannel)
        {
            OnNowHostingArgs model = new OnNowHostingArgs()
            {
                Channel = channel,
                HostedChannel = hostedChannel
            };
            client.RaiseEvent("OnNowHosting", model);
        }

        /// <summary>
        /// Invokes the raid notification.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="color">The color.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emotes">The emotes.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="login">The login.</param>
        /// <param name="moderator">if set to <c>true</c> [moderator].</param>
        /// <param name="msgId">The MSG identifier.</param>
        /// <param name="msgParamDisplayName">Display name of the MSG parameter.</param>
        /// <param name="msgParamLogin">The MSG parameter login.</param>
        /// <param name="msgParamViewerCount">The MSG parameter viewer count.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="subscriber">if set to <c>true</c> [subscriber].</param>
        /// <param name="systemMsg">The system MSG.</param>
        /// <param name="systemMsgParsed">The system MSG parsed.</param>
        /// <param name="tmiSentTs">The tmi sent ts.</param>
        /// <param name="turbo">if set to <c>true</c> [turbo].</param>
        /// <param name="userType">Type of the user.</param>
        public static void InvokeRaidNotification(this TwitchClient client, string channel, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string color, string displayName, string emotes, string id, string login, bool moderator, string msgId, string msgParamDisplayName,
            string msgParamLogin, string msgParamViewerCount, string roomId, bool subscriber, string systemMsg, string systemMsgParsed, string tmiSentTs, bool turbo, UserType userType, string userId)
        {
            OnRaidNotificationArgs model = new OnRaidNotificationArgs()
            {
                Channel = channel,
                RaidNotification = new RaidNotification(badges, badgeInfo, color, displayName, emotes, id, login, moderator, msgId, msgParamDisplayName, msgParamLogin, msgParamViewerCount,
                roomId, subscriber, systemMsg, systemMsgParsed, tmiSentTs, turbo, userType, userId)
            };
            client.RaiseEvent("OnRaidNotification", model);
        }

        /// <summary>
        /// Invokes the re subscriber.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="color">The color.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="login">The login.</param>
        /// <param name="systemMessage">The system message.</param>
        /// <param name="systemMessageParsed">The system message parsed.</param>
        /// <param name="resubMessage">The resub message.</param>
        /// <param name="subscriptionPlan">The subscription plan.</param>
        /// <param name="subscriptionPlanName">Name of the subscription plan.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="isPartner">if set to <c>true</c> [is partner].</param>
        /// <param name="tmiSentTs">The tmi sent ts.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="rawIrc">The raw irc.</param>
        /// <param name="channel">The channel.</param>
        public static void InvokeReSubscriber(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, Color color, string displayName,
            string emoteSet, string id, string login, string systemMessage, string msgId, string msgParamCumulativeMonths, string msgParamStreakMonths, bool msgParamShouldShareStreak, string systemMessageParsed, string resubMessage, SubscriptionPlan subscriptionPlan,
            string subscriptionPlanName, string roomId, string userId, bool isModerator, bool isTurbo, bool isSubscriber, bool isPartner, string tmiSentTs,
            UserType userType, string rawIrc, string channel)
        {
            OnReSubscriberArgs model = new OnReSubscriberArgs()
            {
                ReSubscriber = new ReSubscriber(badges, badgeInfo, colorHex, color, displayName, emoteSet, id, login, systemMessage, msgId, msgParamCumulativeMonths, msgParamStreakMonths, msgParamShouldShareStreak, systemMessageParsed, resubMessage,
                subscriptionPlan, subscriptionPlanName, roomId, userId, isModerator, isTurbo, isSubscriber, isPartner, tmiSentTs, userType, rawIrc, channel)
            };
            client.RaiseEvent("OnReSubscriber", model);
        }

        /// <summary>
        /// Invokes the send receive data.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="data">The data.</param>
        /// <param name="direction">The direction.</param>
        public static void InvokeSendReceiveData(this TwitchClient client, string data, SendReceiveDirection direction)
        {
            OnSendReceiveDataArgs model = new OnSendReceiveDataArgs()
            {
                Data = data,
                Direction = direction
            };
            client.RaiseEvent("OnSendReceiveData", model);
        }

        /// <summary>
        /// Invokes the user banned.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="username">The username.</param>
        /// <param name="banReason">The ban reason.</param>
        /// <param name="roomId">The channel id.</param>
        /// <param name="targetUserId">The user id.</param>
        public static void InvokeUserBanned(this TwitchClient client, string channel, string username, string banReason, string roomId, string targetUserId)
        {
            OnUserBannedArgs model = new OnUserBannedArgs()
            {
                UserBan = new UserBan(channel, username, banReason, roomId, targetUserId)
            };
            client.RaiseEvent("OnUserBanned", model);
        }

        /// <summary>
        /// Invokes the user joined.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="username">The username.</param>
        public static void InvokeUserJoined(this TwitchClient client, string channel, string username)
        {
            OnUserJoinedArgs model = new OnUserJoinedArgs()
            {
                Channel = channel,
                Username = username
            };
            client.RaiseEvent("OnUserJoined", model);
        }

        /// <summary>
        /// Invokes the user left.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="username">The username.</param>
        public static void InvokeUserLeft(this TwitchClient client, string channel, string username)
        {
            OnUserLeftArgs model = new OnUserLeftArgs()
            {
                Channel = channel,
                Username = username
            };
            client.RaiseEvent("OnUserLeft", model);
        }

        /// <summary>
        /// Invokes the user state changed.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="isSubscriber">if set to <c>true</c> [is subscriber].</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        /// <param name="userType">Type of the user.</param>
        public static void InvokeUserStateChanged(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, string displayName,
            string emoteSet, string channel, bool isSubscriber, bool isModerator, UserType userType)
        {
            OnUserStateChangedArgs model = new OnUserStateChangedArgs()
            {
                UserState = new UserState(badges, badgeInfo, colorHex, displayName, emoteSet, channel, isSubscriber, isModerator, userType)
            };
            client.RaiseEvent("OnUserStateChanged", model);
        }

        /// <summary>
        /// Invokes the user timedout.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="username">The username.</param>
        /// <param name="timeoutDuration">Duration of the timeout.</param>
        /// <param name="timeoutReason">The timeout reason.</param>
        public static void InvokeUserTimedout(this TwitchClient client, string channel, string username, int timeoutDuration, string timeoutReason)
        {
            OnUserTimedoutArgs model = new OnUserTimedoutArgs()
            {
                UserTimeout = new UserTimeout(channel, username, timeoutDuration, timeoutReason)
            };
            client.RaiseEvent("OnUserTimedout", model);
        }

        /// <summary>
        /// Invokes the whisper command received.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="color">The color.</param>
        /// <param name="username">The username.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="message">The message.</param>
        /// <param name="userType">Type of the user.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="argumentsAsString">The arguments as string.</param>
        /// <param name="argumentsAsList">The arguments as list.</param>
        /// <param name="commandIdentifier">The command identifier.</param>
        public static void InvokeWhisperCommandReceived(this TwitchClient client, List<KeyValuePair<string, string>> badges, string colorHex, Color color, string username, string displayName, EmoteSet emoteSet, string threadId, string messageId,
            string userId, bool isTurbo, string botUsername, string message, UserType userType, string commandText, string argumentsAsString, List<string> argumentsAsList, char commandIdentifier)
        {
            WhisperMessage whisperMsg = new WhisperMessage(badges, colorHex, color, username, displayName, emoteSet, threadId, messageId, userId, isTurbo, botUsername, message, userType);
            OnWhisperCommandReceivedArgs model = new OnWhisperCommandReceivedArgs()
            {
                Command = new WhisperCommand(whisperMsg, commandText, argumentsAsString, argumentsAsList, commandIdentifier)
            };
            client.RaiseEvent("OnWhisperCommandReceived", model);
        }

        /// <summary>
        /// Invokes the whisper received.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="badges">The badges.</param>
        /// <param name="colorHex">The color hexadecimal.</param>
        /// <param name="color">The color.</param>
        /// <param name="username">The username.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emoteSet">The emote set.</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isTurbo">if set to <c>true</c> [is turbo].</param>
        /// <param name="botUsername">The bot username.</param>
        /// <param name="message">The message.</param>
        /// <param name="userType">Type of the user.</param>
        public static void InvokeWhisperReceived(this TwitchClient client, List<KeyValuePair<string, string>> badges, string colorHex, Color color, string username, string displayName, EmoteSet emoteSet, string threadId, string messageId,
            string userId, bool isTurbo, string botUsername, string message, UserType userType)
        {
            OnWhisperReceivedArgs model = new OnWhisperReceivedArgs()
            {
                WhisperMessage = new WhisperMessage(badges, colorHex, color, username, displayName, emoteSet, threadId, messageId, userId, isTurbo, botUsername, message, userType)
            };
            client.RaiseEvent("OnWhisperReceived", model);
        }

        /// <summary>
        /// Invokes the whisper sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="username">The username.</param>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        public static void InvokeWhisperSent(this TwitchClient client, string username, string receiver, string message)
        {
            OnWhisperSentArgs model = new OnWhisperSentArgs()
            {
                Message = message,
                Receiver = receiver,
                Username = username
            };
            client.RaiseEvent("OnWhisperSent", model);
        }
    }
}
