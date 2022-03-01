using System;
using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Root
{
    public class RootAuthorization
    {
        #region CreatedAt
        /// <summary>Property representing the date time of channel creation.</summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region Scopes
        /// <summary>Property representing the scopes.</summary>
        [JsonPropertyName("scopes")]
        public string[] Scopes { get; protected set; }
        #endregion
        #region UpdatedAt
        /// <summary>Property representing the date time of last channel update.</summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
    }
}
