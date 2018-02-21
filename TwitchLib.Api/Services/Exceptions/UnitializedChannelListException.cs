using System;

namespace TwitchLib.Api.Services.Exceptions
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
