using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
    internal class NameBasedMonitor : CoreMonitor
    {
        private readonly ConcurrentDictionary<string, string> _channelToId = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public NameBasedMonitor(ITwitchAPI api) : base(api) { }

        public override async Task<Func<Stream, bool>> CompareStream(string channel)
        {
            if (!_channelToId.TryGetValue(channel, out var channelId))
            {
                channelId = (await _api.Helix.Users.GetUsersAsync(logins: new List<string> { channel })).Users.FirstOrDefault()?.Id;
                _channelToId[channel] = channelId ?? throw new InvalidOperationException($"No channel with the name \"{channel}\" could be found.");
            }

            return stream => stream.UserId == channelId;
        }

        public override Task<GetStreamsResponse> GetStreamsAsync(List<string> channels)
        {
            return _api.Helix.Streams.GetStreamsAsync(first: channels.Count, userLogins: channels);
        }

        public void ClearCache()
        {
            _channelToId.Clear();
        }
    }
}