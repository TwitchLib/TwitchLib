using System;
using System.Net.WebSockets;

namespace TwitchLib.Websockets.Events
{
    public class OnDisconnectedEventArgs : EventArgs
    {
        public WebSocketCloseStatus Reason;
    }
}
