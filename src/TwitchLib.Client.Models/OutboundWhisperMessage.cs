namespace TwitchLib.Client.Models
{
    public class OutboundWhisperMessage
    {
        public string Username { get; set; }

        public string Receiver { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $":{Username}~{Username}@{Username}.tmi.twitch.tv PRIVMSG #jtv :/w {Receiver} {Message}";
        }
    }
}
