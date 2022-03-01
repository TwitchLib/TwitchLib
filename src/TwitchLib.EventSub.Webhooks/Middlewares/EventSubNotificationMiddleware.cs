using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchLib.EventSub.Webhooks.Core;

#pragma warning disable 1591
namespace TwitchLib.EventSub.Webhooks.Middlewares
{
    public class EventSubNotificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITwitchEventSubWebhooks _eventSubWebhooks;

        public EventSubNotificationMiddleware(RequestDelegate next, ITwitchEventSubWebhooks eventSubWebhooks)
        {
            _next = next;
            _eventSubWebhooks = eventSubWebhooks;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Twitch-Eventsub-Message-Type", out var messageType))
            {
                await _next(context);
                return;
            }

            var headers = context.Request.Headers.ToDictionary(k => k.Key, v => v.Value.ToString(), StringComparer.OrdinalIgnoreCase);

            switch (messageType)
            {
                case "webhook_callback_verification":
                    var json = await JsonDocument.ParseAsync(context.Request.Body);
                    await WriteResponseAsync(context, 200, "text/plain", json.RootElement.GetProperty("challenge").GetString()!);
                    return;
                case "notification":
                    await _eventSubWebhooks.ProcessNotificationAsync(headers, context.Request.Body);
                    await WriteResponseAsync(context, 200, "text/plain", "Thanks for the heads up Jordan");
                    return;
                case "revocation":
                    await _eventSubWebhooks.ProcessRevocationAsync(headers, context.Request.Body);
                    await WriteResponseAsync(context, 200, "text/plain", "Thanks for the heads up Jordan");
                    return;
                default:
                    await WriteResponseAsync(context, 400, "text/plain", $"Unknown EventSub message type: {messageType}");
                    return;
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, int statusCode, string contentType, string responseBody)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = contentType;
            await context.Response.WriteAsync(responseBody);
        }
    }
}
#pragma warning restore 1591