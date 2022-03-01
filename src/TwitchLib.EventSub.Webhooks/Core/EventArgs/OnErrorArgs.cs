namespace TwitchLib.EventSub.Webhooks.Core.EventArgs
{
    public class OnErrorArgs : System.EventArgs
    {
        public string Reason { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}