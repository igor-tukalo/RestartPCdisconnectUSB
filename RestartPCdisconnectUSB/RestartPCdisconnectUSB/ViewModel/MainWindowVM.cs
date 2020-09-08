using GalaSoft.MvvmLight.Messaging;
using RestartPCdisconnectUSB.Controls;
using RestartPCdisconnectUSB.Interfaces;
using RestartPCdisconnectUSB.Model;
using RestartPCdisconnectUSB.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RestartPCdisconnectUSB.ViewModel
{
    class MainWindowVM : IMainWindowVM, INotifyPropertyChanged
    {
        public ApplicationContext BaseUSB { get; set; }
        SystemUSB systemUSB = new SystemUSB();
        readonly WatchUSB watchUSB = new WatchUSB();
        readonly TimerProgram timerProgram;

        public MainWindowVM()
        {

            HistoryUSBmessage = Properties.Settings.Default.VisibilityUSBhistoryMessage;
            ErrorMessageStatus= Properties.Settings.Default.ErrorMessageStatus;

            Messenger.Default.Register<string>(this, "TimerMinusStatus", (s) =>
            {
                MessageStatus = s;
            });
            
            Messenger.Default.Register<object>(this, "CheckEnableUSB", (s) =>
            {
                SysUSBDevices = new ObservableCollection<USBdevice>(systemUSB.CheckEnableUSB(BaseUSB,WatchUSBDevices));
            });

            SysUSBDevices = new ObservableCollection<USBdevice>();
            BaseUSB = new ApplicationContext();

            timerProgram = new TimerProgram(SysUSBDevices, systemUSB)
            {
                EventStartStopTimer = true
            };

            //Запуск программы автоматически
            CheckStartStopApp = true;
            StartStopApp(CheckStartStopApp);

            //Заполняем список устройств usb под наблюдением
            WatchUSBDevices = new ObservableCollection<USBdevice>(BaseUSB.USBdevices);

        }

        private string _MessageStatus;
        public string MessageStatus
        {
            get { return _MessageStatus; }
            set
            {
                _MessageStatus = value;
                OnPropertyChanged("MessageStatus");
            }
        }

        private Visibility _HistoryUSBmessage;
        public Visibility HistoryUSBmessage
        {
            get { return _HistoryUSBmessage; }
            set
            {
                _HistoryUSBmessage = value;
                OnPropertyChanged("HistoryUSBmessage");
            }
        }

        private string _ErrorMessageStatus;
        public string ErrorMessageStatus
        {
            get { return _ErrorMessageStatus; }
            set
            {
                _ErrorMessageStatus = value;
                OnPropertyChanged("ErrorMessageStatus");
            }
        }

        private ObservableCollection<USBdevice> _SysUSBDevices;
        public ObservableCollection<USBdevice> SysUSBDevices
        {
            get { return _SysUSBDevices; }
            set
            {
                _SysUSBDevices = value;
                OnPropertyChanged("SysUSBDevices");
            }
        }

        private ObservableCollection<USBdevice> _WatchUSBDevices;
        public ObservableCollection<USBdevice> WatchUSBDevices
        {
            get { return _WatchUSBDevices; }
            set
            {
                _WatchUSBDevices = value;
                OnPropertyChanged("WatchUSBDevices");
            }
        }

        private bool _CheckStartStopApp { get; set; }
        public bool CheckStartStopApp
        {
            get { return _CheckStartStopApp; }
            set
            {
                _CheckStartStopApp = value;
                OnPropertyChanged("CheckStartStopApp");
            }
        }

        private USBdevice _SelectedDeviceUSB;
        public USBdevice SelectedDeviceUSB
        {
            get { return _SelectedDeviceUSB; }
            set
            {
                _SelectedDeviceUSB = value;
                OnPropertyChanged("SelectedDeviceUSB");
            }
        }

        private USBdevice _SelectedWatchDeviceUSB;
        public USBdevice SelectedWatchDeviceUSB
        {
            get { return _SelectedWatchDeviceUSB; }
            set
            {
                _SelectedWatchDeviceUSB = value;
                OnPropertyChanged("SelectedWatchDeviceUSB");
            }
        }

        private RelayCommand _StartStopAppCommand;
        public RelayCommand StartStopAppCommand
        {
            get
            {
                return _StartStopAppCommand ??
                  (_StartStopAppCommand = new RelayCommand(obj =>
                  {
                      StartStopApp(CheckStartStopApp);
                  }));
            }
        }

        private RelayCommand _WatchDevice;
        public RelayCommand WatchDevice
        {
            get
            {
                return _WatchDevice ??
                  (_WatchDevice = new RelayCommand(obj =>
                  {
                      watchUSB.WatchDevice(SelectedDeviceUSB, BaseUSB);
                      WatchUSBDevices = new ObservableCollection<USBdevice>(BaseUSB.USBdevices);
                  }));
            }
        }

        private RelayCommand _WatchDeviceCancel;
        public RelayCommand WatchDeviceCancel
        {
            get
            {
                return _WatchDeviceCancel ??
                  (_WatchDeviceCancel = new RelayCommand(obj =>
                  {
                      watchUSB.WatchDeviceCancel(SelectedWatchDeviceUSB, BaseUSB);
                      WatchUSBDevices = new ObservableCollection<USBdevice>(BaseUSB.USBdevices);
                  }));
            }
        }

        private RelayCommand _HistoryUSBdisable;
        public RelayCommand HistoryUSBdisable
        {
            get
            {
                return _HistoryUSBdisable ??
                  (_HistoryUSBdisable = new RelayCommand(obj =>
                  {
                      HistoryUSBwindow historyUSBwindow = new HistoryUSBwindow();
                      historyUSBwindow.ShowDialog();

                      HistoryUSBmessage = Properties.Settings.Default.VisibilityUSBhistoryMessage;
                      ErrorMessageStatus = Properties.Settings.Default.ErrorMessageStatus;
                      Properties.Settings.Default.Save();
                  }));
            }
        }

        private RelayCommand _AboutProgramm;
        public RelayCommand AboutProgramm
        {
            get
            {
                return _AboutProgramm ??
                  (_AboutProgramm = new RelayCommand(obj =>
                  {
                      AboutProgrammWindow aboutProgrammWindow = new AboutProgrammWindow();
                      aboutProgrammWindow.ShowDialog();
                  }));
            }
        }

        public void StartStopApp(bool checkStartStopApp)
        {
            if (checkStartStopApp)
            {
                timerProgram.StartTimmer(timerProgram.TimerPlus, timerProgram.EventTimerPlus, timerProgram.TimerIntervalPlus, timerProgram.EventStartStopTimer,SysUSBDevices);
                timerProgram.StartTimmer(timerProgram.TimerMinus, timerProgram.EventTimerMinus, timerProgram.TimerIntervalMinus, timerProgram.EventStartStopTimer, SysUSBDevices);
                timerProgram.EventStartStopTimer = true;
            }
            else
            {
                timerProgram.StopTimmer(timerProgram.TimerPlus);
                timerProgram.StopTimmer(timerProgram.TimerMinus);
                timerProgram.EventStartStopTimer = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}