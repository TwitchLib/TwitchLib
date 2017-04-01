using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Blocks
{
    public class Block
    {
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; set; }
        [JsonProperty(PropertyName = "_id")]
        public int Id { get; set; }
    }
}
