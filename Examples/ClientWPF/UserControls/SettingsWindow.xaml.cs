using System.Windows;

namespace Binance.Net.ClientWPF.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(MainViewModel datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
        }
    }
}
