using System;
using System.Security.Authentication;
using WebSocketSharp;

namespace Binance.Net.Interfaces
{
    public interface IWebsocket
    {
        void SetEnabledSslProtocols(SslProtocols protocols);

        void OnClose(EventHandler<CloseEventArgs> onClose);
        void OnMessage(EventHandler<MessageEventArgs> onMessage);
        void OnError(EventHandler<ErrorEventArgs> onError);
        void OnOpen(EventHandler onOpen);

        void Connect();
        void Close();
    }
}
