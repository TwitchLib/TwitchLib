using System;

namespace TwitchLib.WebSocket.Events
{
    public class OnDataEventArgs : EventArgs
    {
        public byte[] Data;
    }
}
