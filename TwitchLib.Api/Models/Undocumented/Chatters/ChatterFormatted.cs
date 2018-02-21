
namespace TwitchLib.Api.Models.Undocumented.Chatters
{
    public class ChatterFormatted
    {
        public string Username { get; protected set; }
        public Enums.UserType UserType { get; internal set; }

        public ChatterFormatted(string username, Enums.UserType userType)
        {
            Username = username;
            UserType = userType;
        }
    }
}
