﻿using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.User;

namespace TwitchLib.EventSub.Core.EventArgs.User
{
    public class UserUpdateArgs : TwitchLibEventSubEventArgs<EventSubNotification<UserUpdate>>
    { }
}