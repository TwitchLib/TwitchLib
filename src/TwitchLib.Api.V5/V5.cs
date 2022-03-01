using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.V5
{
    public class V5
    {
        private readonly ILogger<V5> _logger;
        public IApiSettings Settings { get; }
        public Badges Badges { get; }
        public Bits Bits { get; }
        public Channels Channels { get; }
        public Chat Chat { get; }
        public Clips Clips { get; }
        public Collections Collections { get; }
        public Games Games { get; }
        public Ingests Ingests { get; }
        public Root Root { get; }
        public Search Search { get; }
        public Streams Streams { get; }
        public Teams Teams { get; }
        public Videos Videos { get; }
        public Users Users { get; }


        /// <summary>
        /// Creates an Instance of the V5 Class.
        /// </summary>
        /// <param name="loggerFactory">Instance Of LoggerFactory, otherwise no logging is used, </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used.</param>
        /// <param name="settings">Instance of ApiSettings, otherwise defaults used, can be changed later</param>
        /// <param name="http">Instance of HttpCallHandler, otherwise default handler used</param>
        public V5(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
        {
            _logger = loggerFactory?.CreateLogger<V5>();
            rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
            http = http ?? new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Badges = new Badges(Settings, rateLimiter, http);
            Bits = new Bits(Settings, rateLimiter, http);
            Channels = new Channels(Settings, rateLimiter, http);
            Chat = new Chat(Settings, rateLimiter, http);
            Clips = new Clips(Settings, rateLimiter, http);
            Collections = new Collections(Settings, rateLimiter, http);
            Games = new Games(Settings, rateLimiter, http);
            Ingests = new Ingests(Settings, rateLimiter, http);
            Root = new Root(Settings, rateLimiter, http);
            Search = new Search(Settings, rateLimiter, http);
            Streams = new Streams(Settings, rateLimiter, http);
            Teams = new Teams(Settings, rateLimiter, http);
            Users = new Users(Settings, rateLimiter, http);
            Videos = new Videos(Settings, rateLimiter, http);
        }

        
    }
}
