using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitchLib.Models.Client;

namespace TwitchLib.Tests
{
    [TestClass()]
    public class TwitchClientTests
    {
        private readonly ConnectionCredentials TestCredentials = new ConnectionCredentials("test", "oauth:1");

        [TestMethod()]
        public void TwitchClient_InitializeWithoutChannel()
        {
            var client = new TwitchClient(TestCredentials);

            Assert.IsNotNull(client, "Expected client to be initialized");
        }
    }
}