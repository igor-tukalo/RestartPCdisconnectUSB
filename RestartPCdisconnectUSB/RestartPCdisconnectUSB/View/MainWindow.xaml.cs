using RestartPCdisconnectUSB.ViewModel;
using System.Windows;

namespace RestartPCdisconnectUSB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM mainWindowVM = new MainWindowVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainWindowVM;
        }
    }
}
