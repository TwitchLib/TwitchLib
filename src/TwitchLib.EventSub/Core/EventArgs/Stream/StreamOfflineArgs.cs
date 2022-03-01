using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Stream;

namespace TwitchLib.EventSub.Core.EventArgs.Stream
{
    public class StreamOfflineArgs : TwitchLibEventSubEventArgs<EventSubNotification<StreamOffline>>, IEventSubEventArgs<StreamOffline>
    { }
}