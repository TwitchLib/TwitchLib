using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    public class SubMessage
    {
        public string Message { get; protected set; }
        public List<Emote> Emotes { get; protected set; } = new List<Emote>();

        public SubMessage(JToken json)
        {
            Message = json.SelectToken("message")?.ToString();
            foreach (var token in json.SelectToken("emotes"))
                Emotes.Add(new Emote(token));
        }

        public class Emote
        {
            public int Start { get; protected set; }
            public int End { get; protected set; }
            public int Id { get; protected set; }

            public Emote(JToken json)
            {
                Start = int.Parse(json.SelectToken("start").ToString());
                End = int.Parse(json.SelectToken("end").ToString());
                Id = int.Parse(json.SelectToken("id").ToString());
            }
        }
    }
}
