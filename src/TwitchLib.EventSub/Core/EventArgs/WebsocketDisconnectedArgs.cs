using System.Net.WebSockets;

namespace TwitchLib.EventSub.Core.EventArgs;

public class WebsocketDisconnectedArgs : System.EventArgs
{
    public WebSocketCloseStatus CloseStatus { get; set; }
    public string? CloseStatusDescription { get; set; }
}