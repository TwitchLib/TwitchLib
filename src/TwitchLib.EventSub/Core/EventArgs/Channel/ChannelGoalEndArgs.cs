using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Core.EventArgs.Channel
{
    public class ChannelGoalEndArgs : TwitchLibEventSubEventArgs<EventSubNotification<ChannelGoalEnd>>, IEventSubEventArgs<ChannelGoalEnd>
    { }
}