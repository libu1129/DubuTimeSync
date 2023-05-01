using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubuTimeSync
{
    internal class Setting
    {
        public List<string> ntp_addresses { get; set; } = new List<string>()
        {
        };
        public string interval { get; set; } = "1m";
        public bool hide_on_startup { get; set; } = false;

        public Setting()
        {

        }

        public void load_default()
        {
            //https://gist.github.com/mutin-sa/eea1c396b1e610a2da1e5550d94b0453
            ntp_addresses = new List<string>() {
                "time.google.com",
                "time.windows.com",
                "time.bora.net",
                "kr.pool.ntp.org",
                "time.facebook.com",
                "time.apple.com",
            };

            interval = "1m";
        }
    }
}
