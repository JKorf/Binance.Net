using Binance.Net.Events;
using System;
using System.Security.Authentication;
using System.Security.Policy;

namespace Binance.Net.Interfaces
{
    public interface IWebsocket
    {
        string Url { get; }
        void SetEnabledSslProtocols(SslProtocols protocols);

        event EventHandler<ClosedEventArgs> OnClose;
        event EventHandler<MessagedEventArgs> OnMessage;
        event EventHandler<ErroredEventArgs> OnError;
        event EventHandler OnOpen;

        void Connect();
        void Close();
    }
}
