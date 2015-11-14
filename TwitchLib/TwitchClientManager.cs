using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class TwitchClientManager
    {
        private List<TwitchChatClient> chatClients = new List<TwitchChatClient>();
        private List<TwitchWhisperClient> whisperClients = new List<TwitchWhisperClient>();
        private int chatClientLimiter, whisperClientLimiter;

        public event EventHandler<NewChatMessageArgs>NewChatMessage;
        public event EventHandler<NewSubscriberArgs>NewSubscriber;
        public event EventHandler<ChannelStateAssignedArgs>ChannelAssignedState;
        public event EventHandler<NewWhisperMessageArgs>NewWhisperMessage;

        public class NewChatMessageArgs : EventArgs {
            public ChatMessage ChatMessage;
        }

        public class NewSubscriberArgs : EventArgs {
            public Subscriber Subscriber;
        }

        public class ChannelStateAssignedArgs : EventArgs { 
            public ChannelState ChannelState;
        }

        public class NewWhisperMessageArgs : EventArgs {
            public WhisperMessage WhisperMessage;
        }

        public TwitchClientManager(int chatClientLimiter = -1, int whisperClientLimiter = -1)
        {
            this.chatClientLimiter = chatClientLimiter;
            this.whisperClientLimiter = whisperClientLimiter;
        }

        public CallResponse addChatClient(string channel, ConnectionCredentials credentials)
        {
            if(chatClients.Count() != chatClientLimiter) {
                if(!chatClientAlreadyExists(credentials)) {
                    TwitchChatClient newClient = new TwitchChatClient(channel.ToLower(), credentials);
                    newClient.NewChatMessage += new EventHandler<TwitchChatClient.NewChatMessageArgs>(onNewChatMessage);
                    newClient.NewSubscriber += new EventHandler<TwitchChatClient.NewSubscriberArgs>(onNewSubscriber);
                    newClient.ChannelStateAssigned += new EventHandler<TwitchChatClient.ChannelStateAssignedArgs>(onChannelAssignedState);
                    newClient.connect();
                    chatClients.Add(newClient);
                    return new CallResponse(true, newClient);
                } else { return new CallResponse(false, "Twitch username already connected to channel!"); }
            } else { return new CallResponse(false, "Chat client limit hit."); }
        }

        public CallResponse addWhisperClient(ConnectionCredentials credentials)
        {
            if (whisperClients.Count() != whisperClientLimiter)
            {
                if (!whisperClientAlreadyExists(credentials))
                {
                    TwitchWhisperClient newClient = new TwitchWhisperClient(credentials);
                    newClient.NewWhisper += new EventHandler<TwitchWhisperClient.NewWhisperReceivedArgs>(onNewWhisperMessage);
                    newClient.connect();
                    whisperClients.Add(newClient);
                    return new CallResponse(true, newClient);
                } else { return new CallResponse(false, "Twitch username already connected to group chat(whisper) server!"); }
            } else { return new CallResponse(false, "Whisper client limit hit."); }
        }

        public CallResponse searchForChatClientByChannel(string channel)
        {
            foreach (TwitchChatClient client in chatClients)
                if (client.Channel == channel.ToLower())
                    return new CallResponse(true, client);
            return new CallResponse(false, String.Format("Could not find a chat client connected to channel '{0}'", channel));
        }

        public CallResponse searchForChatClientByUsername(string username)
        {
            foreach (TwitchChatClient client in chatClients)
                if (client.TwitchUsername == username.ToLower())
                    return new CallResponse(true, client);
            return new CallResponse(false, String.Format("Could not find a chat client under username '{0}'", username));
        }

        public CallResponse searchForWhisperClient(string username)
        {
            foreach (TwitchWhisperClient client in whisperClients)
                if (client.TwitchUsername == username.ToLower())
                    return new CallResponse(true, client);
            return new CallResponse(false, String.Format("Could not find a whisper client under username ''", username));
        }

        public TwitchChatClient getChatClientAt(int index)
        {
            if (index < chatClients.Count())
                return chatClients[index];
            return null;
        }

        public TwitchWhisperClient getWhisperClientAt(int index)
        {
            if (index < whisperClients.Count())
                return whisperClients[index];
            return null;
        }

        public CallResponse removeChatClient(string username)
        {
            int i = 0;
            int foundIndex = -1;
            foreach (TwitchChatClient client in chatClients)
            {
                if (client.TwitchUsername == username.ToLower())
                {
                    client.disconnect();
                    foundIndex = i;
                }
                i += 1;
            }
            if (foundIndex != -1)
            {
                chatClients.RemoveAt(foundIndex);
                return new CallResponse(true);
            }
            else
            {
                return new CallResponse(false, String.Format("Could not find a chat client under username '{0}'", username));
            }
                
        }

        public CallResponse removeWhisperClient(string username)
        {
            int i = 0;
            int foundIndex = -1;
            foreach (TwitchWhisperClient client in whisperClients)
            {
                if (client.TwitchUsername == username.ToLower())
                {
                    client.disconnect();
                    foundIndex = i;
                }
                i += 1;
            }
            if (foundIndex != -1)
            {
                whisperClients.RemoveAt(foundIndex);
                return new CallResponse(true);
            }
            else
            {
                return new CallResponse(false, String.Format("Could not find a whisper client under username '{0}'", username));
            }
        }

        public string[] getChatChannels()
        {
            string[] ret = new string[chatClients.Count()];
            for (int i = 0; i < chatClients.Count(); i++)
                ret[i] = chatClients[i].Channel;
            return ret;
        }

        public string[] getChatClientUsernames()
        {
            string[] ret = new string[chatClients.Count()];
            for (int i = 0; i < chatClients.Count(); i++)
                ret[i] = chatClients[i].TwitchUsername;
            return ret;
        }

        public string[] getWhisperClientUsernames()
        {
            string[] ret = new string[whisperClients.Count()];
            for (int i = 0; i < whisperClients.Count(); i++)
                ret[i] = whisperClients[i].TwitchUsername;
            return ret;
        }

        private void onNewChatMessage(object sender, TwitchChatClient.NewChatMessageArgs e)
        {
            if (NewChatMessage != null)
            {
                NewChatMessage(null, new NewChatMessageArgs { ChatMessage = e.ChatMessage });
            }
        }

        private void onNewSubscriber(object sender, TwitchChatClient.NewSubscriberArgs e)
        {
            if (NewSubscriber != null)
            {
                NewSubscriber(null, new NewSubscriberArgs { Subscriber = e.Subscriber });
            }
        }

        private void onChannelAssignedState(object sender, TwitchChatClient.ChannelStateAssignedArgs e)
        {
            if (ChannelAssignedState != null)
            {
                ChannelAssignedState(null, new ChannelStateAssignedArgs { ChannelState = e.ChannelState });
            }
        }

        private void onNewWhisperMessage(object sender, TwitchWhisperClient.NewWhisperReceivedArgs e)
        {
            if (NewWhisperMessage != null)
            {
                NewWhisperMessage(null, new NewWhisperMessageArgs { WhisperMessage = e.WhisperMessage });
            }
        }

        private bool whisperClientAlreadyExists(ConnectionCredentials credentials)
        {
            foreach(TwitchWhisperClient client in whisperClients) {
                if(client.TwitchUsername == credentials.TwitchUsername)
                    return true;
            }
            return false;
        }

        private bool chatClientAlreadyExists(ConnectionCredentials credentials)
        {
            foreach (TwitchChatClient client in chatClients)
            {
                if (client.Channel == credentials.TwitchUsername)
                    return true;
            }
            return false;
        }
    }
}
