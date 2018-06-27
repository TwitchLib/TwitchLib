using System;
using System.Net.WebSockets;

namespace TwitchLib.WebSocket.Events
{
    public class OnDisconnectedEventArgs : EventArgs
    {
        public WebSocketCloseStatus Reason;
    }
}
