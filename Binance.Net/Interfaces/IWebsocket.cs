using Binance.Net.Events;
using SuperSocket.ClientEngine;
using System;
using System.Security.Authentication;
using WebSocket4Net;

namespace Binance.Net.Interfaces
{
    public interface IWebsocket
    {
        void SetEnabledSslProtocols(SslProtocols protocols);

        event EventHandler OnClose;
        event EventHandler<MessageReceivedEventArgs> OnMessage;
        event EventHandler<ErrorEventArgs> OnError;
        event EventHandler OnOpen;

        void Connect();
        void Close();
    }
}
