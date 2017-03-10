using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TwitchLib.Exceptions.Services;
using TwitchLib.Exceptions.API;
using TwitchLib.Events.Services.FollowerService;
using TwitchLib.Models.Client;

namespace TwitchLib.Services
{
    /// <summary>Service that allows customizability and subscribing to detection of channels going online/offline.</summary>
    public class LiveChannelMonitor
    {
        #region Private Variables
        private string _clientId;
        private int _checkIntervalSeconds;
        private List<JoinedChannel> _channels;
        private Dictionary<JoinedChannel, bool> _statuses;
        private Timer _channelMonitorTimer = new Timer();
        #endregion
        #region Public Variables
        /// <summary>Property representing Twitch channels service is monitoring.</summary>
        public List<JoinedChannel> Channels { get { return _channels; } protected set { _channels = value; } }
        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get { return _clientId; } set { _clientId = value; TwitchApi.SetClientId(value); } }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get { return _checkIntervalSeconds; } set { _checkIntervalSeconds = value; _channelMonitorTimer.Interval = value * 1000; } }
        #endregion
        /// <summary>Service constructor.</summary>
        /// <exception cref="BadResourceException">If channel is invalid, an InvalidChannelException will be thrown.</exception>
        /// <param name="channels">Represents a list of channels to monitor</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
        public LiveChannelMonitor(List<JoinedChannel> channels, int checkIntervalSeconds = 60, string clientId = "")
        {
            Channels = channels;
            CheckIntervalSeconds = checkIntervalSeconds;
            _channelMonitorTimer.Elapsed += _channelMonitorTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Downloads recent followers from Twitch, starts service, fires OnServiceStarted event.</summary>
        public async void StartService()
        {
            foreach (var channel in Channels)
            {
                _statuses.Add(channel, await TwitchApi.Streams.BroadcasterOnlineAsync(channel.Channel));
            }
            _channelMonitorTimer.Start();
            OnChannelMonitorStarted?.Invoke(this,
                new OnChannelMonitorStartedArgs { Channels = Channels, CheckIntervalSeconds = CheckIntervalSeconds });
        }

        /// <summary>Stops service and fires OnChannelMonitorStopped event.</summary>
        public void StopService()
        {
            _channelMonitorTimer.Stop();
            OnChannelMonitorEnded?.Invoke(this,
               new OnChannelMonitorEndedArgs { Channels = Channels, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        #endregion

        private async void _channelMonitorTimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var channel in Channels)
            {
                bool current = await TwitchApi.Streams.BroadcasterOnlineAsync(channel.Channel);
                if (current && !_statuses[channel])
                {
                    OnChannelOnline?.Invoke(this,
                        new OnChannelOnlineArgs { Channel = channel.Channel, CheckIntervalSeconds = CheckIntervalSeconds });
                }
                else if (!current && _statuses[channel])
                {
                    OnChannelOffline?.Invoke(this,
                        new OnChannelOfflineArgs { Channel = channel.Channel, CheckIntervalSeconds = CheckIntervalSeconds });
                }
                _statuses[channel] = current;
            }
            return;
        }

        #region EVENTS
        /// <summary>Event fires when channel goes online</summary>
        public event EventHandler<OnChannelOnlineArgs> OnChannelOnline;
        /// <summary>Event fires when channel goes online</summary>
        public event EventHandler<OnChannelOfflineArgs> OnChannelOffline;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnChannelMonitorStartedArgs> OnChannelMonitorStarted;
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnChannelMonitorEndedArgs> OnChannelMonitorEnded;
        #endregion
    }
}
