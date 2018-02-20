using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.ChatProperties
{
    public class ChatProperties
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "hide_chat_links")]
        public bool HideChatLinks { get; protected set; }
        [JsonProperty(PropertyName = "chat_delay_duration")]
        public int ChatDelayDuration { get; protected set; }
        [JsonProperty(PropertyName = "chat_rules")]
        public string[] ChatRules { get; protected set; }
        [JsonProperty(PropertyName = "twitchbot_rule_id")]
        public int TwitchbotRuleId { get; protected set; }
        [JsonProperty(PropertyName = "devchat")]
        public bool DevChat { get; protected set; }
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        [JsonProperty(PropertyName = "subsonly")]
        public bool SubsOnly { get; protected set; }
        [JsonProperty(PropertyName = "chat_servers")]
        public string[] ChatServers { get; protected set; }
        [JsonProperty(PropertyName = "web_socket_servers")]
        public string[] WebSocketServers { get; protected set; }
        [JsonProperty(PropertyName = "web_socket_pct")]
        public double WebSocketPct { get; protected set; }
        [JsonProperty(PropertyName = "darklaunch_pct")]
        public double DarkLaunchPct { get; protected set; }
        [JsonProperty(PropertyName = "available_chat_notification_tokens")]
        public string[] AvailableChatNotificationTokens { get; protected set; }
        [JsonProperty(PropertyName = "sce_title_preset_text_1")]
        public string SceTitlePresetText1 { get; protected set; }
        [JsonProperty(PropertyName = "sce_title_preset_text_2")]
        public string SceTitlePresetText2 { get; protected set; }
        [JsonProperty(PropertyName = "sce_title_preset_text_3")]
        public string SceTitlePresetText3 { get; protected set; }
        [JsonProperty(PropertyName = "sce_title_preset_text_4")]
        public string SceTitlePresetText4 { get; protected set; }
        [JsonProperty(PropertyName = "sce_title_preset_text_5")]
        public string SceTitlePresetText5 { get; protected set; }
    }
}
