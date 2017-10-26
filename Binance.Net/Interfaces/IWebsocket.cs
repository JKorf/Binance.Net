using Binance.Net.Events;
using System;
using System.Security.Authentication;
using WebSocketSharp;

namespace Binance.Net.Interfaces
{
    public interface IWebsocket
    {
        void SetEnabledSslProtocols(SslProtocols protocols);

        event EventHandler<ClosedEventArgs> OnClose;
        event EventHandler<MessagedEventArgs> OnMessage;
        event EventHandler<ErroredEventArgs> OnError;
        event EventHandler OnOpen;

        void Connect();
        void Close();
    }
}
