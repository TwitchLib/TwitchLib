using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing Channels object.</summary>
    public class Channels
    {
        /// <summary>Property representing the display name of a user.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing the game a channel is playing.</summary>
        public string Game { get; protected set; }
        /// <summary>Property representing the status of a specific channel.</summary>
        public string Status { get; protected set; }
        /// <summary>Property representing whether or not a channel is fighting adblock.</summary>
        public bool FightAdBlock { get; protected set; }
        /// <summary>Property representing internal Id variable.</summary>
        public long Id { get; protected set; }
        /// <summary>Property representing the name of a channel.</summary>
        public string Name { get; protected set; }
        /// <summary>Property representing the partner status of the channel.</summary>
        public bool Partner { get; protected set; }
        /// <summary>Property represeting Twitch's channel Liverail Id (I think related to advertisements)</summary>
        public long TwitchLiverailId { get; protected set; }
        /// <summary>Property representing the LiverailId.</summary>
        public long LiverailId { get; protected set; }
        /// <summary>Property representing the comscore id (I think related to advertisements).</summary>
        public string ComscoreId { get; protected set; }
        /// <summary>Property representing Comscore6(?).</summary>
        public string ComscoreC6 { get; protected set; }
        /// <summary>Property representing the Steam Id of the user (if available).</summary>
        public long SteamId { get; protected set; }
        /// <summary>Property representing the PPV status of the channel.</summary>
        public bool PPV { get; protected set; }
        /// <summary>Property representing the broadcaster software (fairly unreliable).</summary>
        public string BroadcasterSoftware { get; protected set; }
        /// <summary>Property representing the preroll status of the channel (preroll ads)</summary>
        public bool Prerolls { get; protected set; }
        /// <summary>Property representing the postrolls status of the channel (postroll ads)</summary>
        public bool Postrolls { get; protected set; }

        /// <summary>Constructor for Channels object.</summary>
        public Channels(JToken json)
        {
            bool fightAdBlock, partner, ppv, prerolls, postrolls;

            DisplayName = json.SelectToken("display_name")?.ToString();
            Game = json.SelectToken("game")?.ToString();
            Status = json.SelectToken("status")?.ToString();
            FightAdBlock = bool.TryParse(json.SelectToken("fight_ad_block").ToString(), out fightAdBlock) && fightAdBlock;
            if (json.SelectToken("_id") != null)
                Id = long.Parse(json.SelectToken("_id").ToString());
            Name = json.SelectToken("name")?.ToString();
            Partner = bool.TryParse(json.SelectToken("partner").ToString(), out partner) && partner;
            if (json.SelectToken("twitch_liverail_id") != null)
                TwitchLiverailId = long.Parse(json.SelectToken("twitch_liverail_id").ToString());
            if (json.SelectToken("liverail_id") != null)
                LiverailId = long.Parse(json.SelectToken("liverail_id").ToString());
            ComscoreId = json.SelectToken("comscore_id")?.ToString();
            ComscoreC6 = json.SelectToken("comscore_c6")?.ToString();
            if (json.SelectToken("steam_id") != null)
                SteamId = long.Parse(json.SelectToken("steam_id").ToString());
            PPV = (bool.TryParse(json.SelectToken("ppv").ToString(), out ppv) && ppv);
            BroadcasterSoftware = json.SelectToken("broadcaster_software")?.ToString();
            Prerolls = bool.TryParse(json.SelectToken("prerolls").ToString(), out prerolls) && prerolls;
            Postrolls = bool.TryParse(json.SelectToken("postrolls").ToString(), out postrolls) && postrolls;
        }
    }
}
