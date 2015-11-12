using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    class Subscriber
    {
        private string name;
        private int months;

        public string Name { get { return name; } }
        public int Months { get { return months; } }

        public Subscriber(string name, int months)
        {
            this.name = name;
            this.months = months;
        }
    }
}
