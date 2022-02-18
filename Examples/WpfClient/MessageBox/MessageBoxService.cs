using System.Windows;

namespace WpfClient.MessageBox
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton messageButtons, MessageBoxImage messageIcon)
        {
            return System.Windows.MessageBox.Show(text, caption, messageButtons, messageIcon);
        }
    }
}
