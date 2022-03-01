using System.Text.Json;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Channel;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Handler.Channel
{

    /// <summary>
    /// Handler for 'channel.cheer' notifications
    /// </summary>
    public class ChannelCheerHandler : NotificationHandler, INotificationHandler
    {
        /// <inheritdoc />
        public string SubscriptionType => "channel.cheer";

        private const string EVENT_NAME = "ChannelCheer";

        /// <inheritdoc />
        public void Handle(WebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
        {
            try
            {
                Handle<ChannelCheer, ChannelCheerArgs>(client, jsonString, serializerOptions, EVENT_NAME);
            }
            catch (Exception ex)
            {
                client.RaiseEvent("ErrorOccurred", new OnErrorArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
            }
        }

        /// <inheritdoc />
        public void Handle(WebhooksClient client, System.IO.Stream body, Dictionary<string, string> headers, JsonSerializerOptions serializerOptions)
        {
            try
            {
                Handle<ChannelCheer, ChannelCheerArgs>(client, body, headers, serializerOptions, EVENT_NAME);
            }
            catch (Exception ex)
            {
                client.RaiseEvent("ErrorOccurred", new OnErrorArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification!" });
            }
        }
    }
}