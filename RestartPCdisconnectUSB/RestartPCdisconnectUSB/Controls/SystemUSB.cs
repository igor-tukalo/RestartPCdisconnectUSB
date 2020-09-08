using RestartPCdisconnectUSB.Interfaces;
using RestartPCdisconnectUSB.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Windows;

namespace RestartPCdisconnectUSB.Controls
{
    /// <summary>
    /// Управление USB в системе
    /// </summary>
    class SystemUSB : ISystemUSB
    {
        public ObservableCollection<USBdevice> CheckEnableUSB(ApplicationContext baseUSB, ObservableCollection<USBdevice> watchUSBdevices)
        {
            ObservableCollection<USBdevice> sysUSBdevices = new ObservableCollection<USBdevice>();

            ManagementObjectSearcher device_searcher = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub");
            foreach (ManagementObject usb_device in device_searcher.Get())
            {
                sysUSBdevices.Add(new USBdevice()
                {
                    DeviceID = usb_device.Properties["DeviceID"].Value.ToString(),
                    Description = usb_device.Properties["Description"].Value.ToString()
                });
            }

            //Перезагрузка ПК если USB не найдено
            //halt(true, false) //мягкая перезагрузка
            //halt(true, true) //жесткая перезагрузка
            //halt(false, false) //мягкое выключение
            //halt(false, true) //жесткое выключение

            WatchUSB watchUSB = new WatchUSB();
            watchUSBdevices = new ObservableCollection<USBdevice>(baseUSB.USBdevices);

            foreach (var device in watchUSBdevices)
            {
                var deviceSys = sysUSBdevices.Where(x => x.DeviceID == device.DeviceID && x.Description == device.Description).FirstOrDefault();
                if (deviceSys == null)
                {
                    // Записать событие в историю USB
                    baseUSB.USBhistories.Add(new USBhistory()
                    {
                        DeviceID = device.DeviceID,
                        Description = device.Description,
                        DateReboot = DateTime.Now
                    }); ;
                    baseUSB.SaveChanges();

                    Properties.Settings.Default.VisibilityUSBhistoryMessage = Visibility.Visible;
                    Properties.Settings.Default.ErrorMessageStatus = "Посмотрите событие перезагрузки ПК после отключения USB! "+ DateTime.Now;
                    Properties.Settings.Default.Save();

                    PowerPCManager powerPCManager = new PowerPCManager();
                    powerPCManager.Halt(true, true); //жесткая перезагрузка
                    break;
                }
            }

            return sysUSBdevices;
        }
    }
}
