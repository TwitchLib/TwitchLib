using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Tests.ChatClientTesting
{
    public static class TestStrings
    {
        public static class Connected
        {
            public static string CONNECTED_IDENTIFIER = "You are in a maze of twisty passages, all alike.";
        }

        public static class Subscribers
        {
            public static string NEW_SUBSCRIBER_1 = "";
            public static string NEW_SUBSCRIBER_2 = "";

            public static string RE_SUBSCRIBER_1 = "";
            public static string RE_SUBSCRIBER_2 = "";
        }

        public static class Messages
        {
            public static string MESSAGE_1 = "";
            public static string MESSAGE_2 = "";
            public static string MESSAGE_3 = "";
            public static string MESSAGE_4 = "";
            public static string MESSAGE_5 = "";
        }

        public static class Commands
        {
            public static List<char> COMMAND_IDENTIFIER_1 = new List<char>() { '!' };
            public static List<char> COMMAND_IDENTIFIER_2 = new List<char>() { '!', '#' };

            public static string COMMAND_1 = "";
            public static string COMMAND_2 = "";
            public static string COMMAND_3 = "";
            public static string COMMAND_4 = "";
            public static string COMMAND_5 = "";
        }

        public static class JoinsAndLeaves
        {
            public static string VIEWER_JOINED_1 = "";
            public static string VIEWER_JOINED_2 = "";

            public static string VIEWER_LEFT_1 = "";
            public static string VIEWER_LEFT_2 = "";

            public static string MODERATOR_JOINED_1 = "";
            public static string MODERATOR_JOINED_2 = "";

            public static string MODERATOR_LEFT_1 = "";
            public static string MODERATOR_LEFT_2 = "";
        }

        public static class IncorrectLogin
        {
            public static string INCORRECT_LOGIN_1 = "";
            public static string INCORRECT_LOGIN_2 = "";
        }

        public static class Hosts
        {
            public static string HOST_LEFT_1 = "";
            public static string HOST_LEFT_2 = "";

            public static string HOSTING_STARTED_1 = "";
            public static string HOSTING_STARTED_2 = "";

            public static string HOSTING_STOPPED_1 = "";
            public static string HOSTING_STOPPED_2 = "";
        }

        public static class ChannelStateChanged
        {
            public static string CHANNEL_STATE_CHANGED_1 = "";
            public static string CHANNEL_STATE_CHANGED_2 = "";
            public static string CHANNEL_STATE_CHANGED_3 = "";
        }

        public static class UserStateChanged
        {
            public static string USER_STATE_CHANGED_1 = "";
            public static string USER_STATE_CHANGED_2 = "";
            public static string USER_STATE_CHANGED_3 = "";
        }

        public static class Ping
        {
            public static string PING_1 = "";
        }

        public static class ExistingUsers
        {
            public static string EXISTING_USERS_1 = "";
            public static string EXISTING_USERS_2 = "";
            public static string EXISTING_USERS_3 = "";
        }
    }
}
