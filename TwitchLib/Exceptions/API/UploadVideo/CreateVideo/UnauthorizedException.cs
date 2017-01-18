using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API.UploadVideo.CreateVideo
{
    /// <summary>Exception thrown when the passed access token doesn't have the correct scope.</summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>Exception constructor</summary>
        public UnauthorizedException(string apiData)
            : base(apiData)
        {
        }
    }
}
