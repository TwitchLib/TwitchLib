using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    /// <summary>Class representing state of a specific user.</summary>
    public class UserState
    {
        /// <summary>Properrty representing the chat badges a specific user has.</summary>
        public List<KeyValuePair<string, string>> Badges { get; protected set; } = new List<KeyValuePair<string, string>>();
        /// <summary>Properrty representing HEX user's name.</summary>
        public string ColorHex { get; protected set; }
        /// <summary>Property representing user's display name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing emote sets available to user.</summary>
        public string EmoteSet { get; protected set; }
        /// <summary>Property representing channel.</summary>
        public string Channel { get; protected set; }
        /// <summary>Property representing subscriber status.</summary>
        public bool Subscriber { get; protected set; }
        /// <summary>Property representing Turbo status.</summary>
        public bool Moderator { get; protected set; }
        /// <summary>Property representing returned user type of user.</summary>
        public Enums.UserType UserType { get; protected set; }

        /// <summary>
        /// Constructor for UserState.
        /// </summary>
        /// <param name="ircString"></param>
        public UserState(string ircString)
        {
            foreach(string part in ircString.Split(';'))
            {
                // The 'user-type' section does not have a ; suffix, we will account for this outside of for loop, we should exit loop immediately
                if (part.Contains(" :tmi.twitch.tv USERSTATE "))
                    break;
                if(!part.Contains("="))
                {
                    // This should never happen, unless Twitch changes their shit.
                    Console.WriteLine($"Unaccounted for [UserState]: {part}");
                    continue;
                }
                switch(part.Split('=')[0])
                {
                    case "@badges":
                        string badges = part.Split('=')[1];
                        if (badges.Contains('/'))
                        {
                            if (!badges.Contains(","))
                                Badges.Add(new KeyValuePair<string, string>(badges.Split('/')[0], badges.Split('/')[1]));
                            else
                                foreach (string badge in badges.Split(','))
                                    Badges.Add(new KeyValuePair<string, string>(badge.Split('/')[0], badge.Split('/')[1]));
                        }
                        break;
                    case "color":
                        ColorHex = part.Split('=')[1];
                        break;
                    case "display-name":
                        DisplayName = part.Split('=')[1];
                        break;
                    case "emote-sets":
                        EmoteSet = part.Split('=')[1];
                        break;
                    case "mod":
                        Moderator = part.Split('=')[1] == "1";
                        break;
                    case "subscriber":
                        Subscriber = part.Split('=')[1] == "1";
                        break;
                    default:
                        // This should never happen, unless Twitch changes their shit
                        Console.WriteLine($"Unaccounted for [UserState]: {part.Split('=')[0]}");
                        break;
                }
            }
            // Lets deal with that user-type
            switch (ircString.Split('=')[6].Split(' ')[0])
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
            Channel = ircString.Split(' ')[3].Replace("#", "");
            if (DisplayName.ToLower() == Channel.ToLower())
                UserType = Enums.UserType.Broadcaster;
        }
    }
}