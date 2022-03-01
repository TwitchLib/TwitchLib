﻿using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Core.EventArgs.Channel
{
    public class ChannelPredictionLockArgs : TwitchLibEventSubEventArgs<EventSubNotification<ChannelPredictionLock>>, IEventSubEventArgs<ChannelPredictionLock>
    { }
}