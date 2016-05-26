namespace TwitchLib
{
    public class WhisperMessage
    {
        public enum UType
        {
            Viewer,
            GlobalModerator,
            Admin,
            Staff
        }

        public string Badges { get; }

        public string ColorHex { get; }

        public string Username { get; }

        public string DisplayName { get; }

        public string EmoteSet { get; }

        public string ThreadId { get; }

        public int MessageId { get; }

        public int UserId { get; }

        public bool IsTurbo { get; }

        public string BotUsername { get; }

        public string Message { get; }

        public WhisperMessage(string ircString, string botUsername)
        {
            if (ircString.Split(';').Length == 9)
            {
                //@badges=;color=#00FF7F;display-name=Dara226;emotes=;message-id=53;thread-id=62192703_66137196;turbo=0;user-id=62192703;user-type= :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
                Badges = ircString.Split(';')[0].Split('=')[1];
                ColorHex = ircString.Split(';')[1].Split('=')[1];
                Username = ircString.Split('@')[2].Split('.')[0];
                DisplayName = ircString.Split(';')[2].Split('=')[1];
                EmoteSet = ircString.Split(';')[3].Split('=')[1];
                MessageId = int.Parse(ircString.Split(';')[4].Split('=')[1]);
                ThreadId = ircString.Split(';')[5].Split('=')[1];
                IsTurbo = ConvertToBool(ircString.Split(';')[6].Split('=')[1]);
                UserId = int.Parse(ircString.Split(';')[7].Split('=')[1]);
                var userTypeStr = ircString.Split(';')[8].Split('=')[1].Replace(" ", "");
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
                IsTurbo = ConvertToBool(ircString.Split(';')[5].Split('=')[1]);
                UserId = int.Parse(ircString.Split(';')[6].Split('=')[1]);
                var userTypeStr = ircString.Split(';')[7].Split('=')[1].Replace(" ", "");
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