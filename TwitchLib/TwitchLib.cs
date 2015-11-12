using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class TwitchLib
    {
        TwitchChatClientManager chatManager = new TwitchChatClientManager();
        TwitchWhisperClient whisperClient = new TwitchWhisperClient();

        public TwitchLib()
        {

        }
    }
}
