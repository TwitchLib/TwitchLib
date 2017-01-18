using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API.UploadVideo
{
    /// <summary>Exception thrown when the identifying video token is invalid.</summary>
    public class InvalidUploadTokenException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidUploadTokenException(string apiData)
            : base(apiData)
        {
        }
    }
}
