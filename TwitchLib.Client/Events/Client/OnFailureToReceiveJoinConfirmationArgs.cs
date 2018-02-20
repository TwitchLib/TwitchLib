using System;
using TwitchLib.Client.Exceptions.Client;

namespace TwitchLib.Client.Events.Client
{
    public class OnFailureToReceiveJoinConfirmationArgs : EventArgs
    {
        public FailureToReceiveJoinConfirmationException Exception;
    }
}
