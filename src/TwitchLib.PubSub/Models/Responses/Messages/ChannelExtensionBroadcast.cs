using System.Collections.Generic;
using System.Text.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// VideoPlayback model constructor.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class ChannelExtensionBroadcast : MessageData
    {
        /// <summary>
        /// Video playback type
        /// </summary>
        /// <value>The messages.</value>
        public List<string> Messages { get; } = new List<string>();

        /// <summary>
        /// VideoPlayback constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public ChannelExtensionBroadcast(string jsonStr)
        {
            var json = JsonDocument.Parse(jsonStr);
            foreach (var msg in json.RootElement.GetProperty("content").EnumerateArray())
                Messages.Add(msg.GetString());
        }
    }
}
