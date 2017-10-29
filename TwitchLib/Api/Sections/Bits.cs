namespace TwitchLib
{
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    #endregion

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
            public async Task<Models.API.v5.Bits.Cheermotes> GetCheermotesAsync(string channelId = null)
            {
                string optionalQuery = (channelId != null) ? $"?channel_id={channelId}" : string.Empty;
                return await Api.GetGenericAsync<Models.API.v5.Bits.Cheermotes>($"https://api.twitch.tv/kraken/bits/actions{optionalQuery}").ConfigureAwait(false);
            }
            #endregion
        }
    }
}