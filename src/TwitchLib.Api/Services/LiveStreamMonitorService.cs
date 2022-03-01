using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core.LiveStreamMonitor;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace TwitchLib.Api.Services
{
    public class LiveStreamMonitorService : ApiService
    {
        private CoreMonitor _monitor;
        private IdBasedMonitor _idBasedMonitor;
        private NameBasedMonitor _nameBasedMonitor;

        /// <summary>
        /// A cache with streams that are currently live.
        /// </summary>
        public Dictionary<string, Stream> LiveStreams { get; } = new Dictionary<string, Stream>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// The maximum amount of streams to collect per request.
        /// </summary>
        public int MaxStreamRequestCountPerRequest { get; }

        private IdBasedMonitor IdBasedMonitor => _idBasedMonitor ?? (_idBasedMonitor = new IdBasedMonitor(_api));
        private NameBasedMonitor NameBasedMonitor => _nameBasedMonitor ?? (_nameBasedMonitor = new NameBasedMonitor(_api));

        /// <summary>
        /// Invoked when a monitored stream went online.
        /// </summary>
        public event EventHandler<OnStreamOnlineArgs> OnStreamOnline;
        /// <summary>
        /// Invoked when a monitored stream went offline.
        /// </summary>
        public event EventHandler<OnStreamOfflineArgs> OnStreamOffline;
        /// <summary>
        /// Invoked when a monitored stream was already online, but is updated with it's latest information (might be the same).
        /// </summary>
        public event EventHandler<OnStreamUpdateArgs> OnStreamUpdate;

        /// <summary>
        /// The constructor from the LiveStreamMonitorService
        /// </summary>
        /// <exception cref="ArgumentNullException">When the <paramref name="api"/> is null.</exception>
        /// <exception cref="ArgumentException">When the <paramref name="checkIntervalInSeconds"/> is lower than one second.</exception> 
        /// <exception cref="ArgumentException">When the <paramref name="maxStreamRequestCountPerRequest"/> is less than 1 or more than 100.</exception> 
        /// <param name="api">The api used to query information.</param>
        /// <param name="checkIntervalInSeconds"></param>
        /// <param name="maxStreamRequestCountPerRequest"></param>
        public LiveStreamMonitorService(ITwitchAPI api, int checkIntervalInSeconds = 60, int maxStreamRequestCountPerRequest = 100) : 
            base (api, checkIntervalInSeconds)
        {
            if (maxStreamRequestCountPerRequest < 1 || maxStreamRequestCountPerRequest > 100)
                throw new ArgumentException("Twitch doesn't support less than 1 or more than 100 streams per request.", nameof(maxStreamRequestCountPerRequest));

            MaxStreamRequestCountPerRequest = maxStreamRequestCountPerRequest;
        }

        public void ClearCache()
        {
            LiveStreams.Clear();

            _nameBasedMonitor?.ClearCache();

            _nameBasedMonitor = null;
            _idBasedMonitor = null;
        }

        public void SetChannelsById(List<string> channelsToMonitor)
        {
            SetChannels(channelsToMonitor);

            _monitor = IdBasedMonitor;
        }

        public void SetChannelsByName(List<string> channelsToMonitor)
        {
            SetChannels(channelsToMonitor);

            _monitor = NameBasedMonitor;
        }

        public async Task UpdateLiveStreamersAsync(bool callEvents = true)
        {
            var result = await GetLiveStreamersAsync();

            foreach (var channel in ChannelsToMonitor)
            {
                var liveStream = result.FirstOrDefault(await _monitor.CompareStream(channel));

                if (liveStream != null)
                {
                    HandleLiveStreamUpdate(channel, liveStream, callEvents);
                }
                else
                {
                    HandleOfflineStreamUpdate(channel, callEvents);
                }
            }
        }

        protected override async Task OnServiceTimerTick()
        {
            try {
                await base.OnServiceTimerTick();
                await UpdateLiveStreamersAsync();
            } catch {}
        }

        private void HandleLiveStreamUpdate(string channel, Stream liveStream, bool callEvents)
        {
            var wasAlreadyLive = LiveStreams.ContainsKey(channel);

            LiveStreams[channel] = liveStream;

            if (!callEvents)
                return;

            if (!wasAlreadyLive)
            {
                OnStreamOnline?.Invoke(this, new OnStreamOnlineArgs { Channel = channel, Stream = liveStream });
            }
            else
            {
                OnStreamUpdate?.Invoke(this, new OnStreamUpdateArgs { Channel = channel, Stream = liveStream });
            }
        }

        private void HandleOfflineStreamUpdate(string channel, bool callEvents)
        {
            var wasAlreadyLive = LiveStreams.TryGetValue(channel, out var cachedLiveStream);

            if (!wasAlreadyLive)
                return;

            LiveStreams.Remove(channel);

            if (!callEvents)
                return;

            OnStreamOffline?.Invoke(this, new OnStreamOfflineArgs { Channel = channel, Stream = cachedLiveStream });
        }

        private async Task<List<Stream>> GetLiveStreamersAsync()
        {
            var livestreamers = new List<Stream>();
            var pages = Math.Ceiling((double)ChannelsToMonitor.Count / MaxStreamRequestCountPerRequest);

            for (var i = 0; i < pages; i++)
            {
                var selectedSet = ChannelsToMonitor.Skip(i * MaxStreamRequestCountPerRequest).Take(MaxStreamRequestCountPerRequest).ToList();
                var resultset = await _monitor.GetStreamsAsync(selectedSet);

                if (resultset.Streams == null)
                    continue;

                livestreamers.AddRange(resultset.Streams);
            }
            return livestreamers;
        }
    }
}
