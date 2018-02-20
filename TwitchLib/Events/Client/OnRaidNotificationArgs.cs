using System;
using TwitchLib.Models.Client;

namespace TwitchLib.Events.Client
{
    public class OnRaidNotificationArgs : EventArgs
    {
        public RaidNotification RaidNotificaiton;
    }
}
