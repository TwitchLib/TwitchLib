using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.EventSub.Webhooks.Core.Models;

#pragma warning disable 1591
namespace TwitchLib.EventSub.Webhooks.Middlewares
{

    public class EventSubSignatureVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly TwitchLibEventSubOptions _options;

        public EventSubSignatureVerificationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<TwitchLibEventSubOptions> options)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("TwitchLib.EventSub.Webhooks");
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (await IsValidEventSubRequest(context.Request))
            {
                await _next(context);
                return;
            }

            await WriteResponseAsync(context, 403, "text/plain", "Invalid Signature");
        }

        private async Task<bool> IsValidEventSubRequest(HttpRequest request)
        {
            try
            {
                if (!request.Headers.TryGetValue("Twitch-Eventsub-Message-Signature", out var providedSignatureHeader))
                    return false;

                var providedSignatureString = providedSignatureHeader.First().Split('=').ElementAtOrDefault(1);
                if (string.IsNullOrWhiteSpace(providedSignatureString))
                    return false;

                var providedSignature = BytesFromHex(providedSignatureString).ToArray();

                if (!request.Headers.TryGetValue("Twitch-Eventsub-Message-Id", out var idHeader))
                    return false;

                var id = idHeader.First();

                if (!request.Headers.TryGetValue("Twitch-Eventsub-Message-Timestamp", out var timestampHeader))
                    return false;

                var timestamp = timestampHeader.First();

                var computedSignature = CalculateSignature(Encoding.UTF8.GetBytes(id + timestamp + await ReadRequestBodyContentAsync(request)));

                return computedSignature.Zip(providedSignature, (a, b) => a == b).Aggregate(true, (a, r) => a && r);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while calculating signature!");
                return false;
            }
        }

        private static Memory<byte> BytesFromHex(ReadOnlySpan<char> content)
        {
            if (content.IsEmpty || content.IsWhiteSpace())
            {
                return Memory<byte>.Empty;
            }

            try
            {
                var data = MemoryPool<byte>.Shared.Rent(content.Length / 2).Memory;
                var input = 0;
                for (var output = 0; output < data.Length; output++)
                {
                    data.Span[output] = Convert.ToByte(new string(new[] { content[input++], content[input++] }), 16);
                }

                return input != content.Length ? Memory<byte>.Empty : data;
            }
            catch (Exception exception) when (exception is ArgumentException or FormatException)
            {
                return Memory<byte>.Empty;
            }
        }

        private byte[] CalculateSignature(byte[] payload)
        {
            using var hmac = new HMACSHA256(_options.SecretBytes!);
            return hmac.ComputeHash(payload);
        }

        private static async Task PrepareRequestBodyAsync(HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
                await request.Body.DrainAsync(CancellationToken.None);
            }

            request.Body.Seek(0L, SeekOrigin.Begin);
        }

        private static async Task<string> ReadRequestBodyContentAsync(HttpRequest request)
        {
            await PrepareRequestBodyAsync(request);
            using var reader = new StreamReader(request.Body, Encoding.UTF8, false, leaveOpen: true);
            var requestBody = await reader.ReadToEndAsync();
            request.Body.Seek(0L, SeekOrigin.Begin);

            return requestBody;
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