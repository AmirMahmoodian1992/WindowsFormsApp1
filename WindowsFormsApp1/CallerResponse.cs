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

    public class CallerDataItem
    {
        public string Label
        {
            get; set;
        }
        public string Text
        {
            get; set;
        }
        public string Id
        {
            get; set;
        }
        public string CustomScript
        {
            get; set;
        }

    }

    public class CallerData
    {
        public CallerData()
        {
            Items = new List<CallerDataItem>();
        }
        //		icon
        //		public string Receiver
        //		{
        //			get; set;
        //		}
        public string Label
        {
            get; set;
        }
        public string Text
        {
            get; set;
        }
        public string Id
        {
            get; set;
        }
        public string Number
        {
            get; set;
        }
        public List<CallerDataItem> Items
        {
            get; set;
        }
        public string CustomScript
        {
            get; set;
        }
        public string Receiver
        {
            get; set;
        }

    }
    public class InfoData
    {
        public string CallerJson
        {
            get; set;
        }
    }
}
