namespace TwitchLib.EventSub.Websockets.Core.EventArgs
{
    public abstract class TwitchLibEventSubEventArgs<T> : System.EventArgs where T: new()
    {
        public T Notification { get; set; } = new();
    }
}