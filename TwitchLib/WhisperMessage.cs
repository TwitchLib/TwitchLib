using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class WhisperMessage
    {
        public enum UType
        {
            Viewer,
            GlobalModerator,
            Admin,
            Staff
        }

        private string _botUsername, _colorHex, _username, _displayName, _emoteSet, _threadId, _message, _badges;
        private int _messageId, _userId;
        private bool _turbo;

        public string Badges => _badges;
        public string ColorHex => _colorHex;
        public string Username => _username;
        public string DisplayName => _displayName;
        public string EmoteSet => _emoteSet;
        public string ThreadId => _threadId;
        public int MessageId => _messageId;
        public int UserId => _userId;
        public bool Turbo => _turbo;
        public string BotUsername => _botUsername;
        public string Message => _message;

        public WhisperMessage(string ircString, string botUsername)
        {
            if (ircString.Split(';').Length == 9)
            {
                //@badges=;color=#00FF7F;display-name=Dara226;emotes=;message-id=53;thread-id=62192703_66137196;turbo=0;user-id=62192703;user-type= :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
                _badges = ircString.Split(';')[0].Split('=')[1];
                _colorHex = ircString.Split(';')[1].Split('=')[1];
                _username = ircString.Split('@')[2].Split('.')[0];
                _displayName = ircString.Split(';')[2].Split('=')[1];
                _emoteSet = ircString.Split(';')[3].Split('=')[1];
                _messageId = int.Parse(ircString.Split(';')[4].Split('=')[1]);
                _threadId = ircString.Split(';')[5].Split('=')[1];
                _turbo = ConvertToBool(ircString.Split(';')[6].Split('=')[1]);
                _userId = int.Parse(ircString.Split(';')[7].Split('=')[1]);
                var userTypeStr = ircString.Split(';')[8].Split('=')[1].Replace(" ", "");
                _message =
                    ircString.Replace(
                        ircString.Split('@')[0] + "@" + ircString.Split('@')[1] + "@" +
                        ircString.Split('@')[2].Split(':')[0] + ":", string.Empty);
            }
            else
            {
                //@color=#00FF7F;display-name=Dara226;emotes=;message-id=53;thread-id=62192703_66137196;turbo=0;user-id=62192703;user-type= :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
                _badges = "";
                _colorHex = ircString.Split(';')[0].Split('=')[1];
                _username = ircString.Split('@')[2].Split('.')[0];
                _displayName = ircString.Split(';')[1].Split('=')[1];
                _emoteSet = ircString.Split(';')[2].Split('=')[1];
                _messageId = int.Parse(ircString.Split(';')[3].Split('=')[1]);
                _threadId = ircString.Split(';')[4].Split('=')[1];
                _turbo = ConvertToBool(ircString.Split(';')[5].Split('=')[1]);
                _userId = int.Parse(ircString.Split(';')[6].Split('=')[1]);
                var userTypeStr = ircString.Split(';')[7].Split('=')[1].Replace(" ", "");
                _message =
                    ircString.Replace(
                        ircString.Split('@')[0] + "@" + ircString.Split('@')[1] + "@" +
                        ircString.Split('@')[2].Split(':')[0] + ":", string.Empty);
            }
            _botUsername = botUsername;
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}