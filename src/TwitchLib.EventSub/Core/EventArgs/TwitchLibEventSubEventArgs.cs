using TwitchLib.EventSub.Core.Handler;

namespace TwitchLib.EventSub.Core.EventArgs
{
    public abstract class TwitchLibEventSubEventArgs<T> : System.EventArgs where T: new()
    {
        public T Notification { get; set; } = new();
    }
}