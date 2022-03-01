using System.Net.WebSockets;

namespace TwitchLib.EventSub.Websockets.Core.EventArgs;

public class WebsocketDisconnectedArgs : System.EventArgs
{
    public WebSocketCloseStatus CloseStatus { get; set; }
    public string? CloseStatusDescription { get; set; }
}