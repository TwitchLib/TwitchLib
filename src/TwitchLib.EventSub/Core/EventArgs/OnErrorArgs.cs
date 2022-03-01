namespace TwitchLib.EventSub.Core.EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    public class OnErrorArgs : System.EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; internal set; } = new NullReferenceException();
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; internal set; } = string.Empty;
    }
}