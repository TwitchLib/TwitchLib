using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing whisper sent event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnWhisperSentArgs : EventArgs
    {
        /// <summary>
        /// Property representing username of bot.
        /// </summary>
        public string Username;
        /// <summary>
        /// Property representing receiver of the whisper.
        /// </summary>
        public string Receiver;
        /// <summary>
        /// Property representing sent message contents.
        /// </summary>
        public string Message;
    }
}
