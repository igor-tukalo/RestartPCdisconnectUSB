using RestartPCdisconnectUSB.Model;
using System.Collections.ObjectModel;
using System.Timers;

namespace RestartPCdisconnectUSB.Interfaces
{
    interface ITimerProgram
    {
        /// <summary>
        /// Таймер последовательного отсчета
        /// </summary>
        Timer TimerPlus { get; set; }
        /// <summary>
        /// Таймер обратного отсчета
        /// </summary>
        Timer TimerMinus { get; set; }
        /// <summary>
        /// Интервал для таймера последовательного отсчета
        /// </summary>
        int TimerIntervalPlus { get; set; }
        /// <summary>
        /// Интервал после которого срабатывает обработчик таймера обратного отсчета
        /// </summary>
        int TimerIntervalMinus { get; set; }
        /// <summary>
        /// Интервал для таймера обратного отсчета
        /// </summary>
        int TimerIntervalPlusCountDown { get; set; }
        /// <summary>
        /// Запустить таймеры
        /// </summary>
        /// <param name="intervalTimer">Интервал таймера</param>
        /// <param name="timer">Таймер который нужно остановить</param>
        /// <param name="elapsedEventHandler">Метод который выполнится по истечении времени</param>
        /// <param name="sysUSBdevices">USB устройства в системе</param>
        void StartTimmer(Timer timer, ElapsedEventHandler elapsedEventHandler, int intervalTimer, bool eventStartStopTimer, ObservableCollection<USBdevice> sysUSBdevices);
        /// <summary>
        /// Остановить таймеры
        /// </summary>
        /// <param name="timer">Таймер который нужно остановить</param>
        void StopTimmer(Timer timer);
        /// <summary>
        /// Признак выполнения вызова события после интервала таймера
        /// </summary>
        bool EventStartStopTimer {get;set;}
    }
}
