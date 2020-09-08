using RestartPCdisconnectUSB.ViewModel;
using System.Windows;

namespace RestartPCdisconnectUSB.View
{
    /// <summary>
    /// Логика взаимодействия для HistoryUSBwindow.xaml
    /// </summary>
    public partial class HistoryUSBwindow : Window
    {
        HistoryUSBwindowVM historyUSBwindow = new HistoryUSBwindowVM();
        public HistoryUSBwindow()
        {
            InitializeComponent();
            DataContext = historyUSBwindow;
        }
    }
}
