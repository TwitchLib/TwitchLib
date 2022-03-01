using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
    internal class IdBasedMonitor : CoreMonitor
    {
        public IdBasedMonitor(ITwitchAPI api) : base(api) { }

        public override Task<Func<Stream, bool>> CompareStream(string channel)
        {
            return Task.FromResult(new Func<Stream, bool>(stream => stream.UserId == channel));
        }

        public override Task<GetStreamsResponse> GetStreamsAsync(List<string> channels)
        {
            return _api.Helix.Streams.GetStreamsAsync(first: channels.Count, userIds: channels);
        }
    }
}
