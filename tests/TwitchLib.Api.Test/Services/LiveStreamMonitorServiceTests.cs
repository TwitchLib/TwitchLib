using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using TwitchLib.Api.Helix.Models.Streams;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Services;
using TwitchLib.Api.Test.Helpers;
using Xunit;

namespace TwitchLib.Api.Test.Services
{
    public class LiveStreamMonitorServiceTests
    {
        public class Functionality
        {
            private TwitchAPI _api = new TwitchAPI();
            private LiveStreamMonitorService _liveStreamMonitor;

            [Fact]
            public async void LiveStreams_Contains_UserId_When_ServiceUpdated()
            {
                var usersFollowsResponseJson = JMock.Of<GetStreamsResponse>(o =>
                    o.Streams == new[]
                    {
                        Mock.Of<Stream>(u => u.UserId == "UserId")
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/streams", usersFollowsResponseJson)
                );

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithStrings("UserId"));

                await _liveStreamMonitor.UpdateLiveStreamersAsync();

                Assert.True(_liveStreamMonitor.LiveStreams.ContainsKey("UserId"));
            }

            [Fact]
            public void OnServiceStarted_Raised_When_ServiceStarted()
            {
                var eventRaised = false;

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithEmptyString());
                _liveStreamMonitor.OnServiceStarted += (sender, e) => eventRaised = true;
                _liveStreamMonitor.Start();

                Assert.True(eventRaised);
            }

            [Fact]
            public void OnServiceStopped_Raised_When_ServiceStopped()
            {
                var eventRaised = false;

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithEmptyString());
                _liveStreamMonitor.OnServiceStopped += (sender, e) => eventRaised = true;
                _liveStreamMonitor.Start();
                _liveStreamMonitor.Stop();

                Assert.True(eventRaised);
            }

            [Fact]
            public void OnChannelsSet_Raised_When_ChannelsSet()
            {
                var eventRaised = false;

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.OnChannelsSet += (sender, e) => eventRaised = true;
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithEmptyString());

