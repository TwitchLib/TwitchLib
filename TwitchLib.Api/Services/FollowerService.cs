using System;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using System.Threading.Tasks;
using TwitchLib.Api.Services.Exceptions;
using TwitchLib.Api.Services.Events.FollowerService;

namespace TwitchLib.Api.Services
{
    /// <summary>Service that allows customizability and subscribing to detection of new Twitch followers.</summary>
    public class FollowerService
    {
        private string _channel, _clientId;
        private int _queryCount, _checkIntervalSeconds;
        private readonly ITwitchAPI _api;

        private readonly Timer _followerServiceTimer = new Timer();
        /// <summary>Property representing Twitch channel service is monitoring.</summary>
        public string ChannelData { get => _channel; protected set => _channel = value; }
        /// <summary>Property representing whether channeldata is a channel name or channel id.</summary>
        public Enums.ChannelIdentifierType ChannelIdentifier { get; protected set; }
        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get => _clientId; set { _clientId = value; _api.Settings.ClientId = value; } }
        /// <summary>Property representing the number of followers to compare a fresh query against for new followers. Default: 1000.</summary>
        public int CacheSize { get; set; } = 1000;
        /// <summary>Property representing number of recent followers that service should request. Recommended: 25, increase for larger channels. MAX: 100, MINIMUM: 1</summary>
        /// <exception cref="BadQueryCountException">Throws BadQueryCountException if queryCount is larger than 100 or smaller than 1.</exception>
        public int QueryCount { get => _queryCount; set { if (value < 1 || value > 100) { throw new BadQueryCountException("Query count was smaller than 1 or exceeded 100"); } _queryCount = value; } }
        /// <summary>Property representing the cache where detected followers are stored and compared against.</summary>
        public List<Interfaces.IFollow> ActiveCache { get; set; } = new List<Interfaces.IFollow>();
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get => _checkIntervalSeconds; set { _checkIntervalSeconds = value; _followerServiceTimer.Interval = value * 1000; } }

        /// <summary>Service constructor.</summary>
        /// <exception cref="BadResourceException">If channel is invalid, an InvalidChannelException will be thrown.</exception>
        /// <param name="api">TwitchApi instance</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="queryCount">Number of recent followers service should request from Twitch Api. Max: 100, Min: 1</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
        public FollowerService(ITwitchAPI api, int checkIntervalSeconds = 60, int queryCount = 25, string clientId = "")
        {
            _api = api;
            CheckIntervalSeconds = checkIntervalSeconds;
            QueryCount = queryCount;
            _followerServiceTimer.Elapsed += _followerServiceTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Downloads recent followers from Twitch, starts service, fires OnServiceStarted event.</summary>
        public async Task StartService()
        {
            if (ChannelData == null)
            {
                throw new UninitializedChannelDataException("ChannelData must be set before starting the FollowerService. Use SetChannelByName() or SetChannelById()");
            }

            if (ChannelIdentifier == Enums.ChannelIdentifierType.Username)
            {
                var response = await _api.Follows.v3.GetFollowersAsync(ChannelData, QueryCount);
                foreach (var follower in response.Followers)
                    ActiveCache.Add(follower);
            }
            else
            {
                var response = await _api.Channels.v5.GetChannelFollowersAsync(ChannelData, QueryCount);
                foreach (var follower in response.Follows)
                    ActiveCache.Add(follower);
            }

            _followerServiceTimer.Start();
            OnServiceStarted?.Invoke(this,
                new OnServiceStartedArgs { ChannelIdentifier = ChannelIdentifier, ChannelData = ChannelData, CheckIntervalSeconds = CheckIntervalSeconds, QueryCount = QueryCount });
        }

        /// <summary>Stops service and fires OnServiceStopped event.</summary>
        public void StopService()
        {
            _followerServiceTimer.Stop();
            OnServiceStopped?.Invoke(this,
                new OnServiceStoppedArgs { ChannelIdentifier = ChannelIdentifier, ChannelData = ChannelData, CheckIntervalSeconds = CheckIntervalSeconds, QueryCount = QueryCount });
        }

        /// <summary>Tells FollowerService to request the channel by the channel name.</summary>
        /// <param name="channelName"></param>
        public void SetChannelByName(string channelName)
        {
            ChannelIdentifier = Enums.ChannelIdentifierType.Username;
            ChannelData = channelName;
        }

        /// <summary>Tells FollowerService to request the channel by the channel Id</summary>
        /// <param name="channelId"></param>
        public void SetChannelByChannelId(string channelId)
        {
            ChannelIdentifier = Enums.ChannelIdentifierType.UserId;
            ChannelData = channelId;
        }
        #endregion

        private async void _followerServiceTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var mostRecentFollowers = new List<Interfaces.IFollow>();
            try
            {
                if (ChannelIdentifier == Enums.ChannelIdentifierType.Username)
                {
                    var followers = await _api.Follows.v3.GetFollowersAsync(ChannelData, QueryCount);
                    mostRecentFollowers.AddRange(followers.Followers);
                }
                else
                {
                    var followers = await _api.Channels.v5.GetChannelFollowersAsync(ChannelData, QueryCount);
                    mostRecentFollowers.AddRange(followers.Follows);
                }
            }
            catch (WebException)
            {
                return;
            }
            var newFollowers = new List<Interfaces.IFollow>();

            foreach (var recentFollower in mostRecentFollowers)
            {
                var found = false;
                foreach (var cachedFollower in ActiveCache)
                {
                    if (recentFollower.User.Id == cachedFollower.User.Id)
                        found = true;
                }
                if (!found)
                    newFollowers.Add(recentFollower);
            }

            // Check for new followers
            if (newFollowers.Count <= 0) return;

            // add new followers to active cache
            ActiveCache.AddRange(newFollowers);

            // prune cache so we don't use too much space unnecessarily
            if (ActiveCache.Count > CacheSize)
                ActiveCache = ActiveCache.GetRange(ActiveCache.Count - (CacheSize + 1), CacheSize);

            // Invoke followers event with list of follows - IFollow
            OnNewFollowersDetected?.Invoke(this,
                new OnNewFollowersDetectedArgs
                {
                    ChannelIdentifier = ChannelIdentifier,
                    ChannelData = ChannelData,
                    CheckIntervalSeconds = CheckIntervalSeconds,
                    QueryCount = QueryCount,
                    NewFollowers = newFollowers
                });
        }


        #region EVENTS
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnServiceStartedArgs> OnServiceStarted;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnServiceStoppedArgs> OnServiceStopped;
        /// <summary>Event fires when new followers are detected.</summary>
        public event EventHandler<OnNewFollowersDetectedArgs> OnNewFollowersDetected;

        #endregion
    }
}
