using Xunit;

namespace TwitchLib.Api.Test.Unit.Helix
{
    public class HelixClips
    {
        [Fact]
        public async void TestCreateClip()
        {
            var mockHandler = HelixSetup.GetMockHttpCallHandler(CreateClipResponse);
            var api = new TwitchAPI(http: mockHandler.Object);
            var result = await api.Helix.Clips.CreateClipAsync("493057", "RandomTokenThatDoesntMatter");
            Assert.True(result.CreatedClips[0].Id == "FiveWordsForClipSlug");
        }

        [Fact]
        public async void TestGetClips()
        {
            var mockHandler = HelixSetup.GetMockHttpCallHandler(GetClipsResponse);
            var api = new TwitchAPI(http: mockHandler.Object);
            var result = await api.Helix.Clips.GetClipsAsync(new System.Collections.Generic.List<string> { "AwkwardHelplessSalamanderSwiftRage" });
            Assert.True(result.Clips[0].VideoId == "205586603");
        }

        private readonly string CreateClipResponse = "{\"data\":[{\"id\":\"FiveWordsForClipSlug\",\"edit_url\":\"http://clips.twitch.tv/FiveWordsForClipSlug/edit\"}]}";
        private readonly string GetClipsResponse = "{\"data\":[{\"id\":\"AwkwardHelplessSalamanderSwiftRage\",\"url\":\"https://clips.twitch.tv/AwkwardHelplessSalamanderSwiftRage\",\"embed_url\":\"https://clips.twitch.tv/embed?clip=AwkwardHelplessSalamanderSwiftRage\",\"broadcaster_id\":\"67955580\",\"creator_id\":\"53834192\",\"video_id\":\"205586603\",\"game_id\":\"488191\",\"language\":\"en\",\"title\":\"babymetal\",\"view_count\":10,\"created_at\":\"2017-11-30T22:34:18Z\",\"thumbnail_url\":\"https://clips-media-assets.twitch.tv/157589949-preview-480x272.jpg\"}]}";
    }
}
