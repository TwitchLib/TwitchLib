using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Services;
using TwitchLib.Api.Test.Helpers;
using Xunit;

namespace TwitchLib.Api.Test.Services
{
    public class FollowerServiceTests
    {
        public class Functionality
        {
            private TwitchAPI _api = new TwitchAPI();
            private FollowerService _followerService;

            [Fact]
            public async void KnownFollowers_Contains_UserId_When_ServiceUpdated()
            {
                var usersFollowsResponseJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>(u => u.FromUserId == "UserId" && u.ToUserId == "Id")
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseJson)
                );

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());

                await _followerService.UpdateLatestFollowersAsync();

                Assert.NotNull(_followerService.KnownFollowers[string.Empty].FirstOrDefault(f => f.FromUserId == "UserId"));

                //Same check for SetChannelsByName
                var usersResponseJson = JMock.Of<GetUsersResponse>(o =>
                    o.Users == new[]
                    {
                    Mock.Of<User>(u => u.Id == "Id" && u.DisplayName == "DisplayName"),
                    });

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/users", usersResponseJson),
                    ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseJson)
                );

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsByName(Utils.CreateListWithEmptyString());

                await _followerService.UpdateLatestFollowersAsync();

                Assert.NotNull(_followerService.KnownFollowers[string.Empty].FirstOrDefault(f => f.FromUserId == "UserId"));
            }

            [Fact]
            public void OnServiceStarted_Raised_When_ServiceStarted()
            {
                var eventRaised = false;

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnServiceStarted += (sender, e) => eventRaised = true;
                _followerService.Start();

                Assert.True(eventRaised);
            }

            [Fact]
            public void OnServiceStopped_Raised_When_ServiceStopped()
            {
                var eventRaised = false;

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnServiceStopped += (sender, e) => eventRaised = true;
                _followerService.Start();
                _followerService.Stop();

                Assert.True(eventRaised);
            }

            [Fact]
            public void OnChannelsSet_Raised_When_ChannelsSet()
            {
                var eventRaised = false;

                _followerService = new FollowerService(_api);
                _followerService.OnChannelsSet += (sender, e) => eventRaised = true;
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());

                Assert.True(eventRaised);
            }

            [Fact]
            public void OnServiceTick_Raised_When_ServiceTicked()
            {
                var usersFollowsResponseJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>()
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseJson)
                );

                var signalEvent = new ManualResetEvent(false);

                _followerService = new FollowerService(_api, checkIntervalInSeconds: 1);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnServiceTick += (sender, e) => signalEvent.Set();
                _followerService.Start();

                Assert.True(signalEvent.WaitOne(1500));
            }

            [Fact]
            public async void OnNewFollowersDetected_Raised_When_LatestFollowersUpdated()
            {
                var usersFollowsResponseJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>()
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseJson)
                );

                var eventExcecuted = false;

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnNewFollowersDetected += (sender, e) => eventExcecuted = true;

                await _followerService.UpdateLatestFollowersAsync();

                Assert.True(eventExcecuted);
            }

            [Fact]
            public async void OnNewFollowersDetected_NotRaised_When_NoNewFollowers()
            {
                var usersFollowsResponseJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>(f => f.FromUserId == "FromUserId")
                    }
                );

                _api = TwitchLibMock.TwitchApi(
                    ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseJson)
                );

                var eventExcecutCount = 0;

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnNewFollowersDetected += (sender, e) => eventExcecutCount++;

                await _followerService.UpdateLatestFollowersAsync();
                await _followerService.UpdateLatestFollowersAsync();

                Assert.Equal(1, eventExcecutCount);
            }

            [Fact]
            public async void OnNewFollowersDetected_Raised_When_NewFollower()
            {
                var usersFollowsResponseFirstUserJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>(f => f.FromUserId == "FromFirstUserId")
                    }
                );

                var mockHandler = TwitchLibMock.HttpCallHandler(("https://api.twitch.tv/helix/users/follows", usersFollowsResponseFirstUserJson));

                _api = TwitchLibMock.TwitchApi(mockHandler);

                var eventExecuteCount = 0;

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnNewFollowersDetected += (sender, e) => eventExecuteCount++;

                await _followerService.UpdateLatestFollowersAsync();

                var usersFollowsResponseSecondUserJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>(f => f.FromUserId == "FromSecondUserId")
                    }
                );

                TwitchLibMock.ResetHttpCallHandlerResponses(mockHandler, ("https://api.twitch.tv/helix/users/follows", usersFollowsResponseSecondUserJson));

                await _followerService.UpdateLatestFollowersAsync();

                Assert.Equal(2, eventExecuteCount);
            }

            [Fact]
            public async void OnNewFollowersDetected_NotRaised_When_NewFollowerOlderThanLatest()
            {
                var usersFollowsResponseFirstUserJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>(f => f.FromUserId == "FromFirstUserId" && f.FollowedAt == new DateTime(1))
                    }
                );

                var mockHandler = new Mock<IHttpCallHandler>();

                mockHandler
                    .Setup(x => x.GeneralRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Core.Enums.ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(new KeyValuePair<int, string>(200, usersFollowsResponseFirstUserJson));

                _api = TwitchLibMock.TwitchApi(mockHandler);

                var eventExecuteCount = 0;

                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.OnNewFollowersDetected += (sender, e) => eventExecuteCount++;

                await _followerService.UpdateLatestFollowersAsync();

                var usersFollowsResponseSecondUserJson = JMock.Of<GetUsersFollowsResponse>(o =>
                    o.Follows == new[]
                    {
                    Mock.Of<Follow>(f => f.FromUserId == "FromSecondUserId" && f.FollowedAt == new DateTime(0))
                    }
                );

                mockHandler.Reset();

                mockHandler
                    .Setup(x => x.GeneralRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Core.Enums.ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(new KeyValuePair<int, string>(200, usersFollowsResponseSecondUserJson));

                await _followerService.UpdateLatestFollowersAsync();

                Assert.Equal(1, eventExecuteCount);
            }
        }

        public class Exceptions
        {
            private const string ChannelsNotSetExceptionMessage = "You must atleast add 1 channel to service before starting it.";
            private const string ChannelListEmptyExceptionMessage = "The provided list is empty.";
            private const string AlreadyStartedExceptionMessage = "The service has already been started.";
            private const string AlreadyStoppedExceptionMessage = "The service hasn't started yet, or has already been stopped.";
            private const string InvalidQueryCountExceptionMessage = "Twitch doesn't support less than 1 or more than 100 followers per request.";
            private const string CheckIntervalLowerThan1ExceptionMessage = "The interval must be 1 second or more.";
            private const string CacheSizeLessThanQueryCountExceptionMessage = "The cache size must be at least the size of the .* parameter.";

            private TwitchAPI _api = new TwitchAPI();
            private FollowerService _followerService;

            private int _checkInterval = 60;
            private int _queryCount = 100;
            private int _cacheSize = 1000;

            [Fact]
            public void Ctor_Throws_ArgumentException_When_CheckIntervalLowerThan1()
            {
                _checkInterval = 0;

                AssertException.Throws<ArgumentException>(CheckIntervalLowerThan1ExceptionMessage, () => new FollowerService(_api, _checkInterval, _queryCount, _cacheSize));
            }

            [Fact]
            public void Ctor_Throws_ArgumentException_When_QueryCountEquals0()
            {
                _queryCount = 0;

                AssertException.Throws<ArgumentException>(InvalidQueryCountExceptionMessage, () => new FollowerService(_api, _checkInterval, _queryCount, _cacheSize));
            }

            [Fact]
            public void Ctor_Throws_ArgumentException_When_QueryCountLargerThan100()
            {
                _queryCount = 101;

                AssertException.Throws<ArgumentException>(InvalidQueryCountExceptionMessage, () => new FollowerService(_api, _checkInterval, _queryCount, _cacheSize));
            }

            [Fact]
            public void Ctor_Throws_ArgumentException_When_CacheSizeLessThanQueryCount()
            {
                _cacheSize = _queryCount - 1;

                AssertException.Throws<ArgumentException>(CacheSizeLessThanQueryCountExceptionMessage, () => new FollowerService(_api, _checkInterval, _queryCount, _cacheSize));
            }
            [Fact]
            public void Start_Throws_InvalidOperationException_When_ChannelsNotSet()
            {
                _followerService = new FollowerService(_api);

                AssertException.Throws<InvalidOperationException>(ChannelsNotSetExceptionMessage, () => _followerService.Start());
            }

            [Fact]
            public void SetChannelsById_Throws_ArgumentNullException_When_ChannelsArgumentNull()
            {
                _followerService = new FollowerService(_api);

                AssertException.Throws<ArgumentNullException>(() => _followerService.SetChannelsById(null));
            }

            [Fact]
            public void SetChannelsByName_Throws_ArgumentNullException_When_ChannelsArgumentNull()
            {
                _followerService = new FollowerService(_api);

                AssertException.Throws<ArgumentNullException>(() => _followerService.SetChannelsByName(null));
            }

            [Fact]
            public void SetChannelsById_Throws_ArgumentException_When_ChannelsArgumentEmpty()
            {
                _followerService = new FollowerService(_api);

                AssertException.Throws<ArgumentException>(ChannelListEmptyExceptionMessage, () => _followerService.SetChannelsById(new List<string>()));
            }

            [Fact]
            public void SetChannelsByName_Throws_ArgumentException_When_ChannelsArgumentEmpty()
            {
                _followerService = new FollowerService(_api);

                AssertException.Throws<ArgumentException>(ChannelListEmptyExceptionMessage, () => _followerService.SetChannelsByName(new List<string>()));
            }

            [Fact]
            public void Start_Throws_InvalidOperationException_When_ServiceAlreadyStarted()
            {
                _followerService = new FollowerService(_api);
                _followerService.SetChannelsById(Utils.CreateListWithEmptyString());
                _followerService.Start();

                AssertException.Throws<InvalidOperationException>(AlreadyStartedExceptionMessage, () => _followerService.Start());
            }

            [Fact]
            public void Stop_Throws_InvalidOperationException_When_ServiceAlreadyStopped()
            {
                _followerService = new FollowerService(_api);

                AssertException.Throws<InvalidOperationException>(AlreadyStoppedExceptionMessage, () => _followerService.Stop());
            }
        }
    }
}