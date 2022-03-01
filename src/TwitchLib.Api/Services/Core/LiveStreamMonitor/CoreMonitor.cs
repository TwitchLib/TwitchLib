using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
    internal abstract class CoreMonitor
    {
        protected readonly ITwitchAPI _api;

        public abstract Task<GetStreamsResponse> GetStreamsAsync(List<string> channels);
        public abstract Task<Func<Stream, bool>> CompareStream(string channel);

        protected CoreMonitor(ITwitchAPI api)
        {
            _api = api;
        }
    }
}