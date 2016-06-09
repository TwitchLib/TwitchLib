namespace TwitchLib
{
    public class Chatter
    {
        public enum UType
        {
            Viewer,
            Moderator,
            GlobalModerator,
            Admin,
            Staff
        }

        public string Username { get; }

        public UType UserType { get; }

        public Chatter(string username, UType userType)
        {
            Username = username;
            UserType = userType;
        }
    }
}