using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    /// <summary>Extension for implementing Commercial functionality in TwitchClient.</summary>
    public static class CommercialExt
    {
        /// <summary>
        /// Sends command to start a commercial of variable length.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of the channel to send the ad to.</param>
        /// <param name="length">Enum representing the length of advertisement should be.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void StartCommercial(this TwitchClient client, JoinedChannel channel, Enums.CommercialLength length)
        {
            switch(length)
            {
                case Enums.CommercialLength.Seconds30:
                    client.SendMessage(channel, ".commercial 30");
                    break;
                case Enums.CommercialLength.Seconds60:
                    client.SendMessage(channel, ".commercial 60");
                    break;
                case Enums.CommercialLength.Seconds90:
                    client.SendMessage(channel, ".commercial 90");
                    break;
                case Enums.CommercialLength.Seconds120:
                    client.SendMessage(channel, ".commercial 120");
                    break;
                case Enums.CommercialLength.Seconds150:
                    client.SendMessage(channel, ".commercial 150");
                    break;
                case Enums.CommercialLength.Seconds180:
                    client.SendMessage(channel, ".commercial 180");
                    break;
            }
        }

        /// <summary>
        /// Sends command to start a commercial of variable length.
        /// </summary>
        /// <param name="channel">String representation of the channel to send the ad to.</param>
        /// <param name="length">Enum representing the length of advertisement should be.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void StartCommercial(this TwitchClient client, string channel, Enums.CommercialLength length)
        {
            switch (length)
            {
                case Enums.CommercialLength.Seconds30:
                    client.SendMessage(channel, ".commercial 30");
                    break;
                case Enums.CommercialLength.Seconds60:
                    client.SendMessage(channel, ".commercial 60");
                    break;
                case Enums.CommercialLength.Seconds90:
                    client.SendMessage(channel, ".commercial 90");
                    break;
                case Enums.CommercialLength.Seconds120:
                    client.SendMessage(channel, ".commercial 120");
                    break;
                case Enums.CommercialLength.Seconds150:
                    client.SendMessage(channel, ".commercial 150");
                    break;
                case Enums.CommercialLength.Seconds180:
                    client.SendMessage(channel, ".commercial 180");
                    break;
            }
        }

        /// <summary>
        /// Sends command to start a commercial of variable length.
        /// </summary>
        /// <param name="length">Enum representing the length of advertisement should be.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void StartCommercial(this TwitchClient client, Enums.CommercialLength length)
        {
            switch (length)
            {
                case Enums.CommercialLength.Seconds30:
                    client.SendMessage(".commercial 30");
                    break;
                case Enums.CommercialLength.Seconds60:
                    client.SendMessage(".commercial 60");
                    break;
                case Enums.CommercialLength.Seconds90:
                    client.SendMessage(".commercial 90");
                    break;
                case Enums.CommercialLength.Seconds120:
                    client.SendMessage(".commercial 120");
                    break;
                case Enums.CommercialLength.Seconds150:
                    client.SendMessage(".commercial 150");
                    break;
                case Enums.CommercialLength.Seconds180:
                    client.SendMessage(".commercial 180");
                    break;
            }
        }
    }
}
