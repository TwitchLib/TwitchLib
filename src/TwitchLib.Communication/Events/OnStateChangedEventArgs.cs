using System;

namespace TwitchLib.Communication.Events
{
    public class OnStateChangedEventArgs : EventArgs
    {
        public bool IsConnected;
        public bool WasConnected;
    }
}
