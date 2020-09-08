using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartPCdisconnectUSB.Model
{
    public class USBhistory
    {
        public int ID { get; set; }
        public string DeviceID { get; set; }
        public string Description { get; set; }
        public DateTime DateReboot { get; set; }
    }
}
