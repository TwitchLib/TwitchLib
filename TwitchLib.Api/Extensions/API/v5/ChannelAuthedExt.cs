namespace TwitchLib.Api.Extensions.API.v5
{
    public static class ChannelAuthedExt
    {
        public static Models.v5.Channels.Channel ToChannel(this Models.v5.Channels.ChannelAuthed channel)
        {
            return new Models.v5.Channels.Channel
            {
                Id = channel.Id,
                BroadcasterLanguage = channel.BroadcasterLanguage,
                CreatedAt = channel.CreatedAt,
                DisplayName = channel.DisplayName,
                Followers = channel.Followers,
                Game = channel.Game,
                Language = channel.Language,
                Logo = channel.Logo,
                Mature = channel.Mature,
                Name = channel.Name,
                Partner = channel.Partner,
                ProfileBanner = channel.ProfileBanner,
                ProfileBannerBackgroundColor = channel.ProfileBannerBackgroundColor,
                Status = channel.Status,
                UpdatedAt = channel.UpdatedAt,
                Url = channel.Url,
                VideoBanner = channel.VideoBanner,
                Views = channel.Views
            };
        }
    }
}
