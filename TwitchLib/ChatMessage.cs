using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    class ChatMessage
    {
        private string colorHEX, username, display_name, message;
        private Boolean subscriber, turbo;
        List<string> emotes;
        utility.userType user_type;
        public ChatMessage(string _username, string _display_name, string _colorHEX, List<string> _emotes,
            Boolean _subscriber, Boolean _turbo, utility.userType _userType, string _message)
        {
            display_name = _display_name;
            username = _username;
            colorHEX = _colorHEX;
            emotes = _emotes;
            subscriber = _subscriber;
            turbo = _turbo;
            user_type = _userType;
            message = _message;
        }

        public string getSenderDisplayName()
        {
            string senderDisplayNameData = display_name;
            return senderDisplayNameData;
        }

        public string getSenderUsername()
        {
            string senderUsernameData = username;
            return senderUsernameData;
        }

        public string getColorHEX()
        {
            string colorHEXData = colorHEX;
            return colorHEXData;
        }

        public List<string> getEmotes()
        {
            List<string> emoteData = emotes;
            return emoteData;
        }

        public Boolean getSubscriber()
        {
            Boolean subscriberData = subscriber;
            return subscriberData;
        }

        public Boolean getTurbo()
        {
            Boolean turboData = turbo;
            return turboData;
        }

        public utility.userType getUserType()
        {
            utility.userType uTypeData = user_type;
            return uTypeData;
        }

        public string getMessage()
        {
            string messageData = message;
            return message;
        }
    }
}
