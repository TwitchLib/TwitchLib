namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Class FailureToReceiveJoinConfirmationException.
    /// </summary>
    public class FailureToReceiveJoinConfirmationException
    {
        /// <summary>
        /// Exception representing failure of client to receive JOIN confirmation.
        /// </summary>
        /// <value>The channel.</value>
        public string Channel { get; protected set; }
        /// <summary>
        /// Extra details regarding this exception (not always set)
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; protected set; }

        /// <summary>
        /// Exception construtor.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="details">The details.</param>
        public FailureToReceiveJoinConfirmationException(string channel, string details = null)
        {
            Channel = channel;
            Details = details;
        }
    }
}
