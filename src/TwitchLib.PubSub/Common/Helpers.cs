using System;
using System.Text;

namespace TwitchLib.PubSub.Common
{
    /// <summary>
    /// Static class of helper functions used around the project.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Takes date time string received from Twitch API and converts it to DateTime object.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>DateTime.</returns>
        public static DateTime DateTimeStringToObject(string dateTime)
        {
            return dateTime == null ? new DateTime() : Convert.ToDateTime(dateTime);
        }


        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.String.</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}