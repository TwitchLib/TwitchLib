using System.Collections.Generic;

namespace TwitchLib.EventSub.Webhooks.Core.EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TwitchLibEventSubEventArgs<T> : System.EventArgs where T: new()
    {
        /// <summary>
        /// 
        /// </summary>
        public T Notification { get; set; } = new();
    }
}