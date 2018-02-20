using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.API.v5.Clips
{
    public class TopClipsResponse
    {
        public string Cursor { get; protected set; }
        public List<Clip> Clips { get; protected set; } = new List<Clip>();

        public TopClipsResponse(JToken json)
        {

        }
    }
}
