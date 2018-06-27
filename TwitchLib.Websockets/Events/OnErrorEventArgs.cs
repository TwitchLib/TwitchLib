using System;

namespace TwitchLib.Websockets.Events
{
    public class OnErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
