using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitchLib.Services
{
    /// <summary>Service that allows customizability and subscribing to detection of new Twitch followers.</summary>
    public class FollowerService
    {
        private string _channel, _clientId;
        private int _queryCount, _checkIntervalSeconds;

        private Timer _followerServiceTimer = new Timer();
        /// <summary>Property representing Twitch channel service is monitoring.</summary>
        public string Channel { get { return _channel; } protected set { _channel = value; } }
        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get { return _clientId; } set { _clientId = value; TwitchApi.SetClientId(value); } }
        /// <summary>Property representing number of recent followers that service should request. Recommended: 25, increase for larger channels. MAX: 100, MINIMUM: 1</summary>
        /// <exception cref="Exceptions.BadQueryCountException">Throws BadQueryCountException if queryCount is larger than 100 or smaller than 1.</exception>
        public int QueryCount { get { return _queryCount; } set { if (value < 1 || value > 100) { throw new Exceptions.BadQueryCountException("Query count was smaller than 1 or exceeded 100"); } _queryCount = value; } }
        /// <summary>Property representing the cache where detected followers are stored and compared against.</summary>
        public List<TwitchAPIClasses.TwitchFollower> ActiveCache { get; set; }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get { return _checkIntervalSeconds; } set { _checkIntervalSeconds = value; _followerServiceTimer.Interval = value * 1000; } }

        /// <summary>Service constructor.</summary>
        /// <exception cref="Exceptions.InvalidChannelException">If channel is invalid, an InvalidChannelException will be thrown.</exception>
        /// <param name="channel">Param representing the channel the service should monitor.</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="queryCount">Number of recent followers service should request from Twitch Api. Max: 100, Min: 1</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
        public FollowerService(string channel, int checkIntervalSeconds = 60, int queryCount = 25, string clientId = "")
        {
            Channel = channel;
            CheckIntervalSeconds = checkIntervalSeconds;
            _followerServiceTimer.Elapsed += _followerServiceTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Downloads recent followers from Twitch, starts service, fires OnServiceStarted event.</summary>
        public async void StartService()
        {
            TwitchAPIClasses.TwitchFollowersResponse response = await TwitchApi.GetTwitchFollowers(Channel, QueryCount);
            ActiveCache = response.Followers;
            _followerServiceTimer.Start();
            OnServiceStarted?.Invoke(null, 
                new OnServiceStartedArgs { Channel = Channel, CheckIntervalSeconds = CheckIntervalSeconds, QueryCount = QueryCount });
        }

        /// <summary>Stops service and fires OnServiceStopped event.</summary>
        public void StopService()
        {
            _followerServiceTimer.Stop();
            OnServiceStopped?.Invoke(null,
                new OnServiceStoppedArgs { Channel = Channel, CheckIntervalSeconds = CheckIntervalSeconds, QueryCount = QueryCount });
        }
        #endregion

        private async void _followerServiceTimerElapsed(object sender, ElapsedEventArgs e)
        {
            TwitchAPIClasses.TwitchFollowersResponse response = await TwitchApi.GetTwitchFollowers(Channel, QueryCount);
            List<TwitchAPIClasses.TwitchFollower> mostRecentFollowers = response.Followers;
            List<TwitchAPIClasses.TwitchFollower> newFollowers = new List<TwitchAPIClasses.TwitchFollower>();
            if(ActiveCache == null)
            {
                ActiveCache = mostRecentFollowers;
                newFollowers = ActiveCache;
            } else
            {
                foreach (TwitchAPIClasses.TwitchFollower follower in mostRecentFollowers)
                    if (isNewFollower(follower))
                        newFollowers.Add(follower);
            }
            if(newFollowers.Count > 0)
                OnNewFollowersDetected?.Invoke(null,
                    new OnNewFollowersDetectedArgs { Channel = Channel, CheckIntervalSeconds = CheckIntervalSeconds, QueryCount = QueryCount, NewFollowers = newFollowers });
        }

        #region HELPERS
        private bool isNewFollower(TwitchAPIClasses.TwitchFollower follower)
        {
            foreach (TwitchAPIClasses.TwitchFollower oldFollower in ActiveCache)
                if (oldFollower.User.Name.ToLower() == follower.User.Name.ToLower())
                    return false;
            return true;
        }
        #endregion

        #region EVENTS
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnServiceStartedArgs> OnServiceStarted;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnServiceStoppedArgs> OnServiceStopped;
        /// <summary>Event fires when new followers are detected.</summary>
        public event EventHandler<OnNewFollowersDetectedArgs> OnNewFollowersDetected;

        /// <summary>Class representing event args for OnServiceStarted event.</summary>
        public class OnServiceStartedArgs : EventArgs
        {
            /// <summary>Event property representing channel the service is currently monitoring.</summary>
            public string Channel;
            /// <summary>Event property representing number of recent followers a query to Twitch Api should return.</summary>
            public int QueryCount;
            /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
            public int CheckIntervalSeconds;
        }

        /// <summary>Class representing event args for OnServiceStopped event.</summary>
        public class OnServiceStoppedArgs : EventArgs
        {
            /// <summary>Event property representing channel the service is currently monitoring.</summary>
            public string Channel;
            /// <summary>Event property representing number of recent followers a query to Twitch Api should return.</summary>
            public int QueryCount;
            /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
            public int CheckIntervalSeconds;
        }

        /// <summary>Class representing event args for OnNewFollowersDetected event.</summary>
        public class OnNewFollowersDetectedArgs : EventArgs
        {
            /// <summary>Event property representing channel the service is currently monitoring.</summary>
            public string Channel;
            /// <summary>Event property representing number of recent followers a query to Twitch Api should return.</summary>
            public int QueryCount;
            /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
            public int CheckIntervalSeconds;
            /// <summary>Event property representing all new followers detected.</summary>
            public List<TwitchAPIClasses.TwitchFollower> NewFollowers;
        }

        #endregion
    }
}
