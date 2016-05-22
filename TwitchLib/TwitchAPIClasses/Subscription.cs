using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPIClasses
{
    public class Subscription
    {
        private bool _staff;
        private long _id;
        private string _logo, _name, _accountCreatedAt, _accountUpdatedAt, _displayName, _subscriptionCreatedAt;

        public long Id => _id;
        public string AccountCreatedAt => _accountCreatedAt;
        public string AccountUpdatedAt => _accountUpdatedAt;
        public string DisplayName => _displayName;
        public string Logo => _logo;
        public string Name => _name;
        public string SubscriptionCreatedAt => _subscriptionCreatedAt;
    }
}