using System;

namespace TwitchLib.Communication.Events
{
    public class OnSendFailedEventArgs : EventArgs
    {
        public string Data;
        public Exception Exception;
    }
}
