using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Channels
{
    public class RunCommercialRequest : RequestModel
    {
        public int Length { get; set; }
    }
}
