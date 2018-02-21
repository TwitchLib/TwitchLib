using System;

namespace TwitchLib.Client.Exceptions.Services
{
    /// <inheritdoc />
    /// <summary>Exception representing no channel data set.</summary>
    public class UninitializedChannelDataException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor.</summary>
        public UninitializedChannelDataException(string data)
            : base(data)
        {
        }
    }
}
