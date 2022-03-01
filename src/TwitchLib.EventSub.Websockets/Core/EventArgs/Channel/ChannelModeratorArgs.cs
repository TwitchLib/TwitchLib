using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Websockets.Core.EventArgs.Channel
{
    public class ChannelModeratorArgs : TwitchLibEventSubEventArgs<EventSubNotification<ChannelModerator>>
    { }
}