namespace TwitchLib
{
    public class Subscriber
    {
        public string Channel { get; }

        public string Name { get; }

        public int Months { get; }

        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy just subscribed!
        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy subscribed for 9 months in a row!
        //3 viewers resubscribed while you were away!
        public Subscriber(string ircString)
        {
            Channel = ircString.Split('#')[1].Split(' ')[0];
            Name = ircString.Split(':')[2].Split(' ')[0];
            if (ircString.Split(' ').Length == 5 || ircString.Contains("just subscribed!"))
            {
                Months = 0;
            }
            else
            {
                if (!ircString.Contains("while you were away!"))
                    Months = int.Parse(ircString.Split(' ')[6]);
            }
        }
    }
}