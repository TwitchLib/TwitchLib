using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Exceptions.Client;

namespace TwitchLib.Events.Client
{
    public class OnFailureToReceiveJoinConfirmationArgs : EventArgs
    {
        public FailureToReceiveJoinConfirmationException Exception;
    }
}
