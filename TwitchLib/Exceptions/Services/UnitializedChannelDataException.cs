using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.Services
{
    /// <summary>Exception representing no channel data set.</summary>
    public class UninitializedChannelDataException : Exception
    {
        /// <summary>Exception constructor.</summary>
        public UninitializedChannelDataException(string data)
            : base(data)
        {
        }
    }
}
