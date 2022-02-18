using System.Windows;
using WpfClient.ViewModels;

namespace WpfClient.UserControls
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
