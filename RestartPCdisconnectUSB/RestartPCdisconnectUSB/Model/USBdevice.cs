using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartPCdisconnectUSB.Model
{
    public class USBdevice
    {
        public int ID { get; set; }
        public string PID { get; set; }
        public string VID { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceID { get; set; }
        public string Description { get; set; }
    }
}
