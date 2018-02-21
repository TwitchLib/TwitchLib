namespace TwitchLib.PubSub.Events
{
    /// <summary>Class representing arguments of on host event.</summary>
    public class OnHostArgs
    {
        /// <summary>Property representing moderator who issued command.</summary>
        public string Moderator;
        /// <summary>Property representing hosted channel.</summary>
        public string HostedChannel;
    }
}
