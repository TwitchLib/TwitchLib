using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// Class representing a whisper received via PubSub.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class Whisper : MessageData
    {
        /// <summary>
        /// Type of MessageData
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; }
        /// <summary>
        /// Enum of the Message type
        /// </summary>
        /// <value>The type enum.</value>
        public WhisperType TypeEnum { get; }
        /// <summary>
        /// Data identifier in MessageData
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; }
        /// <summary>
        /// Object that houses the data accompanying the type.
        /// </summary>
        /// <value>The data object whisper received.</value>
        public DataObjWhisperReceived DataObjectWhisperReceived { get; }
        /// <summary>
        /// Object that houses the data accompanying the type.
        /// </summary>
        /// <value>The data object thread.</value>
        public DataObjThread DataObjectThread { get; }

        /// <summary>
        /// Whisper object constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public Whisper(string jsonStr)
        {
            var json = JObject.Parse(jsonStr);
            Type = json.SelectToken("type").ToString();
            Data = json.SelectToken("data").ToString();
            switch (Type)
            {
                case "whisper_received":
                    TypeEnum = Enums.WhisperType.WhisperReceived;
                    DataObjectWhisperReceived = new DataObjWhisperReceived(json.SelectToken("data_object"));
                    break;
                case "thread":
                    TypeEnum = Enums.WhisperType.Thread;
                    DataObjectThread = new DataObjThread(json.SelectToken("data_object"));
                    break;
                default:
                    TypeEnum = Enums.WhisperType.Unknown;
                    break;
            }
        }

        /// <summary>
        /// Class DataObjThread.
        /// </summary>
        public class DataObjThread
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public string Id { get; }
            /// <summary>
            /// Gets or sets the last read.
            /// </summary>
            /// <value>The last read.</value>
            public long LastRead { get; }
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="DataObjThread"/> is archived.
            /// </summary>
            /// <value><c>true</c> if archived; otherwise, <c>false</c>.</value>
            public bool Archived { get; }
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="DataObjThread"/> is muted.
            /// </summary>
            /// <value><c>true</c> if muted; otherwise, <c>false</c>.</value>
            public bool Muted { get; }
            /// <summary>
            /// Gets or sets the spam information.
            /// </summary>
            /// <value>The spam information.</value>
            public SpamInfoObj SpamInfo { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DataObjThread"/> class.
            /// </summary>
            /// <param name="json">The json.</param>
            public DataObjThread(JToken json)
            {
                Id = json.SelectToken("id").ToString();
                LastRead = long.Parse(json.SelectToken("last_read").ToString());
                Archived = bool.Parse(json.SelectToken("archived").ToString());
                Muted = bool.Parse(json.SelectToken("muted").ToString());
                SpamInfo = new SpamInfoObj(json.SelectToken("spam_info"));
            }

            /// <summary>
            /// Class SpamInfoObj.
            /// </summary>
            public class SpamInfoObj
            {
                /// <summary>
                /// Gets or sets the likelihood.
                /// </summary>
                /// <value>The likelihood.</value>
                public string Likelihood { get; }
                /// <summary>
                /// Gets or sets the last marked not spam.
                /// </summary>
                /// <value>The last marked not spam.</value>
                public long LastMarkedNotSpam { get; }

                /// <summary>
                /// Initializes a new instance of the <see cref="SpamInfoObj"/> class.
                /// </summary>
                /// <param name="json">The json.</param>
                public SpamInfoObj(JToken json)
                {
                    Likelihood = json.SelectToken("likelihood").ToString();
                    LastMarkedNotSpam = long.Parse(json.SelectToken("last_marked_not_spam").ToString());
                }
            }

        }

        /// <summary>
        /// Class representing the data in the MessageData object.
        /// </summary>
        public class DataObjWhisperReceived
        {
            /// <summary>
            /// DataObject identifier
            /// </summary>
            /// <value>The identifier.</value>
            public string Id { get; protected set; }
            /// <summary>
            /// Twitch assigned thread id
            /// </summary>
            /// <value>The thread identifier.</value>
            public string ThreadId { get; protected set; }
            /// <summary>
            /// Body of data received from Twitch
            /// </summary>
            /// <value>The body.</value>
            public string Body { get; protected set; }
            /// <summary>
            /// Timestamp generated by Twitc
            /// </summary>
            /// <value>The sent ts.</value>
            public long SentTs { get; protected set; }
            /// <summary>
            /// Id of user that sent whisper.
            /// </summary>
            /// <value>From identifier.</value>
            public string FromId { get; protected set; }
            /// <summary>
            /// Tags object housing associated tags.
            /// </summary>
            /// <value>The tags.</value>
            public TagsObj Tags { get; protected set; }
            /// <summary>
            /// Receipient object housing various properties about user who received whisper.
            /// </summary>
            /// <value>The recipient.</value>
            public RecipientObj Recipient { get; protected set; }
            /// <summary>
            /// Uniquely generated string used to identify response from request.
            /// </summary>
            /// <value>The nonce.</value>
            public string Nonce { get; protected set; }

            /// <summary>
            /// DataObj constructor.
            /// </summary>
            /// <param name="json">The json.</param>
            public DataObjWhisperReceived(JToken json)
            {
                Id = json.SelectToken("id").ToString();
                ThreadId = json.SelectToken("thread_id")?.ToString();
                Body = json.SelectToken("body")?.ToString();
                SentTs = long.Parse(json.SelectToken("sent_ts").ToString());
                FromId = json.SelectToken("from_id").ToString();
                Tags = new TagsObj(json.SelectToken("tags"));
                Recipient = new RecipientObj(json.SelectToken("recipient"));
                Nonce = json.SelectToken("nonce")?.ToString();
            }

            /// <summary>
            /// Class representing the tags associated with the whisper.
            /// </summary>
            public class TagsObj
            {
                /// <summary>
                /// Login value associated.
                /// </summary>
                /// <value>The login.</value>
                public string Login { get; protected set; }
                /// <summary>
                /// Display name found in chat.
                /// </summary>
                /// <value>The display name.</value>
                public string DisplayName { get; protected set; }
                /// <summary>
                /// Color of whispers
                /// </summary>
                /// <value>The color.</value>
                public string Color { get; protected set; }
                /// <summary>
                /// User type of whisperer
                /// </summary>
                /// <value>The type of the user.</value>
                public string UserType { get; protected set; }
                /// <summary>
                /// List of emotes found in whisper
                /// </summary>
                public readonly List<EmoteObj> Emotes = new List<EmoteObj>();
                /// <summary>
                /// All badges associated with the whisperer
                /// </summary>
                public readonly List<Badge> Badges = new List<Badge>();

                /// <summary>
                /// Initializes a new instance of the <see cref="TagsObj"/> class.
                /// </summary>
                /// <param name="json">The json.</param>
                public TagsObj(JToken json)
                {
                    Login = json.SelectToken("login")?.ToString();
                    DisplayName = json.SelectToken("login")?.ToString();
                    Color = json.SelectToken("color")?.ToString();
                    UserType = json.SelectToken("user_type")?.ToString();
                    foreach (JToken emote in json.SelectToken("emotes"))
                        Emotes.Add(new EmoteObj(emote));
                    foreach (JToken badge in json.SelectToken("badges"))
                        Badges.Add(new Badge(badge));
                }

                /// <summary>
                /// Class representing a single emote found in a whisper
                /// </summary>
                public class EmoteObj
                {
                    /// <summary>
                    /// Emote ID
                    /// </summary>
                    /// <value>The identifier.</value>
                    public int Id { get; protected set; }
                    /// <summary>
                    /// Starting character of emote
                    /// </summary>
                    /// <value>The start.</value>
                    public int Start { get; protected set; }
                    /// <summary>
                    /// Ending character of emote
                    /// </summary>
                    /// <value>The end.</value>
                    public int End { get; protected set; }

                    /// <summary>
                    /// EmoteObj construcotr.
                    /// </summary>
                    /// <param name="json">The json.</param>
                    public EmoteObj(JToken json)
                    {
                        Id = int.Parse(json.SelectToken("id").ToString());
                        Start = int.Parse(json.SelectToken("start").ToString());
                        End = int.Parse(json.SelectToken("end").ToString());
                    }
                }
            }

            /// <summary>
            /// Class representing the recipient of the whisper.
            /// </summary>
            public class RecipientObj
            {
                /// <summary>
                /// Receiver id
                /// </summary>
                /// <value>The identifier.</value>
                public string Id { get; protected set; }
                /// <summary>
                /// Receiver username
                /// </summary>
                /// <value>The username.</value>
                public string Username { get; protected set; }
                /// <summary>
                /// Receiver display name.
                /// </summary>
                /// <value>The display name.</value>
                public string DisplayName { get; protected set; }
                /// <summary>
                /// Receiver color.
                /// </summary>
                /// <value>The color.</value>
                public string Color { get; protected set; }
                /// <summary>
                /// User type of receiver.
                /// </summary>
                /// <value>The type of the user.</value>
                public string UserType { get; protected set; }
                /// <summary>
                /// List of badges that the receiver has.
                /// </summary>
                /// <value>The badges.</value>
                public List<Badge> Badges { get; protected set; } = new List<Badge>();

                /// <summary>
                /// RecipientObj constructor.
                /// </summary>
                /// <param name="json">The json.</param>
                public RecipientObj(JToken json)
                {
                    Id = json.SelectToken("id").ToString();
                    Username = json.SelectToken("username")?.ToString();
                    DisplayName = json.SelectToken("display_name")?.ToString();
                    Color = json.SelectToken("color")?.ToString();
                    UserType = json.SelectToken("user_type")?.ToString();
                    foreach (JToken badge in json.SelectToken("badges"))
                        Badges.Add(new Badge(badge));
                }
            }

            /// <summary>
            /// Class representing a single badge.
            /// </summary>
            public class Badge
            {
                /// <summary>
                /// Id of the badge.
                /// </summary>
                /// <value>The identifier.</value>
                public string Id { get; protected set; }
                /// <summary>
                /// Version of the badge.
                /// </summary>
                /// <value>The version.</value>
                public string Version { get; protected set; }

                /// <summary>
                /// Initializes a new instance of the <see cref="Badge"/> class.
                /// </summary>
                /// <param name="json">The json.</param>
                public Badge(JToken json)
                {
                    Id = json.SelectToken("id")?.ToString();
                    Version = json.SelectToken("version")?.ToString();
                }
            }
        }
    }
}