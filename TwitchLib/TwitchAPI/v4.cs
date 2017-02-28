using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    /// Fully featured Twitch API wrapper (for Twitch v4 endpoints).
    /// </summary>
    public static class v4
    {
        /// <summary>
        /// Twitch API calls relating to Twitch clips system.
        /// </summary>
        public static class Clips
        {
            /// <summary>
            /// [SYNC] Retrieves a list of top clips given specific (or no) parameters.
            /// </summary>
            /// <param name="channels">List of channels to get top clips from. Limit is 10.</param>
            /// <param name="games">List of games to get top clips from. Limit is 10.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10.</param>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="period">Period enum used to specify a date range. Default is Day.</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <returns>ClipsResponse object containing cursor string as well as List of Clip objects.</returns>
            public static Models.API.Clip.ClipsResponse GetTopClips(List<string> channels = null, List<string> games = null, int limit = 10, string cursor = null, Enums.Period period = Enums.Period.Day, bool trending = false) => Task.Run(() => Internal.TwitchApi.GetTopClips(channels, games, limit, cursor, period, trending)).Result;
            /// <summary>
            /// [ASYNC] Retrieves a list of top clips given specific (or no) parameters.
            /// </summary>
            /// <param name="channels">List of channels to get top clips from. Limit is 10.</param>
            /// <param name="games">List of games to get top clips from. Limit is 10.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10.</param>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="period">Period enum used to specify a date range. Default is Day.</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <returns>ClipsResponse object containing cursor string as well as List of Clip objects.</returns>
            public static async Task<Models.API.Clip.ClipsResponse> GetTopClipsAsync(List<string> channels = null, List<string> games = null, int limit = 10, string cursor = null, Enums.Period period = Enums.Period.Day, bool trending = false) => await Internal.TwitchApi.GetTopClips(channels, games, limit, cursor, period, trending);

            /// <summary>
            /// [SYNC] Retrieves detailed information regarding a specific clip.
            /// </summary>
            /// <param name="channel">The channel that the clip happened in.</param>
            /// <param name="slug">The string of words that identifies the clip.</param>
            /// <returns>Clip object.</returns>
            public static Models.API.Clip.Clip GetClipInformation(string channel, string slug) => Task.Run(() => Internal.TwitchApi.GetClipInformation(channel, slug)).Result;
            /// <summary>
            /// [ASYNC] Retrieves detailed information regarding a specific clip.
            /// </summary>
            /// <param name="channel">The channel that the clip happened in.</param>
            /// <param name="slug">The string of words that identifies the clip.</param>
            /// <returns>Clip object.</returns>
            public static async Task<Models.API.Clip.Clip> GetClipInformationAsync(string channel, string slug) => await Internal.TwitchApi.GetClipInformation(channel, slug);

            /// <summary>
            /// [SYNC] Gets the top Clips for a user's followed games. Required scope: user_read
            /// </summary>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>ClipsResponse object.</returns>
            public static Models.API.Clip.ClipsResponse GetFollowedClips(string cursor = "0", int limit = 10, bool trending = false, string accessToken = null) => Task.Run(() => Internal.TwitchApi.GetFollowedClips(cursor, limit, trending, accessToken)).Result;
            /// <summary>
            /// [ASYNC] Gets the top Clips for a user's followed games. Required scope: user_read
            /// </summary>
            /// <param name="cursor">Cursor used to index through all clips.</param>
            /// <param name="limit">Number of clip objects to return, limit is 100. Default is 10</param>
            /// <param name="trending">Only pull from trending clips? Default is false.</param>
            /// <param name="accessToken">An oauth token with the required scope.</param>
            /// <returns>ClipsResponse object.</returns>
            public static async Task<Models.API.Clip.ClipsResponse> GetFollowedClipsAsync(string cursor = "0", int limit = 10, bool trending = false, string accessToken = null) => await Internal.TwitchApi.GetFollowedClips(cursor, limit, trending, accessToken);
        }
    }
}
