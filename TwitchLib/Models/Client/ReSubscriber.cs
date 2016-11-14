using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    /// <summary>Class representing a resubscriber.</summary>
    public class ReSubscriber
    {
        /// <summary>Property representing list of badges assigned.</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        /// <summary>Property representing the colorhex of the resubscriber.</summary>
        public string ColorHex { get; protected set; }
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
        /// <summary>Property representing </summary>
        public string ResubMessage { get; protected set; }
        /// <summary>Property representing number of months of being subscribed.</summary>
        public int Months { get; protected set; }
        /// <summary>Property representing the room id.</summary>
        public int RoomId { get; protected set; }
        /// <summary>Property representing the user's id.</summary>
        public int UserId { get; protected set; }
        /// <summary>Property representing whether or not the resubscriber is a moderator.</summary>
        public bool Mod { get; protected set; }
        /// <summary>Property representing whether or not the resubscriber is a turbo member.</summary>
        public bool Turbo { get; protected set; }
        /// <summary>Property representing whether or not the resubscriber is a subscriber (YES).</summary>
        public bool Sub { get; protected set; }
        /// <summary>Property representing the user type of the resubscriber.</summary>
        public Enums.UserType UserType { get; protected set; }
        /// <summary>Property representing the raw IRC message (for debugging/customized parsing)</summary>
        public string RawIrc { get; protected set; }
        /// <summary>Property representing the channel the resubscription happened in.</summary>
        public string Channel { get; protected set; }
        /// <summary>Property representing if the resubscription came from Twitch Prime.</summary>
        public bool IsTwitchPrime { get; protected set; }
        // @badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;id=9dasn-asdibas-asdba-as8as;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG

        /// <summary>ReSubscriber object constructor.</summary>
        public ReSubscriber(string ircString)
        {
            RawIrc = ircString;
            foreach(string section in ircString.Split(';'))
            {
                if(section.Contains("="))
                {
                    string key = section.Split('=')[0];
                    string value = section.Split('=')[1];
                    switch (key)
                    {
                        case "@badges":
                            Badges = new List<KeyValuePair<string, string>>();
                            foreach (string badgeValue in value.Split(','))
                                Badges.Add(new KeyValuePair<string, string>(badgeValue.Split('/')[0], badgeValue.Split('/')[1]));
                            break;
                        case "color":
                            ColorHex = value;
                            break;
                        case "display-name":
                            DisplayName = value;
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
                            Mod = value == "1";
                            break;
                        case "msg-param-months":
                            Months = int.Parse(value);
                            break;
                        case "room-id":
                            RoomId = int.Parse(value);
                            break;
                        case "subscriber":
                            Sub = value == "1";
                            break;
                        case "system-msg":
                            SystemMessage = value;
                            SystemMessageParsed = value.Replace("\\s", " ");
                            break;
                        case "turbo":
                            Turbo = value == "1";
                            break;
                        case "user-id":
                            UserId = int.Parse(value);
                            break;
                    }
                }
            }
            // Parse user-type
            if(ircString.Contains("=") && ircString.Contains(" "))
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
            if (ircString.Contains("#") && ircString.Split('#').Count() > 2)
                if (ircString.Split('#')[2].Contains(" "))
                    Channel = ircString.Split('#')[2].Split(' ')[0];
                else
                    Channel = ircString.Split('#')[2];

            // Parse sub message
            ResubMessage = "";
            if(ircString.Contains($"#{Channel} :"))
            {
                string rawParsedIrc = ircString.Split(new string[] { $"#{Channel} :" }, StringSplitOptions.None)[0];
                ResubMessage = ircString.Replace($"{rawParsedIrc}#{Channel} :", "");
            }

            // Check if Twitch Prime
            IsTwitchPrime = SystemMessageParsed.ToLower().Contains("with twitch prime");
            
        }

        /// <summary>Overriden ToString method, prints out all properties related to resub.</summary>
        public override string ToString()
        {
            return $"Badges: {Badges.Count}, color hex: {ColorHex}, display name: {DisplayName}, emote set: {EmoteSet}, login: {Login}, system message: {SystemMessage}, " + 
                $"resub message: {ResubMessage}, months: {Months}, room id: {RoomId}, user id: {UserId}, mod: {Mod}, turbo: {Turbo}, sub: {Sub}, user type: {UserType}, " + 
                $"channel: {Channel}, raw irc: {RawIrc}";
        }
    }
}
