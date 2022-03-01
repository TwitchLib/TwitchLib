namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.User;

/// <summary>
/// User Authorization Grant subscription type model
/// <para>Description:</para>
/// <para>A user’s authorization has been granted to your client id.</para>
/// </summary>
public class UserAuthorizationGrant
{
    /// <summary>
    /// The client_id of the application that was granted user access.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;
    /// <summary>
    /// The user id for the user who has granted authorization for your client id.
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// The user display name for the user who has granted authorization for your client id.
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// The user login for the user who has granted authorization for your client id.
    /// </summary>
    public string UserLogin { get; set; } = string.Empty;
}