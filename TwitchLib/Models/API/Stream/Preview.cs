using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Stream
{
    /// <summary>Class representing the various sizes of previews.</summary>
    public class Preview
    {
        /// <summary>Property representing the small preview size.</summary>
        public string Small { get; protected set; }
        /// <summary>Property representing the medium preview size.</summary>
        public string Medium { get; protected set; }
        /// <summary>Property representing the large preview size.</summary>
        public string Large { get; protected set; }
        /// <summary>Property representing the template preview size.</summary>
        public string Template { get; protected set; }

        /// <summary>PreviewObj object constructor.</summary>
        public Preview(JToken previewData)
        {
            Small = previewData.SelectToken("small").ToString();
            Medium = previewData.SelectToken("medium").ToString();
            Large = previewData.SelectToken("large").ToString();
            Template = previewData.SelectToken("template").ToString();
        }
    }
}
