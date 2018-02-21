using System;

namespace TwitchLib.Client.Exceptions.Services
{
    /// <inheritdoc />
    /// <summary>Exception representing no channel list being provided.</summary>
    public class UnintializedChannelListException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor.</summary>
        public UnintializedChannelListException(string data)
            : base(data)
        {
        }
    }
}
