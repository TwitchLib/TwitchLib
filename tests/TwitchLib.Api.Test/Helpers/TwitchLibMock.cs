using Moq;
using System;
using System.Collections.Generic;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Test.Helpers
{
    public class TwitchLibMock
    {
        public static TwitchAPI TwitchApi(params (string url, string response)[] urlResponses)
        {
            return TwitchApi(HttpCallHandler(urlResponses));
        }

        public static TwitchAPI TwitchApi(Mock<IHttpCallHandler> mockHandler)
        {
            var api = new TwitchAPI(http: mockHandler.Object);
            api.Settings.ClientId = new Guid().ToString();
            return api;
        }

        public static void ResetHttpCallHandlerResponses(Mock<IHttpCallHandler> mockHandler, params (string url, string response)[] urlResponses)
        {
            mockHandler.Reset();

            foreach (var (url, response) in urlResponses)
            {
                mockHandler
                    .Setup(x => x.GeneralRequest(It.Is<string>(y => new Uri(y).GetLeftPart(UriPartial.Path) == url), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Core.Enums.ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(new KeyValuePair<int, string>(200, response));
            }
        }

        public static Mock<IHttpCallHandler> HttpCallHandler(params (string url, string response)[] urlResponses)
        {
            var mockHandler = new Mock<IHttpCallHandler>();

            foreach (var (url, response) in urlResponses)
            {
                mockHandler
                    .Setup(x => x.GeneralRequest(It.Is<string>(y => new Uri(y).GetLeftPart(UriPartial.Path) == url), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Core.Enums.ApiVersion>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(new KeyValuePair<int, string>(200, response));
            }
            
            return mockHandler;
        }
    }
}