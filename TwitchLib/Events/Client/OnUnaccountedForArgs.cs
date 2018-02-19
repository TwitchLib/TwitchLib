using System;
namespace TwitchLib.Events.Client
{
    public class OnUnaccountedForArgs : EventArgs
    {
        public string RawIRC { get; set; }
        public string Location { get; set; }
        public string BotUsername { get; set; } // may not be available
        public string Channel { get; set; } // may not be available
    }
}
