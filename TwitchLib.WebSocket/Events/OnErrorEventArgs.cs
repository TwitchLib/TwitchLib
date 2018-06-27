using System;

namespace TwitchLib.WebSocket.Events
{
    public class OnErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
