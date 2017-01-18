using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video.UploadVideo
{
    /// <summary>Model representing the response from Twitch after making request to create video.</summary>
    public class CreateVideoResponse
    {
        /// <summary>Upload details including URL of the upload and the token.</summary>
        public Upload Upload { get; protected set; }
        /// <summary>All details regarding the created video.</summary>
        public Video Video { get; protected set; }

        /// <summary>CreateVideoResponse constructor.</summary>
        /// <param name="json"></param>
        public CreateVideoResponse(JToken json)
        {
            if (json.SelectToken("upload") != null)
                Upload = new Upload(json.SelectToken("upload"));
            if (json.SelectToken("video") != null)
                Video = new Video(json.SelectToken("video"));
        }
    }
}
