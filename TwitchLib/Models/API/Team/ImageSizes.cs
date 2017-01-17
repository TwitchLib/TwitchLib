using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Team
{
    /// <summary>Class representing the various sizes of images.</summary>
    public class ImageSizes
    {
        /// <summary>Property representing the 28 size url.</summary>
        public string Size28 { get; protected set; }
        /// <summary>Property representing the 50 size url.</summary>
        public string Size50 { get; protected set; }
        /// <summary>Property representing the 70 size url.</summary>
        public string Size70 { get; protected set; }
        /// <summary>Property representing the 150 size url.</summary>
        public string Size150 { get; protected set; }
        /// <summary>Property representing the 300 size url.</summary>
        public string Size300 { get; protected set; }
        /// <summary>Property representing the 600 size url.</summary>
        public string Size600 { get; protected set; }

        /// <summary>ImgSizes object constructor.</summary>
        public ImageSizes(JToken imageData)
        {
            Size28 = imageData.SelectToken("size28").ToString();
            Size50 = imageData.SelectToken("size50").ToString();
            Size70 = imageData.SelectToken("size70").ToString();
            Size150 = imageData.SelectToken("size150").ToString();
            Size300 = imageData.SelectToken("size300").ToString();
            Size600 = imageData.SelectToken("size600").ToString();
        }
    }
}
