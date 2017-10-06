namespace TwitchLib.Services
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Timers;

    using Enums;
    using Events.Services.LiveStreamMonitor;
    using Exceptions.API;
    using Exceptions.Services;
    using System.Linq;
    #endregion
    /// <summary>Service that allows customizability and subscribing to detection of channels going online/offline.</summary>
    public class LiveStreamMonitor
    {
        #region Private Variables
        private string _clientId;
        private int _checkIntervalSeconds;
        private List<long> _channels;

        private Dictionary<long, Models.API.v5.Streams.Stream> _statuses = new Dictionary<long, Models.API.v5.Streams.Stream>();
        private readonly Timer _streamMonitorTimer = new Timer();

        #endregion

        #region Public Variables
        /// <summary>Property representing Twitch channels service is monitoring.</summary>
        public List<long> Channels {
            get
            {
                return _channels.ToList();
            }
            protected set
            {
                _channels = value;
            }
        }
        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get { return _clientId; } set { _clientId = value; TwitchAPI.Settings.ClientId = value; } }
        /// <summary> </summary>
        public List< Models.API.v5.Streams.Stream> CurrentLiveStreams { get { return _statuses.Where(x=>x.Value != null).Select(x=> x.Value).ToList(); } }
        /// <summary> </summary>
        public List<long> CurrentOfflineStreams { get { return _statuses.Where(x => x.Value == null).Select(x => x.Key).ToList(); } }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get { return _checkIntervalSeconds; } set { _checkIntervalSeconds = value; _streamMonitorTimer.Interval = value * 1000; } }
        #endregion

        #region EVENTS
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamOnlineArgs> OnStreamOnline;
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamOfflineArgs> OnStreamOffline;
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamUpdateArgs> OnStreamUpdate;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnStreamMonitorStartedArgs> OnStreamMonitorStarted;
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnStreamMonitorEndedArgs> OnStreamMonitorEnded;
        /// <summary>Event fires when channels to monitor are intitialized.</summary>
        public event EventHandler<OnStreamsSetArgs> OnStreamsSet;
        #endregion
        /// <summary>Service constructor.</summary>
        /// <exception cref="BadResourceException">If channel is invalid, an InvalidChannelException will be thrown.</exception>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
        public LiveStreamMonitor(int checkIntervalSeconds = 60, string clientId = "")
        {
            _channels = new List<long>();
            _statuses = new Dictionary<long, Models.API.v5.Streams.Stream>();
            CheckIntervalSeconds = checkIntervalSeconds;
            _streamMonitorTimer.Elapsed += _streamMonitorTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Starts service, updates status of all channels, fires OnStreamMonitorStarted event. </summary>
        public void StartService()
        {
            _streamMonitorTimer.Start();
            OnStreamMonitorStarted?.Invoke(this,
                new OnStreamMonitorStartedArgs { Channels = Channels, CheckIntervalSeconds = CheckIntervalSeconds });
        }

        /// <summary>Stops service and fires OnStreamMonitorStopped event.</summary>
        public void StopService()
        {
            _streamMonitorTimer.Stop();
            OnStreamMonitorEnded?.Invoke(this,
               new OnStreamMonitorEndedArgs { Channels = Channels, CheckIntervalSeconds = CheckIntervalSeconds });
        }
 
      
        /// <summary> Sets the list of channels to monitor by username </summary>
        /// <param name="userids">List of channels to monitor as userids</param>
        public void SetStreamsByUserId(List<long> userids)
        {
            _channels = userids;
            _channels.ForEach(x => {
                if (!_statuses.ContainsKey(x))
                {
                    _statuses.Add(x, null);
                }
            });

            var toRemove = new List<long>();
            _statuses.Keys.ToList().ForEach(x => {
                if (!_channels.Contains(x))
                {

                    toRemove.Add(x);
                }
            });
            toRemove.ForEach(x =>
            {
                _statuses.Remove(x);
            });
            OnStreamsSet?.Invoke(this,
                new OnStreamsSetArgs { Channels = Channels, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        #endregion

        private async void _streamMonitorTimerElapsed(object sender, ElapsedEventArgs e)
        {
           await _checkOnlineStreams();
        }

        private async Task _checkOnlineStreams()
        {
            var liveStreamers = await _getLiveStreamers();

            foreach (var channel in _statuses)
            {
                var currentStream = liveStreamers.Where(x => x.Channel.Id == channel.Key).FirstOrDefault();
                if (currentStream == null)
                {
                    //offline
                    if (channel.Value != null)
                    {
                        //have gone offline
                        _statuses[channel.Key] = null;
                        OnStreamOffline?.Invoke(this,
                       new OnStreamOfflineArgs { Channel = channel.Key, CheckIntervalSeconds = CheckIntervalSeconds });
                    }
                }
                else
                {
                    _statuses[channel.Key] = currentStream;
                    //online
                    if (channel.Value == null)
                    {
                        //have gone online
                        OnStreamOnline?.Invoke(this,
                       new OnStreamOnlineArgs { Channel = channel.Key, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                    }
                    else
                    {
                        //stream object refreshed, not sure if we should do event for this
                        OnStreamUpdate?.Invoke(this,
                       new OnStreamUpdateArgs { Channel = channel.Key, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                    }
                }
            }

        }

        private async Task<List<Models.API.v5.Streams.Stream>> _getLiveStreamers()
        {
            var livestreamers = new List<Models.API.v5.Streams.Stream>();

            var resultset = await TwitchAPI.Streams.v5.GetLiveStreamsAsync(_channels.Select(x => x.ToString()).ToList(), limit: 100);

            livestreamers.AddRange(resultset.Streams.ToList());

            var pages = (int)Math.Ceiling((double)resultset.Total / 100);
            for (var i = 1; i < pages; i++)
            {
                resultset = await TwitchAPI.Streams.v5.GetLiveStreamsAsync(_channels.Select(x => x.ToString()).ToList(), limit: 100, offset: i * 100);
                livestreamers.AddRange(resultset.Streams.ToList());
            }

            return livestreamers;
        }


    }
}
