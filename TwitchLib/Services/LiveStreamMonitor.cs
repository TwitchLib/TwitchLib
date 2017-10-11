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
        private bool _isStartup = false;
        private List<long> _channelIds;
        private Dictionary<string,long> _channelToId;
        private Dictionary<long, Models.API.v5.Streams.Stream> _statuses;
        private readonly Timer _streamMonitorTimer = new Timer();
        private readonly bool _checkStatusOnStart;
        private readonly bool _invokeEventsOnStart;

        #endregion

        #region Public Variables
        /// <summary>Property representing Twitch channelIds service is monitoring.</summary>
        public List<long> ChannelIds
        {
            get
            {
                return _channelIds.ToList();
            }
            protected set
            {
                _channelIds = value;
            }
        }
        /// <summary> Property representing Twitch channels service is monitoring. </summary>
        public List<string> Channels
        {
            get
            {
                return _channelToId.Keys.ToList();
            }
        }
        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get { return _clientId; } set { _clientId = value; TwitchAPI.Settings.ClientId = value; } }
        /// <summary> </summary>
        public List<Models.API.v5.Streams.Stream> CurrentLiveStreams { get { return _statuses.Where(x => x.Value != null).Select(x => x.Value).ToList(); } }
        /// <summary> </summary>
        public List<long> CurrentOfflineStreams { get { return _statuses.Where(x => x.Value == null).Select(x => x.Key).ToList(); } }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get { return _checkIntervalSeconds; } set { _checkIntervalSeconds = value; _streamMonitorTimer.Interval = value * 1000; } }
        #endregion

        #region EVENTS
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamOnlineArgs> OnStreamOnline;
        /// <summary>Event fires when Stream goes offline</summary>
        public event EventHandler<OnStreamOfflineArgs> OnStreamOffline;
        /// <summary>Event fires when Stream gets updated</summary>
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
        /// <param name="checkStatusOnStart">Checks the channel statuses on starting the service</param>
        /// <param name="invokeEventsOnStart">If checking the status on service start, optionally fire the OnStream Events (OnStreamOnline, OnStreamOffline, OnStreamUpdate)</param>
        public LiveStreamMonitor(int checkIntervalSeconds = 60, string clientId = "", bool checkStatusOnStart = true, bool invokeEventsOnStart = false)
        {
            _channelIds = new List<long>();
            _statuses = new Dictionary<long, Models.API.v5.Streams.Stream>();
            _channelToId = new Dictionary<string, long>();
            _checkStatusOnStart = checkStatusOnStart;
            _invokeEventsOnStart = invokeEventsOnStart;
            CheckIntervalSeconds = checkIntervalSeconds;
            _streamMonitorTimer.Elapsed += _streamMonitorTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Starts service, updates status of all channels, fires OnStreamMonitorStarted event. </summary>
        public void StartService()
        {
            if(_checkStatusOnStart)
            {
                _isStartup = true;
                _checkOnlineStreams();
                _isStartup = false;
            }
            //Timer not started until initial check complete
            _streamMonitorTimer.Start();
            OnStreamMonitorStarted?.Invoke(this,
                new OnStreamMonitorStartedArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }

        /// <summary>Stops service and fires OnStreamMonitorStopped event.</summary>
        public void StopService()
        {
            _streamMonitorTimer.Stop();
            OnStreamMonitorEnded?.Invoke(this,
               new OnStreamMonitorEndedArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        /// <summary>
        /// Sets the list of channels to monitor by username
        /// </summary>
        /// <param name="usernames">List of channels to monitor as usernames</param>
        public void SetStreamsByUsername(List<string> usernames)
        {
            //this gets the userId if it doesn't already exist in _channelToId
            _getUserIds(usernames);

            //The following is done in this way due to IEnumerables not allowing
            //for items being removed while doing iteration

            //get the items that exist in _channelToId but not in usernames
            var toRemove = new List<string>();
            _channelToId.Keys.ToList().ForEach(x =>
            {
                if (!usernames.Contains(x))
                {
                    toRemove.Add(x);
                }
            });
            //remove the items from _channelToId that should be removed
            toRemove.ForEach(x =>
            {
                _channelToId.Remove(x);
            });

            SetStreamsByUserId(_channelToId.Values.ToList());
        }

        /// <summary> Sets the list of channels to monitor by userid </summary>
        /// <param name="userids">List of channels to monitor as userids</param>
        public void SetStreamsByUserId(List<long> userids)
        {
            _channelIds = userids;
            _channelIds.ForEach(x =>
            {
                if (!_statuses.ContainsKey(x))
                {
                    _statuses.Add(x, null);
                }
            });

            //The following is done in this way due to IEnumerables not allowing
            //for items being removed while doing iteration
            var toRemove = new List<long>();
            _statuses.Keys.ToList().ForEach(x =>
            {
                if (!_channelIds.Contains(x))
                {

                    toRemove.Add(x);
                }
            });
            toRemove.ForEach(x =>
            {
                _statuses.Remove(x);
            });
            OnStreamsSet?.Invoke(this,
                new OnStreamsSetArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        #endregion

        private void _streamMonitorTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _checkOnlineStreams();
        }

        private void _checkOnlineStreams()
        {
            var liveStreamers = _getLiveStreamers().Result;

            foreach (var channel in _channelIds)
            {
                var currentStream = liveStreamers.Where(x => x.Channel.Id == channel).FirstOrDefault();
                if (currentStream == null)
                {
                    //offline
                    if (_statuses[channel] != null)
                    {
                        var channelName = _statuses[channel].Channel.Name;
                        //have gone offline
                        _statuses[channel] = null;

                        if (!_isStartup || (_isStartup && _invokeEventsOnStart))
                        {
                            OnStreamOffline?.Invoke(this,
                                new OnStreamOfflineArgs { ChannelId = channel, Channel = channelName, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                }
                else
                {
                    var channelName = currentStream.Channel.Name;
                    //online
                    if (_statuses[channel] == null)
                    {
                        //have gone online
                        if (!_isStartup || (_isStartup && _invokeEventsOnStart))
                        {
                            OnStreamOnline?.Invoke(this,
                                new OnStreamOnlineArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    else
                    {
                        //stream updated
                        if (!_isStartup || (_isStartup && _invokeEventsOnStart))
                        {
                            OnStreamUpdate?.Invoke(this,
                                new OnStreamUpdateArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    _statuses[channel] = currentStream;
                }
            }
        }

        private async Task<List<Models.API.v5.Streams.Stream>> _getLiveStreamers()
        {
            var livestreamers = new List<Models.API.v5.Streams.Stream>();

            var resultset = await TwitchAPI.Streams.v5.GetLiveStreamsAsync(_channelIds.Select(x => x.ToString()).ToList(), limit: 100);

            livestreamers.AddRange(resultset.Streams.ToList());

            var pages = (int)Math.Ceiling((double)resultset.Total / 100);
            for (var i = 1; i < pages; i++)
            {
                resultset = await TwitchAPI.Streams.v5.GetLiveStreamsAsync(_channelIds.Select(x => x.ToString()).ToList(), limit: 100, offset: i * 100);
                livestreamers.AddRange(resultset.Streams.ToList());
            }

            return livestreamers;
        }

        private async void _getUserIds(List<string> usernames)
        {
            var usernamesToGet = new List<string>();

            usernames.ForEach(username => {
                if(!_channelToId.ContainsKey(username))
                {
                    usernamesToGet.Add(username);
                }

            });
            var pages = (int)Math.Ceiling((double)usernamesToGet.Count / 100);
            
            for (var i = 0; i < pages; i++)
            {

                var selectedSet = usernamesToGet.Skip(i * 100).Take(100).ToList();
                var users = await TwitchAPI.Users.v5.GetUsersByNameAsync(selectedSet);

                foreach (var user in users.Matches)
                {
                    _channelToId.Add(user.Name, long.Parse(user.Id));
                }
            }
        }
    }
}
