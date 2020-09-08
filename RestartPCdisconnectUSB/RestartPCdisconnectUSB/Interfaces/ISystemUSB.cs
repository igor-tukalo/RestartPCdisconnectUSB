using RestartPCdisconnectUSB.Model;
using System.Collections.ObjectModel;

namespace RestartPCdisconnectUSB.Interfaces
{
    interface ISystemUSB
    {
        /// <summary>
        /// Проверка наблюдаемых устройств USB и получение списка USB в системе
        /// </summary>
        /// <param name="baseUSB">Контекст БД</param>
        /// <param name="watchUSBdevices">USB под наблюдением</param>
        /// <returns></returns>
        ObservableCollection<USBdevice> CheckEnableUSB(ApplicationContext baseUSB, ObservableCollection<USBdevice> watchUSBdevices);
    }
}
