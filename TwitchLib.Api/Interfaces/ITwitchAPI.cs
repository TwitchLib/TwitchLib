

namespace TwitchLib.Api
{
    public interface ITwitchAPI
    {
        Auth Auth { get; }
        Badges Badges { get; }
        Bits Bits { get; }
        Blocks Blocks { get; }
        ChannelFeeds ChannelFeeds { get; }
        Channels Channels { get; }
        Chat Chat { get; }
        Clips Clips { get; }
        Collections Collections { get; }
        Communities Communities { get; }
        Debugging Debugging { get; }
        Follows Follows { get; }
        Games Games { get; }
        Ingests Ingests { get; }
        Root Root { get; }
        Search Search { get; }
        IApiSettings Settings { get; }
        Streams Streams { get; }
        Subscriptions Subscriptions { get; }
        Teams Teams { get; }
        ThirdParty ThirdParty { get; }
        Undocumented Undocumented { get; }
        Users Users { get; }
        Videos Videos { get; }
        Webhooks Webhooks { get; }
    }
}