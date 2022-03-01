namespace TwitchLib.EventSub.Websockets.Core.Models.Polls;

/// <summary>
/// Whether Bits/ChannelPoints voting is enabled and its cost
/// </summary>
public class PollVotingSettings
{
    /// <summary>
    /// Indicates if Bits/Channel Points can be used for voting.
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Number of Bits/Channel Points required to vote once with Bits/Channel Points.
    /// </summary>
    public int AmountPerVote { get; set; }
}