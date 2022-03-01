using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Websockets.Core.EventArgs.Channel
{
    public class ChannelHypeTrainProgressArgs : TwitchLibEventSubEventArgs<EventSubNotification<HypeTrainProgress>>
    { }
}