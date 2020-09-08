using RestartPCdisconnectUSB.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestartPCdisconnectUSB.Interfaces
{
    interface IWatchUSB
    {
        /// <summary>
        /// Получить список USB под наблюдением
        /// </summary>
        ObservableCollection<USBdevice> GetWatchUSB(IEnumerable<USBdevice> devicesWatchUSB);

        /// <summary>
        /// Взять USB под наблюдение
        /// </summary>
        /// <param name="selectedDeviceUSB">USB для наблюдения</param>
        /// <param name="baseUSB">Контекст БД</param>
        void WatchDevice(USBdevice selectedDeviceUSB, ApplicationContext baseUSB);
        /// <summary>
        /// Снять USB c наблюдения
        /// </summary>
        /// <param name="selectedDeviceUSB">USB для наблюдения</param>
        /// <param name="baseUSB">Контекст БД</param>
        void WatchDeviceCancel(USBdevice selectedDeviceUSB, ApplicationContext baseUSB);
    }
}
