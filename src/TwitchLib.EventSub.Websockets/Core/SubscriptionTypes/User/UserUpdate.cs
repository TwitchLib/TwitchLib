namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.User;

/// <summary>
/// User Update subscription type model
/// <para>Description:</para>
/// <para>A user has updated their account.</para>
/// </summary>
public class UserUpdate
{
    /// <summary>
    /// The user’s user id.
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// The user's user display name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// The user's user login.
    /// </summary>
    public string UserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The user's email. Only included if you have the "user:read:email" scope for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// The user's description 
    /// </summary>
    public string Description { get; set; } = string.Empty;
}