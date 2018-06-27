using System;
using System.Net.WebSockets;

namespace TwitchLib.WebSocket.Events
{
    public class OnStateChangedEventArgs : EventArgs
    {
        public WebSocketState NewState;
        public WebSocketState PreviousState;
    }
}
