using System;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Drop
{
    /// <summary>
    /// Drop Entitlement Grant subscription type model
    /// <para>Description:</para>
    /// <para>An entitlement for a Drop is granted to a user.</para>
    /// </summary>
    public class DropEntitlementGrant
    {
        /// <summary>
        /// The ID of the organization that owns the game that has Drops enabled.
        /// </summary>
        public string OrganizationId { get; set; } = string.Empty;
        /// <summary>
        /// Twitch category ID of the game that was being played when this benefit was entitled.
        /// </summary>
        public string CategoryId { get; set; } = string.Empty;
        /// <summary>
        /// Twitch category Name of the game that was being played when this benefit was entitled.
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;
        /// <summary>
        /// The campaign this entitlement is associated with.
        /// </summary>
        public string CampaignId { get; set; } = string.Empty;
        /// <summary>
        /// Twitch user ID of the user who was granted the entitlement.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The user display name of the user who was granted the entitlement.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The user login of the user who was granted the entitlement.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// Unique identifier of the entitlement. Use this to de-duplicate entitlements.
        /// </summary>
        public string EntitlementId { get; set; } = string.Empty;
        /// <summary>
        /// Identifier of the Benefit.
        /// </summary>
        public string BenefitId { get; set; } = string.Empty;
        /// <summary>
        /// UTC timestamp in ISO format when this entitlement was granted on Twitch.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    }
}