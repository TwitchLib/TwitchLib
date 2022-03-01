using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.Helix
{
    public class Helix
    {
        private readonly ILogger<Helix> _logger;
        public IApiSettings Settings { get; }
        public Analytics Analytics { get; }
        public Ads Ads { get; }
        public Bits Bits { get; }
        public Chat Chat { get; }
        public Channels Channels { get; }
        public ChannelPoints ChannelPoints { get; }
        public Clips Clips { get; }
        public Entitlements Entitlements { get; }
        public EventSub EventSub { get; }
        public Extensions Extensions { get; }
        public Games Games { get; }
        public Goals Goals { get; }
        public HypeTrain HypeTrain { get; }
        public Moderation Moderation { get; }
        public Polls Polls { get; }
        public Predictions Predictions { get; }
        public Schedule Schedule { get; }
        public Search Search { get; }
        public Subscriptions Subscriptions { get; }
        public Streams Streams { get; }
        public Tags Tags { get; }
        public Teams Teams { get; }
        public Videos Videos { get; }
        public Users Users { get; }

        /// <summary>
        /// Creates an Instance of the Helix Class.
        /// </summary>
        /// <param name="loggerFactory">Instance Of LoggerFactory, otherwise no logging is used, </param>
        /// <param name="rateLimiter">Instance Of RateLimiter, otherwise no ratelimiter is used.</param>
        /// <param name="settings">Instance of ApiSettings, otherwise defaults used, can be changed later</param>
        /// <param name="http">Instance of HttpCallHandler, otherwise default handler used</param>
        public Helix(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
        {
            _logger = loggerFactory?.CreateLogger<Helix>();
            rateLimiter ??= BypassLimiter.CreateLimiterBypassInstance();
            http ??= new TwitchHttpClient(loggerFactory?.CreateLogger<TwitchHttpClient>());
            Settings = settings ?? new ApiSettings();

            Analytics = new Analytics(Settings, rateLimiter, http);
            Ads = new Ads(Settings, rateLimiter, http);
            Bits = new Bits(Settings, rateLimiter, http);
            Chat = new Chat(Settings, rateLimiter, http);
            Channels = new Channels(Settings, rateLimiter, http);
            ChannelPoints = new ChannelPoints(Settings, rateLimiter, http);
            Clips = new Clips(Settings, rateLimiter, http);
            Entitlements = new Entitlements(Settings, rateLimiter, http);
            EventSub = new EventSub(Settings, rateLimiter, http);
            Extensions = new Extensions(Settings, rateLimiter, http);
            Games = new Games(Settings, rateLimiter, http);
            Goals = new Goals(settings, rateLimiter, http);
            HypeTrain = new HypeTrain(Settings, rateLimiter, http);
            Moderation = new Moderation(Settings, rateLimiter, http);
            Polls = new Polls(Settings, rateLimiter, http);
            Predictions = new Predictions(Settings, rateLimiter, http);
            Schedule = new Schedule(Settings, rateLimiter, http);
            Search = new Search(Settings, rateLimiter, http);
            Streams = new Streams(Settings, rateLimiter, http);
            Subscriptions = new Subscriptions(Settings, rateLimiter, http);
            Tags = new Tags(Settings, rateLimiter, http);
            Teams = new Teams(Settings, rateLimiter, http);
            Users = new Users(Settings, rateLimiter, http);
            Videos = new Videos(Settings, rateLimiter, http);
        }
    }
}
