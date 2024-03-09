using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class LoginResponse
    {
        public bool succeed { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string token { get; set; }
            public string sth { get; set; }
        }
    }
}
