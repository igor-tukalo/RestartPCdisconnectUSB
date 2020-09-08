using GalaSoft.MvvmLight.Messaging;
using RestartPCdisconnectUSB.Interfaces;
using RestartPCdisconnectUSB.Model;
using System;
using System.Collections.ObjectModel;
using System.Timers;

namespace RestartPCdisconnectUSB.Controls
{
    /// <summary>
    /// Управление таймерами программы
    /// </summary>
    class TimerProgram : ITimerProgram
    {
        /// <summary>
        /// Управление таймерами программы
        /// </summary>
        /// <param name="sysUSB">USB в системе</param>
        /// <param name="systemUSB">Экземляр класса USB в системе</param>
        public TimerProgram(ObservableCollection<USBdevice> sysUSB, SystemUSB systemUSB)
        {
            //Задаем интервалы таймеров
            TimerIntervalPlus = 20000;
            TimerIntervalMinus = 1000;
            TimerIntervalPlusCountDown = TimerIntervalPlus; //Обратный отсчет от интервала последовательного таймера

            //Создаём таймеры, которые будут срабатывать через указаное кол-во секунд
            TimerPlus = new Timer(TimerIntervalPlus);
            TimerMinus = new Timer(TimerIntervalMinus);
        }

        public Timer TimerPlus { get; set; }
        public Timer TimerMinus { get; set; }
        public int TimerIntervalPlus { get; set; }
        public int TimerIntervalMinus { get; set; }
        public int TimerIntervalPlusCountDown { get; set; }
        public bool EventStartStopTimer { get; set; }

        public void StartTimmer(Timer timer, ElapsedEventHandler elapsedEventHandler, int intervalTimer, bool eventStartStopTimer, ObservableCollection<USBdevice> sysUSBdevices)
        {
            //Назначаем обработчик, который будет срабатывать по таймауту
            if (eventStartStopTimer)
            {
                sysUSBdevices = new ObservableCollection<USBdevice>();
                timer.Elapsed += elapsedEventHandler;
            }
            TimerIntervalPlusCountDown += 1000;
            //Таймер будет постоянно работать
            timer.AutoReset = true;
            timer.Start();
        }

        public void StopTimmer(Timer timer)
        {
            timer.Stop();
        }

        /// <summary>
        /// Обработчик метода завершения работы таймера TimerPlus - служит для выполнения проверки доступности USB
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EventTimerPlus(Object source, ElapsedEventArgs e)
        {
            Messenger.Default.Send<object>(this, "CheckEnableUSB");
            TimerIntervalPlusCountDown = TimerIntervalPlus;
        }
        /// <summary>
        /// Обработчик метода завершения работы таймера TimerMinus - служит для отсчета времени выполнения TimerPlus
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EventTimerMinus(Object source, ElapsedEventArgs e)
        {
            TimerIntervalPlusCountDown -= 1000;
            string TimerMinusStatus = "Обновление списка USB устройств через " + (TimerIntervalPlusCountDown / 1000) + " cекунд.";
            Messenger.Default.Send<string>(TimerMinusStatus, "TimerMinusStatus");
        }
    }
}
