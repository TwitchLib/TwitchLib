using Xunit;

namespace TwitchLib.Api.Test.Unit.Helix
{
    public class HelixBits
    {
        [Fact]
        public async void TestGetBitsLeaderboards()
        {
            var mockHandler = HelixSetup.GetMockHttpCallHandler(GetGetBitsLeaderboardResponse);
            var api = new TwitchAPI(http: mockHandler.Object);
            var result = await api.Helix.Bits.GetBitsLeaderboardAsync(accessToken: "RandomTokenThatDoesntMatter");

            Assert.True(result.Total == 2);
            Assert.Contains(result.Listings, x => x.UserId == "158010205");
        }

        private readonly string GetGetBitsLeaderboardResponse = "{\"data\":[{\"user_id\":\"158010205\",\"rank\":1,\"score\":12543},{\"user_id\":\"7168163\",\"rank\":2,\"score\":6900}],\"date_range\":{\"started_at\":\"2018-02-05T08:00:00Z\",\"ended_at\":\"2018-02-12T08:00:00Z\"},\"total\":2}";
    }
}
