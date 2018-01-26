using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.Client
{
    public class RitualNewChatter
    {
        public string Badges { get; protected set; }
        public string Color { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Emotes { get; protected set; }
        public string Id { get; protected set; }
        public string Login { get; protected set; }
        public bool Moderator { get; protected set; }
        public string MsgId { get; protected set; }
        public string MsgParamRitualName { get; protected set; }
        public string RoomId { get; protected set; }
        public bool Subscriber { get; protected set; }
        public string SystemMsgParsed { get; protected set; }
        public string SystemMsg { get; protected set; }
        public string TmiSentTs { get; protected set; }
        public bool Turbo { get; protected set; }
        public string UserId { get; protected set; }
        public string UserType { get; protected set; }
        public string Message { get; protected set; }

        // badges=subscriber/0;color=#0000FF;display-name=KittyJinxu;emotes=30259:0-6;id=1154b7c0-8923-464e-a66b-3ef55b1d4e50;
        // login=kittyjinxu;mod=0;msg-id=ritual;msg-param-ritual-name=new_chatter;room-id=35740817;subscriber=1;
        // system-msg=@KittyJinxu\sis\snew\shere.\sSay\shello!;tmi-sent-ts=1514387871555;turbo=0;user-id=187446639;
        // user-type= USERNOTICE #thorlar kittyjinxu > #thorlar: HeyGuys
        public RitualNewChatter(string ircMessage)
        {
            if (ircMessage[0] == '@')
                ircMessage = ircMessage.Substring(0, ircMessage.Length - 1);
            var main = ircMessage.Split(new string[] { " USERNOTICE" }, StringSplitOptions.None);
            Message = main[1].Split(new string[] { ": " }, StringSplitOptions.None)[1];
            foreach (var part in main[0].Split(';'))
            {
                if (part.Contains("="))
                {
                    var key = part.Split('=')[0];
                    var val = part.Split('=')[1];
                    switch (key)
                    {
                        case "badges":
                            Badges = val;
                            break;
                        case "color":
                            Color = val;
                            break;
                        case "display-name":
                            DisplayName = val;
                            break;
                        case "emotes":
                            Emotes = val;
                            break;
                        case "id":
                            Id = val;
                            break;
                        case "login":
                            Login = val;
                            break;
                        case "mod":
                            Moderator = (val == "1");
                            break;
                        case "msg-id":
                            MsgId = val;
                            break;
                        case "msg-param-ritual-name":
                            MsgParamRitualName = val;
                            break;
                        case "room-id":
                            RoomId = val;
                            break;
                        case "subscriber":
                            Subscriber = (val == "1");
                            break;
                        case "system-msg":
                            SystemMsg = val;
                            SystemMsgParsed = val.Replace("\\s", " ").Replace("\\n", "");
                            break;
                        case "tmi-sent-ts":
                            TmiSentTs = val;
                            break;
                        case "turbo":
                            Turbo = (val == "1");
                            break;
                        case "user-id":
                            UserId = val;
                            break;
                        case "user-type":
                            UserType = val;
                            break;
                    }
                }
            }
        }
    }
}
