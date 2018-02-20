using System;

namespace TwitchLib.Client.Models.Client
{
    public class RaidNotification
    {
        public string Badges { get; protected set; }
        public string Color { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Emotes { get; protected set; }
        public string Id { get; protected set; }
        public string Login { get; protected set; }
        public bool Moderator { get; protected set; }
        public string MsgId { get; protected set; }
        public string MsgParamDisplayName { get; protected set; }
        public string MsgParamLogin { get; protected set; }
        public string MsgParamViewerCount { get; protected set; }
        public string RoomId { get; protected set; }
        public bool Subscriber { get; protected set; }
        public string SystemMsg { get; protected set; }
        public string SystemMsgParsed { get; protected set; }
        public string TmiSentTs { get; protected set; }
        public bool Turbo { get; protected set; }
        public string UserId { get; protected set; }
        public string UserType { get; protected set; }

        // @badges=;color=#FF0000;display-name=Heinki;emotes=;id=4fb7ab2d-aa2c-4886-a286-46e20443f3d6;login=heinki;mod=0;msg-id=raid;msg-param-displayName=Heinki;msg-param-login=heinki;msg-param-viewerCount=4;room-id=27229958;subscriber=0;system-msg=4\sraiders\sfrom\sHeinki\shave\sjoined\n!;tmi-sent-ts=1510249711023;turbo=0;user-id=44110799;user-type= :tmi.twitch.tv USERNOTICE #pandablack
        public RaidNotification(string ircMessage)
        {
            if (ircMessage[0] == '@')
                ircMessage = ircMessage.Substring(0, ircMessage.Length - 1);
            var parts = ircMessage.Split(new string[] { ":tmi.twitch.tv" }, StringSplitOptions.None)[0].Split(';');
            foreach(var part in parts)
            {
                if(part.Contains("="))
                {
                    var key = part.Split('=')[0];
                    var val = part.Split('=')[1];
                    switch(key)
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
                        case "login":
                            Login = val;
                            break;
                        case "mod":
                            Moderator = (val == "1");
                            break;
                        case "msg-id":
                            MsgId = val;
                            break;
                        case "msg-param-displayName":
                            MsgParamDisplayName = val;
                            break;
                        case "msg-param-login":
                            MsgParamLogin = val;
                            break;
                        case "msg-param-viewerCount":
                            MsgParamViewerCount = val;
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
