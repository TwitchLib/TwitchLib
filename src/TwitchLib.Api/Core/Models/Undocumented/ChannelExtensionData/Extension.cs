using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class Extension
    {
        [JsonPropertyName("ud")]
        public string Id { get; protected set; }
        [JsonPropertyName("state")]
        public string State { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }
        [JsonPropertyName("anchor")]
        public string Anchor { get; protected set; }
        [JsonPropertyName("panel_height")]
        public int PanelHeight { get; protected set; }
        [JsonPropertyName("author_name")]
        public string AuthorName { get; protected set; }
        [JsonPropertyName("support_email")]
        public string SupportEmail { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("description")]
        public string Description { get; protected set; }
        [JsonPropertyName("summary")]
        public string Summary { get; protected set; }
        [JsonPropertyName("viewer_url")]
        public string ViewerUrl { get; protected set; }
        [JsonPropertyName("viewer_urls")]
        public ViewerUrls ViewerUrls { get; protected set; }
        [JsonPropertyName("views")]
        public Views Views { get; protected set; }
        [JsonPropertyName("config_url")]
        public string ConfigUrl { get; protected set; }
        [JsonPropertyName("live_config_url")]
        public string LiveConfigUrl { get; protected set; }
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; protected set; }
        [JsonPropertyName("icon_urls")]
        public IconUrls IconUrls { get; protected set; }
        [JsonPropertyName("screenshot_urls")]
        public string[] ScreenshotUrls { get; protected set; }
        [JsonPropertyName("installation_count")]
        public int InstallationCount { get; protected set; }
        [JsonPropertyName("can_install")]
        public bool CanInstall { get; protected set; }
        [JsonPropertyName("whitelisted_panel_urls")]
        public string[] WhitelistedPanelUrls { get; protected set; }
        [JsonPropertyName("whitelisted_config_urls")]
        public string[] WhitelistedConfigUrls { get; protected set; }
        [JsonPropertyName("eula_tos_url")]
        public string EulaTosUrl { get; protected set; }
        [JsonPropertyName("privacy_policy_url")]
        public string PrivacyPolicyUrl { get; protected set; }
        [JsonPropertyName("request_identity_link")]
        public bool RequestIdentityLink { get; protected set; }
        [JsonPropertyName("vendor_code")]
        public string VendorCode { get; protected set; }
        [JsonPropertyName("sku")]
        public string SKU { get; protected set; }
        [JsonPropertyName("bits_enabled")]
        public bool BitsEnabled { get; protected set; }
    }
}
