using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface IBinanceDataProvider
    {
        BinanceStreamKlineData LastKline { get; }
        Action<BinanceStreamKlineData> OnKlineData { get; set; }

        Task Start();
        Task Stop();
    }

    public class BinanceDataProvider: IBinanceDataProvider
    {
        private IBinanceSocketClient _socketClient;
        private UpdateSubscription _subscription;

        public BinanceStreamKlineData LastKline { get; private set; }
        public Action<BinanceStreamKlineData> OnKlineData { get; set; }
       
        public BinanceDataProvider(IBinanceSocketClient socketClient)
        {
            _socketClient = socketClient;

            Start().Wait(); // Probably want to do this in some initialization step at application startup
        }

        public async Task Start()
        {
            var subResult = await _socketClient.SubscribeToKlineUpdatesAsync("BTCUSDT", KlineInterval.FifteenMinutes, data =>
            {
                LastKline = data;
                OnKlineData?.Invoke(data);
            });
            if (subResult.Success)            
                _subscription = subResult.Data;            
        }

        public async Task Stop()
        {
            await _socketClient.Unsubscribe(_subscription);
        }
    }
}
