﻿using System;
using System.IO;
using System.Net.WebSockets;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.UnitTests.TestImplementations
{
    public class TestSocket: IWebsocket
    {
        public bool CanConnect { get; set; } = true;
        public bool Connected { get; set; }

        public event Func<Task> OnClose;
#pragma warning disable 0067
        public event Func<Task> OnReconnected;
        public event Func<Task> OnReconnecting;
        public event Func<int, Task> OnRequestRateLimited;
        public event Func<Exception, Task> OnError;
#pragma warning restore 0067
        public event Func<int, Task> OnRequestSent;
        public event Action<WebSocketMessageType, ReadOnlyMemory<byte>> OnStreamMessage;
        public event Func<Task> OnOpen;

        public int Id { get; }
        public bool ShouldReconnect { get; set; }
        public Func<string, string> DataInterpreterString { get; set; }
        public Func<byte[], string> DataInterpreterBytes { get; set; }
        public DateTime? DisconnectTime { get; set; }
        public string Url { get; }
        public bool IsClosed => !Connected;
        public bool IsOpen => Connected;
        public bool PingConnection { get; set; }
        public TimeSpan PingInterval { get; set; }
        public SslProtocols SSLProtocols { get; set; }
        public TimeSpan Timeout { get; set; }
        public bool Reconnecting { get; set; }
        public string Origin { get; set; }
        public Encoding Encoding { get; set; }
        public int? RatelimitPerSecond { get; set; }
        public double IncomingKbps => 0;

        public Uri Uri => new Uri("wss://test.com/ws");

        public TimeSpan KeepAliveInterval { get; set; }
        public Func<Task<Uri>> GetReconnectionUrl { get; set; }

        public Task<CallResult> ConnectAsync()
        {
            Connected = CanConnect;
            return Task.FromResult(CanConnect ? new CallResult(null) : new CallResult(new CantConnectError()));
        }

        public void Send(int requestId, string data, int weight)
        {
            if(!Connected)
                throw new Exception("Socket not connected");

            OnRequestSent?.Invoke(requestId);
        }

        public void Reset()
        {
            
        }

        public Task CloseAsync()
        {
            Connected = false;
            return Task.FromResult(0);
        }

        public void SetProxy(string host, int port)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
        }

        public void InvokeClose()
        {
            Connected = false;
            OnClose?.Invoke();
        }

        public void InvokeOpen()
        {
            OnOpen?.Invoke();
        }

        public void InvokeMessage(string data)
        {
            OnStreamMessage?.Invoke(WebSocketMessageType.Text, new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(data)));
        }

        public void InvokeMessage<T>(T data)
        {
            OnStreamMessage?.Invoke(WebSocketMessageType.Text, new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data))));
        }

        public void SetProxy(ApiProxy proxy)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync()
        {
            while (Connected)
                await Task.Delay(10);
        }

        public Task ReconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
