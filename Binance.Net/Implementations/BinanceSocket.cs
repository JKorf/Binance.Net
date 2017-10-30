using Binance.Net.Events;
using Binance.Net.Interfaces;
using System;
using System.Security.Authentication;
using WebSocketSharp;

namespace Binance.Net.Implementations
{
    public class BinanceSocket : IWebsocket
    {
        private readonly WebSocket socket;

        public event EventHandler<ClosedEventArgs> OnClose
        {
            add { socket.OnClose += (sender, args) => value(sender, new ClosedEventArgs(args.Code, args.Reason, args.WasClean)); }
            remove { }
        }
        public event EventHandler<MessagedEventArgs> OnMessage
        {
            add { socket.OnMessage += (sender, args) => value(sender, new MessagedEventArgs(args.Data, args.IsBinary, args.IsPing, args.IsText, args.RawData)); }
            remove { }
        }
        public event EventHandler<ErroredEventArgs> OnError
        {
            add { socket.OnError += (sender, args) => value(sender, new ErroredEventArgs(args.Exception, args.Message)); }
            remove { }
        }
        public event EventHandler OnOpen
        {
            add { socket.OnOpen += (sender, args) => value(sender, new EventArgs()); }
            remove { }
        }

        public BinanceSocket(WebSocket socket)
        {
            this.socket = socket;
        }

        public void Close()
        {
            socket.Close();
        }

        public void Connect()
        {
            socket.Connect();
        }

        public string Url => socket.Url.ToString();

        public void SetEnabledSslProtocols(SslProtocols protocols)
        {
            socket.SslConfiguration.EnabledSslProtocols = protocols;
        }
    }
}
