using Binance.Net.Interfaces;
using System;
using System.Security.Authentication;
using WebSocketSharp;

namespace Binance.Net.Implementations
{
    public class BinanceSocket : IWebsocket
    {
        private WebSocket socket;

        public BinanceSocket(WebSocket socket)
        {
            this.socket = socket;
        }

        public void OnClose(EventHandler<CloseEventArgs> args)
        {
            socket.OnClose += args;
        }
        public void OnMessage(EventHandler<MessageEventArgs> args)
        {
            socket.OnMessage += args;
        }
        public void OnError(EventHandler<ErrorEventArgs> args)
        {
            socket.OnError += args;
        }
        public void OnOpen(EventHandler args)
        {
            socket.OnOpen += args;
        }

        public void Close()
        {
            socket.Close();
        }

        public void Connect()
        {
            socket.Connect();
        }

        public void SetEnabledSslProtocols(SslProtocols protocols)
        {
            socket.SslConfiguration.EnabledSslProtocols = protocols;
        }
    }
}
