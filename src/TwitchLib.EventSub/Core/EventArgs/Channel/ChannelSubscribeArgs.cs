using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Core.EventArgs.Channel
{
    public class ChannelSubscribeArgs : TwitchLibEventSubEventArgs<EventSubNotification<ChannelSubscribe>>, IEventSubEventArgs<ChannelSubscribe>
    { }
}