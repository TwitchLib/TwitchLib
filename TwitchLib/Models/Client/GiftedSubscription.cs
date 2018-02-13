using System;

namespace TwitchLib.Models.Client
{
    public class GiftedSubscription
    {
        public string Badges { get; protected set; }
        public string Color { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Emotes { get; protected set; }
        public string Id { get; protected set; }
        public string Login { get; protected set; }
        public bool Moderator { get; protected set; }
        public string MsgId { get; protected set; }
        public string MsgParamMonths { get; protected set; }
        public string MsgParamRecipientDisplayName { get; protected set; }
        public string MsgParamRecipientId { get; protected set; }
        public string MsgParamRecipientUserName { get; protected set; }
        public string MsgParamSubPlanName { get; protected set; }
        public string MsgParamSubPlan { get; protected set; }
        public string RoomId { get; protected set; }
        public bool Subscriber { get; protected set; }
        public string SystemMsg { get; protected set; }
        public string SystemMsgParsed { get; protected set; }
        public string TmiSentTs { get; protected set; }
        public bool Turbo { get; protected set; }
        public string UserType { get; protected set; }

        public GiftedSubscription(string ircMessage)
        {
            if (ircMessage[0] == '@')
                ircMessage = ircMessage.Substring(0, ircMessage.Length - 1);
            var parts = ircMessage.Split(new [] { ":tmi.twitch.tv" }, StringSplitOptions.None)[0].Split(';');
            foreach (var part in parts)
            {
                if (!part.Contains("=")) continue;

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
                    case "msg-param-months":
                        MsgParamMonths = val;
                        break;
                    case "msg-param-recipient-display-name":
                        MsgParamRecipientDisplayName = val;
                        break;
                    case "msg-param-recipient-id":
                        MsgParamRecipientId = val;
                        break;
                    case "msg-param-recipient-user-name":
                        MsgParamRecipientUserName = val;
                        break;
                    case "msg-param-sub-plan-name":
                        MsgParamSubPlanName = val;
                        break;
                    case "msg-param-sub-plan":
                        MsgParamSubPlan = val;
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
                    case "user-type":
                        UserType = val;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(key));
                }
            }
        }
    }
}
