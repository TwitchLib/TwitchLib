namespace TwitchLib.PubSub.Models
{
    /// <summary>
    /// Model representing the leader board.
    /// </summary>
    public class LeaderBoard
    {
        /// <summary>
        /// Place
        /// </summary>
        /// <value>The place</value>
        public int Place { get; set; }
        /// <summary>
        /// Score
        /// </summary>
        /// <value>The Score of the user</value>
        public int Score { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        /// <value>The User id</value>
        public string UserId { get; set; }
    }
}
