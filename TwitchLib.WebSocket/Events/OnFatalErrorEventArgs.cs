using System;

namespace TwitchLib.WebSocket.Events
{
    public class OnFatalErrorEventArgs : EventArgs
    {
        public string Reason;
    }
}
