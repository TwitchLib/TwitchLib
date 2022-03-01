using Moq;
using System.Collections.Generic;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Test.Unit.Helix
{
    public static class HelixSetup
    {
        private static readonly string GetRootScopesResponse = "{\"token\":{\"authorization\":{\"created_at\":\"2016-12-14T15:51:16Z\",\"scopes\":[\"analytics:read:extensions\",\"analytics:read:games\",\"bits:read\",\"clips:edit\",\"user:edit\",\"user:edit:broadcast\",\"user:read:broadcast\",\"user:read:email\",\"channel_check_subscription\",\"channel_commercial\",\"channel_editor\",\"channel_feed_edit\",\"channel_feed_read\",\"channel_read\",\"channel_stream\",\"channel_subscriptions\",\"chat_login\",\"collections_edit\",\"communities_edit\",\"communities_moderate\",\"openid\",\"user_blocks_edit\",\"user_blocks_read\",\"user_follows_edit\",\"user_read\",\"user_subscriptions\",\"viewing_activity_read\"],\"updated_at\":\"2016-12-14T15:51:16Z\"},\"client_id\":\"uo6dggojyb8d6soh92zknwmi5ej1q2\",\"user_id\":\"44322889\",\"user_name\":\"dallas\",\"valid\":true}}";

        private static readonly string V5Root = "https://api.twitch.tv/kraken";
        public static Mock<IHttpCallHandler> GetMockHttpCallHandler(string response, string scopesResponse = null)
        {
            var mockHandler = new Mock<IHttpCallHandler>();
            mockHandler
                .Setup(x => x.GeneralRequest(It.Is<string>(y => y == V5Root), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Core.Enums.ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new KeyValuePair<int, string>(200, GetRootScopesResponse));
            mockHandler
                .Setup(x => x.GeneralRequest(It.IsNotIn(V5Root), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Core.Enums.ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new KeyValuePair<int, string>(200, response));

            return mockHandler;
        }
    }
}
