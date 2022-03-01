namespace TwitchLib.EventSub.Core.EventArgs;

internal class DataReceivedArgs : System.EventArgs
{
    public string Message { get; internal set; } = string.Empty;
}