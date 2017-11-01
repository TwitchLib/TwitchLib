﻿namespace TwitchLib.Models.Client
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using TwitchLib.NetCore.Extensions.NetCore;
    #endregion
    /// <summary>Class represents ChatMessage in a Twitch channel.</summary>
    public class ChatMessage
    {
        private MessageEmoteCollection _emoteCollection;

        /// <summary>Twitch username of the bot that received the message.</summary>
        public string BotUsername { get; protected set; }
        /// <summary>Twitch-unique integer assigned on per account basis.</summary>
        public string UserId { get; protected set; }
        /// <summary>Username of sender of chat message.</summary>
        public string Username { get; protected set; }
        /// <summary>Case-sensitive username of sender of chat message.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Hex representation of username color in chat (THIS CAN BE NULL IF VIEWER HASN'T SET COLOR).</summary>
        public string ColorHex { get; protected set; }
        /// <summary>Property representing HEX color as a System.Drawing.Color object.</summary>
        public Color Color { get; protected set; }
        /// <summary>Emote Ids that exist in message.</summary>
        public EmoteSet EmoteSet { get; protected set; }
        /// <summary>Twitch chat message contents.</summary>
        public string Message { get; protected set; }
        /// <summary>User type can be viewer, moderator, global mod, admin, or staff</summary>
        public Enums.UserType UserType { get; protected set; }
        /// <summary>Twitch channel message was sent from (useful for multi-channel bots).</summary>
        public string Channel { get; protected set; }
        /// <summary>Channel specific subscriber status.</summary>
        public bool IsSubscriber { get; protected set; }
        /// <summary>Number of months a person has been subbed.</summary>
        public int SubscribedMonthCount { get; protected set; }
        /// <summary>Unique identifier of chat room.</summary>
        public string RoomId { get; protected set; }
        /// <summary>Twitch site-wide turbo status.</summary>
        public bool IsTurbo { get; protected set; }
        /// <summary>Channel specific moderator status.</summary>
        public bool IsModerator { get; protected set; }
        /// <summary>Chat message /me identifier flag.</summary>
        public bool IsMe { get; protected set; }
        /// <summary>Chat message from broadcaster identifier flag</summary>
        public bool IsBroadcaster { get; protected set; }
        /// <summary>Experimental property noisy determination by Twitch.</summary>
        public Enums.Noisy Noisy { get; protected set; } = Enums.Noisy.NotSet;
        /// <summary>Raw IRC-style text received from Twitch.</summary>
        public string RawIrcMessage { get; protected set; }
        /// <summary>Text after emotes have been handled (if desired). Will be null if replaceEmotes is false.</summary>
        public string EmoteReplacedMessage { get; protected set; }
        /// <summary>List of key-value pair badges.</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        /// <summary>If a cheer badge exists, this property represents the raw value and color (more later). Can be null.</summary>
        public CheerBadge CheerBadge { get; protected set; }
        /// <summary>If viewer sent bits in their message, total amount will be here.</summary>
        public int Bits { get; protected set; }
        /// <summary>Number of USD (United States Dollars) spent on bits.</summary>
        public double BitsInDollars { get; protected set; }

        private string emoteSetStorage = null;

        //Example IRC message: @badges=moderator/1,warcraft/alliance;color=;display-name=Swiftyspiffyv4;emotes=;mod=1;room-id=40876073;subscriber=0;turbo=0;user-id=103325214;user-type=mod :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #swiftyspiffy :asd
        /// <summary>Constructor for ChatMessage object.</summary>
        /// <param name="botUsername">The username of the bot that received the message.</param>
        /// <param name="ircString">The raw string received from Twitch to be processed.</param>
        /// <param name="emoteCollection">The <see cref="MessageEmoteCollection"/> to register new emotes on and, if desired, use for emote replacement.</param>
        /// <param name="replaceEmotes">Whether to replace emotes for this chat message. Defaults to false.</param>
        public ChatMessage(string botUsername, string ircString, ref MessageEmoteCollection emoteCollection, bool replaceEmotes = false)
        {
            BotUsername = botUsername;
            RawIrcMessage = ircString;
            _emoteCollection = emoteCollection;
            var parts = ircString.Split(';');
            foreach (var part in parts)
            {
                if (part.Contains("!"))
                {
                    if (Channel == null)
                        Channel = part.Split('#')[1].Split(' ')[0];
                    if (Username == null)
                        Username = part.Split('!')[1].Split('@')[0];
                    if(part.Split('=').Count() > 1 && part.Split('=')[1].Contains(" "))
                    {
                        switch (part.Split('=')[1].Split(' ')[0])
                        {
                            case "mod":
                                UserType = Enums.UserType.Moderator;
                                break;
                            case "global_mod":
                                UserType = Enums.UserType.GlobalModerator;
                                break;
                            case "admin":
                                UserType = Enums.UserType.Admin;
                                break;
                            case "staff":
                                UserType = Enums.UserType.Staff;
                                break;
                            default:
                                UserType = Enums.UserType.Viewer;
                                break;
                        }
                    }
                }
                else if (part.Contains("@badges="))
                {
                    Badges = new List<KeyValuePair<string, string>>();
                    string badges = part.Split('=')[1];
                    if (badges.Contains('/'))
                    {
                        if (!badges.Contains(","))
                            Badges.Add(new KeyValuePair<string, string>(badges.Split('/')[0], badges.Split('/')[1]));
                        else
                            foreach (string badge in badges.Split(','))
                                Badges.Add(new KeyValuePair<string, string>(badge.Split('/')[0], badge.Split('/')[1]));
                    }
                    // Iterate through saved badges for special circumstances
                    foreach (KeyValuePair<string, string> badge in Badges)
                    {
                        if (badge.Key == "bits")
                            CheerBadge = new CheerBadge(int.Parse(badge.Value));
                        if (badge.Key == "subscriber")
                            SubscribedMonthCount = int.Parse(badge.Value);
                    }
                }
                else if (part.Contains("bits="))
                {
                    Bits = int.Parse(part.Split('=')[1]);
                    BitsInDollars = convertBitsToUSD(Bits);
                }
                else if (part.Contains("color="))
                {
                    if (ColorHex == null)
                        ColorHex = part.Split('=')[1];
                    if (!string.IsNullOrEmpty(ColorHex))
                        Color = ColorTranslator.FromHtml(ColorHex);
                }
                else if (part.Contains("display-name"))
                {
                    if (DisplayName == null)
                        DisplayName = part.Split('=')[1];
                }
                else if (part.Contains("emotes="))
                {
                    if (emoteSetStorage == null)
                        emoteSetStorage = part.Split('=')[1];
                }
                else if (part.Contains("subscriber="))
                {
                    IsSubscriber = part.Split('=')[1] == "1";
                }
                else if (part.Contains("turbo="))
                {
                    IsTurbo = part.Split('=')[1] == "1";
                }
                else if (part.Contains("user-id="))
                {
                    UserId = part.Split('=')[1];
                }
                else if (part.Contains("mod="))
                {
                    IsModerator = part.Split('=')[1] == "1";
                }
                else if(part.Contains("room-id="))
                {
                    RoomId = part.Split('=')[1];
                }
                else if(part.Contains("noisy="))
                {
                    Noisy = (part.Split('=')[1] == "1") ? Enums.Noisy.True : Enums.Noisy.False;
                }
            }
            Message = ircString.Split(new[] { $" PRIVMSG #{Channel} :" }, StringSplitOptions.None)[1];
            EmoteSet = new EmoteSet(emoteSetStorage, Message);
            if (Message.Length > 0 && (byte)Message[0] == 1 && (byte)Message[Message.Length - 1] == 1)
            {
                //Actions (/me {action}) are wrapped by byte=1 and prepended with "ACTION "
                //This setup clears all of that leaving just the action's text.
                //If you want to clear just the nonstandard bytes, use:
                //_message = _message.Substring(1, text.Length-2);
                if (Message.Contains("ACTION"))
                {
                 	Message = Message.Substring(8, Message.Length - 9);
                        IsMe = true;
                }
            }

            //Parse the emoteSet
            if (EmoteSet != null && Message != null && EmoteSet.Emotes.Count > 0)
            {
                string[] uniqueEmotes = EmoteSet.RawEmoteSetString.Split('/');
                string id, text;
                int firstColon, firstComma, firstDash, low, high;
                foreach (string emote in uniqueEmotes)
                {
                    firstColon = emote.IndexOf(':');
                    firstComma = emote.IndexOf(',');
                    if (firstComma == -1) firstComma = emote.Length;
                    firstDash = emote.IndexOf('-');
                    if (firstColon > 0 && firstDash > firstColon && firstComma > firstDash)
                    {
                        if (Int32.TryParse(emote.Substring(firstColon + 1, (firstDash - firstColon) - 1), out low) &&
                            Int32.TryParse(emote.Substring(firstDash + 1, (firstComma - firstDash) - 1), out high))
                        {
                            if (low >= 0 && low < high && high < Message.Length)
                            {
                                //Valid emote, let's parse
                                id = emote.Substring(0, firstColon);
                                //Pull the emote text from the message
                                text = Message.Substring(low, (high - low) + 1);
                                _emoteCollection.Add(new MessageEmote(id, text));
                            }
                        }
                    }
                }
                if (replaceEmotes)
                {
                    EmoteReplacedMessage = _emoteCollection.ReplaceEmotes(Message);
                }
            }

            // Check if display name was set, and if it wasn't, set it to username
            if (string.IsNullOrEmpty(DisplayName))
                DisplayName = Username;

            // Check if message is from broadcaster
            if (Channel.ToLower() == Username.ToLower())
            {
                UserType = Enums.UserType.Broadcaster;
                IsBroadcaster = true;
            }
        }

        /// <summary>Chat Message constructor with passed in values.</summary>
        public ChatMessage(List<KeyValuePair<string, string>> badges, string channel, string colorHex, string displayName,
            EmoteSet emoteSet, bool moderator, bool subscriber, Enums.UserType userType, string message)
        {
            Badges = badges;
            Channel = channel;
            ColorHex = colorHex;
            Username = DisplayName = displayName;
            EmoteSet = emoteSet;
            IsModerator = moderator;
            IsSubscriber = subscriber;
            UserType = userType;
            Message = message;
        }

        private static bool convertToBool(string data)
        {
            return data == "1";
        }

        private static double convertBitsToUSD(int bits)
        {
            /*
            Conversion Rates
            100 bits = $1.40
            500 bits = $7.00
            1500 bits = $19.95 (5%)
            5000 bits = $64.40 (8%)
            10000 bits = $126.00 (10%)
            25000 bits = $308.00 (12%)
            */
            if (bits < 1500)
            {
                return ((double)(bits) / 100) * 1.4;
            }
            if (bits < 5000)
            {
                return ((double)(bits) / 1500) * 19.95;
            }
            if (bits < 10000)
            {
                return ((double)(bits) / 5000) * 64.40;
            }
            if (bits < 25000)
            {
                return ((double)(bits) / 10000) * 126;
            }
            return ((double)(bits) / 25000) * 308;
        }
    }
}