using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    class WhisperMessage
    {
        public enum uType
        {
            Viewer,
            GlobalModerator,
            Admin,
            Staff
        }
        private string colorHEX, username, displayName, emoteSet, threadID, message;
        private int messageID, userID;
        private bool turbo;

        public string ColorHEX { get { return colorHEX; } }
        public string Username { get { return username; } }
        public string DisplayName { get { return displayName; } }
        public string EmoteSet { get { return emoteSet; } }
        public string ThreadID { get { return threadID; } }
        public int MessageID { get { return messageID; } }
        public int UserID { get { return userID; } }
        public bool Turbo { get { return turbo; } }

        public WhisperMessage(string IRCString)
        {
            //@color=#00FF7F;display-name=Dara226;emotes=;message-id=53;thread-id=62192703_66137196;turbo=0;user-id=62192703;user-type= :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
            colorHEX = IRCString.Split(';')[0].Split('=')[1];
            username = IRCString.Split('@')[2].Split('.')[0];
            displayName = IRCString.Split(';')[1].Split('=')[1];
            emoteSet = IRCString.Split(';')[2].Split('=')[1];
            messageID = int.Parse(IRCString.Split(';')[3].Split('=')[1]);
            threadID = IRCString.Split(';')[4].Split('=')[1];
            turbo = convertToBool(IRCString.Split(';')[5].Split('=')[1]);
            userID = int.Parse(IRCString.Split(';')[6].Split('=')[1]);
            string userTypeStr = IRCString.Split(';')[7].Split('=')[1].Replace(" ", "");
            message = IRCString.Replace(IRCString.Split('@')[0] + "@" + IRCString.Split('@')[1] + "@" + IRCString.Split('@')[2].Split(':')[0] + ":", String.Empty);
            
        }

        private bool convertToBool(string data)
        {
            if (data == "1")
                return true;
            return false;
        }
    }
}
