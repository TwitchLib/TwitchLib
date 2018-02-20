using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace TwitchLib.Api
{
    public class Bits
    {
        public Bits(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }

            #region GetCheermotes
            public async Task<Models.v5.Bits.Cheermotes> GetCheermotesAsync(string channelId = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if (channelId != null)
                    getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("channel_id", channelId) };
                return await Api.GetGenericAsync<Models.v5.Bits.Cheermotes>("https://api.twitch.tv/kraken/bits/actions", getParams).ConfigureAwait(false);
            }
            #endregion
        }
    }
}