using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Sockets;
using System;
using System.Threading.Tasks;

namespace Serverless.FunctionApp
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
        private IBinanceClient _binanceClient;
        private UpdateSubscription _subscription;

        public BinanceStreamKlineData LastKline { get; private set; }
        public Action<BinanceStreamKlineData> OnKlineData { get; set; }
       
        public BinanceDataProvider(IBinanceSocketClient socketClient, IBinanceClient binanceClient)
        {
            _socketClient = socketClient;
            _binanceClient = binanceClient;
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
