namespace TwitchLib.PubSub.Enums
{
    public enum PredictionStatus
    {
        Canceled = -4,
        CancelPending = -3,
        Resolved = -2,
        ResolvePending = -1,
        Locked = 0,
        Active = 1,
    }
}
