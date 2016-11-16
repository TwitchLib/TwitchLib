using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.PubSub.Responses.Messages;

namespace TwitchLib.Events.PubSub
{
    public class OnWhisperArgs
    {
        public Whisper Whisper;
        public OnWhisperArgs()
        {
            
        }
    }
}
