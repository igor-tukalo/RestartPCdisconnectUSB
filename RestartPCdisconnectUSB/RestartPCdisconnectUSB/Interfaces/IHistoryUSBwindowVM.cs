using RestartPCdisconnectUSB.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestartPCdisconnectUSB.Interfaces
{
    interface IHistoryUSBwindowVM
    {
        /// <summary>
        /// История отключений USB
        /// </summary>
        ObservableCollection<USBhistory> HistoryUSBerrors { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string prop = "");
    }
}