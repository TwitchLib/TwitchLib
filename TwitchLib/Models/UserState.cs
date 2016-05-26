namespace TwitchLib
{
    public class UserState
    {
        public enum UType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        // Reversing issue noticed by SimpleVar
        public string ColorHex { get; }

        public string DisplayName { get; }

        public string EmoteSet { get; }

        public string Channel { get; }

        public bool IsSubscriber { get; }

        public bool IsTurbo { get; }

        public UType UserType { get; }

        public UserState(string ircString)
        {
            ColorHex = ircString.Split(';')[0].Contains("#") ? ircString.Split(';')[0].Split('#')[1] : "";
            DisplayName = ircString.Split(';')[1].Split('=')[1];
            EmoteSet = ircString.Split(';')[2].Split('=')[1];
            if (ircString.Split(';')[3].Split('=')[1] == "1")
                IsSubscriber = true;
            if (ircString.Split(';')[4].Split('=')[1] == "1")
                IsTurbo = true;
            switch (ircString.Split('=')[6].Split(' ')[0])
            {
                case "mod":
                    UserType = UType.Moderator;
                    break;

                case "global_mod":
                    UserType = UType.GlobalModerator;
                    break;

                case "admin":
                    UserType = UType.Admin;
                    break;

                case "staff":
                    UserType = UType.Staff;
                    break;

                default:
                    UserType = UType.Viewer;
                    break;
            }
            Channel = ircString.Split(' ')[3].Replace("#", "");
        }
    }
}