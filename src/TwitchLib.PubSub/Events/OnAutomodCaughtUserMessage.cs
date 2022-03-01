
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;

namespace TwitchLib.PubSub.Events
{
    public class OnAutomodCaughtUserMessage
    {
        /// <summary>
        /// Details about the caught message
        /// </summary>
        public AutomodCaughtMessage AutomodCaughtMessage;
        /// <summary>
        /// The ID of the channel that this event fired from.
        /// </summary>
        public string ChannelId;
        /// <summary>
        /// The ID of the user that this event fired from.
        /// </summary>
        public string UserId;
    }
}
