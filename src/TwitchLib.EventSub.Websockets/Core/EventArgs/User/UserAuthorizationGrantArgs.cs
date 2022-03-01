using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.User;

namespace TwitchLib.EventSub.Websockets.Core.EventArgs.User
{
    public class UserAuthorizationGrantArgs : TwitchLibEventSubEventArgs<EventSubNotification<UserAuthorizationGrant>>
    { }
}