using System;

namespace TwitchLib.EventSub.Websockets.Core.Models.Subscriptions;

/// <summary>
/// Defines a subscription message send in chat to share a resubscription
/// </summary>
public class SubscriptionMessage
{
    /// <summary>
    /// The text of the resubscription chat message.
    /// </summary>
    public string Text { get; set; } = string.Empty;
    /// <summary>
    /// An array that includes the emote ID and start and end positions for where the emote appears in the text.
    /// </summary>
    public SubscriptionMessageEmote[] Emotes { get; set; } = Array.Empty<SubscriptionMessageEmote>();
}