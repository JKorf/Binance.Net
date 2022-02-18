using System.Windows;

namespace WpfClient.MessageBox
{
    public interface IMessageBoxService
    {
        MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton messageButtons, MessageBoxImage messageIcon);
    }
}
