using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChatProperties
{
    public class ChatProperties
    {
        [JsonPropertyName("_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("hide_chat_links")]
        public bool HideChatLinks { get; protected set; }
        [JsonPropertyName("chat_delay_duration")]
        public int ChatDelayDuration { get; protected set; }
        [JsonPropertyName("chat_rules")]
        public string[] ChatRules { get; protected set; }
        [JsonPropertyName("twitchbot_rule_id")]
        public int TwitchbotRuleId { get; protected set; }
        [JsonPropertyName("devchat")]
        public bool DevChat { get; protected set; }
        [JsonPropertyName("game")]
        public string Game { get; protected set; }
        [JsonPropertyName("subsonly")]
        public bool SubsOnly { get; protected set; }
        [JsonPropertyName("chat_servers")]
        public string[] ChatServers { get; protected set; }
        [JsonPropertyName("web_socket_servers")]
        public string[] WebSocketServers { get; protected set; }
        [JsonPropertyName("web_socket_pct")]
        public double WebSocketPct { get; protected set; }
        [JsonPropertyName("darklaunch_pct")]
        public double DarkLaunchPct { get; protected set; }
        [JsonPropertyName("available_chat_notification_tokens")]
        public string[] AvailableChatNotificationTokens { get; protected set; }
        [JsonPropertyName("sce_title_preset_text_1")]
        public string SceTitlePresetText1 { get; protected set; }
        [JsonPropertyName("sce_title_preset_text_2")]
        public string SceTitlePresetText2 { get; protected set; }
        [JsonPropertyName("sce_title_preset_text_3")]
        public string SceTitlePresetText3 { get; protected set; }
        [JsonPropertyName("sce_title_preset_text_4")]
        public string SceTitlePresetText4 { get; protected set; }
        [JsonPropertyName("sce_title_preset_text_5")]
        public string SceTitlePresetText5 { get; protected set; }
    }
}
