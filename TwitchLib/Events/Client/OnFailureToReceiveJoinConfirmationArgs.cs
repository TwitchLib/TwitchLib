using System;
using TwitchLib.Exceptions.Client;

namespace TwitchLib.Events.Client
{
    public class OnFailureToReceiveJoinConfirmationArgs : EventArgs
    {
        public FailureToReceiveJoinConfirmationException Exception;
    }
}
