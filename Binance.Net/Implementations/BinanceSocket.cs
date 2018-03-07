using Binance.Net.Interfaces;
using SuperSocket.ClientEngine;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Binance.Net.Implementations
{
    public class BinanceSocket : IWebsocket
    {
        private readonly WebSocket socket;

        public event EventHandler OnClose
        {
            add => socket.Closed += value;
            remove => socket.Closed -= value;
        }
        public event EventHandler<MessageReceivedEventArgs> OnMessage
        {
            add => socket.MessageReceived += value;
            remove => socket.MessageReceived -= value;
        }
        public event EventHandler<ErrorEventArgs> OnError
        {
            add => socket.Error += value;
            remove => socket.Error -= value;
        }
        public event EventHandler OnOpen
        {
            add => socket.Opened += value;
            remove => socket.Opened -= value;
        }

        public BinanceSocket(WebSocket socket)
        {
            this.socket = socket;
        }

        public void Close()
        {
            socket.Close();
        }

        public async Task<bool> Connect()
        {
            return await socket.OpenAsync().ConfigureAwait(false);
        }

        public void SetEnabledSslProtocols(SslProtocols protocols)
        {
            socket.Security.EnabledSslProtocols = protocols;
        }
    }
}
