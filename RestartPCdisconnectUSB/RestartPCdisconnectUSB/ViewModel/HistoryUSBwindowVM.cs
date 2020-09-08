using RestartPCdisconnectUSB.Interfaces;
using RestartPCdisconnectUSB.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RestartPCdisconnectUSB.ViewModel
{
    /// <summary>
    /// История отключений USB
    /// </summary>
    class HistoryUSBwindowVM : INotifyPropertyChanged, IHistoryUSBwindowVM
    {
        ApplicationContext BaseUSB { get; set; }
        public HistoryUSBwindowVM()
        {
            Properties.Settings.Default.VisibilityUSBhistoryMessage = Visibility.Hidden;
            Properties.Settings.Default.ErrorMessageStatus = string.Empty;
            Properties.Settings.Default.Save();

            BaseUSB = new ApplicationContext();
            HistoryUSBerrors = new ObservableCollection<USBhistory>(BaseUSB.USBhistories);
        }

        private ObservableCollection<USBhistory> _HistoryUSBerrors;
        public ObservableCollection<USBhistory> HistoryUSBerrors
        {
            get { return _HistoryUSBerrors; }
            set
            {
                _HistoryUSBerrors = value;
                OnPropertyChanged("HistoryUSBerrors");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
