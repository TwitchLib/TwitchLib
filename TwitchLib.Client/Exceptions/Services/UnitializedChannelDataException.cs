using System;

namespace TwitchLib.Exceptions.Services
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
