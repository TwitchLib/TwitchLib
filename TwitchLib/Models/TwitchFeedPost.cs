using System.Collections.Generic;
using System.Linq;
using Meebey.SmartIrc4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchFeedPost
    {
        // TODO note:
        // - Implement Emote property, doesn't seem to be supported by Twitch at time of writing.

        public long Id { get; }
        public string CreatedAt { get; }
        public bool IsDeleted { get; }
        public string Body { get; }
        public TwitchUser User { get; }
        public List<Reaction> Reactions { get; }

        public TwitchFeedPost(JToken twitchStreamData)
        {
            long id;
            bool isDeleted;

            if (long.TryParse(twitchStreamData.SelectToken("id").ToString(), out id)) Id = id;
            if (bool.TryParse(twitchStreamData.SelectToken("deleted").ToString(), out isDeleted) && isDeleted)
                IsDeleted = true;

            Body = twitchStreamData.SelectToken("body").ToString();
            CreatedAt = twitchStreamData.SelectToken("created_at").ToString();

            User = new TwitchUser((JObject) twitchStreamData.SelectToken("user"));

            var list = new List<Reaction>();
            foreach (var item in (JObject) twitchStreamData.SelectToken("reactions"))
            {
                list.Add(new Reaction(item.Value, item.Key));
            }

            Reactions = list;
        }

        public class Reaction
        {
            public string Id { get; }
            public string Emote { get; }
            public int Count { get; }
            public List<long> UserIds { get; set; }

            public Reaction(JToken twitchStreamData, string reactionId)
            {
                int count;

                if (int.TryParse(twitchStreamData.SelectToken("count").ToString(), out count)) Count = count;

                Id = reactionId;
                Emote = twitchStreamData.SelectToken("emote").ToString();
                UserIds = JsonConvert.DeserializeObject<List<long>>(twitchStreamData.SelectToken("user_ids").ToString());
            }
        }
    }
}