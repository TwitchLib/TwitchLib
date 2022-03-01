using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <inheritdoc />
    /// <summary>
    /// Class SubMessage.
    /// Implements the <see cref="MessageData" />
    /// <seealso cref="MessageData" />
    /// </summary>
    public class SubMessage : MessageData
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; }
        /// <summary>
        /// Gets or sets the emotes.
        /// </summary>
        /// <value>The emotes.</value>
        public List<Emote> Emotes { get; } = new List<Emote>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SubMessage"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public SubMessage(JToken json)
        {
            Message = json.SelectToken("message")?.ToString();
            foreach (var token in json.SelectToken("emotes"))
                Emotes.Add(new Emote(token));
        }

        /// <summary>
        /// Class Emote.
        /// </summary>
        public class Emote
        {
            /// <summary>
            /// Gets or sets the start.
            /// </summary>
            /// <value>The start.</value>
            public int Start { get; }
            /// <summary>
            /// Gets or sets the end.
            /// </summary>
            /// <value>The end.</value>
            public int End { get; }
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public string Id { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Emote"/> class.
            /// </summary>
            /// <param name="json">The json.</param>
            public Emote(JToken json)
            {
                Start = int.Parse(json.SelectToken("start").ToString());
                End = int.Parse(json.SelectToken("end").ToString());
                Id = json.SelectToken("id").ToString();
            }
        }
    }
}