                Assert.True(eventRaised);
            }

            [Fact]
            public void OnServiceTick_Raised_When_ServiceTicked()
            {
                var usersFollowsResponseJson = JMock.Of<GetStreamsResponse>(o =>
                    o.Streams == new[]
                    {
                        Mock.Of<Stream>()
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseJson)
                );

                var signalEvent = new ManualResetEvent(false);

                _liveStreamMonitor = new LiveStreamMonitorService(_api, checkIntervalInSeconds: 1);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithEmptyString());
                _liveStreamMonitor.OnServiceTick += (sender, e) => signalEvent.Set();
                _liveStreamMonitor.Start();

                Assert.True(signalEvent.WaitOne(1500));
            }

            [Fact]
            public async void OnStreamOnline_Called_When_StreamWentOnline()
            {
                var usersFollowsResponseJson = JMock.Of<GetStreamsResponse>(o =>
                    o.Streams == new[]
                    {
                        Mock.Of<Stream>(u => u.UserId == "UserId")
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/streams", usersFollowsResponseJson)
                );

                var eventExecuteCount = 0;

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithStrings("UserId"));
                _liveStreamMonitor.OnStreamOnline += (sender, e) => eventExecuteCount++;

                await _liveStreamMonitor.UpdateLiveStreamersAsync();

                Assert.Equal(1, eventExecuteCount);
            }

            [Fact]
            public async void OnStreamOffline_Called_When_StreamWentOffline()
            {
                var usersFollowsResponseJson = JMock.Of<GetStreamsResponse>(o =>
                    o.Streams == new[]
                    {
                        Mock.Of<Stream>(u => u.UserId == "UserId")
                    }
                );

                var mockHandler = TwitchLibMock.HttpCallHandler(("https://api.twitch.tv/helix/streams", usersFollowsResponseJson));

                _api = TwitchLibMock.TwitchApi(mockHandler);

                var eventExecuteCount = 0;

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithStrings("UserId"));
                _liveStreamMonitor.OnStreamOffline += (sender, e) => eventExecuteCount++;

                await _liveStreamMonitor.UpdateLiveStreamersAsync();

                usersFollowsResponseJson = JMock.Of<GetStreamsResponse>(o =>
                    o.Streams == new[]
                    {
                        Mock.Of<Stream>(u => u.UserId == "SomeOtherUserId")
                    }
                );

                TwitchLibMock.ResetHttpCallHandlerResponses(mockHandler, ("https://api.twitch.tv/helix/streams", usersFollowsResponseJson));

                await _liveStreamMonitor.UpdateLiveStreamersAsync();
                
                Assert.Equal(1, eventExecuteCount);
            }

            [Fact]
            public async void OnStreamUpdate_Called_When_StreamAlreadyOnline()
            {
                var usersFollowsResponseJson = JMock.Of<GetStreamsResponse>(o =>
                    o.Streams == new[]
                    {
                        Mock.Of<Stream>(u => u.UserId == "UserId")
                    }
                );

                var mockHandler = TwitchLibMock.HttpCallHandler(("https://api.twitch.tv/helix/streams", usersFollowsResponseJson));

                _api = TwitchLibMock.TwitchApi(mockHandler);

                var eventExecuteCount = 0;

                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithStrings("UserId"));
                _liveStreamMonitor.OnStreamUpdate += (sender, e) => eventExecuteCount++;

                await _liveStreamMonitor.UpdateLiveStreamersAsync();
                await _liveStreamMonitor.UpdateLiveStreamersAsync();

                Assert.Equal(1, eventExecuteCount);
            }
        }

        public class Exceptions
        {
            private const string ChannelsNotSetExceptionMessage = "You must atleast add 1 channel to service before starting it.";
            private const string ChannelListEmptyExceptionMessage = "The provided list is empty.";
            private const string AlreadyStartedExceptionMessage = "The service has already been started.";
            private const string AlreadyStoppedExceptionMessage = "The service hasn't started yet, or has already been stopped.";
            private const string InvalidQueryCountExceptionMessage = "Twitch doesn't support less than 1 or more than 100 streams per request.";
            private const string CheckIntervalLowerThan1ExceptionMessage = "The interval must be 1 second or more.";

            private TwitchAPI _api = new TwitchAPI();
            private LiveStreamMonitorService _liveStreamMonitor;

            private int _checkInterval = 60;
            private int _maxStreamRequestCount = 100;

            [Fact]
            public void Ctor_Throws_ArgumentException_When_CheckIntervalLowerThan1()
            {
                _checkInterval = 0;

                AssertException.Throws<ArgumentException>(CheckIntervalLowerThan1ExceptionMessage, () => new LiveStreamMonitorService(_api, _checkInterval, _maxStreamRequestCount));
            }

            [Fact]
            public void Ctor_Throws_ArgumentException_When_QueryCountEquals0()
            {
                _maxStreamRequestCount = 0;

                AssertException.Throws<ArgumentException>(InvalidQueryCountExceptionMessage, () => new LiveStreamMonitorService(_api, _checkInterval, _maxStreamRequestCount));
            }

            [Fact]
            public void Ctor_Throws_ArgumentException_When_QueryCountLargerThan100()
            {
                _maxStreamRequestCount = 101;

                AssertException.Throws<ArgumentException>(InvalidQueryCountExceptionMessage, () => new LiveStreamMonitorService(_api, _checkInterval, _maxStreamRequestCount));
            }

            [Fact]
            public void Start_Throws_InvalidOperationException_When_ChannelsNotSet()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);

                AssertException.Throws<InvalidOperationException>(ChannelsNotSetExceptionMessage, () => _liveStreamMonitor.Start());
            }

            [Fact]
            public void SetChannelsById_Throws_ArgumentNullException_When_ChannelsArgumentNull()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);

                AssertException.Throws<ArgumentNullException>(() => _liveStreamMonitor.SetChannelsById(null));
            }

            [Fact]
            public void SetChannelsByName_Throws_ArgumentNullException_When_ChannelsArgumentNull()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);

                AssertException.Throws<ArgumentNullException>(() => _liveStreamMonitor.SetChannelsByName(null));
            }

            [Fact]
            public void SetChannelsById_Throws_ArgumentException_When_ChannelsArgumentEmpty()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);

                AssertException.Throws<ArgumentException>(ChannelListEmptyExceptionMessage, () => _liveStreamMonitor.SetChannelsById(new List<string>()));
            }

            [Fact]
            public void SetChannelsByName_Throws_ArgumentException_When_ChannelsArgumentEmpty()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);

                AssertException.Throws<ArgumentException>(ChannelListEmptyExceptionMessage, () => _liveStreamMonitor.SetChannelsByName(new List<string>()));
            }

            [Fact]
            public void Start_Throws_InvalidOperationException_When_ServiceAlreadyStarted()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);
                _liveStreamMonitor.SetChannelsById(Utils.CreateListWithEmptyString());
                _liveStreamMonitor.Start();

                AssertException.Throws<InvalidOperationException>(AlreadyStartedExceptionMessage, () => _liveStreamMonitor.Start());
            }

            [Fact]
            public void Stop_Throws_InvalidOperationException_When_ServiceAlreadyStopped()
            {
                _liveStreamMonitor = new LiveStreamMonitorService(_api);

                AssertException.Throws<InvalidOperationException>(AlreadyStoppedExceptionMessage, () => _liveStreamMonitor.Stop());
            }
        }
    }
}