using System;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD
using TwitchLib.NetCore.Extensions.NetCore;
#endif
#if NET452
    using System.Drawing;
#endif

namespace TwitchLib.Models.Client
{
    /// <summary>Class representing a resubscriber.</summary>
    public class SubscriberBase
    {
        /// <summary>Property representing list of badges assigned.</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        /// <summary>Property representing the colorhex of the resubscriber.</summary>
        public string ColorHex { get; protected set; }
        /// <summary>Property representing HEX color as a System.Drawing.Color object.</summary>
        public System.Drawing.Color Color { get; protected set; }
        /// <summary>Property representing resubscriber's customized display name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing emote set of resubscriber.</summary>
        public string EmoteSet { get; protected set; }
        /// <summary>Property representing resub message id</summary>
        public string Id { get; protected set; }
        /// <summary>Property representing login of resubscription event.</summary>
        public string Login { get; protected set; }
        /// <summary>Property representing internval system message value.</summary>
        public string SystemMessage { get; protected set; }
        /// <summary>Property representing internal system message value, parsed.</summary>
        public string SystemMessageParsed { get; protected set; }
        /// <summary>Property representing system message.</summary>
        public string ResubMessage { get; protected set; }
        /// <summary>Property representing the plan a user is on.</summary>
        public Enums.SubscriptionPlan SubscriptionPlan { get; protected set; } = Enums.SubscriptionPlan.NotSet;
        /// <summary>Property representing the subscription plan name.</summary>
        public string SubscriptionPlanName { get; protected set; }
        /// <summary>Property representing the room id.</summary>
        public string RoomId { get; protected set; }
        /// <summary>Property representing the user's id.</summary>
        public string UserId { get; protected set; }
        /// <summary>Property representing whether or not the resubscriber is a moderator.</summary>
        public bool IsModerator { get; protected set; }
        /// <summary>Property representing whether or not the resubscriber is a turbo member.</summary>
        public bool IsTurbo { get; protected set; }
        /// <summary>Property representing whether or not the resubscriber is a subscriber (YES).</summary>
        public bool IsSubscriber { get; protected set; }
        /// <summary>Property representing whether or not person is a partner.</summary>
        public bool IsPartner { get; protected set; }
        /// <summary>Property representing the tmi-sent-ts value.</summary>
        public string TmiSentTs { get; protected set; }
        /// <summary>Property representing the user type of the resubscriber.</summary>
        public Enums.UserType UserType { get; protected set; }
        /// <summary>Property representing the raw IRC message (for debugging/customized parsing)</summary>
        public string RawIrc { get; protected set; }
        /// <summary>Property representing the channel the resubscription happened in.</summary>
        public string Channel { get; protected set; }
        /// <summary>[DEPRECATED, USE SUBSCRIPTIONPLAN PROPERTY] Property representing if the resubscription came from Twitch Prime.</summary>
        public bool IsTwitchPrime { get; protected set; }
        // @badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;id=9dasn-asdibas-asdba-as8as;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG

        protected readonly int months;

        /// <summary>Subscriber object constructor.</summary>
        public SubscriberBase(string ircString)
        {
            RawIrc = ircString;
            foreach (var section in ircString.Split(';'))
            {
                if (!section.Contains("=")) continue;

                var key = section.Split('=')[0];
                var value = section.Split('=')[1];
                switch (key)
                {
                    case "@badges":
                        Badges = new List<KeyValuePair<string, string>>();
                        foreach (var badgeValue in value.Split(','))
                            if (badgeValue.Contains('/'))
                                Badges.Add(new KeyValuePair<string, string>(badgeValue.Split('/')[0], badgeValue.Split('/')[1]));
                        // iterate through badges for special circumstances
                        foreach (var badge in Badges)
                        {
                            if (badge.Key == "partner")
                                IsPartner = true;
                        }
                        break;
                    case "color":
                        ColorHex = value;
                        if (!string.IsNullOrEmpty(ColorHex))
                            Color = ColorTranslator.FromHtml(ColorHex);
                        break;
                    case "display-name":
                        DisplayName = value.Replace(" ", "");
                        break;
                    case "emotes":
                        EmoteSet = value;
                        break;
                    case "id":
                        Id = value;
                        break;
                    case "login":
                        Login = value;
                        break;
                    case "mod":
                        IsModerator = value == "1";
                        break;
                    case "msg-param-months":
                        months = int.Parse(value);
                        break;
                    case "msg-param-sub-plan":
                        switch (value.ToLower())
                        {
                            case "prime":
                                SubscriptionPlan = Enums.SubscriptionPlan.Prime;
                                break;
                            case "1000":
                                SubscriptionPlan = Enums.SubscriptionPlan.Tier1;
                                break;
                            case "2000":
                                SubscriptionPlan = Enums.SubscriptionPlan.Tier2;
                                break;
                            case "3000":
                                SubscriptionPlan = Enums.SubscriptionPlan.Tier3;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(value.ToLower));
                        }
                        break;
                    case "msg-param-sub-plan-name":
                        SubscriptionPlanName = value.Replace("\\s", " ");
                        break;
                    case "room-id":
                        RoomId = value;
                        break;
                    case "subscriber":
                        IsSubscriber = value == "1";
                        break;
                    case "system-msg":
                        SystemMessage = value;
                        SystemMessageParsed = value.Replace("\\s", " ");
                        break;
                    case "tmi-sent-ts":
                        TmiSentTs = value;
                        break;
                    case "turbo":
                        IsTurbo = value == "1";
                        break;
                    case "user-id":
                        UserId = value;
                        break;
                }
            }
            // Parse user-type
            if (ircString.Contains("=") && ircString.Contains(" "))
            {
                switch (ircString.Split(' ')[0].Split(';')[13].Split('=')[1])
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


            // Parse channel
            if (ircString.Contains("#"))
            {
                if (ircString.Split('#').Length > 2)
                {
                    Channel = ircString.Split('#')[2].Contains(" ") ? ircString.Split('#')[2].Split(' ')[0].Replace(" ", "") : ircString.Split('#')[2].Replace(" ", "");
                }
                else
                {
                    Channel = ircString.Split('#')[1];
                    if (Channel.Contains(" "))
                        Channel = Channel.Split(' ')[0];
                    if (Channel.Contains(":"))
                        Channel = Channel.Split(':')[0];
                }
            }

            // Parse sub message
            ResubMessage = "";
            if (ircString.Contains($"#{Channel} :"))
            {
                var rawParsedIrc = ircString.Split(new[] { $"#{Channel} :" }, StringSplitOptions.None)[0];
                ResubMessage = ircString.Replace($"{rawParsedIrc}#{Channel} :", "");
            }

            // Check if Twitch Prime
            IsTwitchPrime = SubscriptionPlan == Enums.SubscriptionPlan.Prime;
        }

        /// <summary>Overriden ToString method, prints out all properties related to resub.</summary>
        public override string ToString()
        {
            return $"Badges: {Badges.Count}, color hex: {ColorHex}, display name: {DisplayName}, emote set: {EmoteSet}, login: {Login}, system message: {SystemMessage}, " +
                $"resub message: {ResubMessage}, months: {months}, room id: {RoomId}, user id: {UserId}, mod: {IsModerator}, turbo: {IsTurbo}, sub: {IsSubscriber}, user type: {UserType}, " +
                   $"channel: {Channel}, raw irc: {RawIrc}";
        }
    }
}
