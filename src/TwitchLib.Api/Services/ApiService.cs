using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core;
using TwitchLib.Api.Services.Events;

namespace TwitchLib.Api.Services
{
    public class ApiService
    {
        protected readonly ITwitchAPI _api;

        private readonly ServiceTimer _serviceTimer;

        /// <summary>
        /// The list with channels to monitor.
        /// </summary>
        public List<string> ChannelsToMonitor { get; private set; }
        /// <summary>
        /// How often the service is being updated in seconds.
        /// </summary>
        public int IntervalInSeconds => _serviceTimer.IntervalInSeconds;
        /// <summary>
        /// Whether the service is currently enabled or not.
        /// </summary>
        public bool Enabled => _serviceTimer.Enabled;

        /// <summary>
        /// Event invoked when the service has started.
        /// </summary>
        public event EventHandler<OnServiceStartedArgs> OnServiceStarted;
        /// <summary>
        /// Event invoked when the service has stopped.
        /// </summary>
        public event EventHandler<OnServiceStoppedArgs> OnServiceStopped;
        /// <summary>
        /// Event invoked when the service is updating.
        /// </summary>
        public event EventHandler<OnServiceTickArgs> OnServiceTick;
        /// <summary>
        /// Event invoked when the channels have been set.
        /// </summary>
        public event EventHandler<OnChannelsSetArgs> OnChannelsSet;

        /// <summary>
        /// Constructor from the ApiService.
        /// </summary>
        /// <exception cref="ArgumentNullException">When the <paramref name="api"/> is null.</exception>
        /// <exception cref="ArgumentException">When the <paramref name="checkIntervalInSeconds"/> is lower than one second.</exception> 
        /// <param name="api">The api used to query information.</param>
        /// <param name="checkIntervalInSeconds"></param>
        protected ApiService(ITwitchAPI api, int checkIntervalInSeconds)
        {
            if (api == null)
                throw new ArgumentNullException(nameof(api));

            if (checkIntervalInSeconds < 1)
                throw new ArgumentException("The interval must be 1 second or more.", nameof(checkIntervalInSeconds));

            _api = api;
            _serviceTimer = new ServiceTimer(OnServiceTimerTick, checkIntervalInSeconds);
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <exception cref="InvalidOperationException">When no channels have been added to the service</exception> 
        /// <exception cref="InvalidOperationException">When the service has already been started.</exception>
        public virtual void Start()
        {
            if (ChannelsToMonitor == null)
                throw new InvalidOperationException("You must atleast add 1 channel to service before starting it.");

            if (_serviceTimer.Enabled)
                throw new InvalidOperationException("The service has already been started.");

            _serviceTimer.Start();

            OnServiceStarted?.Invoke(this, new OnServiceStartedArgs());
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the service hasn't started yet, or has already been stopped.</exception>
        public virtual void Stop()
        {
            if (!_serviceTimer.Enabled)
                throw new InvalidOperationException("The service hasn't started yet, or has already been stopped.");

            _serviceTimer.Stop();

            OnServiceStopped?.Invoke(this, new OnServiceStoppedArgs());
        }

        /// <summary>
        /// Sets channels by to monitor.
        /// </summary>
        /// <exception cref="ArgumentNullException">When <paramref name="channelsToMonitor"/> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="channelsToMonitor"/> is empty.</exception>
        /// <param name="channelsToMonitor">The channels to monitor.</param>
        protected virtual void SetChannels(List<string> channelsToMonitor)
        {
            if (channelsToMonitor == null)
                throw new ArgumentNullException(nameof(channelsToMonitor));

            if (channelsToMonitor.Count == 0)
                throw new ArgumentException("The provided list is empty.", nameof(channelsToMonitor));

            ChannelsToMonitor = channelsToMonitor;

            OnChannelsSet?.Invoke(this, new OnChannelsSetArgs {Channels = channelsToMonitor});
        }
        
        /// <summary>
        /// Called when the service timer ticks. Invokes the <see cref="OnServiceTick"/> event.
        /// </summary>
        protected virtual Task OnServiceTimerTick()
        {
            OnServiceTick?.Invoke(this, new OnServiceTickArgs());
            return Task.CompletedTask;
        }
    }
}
