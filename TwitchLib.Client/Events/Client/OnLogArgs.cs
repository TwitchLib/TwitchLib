using System;

namespace TwitchLib.Client.Events.Client
{
    public class OnLogArgs : EventArgs
    {
        public string BotUsername;
        public string Data;
        public DateTime DateTime;
    }
}
