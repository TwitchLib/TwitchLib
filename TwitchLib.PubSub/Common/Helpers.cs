using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Common
{
    /// <summary>Static class of helper functions used around the project.</summary>
    public static class Helpers
    {
        /// <summary>Takes date time string received from Twitch API and converts it to DateTime object.</summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime DateTimeStringToObject(string dateTime)
        {
            return dateTime == null ? new DateTime() : Convert.ToDateTime(dateTime);
        }
        

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}