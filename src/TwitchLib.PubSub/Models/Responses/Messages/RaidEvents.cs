using System;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// RaidEvents model constructor.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class RaidEvents : MessageData
    {
        /// <summary>
        /// Raid type
        /// </summary>
        /// <value>The raid type</value>
        public RaidType Type { get; protected set; }
        /// <summary>
        /// Raid id
        /// </summary>
        /// <value>The raid id</value>
        public Guid Id { get; protected set; }
        /// <summary>
        /// Channel Id
        /// </summary>
        /// <value>The channel id</value>
        public string ChannelId { get; protected set; }
        /// <summary>
        /// Target Channel Id
        /// </summary>
        /// <value>The target channel id</value>
        public string TargetChannelId { get; protected set; }
        /// <summary>
        /// Target Login
        /// </summary>
        /// <value>The target login name</value>
        public string TargetLogin { get; protected set; }
        /// <summary>
        /// Target Display Name
        /// </summary>
        /// <value>The target display name</value>
        public string TargetDisplayName { get; protected set; }
        /// <summary>
        /// Target Profile Image
        /// </summary>
        /// <value>The target profile image (url)</value>
        public string TargetProfileImage { get; protected set; }
        /// <summary>
        /// Announce Time
        /// </summary>
        /// <value>The announce time</value>
        public DateTime AnnounceTime { get; protected set; }
        /// <summary>
        /// Raid Time
        /// </summary>
        /// <value>The raid time</value>
        public DateTime RaidTime { get; protected set; }
        /// <summary>
        /// Remaining Duration Seconds
        /// </summary>
        /// <value>The remaining duration seconds</value>
        public int RemainigDurationSeconds { get; protected set; }
        /// <summary>
        /// Viewer Count
        /// </summary>
        /// <value>The viewer count</value>
        public int ViewerCount { get; protected set; }

        /// <summary>
        /// RaidEvents constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public RaidEvents(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            switch (json.SelectToken("type").ToString())
            {                
                case "raid_update":
                    Type = RaidType.RaidUpdate;
                    break;
                case "raid_update_v2":
                    Type = RaidType.RaidUpdateV2;
                    break;
                case "raid_go_v2":
                    Type = RaidType.RaidGo;
                    break;

            }

            switch (Type)
            {                
                case RaidType.RaidUpdate:
                    Id = Guid.Parse(json.SelectToken("raid.id").ToString());
                    ChannelId = json.SelectToken("raid.source_id").ToString();
                    TargetChannelId = json.SelectToken("raid.target_id").ToString();
                    AnnounceTime = DateTime.Parse(json.SelectToken("raid.announce_time").ToString());
                    RaidTime = DateTime.Parse(json.SelectToken("raid.raid_time").ToString());
                    RemainigDurationSeconds = int.Parse(json.SelectToken("raid.remaining_duration_seconds").ToString());
                    ViewerCount = int.Parse(json.SelectToken("raid.viewer_count").ToString());
                    break;
                case RaidType.RaidUpdateV2:
                    Id = Guid.Parse(json.SelectToken("raid.id").ToString());
                    ChannelId = json.SelectToken("raid.source_id").ToString();
                    TargetChannelId = json.SelectToken("raid.target_id").ToString();
                    TargetLogin = json.SelectToken("raid.target_login").ToString();
                    TargetDisplayName = json.SelectToken("raid.target_display_name").ToString();
                    TargetProfileImage = json.SelectToken("raid.target_profile_image").ToString();
                    ViewerCount = int.Parse(json.SelectToken("raid.viewer_count").ToString());
                    break;
                case RaidType.RaidGo:
                    Id = Guid.Parse(json.SelectToken("raid.id").ToString());
                    ChannelId = json.SelectToken("raid.source_id").ToString();
                    TargetChannelId = json.SelectToken("raid.target_id").ToString();
                    TargetLogin = json.SelectToken("raid.target_login").ToString();
                    TargetDisplayName = json.SelectToken("raid.target_display_name").ToString();
                    TargetProfileImage = json.SelectToken("raid.target_profile_image").ToString();
                    ViewerCount = int.Parse(json.SelectToken("raid.viewer_count").ToString());
                    break;
            }
        }
    }
}
