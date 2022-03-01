namespace TwitchLib.Client.Models
{
    public class OutgoingMessage
    {
        public string Channel { get; set; }

        public string Message { get; set; }

        public int Nonce { get; set; }

        public string Sender { get; set; }

        public MessageState State { get; set; }
    }

    public enum MessageState : byte
    {
        /// <summary> Message did not originate from this session, or was successfully sent. </summary>
		Normal = 0,

        /// <summary> Message is current queued. </summary>
		Queued,

        /// <summary> Message failed to be sent. </summary>
		Failed
    }
}

