using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API
{
    public class ClipsResponse
    {
        public string Cursor { get; protected set; }
        public List<Clip> Clips { get; protected set; }

        public ClipsResponse(JToken json)
        {
            Cursor = json.SelectToken("_cursor")?.ToString();
            if(json.SelectToken("clips") != null)
            {
                Clips = new List<Clip>();
                foreach (JToken token in json.SelectToken("clips"))
                    Clips.Add(new Clip(token));
            }
        }
    }
}
