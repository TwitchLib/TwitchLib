using System;
using TwitchLib.PubSub.Models.Responses.Messages;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// [INCOMPLETE/NOT_FULLY_SUPPORTED]Whisper arguement class.
    /// </summary>
    public class OnWhisperArgs : EventArgs
    {
        /// <summary>
        /// Property representing the whisper object.
        /// </summary>
        public Whisper Whisper;

        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
