using System;

namespace TwitchLib.EventSub.Websockets.Core.EventArgs;

public class ErrorOccuredArgs : System.EventArgs
{
    public Exception Exception { get; internal set; }
    public string Message { get; internal set; }
}