using RestartPCdisconnectUSB.Interfaces;
using RestartPCdisconnectUSB.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace RestartPCdisconnectUSB.Controls
{
    /// <summary>
    /// Управление USB под наблюдением
    /// </summary>
    class WatchUSB : IWatchUSB
    {
        public ObservableCollection<USBdevice> GetWatchUSB(IEnumerable<USBdevice> devicesWatchUSB)
        {
            ObservableCollection<USBdevice> WatchUSBDevices = new ObservableCollection<USBdevice>();
            foreach (var device in devicesWatchUSB)
            {
                WatchUSBDevices.Add(device);
            }
            return WatchUSBDevices;
        }

        public void WatchDevice(USBdevice selectedDeviceUSB, ApplicationContext baseUSB)
        {
            if (selectedDeviceUSB != null)
            {
                baseUSB.USBdevices.Add(new USBdevice()
                {
                    DeviceID = selectedDeviceUSB.DeviceID,
                    Description = selectedDeviceUSB.Description
                });
                baseUSB.SaveChanges();
            }
            else { MessageBox.Show("Выберите USB под наблюдение!"); }
        }

        public void WatchDeviceCancel(USBdevice selectedDeviceUSB, ApplicationContext baseUSB)
        {
            if (selectedDeviceUSB != null)
            {
                baseUSB.USBdevices.Remove(selectedDeviceUSB);
                baseUSB.SaveChanges();
            }
            else { MessageBox.Show("Выберите USB для снятия наблюдения!"); }
        }
    }
}
