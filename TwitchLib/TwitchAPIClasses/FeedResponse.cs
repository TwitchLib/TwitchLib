using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class FeedResponse
    {
        public int Total { get; protected set; }
        public string Cursor { get; protected set; }
        public string Topic { get; protected set; }
        public List<Post> Posts { get; protected set; }

        public FeedResponse(JToken json)
        {
            if (json.SelectToken("_total") != null)
                Total = int.Parse(json.SelectToken("_total").ToString());
            Cursor = json.SelectToken("_cursor")?.ToString();
            Topic = json.SelectToken("_topic")?.ToString();
            Posts = new List<Post>();
            if(json.SelectToken("posts") != null)
                foreach (JToken post in json.SelectToken("posts"))
                    Posts.Add(new Post(post));
        }

        public class Post
        {
            public string Id { get; protected set; }
            public string CreatedAt { get; protected set; }
            public bool Deleted { get; protected set; }
            public string Body { get; protected set; }
            public List<Emote> Emotes { get; protected set; }
            public List<Reaction> Reactions { get; protected set; }
            public User User { get; protected set; }
            public int CommentsTotal { get; protected set; }
            public string CommentsCursor { get; protected set; }
            public List<Comment> Comments { get; protected set; }
            public List<KeyValuePair<string, bool>> Permissions { get; protected set; }

            public Post(JToken json)
            {
                Id = json.SelectToken("id")?.ToString();
                CreatedAt = json.SelectToken("created_at")?.ToString();
                if (json.SelectToken("deleted") != null)
                    Deleted = json.SelectToken("deleted").ToString().ToLower() == "true";
                Body = json.SelectToken("body")?.ToString();
                Emotes = new List<Emote>();
                if (json.SelectToken("emotes") != null)
                    foreach (JToken emote in json.SelectToken("emotes"))
                        Emotes.Add(new Emote(emote));
                Reactions = new List<Reaction>();
                if (json.SelectToken("reactions") != null)
                    foreach (JToken reaction in json.SelectToken("reactions"))
                        Reactions.Add(new Reaction(reaction));
                if (json.SelectToken("user") != null)
                    User = new User(json.SelectToken("user").ToString());
                Comments = new List<Comment>();
                var comments = json.SelectToken("comments");
                if (comments.SelectToken("_total") != null)
                    CommentsTotal = int.Parse(comments.SelectToken("_total").ToString());
                CommentsCursor = comments.SelectToken("_cursor")?.ToString();
                if (comments.SelectToken("comments") != null)
                    foreach (JToken comment in comments.SelectToken("comments"))
                        Comments.Add(new Comment(comment));
                Permissions = new List<KeyValuePair<string, bool>>();
                if(json.SelectToken("permissions") != null)
                {
                    JToken permissions = json.SelectToken("permissions");
                    if (permissions.SelectToken("can_reply") != null)
                        Permissions.Add(new KeyValuePair<string, bool>("can_reply", permissions.SelectToken("can_reply").ToString() == "true"));
                    if (permissions.SelectToken("can_moderate") != null)
                        Permissions.Add(new KeyValuePair<string, bool>("can_moderate", permissions.SelectToken("can_moderate").ToString() == "true"));
                    if (permissions.SelectToken("can_delete") != null)
                        Permissions.Add(new KeyValuePair<string, bool>("can_delete", permissions.SelectToken("can_delete").ToString() == "true"));
                }
            }

            public class Emote
            {
                public int Id { get; protected set; }
                public int Start { get; protected set; }
                public int End { get; protected set; }
                public int Set { get; protected set; }

                public Emote(JToken json)
                {
                    if (json.SelectToken("id") != null)
                        Id = int.Parse(json.SelectToken("id").ToString());
                    if (json.SelectToken("start") != null)
                        Start = int.Parse(json.SelectToken("start").ToString());
                    if (json.SelectToken("end") != null)
                        End = int.Parse(json.SelectToken("end").ToString());
                    if (json.SelectToken("set") != null)
                        Set = int.Parse(json.SelectToken("set").ToString());
                }
            }

            public class Reaction
            {
                public string Emote { get; protected set; }
                public int Count { get; protected set; }

                public Reaction (JToken json)
                {
                    json = json.First;
                    Emote = json.SelectToken("emote")?.ToString();
                    if (json.SelectToken("count") != null)
                        Count = int.Parse(json.SelectToken("count").ToString());
                }
            }

            public class Comment
            {
                public int Id { get; protected set; }
                public string CreatedAt { get; protected set; }
                public bool Deleted { get; protected set; }
                public string Body { get; protected set; }
                public List<KeyValuePair<string, bool>> Permissions { get; protected set; }
                public List<Emote> Emotes { get; protected set; }
                public List<Reaction> Reactions { get; protected set; }
                public User User { get; protected set; }
                public Comment(JToken json)
                {
                    if (json.SelectToken("id") != null)
                        Id = int.Parse(json.SelectToken("id").ToString());
                    CreatedAt = json.SelectToken("created_at")?.ToString();
                    if (json.SelectToken("deleted") != null)
                        Deleted = json.SelectToken("deleted").ToString().ToLower() == "true";
                    Body = json.SelectToken("body")?.ToString();
                    Permissions = new List<KeyValuePair<string, bool>>();
                    if(json.SelectToken("permissions") != null)
                    {
                        JToken permissions = json.SelectToken("permissions");
                        if (permissions.SelectToken("can_delete") != null)
                            Permissions.Add(new KeyValuePair<string, bool>("can_delete", permissions.SelectToken("can_delete").ToString().ToLower() == "true"));
                    }
                    Emotes = new List<Emote>();
                    if (json.SelectToken("emotes") != null)
                        foreach (JToken emote in json.SelectToken("emotes"))
                            Emotes.Add(new Emote(emote));
                    Reactions = new List<Reaction>();
                    if (json.SelectToken("reactions") != null)
                        foreach (JToken reaction in json.SelectToken("reactions"))
                            Reactions.Add(new Reaction(reaction));
                    if (json.SelectToken("user") != null)
                        User = new User(json.SelectToken("user").ToString());
                }
            }
        }
    }
}
