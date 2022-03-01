using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;

namespace TwitchLib.EventSub.Handler
{
    /// <inheritdoc />
    public abstract class NotificationHandler
    {
        /// <inheritdoc />
        protected static void Handle<T, U>(WebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions, string eventName)
                where T : class, new()
                where U : class, IEventSubEventArgs<T>, new()
        {
            var data = JsonSerializer.Deserialize<EventSubNotification<T>>(jsonString.AsSpan(), serializerOptions);

            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent(eventName, new U { Notification = data });
        }

        /// <inheritdoc />
        protected static void Handle<T, U>(WebhooksClient client, System.IO.Stream body, Dictionary<string, string> headers, JsonSerializerOptions serializerOptions, string eventName) 
                where T : class, new()
                where U : class, IEventSubEventArgs<T>, new()
        {
            var data = JsonSerializer.Deserialize<EventSubNotificationPayload<T>>(body, serializerOptions);
            var meta = new EventSubMetadata
            {
                MessageId = headers["Twitch-Eventsub-Message-Id"],
                MessageTimestamp = DateTime.Parse(headers["Twitch-Eventsub-Message-Timestamp"]),
                MessageType = headers["Twitch-Eventsub-Message-Type"],
                SubscriptionType = headers["Twitch-Eventsub-Subscription-Type"],
                SubscriptionVersion = headers["Twitch-Eventsub-Subscription-Version"]
            };
            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent(eventName,
                new U
                {
                    Notification = new EventSubNotification<T>
                    {
                        Metadata = meta,
                        Payload = data
                    }
                });
        }
    }
}
