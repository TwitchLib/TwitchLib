using System;

namespace TwitchLib
{
    public class ChatMessage
    {
        public enum UType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        private string _emoteSet;

        public int UserId { get; }

        public string Username { get; }

        public string DisplayName { get; }

        public string ColorHex { get; }

        public string Message { get; }

        public UType UserType { get; }

        public string Channel { get; }

        public bool IsSubscriber { get; }

        public bool IsTurbo { get; }

        public bool HasModFlag { get; }

        public string RawIrcMessage { get; }

        //@color=#CC00C9;display-name=astickgamer;emotes=70803:6-11;sent-ts=1447446917994;subscriber=1;tmi-sent-ts=1447446957359;turbo=0;user-id=24549902;user-type= :astickgamer!astickgamer@astickgamer.tmi.twitch.tv PRIVMSG #cohhcarnage :cjb2, cohhHi
        public ChatMessage(string ircString)
        {
            RawIrcMessage = ircString;
            //@color=asd;display-name=Swiftyspiffyv4;emotes=;subscriber=0;turbo=0;user-id=103325214;user-type=asd :swiftyspiffyv4!swiftyspiffyv4@swiftyspiffyv4.tmi.twitch.tv PRIVMSG #burkeblack :this is a test lol
            foreach (var part in ircString.Split(';'))
            {
                if (part.Contains("!"))
                {
                    if (Channel == null)
                        Channel = part.Split('#')[1].Split(' ')[0];
                    if (Username == null)
                        Username = part.Split('!')[1].Split('@')[0];
                }
                else if (part.Contains("@color="))
                {
                    if (ColorHex == null)
                        ColorHex = part.Split('=')[1];
                }
                else if (part.Contains("display-name"))
                {
                    if (DisplayName == null)
                        DisplayName = part.Split('=')[1];
                }
                else if (part.Contains("emotes="))
                {
                    if (_emoteSet == null)
                        _emoteSet = part.Split('=')[1];
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
                    UserId = int.Parse(part.Split('=')[1]);
                }
                else if (part.Contains("user-type="))
                {
                    switch (part.Split('=')[1].Split(' ')[0])
                    {
                        case "mod":
                            UserType = UType.Moderator;
                            break;
                        case "global_mod":
                            UserType = UType.GlobalModerator;
                            break;
                        case "admin":
                            UserType = UType.Admin;
                            break;
                        case "staff":
                            UserType = UType.Staff;
                            break;
                        default:
                            UserType = UType.Viewer;
                            break;
                    }
                }
                else if (part.Contains("mod="))
                {
                    HasModFlag = part.Split('=')[1] == "1";
                }
            }
            Message = ircString.Split(new[] {$" PRIVMSG #{Channel} :"}, StringSplitOptions.None)[1];
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}