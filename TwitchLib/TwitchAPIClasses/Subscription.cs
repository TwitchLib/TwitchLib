using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPIClasses
{
    class Subscription
    {
        private int id;
        private string logo, name, account_created_at, account_updated_at, display_name, subscription_created_at;
        private bool staff;
        
        public int ID { get { return id; } }
        public string Logo { get { return logo; } }
        public string Name { get { return name; } }
        public string AccountCreatedAt { get { return account_created_at; } }
        public string AccountUpdatedAt { get { return account_updated_at; } }
        public string DisplayName { get { return display_name; } }
        public string SubscriptionCreatedAt { get { return subscription_created_at; } }
    }
}
