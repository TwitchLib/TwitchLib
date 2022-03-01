namespace TwitchLib.Client.Models
{
    // TODO: Strange class
    // TODO: Missing builder

    /// <summary>Class representing a joined channel.</summary>
    public class JoinedChannel
    {
        /// <summary>The current channel the TwitcChatClient is connected to.</summary>
        public string Channel { get; }

        /// <summary>Object representing current state of channel (r9k, slow, etc).</summary>
        public ChannelState ChannelState { get; protected set; }

        /// <summary>The most recent message received.</summary>
        public ChatMessage PreviousMessage { get; protected set; }

        /// <summary>JoinedChannel object constructor.</summary>
        public JoinedChannel(string channel)
        {
            Channel = channel;
        }

        /// <summary>Handles a message</summary>
        public void HandleMessage(ChatMessage message)
        {
            PreviousMessage = message;
        }
    }
}
