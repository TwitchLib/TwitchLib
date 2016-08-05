using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{

    public class WhisperMessage
    {
        public string Badges { get; protected set; }
        public string ColorHex { get; protected set; }
        public string Username { get; protected set; }
        public string DisplayName { get; protected set; }
        public string EmoteSet { get; protected set; }
        public string ThreadId { get; protected set; }
        public int MessageId { get; protected set; }
        public int UserId { get; protected set; }
        public bool Turbo { get; protected set; }
        public string BotUsername { get; protected set; }
        public string Message { get; protected set; }
        public string UserType { get; protected set; }

        public WhisperMessage(string ircString, string botUsername)
        {
            if (ircString.Substring(0, 8) == "@badges=")
            {
                //@badges=;color=#00FF7F;display-name=Dara226;emotes=;message-id=53;thread-id=62192703_66137196;turbo=0;user-id=62192703;user-type= :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
                Badges = ircString.Split(';')[0].Split('=')[1];
                ColorHex = ircString.Split(';')[1].Split('=')[1];
                Username = ircString.Split('@')[2].Split('.')[0];
                DisplayName = ircString.Split(';')[2].Split('=')[1];
                EmoteSet = ircString.Split(';')[3].Split('=')[1];
                MessageId = int.Parse(ircString.Split(';')[4].Split('=')[1]);
                ThreadId = ircString.Split(';')[5].Split('=')[1];
                Turbo = ConvertToBool(ircString.Split(';')[6].Split('=')[1]);
                UserId = int.Parse(ircString.Split(';')[7].Split('=')[1]);
                // UserType should be converted to an enum value I think at somepoint
                UserType = ircString.Split(';')[8].Split('=')[1].Replace(" ", "");
                Message =
                    ircString.Replace(
                        ircString.Split('@')[0] + "@" + ircString.Split('@')[1] + "@" +
                        ircString.Split('@')[2].Split(':')[0] + ":", string.Empty);
            }
            else
            {
                //@color=#00FF7F;display-name=Dara226;emotes=;message-id=53;thread-id=62192703_66137196;turbo=0;user-id=62192703;user-type= :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
                Badges = "";
                ColorHex = ircString.Split(';')[0].Split('=')[1];
                Username = ircString.Split('@')[2].Split('.')[0];
                DisplayName = ircString.Split(';')[1].Split('=')[1];
                EmoteSet = ircString.Split(';')[2].Split('=')[1];
                MessageId = int.Parse(ircString.Split(';')[3].Split('=')[1]);
                ThreadId = ircString.Split(';')[4].Split('=')[1];
                Turbo = ConvertToBool(ircString.Split(';')[5].Split('=')[1]);
                UserId = int.Parse(ircString.Split(';')[6].Split('=')[1]);
                // UserType should be converted to an enum value I think at somepoint
                UserType = ircString.Split(';')[7].Split('=')[1].Replace(" ", "");
                Message =
                    ircString.Replace(
                        ircString.Split('@')[0] + "@" + ircString.Split('@')[1] + "@" +
                        ircString.Split('@')[2].Split(':')[0] + ":", string.Empty);
            }
            BotUsername = botUsername;
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}