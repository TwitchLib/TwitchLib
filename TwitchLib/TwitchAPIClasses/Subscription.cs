using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPIClasses
{
    public class Subscription
    {
        private int _id;
        private string _logo, _name, _accountCreatedAt, _accountUpdatedAt, _displayName, _subscriptionCreatedAt;
        private bool _staff;
        
        public int Id => _id;
        public string Logo => _logo;
        public string Name => _name;
        public string AccountCreatedAt => _accountCreatedAt;
        public string AccountUpdatedAt => _accountUpdatedAt;
        public string DisplayName => _displayName;
        public string SubscriptionCreatedAt => _subscriptionCreatedAt;
    }
}
