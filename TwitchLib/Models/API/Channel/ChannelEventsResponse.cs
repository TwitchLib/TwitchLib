using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.API.Channel
{
    public class ChannelEventsResponse
    {
        public int Total { get; protected set; }
        public List<Event> Events { get; protected set; } = new List<Event>();

        public ChannelEventsResponse(JToken json)
        {
            Total = int.Parse(json.SelectToken("_total").ToString());
            foreach (var eventData in json.SelectToken("events"))
                Events.Add(new Event(eventData));
        }
    }
}
