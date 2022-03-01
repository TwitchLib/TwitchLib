using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// VideoPlayback model constructor.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class VideoPlayback : MessageData
    {
        /// <summary>
        /// Video playback type
        /// </summary>
        /// <value>The type.</value>
        public VideoPlaybackType Type { get; }
        /// <summary>
        /// Server time stamp
        /// </summary>
        /// <value>The server time.</value>
        public string ServerTime { get; }
        /// <summary>
        /// Current delay (if one exists)
        /// </summary>
        /// <value>The play delay.</value>
        public int PlayDelay { get; }
        /// <summary>
        /// Viewer count
        /// </summary>
        /// <value>The viewers.</value>
        public int Viewers { get; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; }

        /// <summary>
        /// VideoPlayback constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public VideoPlayback(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            switch (json.SelectToken("type").ToString())
            {
                case "stream-up":
                    Type = VideoPlaybackType.StreamUp;
                    break;
                case "stream-down":
                    Type = VideoPlaybackType.StreamDown;
                    break;
                case "viewcount":
                    Type = VideoPlaybackType.ViewCount;
                    break;
                case "commercial":
                    Type = VideoPlaybackType.Commercial;
                    break;
            }
            ServerTime = json.SelectToken("server_time")?.ToString();
            switch (Type)
            {
                case VideoPlaybackType.StreamUp:
                    PlayDelay = int.Parse(json.SelectToken("play_delay").ToString());
                    break;
                case VideoPlaybackType.ViewCount:
                    Viewers = int.Parse(json.SelectToken("viewers").ToString());
                    break;
                case VideoPlaybackType.StreamDown:
                    break;
                case VideoPlaybackType.Commercial:
                    Length = int.Parse(json.SelectToken("length").ToString());
                    break;
            }
        }
    }
}
