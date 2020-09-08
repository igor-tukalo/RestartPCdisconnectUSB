using RestartPCdisconnectUSB.Controls;
using RestartPCdisconnectUSB.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestartPCdisconnectUSB.Model
{
    interface IMainWindowVM
    {
        /// <summary>
        /// Подключение к БД
        /// </summary>
        ApplicationContext BaseUSB { get; set; }

        /// <summary>
        /// Признак старта/остановки программы (таймеров)
        /// </summary>
        bool CheckStartStopApp { get; set; }

        /// <summary>
        /// Выбраное USB устройство в системе
        /// </summary>
        USBdevice SelectedDeviceUSB { get; set; }

        /// <summary>
        /// Выбраное USB устройство под наблюдением
        /// </summary>
        USBdevice SelectedWatchDeviceUSB { get; set; }

        /// <summary>
        /// Начать/остановить работу программы (таймеров)
        /// </summary>
        RelayCommand StartStopAppCommand { get; }

        /// <summary>
        /// Наблюдать за устройством
        /// </summary>
        RelayCommand WatchDevice { get; }

        /// <summary>
        /// Убрать устройство из наблюдения
        /// </summary>
        RelayCommand WatchDeviceCancel { get; }

        /// <summary>
        /// История перезагруки ПК после отключения USB
        /// </summary>
        RelayCommand HistoryUSBdisable { get; }

        /// <summary>
        /// О программе
        /// </summary>
        RelayCommand AboutProgramm { get; }

        /// <summary>
        /// Список USB устройств в системе
        /// </summary>
        ObservableCollection<USBdevice> SysUSBDevices { get; set; }

        /// <summary>
        /// Список USB устройств под наблюдением
        /// </summary>
        ObservableCollection<USBdevice> WatchUSBDevices { get; set; }

        /// <summary>
        /// Сообщение о работе таймера
        /// </summary>
        string MessageStatus { get; set; }

        /// <summary>
        /// Сообщение о непросмотреном сообщении перезагрузки сервера
        /// </summary>
        string ErrorMessageStatus { get; set; }

        /// <summary>
        /// Видимость сообщения о непросмотреном сообщении перезагрузки сервера
        /// </summary>
        Visibility HistoryUSBmessage { get; set; }
        

        /// <summary>
        /// Начать/остановить работу программы (таймеров)
        /// </summary>
        /// <param name="checkStartStopApp">true - запустить, false - остановить</param>
        void StartStopApp(bool checkStartStopApp);
    }
}
