using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TwitchLib.Exceptions.Services;
using TwitchLib.Exceptions.API;
using TwitchLib.Events.Services.LiveStreamMonitor;
using TwitchLib.Models.Client;

namespace TwitchLib.Services
{
    /// <summary>Service that allows customizability and subscribing to detection of channels going online/offline.</summary>
    public class LiveStreamMonitor
    {
        #region Private Variables
        private string _clientId;
        private int _checkIntervalSeconds;
        private List<string> _channels;
        private Dictionary<string, bool> _statuses;
        private Timer _streamMonitorTimer = new Timer();
        #endregion
        #region Public Variables
        /// <summary>Property representing Twitch channels service is monitoring.</summary>
        public List<string> Channels { get { return _channels; } protected set { _channels = value; } }
        /// <summary>Property representing application client Id, also updates it in TwitchApi.</summary>
        public string ClientId { get { return _clientId; } set { _clientId = value; TwitchApi.SetClientId(value); } }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get { return _checkIntervalSeconds; } set { _checkIntervalSeconds = value; _streamMonitorTimer.Interval = value * 1000; } }
        #endregion
        #region EVENTS
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamOnlineArgs> OnStreamOnline;
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamOfflineArgs> OnStreamOffline;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnStreamMonitorStartedArgs> OnStreamMonitorStarted;
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnStreamMonitorEndedArgs> OnStreamMonitorEnded;
        #endregion
        /// <summary>Service constructor.</summary>
        /// <exception cref="BadResourceException">If channel is invalid, an InvalidChannelException will be thrown.</exception>
        /// <param name="channels">Represents a list of channels to monitor</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
        public LiveStreamMonitor(List<string> channels, int checkIntervalSeconds = 60, string clientId = "")
        {
            Channels = channels;
            CheckIntervalSeconds = checkIntervalSeconds;
            _streamMonitorTimer.Elapsed += _streamMonitorTimerElapsed;
            if (clientId != "")
                ClientId = clientId;
        }

        #region CONTROLS
        /// <summary>Downloads recent followers from Twitch, starts service, fires OnServiceStarted event.</summary>
        public async void StartService()
        {
            foreach (var channel in Channels)
            {
                _statuses.Add(channel, await TwitchApi.Streams.BroadcasterOnlineAsync(channel));
            }
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
        #endregion

        private async void _streamMonitorTimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var channel in Channels)
            {
                bool current = await TwitchApi.Streams.BroadcasterOnlineAsync(channel);
                if (current && !_statuses[channel])
                {
                    OnStreamOnline?.Invoke(this,
                        new OnStreamOnlineArgs { Channel = channel, CheckIntervalSeconds = CheckIntervalSeconds });
                }
                else if (!current && _statuses[channel])
                {
                    OnStreamOffline?.Invoke(this,
                        new OnStreamOfflineArgs { Channel = channel, CheckIntervalSeconds = CheckIntervalSeconds });
                }
                _statuses[channel] = current;
            }
            return;
        }

    
    }
}
