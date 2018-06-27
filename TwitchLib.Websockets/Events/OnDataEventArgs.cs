using System;

namespace TwitchLib.Websockets.Events
{
    public class OnDataEventArgs : EventArgs
    {
        public byte[] Data;
    }
}
