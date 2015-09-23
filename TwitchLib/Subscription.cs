using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    //Seperate class for subscriptions because there is a really good chance down the road far more information 
    //will be associated with subscription events
    class Subscription
    {
        private string username;
        private int months;

        public Subscription(string _username, int _months)
        {
            username = _username;
            months = _months;
        }

        public string getUsername()
        {
            string retUsername = username;
            return retUsername;
        }

        public int getMonths()
        {
            int retMonths = months;
            return retMonths;
        }
    }
}
