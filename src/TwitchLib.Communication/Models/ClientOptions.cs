using System;
using System.Collections.Generic;
using TwitchLib.Communication.Enums;
using TwitchLib.Communication.Interfaces;

namespace TwitchLib.Communication.Models
{
    public class ClientOptions : IClientOptions
    {
        public int SendQueueCapacity { get; set; } = 10000;
        public TimeSpan SendCacheItemTimeout { get; set; } = TimeSpan.FromMinutes(30);
        public ushort SendDelay { get; set; } = 50;
        public ReconnectionPolicy ReconnectionPolicy { get; set; } = new ReconnectionPolicy(3000, maxAttempts: 10);
        public bool UseSsl { get; set; } = true;
        public int DisconnectWait { get; set; } = 20000;
        public ClientType ClientType { get; set; } = ClientType.Chat;
        public TimeSpan ThrottlingPeriod { get; set; } = TimeSpan.FromSeconds(30);
        public int MessagesAllowedInPeriod { get; set; } = 100;
        public TimeSpan WhisperThrottlingPeriod { get; set; } = TimeSpan.FromSeconds(60);
        public int WhispersAllowedInPeriod { get; set; } = 100;
        public int WhisperQueueCapacity { get; set; } = 10000;
    }
}
