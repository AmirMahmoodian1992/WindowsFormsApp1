using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPWindowsAgent
{
    public class CallerResponse
    {
        [JsonProperty("CallerMO")]
        public string[] CallerMO { get; set; }
    }
}
