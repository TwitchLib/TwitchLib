using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;

namespace TwitchLib.PubSub.Events
{
    public class OnAutomodCaughtMessageArgs
    {
        /// <summary>
        /// Details about the caught message
        /// </summary>
        public AutomodCaughtMessage AutomodCaughtMessage;
        /// <summary>
        /// The ID of the channel that this event fired from.
        /// </summary>
        public string ChannelId;
    }
}
