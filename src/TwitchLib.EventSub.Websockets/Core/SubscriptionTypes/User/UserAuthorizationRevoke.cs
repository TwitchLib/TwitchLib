namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.User;

/// <summary>
/// User Authorization Revoke subscription type model
/// <para>Description:</para>
/// <para>A user’s authorization has been revoked for your client id.</para>
/// </summary>
public class UserAuthorizationRevoke
{
    /// <summary>
    /// The client_id of the application with revoked user access.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;
    /// <summary>
    /// The user id for the user who has revoked authorization for your client id.
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// The user display name for the user who has revoked authorization for your client id. This is null if the user no longer exists.
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// The user login for the user who has revoked authorization for your client id. This is null if the user no longer exists.
    /// </summary>
    public string? UserLogin { get; set; }
}