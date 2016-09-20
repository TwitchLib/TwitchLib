using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class Channels
    {
        public string DisplayName { get; protected set; }
        public string Game { get; protected set; }
        public string Status { get; protected set; }
        public bool FightAdBlock { get; protected set; }
        public long Id { get; protected set; }
        public string Name { get; protected set; }
        public bool Partner { get; protected set; }
        public long TwitchLiverailId { get; protected set; }
        public long LiverailId { get; protected set; }
        public string ComscoreId { get; protected set; }
        public string ComscoreC6 { get; protected set; }
        public long SteamId { get; protected set; }
        public bool PPV { get; protected set; }
        public string BroadcasterSoftware { get; protected set; }
        public bool Prerolls { get; protected set; }
        public bool Postrolls { get; protected set; }

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
