using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    public class Whisper : MessageData
    {
        public string Type { get; protected set; }
        public string Data { get; protected set; }
        public DataObj DataObject { get; protected set; }

        public Whisper(string jsonStr)
        {
            JObject json = JObject.Parse(jsonStr);
            Type = json.SelectToken("type").ToString();
            Data = json.SelectToken("data").ToString();
            DataObject = new DataObj(json.SelectToken("data_object"));
        }

        public class DataObj
        {
            public int Id { get; protected set; }
            public string ThreadId { get; protected set; }
            public string Body { get; protected set; }
            public long SentTs { get; protected set; }
            public long FromId { get; protected set; }
            public TagsObj Tags { get; protected set; }
            public RecipientObj Recipient { get; protected set; }
            public string Nonce { get; protected set; }

            public DataObj(JToken json)
            {
                Id = int.Parse(json.SelectToken("id").ToString());
                ThreadId = json.SelectToken("thread_id")?.ToString();
                Body = json.SelectToken("body")?.ToString();
                SentTs = long.Parse(json.SelectToken("sent_ts").ToString());
                FromId = long.Parse(json.SelectToken("from_id").ToString());
                Tags = new TagsObj(json.SelectToken("tags"));
                Recipient = new RecipientObj(json.SelectToken("recipient"));
                Nonce = json.SelectToken("nonce")?.ToString();
            }

            public class TagsObj
            {
                public string Login { get; protected set; }
                public string DisplayName { get; protected set; }
                public string Color { get; protected set; }
                public string UserType { get; protected set; }
                public bool Turbo { get; protected set; }
                public List<EmoteObj> Emotes = new List<EmoteObj>();
                public List<Badge> Badges = new List<Badge>();

                public TagsObj(JToken json)
                {
                    Login = json.SelectToken("login")?.ToString();
                    DisplayName = json.SelectToken("login")?.ToString();
                    Color = json.SelectToken("color")?.ToString();
                    UserType = json.SelectToken("user_type")?.ToString();
                    Turbo = bool.Parse(json.SelectToken("turbo").ToString());
                    foreach(JToken emote in json.SelectToken("emotes"))
                        Emotes.Add(new EmoteObj(emote));
                    foreach (JToken badge in json.SelectToken("badges"))
                        Badges.Add(new Badge(badge));
                }

                public class EmoteObj
                {
                    public int Id { get; protected set; }
                    public int Start { get; protected set; }
                    public int End { get; protected set; }

                    public EmoteObj(JToken json)
                    {
                        Id = int.Parse(json.SelectToken("id").ToString());
                        Start = int.Parse(json.SelectToken("start").ToString());
                        End = int.Parse(json.SelectToken("end").ToString());
                    }
                }
            }

            public class RecipientObj
            {
                public long Id { get; protected set; }
                public string Username { get; protected set; }
                public string DisplayName { get; protected set; }
                public string Color { get; protected set; }
                public string UserType { get; protected set; }
                public bool Turbo { get; protected set; }
                public List<Badge> Badges { get; protected set; }

                public RecipientObj(JToken json)
                {
                    Id = long.Parse(json.SelectToken("id").ToString());
                    Username = json.SelectToken("username")?.ToString();
                    DisplayName = json.SelectToken("display_name")?.ToString();
                    Color = json.SelectToken("color")?.ToString();
                    UserType = json.SelectToken("user_type")?.ToString();
                    Turbo = bool.Parse(json.SelectToken("turbo").ToString());
                    foreach (JToken badge in json.SelectToken("badges"))
                        Badges.Add(new Badge(badge));

                }
            }

            public class Badge
            {
                public string Id { get; protected set; }
                public string Version { get; protected set; }

                public Badge(JToken json)
                {
                    Id = json.SelectToken("id")?.ToString();
                    Version = json.SelectToken("version")?.ToString();
                }
            }
        }
    }
}
