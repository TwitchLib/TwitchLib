using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API.UploadVideo
{
    /// <summary>Exception thrown when the video Id provided is invalid.</summary>
    public class InvalidVideoIdException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidVideoIdException(string apiData)
            : base(apiData)
        {
        }
    }
}
