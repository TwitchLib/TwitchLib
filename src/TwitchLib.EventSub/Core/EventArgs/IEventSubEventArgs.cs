using TwitchLib.EventSub.Core.Models;

namespace TwitchLib.EventSub.Core.EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventSubEventArgs<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        EventSubNotification<T> Notification { get; set; }
    }
}
