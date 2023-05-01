using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubuTimeSync
{
    internal class Setting
    {
        public List<string> ntp_addresses { get; set; } = new List<string>();
        public string interval { get; set; } = "1m";
        public bool hide_on_startup { get; set; } = false;
    }
}
