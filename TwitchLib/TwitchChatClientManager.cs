using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    class TwitchChatClientManager
    {
        private List<TwitchChatClient> chatClients = new List<TwitchChatClient>();
        private int channelLimit;

        public int ConnectionCount { get { return chatClients.Count(); } }
        public string[] ConnectedChannels { get { return getChannels(); } }
        public int ChannelLimit { get { return channelLimit; } }

        public TwitchChatClientManager(int channelLimit = -1) {
            this.channelLimit = channelLimit;
        }

        //TODO: Raise event for new chat message
        private void newChatMessage(string channel, ChatMessage chatMessage)
        {
            
        }

        //TODO: Raise event for new subscriber
        private void newSubscriber(string channel, Subscriber subscriber)
        {

        }

        //TODO: Raise event for user joined
        private void userJoined(string channel, string name)
        {

        }

        //TODO: Raise event for user left
        private void userLeft(string channel, string name)
        {

        }

        public CallResponse addChannel(string channel, ConnectionCredentials credentials)
        {
            if (channelLimit != -1 && channelLimit == chatClients.Count())
                return new CallResponse(false, "Channel limit hit");

            foreach (TwitchChatClient chatClient in chatClients)
            {
                if (chatClient.Channel == channel.ToLower())
                {
                    return new CallResponse(false, "Channel already exists");
                }
            }
            TwitchChatClient newClient = new TwitchChatClient(channel, credentials);
            //TODO: Add event hook for chat messages, subs, etc

            chatClients.Add(newClient);
            return new CallResponse(true, newClient);
        }

        public CallResponse removeChannel(string channel)
        {
            int chatClientID = -1;
            for (int i = 0; i < chatClients.Count(); i++)
            {
                if (chatClients[i].Channel == channel.ToLower())
                {
                    chatClientID = i;
                }
            }
            if (chatClientID != -1)
            {
                chatClients[chatClientID].disconnect();
                chatClients.RemoveAt(chatClientID);
                return new CallResponse(true);
            }
            else
            {
                return new CallResponse(false, "Channel not found in connected channels list");
            }
        }

        public CallResponse updateChannelLimit(int limit)
        {
            if ((limit >= chatClients.Count() || (chatClients.Count() == 0 && limit == -1)) && limit > -2)
            {
                channelLimit = limit;
                return new CallResponse(true);
            }
            else
            {
                return new CallResponse(false, "Invalid limit.  Limit must be larger than -2, and also larger than or equal to the number of connected channels");
            }
        }

        private string[] getChannels()
        {
            string[] ret = new string[chatClients.Count()];
            for(int i = 0; i < chatClients.Count(); i++) {
                ret[i] = chatClients[0].Channel;
            }
            return ret;
        }
    }
}
