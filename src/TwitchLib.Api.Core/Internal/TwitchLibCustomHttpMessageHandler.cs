using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.Internal
{
    public class TwitchHttpClientHandler : DelegatingHandler
    {
        private readonly ILogger<IHttpCallHandler> _logger;

        public TwitchHttpClientHandler(ILogger<IHttpCallHandler> logger) : base(new HttpClientHandler())
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
                _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Method: {method} Resource: {url} Content: {content}",
                    DateTime.Now, "Request", request.Method.ToString(), request.RequestUri.ToString(), await request.Content.ReadAsStringAsync());
            else
                _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Method: {method} Resource: {url}",
                    DateTime.Now, "Request", request.Method.ToString(), request.RequestUri.ToString());

            var stopwatch = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                    _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms Content: {content}",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds, await response.Content.ReadAsStringAsync());
                else
                    _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                if (response.Content != null)
                    _logger?.LogError("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms Content: {content}",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds, await response.Content.ReadAsStringAsync());
                else
                    _logger?.LogError("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
    }
}