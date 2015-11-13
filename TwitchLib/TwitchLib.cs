using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class TwitchLib
    {
        private TwitchChatClientManager chatManager = new TwitchChatClientManager();
        private TwitchWhisperClient whisperClient = new TwitchWhisperClient();
        private ConnectionCredentials credentials;

        public TwitchLib(ConnectionCredentials credentials)
        {
            this.credentials = credentials;
        }

        public CallResponse addChannel(string channel)
        {
            return chatManager.addChannel(channel, credentials);
        }

        public CallResponse removeChannel(string channel)
        {
            return chatManager.removeChannel(channel);
        }

        public void connectWhisperClient()
        {
            whisperClient.connect();
        }

        public void disconnectWhisperClient()
        {
            whisperClient.disconnect();
        }

        //TODO: Raise new chat message event
        private void newChatMessage(string channel, ChatMessage chatMessage)
        {

        }

        //TODO: Raise new whipser event
        private void newWhisperMessage(WhisperMessage whisperMessage)
        {

        }

        //TODO: Raise new subscriber event
        private void newSubscriber(string channel, Subscriber subscriber)
        {

        }

        //TODO: Raise user joined event
        private void userJoined(string channel, string username)
        {

        }

        //TODO: Raise user left event
        private void userLeft(string channel, string username)
        {

        }
    }
}
