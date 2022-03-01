using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Stream;

namespace TwitchLib.EventSub.Websockets.Core.EventArgs.Stream
{
    public class StreamOfflineArgs : TwitchLibEventSubEventArgs<EventSubNotification<StreamOffline>>
    { }
}