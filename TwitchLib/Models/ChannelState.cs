namespace TwitchLib
{
    public class ChannelState
    {
        public bool IsR9K { get; }

        public bool IsSubOnly { get; }

        public bool IsSlowMode { get; }

        public string BroadcasterLanguage { get; } = "";

        public string Channel { get; }

        public ChannelState(string ircString)
        {
            //@broadcaster-lang=;r9k=0;slow=0;subs-only=0 :tmi.twitch.tv ROOMSTATE #swiftyspiffy
            if (ircString.Split(';').Length <= 3) return;
            if (ircString.Split(';')[0].Split('=').Length > 1)
                BroadcasterLanguage = ircString.Split(';')[0].Split('=')[1];
            if (ircString.Split(';')[1].Split('=').Length > 1)
                IsR9K = ConvertToBool(ircString.Split(';')[1].Split('=')[1]);
            if (ircString.Split(';')[2].Split('=').Length > 1)
                IsSlowMode = ConvertToBool(ircString.Split(';')[2].Split('=')[1]);
            if (ircString.Split(';')[3].Split('=').Length > 1)
                IsSubOnly = ConvertToBool(ircString.Split(';')[3].Split('=')[1]);
            Channel = ircString.Split('#')[1];
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}