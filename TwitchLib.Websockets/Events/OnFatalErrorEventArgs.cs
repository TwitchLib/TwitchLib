using System;

namespace TwitchLib.Websockets.Events
{
    public class OnFatalErrorEventArgs : EventArgs
    {
        public string Reason;
    }
}
