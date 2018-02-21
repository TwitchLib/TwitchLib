namespace TwitchLib.Client.Exceptions.Client
{
    public class FailureToReceiveJoinConfirmationException
    {
        /// <summary>Exception representing failure of client to receive JOIN confirmation.</summary>
        public string Channel { get; protected set; }

        /// <summary>Exception construtor.</summary>
        public FailureToReceiveJoinConfirmationException(string channel)
        {
            Channel = channel;
        }
    }
}
