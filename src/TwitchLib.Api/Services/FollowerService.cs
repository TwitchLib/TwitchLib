using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core.FollowerService;
using TwitchLib.Api.Services.Events.FollowerService;

namespace TwitchLib.Api.Services
{
    public class FollowerService : ApiService
    {
        private readonly Dictionary<string, DateTime> _lastFollowerDates = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

        private CoreMonitor _monitor;
        private IdBasedMonitor _idBasedMonitor;
        private NameBasedMonitor _nameBasedMonitor;

        /// <summary>
        /// The current known followers for each channel.
        /// </summary>
        public Dictionary<string, List<Follow>> KnownFollowers { get; } = new Dictionary<string, List<Follow>>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// The amount of followers queried per request.
        /// </summary>
        public int QueryCountPerRequest { get; }
        /// <summary>
        /// The maximum amount of followers cached per channel.
        /// </summary>
        public int CacheSize { get; }

        private IdBasedMonitor IdBasedMonitor => _idBasedMonitor ?? (_idBasedMonitor = new IdBasedMonitor(_api));
        private NameBasedMonitor NameBasedMonitor => _nameBasedMonitor ?? (_nameBasedMonitor = new NameBasedMonitor(_api));

        /// <summary>
        /// Event which is called when new followers are detected.
        /// </summary>
        public event EventHandler<OnNewFollowersDetectedArgs> OnNewFollowersDetected;
        
        /// <summary>
        /// FollowerService constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">When the <paramref name="api"/> is null.</exception>
        /// <exception cref="ArgumentException">When the <paramref name="checkIntervalInSeconds"/> is lower than one second.</exception> 
        /// <exception cref="ArgumentException">When the <paramref name="queryCountPerRequest" /> is less than 1 or more than 100 followers per request.</exception>
        /// <exception cref="ArgumentException">When the <paramref name="cacheSize" /> is less than the queryCountPerRequest.</exception>
        /// <param name="api">The api to use for querying followers.</param>
        /// <param name="checkIntervalInSeconds">How often new followers should be queried.</param>
        /// <param name="queryCountPerRequest">The amount of followers to query per request.</param>
        /// <param name="cacheSize">The maximum amount of followers to cache per channel.</param>
        public FollowerService(ITwitchAPI api, int checkIntervalInSeconds = 60, int queryCountPerRequest = 100, int cacheSize = 1000) : 
            base(api, checkIntervalInSeconds)
        {
            if (queryCountPerRequest < 1 || queryCountPerRequest > 100)
                throw new ArgumentException("Twitch doesn't support less than 1 or more than 100 followers per request.", nameof(queryCountPerRequest));

            if (cacheSize < queryCountPerRequest)
                throw new ArgumentException($"The cache size must be at least the size of the {nameof(queryCountPerRequest)} parameter.", nameof(cacheSize));

            QueryCountPerRequest = queryCountPerRequest;
            CacheSize = cacheSize;
        }

        /// <summary>
        /// Clears the existing cache.
        /// </summary>
        public void ClearCache()
        {
            KnownFollowers.Clear();

            _lastFollowerDates.Clear();

            _nameBasedMonitor?.ClearCache();

            _nameBasedMonitor = null;
            _idBasedMonitor = null;
        }

        /// <summary>
        /// Sets the channels to monitor by id. Event's channel properties will be Ids in this case.
        /// </summary>
        /// <exception cref="ArgumentNullException">When <paramref name="channelsToMonitor"/> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="channelsToMonitor"/> is empty.</exception>
        /// <param name="channelsToMonitor">A list with channels to monitor.</param>
        public void SetChannelsById(List<string> channelsToMonitor)
        {
            SetChannels(channelsToMonitor);

            _monitor = IdBasedMonitor;
        }

        /// <summary>
        /// Sets the channels to monitor by name. Event's channel properties will be names in this case.
        /// </summary>
        /// <exception cref="ArgumentNullException">When <paramref name="channelsToMonitor"/> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="channelsToMonitor"/> is empty.</exception>
        /// <param name="channelsToMonitor">A list with channels to monitor.</param>
        public void SetChannelsByName(List<string> channelsToMonitor)
        {
            SetChannels(channelsToMonitor);

            _monitor = NameBasedMonitor;
        }

        /// <summary>
        /// Updates the followerservice with the latest followers. Automatically called internally when service is started.
        /// </summary>
        /// <param name="callEvents">Whether to invoke the update events or not.</param>
        public async Task UpdateLatestFollowersAsync(bool callEvents = true)
        {
            if (ChannelsToMonitor == null)
                return;

            foreach (var channel in ChannelsToMonitor)
            {
                List<Follow> newFollowers;
                var latestFollowers = await GetLatestFollowersAsync(channel);

                if (latestFollowers.Count == 0)
                    return;

                if (!KnownFollowers.TryGetValue(channel, out var knownFollowers))
                {
                    newFollowers = latestFollowers;
                    KnownFollowers[channel] = latestFollowers.Take(CacheSize).ToList();
                    _lastFollowerDates[channel] = latestFollowers.Last().FollowedAt;
                }
                else
                {
                    var existingFollowerIds = new HashSet<string>(knownFollowers.Select(f => f.FromUserId));
                    var latestKnownFollowerDate = _lastFollowerDates[channel];
                    newFollowers = new List<Follow>();

                    foreach (var follower in latestFollowers)
                    {
                        if (!existingFollowerIds.Add(follower.FromUserId)) continue;

                        if (follower.FollowedAt < latestKnownFollowerDate) continue;

                        newFollowers.Add(follower);
                        latestKnownFollowerDate = follower.FollowedAt;
                        knownFollowers.Add(follower);
                    }

                    existingFollowerIds.Clear();
                    existingFollowerIds.TrimExcess();

                    // prune cache so we don't use too much space unnecessarily
                    if (knownFollowers.Count > CacheSize)
                        knownFollowers.RemoveRange(0, knownFollowers.Count - CacheSize);

                    if (newFollowers.Count <= 0)
                        return;

                    _lastFollowerDates[channel] = latestKnownFollowerDate;
                }

                if (!callEvents)
                    return;

                OnNewFollowersDetected?.Invoke(this, new OnNewFollowersDetectedArgs { Channel = channel, NewFollowers = newFollowers });
            }
        }

        protected override async Task OnServiceTimerTick()
        {
            await base.OnServiceTimerTick();
            await UpdateLatestFollowersAsync();
        }

        private async Task<List<Follow>> GetLatestFollowersAsync(string channel)
        {
            var resultset = await _monitor.GetUsersFollowsAsync(channel, QueryCountPerRequest);
            
            return resultset.Follows.Reverse().ToList();
        }
    }
}
