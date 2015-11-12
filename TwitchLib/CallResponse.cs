using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    class CallResponse
    {
        private bool response;
        private object satteliteData;

        public bool Response { get { return response; } }
        public object SatteliteData { get { return satteliteData; } }

        public CallResponse(bool response, object satteliteData = null)
        {
            this.response = response;
            this.satteliteData = satteliteData;
        }
    }
}
