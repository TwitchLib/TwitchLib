namespace TwitchLib.Extensions.API.v5
{
    public static class ChannelPrivilegedExt
    {
        public static Models.API.v5.Channels.Channel ToChannel(this Models.API.v5.Channels.ChannelPrivileged channel)
        {
            return new Models.API.v5.Channels.Channel()
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
