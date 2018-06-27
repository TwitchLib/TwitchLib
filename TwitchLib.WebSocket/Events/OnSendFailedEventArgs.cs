using System;

namespace TwitchLib.WebSocket.Events
{
    public class OnSendFailedEventArgs : EventArgs
    {
        public string Data;
        public Exception Exception;
    }
}
