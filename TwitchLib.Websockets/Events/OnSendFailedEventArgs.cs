using System;

namespace TwitchLib.Websockets.Events
{
    public class OnSendFailedEventArgs : EventArgs
    {
        public string Data;
        public Exception Exception;
    }
}
