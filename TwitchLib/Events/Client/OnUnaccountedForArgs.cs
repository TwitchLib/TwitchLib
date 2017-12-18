using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Events.Client
{
    public class OnUnaccountedForArgs
    {
        public string RawIRC { get; set; }
        public string Location { get; set; }
        public string BotUsername { get; set; } // may not be available
        public string Channel { get; set; } // may not be available
    }
}
