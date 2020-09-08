using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartPCdisconnectUSB.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<USBdevice> USBdevices { get; set; }
        public DbSet<USBhistory> USBhistories { get; set; }
    }
}
