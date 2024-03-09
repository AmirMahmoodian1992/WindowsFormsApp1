using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CallerResponse
    {
        [JsonProperty("CallerMO")]
        public string[] CallerMO { get; set; }
    }
}
