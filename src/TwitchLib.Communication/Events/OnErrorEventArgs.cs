using System;

namespace TwitchLib.Communication.Events
{
    public class OnErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
