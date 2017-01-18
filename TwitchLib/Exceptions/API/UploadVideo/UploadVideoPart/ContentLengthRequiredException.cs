using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API.UploadVideo.UploadVideoPart
{
    /// <summary>Exception thrown when a content-length is missing from the upload request.</summary>
    public class ContentLengthRequiredException : Exception
    {
        /// <summary>Exception constructor</summary>
        public ContentLengthRequiredException(string apiData)
            : base(apiData)
        {
        }
    }
}
