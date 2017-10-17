using System.Windows;

namespace Binance.Net.ClientWPF.MVVM
{
    public interface IMessageBoxService
    {
        MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton messageButtons, MessageBoxImage messageIcon);
    }
}
