using System;
using System.Net.WebSockets;

namespace TwitchLib.Websockets.Events
{
    public class OnStateChangedEventArgs : EventArgs
    {
        public WebSocketState NewState;
        public WebSocketState PreviousState;
    }
}
