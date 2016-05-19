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
        
        public int Id { get { return _id; } }
        public string Logo { get { return _logo; } }
        public string Name { get { return _name; } }
        public string AccountCreatedAt { get { return _accountCreatedAt; } }
        public string AccountUpdatedAt { get { return _accountUpdatedAt; } }
        public string DisplayName { get { return _displayName; } }
        public string SubscriptionCreatedAt { get { return _subscriptionCreatedAt; } }
    }
}
