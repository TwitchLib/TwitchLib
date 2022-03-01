namespace TwitchLib.EventSub.Websockets.Core.Models.Extensions;

/// <summary>
/// Additional information about a product acquired via a Twitch Extension Bits transaction.
/// </summary>
public class BitsProduct
{
    /// <summary>
    /// Product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Unique identifier for the product acquired.
    /// </summary>
    public string Sku { get; set; } = string.Empty;
    /// <summary>
    /// Bits involved in the transaction.
    /// </summary>
    public int Bits { get; set; }
    /// <summary>
    /// Flag indicating if the product is in development. If in_development is true, bits will be 0.
    /// </summary>
    public bool InDevelopment { get; set; }
}