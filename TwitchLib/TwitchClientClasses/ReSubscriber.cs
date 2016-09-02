using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ReSubscriber
    {
        public List<KeyValuePair<string, string>> Badges { get; protected set; }
        public string ColorHex { get; protected set; }
        public string DisplayName { get; protected set; }
        public string EmoteSet { get; protected set; }
        public string Login { get; protected set; }
        public string SystemMessage { get; protected set; }
        public string ResubMessage { get; protected set; }
        public int Months { get; protected set; }
        public int RoomId { get; protected set; }
        public int UserId { get; protected set; }
        public bool Mod { get; protected set; }
        public bool Turbo { get; protected set; }
        public bool Sub { get; protected set; }
        public Common.UserType UserType { get; protected set; }
        public string RawIrc { get; protected set; }
        public string Channel { get; protected set; }

        // @badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG
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
                        UserType = Common.UserType.Moderator;
                        break;
                    case "global_mod":
                        UserType = Common.UserType.GlobalModerator;
                        break;
                    case "admin":
                        UserType = Common.UserType.Admin;
                        break;
                    case "staff":
                        UserType = Common.UserType.Staff;
                        break;
                    default:
                        UserType = Common.UserType.Viewer;
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
            string rawParsedIrc = ircString.Split(new string[] { $"#{Channel} :" }, StringSplitOptions.None)[0];
            ResubMessage = ircString.Replace($"{rawParsedIrc}#{Channel} :", "");
        }

        public override string ToString()
        {
            return $"Badges: {Badges.Count}, color hex: {ColorHex}, display name: {DisplayName}, emote set: {EmoteSet}, login: {Login}, system message: {SystemMessage}, " + 
                $"resub message: {ResubMessage}, months: {Months}, room id: {RoomId}, user id: {UserId}, mod: {Mod}, turbo: {Turbo}, sub: {Sub}, user type: {UserType}, " + 
                $"channel: {Channel}, raw irc: {RawIrc}";
        }
    }
}
