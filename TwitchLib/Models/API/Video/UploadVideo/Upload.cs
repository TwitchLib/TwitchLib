using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video.UploadVideo
{
    /// <summary>Model containing data related to the upload of a video.</summary>
    public class Upload
    {
        /// <summary>URL of the uploaded video.</summary>
        public string URL { get; protected set; }
        /// <summary>Unique Twitch assigned token of uploaded video.</summary>
        public string Token { get; protected set; }

        /// <summary>Upload constructor.</summary>
        /// <param name="json"></param>
        public Upload(JToken json)
        {
            URL = json.SelectToken("url")?.ToString();
            Token = json.SelectToken("token")?.ToString();
        }
    }
}
