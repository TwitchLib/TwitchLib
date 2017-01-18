using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API.UploadVideo.CreateVideo
{
    /// <summary>Exception thrown when attempting to upload to an invalid channel.</summary>
    public class InvalidChannelException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidChannelException(string apiData)
            : base(apiData)
        {
        }
    }
}
