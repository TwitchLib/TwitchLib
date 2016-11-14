using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Enums
{
    // Really cool way of doing string enums, credits to RogueException (https://github.com/RogueException)
    public abstract class StringEnum
    {
        public string Value { get; }

        protected StringEnum(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }
}
