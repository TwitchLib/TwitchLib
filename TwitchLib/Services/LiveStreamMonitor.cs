

namespace TwitchLib.Services
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Timers;
    using Events.Services.LiveStreamMonitor;
    using System.Linq;
    using System.Collections.Concurrent;
    using Exceptions.API;
    using Exceptions.Services;
    #endregion
    /// <summary>Service that allows customizability and subscribing to detection of channels going online/offline.</summary>
    public class LiveStreamMonitor
    {
        #region Private Variables
        private string _clientId;
        private int _checkIntervalSeconds;
        private bool _isStartup;
        private List<string> _channelIds;
        private readonly ConcurrentDictionary<string, string> _channelToId;
        private readonly ConcurrentDictionary<string, Models.API.v5.Streams.Stream> _statuses;
        private readonly Timer _streamMonitorTimer = new Timer();
        private readonly bool _checkStatusOnStart;
        private readonly bool _invokeEventsOnStart;
        private readonly ITwitchAPI _api;
        
        #endregion

        #region Public Variables
        /// <summary>Property representing Twitch channelIds service is monitoring.</summary>
        public List<string> ChannelIds
        {
            get => _channelIds.ToList();
            protected set => _channelIds = value;
        }
        /// <summary> Property representing Twitch channels service is monitoring. </summary>
        public ReadOnlyCollection<string> Channels => _channelToId.Keys.ToList().AsReadOnly();

        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get => _clientId; set { _clientId = value; _api.Settings.ClientId = value; } }
        /// <summary> </summary>
        public List<Models.API.v5.Streams.Stream> CurrentLiveStreams { get { return _statuses.Where(x => x.Value != null).Select(x => x.Value).ToList(); } }
        /// <summary> </summary>
        public List<string> CurrentOfflineStreams { get { return _statuses.Where(x => x.Value == null).Select(x => x.Key).ToList(); } }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get => _checkIntervalSeconds; set { _checkIntervalSeconds = value; _streamMonitorTimer.Interval = value * 1000; } }
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
        /// <param name="api">Instance of the Twitch Api Interface.</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
        /// <param name="checkStatusOnStart">Checks the channel statuses on starting the service</param>
        /// <param name="invokeEventsOnStart">If checking the status on service start, optionally fire the OnStream Events (OnStreamOnline, OnStreamOffline, OnStreamUpdate)</param>
        public LiveStreamMonitor(ITwitchAPI api, int checkIntervalSeconds = 60, string clientId = "", bool checkStatusOnStart = true, bool invokeEventsOnStart = false)
        {
            _api = api;
            _channelIds = new List<string>();
            _statuses = new ConcurrentDictionary<string, Models.API.v5.Streams.Stream>();
            _channelToId = new ConcurrentDictionary<string, string>();
            _checkStatusOnStart = checkStatusOnStart;
            _invokeEventsOnStart = invokeEventsOnStart;
            CheckIntervalSeconds = checkIntervalSeconds;
            _streamMonitorTimer.Elapsed += _streamMonitorTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Starts service, updates status of all channels, fires OnStreamMonitorStarted event. </summary>
        /// <exception cref="UnintializedChannelListException">If no channels to monitor were provided an UnintializedChannelListException will be thrown.</exception>
        public void StartService()
        {
            if(! _channelIds.Any())
                throw new UnintializedChannelListException("You must atleast add 1 channel to monitor before starting the Service");

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
            _getUserIds(usernames);

            foreach (var item in _channelToId.Keys.Where(x => !usernames.Any(channelToId => channelToId.Equals(x))).ToList())
                _channelToId.TryRemove(item, out string _);

            SetStreamsByUserId(_channelToId.Values.ToList());
        }
        
        /// <summary> Sets the list of channels to monitor by userid </summary>
        /// <param name="userids">List of channels to monitor as userids</param>
        public void SetStreamsByUserId(List<string> userids)
        {
            _channelIds = userids;
            _channelIds.ForEach(x => _statuses.TryAdd(x, null));

            foreach (var item in _statuses.Keys.Where(x => !_channelIds.Any(channelId => channelId.Equals(x))).ToList())
                _statuses.TryRemove(item, out Models.API.v5.Streams.Stream _);

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
                var currentStream = liveStreamers.FirstOrDefault(x => x.Channel.Id == channel);
                if (currentStream == null)
                {
                    //offline
                    if (_statuses[channel] != null)
                    {
                        var channelName = _statuses[channel].Channel.Name;
                        //have gone offline
                        _statuses[channel] = null;

                        if (!_isStartup || _invokeEventsOnStart)
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
                        if (!_isStartup || _invokeEventsOnStart)
                        {
                            OnStreamOnline?.Invoke(this,
                                new OnStreamOnlineArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    else
                    {
                        //stream updated
                        if (!_isStartup || _invokeEventsOnStart)
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

            var resultset = await _api.Streams.v5.GetLiveStreamsAsync(_channelIds.Select(x => x.ToString()).ToList(), limit: 100);

            livestreamers.AddRange(resultset.Streams.ToList());

            var pages = (int)Math.Ceiling((double)resultset.Total / 100);
            for (var i = 1; i < pages; i++)
            {
                resultset = await _api.Streams.v5.GetLiveStreamsAsync(_channelIds.Select(x => x.ToString()).ToList(), limit: 100, offset: i * 100);
                livestreamers.AddRange(resultset.Streams.ToList());
            }

            return livestreamers;
        }

        private async void _getUserIds(List<string> usernames)
        {
            var usernamesToGet = usernames.Where(u => !_channelToId.Any(c => c.Key.Equals(u))).ToList();
            var pages = (usernamesToGet.Count + 100 - 1) / 100;

            for (var i = 0; i < pages; i++)
            {
                var selectedSet = usernamesToGet.Skip(i * 100).Take(100).ToList();
                var users = await _api.Users.v5.GetUsersByNameAsync(selectedSet);

                foreach (var user in users.Matches)
                    _channelToId.TryAdd(user.Name, user.Id);
            }
        }
    }
}
