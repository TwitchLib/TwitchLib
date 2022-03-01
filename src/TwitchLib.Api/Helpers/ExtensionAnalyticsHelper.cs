using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Helpers;

namespace TwitchLib.Api.Helpers
{
    public static class ExtensionAnalyticsHelper
    {
        public static async Task<List<ExtensionAnalytics>> HandleUrlAsync(string url)
        {
            var cnts = await GetContentsAsync(url);
            var data = ExtractData(cnts);

            return data.Select(line => new ExtensionAnalytics(line)).ToList();
        }

        private static IEnumerable<string> ExtractData(IEnumerable<string> cnts)
        {
            return cnts.Where(line => line.Any(char.IsDigit)).ToList();
        }

        private static async Task<string[]> GetContentsAsync(string url)
        {
            var client = new HttpClient();
            var lines = (await client.GetStringAsync(url)).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }
    }
}
