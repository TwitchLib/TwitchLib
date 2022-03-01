using System.Text.Json;
using TwitchLib.PubSub.Models.Responses.Messages;

namespace TwitchLib.PubSub.Models.Responses
{
    /// <summary>
    /// PubSub Message model.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Topic that the message is relevant to.
        /// </summary>
        /// <value>The topic.</value>
        public string Topic { get; }
        /// <summary>
        /// Model containing data of the message.
        /// </summary>
        public readonly MessageData MessageData;

        /// <summary>
        /// PubSub Message model constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public Message(string jsonStr)
        {
            var json = JsonDocument.Parse(jsonStr);
            var data = json.RootElement.GetProperty("data");
            Topic = data.GetProperty("topic").GetString();
            var encodedJsonMessage = data.GetProperty("message").GetString();

            switch (Topic?.Split('.')[0])
            {
                case "user-moderation-notifications":
                    MessageData = new UserModerationNotifications(encodedJsonMessage);
                    break;
                case "automod-queue":
                    MessageData = new AutomodQueue(encodedJsonMessage);
                    break;
                case "chat_moderator_actions":
                    MessageData = new ChatModeratorActions(encodedJsonMessage);
                    break;
                case "channel-bits-events-v1":
                    MessageData = new ChannelBitsEvents(encodedJsonMessage);
                    break;
                case "channel-bits-events-v2":
                    MessageData = new ChannelBitsEventsV2(encodedJsonMessage);
                    break;
                case "video-playback-by-id":
                    MessageData = new VideoPlayback(encodedJsonMessage);
                    break;
                case "whispers":
                    MessageData = new Whisper(encodedJsonMessage);
                    break;
                case "channel-subscribe-events-v1":
                    MessageData = new ChannelSubscription(encodedJsonMessage);
                    break;
                case "channel-ext-v1":
                    MessageData = new ChannelExtensionBroadcast(encodedJsonMessage);
                    break;
                case "following":
                    MessageData = new Following(encodedJsonMessage);
                    break;
                case "community-points-channel-v1":
                    MessageData = new CommunityPointsChannel(encodedJsonMessage);
                    break;
                case "channel-points-channel-v1":
                    MessageData = new ChannelPointsChannel(encodedJsonMessage);
                    break;
                case "leaderboard-events-v1":
                    MessageData = new LeaderboardEvents(encodedJsonMessage);
                    break;
                case "raid":
                    MessageData = new RaidEvents(encodedJsonMessage);
                    break;                
                case "predictions-channel-v1":
                    MessageData = new PredictionEvents(encodedJsonMessage);
                    break;
            }
        }
    }
}
