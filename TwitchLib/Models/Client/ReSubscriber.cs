namespace TwitchLib.Models.Client
{
    public class ReSubscriber : SubscriberBase
    {
        public int Months { get; protected set; }

        public ReSubscriber(string ircString) : base(ircString) {
            Months = months;
        }
    }
}
