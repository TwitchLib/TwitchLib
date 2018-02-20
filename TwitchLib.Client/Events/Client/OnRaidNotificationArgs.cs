using System;
using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Events.Client
{
    public class OnRaidNotificationArgs : EventArgs
    {
        public RaidNotification RaidNotificaiton;
    }
}
