using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.Services
{
    /// <summary>Exception representing no channel list being provided.</summary>
    public class UnintializedChannelList : Exception
    {
        /// <summary>Exception constructor.</summary>
        public UnintializedChannelList(string data)
            : base(data)
        {
        }
    }
}
