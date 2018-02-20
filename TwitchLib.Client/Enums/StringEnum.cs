namespace TwitchLib.Client.Enums
{
    ///<summary>Really cool way of doing string enums, credits to RogueException (https://github.com/RogueException)</summary>
    public abstract class StringEnum
    {
        /// <summary>Value of enum</summary>
        public string Value { get; }

        /// <summary>StringEnum constructor.</summary>
        protected StringEnum(string value)
        {
            Value = value;
        }

        /// <summary>Returns string value for overriden ToString()</summary>
        /// <returns>Enum value</returns>
        public override string ToString() => Value;
    }
}
